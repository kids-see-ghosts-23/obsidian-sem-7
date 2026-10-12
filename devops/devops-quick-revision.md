# DevOps Quick Revision

---

## Cloud and DevOps Foundations

- Computing shifted from buying hardware (CapEx) to renting shared remote compute on a pay-per-use basis (OpEx).
- Bare metal is CapEx: large upfront cost, hard to scale, becomes obsolete quickly.
- Cloud is OpEx: pay per use, third party handles maintenance and scaling.
- Analogy: cloud compute is like electricity grid power or a taxi; buying a private power plant or car is CapEx.
- IaaS treats compute infrastructure as an elastic commodity.
- SaaS provides fully managed, rented software applications.

### DevOps

- DevOps is a cultural movement bringing development and operations together to share understanding and responsibility for system reliability and software correctness.
- It is primarily an organizational and human issue, not a software tool.
- CAMS pillars (John Willis): Culture, Automation, Measurement, Sharing. Memory trick: "Cats Are Mostly Sleepy".
- DevOps Trinity (Brian Dawson): People & Culture, Process & Practice, Tools & Technology.
- Infrastructure as Code is provisioning and managing infrastructure using automated software scripts rather than manual racking and cabling.
- The NoOps Fallacy is the mistaken belief that cloud and containers eliminate operations staff; in reality, operational work shifts earlier into the development lifecycle.

### Containers

- A container is a standard packaging format that bundles an application with all required dependencies into an image executed by a container runtime.
- Inspired by Malcolm McLean's standardized shipping boxes.
- VM vs container:
  - VM: whole guest OS plus unrelated libraries, large image around 1 GiB, about 30% slower due to emulated CPU overhead, depends on full guest OS, fixed disk image.
  - Container: only application binary plus required dependencies, compact around 100x smaller, native CPU performance, depends only on host OS kernel, shared addressable cached filesystem layers.

### Container Orchestration

- A container orchestrator joins multiple server machines into a unified cluster and handles scheduling, orchestration, and cluster management.

### Kubernetes

- Open-sourced by Google in 2014, derived from Borg and Omega; won the orchestration wars by late 2017.
- Etymology: Greek word meaning "helmsman" or "pilot".
- Deployment strategies: rolling updates, canary deployments, blue-green deployments.
- Autoscaling adds replicas when CPU/load thresholds are breached and scales down when load subsides.

### Cloud Native

- Applications designed specifically to leverage cloud infrastructure, containers, microservices, and dynamic orchestration.
- Managed under CNCF, founded in 2015 under the Linux Foundation.
- Six core characteristics, memory trick "AURDOD": Automatable, Ubiquitous & Flexible, Resilient & Scalable, Dynamic, Observable, Distributed.
- Monoliths are easier to understand initially but hard to scale; microservices scale independently but add distributed complexity. Containerizing a monolith is a valid first migration step.

### Future of Operations

- Systems administration evolves into Distributed DevOps with SREs and central Developer Productivity Engineering teams.
- The boundary between development and operations fades: "it's all just software now."

---

## First Steps with Kubernetes

- Run a container:
  - `docker container run -p 9999:8888 --name hello cloudnatived/demo:hello`
  - `-p 9999:8888` forwards host port 9999 to container port 8888.
- Demo app: Go app serving "Hello, world" on port 8888.
- Build image: `docker image build -t myhello .`
- Multi-stage build:
  - Stage 1: build with `golang:1.11-alpine`
  - Stage 2: production from `FROM scratch`, copy binary with `COPY --from=build`
  - Benefit: image shrinks from about 350 MiB to about 6 MiB
- Container registries store, tag, and retrieve images by unique name; default is Docker Hub.
- Publishing algorithm: `docker login`, then `docker image tag myhello YOUR_DOCKER_ID/myhello`, then `docker image push YOUR_DOCKER_ID/myhello`.
- Hello Kubernetes:
  - `kubectl run demo --image=YOUR_DOCKER_ID/myhello --port=9999 --labels app=demo`
  - `kubectl port-forward deploy/demo 9999:8888`
  - `kubectl run` creates a Deployment object that creates and manages Pods.
- Pod statuses: Running, ContainerCreating, ErrImagePull, ImagePullBackOff.
- Minikube: local single-node Kubernetes cluster inside a VM.

---

## Cluster Architecture

Control plane components:
- `kube-apiserver`: central REST API frontend
- `etcd`: consistent distributed key-value store for cluster state, not app data
- `kube-scheduler`: assigns unscheduled Pods to worker nodes
- `kube-controller-manager`: runs background controllers to reconcile desired vs. actual state
- `cloud-controller-manager`: integrates with cloud infrastructure

Worker node components:
- `kubelet`: node agent that drives the container runtime via the API server
- `kube-proxy`: manages network rules, IP forwarding, and service routing
- Container runtime: executes containers, for example containerd, Docker

High availability:
- At least 3 master nodes across multiple availability zones
- etcd quorum requires a strict majority

Managed services:
- GKE: industry standard, auto-upgrades, regional clusters
- EKS: more setup, additional hourly fees for control plane master nodes
- AKS: free control plane, billed by worker nodes

PaaS/turnkey alternatives:
- OpenShift: enterprise PaaS covering full software lifecycle
- Turnkey solutions: vendor manages control plane via portal

Installers:
- `kubeadm`, `kops`, `Kubespray`, Kubernetes The Hard Way

Clusterless container services:
- Examples: Amazon Fargate, Azure Container Instances
- Abstract nodes and clusters entirely; no kubectl access; pay per second of execution

---

## Kubernetes Objects

### Pods
- Fundamental unit of execution; one or more co-located containers.
- Containers in the same Pod share storage, network namespace, and node placement.
- Often single-container; multi-container Pods used for tight integration.

### ReplicaSets
- Maintain a specified number of identical Pod replicas.
- Controller continuously monitors and adjusts Pod count.
- Users rarely manage directly; Deployments manage them automatically.

### Deployments
- Declarative high-level resource that supervises Pods and manages lifecycle, rollouts, and updates.
- Creates and manages ReplicaSets.
- Restarts containers on crash, exit, or kill; default restart policy is Always.

### Desired State and Reconciliation
- Declarative architecture: declare desired state in configuration files; Kubernetes aligns actual state to target.
- Reconciliation loop: controllers continuously compare desired state against actual state; if a Pod crashes or is deleted, a replacement is provisioned.

### Scheduler
- Assigns unscheduled Pods from its queue to suitable nodes based on resource requests.
- Kubelet starts the container runtime for scheduled Pods.
- Failed worker nodes cause Pods to become unscheduled and re-enter the scheduler queue.

### Resource Manifests
- Infrastructure as Code via YAML or JSON.
- Key fields: `apiVersion`, `kind`, `metadata`, `spec`.
- Core commands:
  - `kubectl apply -f <file>`
  - `kubectl get <resource>`
  - `kubectl describe <resource>/<name>`
  - `kubectl delete -f <file>` or `kubectl delete <resource> --selector <label>`

### Services
- Stable IP and DNS endpoint; load balance across Pods matching a label selector.
- Map service port to targetPort.

---

## Helm

- CNCF package manager; bundles YAML templates and default configurations into Helm Charts.
- Chart: package of resource definitions.
- Repository: shared chart location.
- Release: running instance of a chart.
- Charts contain templates and metadata; they do not package binary container images directly.
- Commands: `helm install --name <release-name> <chart-path>`, `helm list`, `helm status <release-name>`.

---

## Managing Resources

- CPU: cores or millicpus/millicores; 100m = 0.1 CPU core; 1 CPU unit = 1 AWS vCPU, Google Cloud Core, Azure vCore, or hyperthread.
- Memory: bytes, MiB, GiB.
- Requests: minimum guaranteed resources; scheduler uses them to pick a node; insufficient capacity leaves Pod Pending.
- Limits: maximum allowed resources; CPU limit exceeded leads to throttling; memory limit exceeded leads to OOM kill and possible restart.
- Overcommit and eviction: limits can exceed physical capacity; under memory pressure, Kubernetes evicts Pods exceeding requests first.

---

## Container Lifecycle

- Liveness probe: checks if container is alive; failure triggers restart; types include httpGet, tcpSocket, exec; key settings include initialDelaySeconds and periodSeconds.
- Readiness probe: checks if container can accept traffic; failure removes Pod from Service endpoints without killing it; required for zero-downtime upgrades; must return HTTP 200 OK.
- minReadySeconds: keeps a newly started Pod ready for a set duration before rollout continues.
- Pod Disruption Budgets: protect availability during voluntary evictions using minAvailable or maxUnavailable; do not protect against involuntary node failures.

---

## Namespaces

- Partition a cluster into isolated virtual sub-clusters for teams or environments.
- Resources hidden across namespaces by default; system components run in kube-system.
- Use `--namespace` or `-n` with kubectl.
- Deleting a namespace permanently deletes everything inside it.
- Cross-namespace DNS pattern: `<service-name>.<namespace>.svc.cluster.local`.
- ResourceQuota: cumulative caps per namespace.
- LimitRange: default requests and limits in a namespace.

---

## Optimizing Cluster Costs

- Scale replicas to the minimum needed for availability and SLOs.
- Set limits slightly above peak usage to prevent memory-leak-driven node starvation.
- VPA monitors usage and recommends or tunes requests.
- Larger nodes reduce relative OS/system-agent overhead; target 10 to 100 Pods per node; default limit is 110 Pods/node.
- Use minimal disk and low-IOPS storage for non-critical workloads.
- Sweep unattached disks, orphan load balancers, and unreferenced Pods.
- Use owner annotations to identify maintainers.
- Reserved instances/committed use discounts offer about 50% savings for stable long-term workloads.
- Preemptible/spot instances offer up to 80% savings for stateless, fault-tolerant workloads.
- Use nodeAffinity to keep critical Pods off preemptible nodes.
- Scheduler does not automatically relocate Pods when new nodes are added; use the Descheduler to rebalance.
