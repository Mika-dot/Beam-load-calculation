namespace UniversalEngineeringCalculator.Core.Beam;

public enum SupportType
{
    Fixed,
    Pin,
    Roller
}

public record BeamLoad(double Position, double Value, bool Distributed = false);

public class BeamModel
{
    public double Length { get; init; }
    public List<BeamLoad> Loads { get; } = new();
    public SupportType LeftSupport { get; init; } = SupportType.Pin;
    public SupportType RightSupport { get; init; } = SupportType.Roller;
}
