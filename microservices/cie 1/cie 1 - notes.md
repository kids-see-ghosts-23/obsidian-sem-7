---
share_link: https://share.note.sx/g7r2n4tc#abxVin3f2dEjIQ3YKtiuu0Yl2ZgWpnzBumIL7qlMCzQ
share_updated: 2026-10-11T18:13:42+05:30
---
# UNIT 1
## Toward a microservices architecture

"microservices" emerged in early 2010s to describe a style of software architecture in which applications are built from small, independent components tat work together.

microservices are hard, making many independent parts work together is harder than it sounds and management, maintenance, support and testing costs, but it changes faster and safer at scale

### Microservices

Microservices are an approach to developing a singe application as a  suite of small services, each running in its own process and communication with lightweight mechanisms, built around business capabilities and independently deployable by fully automated deployment machinery (lewis and fowler)

-> nine characteristics
- compensation via services
- organization around business capabilities
- decentralized governance
- infrastructure automation
- products not projects
- smart endpoints and dumb pipes
- design for failure
- evolutionary design

Microservices is an independently deployable component of bounded scope that supports interoperability through message based communication.

Microservices architecture is a style of engineering highly automated,
evolvable software system made up of capability aligned microservices 

### Three design traits

1. the application architecture is primarily composed of machine invocable services made available on network
2. size of services is an important design factor. boundaries include runtime, design time and people factor
3. the software system, the organization and the way of working are holistically optimized to achieve a goal

why you build microservices -> **reducing coordination costs**

Microservices offer a way to increase speed without compromising safety, at scale

### hard parts

**long feedback loops** - impact decisions are not easy to measure. problems any emerge only much later.*You don't know if a decision was good or bad until months later.*
**too many moving parts** - a microservices system is a complex adaptive system; each part affects the other, and together they produce emergent behaviour. *Changing one small thing unexpectedly breaks three other things.*
**analysis paralysis** - long feedback loops plus complex system mean decisions are both highly impactful and hard to measure. this leads to endless speculations and discussions out of fear of building the wrong system, instead of delivering business outcomes. *Getting so scared of making a mistake that you spend all your time arguing instead of building.*

### The Up and running microservices model
 - team design
 - microservice design
 - data design
 - cloud platform
 - microservice deployment
 - release and change
 we need  away to capture important decisions so they can be re evaluated and improved. The tool used is architecture decision record(adr).
**Four elements of good decision record**
- context
- alternatives
- choice
- impact
#### lightweight architecture decision record (LADR)

it is concise, written in md files and can be managed in version control like source code.
**structure**
- title 
- status
- context
- decision
- consequences

---------------------------END OF CHAPTER 1------------------------------

## Designing a microservices operating model

**operating model** - the set of people, processes and tools that underlies your system. It shapes all decision making and work, and can define team responsibilites and governance over decisions and work, think f it as the operating system for your solution.

**conways law** - any organization that designs a system will produce a design whose strucrue is acopy of the organizaton communication structure.

3 people factors impact on microservices system:
- team size - team size should be limited
- team skills - principles for team membership
- inter team coordination - when to design teams and coordination models

### team topologies

team topologies is a design approach, provides a formal language for team design with focus on how teams work with each otehr.
- team types
- team interaction modes 
- diagramming

**Four team types**

- **steam aligned** - owns and runs a deliverable piece of work and continually delivers something relevant to the business. *Owns a specific business feature from start to finish and ships value directly to users.*
- **enabling** - supports other teams with a consulting engagement mode. specialists and subject matter experts bridge gaps in expertise or help others see the bigger picture. *Mentors and teaches other teams to level up their skills and bridge knowledge gaps.*
- **complicated subsystem** - works in a domain that is difficult to understand, where expertise is scarce and cannot be embedded in every team. *Handles deep, highly specialized technical work that requires rare expertise.*
- **platform** - supports other team like and enabling team but delivers a self service experience. invests in tools and processes that scale. needs up front investment and continual support. *Builds self-service tools and infrastructure so other teams can deliver faster without reinventing the wheel.*

**3 interaction modes**

- **collaboration** - both teams work closely together. good for learning, discovery and innovation, but needs high coordination and is difficult to scale. *Two teams work closely side-by-side as equals to solve a shared problem.*
- **facilitating** - like collaboration but unidirectional, one team plays a support role to help the other deliver its outcome. *One team acts as a coach or guide to help another team learn and unblock themselves.*
- **x as a service** - consumer provider relationship is minimal coordination. a team releases a shared process, document ,library, api or platform. scales well natural fit for platform teams. *One team provides a finished product, API, or service that other teams consume with zero back-and-forth required.*

**diagramming**

**five steps** 
- **establish a system design team** - assembly of senior architects and lead engineers who design the high level architecture before teams get built
- **create a microservices team template for future teams** - defining a standardized blueprint for what a standard "stream aligned" team looks like
- **define platform teams** - Setting up internal teams to build foundational, self-service infrastructure tools (CI/CD pipelines, cloud environments, logging).
- **add enabling and complicated subsystem teams** - Bringing in specialized support units for hard technical problems and knowledge sharing.
- **add key consumer teams** - Deploying actual Stream-Aligned business teams that consume the platform and build end-user features.

------------------------END OF CHAPTER 2---------------------------------

## designing microservices: the SEED process

Seven essential evolutions of design for services (SEED)

seed is a top down, multi step methodology, a collection of reusable processes in which each later step evolves from previous one; hence the name seven essential evolutions of design for services.

the tongue in cheek name reflects that the analyses are the seeds from which a complex microservices system grows.

**SEVEN STEPS**
1. **identifying factors** - determine who and what interacts with system(customer centric scoping)
2. **identifying jobs that actors have to do** - outline goals and tasks each actor needs to accomplish
3. **discovering interaction patterns with sequence diagrams** - map out the flow of messages overtime btw actors and system components
4. **deriving high level actions and queries from jtbds and interaction patterns** - translate interaction patterns into explicit commands and reads, turns jobs into developer friendly interface contracts
5. **describing each query and action as a specification with an open standard(openapi, graphql schemas)** - formally document each action and query using standard interface definition formats
6. **getting feedback on the api specification** - review and validate api design with stakeholders and consumer teams before building it
7. **implementing microservices** - write the actual backend code and setup dbs and implement services

jtbd format - when "a circumstance" , i want to "motivation", so i can "goal"

**Microservices are not just smaller apis**
microservices provide the implementation of your system, while apis should still be the outward facing interface of a system. if microservices replace anything it is modular components used to build systems; in a microservices architecture the building blocks are networked services

>web apis are layered on top of microsrevices

---------------------END OF CHAPTER 3 AND UNIT 1 ------------------------

# UNIT 2

## rightsizing your microservices: finding service boundaries

how can bigger application properly sliced into collection of microservices

>we can treat functions as microservices

**Flaws**
1. drawing boundaries based on technical needs anti pattern
2. too much granularity, too soon

> avoid creating too many microservices too early
> start with only a handful of services and slowly increase the number

## Domain - driven design and microservice boundaries

**sam newman 3 rules**

**loose coupling** - services operate with out deep awareness of other services internal implementation. a change in server A should never break or force redeployment in server B

**High cohesion** - behaviours that change for the same business reason are grouped inside a same service boundary

**aligned with business capabilities** - organize service around what business does rather than tech tiers

>a bounded context defines the range of applicability of each model.

> the representation of the domains in a larger system and how they collaborate is the **context map**, identifying and describing these collaborations is **context mapping**

> **DDD aggregate** - and aggregate is a collection of related domain objects viewed as a single unit by external consumers

## event storming

it finds bounded contexts in hours instead of weeks or months. it is not a full replacement for ddd and doesn't give all its benefits, but for discovering bounded it is a good approximation

Use more lightweight even storming process instead of formal ddd to discover main aggregates in your subdomain and identify the edges of the bounded contexts in your system

**process of event storming**

1. **discover domain events** - each participant writes key domain events on orange stickies, one event per note, placed on a time line. No coordination btw participants and no weeding out duplicates yet.
2. **enforce the timeline** - with facilitator, arrange notes int o coherent timeline and remove duplicates; ideally walk through a "storyline", like a user journey.
3. **reverse narrative and command identification** - walt the timeline backward from end to start and identify the commands on blue stickies.
4. **identify aggregates/ bounded contexts** - commands do not produce events directly ,aggregates accept commands and produce events. the wall becomes clusters of events and commands around aggregates. these clusters are the bounded contexts
5. **competitive analysis** - prioritize the bounded contexts

## universal sizing formula

bounded contexts are a fantastic starting point, but microservice boundaries are not synonymous with bounded contexts from ddd or even storming. boundaries are not constant over time; they evolve and tend toward increasing granularity as organizations and applications mature

> it is typically easier to  split a service than to merge services back together or to move a capability from one service to another.

**3 principles of universal sizing formula**

1. start with few microservices, possibly using bounded contexts
2. keep splitting as your main application and services grow, giuded by the needs of coordination avoidance.
3. be on the right trajectory for decreasing coordination. this is vastly more important than how "perfectly" services are sized at present.

-------------------------END OF CHAPTER 4--------------------------------

## Dealing with the data

**Idenpendent deployability** - being able to make changes to one microservice and deploy it without changing or deploying any other microservice.

sharing data spaces is a primary killer of independent development and independent deployability.

it must be clear which microservice owns any dataset, summed up as; microservice mush own their data.

> microservices can share physical database clusters

> microservices must own and embed their own data. no direct cross service data access is allowed.

**Logical Isolation** : services must not share logical schemas, tables or db spaces
**Physical Isolation** : services can and should share physical db clusters to avoid massive infrastructure costs, provided they maintain strict logical isolation

**data delegate pattern** - multiple services need access to same core data. create a authoritative delegate service that wraps and hides the data. all other services must query or mutate the data soley through this services api

**data lakes** - cross boundary read access is required analytics machine learning or auditing without hitting production service endpoints. services stream copies of their data via message brokers in to data lake

**Saga pattern** 
- a sequence of local transactions across multiple services
- every step executes its local action and registers a compensating action on a routing slip
- saga achieve eventual consistency, not instant ACID rollback. compensating actions are visible

### Event sourcing

- instead of storing current state, store an append only log of immutable facts.
- state is transient derivative computed by folding over these facts

Event consists of
1. eventId (UUID)
2. eventType
3. data (JSON payload describing change details)

### command query responsibility segregation (CQRS)

separate the write path(event store) from read path(query indices)

**write side** - accepts commands and writes immutable facts to the simple event store.
**read side** - subscribes to event and builds specialized read optimized projection views.

event store prioritizes consistency where query projection prioritize availability and read speed

----------------------END OF CHAPTER 5-----------------------------------
