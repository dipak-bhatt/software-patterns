namespace FactoryDesignPattern;

/// <summary>
/// How an appraisal template is scored. The factory reads it per call and builds the matching
/// scorer; the choice is not fixed when the client is constructed.
/// </summary>
public enum AppraisalType
{
    SupervisorLed,
    ThreeSixty
}
