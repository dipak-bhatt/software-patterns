using FactoryDesignPattern;

const int yearEndAppraisal = 101;
const int leadershipAppraisal = 102;

Console.WriteLine("=== Factory Method Pattern — one appraisal scorer interface, two appraisal types ===\n");
Console.WriteLine("PRODUCT          IAppraisalScorer");
Console.WriteLine("CONCRETE PRODUCT SupervisorLedScorer   supervisors' feedback decides");
Console.WriteLine("                 ThreeSixtyScorer      employee + colleagues + supervisors, weighted");
Console.WriteLine("CREATOR          IAppraisalScorerFactory.Create(int templateId)");
Console.WriteLine("CONCRETE CREATOR TemplateScorerFactory — asks IAppraisalTemplateStore\n");

var templates = new InMemoryAppraisalTemplateStore()
    .Save(new AppraisalTemplate(yearEndAppraisal, "Year-End Appraisal FY2026", AppraisalType.SupervisorLed))
    .Save(new AppraisalTemplate(leadershipAppraisal, "Leadership 360 FY2026", AppraisalType.ThreeSixty));

IAppraisalScorerFactory factory = new TemplateScorerFactory(templates);

// The same feedback for Ram Bahadur, scored under each template.
Feedback[] feedback =
[
    new(RaterKind.Employee, "Ram Bahadur", 4.0m),
    new(RaterKind.Colleague, "Hari Thapa", 3.5m),
    new(RaterKind.Colleague, "Gita Karki", 4.5m),
    new(RaterKind.Supervisor, "Sita Sharma", 3.0m)
];

Console.WriteLine("--- The client holds one interface and names no appraisal type ---");
Console.WriteLine("    var scorer = factory.Create(templateId);");
Console.WriteLine("    var result = scorer.Score(feedback);\n");

foreach (var (templateId, label) in new[]
         {
             (yearEndAppraisal, "template 101: supervisor-led appraisal"),
             (leadershipAppraisal, "template 102: 360 appraisal")
         })
{
    Console.WriteLine($"  {label}");
    var scorer = factory.Create(templateId);
    var result = scorer.Score(feedback);
    Console.WriteLine($"    {result.Breakdown}");
    Console.WriteLine($"    Type = {scorer.Type}; Ram Bahadur's overall rating = {result.OverallRating:0.00}\n");
}

Console.WriteLine("--- The choice is per call, from the template's current type ---");
Console.WriteLine("    (HR switches template 101 to a 360 appraisal before the cycle opens)");
templates.Save(new AppraisalTemplate(yearEndAppraisal, "Year-End Appraisal FY2026", AppraisalType.ThreeSixty));
var switched = factory.Create(yearEndAppraisal);
Console.WriteLine($"  template 101 now resolves to {switched.Type}; the caller's code never changed\n");

Console.WriteLine("--- A locked template gets no scorer at all ---");
templates.Save(new AppraisalTemplate(leadershipAppraisal, "Leadership 360 FY2026", AppraisalType.ThreeSixty, IsLocked: true));
try
{
    factory.Create(leadershipAppraisal);
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"  {ex.Message}");
}

Console.WriteLine("\n=== Key Insight ===");
Console.WriteLine("One interface, two implementations, and one method that decides which to build.");
Console.WriteLine("The decision lives in the factory, so it is made in one place from the template's");
Console.WriteLine("appraisal type — not scattered across every service that scores an appraisal.");
Console.WriteLine("SupervisorLedScorer and ThreeSixtyScorer never know about each other,");
Console.WriteLine("and the client never knows which one it was handed.");
