
---
### 1. Working with Kubernetes Objects: Deployments, Pods, ReplicaSets
- **Pods**:
    - The fundamental unit of execution in Kubernetes, representing a group of one or more co-located containers.
    - Containers in the same Pod share storage, network namespace (local host communication), and are always scheduled together on the same worker node.
    - Most Pods run a single container, but multi-container Pods are used when containers need tight integration (e.g., a Git-sync container sharing a volume with an Nginx web server).
- **ReplicaSets**:
    - Responsible for maintaining a specified number of identical Pod replicas at any given time.
    - The ReplicaSet controller continuously monitors Pod counts and starts or terminates Pods to match the required count.
    - Users rarely manage ReplicaSets directly; instead, Deployments manage ReplicaSets automatically.
- **Deployments**:
    - A high-level declarative resource that supervises Pods and manages application lifecycle operations.
    - Creates and manages ReplicaSets under the hood to handle version rollouts and updates.
    - Automatically restarts containers if they crash, exit, or are killed (default restart policy is `Always`).

---
### 2. Maintaining Desired State
- **Declarative Architecture**: Instead of running one-off imperative commands, you declare the target **desired state** in configuration files, and Kubernetes continuously aligns the **actual state** with that target.
- **Reconciliation Loop**:
    - Controllers run an infinite loop that continuously compares the desired state stored in the internal database against the cluster's actual state.
    - If a Pod crashes or is manually deleted (e.g., `kubectl delete pod`), the reconciliation loop detects the discrepancy and immediately provisions a replacement Pod.

---
### 3. The Kubernetes Scheduler
- **Role**: Responsible for assigning unscheduled Pods from its queue (inbox) to suitable cluster nodes.
- **Scheduling Logic**:
    - When a new Pod is requested, it enters the scheduler's queue.
    - The scheduler evaluates available node capacities against the Pod's specified **resource requests** to find a matching node.
- **Execution**:
    - Once scheduled to a node, the node's **kubelet** agent picks up the Pod specification and starts the container runtime.
    - If a worker node fails completely, its Pods become unscheduled and return to the scheduler queue for reassignment to healthy nodes.

---
### 4. Resource Manifests in YAML Format
- **Infrastructure as Code**: Resources are defined as structured data records in **YAML** (or JSON) format and checked into version control.
- **Key Manifest Fields**:
    - `apiVersion`: Specifies the API version (e.g., `v1`, `apps/v1`).
    - `kind`: The type of resource being created (e.g., `Deployment`, `Service`, `Pod`).
    - `metadata`: Resource identification including `name`, `labels`, and `annotations`.
    - `spec`: Defines the desired state (e.g., `replicas`, container `image`, `ports`, `resources`).
- **Core `kubectl` Management Commands**:
    - `kubectl apply -f <filename.yaml>`: Submits or updates declarative resource configurations.
    - `kubectl get <resource>`: Retrieves status summaries (`deployments`, `pods`, `services`, `nodes`, `all`).
    - `kubectl describe <resource>/<name>`: Provides detailed information, container states, and recent event logs.
    - `kubectl delete -f <file>` / `kubectl delete <resource> --selector <label>`: Removes specified objects.
- **Service Objects**:
    - Provide a stable IP address and DNS endpoint to load-balance traffic across a dynamic set of Pods matching a label selector.
    - Maps service ports (`port`) to container application ports (`targetPort`).

---
### 5. Helm: A Kubernetes Package Manager
- **Overview**: A CNCF package manager that simplifies deploying complex applications by bundling YAML templates and default configurations into **Helm Charts**.
- **Core Terminology**:
    - **Chart**: A package containing all resource definitions needed to run an application.
    - **Repository**: A shared location where charts are published and downloaded.
    - **Release**: A specific running instance of a chart deployed into a cluster.
- **Key Operations**:
    - Helm charts contain templates and metadata—they do **not** package binary container images directly.
    - Key CLI commands: `helm install --name <release-name> <chart-path>`, `helm list`, and `helm status <release-name>`.

---
### 6. Managing Resources: Understanding Resources
- **Resource Units**:
    - **CPU**: Expressed in cores or **millicpus** / **millicores** (`100m` = 0.1 CPU core). 1 CPU unit equals 1 AWS vCPU, Google Cloud Core, Azure vCore, or hyperthread.
    - **Memory**: Measured in bytes, **mebibytes** (`Mi` / `MiB`), or **gibibytes** (`Gi` / `GiB`).
- **Requests vs. Limits**:
    - **Resource Requests**: The **minimum** CPU and memory guaranteed for a Pod to run. The scheduler uses requests to select a node with sufficient unallocated capacity. If no node has enough capacity, the Pod remains in `Pending` state.
    - **Resource Limits**: The **maximum** resource threshold allowed for a container.
        - **CPU Limit Exceeded**: The container process is **throttled**, causing performance degradation.
        - **Memory Limit Exceeded**: The container is **terminated** (OOM killed) and restarted if permitted.
- **Overcommit & Eviction**:
    - Resource limits on a node can exceed total physical capacity (overcommitment).
    - Under node memory pressure, Kubernetes evicts Pods that exceed their resource requests first to keep system components stable.

---
### 7. Managing the Container Life Cycle
- **Liveness Probes**:
    - Checks if the container process is alive and functioning.
    - If a liveness probe fails, Kubernetes kills and restarts the container.
    - Probe types: `httpGet` (returns 2xx/3xx HTTP response), `tcpSocket` (checks port connectivity), or `exec` (runs a command inside the container; 0 exit code = success). gRPC servers use `grpc-health-probe` via `exec`.
    - Key settings: `initialDelaySeconds` (delay before first probe to allow startup) and `periodSeconds` (check frequency).
- **Readiness Probes**:
    - Determines if a container is ready to accept user network traffic.
    - If a readiness probe fails, the Pod is removed from Service load balancing endpoints without being killed.
    - Essential for zero-downtime upgrades. Readiness probes must strictly return HTTP `200 OK` (3xx redirects can cause cloud load balancers to mark Pods unhealthy).
    - `minReadySeconds`: Ensures a newly started Pod stays ready for a specified duration before rollout continues.
- **Pod Disruption Budgets (PDB)**:
    - Protects application availability during voluntary cluster evictions (e.g., node draining).
    - Configured via `minAvailable` (minimum required running Pods) or `maxUnavailable` (maximum allowed evicted percentage). Does not protect against involuntary node hardware failures.

---
### 8. Using Namespaces
- **Purpose**: Partition a physical cluster into isolated virtual sub-clusters for teams or environments (`prod`, `staging`, `test`).
- **Key Characteristics**:
    - Resources in one namespace are hidden from another by default. System components run in `kube-system`.
    - Access specific namespaces using `--namespace` or `-n` with `kubectl`.
    - **Warning**: Deleting a namespace permanently deletes every resource inside it.
- **Cross-Namespace Service DNS**:
    - Full DNS address pattern: `<service-name>.<namespace>.svc.cluster.local` (e.g., `demo.prod`).
- **ResourceQuotas & LimitRanges**:
    - **ResourceQuota**: Enforces cumulative resource caps on a namespace (e.g., maximum 100 Pods) to prevent resource hogging.
    - **LimitRange**: Sets default container requests and limits within a namespace if they are omitted in individual manifests.

---
### 9. Optimizing Cluster Costs
- **Workload Optimization**:
    - Scale down replica counts to the minimum needed to meet availability and Service Level Objectives (SLOs).
    - Set resource limits slightly above normal peak usage (~100% of peak) to prevent memory leaks from starving the node while avoiding unnecessary pod termination.
    - **Vertical Pod Autoscaler (VPA)**: Monitors workload usage and recommends or automatically tunes resource requests.
- **Node Sizing & Storage**:
    - Larger nodes reduce the relative overhead of operating systems and system agents, lowering stranded resources. Target between 10 and 100 Pods per node (default Kubernetes limit is 110 Pods/node).
    - Provision minimal disk capacity and low-IOPS storage tiers for non-critical workloads to prevent over-provisioning storage costs.
- **Resource Hygiene & Metadata**:
    - Periodically sweep for unattached disks, orphan load balancers, and unreferenced Pods.
    - Mandatory **owner annotations** (e.g., `example.com/owner: "Customer Apps Team"`) help identify maintainers before terminating idle resources.
- **Cost-Effective Compute Instances**:
    - **Reserved Instances / Committed Use Discounts**: Provide ~50% savings for stable, long-term workloads planned 1–3 years ahead.
    - **Preemptible / Spot Instances**: Offer up to 80% savings for stateless or fault-tolerant workloads.
    - **Node Affinities**: Use `nodeAffinity` rules (e.g., matching `cloud.google.com/gke-preemptible`) to prevent critical Pods from running on preemptible nodes while steering non-critical Pods toward them.
- **Cluster Rebalancing**:
    - The scheduler does not automatically relocate running Pods when new nodes are added.
    - Use the **Descheduler** tool to evict duplicate Pods or rebalance workloads onto underutilized nodes to restore high availability.

---
