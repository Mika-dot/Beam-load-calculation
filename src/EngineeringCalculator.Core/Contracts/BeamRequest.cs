using EngineeringCalculator.Beams;
using EngineeringCalculator.Materials;
using EngineeringCalculator.Sections;

namespace EngineeringCalculator.Contracts;

public sealed record SupportInput(double Position, BeamSupportType Type, string? Name = null);
public sealed record PointLoadInput(double Position, double ForceKn, string? Name = null);
public sealed record MomentInput(double Position, double MomentKnm, string? Name = null);
public sealed record DistributedLoadInput(double Start, double End, double StartKnPerM, double? EndKnPerM = null, string? Name = null);

public sealed record BeamRequest
{
    public string Name { get; init; } = "Расчёт балки";
    public double Length { get; init; } = 6;
    public string MaterialId { get; init; } = "steel-s235";
    public string SectionId { get; init; } = "ipe 300";
    public IReadOnlyList<SupportInput> Supports { get; init; } = [new(0, BeamSupportType.Pinned, "A"), new(6, BeamSupportType.Roller, "B")];
    public IReadOnlyList<PointLoadInput> PointLoads { get; init; } = [];
    public IReadOnlyList<MomentInput> Moments { get; init; } = [];
    public IReadOnlyList<DistributedLoadInput> DistributedLoads { get; init; } = [];
    public bool IncludeSelfWeight { get; init; } = true;
    public int MeshElements { get; init; } = 100;
    public double DeflectionLimitRatio { get; init; } = 250;

    public BeamModel ToModel(Section? overrideSection = null) => new()
    {
        Name = Name, Length = Length, Material = MaterialCatalog.Get(MaterialId), Section = overrideSection ?? SectionCatalog.Get(SectionId),
        Supports = Supports.Select(x => new BeamSupport(x.Position, x.Type, x.Name)).ToArray(),
        PointLoads = PointLoads.Select(x => new PointLoad(x.Position, x.ForceKn * 1000, x.Name)).ToArray(),
        Moments = Moments.Select(x => new AppliedMoment(x.Position, x.MomentKnm * 1000, x.Name)).ToArray(),
        DistributedLoads = DistributedLoads.Select(x => new DistributedLoad(x.Start, x.End, x.StartKnPerM*1000, (x.EndKnPerM ?? x.StartKnPerM)*1000, x.Name)).ToArray(),
        IncludeSelfWeight = IncludeSelfWeight, MeshElements = MeshElements, DeflectionLimitRatio = DeflectionLimitRatio
    };
}
