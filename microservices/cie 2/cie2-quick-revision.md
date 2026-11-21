---
tags: [microservices, cie-2, quick-revision]
---

# Microservices - CIE 2 Quick Revision

One-page-per-topic revision sheet. Full detail is in [[cie2notes]]; practice questions are in [[pyq cie 2]].

| Part | Unit | Topics |
|---|---|---|
| 1 | Unit 3 | Infrastructure pipeline, IaC environment, AWS, IaC pipeline |
| 2 | Unit 3 | Microservices infrastructure: network, EKS, Argo CD, modules |
| 3 | Unit 4 | Developer workspace, Docker, Cassandra, local Kubernetes |

---

# UNIT 3 - INFRASTRUCTURE PIPELINE

## 1. The core chain (remember this order)

DevOps culture -> immutable infrastructure -> cloud makes re-creation cheap -> IaC defines the target state -> Terraform applies it -> CI/CD pipeline makes changes safe and repeatable -> GitHub Actions runs the pipeline -> AWS hosts everything.

If you can explain this chain, you can answer most Unit 3 questions.

## 2. Definitions

| Term | One-line answer |
|---|---|
| DevOps | Pursuit of improvements in how software is developed, released and supported, across organization, culture, process and tooling |
| Immutable infrastructure | Components are never changed after creation; they are destroyed and re-created |
| Configuration drift | Gradual divergence of server states caused by inconsistent manual changes |
| IaC | All infrastructure changes are represented as machine-readable files that define the target state |
| Terraform | HashiCorp's declarative IaC tool that tracks state and applies changes |
| HCL | HashiCorp Configuration Language, the JSON-like format of Terraform files |
| Pipeline | A tool that defines and manages the steps of a CI/CD process |
| GitHub Actions | GitHub's built-in CI/CD tool; workflows are YAML |
| Workflow | The set of steps run when a pipeline is triggered |
| Git tag | A label on a point in Git history; used here as the pipeline trigger |
| IAM | AWS service for users, groups and permissions |
| ARN | Unique identifier of an AWS resource |
| S3 | AWS object storage; used as the Terraform state backend |
| Sandbox | Throwaway test environment for trying IaC modules and pipelines |
| VPC | Parent object of a virtual network in AWS |
| Subnet | A smaller network inside a VPC |
| CIDR | Notation for an IP range, for example `10.0.0.0/16` |
| Availability zone | A separate physical data center in AWS |
| Internet gateway | Connects the VPC to the public internet |
| NAT gateway | Gives private subnets outbound internet access; needs an Elastic IP |
| Security group | Rules controlling ingress and egress traffic |
| EKS | AWS managed Kubernetes service |
| Pod | Deployable unit holding one or more containers |
| kubectl | CLI for talking to a Kubernetes cluster |
| kubeconfig | File with cluster connection details |
| GitOps | Git is the source of truth for the system's target state |
| Argo CD | Declarative GitOps tool that deploys Kubernetes apps from Git |
| Helm / chart | Kubernetes package manager / a packaged deployment |

## 3. Immutable infrastructure

- Cannot change after creation. Update = destroy + re-create, including dependents.
- Example: a load balancer's routes are fixed. To change them, replace the load balancer.
- Solves the manual-work and drift problem of traditional servers.
- Trade-off: re-creation costs money and effort. The cloud removes most of that cost.
- Benefit: predictable, reproducible, testable, no drift.

**Immutable vs mutable**

| Basis | Immutable | Traditional |
|---|---|---|
| Change | Destroy and re-create | Patch and edit in place |
| Predictability | High | Low, drifts over time |
| Effort | Automated, low | Manual, high |
| Self-service | Supported | Difficult |

## 4. IaC and Terraform

- IaC constraint: every infrastructure change must exist as code.
- Reapply the code to re-create an environment.
- Tool needs: define changes as files, and apply them to the target.

Why Terraform:

1. Declarative - declare the end state; Terraform plans the steps.
2. Supports immutability - destroys and re-creates on change.
3. Tracks state - knows the current state and plans the difference.

| Declarative | Imperative |
|---|---|
| Declare the end state | Give step-by-step instructions |
| Tool works out the steps | Operator scripts every step |

Terraform four concepts:

| Concept | Meaning |
|---|---|
| Backend | Where the state file lives (S3 here) |
| Resource | An object whose state is declared (VPC, subnet, cluster) |
| Provider | A library for a platform (AWS, Kubernetes, Helm) |
| Module | Reusable block of HCL, like a function |

## 5. CI/CD

- IaC makes changes predictable, not safe. A bad change can still cause an outage.
- Manual double-checking slows change and finds problems late.
- Pipeline: integrate, test and deliver small changes continuously.
- **Key rule:** all changes go through the pipeline, never through the console or CLI.

Why GitHub Actions: same tool as the code host; simple for the book.
Limitation: less feature-rich than Jenkins or GitLab.

## 6. Key decisions

| Decision | Reason |
|---|---|
| Immutable infrastructure | Predictable, reproducible, no drift |
| Build in the cloud | Virtual components are cheap to re-create |
| Adopt IaC | Infrastructure behaves like software |
| Use Terraform | Declarative, immutable, state tracking |
| All changes via CI/CD | Safe and repeatable |
| Use GitHub Actions | One tool for code and pipeline |
| Use GitHub | Popular and used throughout the book |
| Host in AWS | Largest user base; one provider keeps it simple |
| One repo per environment | Team independence; code and pipeline together |
| Managed Kubernetes (EKS) | Hides control-plane complexity |
| GitOps deployment (Argo CD) | Declarative, continuous deployment for the release team |

## 7. AWS setup (6 steps)

1. Create IAM user `ops-account` with **programmatic access**; attach `IAMFullAccess`; save the access key and secret.
2. Run `aws configure` (key, secret, region, `json`). Test with `aws iam list-users`.
3. Create group `Ops-Accounts`, add the user, attach the policy set.
4. Create custom `EKS-Management` policy from JSON and attach it. Note the ARN.
5. Create the private S3 bucket for Terraform state.
6. From here, change nothing by hand. Everything goes through code and the pipeline.

Policies on the group: `IAMFullAccess`, `AmazonEC2FullAccess`, `AmazonEC2ContainerRegistryFullAccess`, `AmazonEKSClusterPolicy`, `AmazonEKSServicePolicy`, `AmazonVPCFullAccess`, `AmazonRoute53FullAccess`, `AmazonS3FullAccess`, plus `EKS-Management`.

Why S3 state:

- State must be shared by machines and users.
- Local state causes conflicts and sync problems.
- S3 is a supported backend; the bucket is private by default.

## 8. Pipeline in one view

Git tag `v*` -> Ubuntu VM -> checkout code -> install tools -> `fmt` -> `init` + `validate` -> `plan` -> `apply` -> upload `kubeconfig`.

- Secrets: `AWS_ACCESS_KEY_ID`, `AWS_SECRET_ACCESS_KEY`.
- Workflow file: `.github/workflows/main.yml`.
- Uses `hashicorp/setup-terraform@v1`, `actions/checkout@v2`, `actions/upload-artifact@v2`.
- `-auto-approve` means no human confirmation.
- Every run gets a fresh VM; nothing persists between runs.
- Always `plan` before `apply`.

---

# UNIT 3 - MICROSERVICES INFRASTRUCTURE

## 9. Three layers

| Layer | Component | Tool |
|---|---|---|
| 1 | Network | AWS VPC, 4 subnets, gateways, routing |
| 2 | Kubernetes | AWS EKS, managed node group |
| 3 | GitOps | Argo CD installed with Helm |

## 10. Network

- VPC: `10.10.0.0/16` in the sandbox.
- Four subnets across two availability zones:

| Subnet | Type | Role tag |
|---|---|---|
| public-subnet-a | Public | `kubernetes.io/role/elb` |
| public-subnet-b | Public | `kubernetes.io/role/elb` |
| private-subnet-a | Private | `kubernetes.io/role/internal-elb` |
| private-subnet-b | Private | `kubernetes.io/role/internal-elb` |

- Public subnets: host EKS load balancers and NAT gateways. Reachable from the internet.
- Private subnets: host worker nodes and services. Traffic only from inside the VPC.
- Internet gateway + public route table (`0.0.0.0/0`) serve public subnets.
- One NAT gateway per zone, each with an Elastic IP, serves the private subnets.
- Two AZs keep the system alive if one data center fails.
- Availability zones come from `data.aws_availability_zones`, not hardcoded.

**Internet gateway vs NAT gateway**

| Basis | Internet gateway | NAT gateway |
|---|---|---|
| Purpose | VPC to internet | Outbound path for private subnets |
| Used by | Public subnets | Private subnets |
| Placement | On the VPC | In a public subnet |
| Extra need | None | Elastic IP |

## 11. EKS

- EKS = managed Kubernetes. AWS runs the control plane; you configure nodes and the network.
- Control plane: the "brain" that starts, stops and replicates containers.
- Node group: the VMs that run pods.
- Cluster role trusts `eks.amazonaws.com` and attaches `AmazonEKSClusterPolicy`.
- Node role trusts `ec2.amazonaws.com` and attaches:
  - `AmazonEKSWorkerNodePolicy`
  - `AmazonEKS_CNI_Policy`
  - `AmazonEC2ContainerRegistryReadOnly`
- Security group: outbound allowed, no inbound.
- Scaling limits (`desired` 1, `min` 1, `max` 3 in the sandbox) prevent runaway billing.
- Module outputs: cluster ID, name, certificate data, endpoint, node group ID.
- The module also generates `kubeconfig`, which the pipeline publishes as an artifact.

## 12. GitOps and Argo CD

- Continuous Delivery: teams ship finished services as containers (CI/CD pipeline).
- Continuous Deployment: the release team deploys them to production (Argo CD).
- GitOps: Git holds the target state; the tool syncs the system and alerts on drift.
- Argo CD is installed into the cluster, so the module uses the **Kubernetes and Helm providers**, not AWS.
- Installation: create namespace `argo`, then a `helm_release` with chart `argo-cd` from the Argo Helm repository.
- Argo CD watches a Git repo and deploys new versions automatically.

## 13. Module pattern

| File | Role |
|---|---|
| `variables.tf` | Inputs |
| `main.tf` | Logic and resources |
| `outputs.tf` | Return values |

- Repos: `module-aws-network`, `module-aws-kubernetes`, `module-argo-cd`, all public.
- Each environment calls the modules with `source = "github.com/..."` and its own values.
- Benefits: DRY, encapsulation, one fix updates every environment.
- Module order: network -> EKS -> Argo CD, because each depends on the previous outputs.

## 14. Sandbox build order

1. Create `env-sandbox` repo (private, Terraform `.gitignore`) and clone it.
2. Write starter `main.tf` with the S3 backend and locals.
3. Commit and push; create the GitHub Actions workflow; add secrets.
4. Add the network module call; tag `v1.0`.
5. Add the EKS module call; tag `v1.1`. Takes 10 to 15 minutes.
6. Add the Argo CD module call; tag `v1.2`.
7. Download `kubeconfig`, run `kubectl get svc` and `kubectl get pods -n argo`.
8. `terraform destroy` when done.

## 15. Verification and cleanup

- `kubectl get svc` shows the `kubernetes` service (ClusterIP, 443/TCP).
- `kubectl get pods -n argo` shows five Running pods: `application-controller`, `dex-server`, `redis`, `repo-server`, `server`.
- `terraform destroy` removes resources in dependency order; about five minutes.
- Success message: `Destroy complete! Resources: 29 destroyed.`
- `aws eks list-clusters` should be empty afterwards.

## 16. Command cheat sheet

| Command | Use |
|---|---|
| `terraform version` | Check installation |
| `terraform fmt` | Format code |
| `terraform init` | Install providers, init backend |
| `terraform validate` | Check syntax |
| `terraform plan` | Dry run |
| `terraform apply -auto-approve` | Apply without prompting |
| `terraform destroy` | Tear down |
| `terraform get -update` | Pull latest module versions |
| `aws configure` | Store AWS credentials and region |
| `aws iam list-users` | Test CLI access |
| `aws s3api create-bucket` | Create the state bucket |
| `aws ec2 describe-vpcs --filters Name=cidr,Values=10.10.0.0/16` | Verify the VPC |
| `aws eks list-clusters` | Verify EKS |
| `git tag -a v1.0 -m "msg"` | Create a tag |
| `git push origin v1.0` | Trigger the pipeline |
| `export KUBECONFIG=<path>` | Point kubectl at the config |
| `kubectl get svc` | Test the cluster connection |
| `kubectl get pods -n argo` | Check Argo CD |

---

# UNIT 4 - DEVELOPER WORKSPACE

## 17. Why it matters

- People take the path of least resistance; make the right behaviour the easiest.
- A good developer experience is an underappreciated prerequisite for microservices success.
- Many pipelines, one per service. Without standards, every team invents its own.
- Guide principles, do not dictate every tool (for example, do not force one editor).

## 18. Three goals

1. Set up code in under one hour.
2. Create new services quickly, easily and predictably using templates.
3. Automate quality control.

## 19. Ten guidelines

1. **Docker only dependency** - no manual setup, no README-only instructions.
2. **Remote or local should not matter** - laptop or cloud server.
3. **Heterogeneous-ready** - multiple languages and databases; two or three stacks.
4. **One service or a subsystem equally easy** - both must be simple to run.
5. **Run databases locally** - Dockerized alternative; MinIO for S3.
6. **Containerization rules** - edit on host, run in container; Dockerfile builds, Compose runs; multistage builds; hot reload and debugger.
7. **Painless migrations** - dated scripts; schema and sample data; run at start and in every build; skippable per environment.
8. **Pragmatic testing** - any timing is fine if tests are meaningful; idiomatic framework; cross-service tests at a higher level; linting.
9. **Branching and merging** - feature branches; merge only when tests pass; visible status; lint blocks merge.
10. **Makefile targets** - same commands in every repository.

**Rule of Twos:** use at least two alternatives for critical components in production, and keep total languages and databases to two or three.

## 20. Makefile targets

`start`, `stop`, `build`, `clean`, `add-module`, `remove-module`, `dependencies`, `test`, `tests-unit`, `tests-at`, `lint`, `migrate`, `add-migration`, `logs`, `exec`.

## 21. Local environment options

| Option | Point |
|---|---|
| Docker4Mac / Docker4Windows | Easiest; CPU and battery heavy; only one Docker and one Kubernetes |
| VirtualBox | Own VMs; even heavier |
| Multipass (Canonical) | Ubuntu VMs; HyperKit on macOS, Hyper-V on Windows |

Multipass basics:

```bash
multipass launch -n docker
multipass launch -m 4G -n dubuntu
multipass list
multipass shell
multipass exec dubuntu -- bash
multipass mount $HOME dubuntu:/home/ubuntu/mac
multipass info dubuntu
multipass restart
```

- Defaults: 1 GB RAM, 5 GB disk, 1 CPU.
- Cassandra needs 6 to 8 GB.
- Do not raise the CPU count on macOS; it broke containers for the authors.

## 22. Installing Docker

1. `sudo apt-get update && sudo apt-get upgrade -y`
2. `sudo apt-get install build-essential -y`
3. Remove old packages: `sudo apt-get remove docker docker-ce-cli docker-engine docker.io containerd runc`
4. `sudo snap install docker`
5. `echo 'export PATH=/snap/bin:$PATH' >> ~/.bashrc` then `source ~/.bashrc`
6. Run without sudo:

```bash
sudo groupadd docker
sudo usermod -aG docker $USER
exit
multipass restart
multipass shell
```

- Log out and back in for the group change to apply.
- Verify: `docker ps`, `docker version`, `docker-compose --version`.

MySQL test (`mysql-stack.yml`): image `mysql`, `restart: always`, `MYSQL_ROOT_PASSWORD`, port `33060:3306`. Run with `docker-compose -f mysql-stack.yml up -d`.

## 23. Cassandra

- Image `cassandra:3.11`, container `cassandra-seed`.
- Port 9042 for native clients. 7199 (JMX) and 9160 (Thrift) are commented out.
- Named volume keeps data after the container is removed.

```bash
docker-compose up -d
docker-compose ps
docker exec -it cassandra-seed cqlsh
cqlsh> DESCRIBE keyspaces;
```

## 24. Local Kubernetes

- Avoid it for everyday coding. Compose is simpler and can build images.
- Kubernetes is for production-like orchestration: staging, preprod, performance testing.
- The official distribution needs a cluster of servers, so single-machine options exist:

| Tool | Created by | Point |
|---|---|---|
| Minikube | Kubernetes community | Default option |
| k3s | Rancher | Simple and reliable; one-line install |
| MicroK8s | Canonical | Snap install; add user to the `microk8s` group |

- Kubernetes has no built-in image build, so there is no natural build-run-test loop.
- **Skaffold** makes image building part of the Kubernetes cycle.

## 25. Comparisons

**Dockerfile vs Docker Compose**

| Aspect | Dockerfile | Compose |
|---|---|---|
| Use | Build an image | Run things locally |
| Output | Image for any production runtime | Multi-container local setup |
| Feature | Multistage builds | `up -d`, `-f` for a custom file |

**Compose vs Kubernetes**

| Aspect | Compose | Kubernetes |
|---|---|---|
| Complexity | Lower | Higher |
| Image build | Built in | Needs a registry image |
| Best for | Everyday development | Production-like orchestration |
| Local use | Recommended | Only for targeted testing |

---

# EXAM SHEET

## 26. Numbers to remember

| Item | Value |
|---|---|
| Terraform (local install) | 0.12.20 |
| Subnets | 4 (2 public, 2 private) |
| Availability zones | 2 |
| NAT gateways | 2 |
| Elastic IPs | 2 |
| Argo CD pods | 5 |
| Resources destroyed | 29 |
| EKS build time | 10 to 15 minutes |
| MySQL test port | 33060 host to 3306 container |
| Cassandra port | 9042 |

## 27. Ten facts that carry marks

1. Immutability = destroy and re-create, never patch.
2. The cloud makes immutability affordable because components are virtual.
3. IaC = infrastructure defined in machine-readable files.
4. Terraform is declarative, supports immutability and tracks state.
5. State lives in S3 so it can be shared safely.
6. All changes go through the CI/CD pipeline, never the console.
7. The platform has three layers: network, EKS, Argo CD.
8. Four subnets across two AZs: public for load balancers, private for workloads.
9. Modules are `variables.tf`, `main.tf`, `outputs.tf`, reused by every environment.
10. Argo CD makes Git the source of truth for deployment.

## 28. Likely long questions and their skeletons

**Immutable infrastructure and cloud**
- Define -> problems of mutable systems and drift -> trade-off -> cloud makes re-creation cheap -> benefits -> IaC completes the picture.

**IaC and Terraform**
- Definition -> consequences -> tool requirements -> Terraform's three reasons -> declarative vs imperative -> HCL.

**AWS setup**
- IAM user with programmatic access -> CLI configure -> group and policies -> custom EKS policy -> S3 bucket -> no manual changes after.

**Pipeline**
- Tag trigger -> VM -> checkout -> install tools -> fmt -> init and validate -> plan -> apply -> upload kubeconfig -> secrets and YAML.

**Three infrastructure components**
- Network (VPC, 4 subnets, IGW, NAT) -> EKS (control plane, node group, IAM) -> Argo CD (Helm, GitOps) -> how they depend on each other.

**Network design**
- Two AZs for resilience -> public subnets for ELBs and NAT -> private subnets for nodes -> role tags -> route tables.

**EKS module**
- Cluster role and policy -> node role and three policies -> security group -> node group and scaling limits -> kubeconfig output.

**Argo CD**
- Kubernetes and Helm providers -> namespace -> helm_release -> tag v1.2 -> verify with kubectl -> destroy.

**Developer workspace**
- Why -> 3 goals -> 10 guidelines -> Rule of Twos -> makefile targets.

**Docker and Cassandra**
- Environment options -> Multipass -> install Docker -> non-root group -> MySQL test -> Cassandra compose, volume and cqlsh.

**Compose vs Kubernetes**
- Complexity -> image building -> best use -> local options -> Skaffold.

## 29. Common mistakes to avoid

- Saying IaC removes the need for testing. It does not; the pipeline tests.
- Calling the pipeline optional. After setup, it is the only way changes are applied.
- Mixing up public and private subnets: load balancers in public, workloads in private.
- Saying the NAT gateway needs no Elastic IP. It does.
- Placing Argo CD on AWS directly. It runs inside the Kubernetes cluster.
- Treating Dockerfile and Compose as the same thing. Build versus run.
- Forgetting that `terraform destroy` must run from the sandbox directory after `terraform init`.

## 30. Fifteen-minute final pass

1. Read the definitions table.
2. Read the 11 key decisions.
3. Read the network and EKS bullets.
4. Read the command cheat sheet.
5. Read the 10 guidelines and the Rule of Twos.
6. Read the numbers table.
7. Sketch Figures 6-1, 6-11, 7-1 and 7-2 from memory.

---

## Related Notes

- [[cie2notes]] - full notes with code and explanations.
- [[pyq cie 2]] - question bank with answers.
