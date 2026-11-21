---
tags: [devops, pyq, cie2]
---

# DevOps — CIE-2 Previous Year Paper (Solved)

**Paper pattern:** 5 questions × 10 marks = 50 marks · Answer all · CIE-2 syllabus: **Unit 2 + Unit 3**

| Q | Question | Marks | CO | Bloom |
|:--|:--|:--:|:--:|:--:|
| 1 | Write specifications of different probes for managing HTTP server container | 10 | 3 | 2 (Understand) |
| 2 | Write short notes on optimizing Deployments, Nodes and Pods | 10 | 2 | 2 (Understand) |
| 3 | With capacity planning analyse the cluster scaling and sizing for smallest, biggest and Federated clusters | 10 | 3 | 3 (Apply) |
| 4 | Illustrate the mastering of kubectl with minimum 3 advanced features | 10 | 2 | 3 (Apply) |
| 5 | Explain in detail about Imperative kubectl commands | 10 | 2 | 4 (Analyse) |

**Topic mapping:** Q1–Q2 → Unit 2 (objects, probes, resources, optimisation); Q3–Q5 → Unit 3 (cluster sizing/scaling + kubectl power tools).

---

## Q1. Specifications of different probes for managing an HTTP server container (10 M)

### Why probes exist
A container being **`Running`** only means its main process started — it does **not** mean the app can serve traffic. Probes are **kubelet-executed health checks** that turn "process is up" into "application is healthy and ready", and they are the backbone of self-healing and zero-downtime rollouts.

### The three probes

| Probe | Question it answers | On failure | Restarts? | Removes from Service? |
|:--|:--|:--|:--:|:--:|
| **`startupProbe`** | Has the app finished booting? | Kubelet kills and restarts the container | ✔ | ✔ (still not ready) |
| **`readinessProbe`** | Can the app accept user traffic *right now*? | Pod marked **NotReady**, removed from Service endpoints | ✘ | ✔ |
| **`livenessProbe`** | Is the app still alive / not deadlocked? | Container **killed** and restarted per `restartPolicy` | ✔ | ✔ (while restarting) |

**Execution order:** `startupProbe` runs first; only after it succeeds do liveness and readiness checks begin. If no startup probe is defined, liveness/readiness start after `initialDelaySeconds`. A **startup probe is the correct fix** for slow-booting apps (avoids the "liveness kills the app mid-boot" bug), replacing the old hack of a huge `initialDelaySeconds`.

### The four probe handlers (mechanisms)

| Handler | How it checks | Success condition | Best for |
|:--|:--|:--|:--|
| **`httpGet`** | GET request to `path:port` (scheme, headers optional) | HTTP status **200–399** | HTTP/REST servers |
| **`tcpSocket`** | Opens a TCP connection to the port | Connection succeeds | Databases, gRPC, non-HTTP TCP |
| **`exec`** | Runs a command **inside** the container | Exit code **`0`** | Custom checks, `grpc-health-probe`, CLI tools |
| **`grpc`** | gRPC health-checking protocol (`grpc.health.v1.Health`) | `SERVING` status (k8s ≥ 1.24+) | gRPC microservices |

### Timing / tuning fields

| Field | Default | Meaning |
|:--|:--:|:--|
| `initialDelaySeconds` | 0 | Wait before the **first** probe |
| `periodSeconds` | 10 | How often to probe |
| `timeoutSeconds` | 1 | Probe fails if no response in this time |
| `successThreshold` | 1 | Consecutive successes needed (must be 1 for liveness/startup) |
| `failureThreshold` | 3 | Consecutive failures before acting |
| `terminationGracePeriodSeconds` | 30 | Grace period before SIGKILL after liveness failure |

**Boot budget with a startup probe** = `failureThreshold × periodSeconds` (e.g. `30 × 5 s = 150 s`). Liveness must **not** use the same aggressive settings, otherwise a GC pause or DB blip causes a restart loop.

### Full specification — HTTP server container

```yaml
apiVersion: v1
kind: Pod
metadata:
  name: web
  labels:
    app: web
spec:
  containers:
    - name: web
      image: myorg/web:1.4.2
      ports:
        - containerPort: 8080

      # 1) STARTUP — slow JVM/Node boot, budget 5 min
      startupProbe:
        httpGet:
          path: /healthz
          port: 8080
        periodSeconds: 5
        failureThreshold: 60          # 60 x 5s = 300s to boot
        timeoutSeconds: 2

      # 2) LIVENESS — restart only on hard failure (deadlock)
      livenessProbe:
        httpGet:
          path: /healthz
          port: 8080
          httpHeaders:
            - name: X-Probe
              value: liveness
        initialDelaySeconds: 10
        periodSeconds: 20
        timeoutSeconds: 2
        failureThreshold: 3           # 3 x 20s = 60s before restart

      # 3) READINESS — traffic gating (dependencies, warm caches)
      readinessProbe:
        httpGet:
          path: /ready
          port: 8080
        periodSeconds: 5
        timeoutSeconds: 2
        failureThreshold: 2
        successThreshold: 1
```

**Endpoint contract**
- `/healthz` → **process-level** check only (no DB calls): `200 OK` while the process is alive. Calling a dependency here means a DB outage restarts every pod.
- `/ready` → **dependency-level** check (DB ping, cache warm, migrations done): `200 OK` only when it can serve traffic. This is what gates Service endpoints during rollouts.

**Alternatives**

```yaml
# TCP check (Postgres, Redis, gRPC without HTTP)
readinessProbe:
  tcpSocket:
    port: 5432
  periodSeconds: 10

# exec check with a health binary
livenessProbe:
  exec:
    command: ["/bin/grpc_health_probe", "-addr=:50051"]
  periodSeconds: 10

# native gRPC health probe
livenessProbe:
  grpc:
    port: 50051
  periodSeconds: 10
```

### How the probes behave in practice
1. **Pod start** → `startupProbe` passes → liveness/readiness begin.
2. **Readiness fails** (e.g. DB down) → pod goes `0/1 Ready`, is **removed from Service endpoints**, but is **not restarted**; traffic goes to healthy replicas.
3. **Liveness fails 3×** → kubelet restarts the container; on a CrashLoop the **backoff** grows 10 s → 20 s → 40 s … up to 5 min.
4. **Rollout**: new pods only receive traffic after readiness; old pods are terminated only when `maxUnavailable` allows → **zero downtime**, provided readiness plus `minReadySeconds` are set correctly.
5. **`kubectl describe pod`** shows `Liveness probe failed:` / `Readiness probe failed:` events — first thing to check when pods flap.

### Best practices (exam-worthy)
- **Never point readiness at a 3xx redirect** or a login page; cloud load balancers treat non-200 as unhealthy. Return strict `200`.
- Keep liveness **cheap and dependency-free**; put dependency checks in readiness.
- Set `timeoutSeconds` > p99 of the health endpoint; avoid probes that hammer the DB.
- Use `startupProbe` for slow starters instead of inflating `initialDelaySeconds`.
- Add `preStop: sleep 5` + adequate `terminationGracePeriodSeconds` so in-flight requests finish before the pod dies.
- Probes work with **all** workload types (Pod, Deployment, StatefulSet, DaemonSet) since they live in the container spec.

---

## Q2. Short notes on optimizing Deployments, Nodes and Pods (10 M)

### (a) Optimizing Deployments
- **Right-size replicas**: run the minimum that satisfies SLOs and let **HPA** add capacity; every idle replica is paid CPU/RAM.
- **Fast, safe rollouts**: `maxUnavailable: 0` + `maxSurge: 25%` keeps capacity during updates; pair with readiness probes and `minReadySeconds`.
- **Trim history**: `revisionHistoryLimit: 5` stops old ReplicaSets from keeping dead pods around.
- **Set `progressDeadlineSeconds`** so a broken rollout fails fast instead of hanging forever.
- **Batch/one-shot work as Jobs/CronJobs**, not always-on Deployments; scale **dev/staging to zero** outside working hours (CronJob or KEDA).
- **Pause non-critical deployments** during incidents (`kubectl rollout pause`) to reduce change risk.
- **Owner labels/annotations** (`team`, `env`, `cost-center`) for cost attribution and cleanup.
- **Avoid `latest` tags** — pin image digests so a rollout is reproducible and cache-friendly.

### (b) Optimizing Nodes
- **Fewer, larger nodes** reduce the relative overhead of the OS, kubelet, CNI and DaemonSets; target **10–100 pods per node** (hard default 110).
- **Right-size the node pool to the workload shape**: memory-heavy (Java, caches) vs CPU-heavy (batch, video) vs GPU pools; separate pools also isolate noisy neighbours.
- **Cheap capacity for interruption-tolerant work**: Spot/preemptible nodes save up to ~60–80%; steer critical pods away with `nodeAffinity`/taints (`cloud.google.com/gke-spot`, `karpenter.sh/capacity-type`).
- **Cluster Autoscaler / Karpenter**: scale node count to match pending pods; set sane min/max and scale-down delays. Karpenter provisions *right-sized* nodes just-in-time instead of fixed node groups.
- **Keep allocatable high**: don't over-reserve `kube-reserved`/`system-reserved`; monitor stranded (requested-but-unused) capacity.
- **Consolidate**: remove underutilised node pools, use **Descheduler** to rebalance pods after scale-up, and check for pods without requests (they distort packing).
- **Image hygiene on nodes**: pre-pull large images, set `imagePullPolicy: IfNotPresent`, prune unused images; node disk pressure evicts pods.
- **Storage**: use the cheapest tier that meets IOPS needs; delete orphaned PVs/snapshots.

### (c) Optimizing Pods
- **Always set requests and limits**:
    - `requests` drive scheduling and QoS; `limits` cap usage. CPU over-limit → **throttling**; memory over-limit → **OOMKill**.
    - Requests ≈ observed p95 usage; limits slightly above peak. Use **VPA recommendations / Goldilocks** instead of guessing.
- **QoS classes** decide eviction order under node pressure: **Guaranteed** (requests = limits, last to die) > **Burstable** > **BestEffort** (first to die). Critical pods should be Guaranteed.
- **Kill sidecar waste**: remove unused service-mesh/log sidecars, use native alternatives, and share a volume instead of duplicating data.
- **Faster starts**: small/distroless images, `imagePullPolicy: IfNotPresent`, cache warm-up in an **init container**, parallelise init work.
- **Graceful lifecycle**: `preStop` hook, correct `terminationGracePeriodSeconds`, `PodDisruptionBudget` for voluntary evictions.
- **Placement**: `topologySpreadConstraints` / `podAntiAffinity` for HA across AZs; `nodeAffinity` to keep workloads on the right hardware; `priorityClass` so critical pods preempt batch jobs.
- **Resource hygiene inside the pod**: tune JVM heap (`-XX:MaxRAMPercentage`) / Node `--max-old-space-size` to the container limit so the runtime doesn't get OOM-killed by the kernel before GC kicks in.

**One-line summary:** *Deployments → fewer, safer, observable replicas; Nodes → fewer, bigger, cheaper, right-shaped; Pods → accurate requests/limits, minimal images, graceful lifecycle.*

---

## Q3. Capacity planning: cluster scaling & sizing for smallest, biggest and Federated clusters (10 M)

### 1. What capacity planning means
Cluster cost — self-hosted **and** managed — is driven by the **number and size of nodes**. Too little capacity → workloads fail under load; too much → money is wasted. Capacity planning decides **how many nodes, of what size, with how much headroom**, before and during production.

**Starting estimate (the book's method)**
- Count the **traditional servers/VMs** the same applications use today.
- Plan for **roughly that many nodes + 1–2 spare** for redundancy.
- You may need **fewer** nodes, because Kubernetes **bin-packs containers** across the whole cluster and achieves higher utilisation than one-app-per-VM.
- Refine over time with real usage — there is no perfect formula.

**Two rules that always apply**
- Keep **20–30% headroom** for spikes, node failure and system DaemonSets (CNI, logging, metrics).
- Remember per-node overhead: kubelet, container runtime, kube-proxy and agents consume CPU/RAM before your apps.

### 2. The smallest cluster
| Item | Sizing |
|:--|:--|
| **Shape** | **Single node** running control plane + workloads (k3s, minikube, kind) |
| **Use** | Learning, experiments, small dev workloads |
| **Control plane (self-hosted)** | **Minimum 3 nodes** for HA — 1 has no resilience; 2 can disagree about leadership (split-brain); **3 forms a quorum** |
| **Workers** | **2 minimum** (survive one node failure and still run 2 replicas); **3 is better** |
| **Control-plane resources (≤ ~5 nodes)** | At least **2 CPUs + 2 GiB RAM** |
| **Managed services** | GKE/EKS/AKS provision an HA control plane for you |

- A single node has **no resilience**: failure of the **hardware**, the **API server** or the **kubelet** takes everything down.
- A cluster of only control-plane nodes works but is **not recommended** — workloads compete with the control plane for resources.
- **K3s** = lightweight distribution packaging all components in **one binary**; ideal for **isolated/resource-constrained** environments (edge, IoT, labs).

### 3. The biggest cluster
- **Kubernetes 1.22 officially supports up to 5,000 nodes.**
- **Why a limit?** Nodes must communicate; as the cluster grows, the number of **communication paths** and the **load on the database (etcd)** grow **exponentially**.
- Beyond 5,000 nodes Kubernetes may still work, but **responsiveness is not guaranteed** for production.
- Bigger cluster ⇒ **much heavier control plane**; self-hosted control-plane machines must be very powerful.

| Resource | Maximum |
|:--|:--:|
| Nodes | **5,000** |
| Total Pods | **150,000** |
| Total containers | **300,000** |
| Pods per node | **100** |

- **Practical sizing at the edge:** 150,000 pods ÷ 100 pods/node = **1,500 nodes** just by the per-node pod cap; CPU/memory requests usually bind first.
- **Operational must-haves:** multiple node pools, pod anti-affinity/topology spread across zones, HPA + Cluster Autoscaler, aggregated monitoring, and change control for cluster-wide objects.
- **Verdict:** use one cluster only while it fits; at 5,000 nodes/150,000 pods, **split into multiple clusters**.

### 4. Federated clusters
- **Federation = keeping two or more clusters synchronised, running identical workloads.**
- **Why federate:**
    - clusters on **different cloud providers** (avoid lock-in, cloud bursting);
    - **resilience** — the group survives one cluster's failure;
    - **geographical distribution** to reduce user latency;
    - very large scale beyond one cluster's limits.
- **How it is sized:** many **medium clusters** (a few hundred to a few thousand nodes each) + a federation/placement layer; each cluster keeps its own control plane and failure domain.
- **Mechanisms:** Federation v2 (KubeFed) with `FederatedDeployment`/`PropagationPolicy`, or modern GitOps/placement tools (Argo CD ApplicationSets, Karmada, Cluster API); global DNS/load balancing for traffic.
- **Costs/limits:** N control planes, duplicated add-ons and monitoring, cross-cluster networking/DNS, eventual consistency, policy drift, and cross-cluster data transfer.
- **When NOT to use it:** most users don't need federation. Even at large scale, **multiple independent clusters** split by team/application are easier to manage. Federation only when you must replicate workloads across clusters.

### 5. Nodes and instances (choosing the node size)
- **10 large nodes vs 100 small ones** — depends on provider and workload:
    - some providers **discount larger instances** → compute-heavy work is cheaper on few big nodes;
    - HA needs work spread across nodes, but **spare capacity is wasted money**;
    - ~10 nodes needed for HA but each runs 2 pods → use **very small instances**;
    - genuinely only 2 nodes → make them **large** and exploit better pricing.
- **Do not use the smallest instance sizes**: Kubernetes components and system pods need resources.
- For tens of nodes, mix **2–3 instance sizes** so memory-hungry Pods land on big nodes and small Pods on small nodes → maximum scheduling freedom.
- **Heterogeneous nodes:** GPU nodes (declared via resource limits) for ML/data work; **Windows nodes** for Windows binaries (containers share the host kernel, so a Linux node cannot run them). Most workloads are Linux → mostly Linux nodes + a few special types.
- **Bare metal:** join existing physical servers into the cluster to reuse CapEx hardware instead of writing it off.

### 6. Scaling the cluster
- **Instance groups**: a set of nodes of one instance type; scale by changing **min/max size**, **instance type**, or both.

| Platform | Term |
|:--|:--|
| kops (self-hosted) | **Instance group** |
| GKE / managed services | **Node pool** |
| EKS (`eksctl`) | **Nodegroup** |

**Scale down safely — drain first**
```bash
kubectl drain node-1 --ignore-daemonsets --delete-emptydir-data
```
- Draining **gracefully evicts** Pods and reschedules them; only then terminate the node.
- **PodDisruptionBudgets** (`minAvailable` / `maxUnavailable`) limit how many Pods can be unavailable; if a drain would violate a PDB, **the drain blocks**.
- Never just power off a node — Pods die immediately without cleanup.

**Autoscaling**
| Mode | Behaviour |
|:--|:--|
| **Fixed min/max** (ASG) | Self-healing; replace failures, keep group size |
| **Scheduled** | Grow/shrink at known times (time-of-day demand) |
| **Dynamic** | Metric-driven — e.g. **average CPU > 90% for 15 min** → add instances; scale down when demand falls |

- The **Cluster Autoscaler** add-on (kops; managed services have equivalents) adds nodes when Pods are pending and removes underused nodes.
- **Don't enable autoscaling by default** — start manual, monitor, and automate only if demand is genuinely variable.

### 7. Comparison summary

| Dimension | Smallest | Biggest (single) | Federated |
|:--|:--|:--|:--|
| Nodes | **1** | up to **5,000** | many clusters × 100–1,000s |
| Control plane | 1, co-located (or managed) | **3–5 HA**, powerful machines | one per cluster |
| Resilience | none | multi-AZ | multi-cluster/multi-region |
| Scaling unit | pod | pod + node | cluster |
| Main constraint | node resources | etcd/API load, 150k pods | networking, ops overhead |
| Cost efficiency | cheapest to start, no SLA | high density, high risk | lower density, high resilience |
| Typical use | dev, learning, edge | large single platform | global/enterprise, cloud bursting |

**Conclusion:** estimate from existing server count (+1–2 spare), enforce **3 control-plane + 2–3 workers** for resilience, stay below **5,000 nodes / 150,000 pods**, pick cost-effective node sizes, **drain before removing nodes (respecting PDBs)**, and autoscale only for variable demand. Federation is the last resort for workloads that genuinely must span clusters.

---

## Q4. Mastering `kubectl` — advanced features (10 M)

`kubectl` is the primary interface to Kubernetes — not just `get/apply/delete`, but a query tool, a manifest generator, a diff engine, a debugger and a context switcher. Any **three** of the features below make a complete answer; the rest are bonus.

### Feature 1 — Aliases, short flags and resource abbreviations
Cuts keystrokes in daily use.

```bash
alias k=kubectl
alias kg=kubectl get
alias ksys="kubectl --namespace=kube-system"

kubectl get pods -n kube-system -w      # -n namespace, -w watch
kubectl logs POD -f -c CONTAINER        # -f follow, -c container
kubectl get pods -l app=demo            # -l selector

kubectl get po,deploy,svc,ns            # po, deploy, svc, ns, no, cm, sa, ds, pv
```
- Aliases live in `.bashrc`/`.zshrc`; Ahmet Alp Balkan's alias system generates **~800** of them.
- Enabling **shell completion** (`kubectl completion -h`) gives Tab-completion of commands, flags **and live object names**.

### Feature 2 — `kubectl explain`, `-o wide`, JSON, jq and JSONPath
Turns kubectl into a self-documenting query engine.

```bash
kubectl -h                       # all commands
kubectl get -h                   # options for one command
kubectl explain deploy.spec.template.spec.containers.livenessProbe.exec

kubectl get pods -o wide         # adds Pod IP + node
kubectl get nodes -o wide        # adds internal IP, version

kubectl get pods -n kube-system -o json | jq '.items[].metadata.name'
kubectl get pods -o json --all-namespaces | jq '.items |
  group_by(.spec.nodeName) |
  map({"node": .[0].spec.nodeName, "count": length}) |
  sort_by(.count) | reverse'

kubectl get pods -o=jsonpath='{.items[0].metadata.name}'
```
- `explain` works **offline** and drills into **any field to any depth** — no browser needed.
- `-o json` is huge (~5,000 lines for `kube-system`), so **jq** is essential; JSONPath is built in for quick one-liners.

### Feature 3 — Watching and describing objects
```bash
kubectl get pods -w
# ContainerCreating -> Running updates appear live

kubectl describe pod POD_NAME
```
- `--watch` replaces repeated `get` calls and shows status transitions as they happen.
- `describe` prints full configuration plus the **Events** section — the **first thing to check** for scheduling, image-pull, probe or crash problems.
- **kubespy** extends this to any resource, showing its state changing in real time.

### Feature 4 — Generating, exporting and diffing manifests (safe declarative workflow)
```bash
# Generate boilerplate YAML without creating anything
kubectl create deployment demo --image=cloudnatived/demo:hello \
  --dry-run=client -o yaml > deployment.yaml

# Export an existing (imperatively created) resource
kubectl get deployment newdemo -o yaml > deployment.yaml
# ...then delete the runtime "status:" section before committing

# See exactly what would change before applying
kubectl diff -f deployment.yaml
# - replicas: 10
# + replicas: 5
```
- `--dry-run=client -o yaml` = **imperative command, declarative output** — the fastest way to start a manifest.
- Exporting is the **migration path** from imperative to declarative management.
- `kubectl diff` detects both your intended change and **drift** caused by someone editing the live object.

### Feature 5 — Working with running containers (debugging power tools)
```bash
kubectl logs POD --tail=20 -f -c CONTAINER     # stdout/stderr, live
kubectl logs POD --previous                    # logs from the crashed instance
kubectl attach POD                             # attach terminal to container output
kubectl port-forward POD 9999:8888             # local:container port
kubectl exec -it POD -- /bin/sh                # shell inside container
kubectl exec -it -c container2 POD -- /bin/sh

# One-off debug pod: DNS + HTTP checks from inside the cluster
kubectl run nslookup --image=busybox:1.28 --rm -it --restart=Never \
  --command -- nslookup demo
kubectl run wget --image=busybox:1.28 --rm -it --restart=Never \
  --command -- wget -qO- http://demo:8888
```
- `--rm` cleans up, `-it` is interactive, `--restart=Never` runs once, `--command --` overrides the entrypoint.
- **BusyBox 1.28** is used because later tags have a **DNS lookup bug** in Kubernetes.
- For **scratch images** with no shell, copy the **1 MiB BusyBox binary** in a multi-stage build (`COPY --from=busybox:1.28 /bin/busybox /bin/busybox`) and run `/bin/busybox sh`.
- **Stern** tails logs from **all Pods matching a regex/label**, survives restarts, and prefixes multi-container output.

### Feature 6 — Contexts and namespace switching
```bash
kubectl config get-contexts
kubectl config use-context gke
kubectl config set-context myapp --cluster=gke --namespace=myapp
kubectl config current-context
kubectl cluster-info

kubectx docker-for-desktop     # switch context
kubectx -                      # toggle back
kubens kube-system             # switch namespace
```
- A **context = cluster + user + namespace**, stored in `~/.kube/config` (or `$KUBECONFIG`); every command runs in the current context.
- **kubectx/kubens** cut typing; **kube-ps1** puts `(⎈ |context:namespace)` in the prompt so you never run a command against the wrong cluster.

### Bonus — shells, GUIs and scripting
- **kube-shell** (pop-up completion menu), **Click** (interactive kubectl that remembers the current object), **kubed-sh** (run JS/Ruby/Python on the cluster).
- **Lens** (GUI dashboard + IDE) and the **VS Code Kubernetes extension** (with AKS integration).
- For production automation, use **client-go** (Go) or the official Python/Java/.NET libraries instead of fragile shell + jq one-liners.

**Conclusion:** kubectl's advanced power is **precise querying** (explain/jq/jsonpath/watch), **safe declarative changes** (dry-run/export/diff) and **in-cluster debugging** (logs/exec/port-forward/BusyBox) — plus fast context switching so you always act on the right cluster.

---

## Q5. Imperative `kubectl` commands in detail (10 M)

### 1. What "imperative" means
- **Imperative:** you tell Kubernetes **what to do right now** (`kubectl run`, `kubectl create`, `kubectl scale`, `kubectl set image`). The cluster executes your command; the intent is not stored in Git.
- **Declarative:** you write the **desired state** in YAML and `kubectl apply` it; the reconciler converges to it.
- Imperative commands are **fast for learning, demos, troubleshooting and one-off tasks**; production state should live in Git (declarative).
- **Bridge between the two:** `--dry-run=client -o yaml` generates the exact YAML for an imperative command.

```bash
kubectl create deployment web --image=nginx --replicas=2 \
  --dry-run=client -o yaml > web.yaml
```

### 2. Object creation
```bash
# Pods / Deployments
kubectl run nginx --image=nginx:1.25 --port=80
kubectl create deployment web --image=nginx:1.25 --replicas=3

# Namespaces
kubectl create namespace staging

# Services
kubectl expose deployment web --port=80 --target-port=80 --type=ClusterIP
kubectl expose deployment web --port=443 --name=web-tls --type=LoadBalancer

# ConfigMap / Secret
kubectl create configmap app-config --from-literal=MODE=prod --from-file=app.conf
kubectl create secret generic db-creds \
  --from-literal=username=admin --from-literal=password='S3cret!'

# Jobs / CronJobs
kubectl create job db-migrate --image=myorg/migrate:1.2 -- ./migrate.sh
kubectl create cronjob backup --image=myorg/backup:1.0 --schedule="0 2 * * *" -- /backup.sh

# Ingress / HPA / quota
kubectl create ingress web --rule="example.com/*=web:80" --class=nginx
kubectl create quota dev-quota --hard=cpu=4,memory=8Gi,pods=20
```

### 3. Inspecting and querying
```bash
kubectl get pods -o wide
kubectl get all -n staging
kubectl describe pod web-7d9f-abcde
kubectl logs web-7d9f-abcde -c web --tail=100 -f
kubectl logs web-7d9f-abcde --previous        # logs from the crashed instance
kubectl exec -it web-7d9f-abcde -- /bin/sh
kubectl top pods --containers
kubectl explain pod.spec.containers.resources
kubectl api-resources
kubectl get events --sort-by=.lastTimestamp
```

### 4. Modifying running workloads
```bash
kubectl scale deployment web --replicas=5
kubectl set image deployment/web web=nginx:1.26
kubectl set env deployment/web LOG_LEVEL=debug
kubectl set resources deployment/web --limits=cpu=500m,memory=512Mi \
                                     --requests=cpu=250m,memory=256Mi
kubectl label pod web-7d9f-abcde tier=frontend
kubectl annotate deployment web owner="team-web"
kubectl edit deployment web            # opens $EDITOR, applies on save
kubectl patch deployment web -p '{"spec":{"replicas":4}}'
kubectl autoscale deployment web --min=2 --max=10 --cpu-percent=70
```
> Note: `kubectl set image` / `edit` / `patch` are **imperative mutations** — if GitOps is in use, Argo CD/Flux will revert them (drift correction). Use them for hotfixes or learning.

### 5. Lifecycle and rollout management
```bash
kubectl rollout status deployment/web
kubectl rollout history deployment/web
kubectl rollout undo deployment/web --to-revision=2
kubectl rollout restart deployment/web
kubectl rollout pause deployment/web
kubectl rollout resume deployment/web
```

### 6. Deletion
```bash
kubectl delete pod web-7d9f-abcde
kubectl delete deployment web
kubectl delete -f web.yaml
kubectl delete pods -l app=web
kubectl delete namespace staging        # deletes EVERYTHING inside — irreversible
```

### 7. Imperative cluster/admin operations
```bash
kubectl cordon node-1
kubectl drain node-1 --ignore-daemonsets --delete-emptydir-data
kubectl uncordon node-1
kubectl taint nodes node-1 dedicated=gpu:NoSchedule
kubectl label node node-1 disk=ssd
kubectl certificate approve csr-abc123
kubectl config use-context prod-eu
kubectl port-forward svc/web 8080:80
kubectl cp web-7d9f-abcde:/var/log/app.log ./app.log
```

### 8. Imperative vs declarative — when to use which

| Aspect | Imperative | Declarative (`apply -f`) |
|:--|:--|:--|
| Intent | "Do this now" | "This is the desired state" |
| Source of truth | Shell history / cluster | Git repository |
| Idempotent | No (re-running may error/duplicate) | Yes (apply is idempotent) |
| Review/audit | Hard | PR review + diff + history |
| Drift handling | None | Reconciles automatically |
| Best for | Learning, debugging, one-offs, CI dry-runs | Production workloads, GitOps |
| Example | `kubectl run nginx --image=nginx` | `kubectl apply -f nginx.yaml` |

### 9. Limitations and cautions
- Imperative changes are **invisible to Git** → unauditable, unreproducible, and reverted by GitOps agents.
- `kubectl run` creates a **bare Pod** in current versions (older versions created a Deployment) — it is **not** managed by a controller, so use it only for one-off/debug containers (`--restart=Never`).
- No validation against policy unless `--dry-run=server` is used; typos can create half-configured objects.
- Deleting objects imperatively can leave **orphans** (Services without Deployments, PVCs, load balancers) that cost money.
- **Recommended workflow:** prototype imperatively with `--dry-run=client -o yaml`, commit the generated YAML, then manage everything with `kubectl apply`/GitOps.

### 10. The classic failure — Alice and Bob (why imperative is dangerous)
1. **Alice** is on-call; load spikes and she runs `kubectl scale deploy/web --replicas=10` (Git still says **5**).
2. Days later **Bob** edits the YAML in Git to use a new image — the file still says `replicas: 5`.
3. Bob applies → replicas are **cut in half** → overload/outage.

Lessons: Alice forgot to write the change back to version control (easy under incident stress); Bob should have run **`kubectl diff`** first; the real fix is to make **every** change through version-controlled YAML (GitOps).

### 11. Safer helpers around imperative commands
```bash
# 1) GENERATE boilerplate without creating anything
kubectl create deployment demo --image=cloudnatived/demo:hello \
  --dry-run=client -o yaml > deployment.yaml

# 2) EXPORT an existing live object (remove the status: block before committing)
kubectl get deployment demo -o yaml > deployment.yaml

# 3) DIFF before applying — catches your change and drift from imperative edits
kubectl diff -f deployment.yaml

# 4) APPLY declaratively
kubectl apply -f deployment.yaml
```

| Helper | Purpose |
|:--|:--|
| `--dry-run=client -o yaml` | Turn an imperative command into a manifest (imperative → declarative bridge) |
| `kubectl get -o yaml` | Export live resources during migration |
| `kubectl diff` | Preview changes and detect drift **before** apply |

**Rule of thumb:** use imperative commands to *learn, debug and generate YAML*; use **declarative manifests in Git** to *own production state*.


---

## Exam strategy (quick tips)

- **Q1/Q3 (Unit 3):** draw the **probe lifecycle timeline** or the **cluster sizing table** — diagrams earn marks fast. Quote concrete numbers (**100 pods/node, 5,000 nodes, 150,000 pods, 300,000 containers, 3 control-plane nodes, 2–3 workers**).
- **Q2/Q4/Q5:** 3–5 crisp bullets per sub-heading + one short YAML/command block beats long prose.
- Label answers with the question's unit if you have space; examiners map marks to syllabus units.
- Keep mental skeletons: **3 probes / 3 scaling modes (fixed, scheduled, dynamic) / 3 chaos tools / 3 units of a kubectl command (verb, resource, flags)**.

---

## Related notes

- [[unit 2]] — Deployments, Pods, probes, resources, namespaces, cost optimisation (Q1/Q2 theory).
- [[unit 3]] — cluster sizing/scaling, conformance, chaos testing and kubectl power tools (Q3/Q4/Q5 theory).
- [[unit 4]] — running containers and managing Pods (extension reading).
- [[DevOps MOC]] — subject index.
- [[devops-quick-revision]] — condensed one-pager before the exam.
