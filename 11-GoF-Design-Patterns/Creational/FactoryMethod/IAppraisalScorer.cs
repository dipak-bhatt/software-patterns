namespace FactoryDesignPattern;

/// <summary>
/// PRODUCT — the one interface the client depends on. It takes the feedback collected for an
/// employee and returns an overall rating; it never tells the client which rules produced it.
/// </summary>
public interface IAppraisalScorer
{
    /// <summary>Which appraisal type this scorer handles, shown for diagnostics in the demo.</summary>
    AppraisalType Type { get; }

    AppraisalResult Score(IReadOnlyList<Feedback> feedback);
}
