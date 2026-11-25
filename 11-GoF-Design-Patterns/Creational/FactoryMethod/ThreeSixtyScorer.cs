namespace FactoryDesignPattern;

/// <summary>
/// CONCRETE PRODUCT — a 360 appraisal: feedback from the employee, colleagues and supervisors each
/// carries a share of the overall rating, and the result is their weighted sum.
/// </summary>
public sealed class ThreeSixtyScorer : IAppraisalScorer
{
    private static readonly (RaterKind Kind, decimal Share)[] Shares =
    [
        (RaterKind.Employee, 0.20m),
        (RaterKind.Colleague, 0.30m),
        (RaterKind.Supervisor, 0.50m)
    ];

    public AppraisalType Type => AppraisalType.ThreeSixty;

    public AppraisalResult Score(IReadOnlyList<Feedback> feedback)
    {
        decimal overall = 0;
        var parts = new List<string>();

        foreach (var (kind, share) in Shares)
        {
            var fromKind = feedback.Where(f => f.Kind == kind).ToList();
            if (fromKind.Count == 0)
                throw new InvalidOperationException($"A 360 appraisal needs feedback from at least one {kind}.");

            var average = fromKind.Average(f => f.Points);
            overall += average * share;
            parts.Add($"{kind} {average:0.00} x {share * 100:0}%");
        }

        return new AppraisalResult(Math.Round(overall, 2), string.Join(" + ", parts));
    }
}
