---
tags: [devops, quick-revision]
---

# Unit 3 + 4 — Quick Recap

> 10-minute skim before the exam. Full detail: [[unit 3]] · [[unit 4]] · solved paper: [[DevOps CIE-2 PYQ]]

---

## UNIT 3 — OPERATING CLUSTERS

### Sizing & scaling
- Cost = **number × size of nodes**. Too small → failures; too large → waste.
- Start from **existing VMs** + **1–2 spare**; Kubernetes bin-packs, so often fewer nodes are needed.
- **Smallest cluster** = 1 node (no resilience: hardware / API server / kubelet failure kills everything).
- **Self-hosted HA** = **3 control-plane nodes** (1 = no HA, 2 = split-brain) + **2 workers** (3 better). Managed services give you the HA control plane.
- **K3s** = lightweight single-binary distribution for edge/resource-constrained setups.
- **Biggest cluster (v1.22): 5,000 nodes · 150,000 Pods · 300,000 containers · 100 Pods/node.** Why limit? Node-to-node paths and DB (etcd) load grow *exponentially*.
- **Beyond that → multiple clusters.** Usually **1 prod + 1 staging**; separate clusters only for **compliance / PHI / data-residency** isolation; otherwise use **namespaces**.
- **Federation** = 2+ clusters kept in sync running identical workloads. Uses: multi-cloud, resilience, geo-latency. **Most users don't need it** — independent medium clusters are easier.
- Node size: bigger is often cheaper, but HA needs spread; small control plane (≤5 nodes) = **2 CPU + 2 GiB**; mix 2–3 instance sizes; GPU nodes for ML, **Windows nodes for Windows binaries** (containers share the host kernel); join **bare metal** to reuse CapEx.
- **Instance group** (kops) = **node pool** (GKE) = **nodegroup** (EKS/eksctl).
- **Scale down = drain first** (`kubectl drain NODE`), never power off. **PDBs** (`minAvailable`/`maxUnavailable`) block a drain that would break availability.
- **Autoscaling modes:** fixed min/max (self-healing), scheduled (time of day), dynamic (e.g. **avg CPU > 90% for 15 min**). **Cluster Autoscaler** add-on. Don't enable it by default — start manual.

### Conformance & audit
- **Conformance** = cluster passes the Kubernetes test suite → same API/objects/behaviour everywhere.
- **Certified Kubernetes** mark: mandatory for products with "Kubernetes" in the name; vendor **self-certifies with Sonobuoy**; must update **at least annually**.
- **CKA** = individual admin exam (practical). **KCSP** = vendor needs CNCF membership, enterprise support, contributions, **3+ CKA engineers**.
- **Sonobuoy:** `sonobuoy run` → `kubectl get pods -n sonobuoy` → `sonobuoy retrieve` → `sonobuoy results $f`; full suite ≈ **1 hour**, `--mode quick` for a smoke test.
- **Audit logging** = who did what, when, to which API resource, and the response → ship to central logging.

### Chaos testing
- Automated random failure injection (Netflix **Simian Army**: Chaos Monkey = kill servers, Latency Monkey = delays, Security Monkey = vulns, Chaos Gorilla = drop an AZ).
- **"Only production is production"** — staging can't clone scale or real traffic; experiments must be **continuous**.
- **chaoskube** = random Pods, **dry-run by default**, filters by labels/annotations/namespaces/blackout dates.
- **kube-monkey** = **opt-in annotations** (`mtbf`, `kill-mode`, `kill-value`), schedules at **8 a.m.**, kills **10 a.m.–4 p.m.** weekdays.
- **PowerfulSeal** = **interactive or autonomous (policy files)**; most flexible, can kill nodes/namespaces/Deployments.

---

## UNIT 3 — POWER TOOLS

### kubectl essentials
- Aliases (`alias k=kubectl`), short flags: `-n`, `-l`, `-w`, `-f`, `-c`.
- Abbreviations: `po, deploy, svc, ns, no, cm, sa, ds, pv`.
- `kubectl completion -h` for Tab-completion of commands, flags **and object names**.
- `kubectl -h` / `kubectl get -h`; **`kubectl explain deploy.spec...`** = offline docs to any depth.
- `-o wide` adds IP/node; `-o json | jq` filters; **JSONPath** built in; `-w` watches; **`describe` → check Events first**.
- Imperative = `run`, `create`, `delete`, `edit`; **avoid in production** — no single source of truth, drift, next apply overwrites.
- **Alice & Bob:** Alice scales 5→10 imperatively; Bob applies Git (still 5) → outage. Fix: version-controlled YAML + **`kubectl diff`** first.
- Generate: `kubectl create ... --dry-run=client -o yaml > f.yaml`; export: `kubectl get ... -o yaml` (drop `status:`); `kubectl diff -f f.yaml` shows `-` current / `+` new.

### Containers & contexts
- Logs = **stdout/stderr**; `--tail`, `-f`, `-c`, `--previous`. API server logs show RBAC errors.
- `attach` = see container output; `port-forward POD L:R`; `exec -it POD -- /bin/sh` (first container by default).
- One-off debug pod: `kubectl run x --image=busybox:1.28 --rm -it --restart=Never --command -- CMD` (`--rm` cleanup, `-it` TTY, `Never` run once, `--command --` override entrypoint).
- **BusyBox 1.28** — later tags have a DNS bug. No shell (scratch)? copy the **1 MiB** `busybox` binary via multi-stage `COPY --from=busybox:1.28`.
- **Context = cluster + user + namespace**, stored in `~/.kube/config` (`$KUBECONFIG`); `config get/use/set-context`, `current-context`, `cluster-info`.
- **kubectx / kubens** switch fast (`kubectx -` toggles); **kube-ps1** puts context in the prompt.
- Tools: **kube-shell** (completion menu), **Click** (remembers current object), **kubed-sh** (JS/Ruby/Python on the cluster), **Stern** (multi-Pod regex log tail), **kubespy** (live resource state), **Lens** / **VS Code** extension; **client-go** for real automation (also Python/Java/.NET).

---

## UNIT 4 — RUNNING CONTAINERS

- **Pod = smallest deployable / scheduling unit**; containers in a Pod always land on the **same node**, share lifecycle and volumes.
- Container = **isolated process in Linux namespaces**, not a VM; **one main process per container**.
- **"Will these containers work on different machines?"** No → same Pod; Yes → separate Pods.
- Runtimes: Docker runtime deprecated (Dec 2020) for **CRI**; Docker-built images still run because they are **OCI**-compliant.
- Image = `registry / namespace / repo : tag`; only repo required. `latest` = last **untagged** push, not newest → **never in prod**. **Digest** (`@sha256:…`) is immutable/deterministic.
- `imagePullPolicy`: **IfNotPresent** (default) / Always / Never. Pod isn't updating? Check this.
- `env` = strings only, **32 KiB** max, overrides image `ENV`; use **ConfigMap/Secret** for more.
- Security: **`runAsUser: 1000`**, **`runAsNonRoot: true`** (else `CreateContainerConfigError`), **`readOnlyRootFilesystem: true`**, **`allowPrivilegeEscalation: false`**, **`capabilities.drop: ["all"]` + add only needed** (dropped caps **can't be regained**).
- Pods run as the namespace's **default service account** unless `serviceAccountName` is set.
- **emptyDir** = node storage, dies with the Pod (scratch/cache/sharing); **PV/PVC** = persists, dynamic provisioning; K8s does **not** lock shared files. Prefer managed DBs over in-cluster.
- **restartPolicy**: Always (default) / OnFailure / Never (Jobs, debug).
- **imagePullSecrets** (or attach to ServiceAccount) for private registries.
- **initContainers** run **before** app containers, **no probes**, must **exit successfully**, must be **idempotent** (pre-flight, bootstrap, fetch secrets).

---

## UNIT 4 — MANAGING PODS

- **Labels** = key/value identity; useless without a **selector** (Service supports **equality only**); `kubectl get pods -l app=demo`, `--show-labels`.
- Set-based selectors: `In`, `NotIn` via `matchExpressions`; `!=` also matches Pods **without** the label. **Labels** identify, **annotations** inform tools.
- Label limits: **63 chars** (prefix 253), start alphanumeric, only alphanumerics/`-`/`_`/`.`.
- **Node affinity:** hard `required...` (blocks scheduling) vs soft `preferred...` (**weight 1–100**); rules apply **during scheduling only** — Pod is not moved later.
- **Pod affinity** = attract (app + cache); **podAntiAffinity** = repel (spread replicas). Hard may leave Pod **Pending** / reduce replicas; **soft anti-affinity is recommended** for spreading.
- **Taints** are on **nodes**, **tolerations** on **Pods**: `kubectl taint nodes N dedicated=true:NoSchedule` (remove with trailing `-`). Node problems (`node.kubernetes.io/unreachable`) are auto-tainted.
- Controllers:
    - **Deployment** → ReplicaSets, rolling updates (most stateless apps)
    - **DaemonSet** → one Pod per node (log/metric agents)
    - **StatefulSet** → numbered, ordered, stable DNS + **headless Service** (`clusterIP: None`) + `volumeClaimTemplate`
    - **Job** → `completions` + `parallelism`, `ttlSecondsAfterFinished`; **CronJob** → `schedule` + `jobTemplate`
    - **HPA** → replicas from CPU/%-of-request (needs Metrics Server), min/max; can be scheduled via CronJob patching `minReplicas`
    - **Operator + CRD** → custom controller for complex apps (OperatorHub.io)
- **Ingress** = external → Service routing via **Ingress controller**; rules/fanout by path; TLS via Secret (`kubernetes.io/tls`) + **cert-manager** for Let's Encrypt.
- **Service mesh** (Istio, Linkerd, Consul Connect, NGINX SM) = mTLS, retries, rate limiting, metrics/tracing, traffic splitting; uses the **sidecar** pattern. Start with Service + Ingress, add mesh when needed.

---

## Numbers to blurt out
**3** control-plane · **2–3** workers · **5,000 / 150,000 / 300,000 / 100** · **2 CPU + 2 GiB** control plane · **90% CPU for 15 min** autoscale example · **8 a.m. / 10–4** kube-monkey · **1 MiB / 1.28** BusyBox · **3+ CKA** for KCSP · **63**-char labels · **32 KiB** env · **UID 1000+** · **weight 1–100**.

## If you only remember 5 things
1. Drain before removing a node, and respect **PDBs**.
2. **5,000 nodes/150k Pods** is the ceiling → then multiple/federated clusters.
3. Imperative is for learning/debugging; **Git + `kubectl diff` + apply** owns production.
4. Containers: **non-root, no privilege escalation, drop ALL capabilities, read-only FS**.
5. Pods: **labels select, affinities place, taints repel, controllers keep them alive**.

---

## Related notes

- [[unit 3]] — full operating clusters + kubectl power tools notes.
- [[unit 4]] — full running containers + managing Pods notes.
- [[DevOps CIE-2 PYQ]] — solved paper with model answers.
- [[DevOps MOC]] — subject index.
