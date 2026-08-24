using EngineeringCalculator.Mathematics;

namespace EngineeringCalculator.Beams;

public sealed class BeamSolver
{
    private const double PositionTolerance = 1e-8;

    public BeamResult Solve(BeamModel model)
    {
        BeamValidator.Validate(model);
        var positions = BuildMesh(model);
        var size = positions.Length * 2;
        var stiffness = new double[size, size];
        var loads = new double[size];
        var elementLoads = new (double Left, double Right)[positions.Length - 1];

        for (var element = 0; element < positions.Length - 1; element++)
        {
            var x0 = positions[element]; var x1 = positions[element + 1]; var length = x1 - x0;
            var local = ElementStiffness(model.Material.ElasticModulus * model.Section.MomentOfInertia, length);
            var dofs = new[] { 2 * element, 2 * element + 1, 2 * element + 2, 2 * element + 3 };
            Assemble(stiffness, local, dofs);

            var q0 = TotalDownwardIntensity(model, x0);
            var q1 = TotalDownwardIntensity(model, x1);
            elementLoads[element] = (q0, q1);
            var equivalent = EquivalentNodalLoad(-q0, -q1, length);
            for (var i = 0; i < 4; i++) loads[dofs[i]] += equivalent[i];
        }

        foreach (var point in model.PointLoads) loads[2 * FindNode(positions, point.Position)] -= point.Force;
        foreach (var moment in model.Moments) loads[2 * FindNode(positions, moment.Position) + 1] += moment.Moment;

        var constrained = new HashSet<int>();
        foreach (var support in model.Supports)
        {
            var node = FindNode(positions, support.Position);
            if (support.Type is BeamSupportType.Pinned or BeamSupportType.Roller or BeamSupportType.Fixed) constrained.Add(2 * node);
            if (support.Type is BeamSupportType.Fixed) constrained.Add(2 * node + 1);
        }

        var free = Enumerable.Range(0, size).Where(x => !constrained.Contains(x)).ToArray();
        if (free.Length == 0) throw new InvalidOperationException("В расчётной схеме отсутствуют свободные степени свободы.");
        var reducedK = new double[free.Length, free.Length]; var reducedF = new double[free.Length];
        for (var i = 0; i < free.Length; i++)
        {
            reducedF[i] = loads[free[i]];
            for (var j = 0; j < free.Length; j++) reducedK[i, j] = stiffness[free[i], free[j]];
        }
        var reducedU = LinearSystem.Solve(reducedK, reducedF);
        var displacements = new double[size];
        for (var i = 0; i < free.Length; i++) displacements[free[i]] = reducedU[i];

        var reactionsVector = Multiply(stiffness, displacements);
        for (var i = 0; i < size; i++) reactionsVector[i] -= loads[i];
        var reactions = model.Supports.Select((support, index) =>
        {
            var node = FindNode(positions, support.Position);
            return new BeamReaction(support.Position,
                support.Type == BeamSupportType.Free ? 0 : reactionsVector[2 * node],
                support.Type == BeamSupportType.Fixed ? reactionsVector[2 * node + 1] : 0,
                support.Name ?? $"R{index + 1}");
        }).ToArray();

        var samples = BuildSamples(model, positions, displacements, reactions);
        var maxShear = samples.Max(x => Math.Abs(x.Shear));
        var maxMoment = samples.Max(x => Math.Abs(x.Moment));
        var maxDeflection = samples.Max(x => Math.Abs(x.Deflection));
        var maxStress = samples.Max(x => Math.Abs(x.Stress));
        var deflectionLimit = model.Length / model.DeflectionLimitRatio;
        var strengthUtilization = maxStress / model.Material.AllowableStress;
        var deflectionUtilization = maxDeflection / deflectionLimit;

        return new BeamResult
        {
            Name = model.Name, Reactions = reactions, Samples = samples,
            MaximumAbsoluteShear = maxShear, MaximumAbsoluteMoment = maxMoment,
            MaximumAbsoluteDeflection = maxDeflection, MaximumAbsoluteStress = maxStress,
            StrengthUtilization = strengthUtilization, DeflectionUtilization = deflectionUtilization,
            SafetyFactor = maxStress < 1e-12 ? double.PositiveInfinity : model.Material.YieldStrength / maxStress,
            DeflectionLimit = deflectionLimit,
            StrengthPasses = strengthUtilization <= 1, DeflectionPasses = deflectionUtilization <= 1
        };
    }

    private static double[] BuildMesh(BeamModel model)
    {
        var points = new List<double>();
        for (var i = 0; i <= model.MeshElements; i++) points.Add(model.Length * i / model.MeshElements);
        points.AddRange(model.Supports.Select(x => x.Position));
        points.AddRange(model.PointLoads.Select(x => x.Position));
        points.AddRange(model.Moments.Select(x => x.Position));
        points.AddRange(model.DistributedLoads.SelectMany(x => new[] { x.Start, x.End }));
        return points.Order().Aggregate(new List<double>(), (result, value) =>
        {
            if (result.Count == 0 || Math.Abs(result[^1] - value) > PositionTolerance) result.Add(value);
            return result;
        }).ToArray();
    }

    private static int FindNode(double[] positions, double position)
    {
        var index = Array.BinarySearch(positions, position);
        if (index >= 0) return index;
        index = ~index;
        if (index < positions.Length && Math.Abs(positions[index] - position) < PositionTolerance) return index;
        if (index > 0 && Math.Abs(positions[index - 1] - position) < PositionTolerance) return index - 1;
        throw new InvalidOperationException($"Узел в позиции {position} не найден.");
    }

    private static double TotalDownwardIntensity(BeamModel model, double x)
    {
        var total = model.IncludeSelfWeight ? model.Section.Area * model.Material.Density * 9.80665 : 0;
        foreach (var load in model.DistributedLoads)
        {
            if (x < load.Start - PositionTolerance || x > load.End + PositionTolerance) continue;
            var t = (x - load.Start) / (load.End - load.Start);
            total += load.StartIntensity + t * (load.EndIntensity - load.StartIntensity);
        }
        return total;
    }

    private static double[,] ElementStiffness(double ei, double length)
    {
        var l2 = length * length; var l3 = l2 * length; var c = ei / l3;
        return new[,]
        {
            { 12*c, 6*length*c, -12*c, 6*length*c },
            { 6*length*c, 4*l2*c, -6*length*c, 2*l2*c },
            { -12*c, -6*length*c, 12*c, -6*length*c },
            { 6*length*c, 2*l2*c, -6*length*c, 4*l2*c }
        };
    }

    private static double[] EquivalentNodalLoad(double q0, double q1, double length) =>
    [
        length * (7*q0 + 3*q1) / 20,
        length * length * (3*q0 + 2*q1) / 60,
        length * (3*q0 + 7*q1) / 20,
        -length * length * (2*q0 + 3*q1) / 60
    ];

    private static void Assemble(double[,] global, double[,] local, int[] dofs)
    {
        for (var i = 0; i < dofs.Length; i++)
            for (var j = 0; j < dofs.Length; j++) global[dofs[i], dofs[j]] += local[i, j];
    }

    private static double[] Multiply(double[,] matrix, double[] vector)
    {
        var result = new double[vector.Length];
        for (var i = 0; i < vector.Length; i++)
            for (var j = 0; j < vector.Length; j++) result[i] += matrix[i, j] * vector[j];
        return result;
    }

    private static IReadOnlyList<BeamSample> BuildSamples(BeamModel model, double[] nodes, double[] u, IReadOnlyList<BeamReaction> reactions)
    {
        var result = new List<BeamSample>(nodes.Length * 3);
        for (var element = 0; element < nodes.Length - 1; element++)
        {
            var x0 = nodes[element]; var length = nodes[element + 1] - x0;
            for (var part = 0; part < 3; part++)
            {
                if (element > 0 && part == 0) continue;
                var xi = part / 2d; var x = x0 + xi * length;
                var n1 = 1 - 3*xi*xi + 2*xi*xi*xi;
                var n2 = length * (xi - 2*xi*xi + xi*xi*xi);
                var n3 = 3*xi*xi - 2*xi*xi*xi;
                var n4 = length * (-xi*xi + xi*xi*xi);
                var deflection = n1*u[2*element] + n2*u[2*element+1] + n3*u[2*element+2] + n4*u[2*element+3];
                var dn1 = (-6*xi + 6*xi*xi) / length;
                var dn2 = 1 - 4*xi + 3*xi*xi;
                var dn3 = (6*xi - 6*xi*xi) / length;
                var dn4 = -2*xi + 3*xi*xi;
                var rotation = dn1*u[2*element] + dn2*u[2*element+1] + dn3*u[2*element+2] + dn4*u[2*element+3];
                var (shear, moment) = InternalForces(model, reactions, x);
                result.Add(new BeamSample(x, shear, moment, deflection, rotation, moment / model.Section.SectionModulus));
            }
        }
        return result;
    }

    private static (double Shear, double Moment) InternalForces(BeamModel model, IReadOnlyList<BeamReaction> reactions, double x)
    {
        var shear = 0d; var moment = 0d;
        foreach (var reaction in reactions.Where(r => r.Position <= x + PositionTolerance))
        {
            shear += reaction.VerticalForce;
            moment += reaction.VerticalForce * (x - reaction.Position) - reaction.Moment;
        }
        foreach (var load in model.PointLoads.Where(p => p.Position <= x + PositionTolerance))
        {
            shear -= load.Force; moment -= load.Force * (x - load.Position);
        }
        foreach (var applied in model.Moments.Where(m => m.Position <= x + PositionTolerance)) moment -= applied.Moment;
        foreach (var load in model.DistributedLoads)
        {
            var end = Math.Min(x, load.End);
            if (end <= load.Start) continue;
            var covered = end - load.Start; var full = load.End - load.Start;
            var slope = (load.EndIntensity - load.StartIntensity) / full;
            var force = load.StartIntensity * covered + slope * covered * covered / 2;
            var firstMomentAtStart = load.StartIntensity * covered * covered / 2 + slope * covered * covered * covered / 3;
            shear -= force; moment -= force * (x - load.Start) - firstMomentAtStart;
        }
        if (model.IncludeSelfWeight)
        {
            var q = model.Section.Area * model.Material.Density * 9.80665;
            shear -= q * x; moment -= q * x * x / 2;
        }
        return (shear, moment);
    }
}
