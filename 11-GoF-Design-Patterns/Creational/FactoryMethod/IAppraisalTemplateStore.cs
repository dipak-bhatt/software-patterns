namespace FactoryDesignPattern;

/// <summary>The one input the factory method reads: the appraisal template being scored.</summary>
public interface IAppraisalTemplateStore
{
    AppraisalTemplate Get(int templateId);
}

/// <summary>An in-memory store, so the demo can change a template's type or lock it.</summary>
public sealed class InMemoryAppraisalTemplateStore : IAppraisalTemplateStore
{
    private readonly Dictionary<int, AppraisalTemplate> _templates = [];

    public InMemoryAppraisalTemplateStore Save(AppraisalTemplate template)
    {
        _templates[template.Id] = template;
        return this;
    }

    public AppraisalTemplate Get(int templateId) =>
        _templates.TryGetValue(templateId, out var template)
            ? template
            : throw new KeyNotFoundException($"Appraisal template {templateId} does not exist.");
}
