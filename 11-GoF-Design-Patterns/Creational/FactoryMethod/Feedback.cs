namespace FactoryDesignPattern;

/// <summary>Who gave a piece of feedback, relative to the employee being appraised.</summary>
public enum RaterKind
{
    Employee,
    Colleague,
    Supervisor
}

/// <summary>One piece of feedback, as points on a 1-5 scale.</summary>
public sealed record Feedback(RaterKind Kind, string RaterName, decimal Points);

/// <summary>What every scorer returns: the overall rating and how it was reached.</summary>
public sealed record AppraisalResult(decimal OverallRating, string Breakdown);
