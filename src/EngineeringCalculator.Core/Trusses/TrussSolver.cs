using EngineeringCalculator.Common;
using EngineeringCalculator.Mathematics;

namespace EngineeringCalculator.Trusses;

public sealed class TrussSolver
{
    public TrussResult Solve(TrussModel model)
    {
        Validate(model);
        var index = model.Nodes.Select((node, i) => (node.Id, i)).ToDictionary(x => x.Id, x => x.i);
        var size = model.Nodes.Count * 2;
        var stiffness = new double[size, size]; var loads = new double[size];

        foreach (var member in model.Members)
        {
            var a = model.Nodes[index[member.StartNodeId]]; var b = model.Nodes[index[member.EndNodeId]];
            var dx = b.X - a.X; var dy = b.Y - a.Y; var length = Math.Sqrt(dx * dx + dy * dy);
            var c = dx / length; var s = dy / length; var k = member.Material.ElasticModulus * member.Area / length;
            var local = new[,]
            {
                { c*c*k, c*s*k, -c*c*k, -c*s*k }, { c*s*k, s*s*k, -c*s*k, -s*s*k },
                { -c*c*k, -c*s*k, c*c*k, c*s*k }, { -c*s*k, -s*s*k, c*s*k, s*s*k }
            };
            var ai = index[a.Id]; var bi = index[b.Id]; var dofs = new[] { 2*ai, 2*ai+1, 2*bi, 2*bi+1 };
            for (var i = 0; i < 4; i++) for (var j = 0; j < 4; j++) stiffness[dofs[i], dofs[j]] += local[i, j];
        }

        foreach (var load in model.Loads)
        {
            var node = index[load.NodeId]; loads[2 * node] += load.Fx; loads[2 * node + 1] += load.Fy;
        }
        var constrained = new HashSet<int>();
        for (var i = 0; i < model.Nodes.Count; i++)
        {
            if (model.Nodes[i].FixX) constrained.Add(2*i);
            if (model.Nodes[i].FixY) constrained.Add(2*i+1);
        }
        var free = Enumerable.Range(0, size).Where(x => !constrained.Contains(x)).ToArray();
        var reducedK = new double[free.Length, free.Length]; var reducedF = new double[free.Length];
        for (var i = 0; i < free.Length; i++)
        {
            reducedF[i] = loads[free[i]];
            for (var j = 0; j < free.Length; j++) reducedK[i, j] = stiffness[free[i], free[j]];
        }
        var solved = LinearSystem.Solve(reducedK, reducedF); var u = new double[size];
        for (var i = 0; i < free.Length; i++) u[free[i]] = solved[i];
        var reactions = new double[size];
        for (var i = 0; i < size; i++)
        {
            for (var j = 0; j < size; j++) reactions[i] += stiffness[i, j] * u[j];
            reactions[i] -= loads[i];
        }

        var nodeResults = model.Nodes.Select((node, i) => new TrussNodeResult(node.Id, u[2*i], u[2*i+1],
            node.FixX ? reactions[2*i] : 0, node.FixY ? reactions[2*i+1] : 0)).ToArray();
        var memberResults = model.Members.Select(member =>
        {
            var ai = index[member.StartNodeId]; var bi = index[member.EndNodeId]; var a = model.Nodes[ai]; var b = model.Nodes[bi];
            var dx = b.X-a.X; var dy = b.Y-a.Y; var length = Math.Sqrt(dx*dx+dy*dy); var c=dx/length; var s=dy/length;
            var extension = -c*u[2*ai] - s*u[2*ai+1] + c*u[2*bi] + s*u[2*bi+1];
            var axial = member.Material.ElasticModulus * member.Area / length * extension;
            var stress = axial / member.Area; var utilization = Math.Abs(stress) / member.Material.AllowableStress;
            return new TrussMemberResult(member.Id, member.Name ?? $"Стержень {member.Id}", axial, stress, utilization, utilization <= 1);
        }).ToArray();
        return new TrussResult(model.Name, nodeResults, memberResults);
    }

    private static void Validate(TrussModel model)
    {
        ArgumentNullException.ThrowIfNull(model);
        if (model.Nodes.Count < 2) throw new ArgumentException("Ферма должна содержать не менее двух узлов.");
        if (model.Members.Count == 0) throw new ArgumentException("Ферма должна содержать стержни.");
        if (model.Nodes.Select(x => x.Id).Distinct().Count() != model.Nodes.Count) throw new ArgumentException("Идентификаторы узлов должны быть уникальны.");
        var ids = model.Nodes.Select(x => x.Id).ToHashSet();
        foreach (var member in model.Members)
        {
            Guard.Positive(member.Area, nameof(member.Area)); member.Material.Validate();
            if (!ids.Contains(member.StartNodeId) || !ids.Contains(member.EndNodeId) || member.StartNodeId == member.EndNodeId)
                throw new ArgumentException($"Стержень {member.Id} ссылается на некорректные узлы.");
        }
        if (model.Loads.Any(x => !ids.Contains(x.NodeId))) throw new ArgumentException("Нагрузка ссылается на неизвестный узел.");
    }
}
