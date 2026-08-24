using System.Globalization;
using System.Net;
using System.Text;
using System.Text.Json;
using EngineeringCalculator.Beams;

namespace EngineeringCalculator.Reports;

public static class EngineeringReportGenerator
{
    private static readonly CultureInfo Invariant = CultureInfo.InvariantCulture;

    public static string ToJson(BeamResult result, bool indented = true) => JsonSerializer.Serialize(result,
        new JsonSerializerOptions { WriteIndented = indented, PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

    public static string ToCsv(BeamResult result)
    {
        var builder = new StringBuilder("x_m;shear_N;moment_Nm;deflection_m;rotation_rad;stress_Pa\n");
        foreach (var s in result.Samples)
            builder.AppendLine(string.Join(';', F(s.X), F(s.Shear), F(s.Moment), F(s.Deflection), F(s.Rotation), F(s.Stress)));
        return builder.ToString();
    }

    public static string ToHtml(BeamModel model, BeamResult result)
    {
        var status = result.Passes ? "СООТВЕТСТВУЕТ" : "НЕ СООТВЕТСТВУЕТ";
        var statusClass = result.Passes ? "ok" : "bad";
        var selfWeight = model.IncludeSelfWeight ? "включён" : "не включён";
        var reactionRows = string.Join("", result.Reactions.Select(x =>
            $"<tr><td>{H(x.Name)}</td><td>{x.Position:F3}</td><td>{x.VerticalForce/1000:F3}</td><td>{x.Moment/1000:F3}</td></tr>"));
        return $$"""
        <!doctype html><html lang="ru"><head><meta charset="utf-8"><meta name="viewport" content="width=device-width">
        <title>{{H(result.Name)}} — инженерный отчёт</title><style>
        :root{--ink:#111827;--muted:#64748b;--line:#dbe3ec;--accent:#0f766e;--blue:#2563eb;--bad:#b91c1c}*{box-sizing:border-box}
        body{font:14px/1.5 Inter,Segoe UI,Arial,sans-serif;color:var(--ink);margin:0;background:#eef2f6}.page{width:210mm;min-height:297mm;margin:16px auto;background:white;padding:18mm;box-shadow:0 8px 30px #0f172a22}
        h1{font-size:27px;margin:0 0 4px}.eyebrow{color:var(--accent);font-weight:800;letter-spacing:.12em;text-transform:uppercase}.muted{color:var(--muted)}
        .status{margin:20px 0;padding:14px 18px;border-left:5px solid var(--accent);background:#ecfdf5;font-weight:800}.status.bad{border-color:var(--bad);background:#fef2f2;color:var(--bad)}
        .grid{display:grid;grid-template-columns:repeat(3,1fr);gap:10px}.metric{border:1px solid var(--line);border-radius:10px;padding:12px}.metric b{display:block;font-size:19px}.metric span{color:var(--muted)}
        h2{font-size:18px;margin:26px 0 10px;border-bottom:1px solid var(--line);padding-bottom:6px}table{width:100%;border-collapse:collapse}th,td{padding:7px 8px;border-bottom:1px solid var(--line);text-align:left}th{color:var(--muted)}
        svg{width:100%;height:170px;border:1px solid var(--line);border-radius:8px;background:#fff}.axis{stroke:#94a3b8}.curve{fill:none;stroke:var(--blue);stroke-width:2}.area{fill:#2563eb16}.foot{margin-top:28px;color:var(--muted);font-size:11px}
        @page{size:A4;margin:12mm}@media print{body{background:white}.page{width:auto;min-height:auto;margin:0;padding:0;box-shadow:none} }
        </style></head><body><main class="page"><div class="eyebrow">Universal Engineering Calculator</div><h1>{{H(result.Name)}}</h1>
        <div class="muted">Расчёт по линейной теории Эйлера—Бернулли • {{DateTime.UtcNow:yyyy-MM-dd HH:mm}} UTC</div>
        <div class="status {{statusClass}}">Итог проверки: {{status}}</div>
        <section class="grid">
          <div class="metric"><b>{{result.MaximumAbsoluteMoment/1000:F2}} кН·м</b><span>максимальный момент</span></div>
          <div class="metric"><b>{{result.MaximumAbsoluteDeflection*1000:F2}} мм</b><span>прогиб, предел {{result.DeflectionLimit*1000:F2}} мм</span></div>
          <div class="metric"><b>{{result.MaximumAbsoluteStress/1e6:F1}} МПа</b><span>напряжение</span></div>
          <div class="metric"><b>{{result.MaximumAbsoluteShear/1000:F2}} кН</b><span>максимальная поперечная сила</span></div>
          <div class="metric"><b>{{result.StrengthUtilization*100:F1}} %</b><span>использование прочности</span></div>
          <div class="metric"><b>{{result.DeflectionUtilization*100:F1}} %</b><span>использование по прогибу</span></div>
        </section>
        <h2>Исходные данные</h2><table><tr><th>Параметр</th><th>Значение</th></tr>
        <tr><td>Длина</td><td>{{model.Length:F3}} м</td></tr><tr><td>Материал</td><td>{{H(model.Material.Name)}}, E = {{model.Material.ElasticModulus/1e9:F1}} ГПа</td></tr>
        <tr><td>Сечение</td><td>{{H(model.Section.Name)}}, I = {{model.Section.MomentOfInertia:E3}} м⁴, W = {{model.Section.SectionModulus:E3}} м³</td></tr>
        <tr><td>Расчётная модель</td><td>{{model.MeshElements}} КЭ; собственный вес: {{selfWeight}}</td></tr></table>
        <h2>Опорные реакции</h2><table><tr><th>Опора</th><th>x, м</th><th>R, кН</th><th>M, кН·м</th></tr>
        {{reactionRows}}</table>
        <h2>Эпюра поперечных сил Q</h2>{{Chart(result.Samples, x => x.Shear/1000)}}
        <h2>Эпюра изгибающих моментов M</h2>{{Chart(result.Samples, x => x.Moment/1000)}}
        <h2>Линия прогибов</h2>{{Chart(result.Samples, x => x.Deflection*1000)}}
        <div class="foot">Отчёт предназначен для предварительных инженерных расчётов. Для ответственных конструкций требуется проверка исходных данных, расчётной схемы и норм проектирования аттестованным специалистом.</div>
        </main></body></html>
        """;
    }

    private static string Chart(IReadOnlyList<BeamSample> samples, Func<BeamSample,double> selector)
    {
        const double width=700, height=160, pad=18; var maxX=samples.Max(x=>x.X); var values=samples.Select(selector).ToArray();
        var maxAbs=Math.Max(values.Max(Math.Abs),1e-12); var points=samples.Select((s,i)=>
            FormattableString.Invariant($"{pad+s.X/maxX*(width-2*pad):F1},{height/2-values[i]/maxAbs*(height/2-pad):F1}"));
        return $"<svg viewBox=\"0 0 {width} {height}\" role=\"img\"><line class=\"axis\" x1=\"{pad}\" y1=\"{height/2}\" x2=\"{width-pad}\" y2=\"{height/2}\"/><polyline class=\"curve\" points=\"{string.Join(" ",points)}\"/></svg>";
    }
    private static string H(string value)=>WebUtility.HtmlEncode(value);
    private static string F(double value)=>value.ToString("G17",Invariant);
}
