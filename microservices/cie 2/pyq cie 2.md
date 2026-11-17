---
tags: [microservices, cie-2, pyq]
---

# Microservices - CIE 2 PYQ and Question Bank

Compiled from every question in the Unit 3 and Unit 4 revision sections of [[cie2notes]], arranged as a practice paper with an answer key.

**Coverage:** infrastructure pipeline, IaC environment, AWS configuration, IaC pipeline, infrastructure components, infrastructure implementation, developer workspace, local containerized setup, Docker, Cassandra, local Kubernetes.

**How to use:**

1. Attempt each section without looking at the notes.
2. Check the answer key and mark what you missed.
3. For a timed mock, pick 8 short, 5 medium and 3 long questions (about 75 marks, 2.5 hours).

| Section | Type | Marks each | Questions |
|---|---|---|---|
| A | Short answer | 2 | 8 |
| B | Medium answer | 5 | 8 |
| C | Long answer | 10 | 8 |

---

## Section A - Short Answers (2 marks each)

**A1.** Define immutable infrastructure.

**A2.** Define Infrastructure as Code (IaC).

**A3.** What is configuration drift?

**A4.** What is CIDR? Give an example.

**A5.** Differentiate an internet gateway and a NAT gateway.

**A6.** Define GitOps.

**A7.** What is the Rule of Twos?

**A8.** Name the three single-machine Kubernetes options and state one point about each.

---

## Section B - Medium Answers (5 marks each)

**B1.** Why does the infrastructure need a CI/CD pipeline?

**B2.** Why is the Terraform state file stored in S3 instead of locally?

**B3.** Describe the steps of the infrastructure pipeline.

**B4.** Why are four subnets created across two availability zones?

**B5.** How does the network module get reused across environments?

**B6.** Explain the IAM roles used by the EKS module.

**B7.** Differentiate a Dockerfile and Docker Compose. Why are multistage builds used?

**B8.** Explain how Cassandra is installed locally using Docker Compose.

---

## Section C - Long Answers (10 marks each)

**C1.** Explain immutable infrastructure and why the cloud enables it.

**C2.** What is Infrastructure as Code? Why is Terraform used for it?

**C3.** List and explain the steps to configure the AWS operations account.

**C4.** Explain the three infrastructure components of the microservices platform.

**C5.** How is Argo CD installed, verified and cleaned up?

**C6.** Why is a good developer workspace important in microservices? State the three goals and explain the ten workspace guidelines.

**C7.** Explain the Multipass setup, the Docker installation steps, running Docker without sudo, and the standard makefile targets.

**C8.** Compare Docker Compose with Kubernetes for local development. Explain the local Kubernetes options.

---

# Answer Key

## Section A

### A1. Immutable infrastructure

- An object is immutable if it cannot be changed after it is created. The only way to update it is to destroy it and create a new one.
- Applied to infrastructure: components are never patched or altered in place. A change means re-creating the component and any dependent components with the new properties.
- Benefit: infrastructure becomes predictable, reproducible and free of configuration drift.

### A2. Infrastructure as Code

- All infrastructure changes are represented as machine-readable files (code).
- The files define the target state of the infrastructure, and reapplying them re-creates the environment.
- Infrastructure is treated like an application: changes are written, tested, reviewed and deployed through a pipeline instead of manual operator work.

### A3. Configuration drift

- The gradual divergence of server and environment states caused by inconsistent manual changes over time.
- Servers that were once identical slowly differ in patches, configuration and running processes.
- Result: environments become hard to reproduce, new servers are unpredictable, and more manual expertise is needed.

### A4. CIDR

- Classless Inter-Domain Routing is a shorthand notation for an IP address range.
- The number after the slash is the network prefix length.
- Example: `10.0.0.0/16` covers `10.0.0.0` to `10.0.255.255`.
- In the notes it is used to define the VPC range (`10.10.0.0/16`) and the four subnet ranges (`/18` blocks inside it).

### A5. Internet gateway vs NAT gateway

| Basis | Internet gateway | NAT gateway |
|---|---|---|
| Purpose | Connects the VPC to the public internet | Gives private-subnet resources outbound access to the internet |
| Used by | Public subnets, route `0.0.0.0/0` | Private subnets, route `0.0.0.0/0` for outbound only |
| Placement | Attached to the VPC | Placed in a public subnet |
| Requirement | None extra | Needs an Elastic IP address |

### A6. GitOps

- A way of working that uses Git as the source of truth. Whatever is in Git is the target state of the system.
- It prescribes a declarative approach, like Terraform.
- GitOps tools synchronize the system to match the Git repository and alert operations teams when the real system drifts.
- The term was created by WeaveWorks. Argo CD is the GitOps tool used in the book.

### A7. Rule of Twos

- Practise heterogeneity proactively.
- For any critical component, use at least two alternatives in production at the same time, even if only one is needed.
- The infrastructure must support two alternatives as easily as one.
- Limit the total languages and database systems across the application to two or three to avoid maintenance overhead.

### A8. Single-machine Kubernetes options

| Tool | Created by | Point |
|---|---|---|
| Minikube | Kubernetes community | The default single-machine option |
| k3s | Rancher | Favoured for simplicity and reliability; installed with a single script |
| MicroK8s | Canonical | Favoured for simplicity and reliability; installed via snap; requires adding the user to the `microk8s` group |

---

## Section B

### B1. Why the infrastructure needs a CI/CD pipeline

- Immutability and IaC make changes predictable, but not necessarily safe. A small network change can bring down a load balancer, or a development change can reach production.
- Manual validation (checking and double-checking every change) slows the rate of change and finds problems late, leading to a large, risky testing phase.
- A pipeline continuously integrates changes, tests them and delivers them, producing a rhythm of small, testable changes.
- It applies every change the same way every time and relies on tooling to run many tests efficiently.
- In this project the pipeline is GitHub Actions, triggered by a Git tag, and it formats, validates, plans and applies the Terraform code automatically.
- Result: infrastructure changes become faster, safer and repeatable, and teams can provision environments without manual steps.

### B2. Why the state file is stored in S3

- Terraform must know the current state of the environment and the last operations it performed to plan the right changes.
- By default the state file is kept on the local filesystem.
- State often has to be shared across machines and users so one environment can be managed from several places.
- Local files are hard to share and cause state conflicts and synchronization issues.
- S3 is a supported Terraform backend, so only a private bucket and correct permissions for the ops user are needed.
- The bucket must have a unique name and be in the chosen region. It is private by default, which suits sensitive state data.

### B3. Infrastructure pipeline steps

| Order | Step | Action |
|---|---|---|
| Trigger | New Git tag | A tag matching `v*` starts the workflow |
| 1 | Provision machine | An Ubuntu virtual machine is created for the job |
| 2 | Get the code | `actions/checkout@v2` checks out the Terraform code |
| 3 | Install tools | Set up Terraform and install dependencies such as the AWS IAM authenticator |
| 4 | Format | `terraform fmt` |
| 5 | Validate | `terraform init` and `terraform validate` |
| 6 | Plan | `terraform plan` |
| 7 | Apply | `terraform apply -auto-approve` changes the AWS infrastructure |
| 8 | Publish | Upload the `kubeconfig` file as a downloadable artifact |

- AWS credentials are provided through repository secrets (`AWS_ACCESS_KEY_ID`, `AWS_SECRET_ACCESS_KEY`).
- The workflow is stored in `.github/workflows/main.yml` and written in YAML.

### B4. Four subnets and two availability zones

- An availability zone is a separate physical data center in AWS. Using two zones keeps services running if one data center fails.
- EKS requires subnets in at least two availability zones.
- Amazon recommends a VPC with both public and private subnets.
- Public subnets allow traffic from the internet. EKS deploys load balancers there to manage inbound traffic.
- Private subnets accept traffic only from inside the VPC. The containerized microservices and worker nodes run there.
- Four subnets give one public and one private subnet in each of the two zones:
  - `public-subnet-a` and `private-subnet-a` in zone A
  - `public-subnet-b` and `private-subnet-b` in zone B
- Role tags tell EKS where it may create load balancers (`kubernetes.io/role/elb` for public, `kubernetes.io/role/internal-elb` for private).

### B5. Reusing the network module

- The network is written once as a module in `module-aws-network`, with `variables.tf`, `main.tf` and `outputs.tf`.
- The module exposes `vpc_id`, `subnet_ids`, `public_subnet_ids` and `private_subnet_ids`.
- Each environment's `main.tf` calls the module with the `module` resource, a GitHub `source` and its own variable values (environment name, CIDRs, cluster name).
- The sandbox and production environments therefore point at the same module, as shown in Figure 7-1.
- Benefits:
  - New environments are created by writing a small file that reuses the module.
  - A change in the module changes the configuration in every environment at once.
  - It follows the coding practices of modules, encapsulation and DRY.

### B6. IAM roles in the EKS module

**Cluster role**

- Trust policy allows the EKS service (`eks.amazonaws.com`) to assume the role with `sts:AssumeRole`.
- The `AmazonEKSClusterPolicy` policy is attached.
- This gives EKS permission to create VMs and make network changes while managing Kubernetes.

**Node role**

- Trust policy allows the EC2 service (`ec2.amazonaws.com`) to assume the role.
- Three policies are attached:

| Policy | Purpose |
|---|---|
| `AmazonEKSWorkerNodePolicy` | Lets worker nodes connect to EKS clusters |
| `AmazonEKS_CNI_Policy` | Networking (CNI plug-in) permissions for the nodes |
| `AmazonEC2ContainerRegistryReadOnly` | Lets nodes pull container images from the Amazon registry |

- The node group uses this role, specifies its subnets, disk size, instance type and scaling limits (`desired`, `min`, `max`).
- The security group allows unrestricted egress and no ingress.
- Scaling limits matter because EKS automatically creates and removes nodes. Without limits a very large AWS bill is possible.

### B7. Dockerfile vs Docker Compose and multistage builds

| Aspect | Dockerfile | Docker Compose |
|---|---|---|
| Use | Building a container image | Running things locally, including complex integrations |
| Output | An image runnable on Kubernetes, AWS ECR, Swarm or any production runtime | A local multi-container setup with services, ports, volumes and environment variables |
| Main feature | Multistage builds | `docker-compose up -d`, custom files with `-f` |

- **Multistage builds** use several stages in one Dockerfile.
- The production stage produces a slim image with only what is needed to run, improving security and performance.
- The development stage keeps a fuller image with tools for hot-reloading and debugging.
- This lets one Dockerfile serve both local usability and production safety.

### B8. Installing Cassandra with Docker Compose

- Cassandra needs more memory than the default Multipass allocation. Give the instance 6 to 8 GB.
- Create a `docker-compose.yml` in the instance:

```yaml
version: '3'
services:
  cassandra-seed:
    container_name: cassandra-seed
    image: cassandra:3.11
    ports:
      - "9042:9042"
    volumes:
      - local_cassandra_data_seed:/var/lib/cassandra

volumes:
  local_cassandra_data_seed:
```

- Port 9042 serves native protocol clients. Ports 7199 (JMX) and 9160 (Thrift) are usually commented out.
- The named volume keeps data after the container is removed.
- Run and verify:

```bash
docker-compose up -d
docker-compose ps
docker exec -it cassandra-seed cqlsh
```

- Inside the CQL shell, `DESCRIBE keyspaces;` lists the keyspaces and proves the installation works.

---

## Section C

### C1. Immutable infrastructure and the cloud

**Definition**

- An immutable object cannot be changed after creation. Updating it means destroying it and creating a new one.
- Immutable infrastructure applies this to servers, networks and other components.
- Example: a load balancer with defined routes cannot have its routes edited. It is destroyed and re-created with the new routes.

**Problems with mutable infrastructure**

- Operators patch systems, edit configurations and restart processes by hand.
- Servers run continuously and the operator "shapes" each environment.
- With many servers and environments, changes are applied inconsistently.
- Over time the states drift apart, making environments hard to reproduce and new servers unpredictable.
- This needs expertise and manual effort, slows delivery and makes a self-service platform difficult.

**Benefits of immutability**

- Predictable and reproducible infrastructure.
- No configuration drift.
- Easy replication and instantiation of production-like environments.
- Testing and replication become easier because structure and behaviour are predictable.

**The trade-off**

- Destroying and re-creating components has a cost. If it were too high, changes would never be made.

**Why the cloud enables it**

- Without the cloud, acquiring hardware, managing servers and procuring software adds heavy cost and complexity.
- In the cloud, infrastructure components are virtual, so they can be treated like software objects.
- Servers and devices can be created and destroyed as cheaply as software components.
- The cloud removes the cost barrier that would otherwise make immutability impractical.

**Remaining requirement**

- A manageable definition of the whole infrastructure is still needed, which is provided by Infrastructure as Code.

### C2. IaC and Terraform

**Definition of IaC**

- All infrastructure changes must be represented as machine-readable files (code). This is the single constraint on which IaC is based.

**Consequences**

- Teams can point to a group of files that define the target state.
- An environment can be re-created by reapplying the code that created it.
- Managing the code becomes a way of managing infrastructure state.
- Changes are managed through how the code is written, tested and deployed.

**Role in immutability**

- Immutability needs object definitions so changes happen by re-creation.
- IaC makes infrastructure behave like an application, so creating or changing a component is similar to running a program.
- Development knowledge such as version control, testing and review can be applied to infrastructure.

**Tool requirements**

1. Changes can be defined as machine-readable files.
2. The tool interprets those files and applies them to a target environment.

**Why Terraform**

- **Declarative:** the target state is declared and Terraform works out the steps. Traditional configuration management needs imperative, step-by-step instructions.
- **Supports immutability:** when the desired state changes, Terraform destroys and re-creates the component and its dependents.
- **State tracking:** Terraform records the current state so it can plan how to reach the declared state. The state must be shared carefully by everyone using the tool.
- It uses **HCL** (HashiCorp Configuration Language), a JSON-like format that is easy to learn.
- It has providers for many platforms, so the same tool works with AWS, Kubernetes, Helm and others.

**Key decision**

- Adopt IaC and use Terraform for all infrastructure changes. No manual changes outside the code.

### C3. Configuring the AWS operations account

**Purpose**

- The pipeline must deploy infrastructure into AWS automatically. AWS infrastructure should not be changed directly through the browser, but a few initial manual steps are needed to create credentials and permissions.

**Step 1: Create the IAM user**

1. Log in to the AWS console as the root user.
2. Open the IAM service (under Security, Identity and Compliance).
3. Select Users, then Add user.
4. Name it `ops-account` and choose Programmatic access so it can use the CLI and API.
5. Attach the existing policy `IAMFullAccess`.
6. Skip tags and review the details.
7. Create the user.
8. Copy the Access key ID and Secret access key to a safe place. Whoever holds them can create resources at the owner's expense.

**Step 2: Configure the AWS CLI**

```bash
aws configure
```

- Enter the access key ID, secret access key, default region (for example `eu-west-2`) and output format (`json`).
- Test with `aws iam list-users`. Seeing the users proves the CLI works.

**Step 3: Create the Ops-Accounts group and policies**

```bash
aws iam create-group --group-name Ops-Accounts
aws iam add-user-to-group --user-name ops-account --group-name Ops-Accounts
aws iam attach-group-policy --group-name Ops-Accounts --policy-arn <policy ARN>
```

- Policies attached: `IAMFullAccess`, `AmazonEC2FullAccess`, `AmazonEC2ContainerRegistryFullAccess`, `AmazonEKSClusterPolicy`, `AmazonEKSServicePolicy`, `AmazonVPCFullAccess`, `AmazonRoute53FullAccess`, `AmazonS3FullAccess`.
- A group means new users get the same permissions automatically.

**Step 4: Create the custom EKS policy**

- No built-in policy covers the required EKS actions.
- Create `custom-eks-policy.json` with `Version "2012-10-17"` and two statements:
  - Allow `eks:DescribeNodegroup`, `eks:DeleteNodegroup`, `eks:ListClusters`, `eks:CreateCluster` on `*`.
  - Allow `eks:*` on `arn:aws:eks:*:*:cluster/*`.
- Create and attach it:

```bash
aws iam create-policy --policy-name EKS-Management --policy-document file://custom-eks-policy.json
aws iam attach-group-policy --group-name Ops-Accounts --policy-arn {YOUR_POLICY_ARN}
```

- The ARN (Amazon Resource Name) is the unique identifier of the policy and must be noted.

**Step 5: Create the S3 state bucket**

```bash
aws s3api create-bucket --bucket {YOUR_S3_BUCKET_NAME} --region {YOUR_AWS_REGION} \
  --create-bucket-configuration LocationConstraint={YOUR_AWS_REGION}
```

- For `us-east-1`, omit the location constraint.
- The bucket name must be unique in the region. The bucket is private by default, which is required for state.

**Result**

- The `ops-account` user can create, edit and delete AWS resources and use the state bucket. From this point on, changes are made only through Terraform code and the automated pipeline.

### C4. Three infrastructure components

**1. Network (base layer)**

- An AWS VPC is the parent object of the virtual network.
- Four subnets: two public and two private, spread across two availability zones.
- An internet gateway connects the VPC to the internet for public subnets.
- Two NAT gateways, each with an Elastic IP, give private subnets outbound internet access.
- Route tables and associations direct `0.0.0.0/0` traffic appropriately.
- Security groups and role tags control and identify traffic.

**2. Kubernetes service (middle layer)**

- AWS EKS is a managed Kubernetes service.
- EKS provides the control plane as a managed service. The control plane is the "brain" that decides when to start, stop and replicate containers.
- A managed node group provides the worker nodes (VMs) that run the pods.
- Kubernetes orchestrates the containers: rollouts and rollbacks, scaling, storage, secrets, load balancing and traffic management.
- Result: an automated solution for resilience, scaling and fault tolerance.

**3. GitOps deployment server (top layer)**

- Argo CD is installed into the cluster with Helm.
- Continuous Delivery: teams automatically ship finished services as containers through their CI/CD pipelines.
- Continuous Deployment: the release team releases those containers into production using Argo CD.
- Argo CD watches Git as the source of truth, keeps the environment synchronized and alerts when the real system drifts.
- A Helm chart is the package used to install the Argo CD server.

| Layer | Component | Tool | Role |
|---|---|---|---|
| 1 | Network | AWS VPC, subnets, gateways | Foundation on which everything runs |
| 2 | Kubernetes | AWS EKS | Orchestrates the service containers |
| 3 | GitOps | Argo CD via Helm | Deploys services from Git into the cluster |

### C5. Argo CD installation, verification and cleanup

**Installation**

- Argo CD is installed on the Kubernetes cluster, not on AWS directly, so the module uses the Kubernetes and Helm providers instead of the AWS provider.
- The Kubernetes provider is configured with the EKS endpoint, certificate authority data and the `aws-iam-authenticator token -i <cluster>` command.
- The module creates a namespace `argo`.
- A `helm_release` installs the `argo-cd` chart from `https://argoproj.github.io/argo-helm`.
- The module receives the cluster ID, name, certificate, endpoint and node group ID from the EKS module's outputs.
- The sandbox `main.tf` calls the module under `# GitOps Configuration`.
- Commit, push and tag `v1.2` to run the pipeline. The EKS build must finish first.

**Verification**

1. Download the `kubeconfig` artifact from the latest GitHub Actions run and unzip it.
2. Point kubectl at it:

```bash
export KUBECONFIG=~/Downloads/kubeconfig
```

3. Check the cluster:

```bash
kubectl get svc
```

- The `kubernetes` service should appear (ClusterIP, port 443/TCP), proving the network, EKS and connection work.

4. Check Argo CD:

```bash
kubectl get pods -n "argo"
```

- Five pods should be `1/1 Running`: `application-controller`, `dex-server`, `redis`, `repo-server`, `server`.

**Cleanup**

- Reason: avoid charges, especially for idle EKS clusters and elastic IPs.
- Destroying is low risk because the environment is fully defined in Terraform and can be re-created.
- Steps:

```bash
git pull
terraform init
terraform destroy
```

- Confirm with `yes`. Terraform destroys resources in dependency order using its dependency graph.
- Success message: `Destroy complete! Resources: 29 destroyed.`
- Verify:

```bash
aws eks list-clusters
```

- The list should be empty. Optionally check VPCs and load balancers.
- Destroy works locally because the AWS keys are in the local credentials file, which is not acceptable for a secure production environment.

### C6. Developer workspace, goals and guidelines

**Why it matters**

- In a complex system, the only sustainable way to get predictable collaboration is to make the right behaviours the easiest and most intuitive.
- If doing the right thing is hard, people take the path of least resistance, which leads them the wrong way.
- An exceptional developer experience is one of the most underappreciated prerequisites of a successful microservices culture.
- There will be many pipelines, one per microservice. Without standards, every team builds a different one. Creating a service should be templated and mostly automated.
- Do not dictate every aspect of the workflow, such as forcing one editor. Declare only the fundamental principles.

**Three goals**

1. Code can be set up in a short time frame. Target: under one hour for a new developer.
2. New microservices can be created quickly, easily and predictably, using well-thought-out templates for each tech stack.
3. Quality control must be automated, not left to human error.

**Ten workspace guidelines**

1. **Make Docker the only dependency.** Avoid the "works for me" syndrome. Only Docker and Docker Compose are expected on the host. Setup must be automated, not written in a README.
2. **Remote or local should not matter.** The setup works on a laptop or a cloud server. Exceptions must be justified and documented.
3. **Ensure a heterogeneous-ready workspace.** Support multiple languages and data stores. When uncertain, use two or at most three stacks. Apply the Rule of Twos.
4. **Running one microservice or a subsystem should be equally easy.** A developer can check out one service or the whole interacting group.
5. **Run databases locally, if possible.** Provide a Dockerized local alternative for every database. Switching to AWS should be a configuration change. MinIO is a local stand-in for S3.
6. **Implement containerization guidelines.** Edit on the host, run in a container. Dockerfile builds images, Compose runs things locally. Use multistage builds. Provide hot reloading and debugger attachment.
7. **Establish rules for painless database migrations.** Every schema change is a dated migration script. Migrations also insert sample data. They run at project launch and in every build. They can be skipped per environment. They apply to all data stores.
8. **Determine a pragmatic automated testing practice.** Test-first, test-as-you-code and test-after are all acceptable if tests are meaningful. Use an idiomatic framework. External tools must be fully integrated. Cross-service tests need a higher level or a dedicated repository. Set up linting and static analysis.
9. **Branching and merging.** Work on feature and bug branches. Merge to main only when all tests pass. Show test status in pull requests. Lint errors block pushing and merging.
10. **Codify common targets in a makefile.** Every repository has one, so `make start` and `make test` work in any stack.

### C7. Multipass, Docker and makefile targets

**Environment options for macOS and Windows**

- Docker4Mac and Docker4Windows: easiest, but heavy on CPU and battery, and allow only one Docker and one Kubernetes instance.
- VirtualBox or commercial alternatives: own VMs, even heavier.
- Multipass by Canonical: quickly launches Ubuntu hosts, uses HyperKit on macOS and Hyper-V on Windows.

**Multipass setup**

```bash
multipass launch -n docker
multipass list
```

- Defaults: 1 GB RAM, 5 GB disk, 1 CPU.
- Override memory with `-m 4G`. Cassandra needs 6 to 8 GB.
- Useful commands: `multipass shell`, `multipass exec`, `multipass mount`, `multipass info`, `multipass restart`.
- Do not raise the CPU count on macOS; the authors saw broken containers.

**Docker installation inside the instance**

1. Open a shell with `multipass shell`.
2. Update and upgrade:

```bash
sudo apt-get update && sudo apt-get upgrade -y
```

3. Install build tools:

```bash
sudo apt-get install build-essential -y
```

4. Remove old Docker packages:

```bash
sudo apt-get remove docker docker-ce-cli docker-engine docker.io containerd runc
```

5. Install Docker via snap:

```bash
sudo snap install docker
```

6. Add the snap binaries to PATH:

```bash
echo 'export PATH=/snap/bin:$PATH' >> ~/.bashrc
source ~/.bashrc
```

**Run Docker without sudo**

```bash
sudo groupadd docker
sudo usermod -aG docker $USER
exit
multipass restart
multipass shell
docker ps
docker version
docker-compose --version
```

- Logging out and back in is required for the group change to take effect.
- Testing with a small MySQL Compose file and `docker-compose -f mysql-stack.yml up -d` confirms the setup.

**Standard makefile targets**

| Target | Purpose |
|---|---|
| `start` | Run the code |
| `stop` | Stop the code |
| `build` | Build the code, usually a container image |
| `clean` | Clean caches and run from scratch |
| `add-module` | Add a dependency |
| `remove-module` | Remove a dependency |
| `dependencies` | Install all declared modules |
| `test` | Run all tests and produce coverage |
| `tests-unit` | Run unit tests only |
| `tests-at` | Run acceptance tests only |
| `lint` | Check coding style |
| `migrate` | Run database migrations |
| `add-migration` | Create a new migration |
| `logs` | Show container logs |
| `exec` | Run a custom command in the container |

### C8. Docker Compose vs Kubernetes for local development

| Aspect | Docker / Docker Compose | Kubernetes |
|---|---|---|
| Complexity | Lower | Proportionally higher |
| Image building | Straightforward tooling | No build tooling; needs a pre-built image URI from a registry |
| Best for | Everyday development | Production, preprod, staging and performance testing |
| Local use | Recommended | Avoid unless needed for targeted testing |
| Orchestration | Enough for most local component orchestration | Wide functionality for a runtime fleet of containers |

**Why Kubernetes is discouraged locally**

- The official distribution is designed for a cluster of servers, not one machine.
- It does not provide an obvious build-run-test cycle, which makes active development cumbersome.
- Docker and Docker Compose do most local containerization tasks more easily.

**Single-machine options**

| Tool | Created by | Point |
|---|---|---|
| Minikube | Kubernetes community | Default choice |
| k3s | Rancher | Simple and reliable; installed with the official one-line script |
| MicroK8s | Canonical | Simple and reliable; installed with `snap install microk8s --classic`; add the user to the `microk8s` group and log in again |

**Skaffold**

- An open-source toolset that makes building container images pluggable into Kubernetes life cycles.
- It restores a usable build-run-test loop when local Kubernetes is needed.
- The book does not use local Kubernetes in most examples; the Skaffold microservices repository is a sample.

---

# Diagrams to Practise

| Figure | What to draw |
|---|---|
| Figure 6-1 | The target pipeline: code repository, infrastructure CI/CD pipeline, sandbox environment |
| Figure 6-11 | The eight pipeline steps from tag trigger to published kubeconfig |
| Figure 7-1 | One network module reused by the sandbox and production environments |
| Figure 7-2 | Four subnets: public and private in availability zone A and availability zone B |

# Last-Minute Checklist

- Definitions: DevOps, immutable infrastructure, drift, IaC, Terraform, HCL, pipeline, GitOps, VPC, subnet, CIDR, NAT gateway, EKS, pod, Argo CD, Helm.
- Nine key decisions of Chapter 6 and two key decisions of Chapter 7.
- AWS setup sequence: user, policies, group, custom EKS policy, CLI, S3 bucket.
- Network module: VPC, four subnets, IGW, two NAT gateways, two EIPs, route tables.
- EKS module: cluster role, node role, security group, node group, kubeconfig.
- Argo CD module: Kubernetes and Helm providers, `argo` namespace, `helm_release`.
- Terraform commands and when each is used.
- Three developer-workspace goals, ten guidelines, Rule of Twos, makefile targets.
- Docker install steps, non-root fix, MySQL test, Cassandra Compose file.
- Compose vs Kubernetes, three local Kubernetes options, Skaffold.

# Related Notes

- [[cie2notes]] - full CIE-2 notes.
- [[cie 1 - notes]] - CIE-1 class notes.
- [[pyq cie 1]] - CIE-1 question paper and answers.
