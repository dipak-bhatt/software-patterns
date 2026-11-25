namespace FactoryDesignPattern;

/// <summary>
/// CONCRETE CREATOR — the body of the factory method. It looks up the appraisal template, refuses a
/// locked template, and otherwise builds one scorer per appraisal type and keeps it.
/// </summary>
public sealed class TemplateScorerFactory(IAppraisalTemplateStore templates) : IAppraisalScorerFactory
{
    private readonly Dictionary<AppraisalType, IAppraisalScorer> _built = [];

    public IAppraisalScorer Create(int templateId)
    {
        var template = templates.Get(templateId);

        // A locked template's ratings are final: no scorer may be built to rescore it.
        if (template.IsLocked)
            throw new InvalidOperationException(
                $"Appraisal template {template.Id} '{template.Title}' is locked; its ratings are final and no scorer may be created.");

        // Scorers are stateless, so one per appraisal type is enough.
        if (_built.TryGetValue(template.Type, out var existing))
            return existing;

        Console.WriteLine($"    [factory] template {template.Id} -> {template.Type,-13} -> creating scorer");
        IAppraisalScorer scorer = template.Type == AppraisalType.ThreeSixty
            ? new ThreeSixtyScorer()
            : new SupervisorLedScorer();
        _built[template.Type] = scorer;
        return scorer;
    }
}
