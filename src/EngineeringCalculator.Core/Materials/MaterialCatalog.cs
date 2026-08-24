namespace EngineeringCalculator.Materials;

public static class MaterialCatalog
{
    private static readonly IReadOnlyList<Material> Items =
    [
        new() { Id = "steel-s235", Name = "Сталь S235", ElasticModulus = 210e9, YieldStrength = 235e6, Density = 7850 },
        new() { Id = "steel-s355", Name = "Сталь S355", ElasticModulus = 210e9, YieldStrength = 355e6, Density = 7850 },
        new() { Id = "al-6061-t6", Name = "Алюминий 6061-T6", ElasticModulus = 69e9, YieldStrength = 276e6, Density = 2700, PoissonRatio = 0.33 },
        new() { Id = "timber-c24", Name = "Древесина C24", ElasticModulus = 11e9, YieldStrength = 24e6, Density = 420, PoissonRatio = 0.35, PartialSafetyFactor = 1.3 },
        new() { Id = "concrete-c30", Name = "Бетон C30/37 (упрощённо)", ElasticModulus = 33e9, YieldStrength = 30e6, Density = 2500, PoissonRatio = 0.2, PartialSafetyFactor = 1.5 }
    ];

    public static IReadOnlyList<Material> All => Items;

    public static Material Get(string id) => Items.FirstOrDefault(x => x.Id.Equals(id, StringComparison.OrdinalIgnoreCase))
        ?? throw new KeyNotFoundException($"Материал '{id}' не найден.");
}
