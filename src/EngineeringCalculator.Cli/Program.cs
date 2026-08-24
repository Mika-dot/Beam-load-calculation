using System.Text.Json;
using System.Text.Json.Serialization;
using EngineeringCalculator.Beams;
using EngineeringCalculator.Contracts;
using EngineeringCalculator.Examples;
using EngineeringCalculator.Reports;
using EngineeringCalculator.Sections;
using EngineeringCalculator.Trusses;

Console.OutputEncoding = System.Text.Encoding.UTF8;
var jsonOptions = new JsonSerializerOptions
{
    PropertyNameCaseInsensitive = true,
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    WriteIndented = true,
    Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
};

try
{
    var command = args.FirstOrDefault()?.ToLowerInvariant() ?? "help";
    switch (command)
    {
        case "demo":
        {
            var model = ExampleModels.SimplySupportedBeam();
            var result = new BeamSolver().Solve(model);
            Print(result);
            SaveReports(model, result, Option(args, "--output") ?? "reports/demo");
            break;
        }
        case "beam":
        {
            var input = RequiredOption(args, "--input");
            var request = JsonSerializer.Deserialize<BeamRequest>(File.ReadAllText(input), jsonOptions)
                          ?? throw new InvalidDataException("Входной JSON пуст.");
            var model = request.ToModel(); var result = new BeamSolver().Solve(model);
            Print(result); SaveReports(model, result, Option(args, "--output") ?? "reports/beam");
            break;
        }
        case "select":
        {
            var input = RequiredOption(args, "--input");
            var request = JsonSerializer.Deserialize<BeamRequest>(File.ReadAllText(input), jsonOptions)
                          ?? throw new InvalidDataException("Входной JSON пуст.");
            var selection = new SectionSelector().Select(request.ToModel());
            Console.WriteLine("Подходящие профили (по возрастанию массы):");
            foreach (var item in selection.Suitable)
                Console.WriteLine($"  {item.Section.Name,-10} {item.Section.MassPerMetre,6:F1} кг/м  σ={item.Result.MaximumAbsoluteStress/1e6,7:F1} МПа  f={item.Result.MaximumAbsoluteDeflection*1000,7:F2} мм  η={Math.Max(item.Result.StrengthUtilization,item.Result.DeflectionUtilization)*100,6:F1}%");
            if (selection.Best is null) Environment.ExitCode = 2;
            break;
        }
        case "truss-demo":
        {
            var result = new TrussSolver().Solve(ExampleModels.TriangularTruss());
            Console.WriteLine($"{result.Name}: {(result.Passes ? "СООТВЕТСТВУЕТ" : "НЕ СООТВЕТСТВУЕТ")}");
            foreach (var member in result.Members)
                Console.WriteLine($"  {member.Name,-20} N={member.AxialForce/1000,9:F2} кН  σ={member.Stress/1e6,8:F2} МПа  η={member.Utilization*100,6:F1}%");
            break;
        }
        case "profiles":
            Console.WriteLine("Профиль       Семейство          кг/м      I, см⁴      W, см³");
            foreach (var section in SectionCatalog.All)
                Console.WriteLine($"{section.Name,-13} {section.Family,-16} {section.MassPerMetre,7:F1} {section.MomentOfInertia/1e-8,11:F0} {section.SectionModulus/1e-6,11:F1}");
            break;
        default:
            PrintHelp();
            break;
    }
}
catch (Exception error)
{
    Console.Error.WriteLine($"Ошибка: {error.Message}");
    Environment.ExitCode = 1;
}

static void Print(BeamResult result)
{
    Console.WriteLine($"\n{result.Name}");
    Console.WriteLine(new string('─', Math.Min(70, result.Name.Length + 4)));
    foreach (var reaction in result.Reactions)
        Console.WriteLine($"{reaction.Name}: R={reaction.VerticalForce/1000:F3} кН, M={reaction.Moment/1000:F3} кН·м");
    Console.WriteLine($"|Q|max = {result.MaximumAbsoluteShear/1000:F3} кН");
    Console.WriteLine($"|M|max = {result.MaximumAbsoluteMoment/1000:F3} кН·м");
    Console.WriteLine($"|f|max = {result.MaximumAbsoluteDeflection*1000:F3} мм (предел {result.DeflectionLimit*1000:F3} мм)");
    Console.WriteLine($"|σ|max = {result.MaximumAbsoluteStress/1e6:F2} МПа");
    Console.WriteLine($"Итог: {(result.Passes ? "СООТВЕТСТВУЕТ" : "НЕ СООТВЕТСТВУЕТ")}\n");
}

static void SaveReports(EngineeringCalculator.Beams.BeamModel model, BeamResult result, string basePath)
{
    var directory = Path.GetDirectoryName(basePath); if (!string.IsNullOrWhiteSpace(directory)) Directory.CreateDirectory(directory);
    File.WriteAllText(basePath + ".html", EngineeringReportGenerator.ToHtml(model, result));
    File.WriteAllText(basePath + ".csv", EngineeringReportGenerator.ToCsv(result));
    File.WriteAllText(basePath + ".json", EngineeringReportGenerator.ToJson(result));
    Console.WriteLine($"Отчёты: {Path.GetFullPath(basePath)}.html/.csv/.json");
}

static string RequiredOption(string[] values, string name) => Option(values, name)
    ?? throw new ArgumentException($"Не указан обязательный параметр {name}.");

static string? Option(string[] values, string name)
{
    var index = Array.FindIndex(values, x => x.Equals(name, StringComparison.OrdinalIgnoreCase));
    return index >= 0 && index + 1 < values.Length ? values[index + 1] : null;
}

static void PrintHelp() => Console.WriteLine("""
Universal Engineering Calculator

  demo                         Выполнить демонстрационный расчёт и создать отчёты
  beam --input file.json       Рассчитать балку из JSON
       [--output reports/name] Базовый путь для HTML/CSV/JSON
  select --input file.json     Подобрать минимальный профиль IPE
  truss-demo                   Рассчитать демонстрационную ферму
  profiles                     Показать каталог сечений

Веб-интерфейс: dotnet run --project src/EngineeringCalculator.Web
""");
