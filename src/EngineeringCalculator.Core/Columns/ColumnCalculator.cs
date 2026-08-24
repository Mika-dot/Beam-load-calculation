using EngineeringCalculator.Common;
using EngineeringCalculator.Materials;
using EngineeringCalculator.Sections;

namespace EngineeringCalculator.Columns;

public enum ColumnEndCondition { PinnedPinned, FixedFree, FixedPinned, FixedFixed }

public sealed record ColumnModel(double Length, double AxialForce, Material Material, Section Section,
    ColumnEndCondition EndCondition = ColumnEndCondition.PinnedPinned, double ImperfectionFactor = 1.0);

public sealed record ColumnResult(double EffectiveLength, double Slenderness, double EulerCriticalForce,
    double YieldCapacity, double BucklingUtilization, double StrengthUtilization, double CombinedUtilization, bool Passes);

public sealed class ColumnCalculator
{
    public ColumnResult Calculate(ColumnModel model)
    {
        Guard.Positive(model.Length, nameof(model.Length)); Guard.NonNegative(model.AxialForce, nameof(model.AxialForce));
        Guard.Positive(model.ImperfectionFactor, nameof(model.ImperfectionFactor)); model.Material.Validate(); model.Section.Validate();
        var factor = model.EndCondition switch
        {
            ColumnEndCondition.FixedFree => 2.0, ColumnEndCondition.FixedPinned => 0.7,
            ColumnEndCondition.FixedFixed => 0.5, _ => 1.0
        };
        var effectiveLength = factor * model.Length;
        var critical = Math.PI * Math.PI * model.Material.ElasticModulus * model.Section.MomentOfInertia
                       / (effectiveLength * effectiveLength) / model.ImperfectionFactor;
        var yield = model.Section.Area * model.Material.AllowableStress;
        var bucklingUtilization = model.AxialForce / critical;
        var strengthUtilization = model.AxialForce / yield;
        var combined = Math.Max(bucklingUtilization, strengthUtilization);
        return new ColumnResult(effectiveLength, effectiveLength/model.Section.RadiusOfGyration, critical, yield,
            bucklingUtilization, strengthUtilization, combined, combined <= 1);
    }
}
