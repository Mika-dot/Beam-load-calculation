using EngineeringCalculator.Materials;

namespace EngineeringCalculator.Trusses;

public sealed record TrussNode(int Id, double X, double Y, bool FixX = false, bool FixY = false);
public sealed record TrussMember(int Id, int StartNodeId, int EndNodeId, double Area, Material Material, string? Name = null);
public sealed record TrussLoad(int NodeId, double Fx, double Fy, string? Name = null);

public sealed record TrussModel
{
    public string Name { get; init; } = "Расчёт фермы";
    public IReadOnlyList<TrussNode> Nodes { get; init; } = [];
    public IReadOnlyList<TrussMember> Members { get; init; } = [];
    public IReadOnlyList<TrussLoad> Loads { get; init; } = [];
}

public sealed record TrussNodeResult(int NodeId, double DisplacementX, double DisplacementY, double ReactionX, double ReactionY);
public sealed record TrussMemberResult(int MemberId, string Name, double AxialForce, double Stress, double Utilization, bool Passes);
public sealed record TrussResult(string Name, IReadOnlyList<TrussNodeResult> Nodes, IReadOnlyList<TrussMemberResult> Members)
{
    public bool Passes => Members.All(x => x.Passes);
    public double MaximumDisplacement => Nodes.Max(x => Math.Sqrt(x.DisplacementX * x.DisplacementX + x.DisplacementY * x.DisplacementY));
}
