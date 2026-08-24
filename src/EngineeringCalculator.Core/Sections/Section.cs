using EngineeringCalculator.Common;

namespace EngineeringCalculator.Sections;

public sealed record Section
{
    public required string Id { get; init; }
    public required string Name { get; init; }
    public required string Family { get; init; }
    public required double Area { get; init; }
    public required double MomentOfInertia { get; init; }
    public required double SectionModulus { get; init; }
    public required double Height { get; init; }
    public required double MassPerMetre { get; init; }
    public double RadiusOfGyration => Math.Sqrt(MomentOfInertia / Area);

    public void Validate()
    {
        Guard.Positive(Area, nameof(Area));
        Guard.Positive(MomentOfInertia, nameof(MomentOfInertia));
        Guard.Positive(SectionModulus, nameof(SectionModulus));
        Guard.Positive(Height, nameof(Height));
        Guard.NonNegative(MassPerMetre, nameof(MassPerMetre));
    }
}
