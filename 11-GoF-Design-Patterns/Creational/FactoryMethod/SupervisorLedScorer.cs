namespace FactoryDesignPattern;

/// <summary>
/// CONCRETE PRODUCT — a supervisor-led appraisal: the employee's own feedback is kept for the
/// appraisal conversation, but the overall rating is the supervisors' average.
/// </summary>
public sealed class SupervisorLedScorer : IAppraisalScorer
{
    public AppraisalType Type => AppraisalType.SupervisorLed;

    public AppraisalResult Score(IReadOnlyList<Feedback> feedback)
    {
        var supervisors = feedback.Where(f => f.Kind == RaterKind.Supervisor).ToList();
        if (supervisors.Count == 0)
            throw new InvalidOperationException("A supervisor-led appraisal needs feedback from at least one supervisor.");

        var overall = Math.Round(supervisors.Average(f => f.Points), 2);
        return new AppraisalResult(overall, $"Supervisor {overall:0.00} x 100% (employee and colleague feedback not counted)");
    }
}
