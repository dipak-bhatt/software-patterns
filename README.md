# Software Patterns

A catalog of software patterns used in real-world systems, from object-level design patterns (GoF) up to architecture, distributed-systems, resilience, data, security and operations patterns. Each pattern is implemented in **C# (.NET 10)** as a small, standalone, runnable console project with its own README explaining the problem, the solution, and the trade-offs.

> **Status legend:** ✅ implemented · ⬜ planned

---

## Table of Contents

1. [Getting Started](#getting-started)
2. [Repository Layout](#repository-layout)
3. [Pattern Catalog](#pattern-catalog)
   - [01 · Architectural Patterns](#01--architectural-patterns)
   - [02 · Distributed System Patterns](#02--distributed-system-patterns)
   - [03 · Resilience Patterns](#03--resilience-patterns)
   - [04 · Data Consistency & Messaging](#04--data-consistency--messaging)
   - [05 · Data Access & Persistence](#05--data-access--persistence)
   - [06 · Security Patterns](#06--security-patterns)
   - [07 · Performance & Scalability](#07--performance--scalability)
   - [08 · Integration Patterns](#08--integration-patterns)
   - [09 · Deployment & Operations](#09--deployment--operations)
   - [10 · Domain Patterns (DDD)](#10--domain-patterns-ddd)
   - [11 · GoF Design Patterns](#11--gof-design-patterns)
   - [12 · Concurrency Patterns](#12--concurrency-patterns)
   - [13 · Additional Object-Oriented Patterns](#13--additional-object-oriented-patterns)
   - [14 · Testing Patterns](#14--testing-patterns)
4. [Pattern Project Conventions](#pattern-project-conventions)
5. [Adding a New Pattern](#adding-a-new-pattern)
6. [References](#references)
7. [License](#license)

---

## Getting Started

### Prerequisites

- [.NET SDK 10.0](https://dotnet.microsoft.com/download) or later (pinned in `global.json`)
- Any editor: Visual Studio 2022 17.14+, JetBrains Rider, or VS Code with C# Dev Kit

### Build everything

```bash
dotnet build SoftwarePatterns.slnx
```

### Run a single pattern

Each pattern is its own console app. Change into its folder and run it:

```bash
cd 11-GoF-Design-Patterns/Creational/Singleton
dotnet run
```

---

## Repository Layout

```
software-patterns/
│
├── SoftwarePatterns.slnx           # Solution containing every pattern project
├── Directory.Build.props           # Shared build settings (net10.0, nullable, implicit usings)
├── global.json                     # Pinned .NET SDK version
├── README.md
├── LICENSE
│
├── 01-Architectural-Patterns/
├── 02-Distributed-System-Patterns/
├── 03-Resilience-Patterns/
├── 04-Data-Consistency-Messaging/
├── 05-DataAccess-Persistence/
├── 06-Security-Patterns/
├── 07-Performance-Scalability/
├── 08-Integration-Patterns/
├── 09-Deployment-Operations/
├── 10-Domain-Patterns/
├── 11-GoF-Design-Patterns/
│   ├── Creational/
│   ├── Structural/
│   └── Behavioral/
├── 12-Concurrency-Patterns/
├── 13-Additional-OO-Patterns/
└── 14-Testing-Patterns/
```

Inside every category, each pattern lives in its own folder named in PascalCase (for example `03-Resilience-Patterns/CircuitBreaker/`).

---

## Pattern Catalog

### 01 · Architectural Patterns

How a whole application is structured: where the boundaries are and which way the dependencies point.

| Pattern | Intent | Status |
|---------|--------|:------:|
| Layered Architecture | Splits the application into horizontal layers (presentation, business, data). Each layer depends only on the one below it. | ⬜ |
| Clean Architecture | Puts the domain at the centre and makes every dependency point inward, so frameworks and databases become replaceable details. | ⬜ |
| Onion Architecture | Arranges concentric rings (domain model → domain services → application → infrastructure) with dependencies that point inward. | ⬜ |
| Hexagonal Architecture (Ports & Adapters) | Isolates the core behind ports. Adapters plug in UI, databases and external systems. | ⬜ |
| Modular Monolith | Ships one deployable unit that is internally split into strongly bounded, independently owned modules. | ⬜ |
| Vertical Slice Architecture | Organises code by feature (one request = one slice) instead of by technical layer. | ⬜ |
| CQRS | Separates the write model (commands) from the read model (queries) so each can be optimised on its own. | ⬜ |
| Event-Driven Architecture | Has components communicate by producing and reacting to events instead of calling each other directly. | ⬜ |
| Microservices | Decomposes a system into small, independently deployable services, each owning its own data. | ⬜ |
| Microkernel (Plug-in) | Keeps a minimal core system that is extended with independently developed plug-ins. | ⬜ |
| Pipes and Filters | Breaks processing into independent steps (filters) that are connected by channels (pipes). | ⬜ |
| MVC / MVVM | Separates UI state, presentation logic and rendering. | ⬜ |

### 02 · Distributed System Patterns

Coordination, routing and decomposition across process and network boundaries.

| Pattern | Intent | Status |
|---------|--------|:------:|
| Saga (Orchestration & Choreography) | Runs a long-lived business transaction as a series of local transactions with compensating actions. | ⬜ |
| API Gateway | Gives clients a single entry point that routes, aggregates and applies cross-cutting concerns. | ⬜ |
| Backend for Frontend (BFF) | Gives each client type (web, mobile) a dedicated backend tailored to its needs. | ⬜ |
| Service Discovery | Lets services find each other's network locations dynamically through a registry. | ⬜ |
| Strangler Fig | Migrates a legacy system gradually by routing features to the new implementation one at a time. | ⬜ |
| Sidecar | Deploys helper functionality (logging, proxying, config) in a separate process alongside the main service. | ⬜ |
| Ambassador | Uses an out-of-process proxy that handles outbound connectivity concerns (retries, TLS, monitoring) for a service. | ⬜ |
| Scatter-Gather | Broadcasts a request to many recipients and aggregates their replies into one response. | ⬜ |
| Two-Phase Commit (2PC) | Coordinates an atomic commit across multiple resources through prepare and commit phases. | ⬜ |
| Leader-Follower Replication | Sends all writes to one leader node that replicates them to followers. | ⬜ |

### 03 · Resilience Patterns

How a system keeps working when its dependencies are slow, failing or overloaded.

| Pattern | Intent | Status |
|---------|--------|:------:|
| Circuit Breaker | Stops calling a failing dependency for a while so it can recover, and fails fast instead. | ⬜ |
| Retry with Exponential Backoff | Retries transient failures with growing, jittered delays. | ⬜ |
| Timeout | Bounds how long a caller waits for a response. | ⬜ |
| Bulkhead | Isolates resources (threads, connections) per dependency so one failure cannot sink the whole system. | ⬜ |
| Fallback | Returns a degraded but acceptable response when the primary path fails. | ⬜ |
| Rate Limiting | Caps how many requests a client may make in a time window. | ⬜ |
| Load Shedding | Deliberately rejects low-priority work under overload to protect critical work. | ⬜ |
| Hedged Requests | Sends a duplicate request to another replica if the first is slow, then uses whichever answers first. | ⬜ |

### 04 · Data Consistency & Messaging

Reliable messaging and keeping data consistent across services.

| Pattern | Intent | Status |
|---------|--------|:------:|
| Transactional Outbox | Writes business data and outgoing events in the same local transaction, then publishes the events reliably. | ⬜ |
| Inbox / Idempotent Consumer | Records processed message IDs so duplicate deliveries have no extra effect. | ⬜ |
| Change Data Capture (CDC) | Streams row-level changes from a database log to downstream consumers. | ⬜ |
| Event Sourcing | Stores state as an append-only sequence of events and rebuilds current state by replaying them. | ⬜ |
| Competing Consumers | Lets several consumers read from one queue to process messages in parallel. | ⬜ |
| Dead Letter Queue | Moves messages that cannot be processed aside for inspection instead of blocking the queue. | ⬜ |
| Publish-Subscribe | Delivers each published message to every interested subscriber. | ⬜ |
| Request-Reply | Correlates a reply message with the request that caused it over asynchronous channels. | ⬜ |
| Claim Check | Stores a large payload externally and sends only a reference through the message bus. | ⬜ |
| Priority Queue | Processes higher-priority messages before lower-priority ones. | ⬜ |

### 05 · Data Access & Persistence

How application code talks to storage.

| Pattern | Intent | Status |
|---------|--------|:------:|
| Repository | Puts a collection-like interface over persistence for aggregate roots. | ⬜ |
| Unit of Work | Tracks changes made during a business transaction and commits them atomically. | ⬜ |
| Data Mapper | Moves data between objects and the database while keeping them independent of each other. | ⬜ |
| Active Record | Makes an object wrap a database row and carry its own persistence methods. | ⬜ |
| Table Data Gateway | Gives one object that handles all SQL access for a single table. | ⬜ |
| Identity Map | Ensures each row is loaded only once per unit of work by caching loaded objects by key. | ⬜ |
| Lazy Load | Defers loading related data until it is actually accessed. | ⬜ |
| Optimistic Concurrency | Detects conflicting updates with a version or row-stamp check instead of locks. | ⬜ |
| Query Object | Represents a database query as an object that can be built up and reused. | ⬜ |
| Database per Service | Has each service own its own private database, with no shared tables. | ⬜ |

> CQRS lives in [01 · Architectural Patterns](#01--architectural-patterns).

### 06 · Security Patterns

| Pattern | Intent | Status |
|---------|--------|:------:|
| Least Privilege | Grants each component only the permissions it strictly needs. | ⬜ |
| Defense in Depth | Layers independent security controls so one breach is not total. | ⬜ |
| Tenant Isolation | Guarantees that one tenant can never read or affect another tenant's data (e.g. row-level security). | ⬜ |
| Token-Based Authentication | Authenticates requests with signed, short-lived tokens (JWT / OAuth 2.0 / OIDC). | ⬜ |
| Federated Identity | Delegates authentication to a trusted external identity provider. | ⬜ |
| Role- / Attribute-Based Access Control | Authorises actions by role or by attributes of the user, resource and context. | ⬜ |
| Gatekeeper | Puts a dedicated host in front of services that validates and sanitises requests. | ⬜ |
| Valet Key | Gives clients a scoped, time-limited token for direct access to a specific resource. | ⬜ |
| Secrets Management | Keeps credentials out of code and config, fetched from a vault at runtime. | ⬜ |
| Audit Logging | Records who did what and when in a tamper-evident log. | ⬜ |

### 07 · Performance & Scalability

| Pattern | Intent | Status |
|---------|--------|:------:|
| Cache-Aside | Has the application read from the cache first and populate it on a miss. | ⬜ |
| Write-Through / Write-Behind Cache | Writes go through the cache, to the store either synchronously or asynchronously. | ⬜ |
| Read Replica | Offloads read traffic to replicated copies of the primary database. | ⬜ |
| Sharding | Partitions data horizontally across multiple databases by a shard key. | ⬜ |
| Pagination (Offset & Keyset) | Returns large result sets in bounded pages. | ⬜ |
| Asynchronous Processing | Moves slow work out of the request path into background workers. | ⬜ |
| Queue-Based Load Leveling | Puts a queue between producer and consumer to smooth out traffic spikes. | ⬜ |
| Materialized View | Precomputes and stores a query-optimised view of data. | ⬜ |
| Load Balancing | Distributes requests across multiple instances. | ⬜ |
| Object Pool | Reuses expensive objects (connections, buffers) instead of creating them each time. | ⬜ |

### 08 · Integration Patterns

Connecting systems that were not designed to talk to each other (based on *Enterprise Integration Patterns*).

| Pattern | Intent | Status |
|---------|--------|:------:|
| Anti-Corruption Layer | Adds a translation layer that keeps a foreign model from leaking into your domain. | ⬜ |
| Channel Adapter | Connects an application to a messaging system without changing the application. | ⬜ |
| Messaging (Point-to-Point) | Integrates systems by exchanging messages through a channel. | ⬜ |
| Message Translator | Converts a message from one format to another. | ⬜ |
| Content-Based Router | Routes each message to a destination based on its content. | ⬜ |
| Aggregator | Combines related messages into a single message. | ⬜ |
| Canonical Data Model | Defines one shared message format that every integration translates to and from. | ⬜ |
| Webhook | Has a system notify subscribers by calling their HTTP endpoint when an event happens. | ⬜ |

> For the object-level Adapter, see [GoF › Structural › Adapter](11-GoF-Design-Patterns/Structural/Adapter/).

### 09 · Deployment & Operations

| Pattern | Intent | Status |
|---------|--------|:------:|
| Health Checks (Liveness / Readiness) | Exposes endpoints that tell orchestrators whether an instance is alive and ready. | ⬜ |
| Blue-Green Deployment | Runs two identical environments and switches traffic between them for zero-downtime releases. | ⬜ |
| Canary Release | Rolls a new version out to a small share of users first. | ⬜ |
| Rolling Deployment | Replaces instances in batches, a few at a time. | ⬜ |
| Feature Toggles | Turns functionality on or off at runtime without redeploying. | ⬜ |
| External Configuration Store | Moves configuration out of the deployment package into a central store. | ⬜ |
| Leader Election | Has one instance among many coordinate work that must run only once. | ⬜ |
| Distributed Tracing | Propagates a trace context across services to follow a request end to end. | ⬜ |

### 10 · Domain Patterns (DDD)

Modelling business logic (based on *Domain-Driven Design*).

| Pattern | Intent | Status |
|---------|--------|:------:|
| Domain Model | Uses an object model that combines behaviour and data for the business domain. | ⬜ |
| Entity | Is an object defined by a persistent identity rather than by its attributes. | ⬜ |
| Value Object | Is an immutable object defined only by its values, with no identity. | ⬜ |
| Aggregate | Is a consistency boundary that is modified only through its root. | ⬜ |
| Domain Events | Records something meaningful that happened in the domain, for other parts to react to. | ⬜ |
| Domain Service | Holds domain logic that does not naturally belong to one entity or value object. | ⬜ |
| Specification | Encapsulates a business rule as a composable, reusable predicate. | ⬜ |
| Policy | Is a pluggable business rule that decides an outcome (pricing, eligibility). | ⬜ |
| Process Manager | Coordinates a multi-step business process by reacting to events and issuing commands. | ⬜ |
| State Machine | Models an entity's lifecycle as explicit states and allowed transitions. | ⬜ |
| Bounded Context | Is an explicit boundary within which a particular domain model applies. | ⬜ |

### 11 · GoF Design Patterns

The 23 classic patterns from *Design Patterns: Elements of Reusable Object-Oriented Software* (Gamma, Helm, Johnson, Vlissides). **All 23 are implemented.** Each one uses a realistic business scenario (identity, HR, payroll, multi-tenant SaaS) instead of toy examples.

#### Creational — how objects are created

| Pattern | Scenario | Intent | Status |
|---------|----------|--------|:------:|
| [Abstract Factory](11-GoF-Design-Patterns/Creational/AbstractFactory/) | Per-tenant identity store (SQL Server / PostgreSQL) | Creates a family of related products for one variant, so the family can never be mixed. | ✅ |
| [Builder](11-GoF-Design-Patterns/Creational/Builder/) | Audit event construction | Assembles a complex immutable object step by step, validating it before it is published. | ✅ |
| [Factory Method](11-GoF-Design-Patterns/Creational/FactoryMethod/) | Product catalog by storage kind | Declares one creation method that returns a product interface and decides the concrete class per call. | ✅ |
| [Prototype](11-GoF-Design-Patterns/Creational/Prototype/) | Deep copy of an object graph | Copies an existing object, including its whole child graph, instead of building one from scratch. | ✅ |
| [Singleton](11-GoF-Design-Patterns/Creational/Singleton/) | Tenant connection pool; classic, `Lazy<T>` and DI forms | Ensures a class has exactly one instance with a global point of access. | ✅ |

#### Structural — how objects are composed

| Pattern | Scenario | Intent | Status |
|---------|----------|--------|:------:|
| [Adapter](11-GoF-Design-Patterns/Structural/Adapter/) | Employee directory → reader interface | Lets two incompatible interfaces work together by wrapping one of them. | ✅ |
| [Bridge](11-GoF-Design-Patterns/Structural/Bridge/) | Employee service × database engine | Decouples abstraction from implementation so both can vary independently. | ✅ |
| [Composite](11-GoF-Design-Patterns/Structural/Composite/) | Organisation hierarchy | Treats individual objects and compositions uniformly through a tree structure. | ✅ |
| [Decorator](11-GoF-Design-Patterns/Structural/Decorator/) | Caching, auditing, retry and metrics wrappers | Adds behaviour to an object by wrapping it in objects with the same interface. | ✅ |
| [Facade](11-GoF-Design-Patterns/Structural/Facade/) | Employee offboarding | Provides one simple entry point over a group of subsystem calls. | ✅ |
| [Flyweight](11-GoF-Design-Patterns/Structural/Flyweight/) | Shared row-level security command prefixes | Shares immutable state across many objects and passes the varying state in per call. | ✅ |
| [Proxy](11-GoF-Design-Patterns/Structural/Proxy/) | Routing proxy and protection proxy | Stands in for an object with the same interface to route or guard access. | ✅ |

#### Behavioral — how objects interact and share responsibility

| Pattern | Scenario | Intent | Status |
|---------|----------|--------|:------:|
| [Chain of Responsibility](11-GoF-Design-Patterns/Behavioral/ChainOfResponsibility/) | HTTP request pipeline (tenant, session, endpoint) | Passes a request along a chain of handlers until one answers it. | ✅ |
| [Command](11-GoF-Design-Patterns/Behavioral/Command/) | Payroll run, both immediate and queued with a worker | Turns each operation into an object that can be executed, queued or logged. | ✅ |
| [Interpreter](11-GoF-Design-Patterns/Behavioral/Interpreter/) | Salary formula evaluator | Represents each grammar rule as a class and evaluates the expression tree. | ✅ |
| [Iterator](11-GoF-Design-Patterns/Behavioral/Iterator/) | Tenant run rotation | Walks a collection without exposing its storage, with lazy stop rules. | ✅ |
| [Mediator](11-GoF-Design-Patterns/Behavioral/Mediator/) | Approval workflow | Centralises interaction between related modules behind one coordinator. | ✅ |
| [Memento](11-GoF-Design-Patterns/Behavioral/Memento/) | Undo / redo | Captures state in an opaque snapshot so it can be restored later. | ✅ |
| [Observer](11-GoF-Design-Patterns/Behavioral/Observer/) | Event-driven notifications | Notifies independent subscribers when a subject changes, without the subject knowing them. | ✅ |
| [State](11-GoF-Design-Patterns/Behavioral/State/) | Exam lifecycle | Changes behaviour when the lifecycle state changes, with one class per state. | ✅ |
| [Strategy](11-GoF-Design-Patterns/Behavioral/Strategy/) | File archive job | Puts interchangeable algorithms behind one interface, so they can be swapped at runtime. | ✅ |
| [Template Method](11-GoF-Design-Patterns/Behavioral/TemplateMethod/) | Catalog bulk import | Fixes an algorithm's steps in a base class and lets subclasses supply individual steps. | ✅ |
| [Visitor](11-GoF-Design-Patterns/Behavioral/Visitor/) | SQL routine analyzer | Adds new operations over a stable object structure without changing its element classes. | ✅ |

### 12 · Concurrency Patterns

| Pattern | Intent | Status |
|---------|--------|:------:|
| Producer-Consumer | Decouples work producers from consumers through a bounded buffer (`System.Threading.Channels`). | ⬜ |
| Thread Pool | Reuses a fixed set of worker threads for many short tasks. | ⬜ |
| Async / Await (Future-Promise) | Represents a value that will be available later without blocking a thread. | ⬜ |
| Active Object | Decouples method invocation from execution by running calls on the object's own thread. | ⬜ |
| Monitor Object | Synchronises access to an object so only one thread runs its methods at a time. | ⬜ |
| Read-Write Lock | Allows concurrent readers but exclusive writers. | ⬜ |
| Double-Checked Locking | Reduces locking overhead for lazy initialisation (and shows why `Lazy<T>` is preferred). | ⬜ |
| Actor Model | Has isolated actors that hold private state and communicate only through asynchronous messages. | ⬜ |

### 13 · Additional Object-Oriented Patterns

Widely used object-level patterns that are not part of the original GoF catalog.

| Pattern | Intent | Status |
|---------|--------|:------:|
| Dependency Injection | Supplies an object's dependencies from outside rather than having it create them. | ⬜ |
| Null Object | Replaces `null` checks with an object that does nothing safely. | ⬜ |
| Lazy Initialization | Delays creating an expensive object until first use. | ⬜ |
| Multiton | Keeps one instance per key (a keyed Singleton). | ⬜ |
| Service Locator *(anti-pattern discussion)* | Uses a central registry to resolve dependencies, and shows why DI is usually preferred. | ⬜ |
| Fluent Interface | Uses method chaining that reads like a sentence. | ⬜ |
| Result / Either | Returns success or failure as a value instead of throwing exceptions for expected errors. | ⬜ |

### 14 · Testing Patterns

| Pattern | Intent | Status |
|---------|--------|:------:|
| Arrange-Act-Assert | Uses a standard three-part structure for readable tests. | ⬜ |
| Test Doubles (Dummy, Stub, Spy, Mock, Fake) | Substitutes collaborators to isolate the unit under test. | ⬜ |
| Test Data Builder | Uses a fluent builder that creates valid test objects with overridable defaults. | ⬜ |
| Object Mother | Uses a factory of ready-made, named test fixtures. | ⬜ |
| Contract Testing | Verifies that a provider still satisfies the expectations of its consumers. | ⬜ |

---

## Pattern Project Conventions

Every pattern folder follows the same shape:

```
<Category>/<PatternName>/
├── <pattern-name>.csproj   # Console app targeting net10.0
├── Program.cs              # Runnable demo: the entry point that shows the pattern in action
├── README.md               # Definition, problem, structure, code walkthrough, trade-offs, when to use / avoid
└── ...                     # Pattern participants, grouped into sub-folders when there are many
```

Each pattern README should answer:

1. **Definition:** what the pattern is, in one or two sentences.
2. **Problem:** the concrete pain it removes, with a realistic scenario.
3. **Structure:** the participants and how they relate.
4. **Walkthrough:** the key code and the demo's console output.
5. **Trade-offs:** when to use it, when not to, and related patterns.

---

## Adding a New Pattern

```bash
# 1. Create the project inside its category folder
dotnet new console -n circuit-breaker-pattern -o 03-Resilience-Patterns/CircuitBreaker

# 2. Add it to the solution under the matching solution folder
dotnet sln SoftwarePatterns.slnx add 03-Resilience-Patterns/CircuitBreaker/circuit-breaker-pattern.csproj --solution-folder 03-Resilience-Patterns

# 3. Write Program.cs + README.md, then flip the status in this README from ⬜ to ✅
```

Shared settings (`TargetFramework`, `Nullable`, `ImplicitUsings`, `LangVersion`) come from `Directory.Build.props`, so new `.csproj` files can stay minimal.

---

## References

- *Design Patterns: Elements of Reusable Object-Oriented Software*: Gamma, Helm, Johnson, Vlissides (1994)
- *Patterns of Enterprise Application Architecture*: Martin Fowler (2002)
- *Enterprise Integration Patterns*: Gregor Hohpe, Bobby Woolf (2003)
- *Domain-Driven Design*: Eric Evans (2003)
- *Release It!*: Michael T. Nygard (2nd ed., 2018)
- *Microservices Patterns*: Chris Richardson (2018)
- *Designing Data-Intensive Applications*: Martin Kleppmann (2017)
- *Pattern-Oriented Software Architecture, Vol. 2: Patterns for Concurrent and Networked Objects*: Schmidt et al. (2000)
- *xUnit Test Patterns*: Gerard Meszaros (2007)
- [microservices.io](https://microservices.io/patterns/)

---

## License

[MIT](LICENSE)
