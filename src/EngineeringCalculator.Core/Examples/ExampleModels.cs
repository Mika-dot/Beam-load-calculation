using EngineeringCalculator.Beams;
using EngineeringCalculator.Materials;
using EngineeringCalculator.Sections;
using EngineeringCalculator.Trusses;

namespace EngineeringCalculator.Examples;

public static class ExampleModels
{
    public static BeamModel SimplySupportedBeam() => new()
    {
        Name = "Балка покрытия Б-1", Length = 6, Material = MaterialCatalog.Get("steel-s235"), Section = SectionCatalog.Get("ipe 300"),
        Supports = [new(0, BeamSupportType.Pinned, "A"), new(6, BeamSupportType.Roller, "B")],
        DistributedLoads = [new(0, 6, 12_000, "Постоянная + временная")], PointLoads = [new(2, 18_000, "Оборудование")],
        IncludeSelfWeight = true, MeshElements = 120, DeflectionLimitRatio = 250
    };

    public static TrussModel TriangularTruss()
    {
        var steel = MaterialCatalog.Get("steel-s355");
        return new TrussModel
        {
            Name = "Треугольная ферма", Nodes = [new(1,0,0,true,true), new(2,4,0,false,true), new(3,2,2)],
            Members = [new(1,1,2,0.002,steel,"Нижний пояс"), new(2,1,3,0.002,steel,"Раскос L"), new(3,2,3,0.002,steel,"Раскос R")],
            Loads = [new(3,0,-100_000,"Узловая нагрузка")]
        };
    }
}
