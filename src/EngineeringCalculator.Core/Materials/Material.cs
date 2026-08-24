using EngineeringCalculator.Common;

namespace EngineeringCalculator.Materials;

public sealed record Material
{
    public required string Id { get; init; }
    public required string Name { get; init; }
    public required double ElasticModulus { get; init; }
    public required double YieldStrength { get; init; }
    public required double Density { get; init; }
    public double PoissonRatio { get; init; } = 0.3;
    public double PartialSafetyFactor { get; init; } = 1.1;
    public double AllowableStress => YieldStrength / PartialSafetyFactor;

    public void Validate()
    {
        Guard.Positive(ElasticModulus, nameof(ElasticModulus));
        Guard.Positive(YieldStrength, nameof(YieldStrength));
        Guard.Positive(Density, nameof(Density));
        Guard.Positive(PartialSafetyFactor, nameof(PartialSafetyFactor));
        Guard.InRange(PoissonRatio, 0, 0.5, nameof(PoissonRatio));
    }
}
