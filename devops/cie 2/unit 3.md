---
tags: [devops, unit-notes]
---

# UNIT 3 — OPERATING CLUSTERS & KUBERNETES POWER TOOLS

> **Syllabus (Ch. 6 + Ch. 7):**
> **Operating Clusters** — cluster sizing and scaling, conformance checking, chaos testing.
> **Kubernetes Power Tools** — mastering `kubectl`, working with resources, working with containers, contexts and namespaces, Kubernetes shells and tools, Kubernetes IDEs.

---

# PART A — OPERATING CLUSTERS

## 1. Introduction
Once a cluster is running, three questions decide whether it is production-ready:

1. **Is it healthy?** → conformance checking, audit logging.
2. **Can it scale with demand?** → cluster sizing and scaling, autoscaling, federation.
3. **Will it survive failures, and are we wasting money?** → chaos testing, right node sizes, draining.

A good **managed service** (GKE, EKS, AKS) handles control-plane availability, authentication and upgrades. But **what you run on the cluster is still your responsibility** — sizing, capacity, resilience and cost are yours.

---

## 2. Cluster Sizing and Scaling

### 2.1 Why sizing matters
- Cluster cost (self-hosted *and* nearly all managed services) depends directly on the **number and size of nodes**.
- **Too small** → workloads fail or degrade under load.
- **Too large** → money is wasted on idle capacity.
- Kubernetes packs containers evenly across machines, so it usually achieves **higher utilisation than traditional servers** — but only if you size it deliberately.

### 2.2 Capacity planning
- **Start from what you already run**: if the current architecture needs **10 cloud VMs**, plan for roughly **10 similar nodes + 1–2 spare** for redundancy.
- You may need **fewer** nodes than VMs, because the scheduler bin-packs containers across the whole cluster instead of leaving each server half-empty.
- There is no formula that replaces experience — **tune over time** using real usage data.
- Practical rules:
    - Add **20–30% headroom** for spikes, node failure and DaemonSets.
    - Remember **system overhead per node**: kubelet, container runtime, CNI, kube-proxy, logging/metrics agents.
    - Watch **requests vs actual usage** — unused requests are stranded capacity.

### 2.3 The smallest cluster
- The smallest possible cluster is a **single node** — fine for learning and small development workloads (Chapter 2).
- **Single-node drawback**: no resilience. If the **node hardware**, the **API server**, or the **kubelet** (the node agent that runs workloads) fails, everything is down.
- **Managed services** provision the control plane for you, so you only think about workers.
- **Self-built cluster — control plane layout**:
    - **Minimum for resilience = 3 control-plane nodes.**
    - 1 node → no resilience.
    - 2 nodes → they can disagree about leadership (split-brain).
    - 3 nodes → a majority/quorum can always be formed.
- A cluster of **only control-plane nodes** works but is **not recommended** — workloads then compete with the control plane for CPU/memory. Add worker nodes.
- **Worker nodes**: with an HA control plane, **one** worker is possible, but **two is the sensible minimum** (survive one node failure and still run at least two replicas); **three is better**.
- More nodes is generally better because the scheduler **cannot always balance workloads perfectly**.

**K3s**
- A **lightweight Kubernetes distribution** that packages all components into a **single binary**.
- Great for **isolated and/or resource-constrained** environments (edge, IoT, labs, single-node installs).

> **Exam tip:** Managed = provider gives you 3 HA control-plane nodes. Self-hosted = **3 control-plane + 2 (ideally 3) workers** minimum.

### 2.4 The biggest cluster
- There **is** an upper limit, but most users never reach it.
- **Kubernetes 1.22 officially supports up to 5,000 nodes.**
- **Why the limit?** Clustering requires communication between nodes. As the cluster grows, the number of **communication paths** and the cumulative **load on the underlying database (etcd)** grow **exponentially**.
- Beyond 5,000 nodes Kubernetes may still run, but **responsiveness is not guaranteed** for production.
- A larger cluster puts a **much heavier load on the control plane** — self-hosted control-plane nodes must be very powerful machines.

| Resource | Maximum |
|:--|:--:|
| Nodes | **5,000** |
| Total Pods | **150,000** |
| Total containers | **300,000** |
| Pods per node | **100** |

> **Best practice:** stay below **5,000 nodes / 150,000 Pods**. If you need more, run **multiple clusters** instead of one giant one.

### 2.5 Federated clusters
- **Federation = keeping two or more clusters synchronised, running identical workloads.**
- Uses:
    - clusters on **different cloud providers**;
    - **resilience** — the group keeps running if one cluster fails;
    - **different geographical locations** to cut latency for users.
- Most users **do not need federation**. Even at very large scale, most organisations run **multiple unfederated clusters** of a few hundred to a few thousand nodes.
- Smaller clusters split by **team or application boundary** are usually **easier to manage** than one huge federated cluster.

> **Best practice:** use federation only when you must replicate workloads across clusters for **geo redundancy or latency**.

### 2.6 Do I need multiple clusters?
- Unless you are at very large scale or multicloud, you probably need **one or two clusters**: **production + staging/testing**.
- For convenience, **partition one cluster with namespaces** instead of paying the admin overhead of many clusters.
- Separate clusters **are** justified when you need **complete isolation**:
    - security and **regulatory compliance**;
    - **protected health information (PHI)**;
    - data that **legally cannot leave a geographic region**.

### 2.7 Nodes and instances
Node capacity = **CPU cores + memory + (to a lesser extent) disk**. The design question: *10 very large nodes, or 100 smaller ones?*

**(a) Picking the right node size**
- No universal answer — it depends on the provider and your workload.
- Some providers **discount larger instances**, so compute-heavy workloads can be cheaper on a few big nodes.
- But HA needs **work spread across nodes**, and nodes with too much spare capacity waste money.
- If you need ~10 nodes for HA but each runs only a couple of Pods → use **very small instances**.
- If you genuinely need only **two nodes** → make them **large** and take advantage of better instance pricing.

**(b) Cloud instance types**
- Kubernetes components themselves consume resources, so **the smallest instance sizes are usually unsuitable**.
- **Control plane for a small cluster (up to ~5 nodes): at least 2 CPUs and 2 GiB memory.** Larger clusters need more per control-plane node.
- For **tens of nodes**, provision a **mix of 2–3 instance sizes**:
    - memory-hungry / compute-intensive Pods → large nodes;
    - small Pods → small nodes (via node affinities);
    - this gives the **scheduler maximum freedom**.

**(c) Heterogeneous nodes**
- Not all nodes are equal: some need a **GPU** (high-performance parallel processor used for ML, data analysis — not just graphics).
- Declare a GPU in **resource limits** → the Pod only runs on GPU nodes and gets priority over Pods that could run anywhere.
- **Containers are not VMs**: the process runs directly on the host kernel, so a **Windows binary needs a Windows node**. Most containers are Linux → run mostly Linux nodes, plus one or two special node types.

**(d) Bare-metal servers**
- Kubernetes can join machines of different sizes, architectures and capabilities into **one unified logical machine**.
- Many organisations already own **bare-metal servers in data centres** — join them into the cluster instead of writing them off.
- Cloud converts **CapEx → OpEx**; bare metal is still CapEx. Kubernetes lets you get value from hardware you already own.

### 2.8 Scaling the cluster
Over time you grow or shrink the cluster as demand changes.

**(a) Instance groups**
- Adding nodes is easy; self-hosted clusters use a tool such as **kops**.
- An **instance group** is a set of nodes of a given instance type (e.g. `m5.large`). Scale it by changing **min/max size**, **instance type**, or both.

| Platform / tool | Term for the same concept |
|:--|:--|
| kops (self-hosted) | **Instance group** |
| GKE and managed services | **Node pool** |
| EKS `eksctl` | **Nodegroup** |

**(b) Scaling down — always drain first**
```bash
kubectl drain node-1 --ignore-daemonsets --delete-emptydir-data
```
- **Draining** gradually shuts down or moves running Pods elsewhere. Most cluster tools drain automatically, but `kubectl drain` works manually.
- Only terminate a node **after** its Pods have been rescheduled and you have confirmed there is enough spare capacity.
- **PodDisruptionBudgets (PDBs)** cap how many Pods can be unavailable: specify `minAvailable` or `maxUnavailable`. If a drain would violate a PDB, **the drain blocks** until you change the budget or free capacity.
- **Why drain?** It lets Pods **shut down gracefully**, clean up and save state. Powering a node off terminates Pods immediately.

**(c) Autoscaling**
- **Autoscaling** = automatically adding/removing instances in a group, driven by a **metric** or a **schedule**.
- **AWS Auto Scaling Groups (ASGs)** maintain min/max instance counts: replace a failed instance, remove excess ones.
- Three ways to scale:

| Mode | Behaviour |
|:--|:--|
| **Fixed min/max** | Self-healing: replace failed instances, keep the group size |
| **Scheduled** | Grow/shrink at known times (e.g. business hours) |
| **Dynamic on demand** | Metric-driven — e.g. average CPU **> 90% for 15 minutes** → add instances until usage falls; scale down when demand drops |

- **Cluster Autoscaler** is the Kubernetes add-on (used by kops; AKS and other managed services offer their own).
- Tuning takes time; **many users don't need it** — most clusters start small and add a node occasionally.
- It is most useful for **large-scale users** and **highly variable demand**.

> **Best practice:** don't enable cluster autoscaling just because it exists. Start manual, monitor usage, and automate once you understand the pattern.

---

## 3. Conformance Checking

### 3.1 What and why
- *"When is Kubernetes not Kubernetes?"* — Kubernetes can be installed and configured in many ways, which is a problem for portability.
- For Kubernetes to be a **universal platform**, a workload must run on **any** conformant cluster and behave the same: same API calls, same objects, same behaviour.
- Kubernetes ships a **test suite** that verifies a cluster is **conformant** — it satisfies the core requirements for a given version.
- **Fails** → there is a problem in your setup that must be fixed.
- **Passes** → confidence that Kubernetes apps work on your cluster *and* that what you build will work elsewhere.

### 3.2 CNCF certification
The **CNCF (Cloud Native Computing Foundation)** owns the Kubernetes project and trademark and certifies products, engineers and vendors.

| Certification | For whom | Key points |
|:--|:--|:--|
| **Certified Kubernetes** (mark) | Managed services, distributions, installer tools | Meets the CNCF standard. Products with "Kubernetes" in the name **must** be certified. Vendors **self-certify by running Sonobuoy**. Must track the latest version and update **at least annually**. |
| **CKA** — Certified Kubernetes Administrator | Individual engineers | Proves skills in installation/configuration, networking, maintenance, API knowledge, security, troubleshooting. Online **practical** exam; reputed as tough. |
| **KCSP** — Kubernetes Certified Service Provider | Vendors | Must be a CNCF member, provide enterprise support, contribute to the community, and employ **3+ CKA-certified engineers**. |

> **Best practice:** look for the Certified Kubernetes mark when choosing a platform; prefer **KCSP** vendors; look for **CKA** when hiring administrators.

### 3.3 Conformance testing with Sonobuoy
- **Sonobuoy** is the standard CLI for running the Kubernetes conformance suite **inside your cluster**, using your existing `kubectl` credentials.
- Use it when self-managing a cluster, or to verify a managed service is configured correctly and up to date.

```bash
sonobuoy run                      # start the suite (creates the sonobuoy namespace)
kubectl get pods -n sonobuoy      # watch progress
sonobuoy run --mode quick         # single test, verifies connectivity
results=$(sonobuoy retrieve)      # save results locally
sonobuoy results $results         # inspect
```

Sample output:
```
Plugin: e2e
Status: passed
Total: 5771
Passed: 1
Failed: 0
```

- The **full suite can take an hour or more**; `--mode quick` is the smoke test.
- `Plugin: e2e` = end-to-end conformance tests.
- Security scanners (Ch. 15) used alongside Sonobuoy show where the cluster deviates from industry best practice.

### 3.4 Kubernetes audit logging
- Answers **"who did what on the cluster, and when?"** — e.g. you find an unknown Pod and want its origin.
- With audit logging enabled, **every request to the API** records:
    - **timestamp**;
    - **who** made it (user / service account);
    - **details** of the request (resources queried);
    - the **response**.
- Ship audit events to central logging and **alert** on them.
- Good managed services (e.g. **GKE**) enable audit logging by default; otherwise you must configure it.

---

## 4. Chaos Testing

### 4.1 Concept
- The only real way to verify high availability is to **kill nodes and see what happens** — *"trust, but verify"*. Same for Pods: terminate one at random and check that Kubernetes restarts it and your error rate is unaffected.
- Manual testing is slow and biased — you unconsciously avoid critical resources.
- **Chaos Monkey testing** = **automated, random interference with production services**, named after Netflix's tool.
- Mental model: a monkey in the data centre randomly ripping out cables; the system must be designed to keep working.

**Netflix Simian Army**

| Tool | What it does |
|:--|:--|
| **Chaos Monkey** | Terminates random cloud servers |
| **Latency Monkey** | Injects communication delays to simulate network problems |
| **Security Monkey** | Looks for known vulnerabilities |
| **Chaos Gorilla** | Drops an entire AWS availability zone |

### 4.2 Only production is production
- You *can* run chaos on staging, but that tells you little.
- Reasons (Charity Majors):
    - Many systems are **too big, complex and costly to clone**.
    - **User traffic is unpredictable** and cannot be mocked — replaying yesterday's traffic cannot predict tomorrow's.
- Experiments must be **automated and continuous**, not a one-off check — repeated runs surface new weaknesses and confirm that old ones are actually fixed.

### 4.3 Kubernetes chaos tools

| Feature | **chaoskube** | **kube-monkey** | **PowerfulSeal** |
|:--|:--|:--|:--|
| What it kills | Random Pods | Pods of **opted-in** Deployments | Nodes, namespaces, Deployments, Pods |
| Default behaviour | **Dry-run** (shows what it would do) | Builds a schedule daily at **8 a.m.**; kills **10 a.m.–4 p.m.** on weekdays | Interactive **or** autonomous (policy-driven) |
| Targeting | Labels, annotations, namespaces, blackout dates/times | Opt-in annotations (`kube-monkey/mtbf`, `kill-mode`, `kill-value`) | Policy files (targets, exclusions, schedule, aggression) |
| Ease | **Simplest** — good starting point | Per-service control | **Most flexible** |

- **chaoskube** may kill **any** Pod in **any** namespace, including `kube-system` Pods and itself — set filters *before* disabling dry-run.
- **kube-monkey** annotations:
    ```yaml
    kube-monkey/mtbf: "2"                      # mean time between failures = 2 days
    kube-monkey/kill-mode: "random-max-percent"
    kube-monkey/kill-value: "50"               # kill up to 50% of the Deployment's Pods
    ```
- **PowerfulSeal** interactive mode can terminate nodes/namespaces/Deployments/Pods; autonomous mode follows policies (what to target, what to avoid, when, how aggressive).

> **Best practice:** if your app needs HA, run a chaos tool regularly — but clear it first with the people running the cluster and the apps under test. The cloud provider is already doing its own chaos testing.

---

# PART B — KUBERNETES POWER TOOLS

## 5. Mastering `kubectl`

### 5.1 Shell aliases
```bash
alias k=kubectl
alias kg=kubectl get
alias kgdep=kubectl get deployment
alias ksys=kubectl --namespace=kube-system
alias kd=kubectl describe
```
- Put them in `.bash_profile` / `.zshrc`; most people start with `k` and add only memorable aliases.
- Ahmet Alp Balkan published a **logical alias system** that generates ~**800 aliases**.

### 5.2 Short flags
| Long flag | Short | Example |
|:--|:--:|:--|
| `--namespace` | `-n` | `kubectl get pods -n kube-system` |
| `--selector` | `-l` | `kubectl get pods -l "environment=staging"` |
| `--watch` | `-w` | `kubectl get pods -w` |
| `--follow` | `-f` | `kubectl logs -f POD` |
| `--container` | `-c` | `kubectl logs POD -c CONTAINER` |

### 5.3 Abbreviating resource types
| Short | Resource | Short | Resource |
|:--|:--|:--|:--|
| `po` | pods | `cm` | configmaps |
| `deploy` | deployments | `sa` | serviceaccounts |
| `svc` | services | `ds` | daemonsets |
| `ns` | namespaces | `pv` | persistentvolumes |
| `no` | nodes | | |

```bash
kubectl get po,deploy,svc,ns
```

### 5.4 Auto-completion
- Works in **bash and zsh**; `kubectl completion -h` explains how to enable it.
- `kubectl cl<TAB>` → `cluster-info`; `kubectl <TAB><TAB>` lists all commands; `kubectl get pods --<TAB><TAB>` lists flags.
- It also completes **live object names** — Pods, Deployments, namespaces.

### 5.5 Getting help
```bash
kubectl -h                 # overview of all commands
kubectl get -h             # detailed help for one command
kubectl explain pods       # documentation for a resource type
kubectl explain deploy.spec.template.spec.containers.livenessProbe.exec
```
`kubectl explain` works fully offline and lets you drill into **any field**, to any depth.

### 5.6 More detailed output
```bash
kubectl get pods -o wide     # adds IP and NODE
kubectl get nodes -o wide    # adds INTERNAL-IP, VERSION, etc.
```

### 5.7 JSON output and `jq`
- `-o json` prints the full object (~**5,000 lines** for `kube-system` on the author's cluster) — filter it with **jq** instead of reading it.
- Install: `brew install jq`, `apt install jq`.

```bash
kubectl get pods -n kube-system -o json | jq '.items[].metadata.name'

# Busiest nodes by Pod count
kubectl get pods -o json --all-namespaces | jq '.items |
  group_by(.spec.nodeName) |
  map({"nodeName": .[0].spec.nodeName, "count": length}) |
  sort_by(.count) | reverse'
```

- **JSONPath** is built into `kubectl` and is enough for quick one-liners (less powerful than jq):
    ```bash
    kubectl get pods -o=jsonpath={.items[0].metadata.name}
    ```

### 5.8 Watching objects
```bash
kubectl get pods --watch
```
- Prints an update line every time a matching Pod's status changes (`ContainerCreating` → `Running`) instead of re-running `get`.
- For watching **any** resource in real time, use **kubespy** (§8.3).

### 5.9 Describing objects
```bash
kubectl describe pod POD_NAME
```
- Shows configuration plus **Events** — each stage of the container lifecycle and any errors. **Check Events first** when something is broken.

---

## 6. Working with Resources

### 6.1 Imperative commands
- So far: querying/listing and declarative `kubectl apply`.
- `kubectl` also has a full set of **imperative** commands that create/modify resources directly.

| Command | Purpose / example |
|:--|:--|
| `kubectl run` | Implicitly creates a Pod to run the given container |
| `kubectl create` | Explicitly create most resources: `kubectl create namespace my-new-namespace` |
| `kubectl delete` | Delete a resource: `kubectl delete namespace my-new-namespace` |
| `kubectl edit` | Open any resource in `$EDITOR`; on save, kubectl updates it like `apply`. Invalid YAML → kubectl reopens the file |

### 6.2 When NOT to use imperative commands
- The book stresses **declarative infrastructure as code**; imperative commands are **not recommended**.
- Only useful for **quick testing** and trying ideas.
- **The core problem is no single source of truth**: no record of who ran what, when, and with what effect; cluster state drifts from the manifests in Git; and the next `kubectl apply` **overwrites** the imperative changes — with surprising and potentially harmful results.

**The Alice and Bob story** (Kubernetes Up & Running):
1. Alice is on-call. Load spikes; she runs `kubectl scale` to raise replicas **5 → 10**.
2. Days later Bob edits the YAML in Git to use a new image — the file still says **5 replicas**.
3. Bob applies → replicas are cut in half → **overload or outage**.

Lessons:
- Alice forgot to update version control — easy under incident stress.
- Bob should have run **`kubectl diff`** before applying.
- Fix: **always change resources by editing version-controlled YAML** and applying it (GitOps).

### 6.3 Generating resource manifests
- Imperative commands are a great time-saver for **boilerplate**.
- `--dry-run=client` → don't create anything, just print what *would* be created.
- `-o yaml` → output YAML; `>` → write to a file that you edit and apply.

```bash
kubectl create deployment demo --image=cloudnatived/demo:hello \
  --dry-run=client -o yaml > deployment.yaml
```

### 6.4 Exporting resources
- Generate a manifest for a resource **already in the cluster** (e.g. one you created imperatively and tuned):

```bash
kubectl create deployment newdemo --image=cloudnatived/demo:hello
kubectl get deployments newdemo -o yaml > deployment.yaml
```
- The output contains runtime fields such as **`status`** — remove them before committing.
- **Migration path:** export all imperative resources to manifests and switch to declarative style.

### 6.5 Diffing resources
```bash
kubectl diff -f deployment.yaml
```
```
- replicas: 10
+ replicas: 5
```
- Shows exactly what would change **before** `apply`; `-` is the current value, `+` the new one.
- Two uses: confirm the change has the expected effect, and **detect drift** (someone edited the live object imperatively).

> **Best practice:** always `kubectl diff` before applying to production.

---

## 7. Working with Containers

### 7.1 Viewing logs
- In Kubernetes, **logs = whatever the container writes to stdout/stderr**.
- `kubectl logs` is still a first-class troubleshooting tool; production needs central aggregation, storage and graphing (Ch. 15).

```bash
kubectl logs POD_NAME
kubectl logs -n kube-system --tail=20 kube-dns-autoscaler-...
kubectl logs --namespace kube-system --tail=10 --follow etcd-docker-for-desktop
kubectl logs -n kube-system metrics-server -c metrics-server-nanny
```
- `--tail=N` → most recent N lines (long-running containers produce a lot).
- `--follow` / `-f` → stream new output live.
- `-c` / `--container` → choose a container in a multi-container Pod.
- **API server logs** show RBAC permission errors. If you can reach the control plane, `kubectl logs` the `kube-apiserver` Pod in `kube-system`; managed services hide the control plane — check the provider's docs.
- **Stern** (§9.4) is better for multi-Pod log watching.

### 7.2 Attaching to a container
```bash
kubectl attach demo-54f4458547-fcx2n
```
- Attaches your terminal to the container's output directly. Press **Enter** if no prompt appears.
- Defaults to the first container; use `-c` for another.

### 7.3 Watching resources with kubespy
- After a deploy, many things happen behind the scenes — `get`/`describe` only show **snapshots**.
- **kubespy** (from **Pulumi**, an IaC framework) watches one resource and prints how it changes in real time — e.g. for a Service: created → IP allocated → endpoints connected.

### 7.4 Forwarding a container port
```bash
kubectl port-forward POD_NAME 9999:8888
```
```
Forwarding from 127.0.0.1:9999 -> 8888
```
- Maps a **local** port to a **container** port; works for Pods and Services. Great for testing without exposing anything publicly.

### 7.5 Executing commands on containers
```bash
kubectl run alpine --image alpine --command -- sleep 999
kubectl exec -it alpine -- /bin/sh
kubectl exec -it -c container2 POD_NAME -- /bin/sh
```
- `-it` = interactive + TTY; defaults to the first container (`-c` to choose).
- Container isolation is good for security but inconvenient for debugging — `exec` gives you `ps`, `ls`, `cat`, editors, etc.
- **No shell in the image?** See §7.7 (BusyBox).

### 7.6 Running containers for troubleshooting
Run one-off containers to see what your app sees (DNS, HTTP, connectivity):

```bash
kubectl run demo --image cloudnatived/demo:hello --expose --port 8888

kubectl run nslookup --image=busybox:1.28 --rm -it --restart=Never \
  --command -- nslookup demo

kubectl run wget --image=busybox:1.28 --rm -it --restart=Never \
  --command -- wget -qO- http://demo:8888
# Hello, 世界
```

| Flag | Meaning |
|:--|:--|
| `--rm` | Delete the resources created when the command finishes (keeps node disk clean) |
| `-it` | Interactive (`i`) with a TTY (`t`) |
| `--restart=Never` | Don't restart when the container exits — run it once |
| `--command --` | Override the image entrypoint; everything after `--` is the command line |

**BusyBox**
- Tiny image containing many Unix tools — `cat`, `echo`, `find`, `grep`, `kill`, … — plus `ash`, a bash-like shell.
- Interactive shell:
    ```bash
    kubectl run busybox --image=busybox:1.28 --rm -it --restart=Never /bin/sh
    ```
- **Use version 1.28**: later BusyBox tags have a **DNS lookup problem** in Kubernetes.
- Handy alias:
    ```bash
    alias bb="kubectl run busybox --image=busybox:1.28 --rm -it --restart=Never --command --"
    bb nslookup demo
    bb wget -qO- http://demo:8888
    ```

### 7.7 Adding BusyBox to your containers
- If the container has a shell (e.g. built from `alpine`): `kubectl exec -it POD -- /bin/sh`.
- If it is built from **scratch**, there is no shell. Copy the **1 MiB BusyBox binary** in at build time using a multi-stage build:

```dockerfile
FROM golang:1.17-alpine AS build
WORKDIR /src/
COPY main.go go.* /src/
RUN CGO_ENABLED=0 go build -o /bin/demo

FROM scratch
COPY --from=build /bin/demo /bin/demo
COPY --from=busybox:1.28 /bin/busybox /bin/busybox
ENTRYPOINT ["/bin/demo"]
```
```bash
kubectl exec -it POD_NAME -- /bin/busybox sh
```
- `--from=busybox:1.28` can copy from **any** image (public library image or your own).

### 7.8 Installing programs on a container
```bash
kubectl run alpine --image alpine --rm -it --restart=Never /bin/sh
apk --update add emacs
```
- **Cautions**: temporary debug containers usually need **root** to install packages; **changes are lost on exit**; use only for one-off troubleshooting.

---

## 8. Contexts and Namespaces

### 8.1 Contexts
- With several clusters (local test, cloud production, remote staging), how does `kubectl` know which one you mean?
- A **context = cluster + user (authinfo) + namespace**; every command runs in the **current context**.
- Contexts are **bookmarks**: switch quickly to a particular cluster *and* namespace.

| Command | Purpose |
|:--|:--|
| `kubectl config get-contexts` | List contexts; current one marked `*` |
| `kubectl config use-context gke` | Switch context |
| `kubectl config set-context myapp --cluster=gke --namespace=myapp` | Create a context |
| `kubectl config current-context` | Show current context |
| `kubectl cluster-info` | Control plane + CoreDNS addresses of the current cluster |

```
CURRENT   NAME                 CLUSTER        AUTHINFO       NAMESPACE
          gke                  gke_test_us-w  gke_test_us    myapp
*         docker-for-desktop   docker-for-d   docker-for-d
```

### 8.2 kubeconfig files
- Contexts live in **`~/.kube/config`** by default (one or more files).
- Override the location with the **`KUBECONFIG`** environment variable (can be a colon-separated list, merged together).

### 8.3 kubectx and kubens
- Reduce typing when switching contexts/namespaces; install from GitHub or **krew** (the kubectl plugin manager).
- **kubectx**: lists contexts, switches, and `kubectx -` **toggles** back to the previous context.
- **kubens**: lists/switches namespaces; `kubens -` returns to the previous one.

```bash
kubectx docker-for-desktop
kubectx -                     # back to previous
kubens                        # list namespaces
kubens kube-system
kubens -
```

### 8.4 kube-ps1
- Adds the **current context and namespace to your shell prompt** so you can't forget where you are.
- Example: `(⎈ |docker-for-desktop:default)`.
- Setup:
    ```bash
    source "/usr/local/opt/kube-ps1/share/kube-ps1.sh"
    PS1='[$(kube_ps1)]$ '
    ```

---

## 9. Kubernetes Shells and Tools

### 9.1 kube-shell
- A **wrapper around kubectl** that pops up a **menu of possible completions** for each command — fancier auto-completion.
- Toolbar shows cluster, namespace, user, in-line help and exit keys.

### 9.2 Click
- A more sophisticated terminal experience: an **interactive kubectl that remembers the current object**.
- Problem with kubectl: to `describe` a Pod you must list Pods and **copy-paste** the unique name.
- **Click**: pick a resource by **number** (e.g. `1`); it becomes the current object and the next command operates on it by default. Supports **regex search**.

### 9.3 kubed-sh ("kube-dash")
- kube-shell and Click are local shells that *know about* Kubernetes; **kubed-sh runs, in some sense, on the cluster**.
- Pulls and runs the containers needed to execute **JavaScript, Ruby or Python** programs on your current cluster — e.g. run a local Ruby script as a Kubernetes Deployment.

### 9.4 Stern
- `kubectl logs` limits: you must find the unique Pod/container name (copy-paste); with `-f` the stream **stops when the container restarts**; you can follow only **one Pod**.
- **Stern** tails logs from **all Pods matching a regex** (e.g. `demo.*`) and keeps streaming across restarts.
- Multi-container Pods → each line is **prefixed with the container name**.
- `--since` limits output (e.g. last 10 minutes); supports **label selectors** and `--all-namespaces`.

---

## 10. Kubernetes IDEs

| Tool | Key points |
|:--|:--|
| **Lens** | GUI **dashboard + IDE** for Kubernetes. Advanced visualiser, easy jumping between contexts/namespaces, integrated terminal for kubectl. |
| **VS Code Kubernetes extension** | Dashboard inside VS Code; special **Azure/AKS** integration for creating clusters and deploying apps. |

**Building your own tools**
- `kubectl` + `jq` + Unix utilities (`cut`, `grep`, `xargs`) can script Kubernetes, but clever one-liners are hard to maintain.
- For real automation use a systems language — **Go** is the natural choice (Kubernetes itself is written in Go).
- **client-go** gives complete API access — anything kubectl can do, and more (including CRDs):

```go
podList, err := clientset.CoreV1().Pods("").List(metav1.ListOptions{})
if err != nil { log.Fatal(err) }
fmt.Println("There are", len(podList.Items), "pods in the cluster:")
for _, i := range podList.Items { fmt.Println(i.ObjectMeta.Name) }
```

- Official client libraries also exist for **Python, Java and .NET**; community libraries cover other languages.

---

## Quick revision

### Numbers to remember
| Fact | Value |
|:--|:--|
| Minimum control-plane nodes (HA) | **3** |
| Minimum worker nodes | **2** (3 better) |
| Max nodes (Kubernetes 1.22) | **5,000** |
| Max total Pods / containers | **150,000 / 300,000** |
| Max Pods per node | **100** |
| Control plane for ≤ 5 nodes | **2 CPUs + 2 GiB** |
| Example autoscale trigger | avg CPU **> 90% for 15 min** |
| kube-monkey schedule | schedule **8 a.m.** weekdays, kills **10 a.m.–4 p.m.** |
| BusyBox binary size / DNS-safe version | **≈1 MiB** / **1.28** |
| KCSP requirement | **3+ CKA** engineers |
| Certified Kubernetes update frequency | at least **annually** |

### Command cheat sheet
| Command | What it does |
|:--|:--|
| `kubectl drain NODE` | Gracefully evict Pods before node removal |
| `sonobuoy run / retrieve / results` | Run / fetch / inspect conformance tests |
| `kubectl completion -h` | Shell auto-completion setup |
| `kubectl explain RESOURCE.FIELD` | Documentation for any resource/field |
| `kubectl get ... -o wide/json/yaml` | More detail / JSON / YAML |
| `kubectl get pods -w` | Watch changes |
| `kubectl describe` | Detailed info incl. **Events** |
| `kubectl create / delete / edit` | Imperative (avoid in production) |
| `kubectl create ... --dry-run=client -o yaml` | Generate a manifest |
| `kubectl diff -f file.yaml` | Preview changes before apply |
| `kubectl apply -f file.yaml` | Declarative apply |
| `kubectl logs [-f] [--tail=N] [-c C] POD` | View/stream logs |
| `kubectl attach POD` | Attach terminal to container |
| `kubectl port-forward POD L:R` | Forward local port to container |
| `kubectl exec -it POD -- /bin/sh` | Shell inside container |
| `kubectl run ... --rm -it --restart=Never --command -- CMD` | One-off debug container |
| `kubectl config get-contexts / use-context / set-context` | Manage contexts |
| `kubectx` / `kubens` | Fast context/namespace switching |

### Tool match-up
| Tool | One-line purpose |
|:--|:--|
| **K3s** | Lightweight single-binary Kubernetes |
| **Sonobuoy** | Run conformance tests |
| **chaoskube / kube-monkey / PowerfulSeal** | Chaos testing |
| **jq** | Query/transform JSON |
| **kubespy** | Watch a resource change in real time (Pulumi) |
| **BusyBox** | Tiny image with Unix commands + shell |
| **kubectx / kubens** | Switch contexts/namespaces |
| **kube-ps1** | Context in shell prompt |
| **kube-shell** | Pop-up auto-completion wrapper |
| **Click** | Interactive kubectl with object state |
| **kubed-sh** | Run JS/Ruby/Python on the cluster |
| **Stern** | Tail logs from many Pods |
| **Lens** | GUI dashboard + IDE |
| **client-go** | Go library for full API access |

### Likely exam questions
1. Explain cluster sizing and scaling (capacity planning, smallest/biggest cluster, node sizes, instance groups, scaling down, autoscaling).
2. What is federation? When is it needed? Do you need multiple clusters?
3. Why drain nodes before removal? Role of PodDisruptionBudgets.
4. Explain conformance checking + CNCF certifications (Certified Kubernetes, CKA, KCSP).
5. Explain Sonobuoy with commands. What is audit logging?
6. What is chaos testing? Netflix Simian Army, "only production is production".
7. Compare chaoskube, kube-monkey and PowerfulSeal.
8. Explain kubectl tips: aliases, short flags, abbreviations, completion, `explain`, `-o wide`, JSON+jq, watch, describe.
9. Why avoid imperative commands? Alice and Bob; generate/export/diff.
10. Debugging containers: logs, attach, exec, port-forward, BusyBox, adding BusyBox to images.
11. What are contexts? kubeconfig, kubectx, kubens, kube-ps1.
12. Short notes: kube-shell, Click, kubed-sh, Stern, Lens, VS Code extension.

---

## Related notes

- [[DevOps MOC]] — subject index and reading order.
- [[unit 2]] — pods, deployments, probes, resources and scheduling basics.
- [[unit 4]] — running containers and managing pods (the next unit).
- [[DevOps CIE-2 PYQ]] — solved paper (Q3 capacity planning, Q4 advanced kubectl, Q5 imperative commands).
- [[unit 3-4 quick recap]] — 10-minute recap of this unit and unit 4.
- [[devops-quick-revision]] — condensed revision.
