namespace EngineeringCalculator.Units;

public static class UnitConverter
{
    public static double MillimetresToMetres(double value) => value / 1000;
    public static double MetresToMillimetres(double value) => value * 1000;
    public static double KilonewtonsToNewtons(double value) => value * 1000;
    public static double NewtonsToKilonewtons(double value) => value / 1000;
    public static double MegapascalsToPascals(double value) => value * 1e6;
    public static double PascalsToMegapascals(double value) => value / 1e6;
    public static double CentimetresFourthToMetresFourth(double value) => value * 1e-8;
    public static double CentimetresCubedToMetresCubed(double value) => value * 1e-6;
}
