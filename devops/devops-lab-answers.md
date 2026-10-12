# DevOps Lab Answers

---

## 1. Build a container image for the application.

To build a container image, the application is packaged with its required dependencies into a standard container image using a Dockerfile and the container build command.

From the notes, the basic build command is:

```
docker image build -t myhello .
```

The recommended pattern for production images is a **multi-stage build**:

- **Stage 1, build stage**: Use a full build image such as `golang:1.11-alpine` to compile the application binary.
- **Stage 2, production stage**: Use a minimal base such as `FROM scratch`, then copy only the compiled binary with `COPY --from=build`.

The key benefit of this pattern is image size reduction. In the notes, the final image shrinks from about **350 MiB down to about 6 MiB**, which reduces deployment time and minimizes the security attack surface.

A minimal Dockerfile shape for a multi-stage build looks like this:

```
# Stage 1: build
FROM golang:1.11-alpine AS build
WORKDIR /app
COPY . .
RUN go build -o myapp

# Stage 2: production
FROM scratch
COPY --from=build /app/myapp /myapp
EXPOSE 8888
ENTRYPOINT ["/myapp"]
```

After the Dockerfile is ready, run the build command from the directory containing the Dockerfile:

```
docker image build -t myhello .
```

If the image will be pushed to a registry later, it should be tagged with a registry-qualified name:

```
docker image tag myhello YOUR_DOCKER_ID/myhello
```

---

## 2. Create 3 pods for the docker image created in Q1.

In Kubernetes, Pods are usually created through a controller such as a Deployment, because controllers maintain the desired number of replicas and self-heal when a Pod is deleted.

To run the image created in Q1 and create the desired number of Pod replicas, use a Deployment. One practical path is:

1. Deploy the image with `kubectl run`, which creates a Deployment object that in turn creates and manages Pods.
2. Scale the Deployment to 3 replicas.

Example using `kubectl run`:

```
kubectl run demo --image=YOUR_DOCKER_ID/myhello --port=9999 --labels app=demo
```

From the notes, `kubectl run` creates a **Deployment** object, which creates and manages Pods.

If you need exactly three running Pods, scale the Deployment:

```
kubectl scale deployment demo --replicas=3
```

Alternatively, a declarative YAML approach is cleaner for repeat use. A Deployment manifest for 3 replicas looks like this:

```
apiVersion: apps/v1
kind: Deployment
metadata:
  name: demo
  labels:
    app: demo
spec:
  replicas: 3
  selector:
    matchLabels:
      app: demo
  template:
    metadata:
      labels:
        app: demo
    spec:
      containers:
        - name: myhello
          image: YOUR_DOCKER_ID/myhello
          ports:
            - containerPort: 8888
```

Apply it with:

```
kubectl apply -f deployment.yaml
```

After applying, Kubernetes creates a ReplicaSet, and the ReplicaSet creates and maintains the 3 Pods.

---

## 3. Delete a pod and check status of running pods.

To delete a Pod, use:

```
kubectl delete pod <pod-name>
```

For example:

```
kubectl delete pod demo-<pod-identifier>
```

From the notes, if a Pod is manually deleted with `kubectl delete pod`, the reconciliation loop detects the discrepancy and immediately provisions a replacement Pod. This is because the controlling resource, such as a Deployment, continues to strive for the desired state.

To check the status of running Pods, use:

```
kubectl get pods
```

Useful variants include:

```
kubectl get pods -o wide
kubectl get pods --show-labels
kubectl get pods -l app=demo
```

Typical Pod statuses from the notes include:

- **Running**: Container is executing cleanly.
- **ContainerCreating**: Image is being fetched or started.
- **ErrImagePull** / **ImagePullBackOff**: The cluster cannot locate or download the specified container image.

If the Pod belongs to a Deployment, deleting it should result in a new Pod being created so the replica count returns to the desired value.

---

## 4. List pods created in Kubernetes through kubectl command.

The primary command to list Pods is:

```
kubectl get pods
```

Common variations:

```
kubectl get pods
kubectl get pods -o wide
kubectl get pods --all-namespaces
kubectl get pods -n <namespace>
kubectl get pods -l app=demo
kubectl get pods --show-labels
```

To see more detail about a specific Pod, use:

```
kubectl describe pod <pod-name>
```

To list the Pods created by the Deployment from the earlier questions, you can filter by label:

```
kubectl get pods -l app=demo
```

In general, `kubectl get <resource>` retrieves status summaries for the requested resource type, and `kubectl get pods` is the direct answer to listing Pods.

---

## 5. Write a web application of your profile (1 page) in any language.

Below is a simple one-page personal profile web application written in Python using the standard library. It is intentionally small and self-contained so it can be containerized easily.

**File: app.py**

```
import http.server
import socketserver
import datetime

PORT = 8888

PROFILE = """
<html>
<head>
    <title>Student Profile</title>
    <style>
        body { font-family: sans-serif; max-width: 720px; margin: 40px auto; }
        h1 { color: #222; }
        table { border-collapse: collapse; width: 100%; }
        th, td { border: 1px solid #ccc; padding: 8px; text-align: left; }
        th { background: #f5f5f5; }
    </style>
</head>
<body>
    <h1>Student Profile</h1>
    <table>
        <tr><th>Name</th><td>Student Name</td></tr>
        <tr><th>Course</th><td>DevOps and Cloud Native Computing</td></tr>
        <tr><th>Topic</th><td>Containers, Kubernetes, and DevOps Practices</td></tr>
        <tr><th>Focus Areas</th><td>Container images, Pods, Deployments, ReplicaSets, Helm, Git and Docker Hub</td></tr>
        <tr><th>Generated At</th><td>TIMESTAMP</td></tr>
    </table>
</body>
</html>
"""

class Handler(http.server.SimpleHTTPRequestHandler):
    def do_GET(self):
        if self.path == "/" or self.path == "/index.html":
            page = PROFILE.replace("TIMESTAMP", datetime.datetime.now().isoformat())
            self.send_response(200)
            self.send_header("Content-type", "text/html")
            self.end_headers()
            self.wfile.write(page.encode("utf-8"))
        else:
            self.send_response(404)
            self.end_headers()
            self.wfile.write(b"Not Found")

with socketserver.TCPServer(("", PORT), Handler) as httpd:
    print("Serving profile on port", PORT)
    httpd.serve_forever()
```

This application:

- Serves a single profile page on port 8888.
- Returns a small HTML table with profile information.
- Includes a timestamp that changes on each request.

A corresponding Dockerfile could be:

```
FROM python:3.11-slim
WORKDIR /app
COPY app.py .
EXPOSE 8888
CMD ["python", "app.py"]
```

Build and run it locally for testing:

```
docker image build -t profile-app .
docker container run -p 9999:8888 --name profile profile-app
```

The app can then be reached on the host through port 9999.

---

## 6. Run the container image for version controlling and reflecting code changes.

Running a container image itself does not perform version control. Version control is handled by a source control system such as Git, while containers package a specific, versioned build of the application.

The practical workflow from the notes is:

1. Store the application source code and Dockerfile in Git.
2. Make code changes in the repository and commit them.
3. Rebuild the container image so the new image reflects the updated code.
4. Tag the new image appropriately.
5. Run or redeploy the new image so the running container reflects the latest code.

A representative workflow:

```
git add .
git commit -m "Update profile page"
docker image build -t profile-app .
docker image tag profile-app YOUR_DOCKER_ID/profile-app:v2
docker image push YOUR_DOCKER_ID/profile-app:v2
```

Then run the newer image:

```
docker container stop profile
docker container rm profile
docker container run -p 9999:8888 --name profile YOUR_DOCKER_ID/profile-app:v2
```

In Kubernetes, reflecting code changes means updating the Deployment to use the new image tag and letting the Deployment roll out the change. The notes emphasize that Kubernetes is declarative: you update the desired state, and the system reconciles the actual state to match it.

A key point from the notes is that the container image is a packaging artifact of a specific build, while Git is the system of record for source changes. The two work together: Git tracks code evolution, and rebuilt images capture the result of that evolution.

---

## 7. Differentiate between Git Hub and Docker Hub with minimum 2 examples.

**GitHub** is a source code collaboration and version control platform centered on Git repositories. It is used to store, review, version, and collaborate on source code and related project files.

**Docker Hub** is a container registry used to store, tag, and retrieve container images by unique name. It is used to distribute packaged container images, not source code.

Two concrete examples of the difference:

1. **What is stored**
   - GitHub stores source code, commit history, pull requests, issues, and repository-level collaboration artifacts.
   - Docker Hub stores container images and image tags, along with registry metadata for image retrieval.

   Example:
   - GitHub: A repository holds `app.py`, `Dockerfile`, and YAML manifests, with commits tracking changes over time.
   - Docker Hub: A repository holds images such as `myhello:latest` and `myhello:v2`, which are pulled by container runtimes.

2. **Primary workflow**
   - GitHub workflow example: A developer pushes source code, opens a pull request for review, and merges changes into the main branch.
   - Docker Hub workflow example: A developer logs in with `docker login`, tags an image with `docker image tag myhello YOUR_DOCKER_ID/myhello`, and pushes it with `docker image push YOUR_DOCKER_ID/myhello`.

Another useful contrast is purpose. GitHub is about collaborating on code and project history. Docker Hub is about distributing and retrieving packaged runtime images. They are complementary: code often lives in GitHub, and the resulting built container image can be published to Docker Hub.

---

## 8. Create the following for the application developed in Q.1:
### i) ii) iii)

Because the question lists items as i), ii), and iii) without specifying them, the most useful interpretation from the notes is to create the core Kubernetes resource artifacts needed to run the application: a **Deployment**, a **Service**, and a **ReplicaSet-oriented runtime setup**. These three artifacts together represent a typical small Kubernetes application.

### i) Deployment

The Deployment declares the desired state for the application: which image to run, how many replicas to maintain, and how to identify the Pods.

Example manifest:

```
apiVersion: apps/v1
kind: Deployment
metadata:
  name: demo
  labels:
    app: demo
spec:
  replicas: 3
  selector:
    matchLabels:
      app: demo
  template:
    metadata:
      labels:
        app: demo
    spec:
      containers:
        - name: myhello
          image: YOUR_DOCKER_ID/myhello
          ports:
            - containerPort: 8888
```

Apply it with:

```
kubectl apply -f deployment.yaml
```

### ii) Service

The Service provides a stable network endpoint and load balances traffic across the Pods matching the label selector.

Example manifest:

```
apiVersion: v1
kind: Service
metadata:
  name: demo
spec:
  selector:
    app: demo
  ports:
    - port: 9999
      targetPort: 8888
```

This maps service port 9999 to the container port 8888. From the notes, Service objects give a stable IP address and DNS endpoint and load-balance traffic across a dynamic set of Pods matching a label selector.

### iii) ReplicaSet-backed Pod management

In normal use, you do not create the ReplicaSet directly. Instead, the Deployment creates and manages a ReplicaSet, and the ReplicaSet maintains the specified number of identical Pod replicas.

The practical outcome is:

- The Deployment declares `replicas: 3`.
- A ReplicaSet is created to maintain 3 identical Pods.
- If a Pod is deleted, the reconciliation loop causes the ReplicaSet to create a replacement Pod so the desired count is preserved.

This is why the notes say users rarely manage ReplicaSets directly; Deployments manage them automatically.

If the assignment literally wants an explicit ReplicaSet manifest, it can be written as:

```
apiVersion: apps/v1
kind: ReplicaSet
metadata:
  name: demo-rs
  labels:
    app: demo
spec:
  replicas: 3
  selector:
    matchLabels:
      app: demo
  template:
    metadata:
      labels:
        app: demo
    spec:
      containers:
        - name: myhello
          image: YOUR_DOCKER_ID/myhello
          ports:
            - containerPort: 8888
```

But the preferred Kubernetes practice is to use a Deployment, not a raw ReplicaSet.

---

## 9. Explain Deployments, Pods and Replica sets in detail.

### Pods

A Pod is the fundamental unit of execution in Kubernetes. It represents a group of one or more co-located containers.

Key points:
- Containers in the same Pod share storage and network namespace, which means they can communicate on localhost and share volumes.
- They are always scheduled together on the same worker node.
- Most Pods run a single container, but multi-container Pods are used when containers need tight integration.
- A common multi-container example is a Git-sync container sharing a volume with an Nginx web server.

A Pod is relatively ephemeral. It is not usually managed directly by users for production workloads; instead, controllers manage Pods to maintain desired state.

### ReplicaSets

A ReplicaSet is responsible for maintaining a specified number of identical Pod replicas at any given time.

Key points:
- The ReplicaSet controller continuously monitors Pod counts.
- It starts or terminates Pods to match the required count.
- Users rarely manage ReplicaSets directly.
- Deployments manage ReplicaSets automatically.

ReplicaSets are the mechanism that provides replica maintenance. If the desired count is 3, the ReplicaSet works to keep 3 matching Pods running.

### Deployments

A Deployment is a high-level declarative resource that supervises Pods and manages application lifecycle operations.

Key points:
- It creates and manages ReplicaSets under the hood.
- It handles version rollouts and updates.
- It automatically restarts containers if they crash, exit, or are killed.
- The default restart policy is `Always`.

Deployments are the recommended way to run replicated stateless workloads because they provide both replica management and update control.

### How they work together

The typical relationship is:

1. The user declares a Deployment with a desired image and replica count.
2. The Deployment creates a ReplicaSet configured to maintain that number of Pods.
3. The ReplicaSet creates and maintains the Pods.
4. If a Pod dies or is deleted, the reconciliation loop detects the mismatch and provisions a replacement Pod.

This is the declarative model described in the notes: you declare the desired state, and Kubernetes continuously aligns the actual state with that target.

### Declarative design and reconciliation

Kubernetes resources are defined as structured data records in YAML or JSON and checked into version control. Key manifest fields include:

- `apiVersion`: the API version, such as `v1` or `apps/v1`
- `kind`: the resource type, such as `Deployment`, `Service`, or `Pod`
- `metadata`: identification including `name`, `labels`, and `annotations`
- `spec`: the desired state, such as `replicas`, container `image`, `ports`, and `resources`

Core management commands include:

- `kubectl apply -f <filename.yaml>`: submits or updates declarative resource configurations
- `kubectl get <resource>`: retrieves status summaries
- `kubectl describe <resource>/<name>`: provides detailed information, container states, and recent event logs
- `kubectl delete -f <file>` or `kubectl delete <resource> --selector <label>`: removes specified objects

The reconciliation loop is central: controllers continuously compare the desired state stored internally against the cluster's actual state. If a Pod crashes or is manually deleted, the discrepancy is detected and corrected.

### Scheduling and node assignment

The Kubernetes scheduler assigns unscheduled Pods to suitable nodes:

- When a new Pod is requested, it enters the scheduler's queue.
- The scheduler evaluates available node capacities against the Pod's specified resource requests.
- Once scheduled to a node, the node's kubelet agent picks up the Pod specification and starts the container runtime.
- If a worker node fails completely, its Pods become unscheduled and return to the scheduler queue for reassignment to healthy nodes.

### Lifecycle management

Deployments also rely on probes and availability controls:

- **Liveness probes** check whether the container is alive and functioning. If a liveness probe fails, Kubernetes kills and restarts the container. Probe types include `httpGet`, `tcpSocket`, and `exec`.
- **Readiness probes** determine whether a container is ready to accept user traffic. If a readiness probe fails, the Pod is removed from Service load balancing endpoints without being killed. This is essential for zero-downtime upgrades.
- **Pod Disruption Budgets** protect availability during voluntary cluster evictions by using `minAvailable` or `maxUnavailable`. They do not protect against involuntary node hardware failures.

---

## 10. Illustrate the complete functionality of package manager for Kubernetes.

The package manager for Kubernetes described in the notes is **Helm**, a CNCF package manager that simplifies deploying complex applications by bundling YAML templates and default configurations into **Helm Charts**.

### What Helm does

Helm helps manage Kubernetes applications that consist of many resources. Instead of applying many separate YAML files by hand, Helm packages the needed resource definitions and default configuration into a chart, then installs that chart as a running instance in the cluster.

Key characteristics from the notes:
- Helm charts contain templates and metadata.
- Helm charts do **not** package binary container images directly.
- Helm simplifies deployment of complex applications by bundling YAML templates and default configurations.

### Core terminology

- **Chart**: A package containing all resource definitions needed to run an application.
- **Repository**: A shared location where charts are published and downloaded.
- **Release**: A specific running instance of a chart deployed into a cluster.

This separation matters: the chart is the package, the repository is where charts are shared, and the release is what actually runs in a cluster.

### How it works in practice

A Helm chart typically includes:
- templated Kubernetes manifests
- default configuration values
- metadata describing the package

When you install a chart, Helm combines the templates with the configuration values and renders the Kubernetes resources, then deploys them into the cluster as a release.

This is useful because many applications require multiple coordinated resources, such as Deployments, Services, and configuration objects. A chart bundles them together with sensible defaults.

### Key CLI operations

From the notes, the key commands include:

```
helm install --name <release-name> <chart-path>
helm list
helm status <release-name>
```

- `helm install` deploys a chart as a named release.
- `helm list` lists releases.
- `helm status` shows the status of a specific release.

### Why it is called a package manager

Helm behaves like a package manager for Kubernetes applications in the same general sense that package managers help with software distribution and installation:
- It provides a bundle format for application definitions.
- It supports shared repositories.
- It supports installed instances, or releases, that can be tracked and managed.

The important limitation is that Helm packages the Kubernetes resource definitions and configuration, not the container image binaries themselves. The container image still comes from a registry; Helm helps describe and deploy the Kubernetes objects that run that image.

### Example usage pattern

A simple Helm-based workflow looks like this:

1. Obtain or create a chart for the application.
2. Install the chart as a release.
3. List and inspect the release.
4. Upgrade or uninstall the release as needed.

Conceptually:

```
helm install --name demo-release ./mychart
helm list
helm status demo-release
```

### Summary of functionality

Helm's complete functionality, as described in the notes, can be summarized as:
- Packaging Kubernetes resource definitions into charts
- Providing shared chart repositories
- Installing charts as releases
- Managing releases with commands such as install, list, and status
- Simplifying deployment of complex applications through templated YAML and default configuration
- Leaving container image binaries to the container registry, while Helm manages the Kubernetes-side application packaging and release lifecycle

---

## Quick Revision File — DevOps

### Chapter 1: Revolution in the Cloud

- **CapEx vs. OpEx**: Computing moved from buying hardware (CapEx) to renting shared remote compute on a pay-per-use basis (OpEx).
  - Bare metal = CapEx: large upfront cost, hard to scale, becomes obsolete quickly.
  - Cloud = OpEx: pay per use, third party handles maintenance and scaling.
  - Analogy: cloud compute is like electricity grid power or a taxi; buying a private power plant or car is CapEx.
- **IaaS**: Infrastructure as a Service; compute treated as an elastic commodity.
- **SaaS**: Software as a Service; fully managed rented applications.

### Chapter 2: Dawn of DevOps

- **DevOps definition**: A cultural movement bringing development and operations together to share understanding and responsibility for system reliability and software correctness. It is primarily an organizational and human issue, not a software tool.
- **CAMS pillars (John Willis)**:
  1. Culture
  2. Automation
  3. Measurement
  4. Sharing
  - Memory trick: "Cats Are Mostly Sleepy"
- **DevOps Trinity (Brian Dawson)**: People & Culture, Process & Practice, Tools & Technology.
- **Infrastructure as Code (IaC)**: Provisioning and managing infrastructure using automated software scripts instead of manual racking and cabling.
- **NoOps Fallacy**: The mistaken belief that cloud and containers eliminate operations staff. In reality, operational work shifts earlier into the development lifecycle.

### Chapter 3: Coming of Containers

- **Container definition**: A standard packaging format that bundles an application with all required dependencies into an image executed by a container runtime.
- **Origin**: Inspired by Malcolm McLean (Sea-Land, 1950s) and standardized shipping boxes.
- **VM vs. Container**:
  - VM: whole guest OS plus unrelated libraries; large image (~1 GiB); about 30% slower due to emulated CPU overhead; depends on a full guest OS; fixed disk image.
  - Container: only application binary plus required dependencies; compact (~100x smaller); native CPU performance; depends only on the host OS kernel; shared, addressable, cached filesystem layers.

### Chapter 4: Container Orchestration

- **Container orchestrator**: software that joins multiple server machines into a unified cluster and handles scheduling, orchestration, and cluster management.

### Chapter 5: Kubernetes

- Open-sourced by Google in 2014, derived from Borg and Omega; won the orchestration wars by late 2017.
- Etymology: Greek word meaning "helmsman" or "pilot".
- **Deployment strategies**:
  - Rolling updates: replace old Pods with new Pods incrementally for zero downtime.
  - Canary deployments: roll out to a small fraction first to detect defects before full rollout.
  - Blue-green deployments: run the new version in parallel and switch traffic once verified.
- **Autoscaling**: automatically adds replicas when CPU/load thresholds are breached and scales down when load subsides.

### Chapter 6: Cloud Native

- **Definition**: applications designed to leverage cloud infrastructure, containers, microservices, and dynamic orchestration; managed under CNCF, founded in 2015 under the Linux Foundation.
- **6 core characteristics (AURDOD)**:
  1. Automatable
  2. Ubiquitous & Flexible
  3. Resilient & Scalable
  4. Dynamic
  5. Observable
  6. Distributed
- **Monolith vs. microservices**: monoliths are easier to understand initially but hard to scale; microservices scale independently but add distributed complexity. Containerizing a monolith is a valid first migration step.

### Chapter 7: Future of Operations

- Systems administration evolves into Distributed DevOps with SREs and central Developer Productivity Engineering teams.
- The boundary between development and operations fades: "it's all just software now."

### First Steps with Kubernetes

- **Run a container**:
  - `docker container run -p 9999:8888 --name hello cloudnatived/demo:hello`
  - `-p 9999:8888` forwards host port 9999 to container port 8888.
- **Demo app**: Go application serving "Hello, 世界" on port 8888.
- **Build image**: `docker image build -t myhello .`
- **Multi-stage build**:
  - Stage 1: build with `golang:1.11-alpine`
  - Stage 2: production from `FROM scratch`, copy binary with `COPY --from=build`
  - Benefit: image shrinks from ~350 MiB to ~6 MiB
- **Container registries**: central service to store, tag, and retrieve images by unique name; default is Docker Hub.
- **Publishing algorithm**: `docker login` -> `docker image tag myhello YOUR_DOCKER_ID/myhello` -> `docker image push YOUR_DOCKER_ID/myhello`
- **Hello Kubernetes**:
  - `kubectl run demo --image=YOUR_DOCKER_ID/myhello --port=9999 --labels app=demo`
  - `kubectl port-forward deploy/demo 9999:8888`
  - `kubectl run` creates a Deployment object that creates and manages Pods.
- **Pod statuses**:
  - Running
  - ContainerCreating
  - ErrImagePull / ImagePullBackOff
- **Minikube**: local single-node Kubernetes cluster inside a VM.

### Getting Kubernetes: Cluster Architecture

Control plane components:
- `kube-apiserver`: central REST API frontend
- `etcd`: consistent distributed key-value store for cluster state, not app data
- `kube-scheduler`: assigns unscheduled Pods to worker nodes
- `kube-controller-manager`: runs background controllers to reconcile desired vs. actual state
- `cloud-controller-manager`: integrates with cloud infrastructure

Worker node components:
- `kubelet`: node agent that drives the container runtime via the API server
- `kube-proxy`: manages network rules, IP forwarding, and service routing
- Container runtime: executes containers, e.g., containerd, Docker

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
- `kubeadm`: official installer
- `kops`: declarative cluster lifecycle tool for AWS/GCP
- `Kubespray`: Ansible-based installer
- Kubernetes The Hard Way: manual component installation tutorial

Clusterless container services:
- Examples: Amazon Fargate, Azure Container Instances
- Abstract nodes and clusters entirely; no kubectl access; pay per second of execution

### Kubernetes Objects: Deployments, Pods, ReplicaSets

- **Pods**: fundamental execution unit; one or more co-located containers; share storage, network namespace, and node placement; often single-container; multi-container Pods used for tight integration.
- **ReplicaSets**: maintain a specified number of identical Pod replicas; controller continuously monitors and adjusts Pod count; users rarely manage directly; Deployments manage them automatically.
- **Deployments**: declarative high-level resource; supervises Pods; manages lifecycle, rollouts, and updates; creates and manages ReplicaSets; restarts containers on crash/exit/kill; default restart policy is Always.

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
- Services: stable IP and DNS endpoint; load balance across Pods matching a label selector; map service port to targetPort.

### Helm: Kubernetes Package Manager

- CNCF package manager; bundles YAML templates and default configurations into Helm Charts.
- Chart: package of resource definitions.
- Repository: shared chart location.
- Release: running instance of a chart.
- Charts contain templates and metadata; they do not package binary container images directly.
- Commands: `helm install --name <release-name> <chart-path>`, `helm list`, `helm status <release-name>`.

### Managing Resources

- CPU: cores or millicpus/millicores; 100m = 0.1 CPU core; 1 CPU unit = 1 AWS vCPU, Google Cloud Core, Azure vCore, or hyperthread.
- Memory: bytes, MiB, GiB.
- Requests: minimum guaranteed resources; scheduler uses them to pick a node; insufficient capacity leaves Pod Pending.
- Limits: maximum allowed resources; CPU limit exceeded leads to throttling; memory limit exceeded leads to OOM kill and possible restart.
- Overcommit and eviction: limits can exceed physical capacity; under memory pressure, Kubernetes evicts Pods exceeding requests first.

### Container Lifecycle

- Liveness probe: checks if container is alive; failure triggers restart; types include httpGet, tcpSocket, exec; key settings include initialDelaySeconds and periodSeconds.
- Readiness probe: checks if container can accept traffic; failure removes Pod from Service endpoints without killing it; required for zero-downtime upgrades; must return HTTP 200 OK.
- minReadySeconds: keeps a newly started Pod ready for a set duration before rollout continues.
- Pod Disruption Budgets: protect availability during voluntary evictions using minAvailable or maxUnavailable; do not protect against involuntary node failures.

### Namespaces

- Partition a cluster into isolated virtual sub-clusters for teams or environments.
- Resources hidden across namespaces by default; system components run in kube-system.
- Use `--namespace` or `-n` with kubectl.
- Deleting a namespace permanently deletes everything inside it.
- Cross-namespace DNS pattern: `<service-name>.<namespace>.svc.cluster.local`.
- ResourceQuota: cumulative caps per namespace.
- LimitRange: default requests and limits in a namespace.

### Optimizing Cluster Costs

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

---

## Quick Revision File — Microservices

### What Are Microservices?

- The term emerged in the early 2010s for an architecture style built from small, independent components that work together.
- Microservices are hard: many independent parts must work together, and management, maintenance, support, and testing costs add up at scale.
- Done well, microservices make software changes faster and safer at scale.

Two definitions from the notes:

1. **Lewis and Fowler**: an approach to developing a single application as a suite of small services, each running in its own process and communicating with lightweight mechanisms, built around business capabilities and independently deployable by fully automated deployment machinery.
   - Nine characteristics: componentization via services; organization around business capabilities; decentralized governance; infrastructure automation; products not projects; smart endpoints and dumb pipes; design for failure; evolutionary design.

2. **Nadareishvili, Mitra, McLarty, and Amundsen**: a microservice is an independently deployable component of bounded scope that supports interoperability through message-based communication; microservice architecture is a style of engineering highly automated, evolvable software systems made up of capability-aligned microservices.

### Three General Design Traits

1. The application architecture is primarily composed of machine-invocable services made available on a network.
2. Service size is an important design factor; boundaries include runtime, design-time, and people factors.
3. The software system, the organization, and the way of working are holistically optimized to achieve a goal.

### Why Build Microservices

- The key reason is to reduce coordination costs.
- Microservices offer a way to increase speed without compromising safety, at scale.

### Coordination Cost Problem

- Complex software needs many people and a lot of time.
- Multiple teams build different parts on independent roadmaps and must periodically coordinate.
- That creates a tension between speed and safety.
- Microservices reduce coordination needs through autonomous teams working on small, isolated batches.

### Litmus Test

- When in doubt, ask: "Is this decision going to reduce coordination costs for my teams or not?"

### The Hard Parts

- **Long feedback loops**: impactful decisions are hard to measure; problems may emerge much later.
- **Too many moving parts**: microservices are a complex adaptive system with emergent behavior; small changes can break other things.
- **Analysis paralysis**: long feedback loops plus complexity make decisions highly impactful and hard to measure, leading to endless speculation instead of delivery.

### Learning by Doing

- Many successful implementers learned through iteration and often through failed architectures first.
- The book provides prescriptive guidance to accelerate learning.
- The Dreyfus model underlies the approach: start by following prescriptive guidance, then develop proficiency.

### Running Example

- A simplified airline reservations system with a read-only flight information service and a seat reservation service.

### The "Up and Running" Microservices Model

Focus areas:
1. Team design
2. Microservice design
3. Data design
4. Cloud platform
5. Microservice deployment
6. Release and change

### Operating Model

- The set of people, processes, and tools that underlies the system.
- It shapes decision-making and work; think of it as the operating system for the solution.

### Conway's Law

- Any organization that designs a system will produce a design whose structure is a copy of the organization's communication structure.

### People Factors

- **Team size**: too many people increases coordination; too few reduces capacity. The book sets team size at about five to eight people.
- **Team skills**: cross-functional teams make better decisions faster; members should directly influence output.
- **Inter-team coordination**: communication among teams can bog down a system; balance independence and coordination; design teams before architecture where possible (inverse Conway maneuver).

### Team Topologies

Four team types:
- **Stream-aligned**: owns and runs a deliverable piece of work and continually delivers business value; embodies "you build it, you run it."
- **Enabling**: supports other teams through consulting; specialists bridge gaps.
- **Complicated-subsystem**: works on domains that are difficult to understand and require scarce expertise.
- **Platform**: supports other teams through self-service tooling; scales through tools and processes.

Three interaction modes:
- **Collaboration**: both teams work closely together; good for learning and innovation; hard to scale.
- **Facilitating**: unidirectional support; one team helps another deliver.
- **X-as-a-service**: minimal coordination; one team provides a finished product, API, or platform for others to consume.

### Designing a Team Topology

Five steps:
1. Establish a system design team.
2. Create a microservices team template for future teams.
3. Define platform teams.
4. Add enabling and complicated-subsystem teams.
5. Add key consumer teams.

### SEED(S): Seven Essential Evolutions of Design for Services

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

### Rightsizing Microservices

- Do not draw boundaries based only on technical needs.
- Avoid creating too many microservices too soon.
- Start with a handful of services and increase slowly.

### Sam Newman's Three Rules

- **Loose coupling**: services operate without deep awareness of other services' internal implementation; a change in one should not force redeployment of another.
- **High cohesion**: behaviors that change for the same business reason are grouped inside the same service boundary.
- **Aligned with business capabilities**: organize services around what the business does, not technical tiers.

### DDD and Context Mapping

- A bounded context defines the range of applicability of each model.
- The representation of domains in a larger system and how they collaborate is the context map.
- Describing those collaborations is context mapping.
- A DDD aggregate is a collection of related domain objects viewed as a single unit by external consumers.

### Event Storming

- Finds bounded contexts in hours instead of weeks or months.
- It is not a full replacement for DDD but is a good approximation for discovering bounded contexts.
- Process:
  1. Discover domain events: participants write key domain events on orange stickies, one per note, on a timeline.
  2. Enforce the timeline: arrange notes into a coherent timeline and remove duplicates.
  3. Reverse narrative and command identification: walk the timeline backward and identify commands on blue stickies.
  4. Identify aggregates/bounded contexts: commands are accepted by aggregates and produce events; clusters around aggregates become bounded contexts.
  5. Competitive analysis: prioritize the bounded contexts.

### Universal Sizing Formula

- Bounded contexts are a great starting point, but microservice boundaries are not synonymous with bounded contexts.
- Boundaries evolve and tend toward increasing granularity as organizations and applications mature.
- It is typically easier to split a service than to merge services or move capabilities between services.

Three principles:
1. Start with few microservices, possibly using bounded contexts.
2. Keep splitting as the application and services grow, guided by the needs of coordination avoidance.
3. Be on the right trajectory for decreasing coordination; this is more important than how perfectly services are sized now.

### Data: Independent Deployability and Data Sharing

- Independent deployability means changing and deploying one microservice without changing or deploying any other microservice.
- Sharing data spaces is a primary killer of independent development and independent deployability.
- Microservices must own their data.
- Microservices can share physical database clusters.
- Microservices must own and embed their own data; no direct cross-service data access is allowed.

Isolation:
- **Logical isolation**: services must not share logical schemas, tables, or database spaces.
- **Physical isolation**: services can and should share physical database clusters to avoid massive infrastructure costs, provided they maintain strict logical isolation.

### Data Delegate Pattern

- When multiple services need the same core data, create an authoritative delegate service that wraps and hides the data.
- All other services must query or mutate the data solely through that service's API.

### Data Lakes

- Used when cross-boundary read access is required for analytics, machine learning, or auditing without hitting production service endpoints.
- Services stream copies of their data via message brokers into a data lake.
- Data lakes are read-only, queryable aggregates and are never the database of record.

### Saga Pattern

- A saga is a sequence of local transactions across multiple services.
- Every step executes its local action and registers a compensating action on a routing slip.
- Sagas achieve eventual consistency, not instant ACID rollback.
- Compensating actions are visible.
- Order steps carefully; steps that are hard to compensate should generally come later.

### Event Sourcing

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

### CQRS

- Command Query Responsibility Segregation: separate the write path from the read path.
- Write side: accepts commands and writes immutable facts to the event store.
- Read side: subscribes to events and builds specialized, read-optimized projection views.
- The event store prioritizes consistency; query projections prioritize availability and read speed.
- Use Event Sourcing and CQRS only when necessary; they complicate implementation.
- They are most useful when you need to avoid data sharing in sophisticated cases that require joins across service boundaries, or when auditability and time-based state reconstruction matter.
- Conventional relational modeling is often simpler.

### CAP Theorem

- A distributed shared-data system can have only two of consistency, availability, and partition tolerance.
- Network partitions cannot be avoided, so the real choice is between consistency and availability.
- With CQRS, use multiple systems and minimize data sharing: prioritize consistency in the event store and availability in the query indices.
- Query indices may get consistency wrong, but they are not authoritative and can be re-indexed from the event store.

### Auditability

- Event Sourcing preserves a perfect history of every change, including the value at any past time and how and when it changed.
- Logging to a file or to tools like Splunk or ELK is not Event Sourcing.
- In Event Sourcing, the event log is the source of truth; with Splunk logs, the source of truth is usually the relational model, and logs are used only occasionally to hunt bugs.

### Microservices and APIs

- Microservices are the implementation of the system.
- APIs are the outward-facing interface.
- Web APIs are layered on top of microservices.

### Decisions and ADRs

- Software and business outcomes depend on the quality of decisions.
- Decisions may become outdated as technology, people, or situations change.
- Architecture Decision Records capture important decisions so they can be re-evaluated and improved.
- Four elements: context, alternatives, choice, impact.
- Lightweight Architectural Decision Record (LADR) is concise, written in Markdown, and managed in version control like source code.
- LADR structure: title, status, context, decision, consequences.
