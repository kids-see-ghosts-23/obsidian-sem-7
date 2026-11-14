# Microservices Quick Revision

---

## What Are Microservices?

- The term emerged in the early 2010s for an architecture style built from small, independent components that work together.
- Microservices are hard: many independent parts must work together, and management, maintenance, support, and testing costs add up at scale.
- Done well, microservices make software changes faster and safer at scale.

Two definitions from the notes:

1. **Lewis and Fowler**: an approach to developing a single application as a suite of small services, each running in its own process and communicating with lightweight mechanisms, built around business capabilities and independently deployable by fully automated deployment machinery.
   - Nine characteristics: componentization via services; organization around business capabilities; decentralized governance; infrastructure automation; products not projects; smart endpoints and dumb pipes; design for failure; evolutionary design.

2. **Nadareishvili, Mitra, McLarty, and Amundsen**: a microservice is an independently deployable component of bounded scope that supports interoperability through message-based communication; microservice architecture is a style of engineering highly automated, evolvable software systems made up of capability-aligned microservices.

---

## Three General Design Traits

1. The application architecture is primarily composed of machine-invocable services made available on a network.
2. Service size is an important design factor; boundaries include runtime, design-time, and people factors.
3. The software system, the organization, and the way of working are holistically optimized to achieve a goal.

---

## Why Build Microservices

- The key reason is to reduce coordination costs.
- Microservices offer a way to increase speed without compromising safety, at scale.

---

## Coordination Cost Problem

- Complex software needs many people and a lot of time.
- Multiple teams build different parts on independent roadmaps and must periodically coordinate.
- That creates a tension between speed and safety.
- Microservices reduce coordination needs through autonomous teams working on small, isolated batches.

---

## Litmus Test

- When in doubt, ask: "Is this decision going to reduce coordination costs for my teams or not?"

---

## The Hard Parts

- **Long feedback loops**: impactful decisions are hard to measure; problems may emerge much later.
- **Too many moving parts**: microservices are a complex adaptive system with emergent behavior; small changes can break other things.
- **Analysis paralysis**: long feedback loops plus complexity make decisions highly impactful and hard to measure, leading to endless speculation instead of delivery.

---

## Learning by Doing

- Many successful implementers learned through iteration and often through failed architectures first.
- The book provides prescriptive guidance to accelerate learning.
- The Dreyfus model underlies the approach: start by following prescriptive guidance, then develop proficiency.

---

## Running Example

- A simplified airline reservations system with a read-only flight information service and a seat reservation service.

---

## The "Up and Running" Microservices Model

Focus areas:
1. Team design
2. Microservice design
3. Data design
4. Cloud platform
5. Microservice deployment
6. Release and change

---

## Operating Model

- The set of people, processes, and tools that underlies the system.
- It shapes decision-making and work; think of it as the operating system for the solution.

---

## Conway's Law

- Any organization that designs a system will produce a design whose structure is a copy of the organization's communication structure.

---

## People Factors

- **Team size**: too many people increases coordination; too few reduces capacity. The book sets team size at about five to eight people.
- **Team skills**: cross-functional teams make better decisions faster; members should directly influence output.
- **Inter-team coordination**: communication among teams can bog down a system; balance independence and coordination; design teams before architecture where possible (inverse Conway maneuver).

---

## Team Topologies

Four team types:
- **Stream-aligned**: owns and runs a deliverable piece of work and continually delivers business value; embodies "you build it, you run it."
- **Enabling**: supports other teams through consulting; specialists bridge gaps.
- **Complicated-subsystem**: works on domains that are difficult to understand and require scarce expertise.
- **Platform**: supports other teams through self-service tooling; scales through tools and processes.

Three interaction modes:
- **Collaboration**: both teams work closely together; good for learning and innovation; hard to scale.
- **Facilitating**: unidirectional support; one team helps another deliver.
- **X-as-a-service**: minimal coordination; one team provides a finished product, API, or platform for others to consume.

---

## Designing a Team Topology

Five steps:
1. Establish a system design team.
2. Create a microservices team template for future teams.
3. Define platform teams.
4. Add enabling and complicated-subsystem teams.
5. Add key consumer teams.

---

## SEED(S): Seven Essential Evolutions of Design for Services

SEED is a top-down, multi-step methodology in which each later step evolves from the previous one.

The seven steps:
1. Identifying factors: determine who and what interacts with the system, with customer-centric scoping.
2. Identifying jobs that actors have to do: outline goals and tasks each actor needs to accomplish, using the JTBD format: when "a circumstance", I want to "motivation", so I can "goal".
3. Discovering interaction patterns with sequence diagrams: map the flow of messages over time between actors and system components.
4. Deriving high-level actions and queries from JTBDs and interaction patterns: translate patterns into explicit commands and reads.
5. Describing each query and action as a specification with an open standard: use OpenAPI or GraphQL schemas.
6. Getting feedback on the API specification: review and validate with stakeholders and consumer teams before building.
7. Implementing microservices: write the backend code, set up databases, and implement services.

Key idea: microservices provide the implementation; APIs remain the outward-facing interface. Web APIs are layered on top of microservices.

---

## Rightsizing Microservices

- Do not draw boundaries based only on technical needs.
- Avoid creating too many microservices too soon.
- Start with a handful of services and increase slowly.

---

## Sam Newman's Three Rules

- **Loose coupling**: services operate without deep awareness of other services' internal implementation; a change in one should not force redeployment of another.
- **High cohesion**: behaviors that change for the same business reason are grouped inside the same service boundary.
- **Aligned with business capabilities**: organize services around what the business does, not technical tiers.

---

## DDD and Context Mapping

- A bounded context defines the range of applicability of each model.
- The representation of domains in a larger system and how they collaborate is the context map.
- Describing those collaborations is context mapping.
- A DDD aggregate is a collection of related domain objects viewed as a single unit by external consumers.

---

## Event Storming

- Finds bounded contexts in hours instead of weeks or months.
- It is not a full replacement for DDD but is a good approximation for discovering bounded contexts.
- Process:
  1. Discover domain events: participants write key domain events on orange stickies, one per note, on a timeline.
  2. Enforce the timeline: arrange notes into a coherent timeline and remove duplicates.
  3. Reverse narrative and command identification: walk the timeline backward and identify commands on blue stickies.
  4. Identify aggregates/bounded contexts: commands are accepted by aggregates and produce events; clusters around aggregates become bounded contexts.
  5. Competitive analysis: prioritize the bounded contexts.

---

## Universal Sizing Formula

- Bounded contexts are a great starting point, but microservice boundaries are not synonymous with bounded contexts.
- Boundaries evolve and tend toward increasing granularity as organizations and applications mature.
- It is typically easier to split a service than to merge services or move capabilities between services.

Three principles:
1. Start with few microservices, possibly using bounded contexts.
2. Keep splitting as the application and services grow, guided by the needs of coordination avoidance.
3. Be on the right trajectory for decreasing coordination; this is more important than how perfectly services are sized now.

---

## Data: Independent Deployability and Data Sharing

- Independent deployability means changing and deploying one microservice without changing or deploying any other microservice.
- Sharing data spaces is a primary killer of independent development and independent deployability.
- Microservices must own their data.
- Microservices can share physical database clusters.
- Microservices must own and embed their own data; no direct cross-service data access is allowed.

Isolation:
- **Logical isolation**: services must not share logical schemas, tables, or database spaces.
- **Physical isolation**: services can and should share physical database clusters to avoid massive infrastructure costs, provided they maintain strict logical isolation.

---

## Data Delegate Pattern

- When multiple services need the same core data, create an authoritative delegate service that wraps and hides the data.
- All other services must query or mutate the data solely through that service's API.

---

## Data Lakes

- Used when cross-boundary read access is required for analytics, machine learning, or auditing without hitting production service endpoints.
- Services stream copies of their data via message brokers into a data lake.
- Data lakes are read-only, queryable aggregates and are never the database of record.

---

## Saga Pattern

- A saga is a sequence of local transactions across multiple services.
- Every step executes its local action and registers a compensating action on a routing slip.
- Sagas achieve eventual consistency, not instant ACID rollback.
- Compensating actions are visible.
- Order steps carefully; steps that are hard to compensate should generally come later.

---

## Event Sourcing

- Instead of storing current state, store an append-only log of immutable facts.
- State is a transient derivative computed by folding over these facts.
- An event consists of:
  1. eventId, for example a UUID
  2. eventType
  3. data, a JSON payload describing change details
- State is computed with a projection function; current state is the left fold of events so far.
- Rolling snapshots save intermediary values to avoid recalculating from the beginning every time.
- Event stores can be simple; they must support storing events in order, notifying subscribers, and retrieving N events after event X.
- Competing Consumers: projection builders run multiple listener instances for redundancy and scalability; each event must be delivered only once to a single instance.
- Notification can be push-based, such as Kafka, or pull-based with HTTP callback endpoints.

---

## CQRS

- Command Query Responsibility Segregation: separate the write path from the read path.
- Write side: accepts commands and writes immutable facts to the event store.
- Read side: subscribes to events and builds specialized, read-optimized projection views.
- The event store prioritizes consistency; query projections prioritize availability and read speed.
- Use Event Sourcing and CQRS only when necessary; they complicate implementation.
- They are most useful when you need to avoid data sharing in sophisticated cases that require joins across service boundaries, or when auditability and time-based state reconstruction matter.
- Conventional relational modeling is often simpler.

---

## CAP Theorem

- A distributed shared-data system can have only two of consistency, availability, and partition tolerance.
- Network partitions cannot be avoided, so the real choice is between consistency and availability.
- With CQRS, use multiple systems and minimize data sharing: prioritize consistency in the event store and availability in the query indices.
- Query indices may get consistency wrong, but they are not authoritative and can be re-indexed from the event store.

---

## Auditability

- Event Sourcing preserves a perfect history of every change, including the value at any past time and how and when it changed.
- Logging to a file or to tools like Splunk or ELK is not Event Sourcing.
- In Event Sourcing, the event log is the source of truth; with Splunk logs, the source of truth is usually the relational model, and logs are used only occasionally to hunt bugs.

---

## Microservices and APIs

- Microservices are the implementation of the system.
- APIs are the outward-facing interface.
- Web APIs are layered on top of microservices.

---

## Decisions and ADRs

- Software and business outcomes depend on the quality of decisions.
- Decisions may become outdated as technology, people, or situations change.
- Architecture Decision Records capture important decisions so they can be re-evaluated and improved.
- Four elements: context, alternatives, choice, impact.
- Lightweight Architectural Decision Record (LADR) is concise, written in Markdown, and managed in version control like source code.
- LADR structure: title, status, context, decision, consequences.
