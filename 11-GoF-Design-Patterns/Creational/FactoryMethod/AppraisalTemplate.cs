namespace FactoryDesignPattern;

/// <summary>
/// An appraisal template HR sets up for a cycle. The factory method reads its type and whether it
/// is locked.
/// </summary>
public sealed record AppraisalTemplate(int Id, string Title, AppraisalType Type, bool IsLocked = false);
