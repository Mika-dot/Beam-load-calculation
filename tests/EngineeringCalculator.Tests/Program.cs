using System;
using System.Collections.Generic;
using System.Linq;
using EngineeringCalculator.Beams;
using EngineeringCalculator.Columns;
using EngineeringCalculator.Examples;
using EngineeringCalculator.Materials;
using EngineeringCalculator.Reports;
using EngineeringCalculator.Sections;
using EngineeringCalculator.Trusses;

var tests = new (string Name, Action Run)[]
{
    ("Балка: равномерная нагрузка", UniformBeam),
    ("Балка: центральная сила", CentrePointLoad),
    ("Балка: консоль", Cantilever),
    ("Балка: линейная нагрузка и равновесие", TriangularLoad),
    ("Сечения: геометрические характеристики", Sections),
    ("Ферма: симметрия и равновесие", Truss),
    ("Колонна: критическая сила Эйлера", Column),
    ("Подбор: минимальное сечение", Selection),
    ("Отчёт: HTML/CSV/JSON", Reports)
};

var failed = 0;
foreach (var test in tests)
{
    try { test.Run(); Console.WriteLine($"PASS  {test.Name}"); }
    catch (Exception error) { failed++; Console.Error.WriteLine($"FAIL  {test.Name}: {error.Message}"); }
}
Console.WriteLine($"\nИтог: {tests.Length-failed}/{tests.Length} проверок пройдено.");
return failed == 0 ? 0 : 1;

static BeamModel Beam(double length, Section section, IReadOnlyList<BeamSupport> supports,
    IReadOnlyList<PointLoad>? points=null, IReadOnlyList<DistributedLoad>? distributed=null) => new()
{
    Length=length, Material=MaterialCatalog.Get("steel-s235"), Section=section, Supports=supports,
    PointLoads=points??[], DistributedLoads=distributed??[], MeshElements=160, DeflectionLimitRatio=250
};

static void UniformBeam()
{
    const double l=6,q=10_000; var section=SectionCatalog.Get("ipe 300");
    var model=Beam(l,section,[new(0,BeamSupportType.Pinned),new(l,BeamSupportType.Roller)],distributed:[new(0,l,q)]);
    var r=new BeamSolver().Solve(model);
    Near(r.Reactions[0].VerticalForce,q*l/2,1e-7,"левая реакция"); Near(r.Reactions[1].VerticalForce,q*l/2,1e-7,"правая реакция");
    Near(r.MaximumAbsoluteMoment,q*l*l/8,2e-3,"максимальный момент");
    var exact=5*q*Math.Pow(l,4)/(384*model.Material.ElasticModulus*section.MomentOfInertia);
    Near(r.MaximumAbsoluteDeflection,exact,2e-3,"максимальный прогиб");
}

static void CentrePointLoad()
{
    const double l=4,p=20_000;var section=SectionCatalog.Get("ipe 240");
    var model=Beam(l,section,[new(0,BeamSupportType.Pinned),new(l,BeamSupportType.Roller)],points:[new(l/2,p)]);
    var r=new BeamSolver().Solve(model);Near(r.MaximumAbsoluteMoment,p*l/4,2e-3,"момент от центральной силы");
    var exact=p*Math.Pow(l,3)/(48*model.Material.ElasticModulus*section.MomentOfInertia);Near(r.MaximumAbsoluteDeflection,exact,2e-3,"прогиб от центральной силы");
}

static void Cantilever()
{
    const double l=3,p=12_000;var section=SectionCatalog.Get("ipe 200");
    var model=Beam(l,section,[new(0,BeamSupportType.Fixed)],points:[new(l,p)]);var r=new BeamSolver().Solve(model);
    Near(r.Reactions[0].VerticalForce,p,1e-6,"реакция консоли");Near(Math.Abs(r.Reactions[0].Moment),p*l,1e-6,"момент защемления");
    var exact=p*Math.Pow(l,3)/(3*model.Material.ElasticModulus*section.MomentOfInertia);Near(r.MaximumAbsoluteDeflection,exact,2e-3,"прогиб консоли");
}

static void TriangularLoad()
{
    const double l=5,q=15_000;var r=new BeamSolver().Solve(Beam(l,SectionCatalog.Get("ipe 270"),[new(0,BeamSupportType.Pinned),new(l,BeamSupportType.Roller)],distributed:[new(0,l,0,q)]));
    var total=q*l/2;Near(r.Reactions.Sum(x=>x.VerticalForce),total,1e-6,"сумма реакций");Near(r.Reactions[0].VerticalForce,total/3,3e-3,"реакция A");Near(r.Reactions[1].VerticalForce,2*total/3,3e-3,"реакция B");
}

static void Sections()
{
    var s=SectionFactory.Rectangle("R",0.1,0.2);Near(s.Area,.02,1e-12,"площадь");Near(s.MomentOfInertia,.1*Math.Pow(.2,3)/12,1e-12,"момент инерции");
    var tube=SectionFactory.CircularHollow("T",.1,.005);True(tube.Area>0&&tube.MomentOfInertia>0,"характеристики трубы");
}

static void Truss()
{
    var r=new TrussSolver().Solve(ExampleModels.TriangularTruss());var rx=r.Nodes.Sum(x=>x.ReactionX);var ry=r.Nodes.Sum(x=>x.ReactionY);
    Near(rx,0,1e-6,"горизонтальное равновесие");Near(ry,100_000,1e-6,"вертикальное равновесие");
    Near(r.Nodes.Single(x=>x.NodeId==1).ReactionY,50_000,1e-6,"левая реакция");
    Near(r.Nodes.Single(x=>x.NodeId==2).ReactionY,50_000,1e-6,"правая реакция");True(r.Members.Count==3,"число стержней");
}

static void Column()
{
    var material=MaterialCatalog.Get("steel-s235");var section=SectionCatalog.Get("ipe 200");var model=new ColumnModel(3,100_000,material,section);
    var r=new ColumnCalculator().Calculate(model);var exact=Math.PI*Math.PI*material.ElasticModulus*section.MomentOfInertia/9;
    Near(r.EulerCriticalForce,exact,1e-12,"сила Эйлера");True(r.Slenderness>0&&r.CombinedUtilization>0,"показатели колонны");
}

static void Selection()
{
    var model=Beam(3,SectionCatalog.Get("ipe 100"),[new(0,BeamSupportType.Pinned),new(3,BeamSupportType.Roller)],distributed:[new(0,3,2_000)]);
    var result=new SectionSelector().Select(model);True(result.Best is not null,"найден профиль");True(result.Suitable.SequenceEqual(result.Suitable.OrderBy(x=>x.TotalMass)),"сортировка по массе");
}

static void Reports()
{
    var model=ExampleModels.SimplySupportedBeam();var result=new BeamSolver().Solve(model);var html=EngineeringReportGenerator.ToHtml(model,result);
    True(html.Contains("<svg",StringComparison.Ordinal)&&html.Contains("Итог проверки",StringComparison.Ordinal),"HTML отчёт");
    True(EngineeringReportGenerator.ToCsv(result).Split('\n').Length>100,"CSV отчёт");True(EngineeringReportGenerator.ToJson(result).Contains("maximumAbsoluteMoment",StringComparison.Ordinal),"JSON отчёт");
}

static void Near(double actual,double expected,double relative,string label)
{
    var tolerance=Math.Max(Math.Abs(expected)*relative,1e-8);if(Math.Abs(actual-expected)>tolerance)throw new InvalidOperationException($"{label}: {actual:G8}, ожидалось {expected:G8}, допуск {tolerance:G3}");
}
static void True(bool condition,string label){if(!condition)throw new InvalidOperationException(label);}
