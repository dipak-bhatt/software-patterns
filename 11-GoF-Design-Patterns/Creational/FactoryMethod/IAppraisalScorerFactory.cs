namespace FactoryDesignPattern;

/// <summary>
/// CREATOR — declares the factory method. It returns the PRODUCT (<see cref="IAppraisalScorer"/>),
/// so callers never name a concrete scorer.
/// </summary>
/// <remarks>
/// This is the parameterised form of Factory Method: the template's appraisal type, not a subclass,
/// chooses the product. The classic subclass form is the same decision moved into one creator type
/// per product (for example <c>SupervisorLedScorerCreator</c> and <c>ThreeSixtyScorerCreator</c>);
/// the parameterised form is used when the choice is runtime data rather than a compile-time choice.
/// </remarks>
public interface IAppraisalScorerFactory
{
    IAppraisalScorer Create(int templateId);
}
