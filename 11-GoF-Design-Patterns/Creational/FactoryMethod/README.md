# Factory Method Design Pattern

## Definition

The Factory Method pattern **defines one interface for creating an object and lets the creation
site decide which concrete object to build**. The caller asks for the product through the
interface and never names a concrete class, so the choice can change without touching the caller.

The pattern has four roles:

| Role | In this demo |
|------|--------------|
| **Product** | `IAppraisalScorer` — what the client depends on |
| **Concrete Product** | `SupervisorLedScorer`, `ThreeSixtyScorer` |
| **Creator** | `IAppraisalScorerFactory` — declares `Create(int templateId)` |
| **Concrete Creator** | `TemplateScorerFactory` — the factory method's body |

## HRMS Context

In an HRMS, HR sets up an **appraisal template** for each appraisal cycle, and every template has
an **appraisal type**:

- **Supervisor-led** — the employee may give their own feedback, but the overall rating is the
  supervisors' average.
- **360** — feedback from the employee, colleagues and supervisors is combined by fixed shares,
  here 20% / 30% / 50%.

Every service that scores an appraisal needs "the scorer for this template". The factory method
reads the template's type and builds the matching scorer, so the rule lives in one place.

This demo uses the **parameterised** form: the template's appraisal type, not a subclass, chooses
the product. The classic GoF form moves the same decision into one creator type per product
(`SupervisorLedScorerCreator`, `ThreeSixtyScorerCreator`). Use the subclass form when the choice is
made at compile time; use the parameterised form when it comes from runtime data.

## Structure

```
          Client
             │ holds the creator + product interfaces
             ▼
   ┌─────────────────────────┐        ┌─────────────────────────┐
   │ IAppraisalScorerFactory │        │  IAppraisalScorer       │
   │  Create(templateId)     │───────▶│  Score(feedback)        │
   └────────────▲────────────┘        └────────────▲────────────┘
                │                                  │
   ┌────────────┴────────────┐        ┌────────────┴────────────┐
   │ TemplateScorerFactory   │        │ SupervisorLedScorer     │
   │                         │        ├─────────────────────────┤
   │ reads AppraisalType ────┼───┐    │ ThreeSixtyScorer        │
   └─────────────────────────┘   │    └─────────────────────────┘
                                 ▼
                      IAppraisalTemplateStore
                (supervisor-led / 360 / locked)
```

The client holds `IAppraisalScorerFactory` and `IAppraisalScorer`. It never names
`SupervisorLedScorer` or `ThreeSixtyScorer`. Change the template's type and the next `Create`
builds the other product — the client code does not change.

## Project Layout

```
factory-design-pattern/
├── AppraisalType.cs              — the per-call creation condition
├── AppraisalTemplate.cs          — the template the factory reads
├── Feedback.cs                   — feedback given and the result both products return
├── IAppraisalScorer.cs           — PRODUCT interface
├── SupervisorLedScorer.cs        — CONCRETE PRODUCT (supervisors' feedback decides)
├── ThreeSixtyScorer.cs           — CONCRETE PRODUCT (employee + colleagues + supervisors)
├── IAppraisalTemplateStore.cs    — the input the factory method reads
├── IAppraisalScorerFactory.cs    — CREATOR (declares the factory method)
├── TemplateScorerFactory.cs      — CONCRETE CREATOR (the factory method's body)
├── Program.cs                    — composition root / demo
├── factory-design-pattern.csproj
└── README.md
```

## How to Run

```bash
cd 11-GoF-Design-Patterns/Creational/FactoryMethod
dotnet run
```

Requires the .NET 10 SDK.

## Output

```
=== Factory Method Pattern — one appraisal scorer interface, two appraisal types ===

PRODUCT          IAppraisalScorer
CONCRETE PRODUCT SupervisorLedScorer   supervisors' feedback decides
                 ThreeSixtyScorer      employee + colleagues + supervisors, weighted
CREATOR          IAppraisalScorerFactory.Create(int templateId)
CONCRETE CREATOR TemplateScorerFactory — asks IAppraisalTemplateStore

--- The client holds one interface and names no appraisal type ---
    var scorer = factory.Create(templateId);
    var result = scorer.Score(feedback);

  template 101: supervisor-led appraisal
    [factory] template 101 -> SupervisorLed -> creating scorer
    Supervisor 3.00 x 100% (employee and colleague feedback not counted)
    Type = SupervisorLed; Ram Bahadur's overall rating = 3.00

  template 102: 360 appraisal
    [factory] template 102 -> ThreeSixty    -> creating scorer
    Employee 4.00 x 20% + Colleague 4.00 x 30% + Supervisor 3.00 x 50%
    Type = ThreeSixty; Ram Bahadur's overall rating = 3.50

--- The choice is per call, from the template's current type ---
    (HR switches template 101 to a 360 appraisal before the cycle opens)
  template 101 now resolves to ThreeSixty; the caller's code never changed

--- A locked template gets no scorer at all ---
  Appraisal template 102 'Leadership 360 FY2026' is locked; its ratings are final and no scorer may be created.

=== Key Insight ===
One interface, two implementations, and one method that decides which to build.
The decision lives in the factory, so it is made in one place from the template's
appraisal type — not scattered across every service that scores an appraisal.
SupervisorLedScorer and ThreeSixtyScorer never know about each other,
and the client never knows which one it was handed.
```

## Why Factory Method?

- **The caller names no concrete class.** `Program.cs` holds `IAppraisalScorerFactory` and
  `IAppraisalScorer`; the `new` for a scorer appears in exactly one method.
- **The decision has one home.** The "which appraisal type" rule sits in the factory, not in the
  appraisal summary, the ratings report, the export and every other service that scores feedback.
- **The product can change per call.** The template is read on every `Create`, so when HR switches
  a template from supervisor-led to 360 before the cycle opens, the next request gets the other scorer.
- **Construction can refuse.** A locked template throws before either scorer is built, which is a
  rule of *creation*, not of scoring.

## When to Use

- A family of classes implements one interface and the right one depends on runtime state
  (appraisal type, leave type, pay frequency, country of employment, feature flag)
- Construction is non-trivial or must be centralised (configuration, lifetime, caching, guarding)
- You want callers to depend on an abstraction and not on `new`
- The set of concrete products is expected to grow (for example a project-based appraisal), and
  the choice should be updated in one place

## Factory Method vs. Related Patterns

- **Simple Factory** is a single class with a `switch`; **Factory Method** puts the method on a
  creator abstraction so it can be varied or overridden.
- **Abstract Factory** creates a *family* of related products; **Factory Method** creates one
  product.
- **Builder** constructs one complex object step by step; **Factory Method** chooses which object
  to construct in one call.
- **Strategy** swaps an algorithm the caller already holds; **Factory Method** decides which
  strategy/product the caller receives. Here the scorers are strategies, and the factory method is
  how the client gets the right one.
- **Proxy** wraps an existing object with the same interface; **Factory Method** creates the object
  in the first place.

## Notes on This Repo

The example is self-contained: no database, no NuGet packages beyond the .NET SDK, and no
environment configuration. It is intended to be read next to the other pattern demos in this
repository and run on its own.
