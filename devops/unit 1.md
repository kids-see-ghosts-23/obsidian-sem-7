
## CHAPTER 1: REVOLUTION IN THE CLOUD
### 1. The Creation of the Cloud
- **CapEx vs. OpEx Shift**: Modern computing shifted from purchasing physical hardware (**Capital Expense / CapEx**) to renting shared, remote compute on a pay-per-use basis (**Operating Expense / OpEx**). Bare-metal represents CapEx (large upfront purchase, hard to scale, becomes obsolete quickly), whereas the cloud represents OpEx (pay per use, third-party provider handles maintenance and scaling).
- **Analogy**: Renting cloud compute is like plugging into an electricity grid or taking a taxi (OpEx) instead of buying a private car or power plant (CapEx).
- **IaaS & SaaS**:
    - **IaaS (Infrastructure as a Service)**: Treats compute infrastructure as an elastic commodity like water or electricity.
    - **SaaS (Software as a Service)**: Provides fully managed, rented software applications.

---
### 2. The Dawn of DevOps
- **Definition**: A cultural movement bringing development and operations together to share understanding and responsibility for system reliability and software correctness. It is primarily an **organizational and human issue**, not a software tool.
- **CAMS Pillars (John Willis)**:
    1. **C**ulture
    2. **A**utomation
    3. **M**easurement
    4. **S**haring
        _(Memory Trick: "Cats Are Mostly Sleepy")_
- **DevOps Trinity (Brian Dawson)**: **P**eople & Culture, **P**rocess & Practice, **T**ools & Technology.
- **Infrastructure as Code (IaC)**: Provisioning and managing infrastructure using automated software scripts rather than manually racking and cabling physical machines.
- **The NoOps Fallacy**: The mistaken belief that cloud and containers eliminate operations staff; in reality, operational work (CI/CD, automated testing, security checks, monitoring) shifts earlier into the development lifecycle.

---
### 3. The Coming of Containers
- **Definition**: A standard packaging format that bundles an application along with all its required dependencies into an image executed by a container runtime.
- **Origin**: Inspired by Malcolm McLean (Sea-Land, 1950s), who proposed shipping standardized steel truck boxes directly onto cargo ships.
- **Virtual Machines (VMs) vs. Containers**:

|Feature|Virtual Machine (VM)|Container|
|:--|:--|:--|
|**Contents**|Whole guest OS + unrelated libraries|Only application binary + required dependencies|
|**Image Size**|Large (~1 GiB)|Compact (~100× smaller)|
|**Performance**|~30% slower due to emulated CPU overhead|Native CPU performance (no virtualization layer)|
|**Dependency**|Full guest OS|**Only the host OS kernel**|
|**Filesystem**|Fixed disk image|Shared, addressable, cached filesystem layers|

---
### 4. Conducting the Container Orchestra
- **Container Orchestrator**: Software that joins multiple server machines into a unified cluster, handling **scheduling** (assigning workloads to nodes), **orchestration** (coordinating workflows), and **cluster management**.

---
### 5. Kubernetes
- **History & Origins**: Open-sourced by Google in **2014**, derived from Google's internal cluster management systems **Borg** and **Omega**. Won the "orchestration wars" by late 2017.
- **Etymology**: Derived from the Greek word _κυβερνήτης_, meaning **"helmsman"** or **"pilot"**.
- **Key Deployment Strategies**:
    - **Rolling Updates**: Replaces old Pods with new Pods incrementally to guarantee zero downtime.
    - **Canary Deployments**: Rolls out updates gradually to a small fraction of servers first to detect defects before full rollout.
    - **Blue-Green Deployments**: Spins up the new version in parallel and switches incoming network traffic over once verified.
- **Autoscaling**: Automatically adds container replicas when CPU or load thresholds are breached and scales down when load subsides.

---
### 6. Cloud Native
- **Definition**: Applications designed specifically to leverage cloud infrastructure, containers, microservices, and dynamic orchestration. Managed under the **CNCF (Cloud Native Computing Foundation)**, founded in 2015 under the Linux Foundation.
- **6 Core Characteristics (Memory Trick: "AURDOD")**:
    1. **A**utomatable (managed via standardized machine interfaces)
    2. **U**biquitous & Flexible (decoupled from node/disk specifics)
    3. **R**esilient & Scalable (no single point of failure)
    4. **D**ynamic (scheduled for optimal resource utilization)
    5. **O**bservable (monitored via metrics, logs, and tracing)
    6. **D**istributed (composed of small, cooperating microservices)
- **Monolith vs. Microservices**: Monoliths are easier to understand initially but hard to scale; microservices scale independently but add distributed system complexity. Containerizing a monolith is a valid first migration step.

---
### 7. The Future of Operations
- Systems administration evolves into **Distributed DevOps** (ops specialists embedded in software teams), backed by central **Developer Productivity Engineering (DPE)** teams and **Site Reliability Engineers (SREs)**.
- The boundary between development and operations disappears: _"it's all just software now"_.

---
## CHAPTER 2: FIRST STEPS WITH KUBERNETES
### 1. Running Your First Container
- **Docker CLI Command**:

```
docker container run -p 9999:8888 --name hello cloudnatived/demo:hello
```

- **Port Mapping (`-p 9999:8888`)**: Forwards host machine port `9999` to container port `8888`.

---
### 2. The Demo Application
- Written in **Go**; registers an HTTP handler serving `"Hello, 世界"` on port `8888`.

---
### 3. Building a Container & Multi-Stage Builds
- **Building**: `docker image build -t myhello .`.
- **Multi-Stage Build Pattern**:
    - **Stage 1 (Build Stage)**: Uses `golang:1.11-alpine` to compile the Go binary.
    - **Stage 2 (Production Stage)**: Uses `FROM scratch` (an empty base image) and copies only the compiled binary using `COPY --from=build`.
    - **Key Benefit**: Shrinks the final container image size from **~350 MiB down to ~6 MiB**, reducing deployment time and minimizing security attack surfaces.

---
### 4. Container Registries
- **Definition**: A central service used to store, tag, and retrieve container images by unique name (default: **Docker Hub**).
- **Publishing Algorithm**: `docker login` $\rightarrow$ `docker image tag myhello YOUR_DOCKER_ID/myhello` $\rightarrow$ `docker image push YOUR_DOCKER_ID/myhello`.

---
### 5. Hello Kubernetes & Pod Statuses
- **Deploying Workloads**:

```
kubectl run demo --image=YOUR_DOCKER_ID/myhello --port=9999 --labels app=demo
kubectl port-forward deploy/demo 9999:8888
```

- `kubectl run` creates a **Deployment** object (which creates and manages Pods).
- **Pod Status Overview**:
    - `Running`: Container is executing cleanly.
    - `ContainerCreating`: Image is being fetched/started.
    - `ErrImagePull` / `ImagePullBackOff`: Cluster cannot locate or download the specified container image.

---
### 6. Minikube
- A tool that provisions a local, **single-node Kubernetes cluster** inside a VM on a developer's workstation.

---
## CHAPTER 3: GETTING KUBERNETES
### 1. Cluster Architecture

```
+-------------------------------------------------------------------+
|                        CONTROL PLANE (Master)                     |
|  +------------------+  +------+  +-----------------------------+  |
|  | kube-apiserver   |  | etcd |  | kube-controller-manager     |  |
|  +------------------+  +------+  +-----------------------------+  |
|  | kube-scheduler   |            | cloud-controller-manager    |  |
|  +------------------+            +-----------------------------+  |
+-------------------------------------------------------------------+
                                  |
                                  v
+-------------------------------------------------------------------+
|                            WORKER NODES                           |
|  +-------------------+  +---------------+  +-------------------+  |
|  | kubelet           |  | kube-proxy    |  | Container Runtime |  |
|  +-------------------+  +---------------+  +-------------------+  |
+-------------------------------------------------------------------+
```

#### Control Plane Components (Master Nodes)
1. **`kube-apiserver`**: Central REST API frontend handling all component and CLI communications.
2. **`etcd`**: Consistent, distributed key-value store maintaining cluster state (does not store app data).
3. **`kube-scheduler`**: Assigns unscheduled Pods to worker nodes based on resource availability.
4. **`kube-controller-manager`**: Runs background controller processes to reconcile desired vs. actual state.
5. **`cloud-controller-manager`**: Integrates with underlying public cloud infrastructure (load balancers, routing).
#### Worker Node Components
1. **`kubelet`**: Node agent communicating with the API server to drive the container runtime.
2. **`kube-proxy`**: Manages network rules, IP forwarding, and service routing.
3. **`Container Runtime`**: Underlying engine executing containers (e.g., containerd, Docker).
#### High Availability (HA)
- Requires at least **3 master nodes** across multiple availability zones.
- **`etcd` Quorum**: Requires a strict majority ($1M/year estimate).
- **"Run Less Software" Philosophy**: Outsource undifferentiated infrastructure heavy lifting to focus resources on business differentiation.

---
### 3. Managed Kubernetes Services
- **Managed Services**: Cloud providers manage control plane setup, HA, and upgrades.
    - **GKE (Google Kubernetes Engine)**: Industry standard, auto-upgrades, regional clusters.
    - **EKS (AWS Elastic Kubernetes Service)**: More setup required; charges additional hourly fees for control plane master nodes.
    - **AKS (Azure Kubernetes Service)**: Free control plane, billed by worker nodes.
- **PaaS / Turnkey Alternatives**:
    - **OpenShift**: Enterprise PaaS by Red Hat covering the full software lifecycle (CI/CD, builds, monitoring) across bare-metal and multi-cloud.
    - **Turnkey Solutions (Stackpoint / CKE)**: Vendor manages control plane via a portal while worker nodes run on user infrastructure.

---
### 4. Kubernetes Installers
- **`kubeadm`**: Official installer tool for bootstrapping best-practice clusters.
- **`kops`**: Declarative cluster lifecycle tool for AWS/GCP.
- **`Kubespray`**: Ansible-based installer for bare-metal and multi-cloud setups.
- **`Kubernetes The Hard Way`**: Kelsey Hightower's tutorial for learning manual component installation.

---
### 5. Clusterless Container Services
- Examples: **Amazon Fargate**, **Azure Container Instances (ACI)**.
- Abstracts nodes and clusters entirely (no `kubectl` access); users supply container images alongside CPU/memory limits and pay strictly per second of execution.

---

