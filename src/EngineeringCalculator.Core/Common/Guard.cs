namespace EngineeringCalculator.Common;

internal static class Guard
{
    public static void Positive(double value, string name)
    {
        if (!double.IsFinite(value) || value <= 0)
            throw new ArgumentOutOfRangeException(name, value, "Значение должно быть конечным и больше нуля.");
    }

    public static void NonNegative(double value, string name)
    {
        if (!double.IsFinite(value) || value < 0)
            throw new ArgumentOutOfRangeException(name, value, "Значение должно быть конечным и неотрицательным.");
    }

    public static void InRange(double value, double min, double max, string name)
    {
        if (!double.IsFinite(value) || value < min - 1e-10 || value > max + 1e-10)
            throw new ArgumentOutOfRangeException(name, value, $"Значение должно находиться в диапазоне [{min}; {max}].");
    }
}
