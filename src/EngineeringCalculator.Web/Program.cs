using System.Text.Json.Serialization;
using EngineeringCalculator.Beams;
using EngineeringCalculator.Columns;
using EngineeringCalculator.Contracts;
using EngineeringCalculator.Examples;
using EngineeringCalculator.Materials;
using EngineeringCalculator.Reports;
using EngineeringCalculator.Sections;
using EngineeringCalculator.Trusses;

var builder = WebApplication.CreateBuilder(args);
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter(System.Text.Json.JsonNamingPolicy.CamelCase));
});
var app = builder.Build();
app.UseDefaultFiles(); app.UseStaticFiles();

app.MapGet("/api/catalog/materials", () => MaterialCatalog.All);
app.MapGet("/api/catalog/sections", () => SectionCatalog.All);
app.MapPost("/api/beam", (BeamRequest request) => Results.Ok(new BeamSolver().Solve(request.ToModel())));
app.MapPost("/api/beam/select", (BeamRequest request) => Results.Ok(new SectionSelector().Select(request.ToModel())));
app.MapPost("/api/beam/report", (BeamRequest request) =>
{
    var model = request.ToModel(); var result = new BeamSolver().Solve(model);
    return Results.Content(EngineeringReportGenerator.ToHtml(model, result), "text/html; charset=utf-8");
});
app.MapPost("/api/column", (ColumnRequest request) =>
{
    var model = new ColumnModel(request.Length, request.AxialForceKn*1000, MaterialCatalog.Get(request.MaterialId),
        SectionCatalog.Get(request.SectionId), request.EndCondition, request.ImperfectionFactor);
    return Results.Ok(new ColumnCalculator().Calculate(model));
});
app.MapGet("/api/truss/demo", () => new TrussSolver().Solve(ExampleModels.TriangularTruss()));
app.MapGet("/api/health", () => Results.Ok(new { status = "ok", version = "2.0.0" }));
app.Run();

public sealed record ColumnRequest(double Length, double AxialForceKn, string MaterialId, string SectionId,
    ColumnEndCondition EndCondition = ColumnEndCondition.PinnedPinned, double ImperfectionFactor = 1.0);
