using EngineeringCalculator.Sections;

namespace EngineeringCalculator.Beams;

public sealed record SelectionCandidate(Section Section, BeamResult Result, double TotalMass);
public sealed record SelectionResult(IReadOnlyList<SelectionCandidate> Suitable, IReadOnlyList<SelectionCandidate> Checked)
{
    public SelectionCandidate? Best => Suitable.FirstOrDefault();
}

public sealed class SectionSelector
{
    private readonly BeamSolver _solver = new();

    public SelectionResult Select(BeamModel template, IEnumerable<Section>? sections = null, int limit = 10)
    {
        if (limit <= 0) throw new ArgumentOutOfRangeException(nameof(limit));
        var checkedItems = (sections ?? SectionCatalog.All)
            .Where(x => x.Family == "IPE")
            .Select(section => new SelectionCandidate(section, _solver.Solve(template with { Section = section }), section.Area * template.Material.Density * template.Length))
            .OrderBy(x => x.TotalMass)
            .ToArray();
        return new SelectionResult(checkedItems.Where(x => x.Result.Passes).Take(limit).ToArray(), checkedItems);
    }
}
