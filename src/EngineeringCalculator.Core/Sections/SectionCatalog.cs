namespace EngineeringCalculator.Sections;

public static class SectionCatalog
{
    private static Section Ipe(string name, double hMm, double areaCm2, double iCm4, double wCm3, double kgM) => new()
    {
        Id = name.ToLowerInvariant(), Name = name, Family = "IPE", Height = hMm / 1000,
        Area = areaCm2 * 1e-4, MomentOfInertia = iCm4 * 1e-8, SectionModulus = wCm3 * 1e-6, MassPerMetre = kgM
    };

    private static readonly IReadOnlyList<Section> Items =
    [
        Ipe("IPE 100", 100, 10.3, 171, 34.2, 8.1), Ipe("IPE 120", 120, 13.2, 318, 53.0, 10.4),
        Ipe("IPE 140", 140, 16.4, 541, 77.3, 12.9), Ipe("IPE 160", 160, 20.1, 869, 108.6, 15.8),
        Ipe("IPE 180", 180, 23.9, 1317, 146.3, 18.8), Ipe("IPE 200", 200, 28.5, 1943, 194.3, 22.4),
        Ipe("IPE 220", 220, 33.4, 2772, 252.0, 26.2), Ipe("IPE 240", 240, 39.1, 3892, 324.3, 30.7),
        Ipe("IPE 270", 270, 45.9, 5790, 428.9, 36.1), Ipe("IPE 300", 300, 53.8, 8356, 557.1, 42.2),
        Ipe("IPE 330", 330, 62.6, 11770, 713.1, 49.1), Ipe("IPE 360", 360, 72.7, 16270, 903.6, 57.1),
        Ipe("IPE 400", 400, 84.5, 23130, 1156, 66.3), Ipe("IPE 450", 450, 98.8, 33740, 1500, 77.6),
        Ipe("IPE 500", 500, 116.0, 48200, 1928, 90.7), Ipe("IPE 550", 550, 134.0, 67120, 2441, 106),
        Ipe("IPE 600", 600, 156.0, 92080, 3069, 122),
        SectionFactory.Rectangle("RECT 100×200", 0.1, 0.2),
        SectionFactory.RectangularHollow("RHS 200×100×8", 0.1, 0.2, 0.008),
        SectionFactory.CircularHollow("CHS 168×8", 0.168, 0.008)
    ];

    public static IReadOnlyList<Section> All => Items;

    public static Section Get(string id) => Items.FirstOrDefault(x => x.Id.Equals(id, StringComparison.OrdinalIgnoreCase))
        ?? throw new KeyNotFoundException($"Сечение '{id}' не найдено.");
}
