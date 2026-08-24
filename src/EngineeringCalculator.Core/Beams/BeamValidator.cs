using EngineeringCalculator.Common;

namespace EngineeringCalculator.Beams;

internal static class BeamValidator
{
    public static void Validate(BeamModel model)
    {
        ArgumentNullException.ThrowIfNull(model);
        Guard.Positive(model.Length, nameof(model.Length));
        model.Material.Validate(); model.Section.Validate();
        if (model.MeshElements < 4 || model.MeshElements > 2000)
            throw new ArgumentOutOfRangeException(nameof(model.MeshElements), "Число конечных элементов должно быть от 4 до 2000.");
        Guard.Positive(model.DeflectionLimitRatio, nameof(model.DeflectionLimitRatio));
        if (model.Supports.Count == 0) throw new ArgumentException("У балки должна быть хотя бы одна опора.", nameof(model.Supports));

        foreach (var support in model.Supports) Guard.InRange(support.Position, 0, model.Length, nameof(support.Position));
        foreach (var load in model.PointLoads)
        {
            Guard.InRange(load.Position, 0, model.Length, nameof(load.Position));
            Guard.NonNegative(load.Force, nameof(load.Force));
        }
        foreach (var moment in model.Moments)
        {
            Guard.InRange(moment.Position, 0, model.Length, nameof(moment.Position));
            if (!double.IsFinite(moment.Moment)) throw new ArgumentOutOfRangeException(nameof(moment.Moment));
        }
        foreach (var load in model.DistributedLoads)
        {
            Guard.InRange(load.Start, 0, model.Length, nameof(load.Start));
            Guard.InRange(load.End, 0, model.Length, nameof(load.End));
            if (load.End <= load.Start) throw new ArgumentException("Конец распределённой нагрузки должен быть правее начала.");
            Guard.NonNegative(load.StartIntensity, nameof(load.StartIntensity));
            Guard.NonNegative(load.EndIntensity, nameof(load.EndIntensity));
        }
    }
}
