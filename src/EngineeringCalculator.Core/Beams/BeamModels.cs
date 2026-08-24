using EngineeringCalculator.Materials;
using EngineeringCalculator.Sections;

namespace EngineeringCalculator.Beams;

public enum BeamSupportType { Free, Pinned, Roller, Fixed }

public sealed record BeamSupport(double Position, BeamSupportType Type, string? Name = null);
public sealed record PointLoad(double Position, double Force, string? Name = null);
public sealed record AppliedMoment(double Position, double Moment, string? Name = null);
public sealed record DistributedLoad(double Start, double End, double StartIntensity, double EndIntensity, string? Name = null)
{
    public DistributedLoad(double start, double end, double intensity, string? name = null)
        : this(start, end, intensity, intensity, name) { }
}

public sealed record BeamModel
{
    public string Name { get; init; } = "Расчёт балки";
    public double Length { get; init; }
    public required Material Material { get; init; }
    public required Section Section { get; init; }
    public IReadOnlyList<BeamSupport> Supports { get; init; } = [];
    public IReadOnlyList<PointLoad> PointLoads { get; init; } = [];
    public IReadOnlyList<AppliedMoment> Moments { get; init; } = [];
    public IReadOnlyList<DistributedLoad> DistributedLoads { get; init; } = [];
    public bool IncludeSelfWeight { get; init; }
    public int MeshElements { get; init; } = 80;
    public double DeflectionLimitRatio { get; init; } = 250;
}

public sealed record BeamSample(double X, double Shear, double Moment, double Deflection, double Rotation, double Stress);
public sealed record BeamReaction(double Position, double VerticalForce, double Moment, string Name);

public sealed record BeamResult
{
    public required string Name { get; init; }
    public required IReadOnlyList<BeamReaction> Reactions { get; init; }
    public required IReadOnlyList<BeamSample> Samples { get; init; }
    public required double MaximumAbsoluteShear { get; init; }
    public required double MaximumAbsoluteMoment { get; init; }
    public required double MaximumAbsoluteDeflection { get; init; }
    public required double MaximumAbsoluteStress { get; init; }
    public required double StrengthUtilization { get; init; }
    public required double DeflectionUtilization { get; init; }
    public required double SafetyFactor { get; init; }
    public required double DeflectionLimit { get; init; }
    public required bool StrengthPasses { get; init; }
    public required bool DeflectionPasses { get; init; }
    public bool Passes => StrengthPasses && DeflectionPasses;
}
