---
share_link: https://share.note.sx/9idtqr6r#ngUA8k9HJiTC7dot8pI+31Bxf40JewLFe5T7/naMC0Q
share_updated: 2026-11-14T12:05:59+05:30
---
# Microservices - CIE 2 Notes

Covers Unit 3 (Chapters 6 and 7) and Unit 4 (Chapter 8) in syllabus order.

| Syllabus topic | Where it is covered |
|---|---|
| Building an Infrastructure Pipeline: DevOps Principles and Practices | Part 1, Section 1 |
| Setting Up the IaC Environment | Part 1, Section 2 |
| Configuring Amazon Web Services | Part 1, Section 3 |
| Building an IaC Pipeline | Part 1, Section 4 |
| Building a Microservices Infrastructure: Infrastructure Components | Part 2, Section 5 |
| Implementing the Infrastructure | Part 2, Section 6 |
| Developer Workspace: Coding Standards and the Developer's Setup | Part 3, Section 7 |
| Setting Up a Containerized Environment Locally | Part 3, Section 8 |
| Installing Docker | Part 3, Section 9 |
| Advanced Local Docker Usage: Installing Cassandra | Part 3, Section 10 |
| Installing Kubernetes | Part 3, Section 11 |

## Contents

- Part 1: Unit 3 - Building an Infrastructure Pipeline
  - 1. DevOps Principles and Practices
  - 2. Setting Up the IaC Environment
  - 3. Configuring Amazon Web Services
  - 4. Building an IaC Pipeline
  - Unit 3 Revision (definitions, comparisons, commands, questions)
- Part 2: Building a Microservices Infrastructure
  - 5. Infrastructure Components
  - 6. Implementing the Infrastructure
- Part 3: Unit 4 - Developer Workspace
  - 7. Coding Standards and the Developer's Setup
  - 8. Setting Up a Containerized Environment Locally
  - 9. Installing Docker
  - 10. Advanced Local Docker Usage: Installing Cassandra
  - 11. Installing Kubernetes
  - Unit 4 Revision
- Appendix: Chapter 9 Outline (optional)

---

# PART 1: UNIT 3 - BUILDING AN INFRASTRUCTURE PIPELINE

## 1. DevOps Principles and Practices

### 1.1 DevOps and microservices

- **DevOps** is the pursuit of improvements in how software is developed, released and supported. It covers organizational design, culture, process and tooling.
- **Microservices** share the same goal but add bounded services with independent deployment and management.
- The two go hand in hand. Building microservices without DevOps practices would be extremely challenging.
- Adopting DevOps gives access to a large ecosystem of tools for code management, build management and releases, which reduces the time needed to get the infrastructure running.
- **Core benefit:** DevOps reduces the time taken to change applications without adding risk, giving change speed and change safety together.
- For infrastructure, this means more changes, more improvements and a better platform service for microservices teams.

Three DevOps concepts used in the infrastructure platform:

1. Immutable infrastructure
2. Infrastructure as Code (IaC)
3. Continuous Integration (CI) and Continuous Delivery (CD)

### 1.2 Immutable infrastructure

- **Definition:** an object is immutable if it cannot be changed after it is created. The only way to update it is to destroy it and create a new one.
- Immutable things have predictable and reproducible behaviour and structure, which makes testing and replication easier.
- Programming analogy: an immutable variable `x` assigned 10 will always remain 10.
- **Immutable infrastructure principle:** applies this property to infrastructure components.
  - Example: a network load balancer with defined routes. The routes cannot be changed without destroying the load balancer and creating a new one.
- **Main advantage:** infrastructures become predictable and easily reproducible.

Problems with traditional (mutable) systems:

- Human operators do a lot of manual work: patching systems, altering configurations, stopping and starting processes.
- Servers and devices run continuously and the operator "shapes" the environment for the application.
- With multiple environments and servers, the operator must shape each one.
- Over time, changes are applied inconsistently and the state of systems drifts. This is called **configuration drift** or **server drift**.
- Consequences of drift:
  - Hard to keep all servers in the same state.
  - Adding new servers or changing environment state becomes a problem because of variability and unpredictability.
  - More expertise and manual effort are needed, which slows delivery.
  - A self-service infrastructure platform becomes difficult to offer.

**Key decision:** Apply the principle of immutable infrastructure. Infrastructure components must not be changed after creation. Changes are made by re-creating the component (and any dependent components) with the new or altered properties.

Trade-off and how it is handled:

- **Trade-off:** destroying and re-creating configurations has a cost. If it were too costly, infrastructure changes would never be made.
- **First mitigation:** build the platform in the cloud.

**Key decision:** Implement the infrastructure in the cloud. Infrastructure components are deployed and managed in a cloud platform.

Why the cloud enables immutability:

- Without the cloud, physical hardware acquisition, server management and software procurement would bring heavy complexity and cost.
- In the cloud, infrastructure components are virtual, so they can be treated like software.
- Servers and devices can be created and destroyed like software components or objects in an object-oriented system.

Benefits of immutable infrastructure:

- Avoids server drift.
- Improves the ability to replicate and instantiate new environments in a production-like state.

Remaining need: a way to define all infrastructure through a manageable set of configurations. This is provided by IaC.

### 1.3 Infrastructure as Code (IaC)

- **Definition:** all infrastructure changes must be represented as a set of machine-readable files (code). This is the single constraint on which IaC is based.
- Consequences:
  - Teams can point to a group of files that define the target state of the infrastructure.
  - An environment can be re-created by reapplying the code that created it.
  - Managing the infrastructure code becomes a way of managing the infrastructure state.
  - Changes are managed by managing how infrastructure code is written, tested and deployed.
- Role in immutability: immutability requires definitions of objects to be managed so that changes happen by re-creation. IaC lets infrastructure be treated like applications, so creating or changing components is similar to running a program.
- Advantage: knowledge from application development can be applied to infrastructure.

**Key decision:** Adopt IaC. All infrastructure changes should be made in managed code files. Changes should not be made manually by human operators outside the code.

Requirements for an IaC tool:

1. Lets changes be defined as machine-readable code files.
2. Interprets those files and applies them to a target environment.

Tool chosen: HashiCorp's **Terraform**.

### 1.4 Terraform

- Terraform is a popular tool for teams applying IaC and managing infrastructure in an automated, repeatable way.
- It is not the only tool; many alternatives exist.

**Key decision:** Use Terraform for infrastructure changes. HashiCorp's Terraform is used to manage and apply changes to the platform infrastructure.

Reasons for choosing Terraform:

1. **Declarative approach:** the target state is declared and Terraform makes it happen. Traditional configuration management needs step-by-step imperative instructions.
2. **Supports immutable infrastructure:** code describes the desired state. When applied, Terraform destroys and re-creates the component in its new form, including dependent objects.
3. **State tracking:** Terraform tracks the current state of the environment so it can plan how to reach the defined end state.
   - State must be managed carefully and shared by everyone using the tool.

Managing a Terraform solution means managing the state, configuration files, and the quality, safety and maintainability of the whole solution, as for a software application.

| Aspect | Declarative (Terraform) | Imperative (traditional config management) |
|---|---|---|
| What is specified | Desired end state of the infrastructure | Step-by-step instructions to reach a state |
| Who works out the steps | The tool (Terraform) | The user / operator |
| Handling of changes | Tool compares current state with declared state and plans changes | Operator must script every change |

### 1.5 Continuous Integration and Continuous Delivery (CI/CD)

- **Problem:** immutability and IaC make changes predictable, but not necessarily safe.
  - Example 1: a small network change accidentally brings down a load balancer in production.
  - Example 2: a change meant only for the development environment reaches production and causes an outage.
- **Manual approach** (checking and double-checking every change) has drawbacks:
  - Slows the rate of change because of validation work.
  - Late discovery of problems that should have been found earlier in design and development.
  - A long testing phase to fix a large batch of problems, which may alter the infrastructure plan.
- **CI/CD approach:**
  - Continuously integrate changes into the repository.
  - Continuously test that the changes work.
  - Automatically deliver the changes.
  - Goal: a rhythm of releasing small, testable changes instead of one big batch.
- CI/CD relies heavily on tooling, which allows higher volumes of tests to run efficiently.
- **Pipeline:** a tool to define and manage the steps of a CI/CD process, so any code change is integrated and delivered in the same way every time.
- The pipeline is used for all changes, not only infrastructure. Chapter 6 covers the infrastructure pipeline; Chapter 10 defines the microservices pipeline.

**Key decision:** Apply system changes with a CI/CD pipeline. All changes must be applied through an automated pipeline and/or tool. No changes are introduced through command-line instructions or operator consoles.

**Key decision:** Use GitHub Actions for CI/CD pipelines. Teams use GitHub Actions to implement CI/CD pipelines for infrastructure and microservices.

Why GitHub Actions:

- GitHub is already used to manage the code, so a single tool can be used for code management and CI/CD.
- Suitable for the book because of the limits of the printed page.

Limitation: GitHub Actions is relatively new and not as feature-rich as established options such as Jenkins and GitLab.

By the end of Chapter 6, there is a CI/CD pipeline in GitHub Actions that handles Terraform code and makes changes to a cloud-hosted environment. Chapter 7 uses it to provision the microservices infrastructure.

### 1.6 Key decisions of Chapter 6

| Key decision | What it states | Main reason |
|---|---|---|
| Immutable infrastructure | Components are never changed after creation; re-create them instead | Predictable and reproducible infrastructure; avoids drift |
| Implement in the cloud | Infrastructure is deployed and managed on a cloud platform | Virtual components can be created and destroyed like software |
| Adopt IaC | All changes are made in managed code files, not manually | Treat infrastructure like applications; re-create by reapplying code |
| Use Terraform | Terraform manages and applies infrastructure changes | Declarative; supports immutability; tracks state |
| CI/CD pipeline for changes | All changes go through an automated pipeline; no console or command-line changes | Safe, repeatable, small and frequent changes |
| Use GitHub Actions | GitHub Actions implements CI/CD pipelines | Same tool as code management |
| Use GitHub for code | All code is managed with Git and hosted in GitHub | Very popular; code is shared in the book |
| Host in AWS | AWS is the cloud platform for microservices | Largest user base at the time; one provider keeps things simple |
| One repository per environment | Each environment's code and pipeline is in its own repository | Independence for teams; pipeline and code kept together |

---

## 2. Setting Up the IaC Environment

Infrastructure code needs a development environment, just like application code: tools to write, manage, test and run code. Two environments are set up: a local environment and a cloud-hosted environment, used to write, test and publish infrastructure code.

### 2.1 Set up GitHub

- **Git** is used for code management (version control).
- **GitHub** is used as the Git host. GitLab is a popular alternative.
- Reason for GitHub: it is a very popular place to share code, and a lot of code and configuration is shared in the book.

**Key decision:** Use GitHub for code management. All code is managed using the Git version control system and hosted in GitHub.

Requirements:

- A GitHub account (a free account is sufficient).
- A local Git client (download from the Git downloads page if not installed).

Usage: Git and GitHub manage the microservices code and, applying the IaC principle, also the infrastructure code.

Infrastructure code language: **HCL**, which Terraform understands.

### 2.2 Install Terraform

- Terraform is used to manage and apply infrastructure code declaratively.
- It runs automatically in the CI/CD pipeline hosted in GitHub, so a local installation is not strictly required.
- A local installation is still needed to test code before committing it to the pipeline.
- Supported platforms: OS/X, FreeBSD, Linux, OpenBSD, Solaris, Windows.
- Version used in the book: **0.12.20**.
- Verification command: `terraform version`, which prints a response such as `Terraform v0.12.20`.
- Terraform is used to manage resources hosted in AWS.

---

## 3. Configuring Amazon Web Services

### 3.1 Choice of cloud platform

- The three platforms most used by microservices practitioners: Microsoft Azure, Google Cloud Platform (GCP) and AWS.
- The model builds for a single cloud provider to keep the implementation simpler and faster.
- AWS is chosen mainly because it had the biggest user base at the time of writing.
- All three vendors offer similar services, so the model can be adapted to the others with some work.

**Key decision:** Host microservices in AWS. AWS is the cloud platform for microservices.

- An AWS account is required, and a credit card is needed to activate it.
- **Billing caution:** AWS has a free tier, but the resources used in the examples are not covered by free services. The user is responsible for destroying the resources created.
- An operator account must also be set up so the tools can access the AWS instance.

### 3.2 Setting up an AWS operations account

- Purpose: by the end of the chapter the pipeline must deploy infrastructure into AWS automatically.
- As per IaC and immutability, AWS infrastructure should never be changed directly through the browser. A few initial manual steps are still needed.
- First step: configure credentials and permissions so that tools can work with AWS objects.
- **IAM (Identity and Access Management):** the AWS service where users, groups and permissions are managed.
- A special user is created to represent the tooling, with a defined set of permissions. This identity is used for calls from the CI/CD pipeline platform.

Steps to create the Terraform user (`ops-account`):

1. Log in to the AWS management console with the root user credentials.
2. Find and select the IAM service (usually under *Security, Identity and Compliance*).
3. Select Users from the IAM navigation menu on the left and click Add user.
4. Enter the user name `ops-account`; select **Programmatic access** as the access type (so the account can use the CLI and API); click Next: Permissions.
5. Choose **Attach existing policies directly**, search for the policy `IAMFullAccess` and tick its checkbox (a single set of permissions to start with).
6. Click Next: Tags (no tags are created), then Next: Review to review the user's details.
7. Click Create user.
8. Note the **Access key ID** and **Secret access key** (use the *Show* link) and copy them to a temporary file. They are needed later with the pipeline.

Security caution: whoever holds the key material can create resources in the AWS environment at the owner's expense. Keys must be noted before leaving the screen.

Result: user `ops-account` with permission to make IAM changes. This allows the switch from the browser console to the AWS CLI.

### 3.3 Configure the AWS CLI

Three ways to manage a major cloud provider's configuration:

1. Web browser
2. Web-based APIs
3. CLI

- The browser was used to create the operator account.
- Terraform will later configure changes through the AWS APIs.
- The CLI is used for the changes needed before Terraform can make API calls.

Advantages of the CLI:

- Easier to describe the changes needed.
- Less prone to the changes that user interfaces go through.

Installation: install the CLI on the local system from the AWS CLI download page.

Configuration: run `aws configure`, which asks for four values:

- AWS Access Key ID
- AWS Secret Access Key
- Default region name (for example `eu-west-2`; a region closer to the user can be chosen)
- Default output format (for example `json`)

```bash
aws configure
AWS Access Key ID [None]: <access key id of ops-account>
AWS Secret Access Key [None]: <secret access key of ops-account>
Default region name [None]: eu-west-2
Default output format [None]: json
```

Test: run `aws iam list-users`. A list of user accounts (such as `admin` and `ops-account`) shows that the CLI is working and has access to the instance.

### 3.4 Setting up AWS permissions

- The `ops-account` user currently has a policy that only permits modifying IAM settings.
- It needs many more permissions to manage the AWS resources for the infrastructure build.
- Additional policies are created and attached using the AWS command-line tool.

Steps:

1. Create a group called `Ops-Accounts`.
   ```bash
   aws iam create-group --group-name Ops-Accounts
   ```
   Benefit: new users added to the group get the same permissions.
2. Add the user to the group.
   ```bash
   aws iam add-user-to-group --user-name ops-account --group-name Ops-Accounts
   ```
   No response from the CLI means success ("no news is good news").
3. Attach policies to the group.
   ```bash
   aws iam attach-group-policy --group-name Ops-Accounts --policy-arn <policy ARN>
   ```
   The permissions apply automatically to the users in the group.
4. Create a custom EKS policy (no existing AWS policy gives the needed Elastic Kubernetes Service permissions) and attach it to the group.

In practice the permissions of the Ops user change as the infrastructure design progresses. In the book the design is already done, so the required policies are known in advance.

Policies attached to the `Ops-Accounts` group:

| Policy | Purpose (what it allows the group to manage) |
|---|---|
| IAMFullAccess | Identity and Access Management (users, groups, roles, policies) |
| AmazonEC2FullAccess | EC2 virtual machines and related compute resources |
| AmazonEC2ContainerRegistryFullAccess | Container registry used to store container images |
| AmazonEKSClusterPolicy | Permissions for EKS clusters |
| AmazonEKSServicePolicy | Permissions for the EKS service |
| AmazonVPCFullAccess | Virtual private cloud networking |
| AmazonRoute53FullAccess | Route 53 (DNS) service |
| AmazonS3FullAccess | S3 storage (needed for the Terraform state bucket) |
| EKS-Management (custom) | Custom policy for EKS actions not covered by the policies above |

Custom EKS policy (`custom-eks-policy.json`):

- A JSON file with `Version` `"2012-10-17"` and two Statement entries.
  - Statement 1: `Effect` Allow; Action list = `eks:DescribeNodegroup`, `eks:DeleteNodegroup`, `eks:ListClusters`, `eks:CreateCluster`; Resource = `*`.
  - Statement 2: `Effect` Allow; Action = `eks:*`; Resource = `arn:aws:eks:*:*:cluster/*` (all actions on clusters).
- Create the policy:
  ```bash
  aws iam create-policy --policy-name EKS-Management --policy-document file://custom-eks-policy.json
  ```
- The command returns a JSON description of the policy, including its **ARN**.
- **ARN (Amazon Resource Name):** the unique identifier of every AWS resource. The ARN of the new policy must be noted.
- Attach it:
  ```bash
  aws iam attach-group-policy --group-name Ops-Accounts --policy-arn {YOUR_POLICY_ARN}
  ```
- Result: `ops-account` can now create AWS infrastructure resources automatically. It is used in the Terraform code and in the pipeline configuration. The access key and secret must be kept safe.

### 3.5 Creating an S3 backend for Terraform

- **Why Terraform needs state:** Terraform declares what the infrastructure should look like, not the steps. To make the right changes it must know what the environment looks like and the last operations it performed.
- **State file:** Terraform keeps this information in a JSON-based state file that is read and updated every time Terraform runs.
- **Default behaviour:** the state file is kept on the local filesystem.

Problems with local state:

- State often needs to be shared across machines and users so an environment can be managed from several places.
- Local files are difficult to share and cause state conflicts and synchronization issues.

Solution: store the state file in AWS S3. Terraform supports S3 as a state backend out of the box. Only a new bucket has to be created and correct permissions given to the ops user.

- **S3 (Simple Storage Service):** AWS storage service for data objects referenced by a key. Objects are just blobs in any format. Here Terraform stores environment state as JSON objects.

Bucket requirements:

- Needs a unique name across the entire AWS region chosen. Generic names like `test` or `microservices` will probably fail; append your own name.
- Needs a region. Use the same region as the CLI default.
- In the book the bucket name is written as the token `{YOUR_S3_BUCKET_NAME}`.

| Region | Command to create the bucket |
|---|---|
| `us-east-1` (default region) | `aws s3api create-bucket --bucket {YOUR_S3_BUCKET_NAME} --region us-east-1` |
| Any other region | `aws s3api create-bucket --bucket {YOUR_S3_BUCKET_NAME} --region {YOUR_AWS_REGION} --create-bucket-configuration LocationConstraint={YOUR_AWS_REGION}` |

- Success output: a JSON object with the `Location` (URL) of the bucket.
- Security: S3 buckets are not publicly accessible by default, which is good because the state file must not be visible to or changed by anyone. The ops account already has full S3 permission.
- End of manual setup: after this step, the AWS user `ops-account` can create, edit and delete resources and store objects in the state bucket. From here on, changes are made only through code and the automated pipeline.

---

## 4. Building an IaC Pipeline

Importance of the pipeline:

- Gives a safe and easy way to provision environments quickly.
- Without it there would be many manual steps and environments that drift apart.
- With the pipeline there is a stable, declarative definition of the infrastructure.
- Development and operations teams can create their own environments for testing, changes and production release.

No actual AWS infrastructure is created in this chapter. A foundation for the next chapter is built.

Components built in this section:

1. A GitHub-hosted Git repository for a sandbox testing environment.
2. A Terraform root module that defines the sandbox.
3. A GitHub Actions CI/CD pipeline that can create a sandbox environment.

**Sandbox environment:** a test environment for trying out the IaC modules and pipelines. It is built out in Chapter 7 and thrown away when everything works. The assets are later reused for the microservices test environment.

### 4.1 Creating the sandbox repository

**Key decision:** One repository per environment. Each environment's code and pipeline are managed independently in their own code repository.

- Reason: gives teams more independence in managing their environments while keeping the pipeline configuration and code together for easier management.
- **Alternative: monorepo** keeps all environment configurations in a single repository.
  - Makes it easier to share libraries, components and actions between environments.
  - Helps keep things consistent.
  - Practitioners using this usually use specialized CI/CD tooling (Jenkins is the most popular) instead of building inside GitHub.
  - The trade-offs should be considered when designing the next microservices architecture.

Steps:

1. Use GitHub's browser-based interface to create the repository (a GitHub CLI exists, but the browser is quicker here and is later used to run and monitor the pipeline).
2. Repository name: `env-sandbox`; access option: **Private**.
3. Tick **Add .gitignore** and choose **Terraform** from the drop-down.
4. Clone the repository to a local development environment (GitHub's browser text editor is not practical for real work).

Reason for the Terraform `.gitignore`: prevents Terraform's hidden working files from being accidentally committed. It can be added later by copying the source file if missed.

### 4.2 Understanding Terraform

- Terraform does a lot of complicated work to make changes match a declared state, yet it is easy to start with and the language is intuitive.
- **HCL (HashiCorp Configuration Language):** the data format in which Terraform files are written. Invented by HashiCorp.
  - Similar to JSON with some improvements.
  - Does not use a `:` delimiter between key and value. Keys and values are separated by white space or `=` depending on context.
  - Other improvements include comments and multiline strings.
  - Low learning curve for anyone who has used JSON or YAML.

Four key Terraform concepts:

| Concept | Definition | Role in this project |
|---|---|---|
| Backend | The location of the state file that Terraform keeps to know what changes to make; default is the local filesystem | An AWS S3 bucket created earlier |
| Resource | An object that represents a thing whose state is declared; Terraform makes the changes to bring the resource to that state | VPCs, subnets, EKS clusters and similar objects |
| Provider | A packaged library of resources usable in the code; the same tool can work with many cloud platforms and environments by naming the provider | Terraform's AWS provider (later also the Kubernetes and Helm providers) |
| Module | Similar to a function or procedure in a programming language; encapsulates HCL code in a reusable, modular form | Network, Kubernetes and Argo CD modules in Chapter 7 |

### 4.3 Writing the code for the sandbox environment

- Goal for now: set up tooling, not a complete infrastructure definition. A simple starter file tests the Terraform-based tool chain.
- The Terraform CLI looks for recognized files in the working directory. In particular it parses `main.tf` and applies changes based on its contents.
- Only one `main.tf` per directory, so each environment needs its own dedicated directory (here the `env-sandbox` repository).

Contents of the starter `main.tf`:

- A `terraform` block with an S3 backend (bucket, key = `terraform/backend`, region).
- A `locals` block with local variables: `env_name = "sandbox"`, `aws_region`, `k8s_cluster_name = "ms-cluster"`.
- Comments marking where later configuration goes: Network Configuration, EKS Configuration, GitOps Configuration.

The bucket value must be only the bucket name, not the full URL.

```hcl
terraform {
  backend "s3" {
    bucket = "{YOUR_S3_BUCKET_NAME}"
    key    = "terraform/backend"
    region = "{YOUR_AWS_REGION}"
  }
}

locals {
  env_name         = "sandbox"
  aws_region       = "{YOUR_AWS_REGION}"
  k8s_cluster_name = "ms-cluster"
}

# Network Configuration
# EKS Configuration
# GitOps Configuration
```

Terraform CLI quality and safety commands:

| Command | Purpose |
|---|---|
| `terraform fmt` | Formatter (lint): examines the HCL file and improves consistency and readability; prints the name of any file it changed |
| `terraform init` | Installs the providers used and initializes the backend; needed before validation, otherwise Terraform cannot do the syntax check. Prints `Successfully configured the backend "s3"`. Must be rerun if modules or backend configuration change |
| `terraform validate` | Checks that the HCL syntax is valid; prints `Success! The configuration is valid.` |
| `terraform plan` | Dry run: shows the changes Terraform would make to reach the specified state, without making any change. Here it prints `No changes. Infrastructure is up-to-date.` because no resources are defined yet |

- If an AWS credentials error appears, the AWS access configuration done earlier in the chapter must be revisited.
- The valid file is then committed and pushed to GitHub:
  ```bash
  git add .
  git commit -m "The sandbox starter file"
  git push origin
  ```

### 4.4 Building the pipeline

- Aim: an automated CI/CD pipeline that automatically applies the Terraform file.
- Tool: GitHub's built-in DevOps tool, **GitHub Actions**. The pipeline configuration lives in the same place as the infrastructure code.
- Easiest to configure through the browser interface of the sandbox repository.
- The pipeline will create resources in the AWS account, so GitHub must be able to use the AWS access key and secret created for the operator account.

#### 4.4.1 Setting up secrets

Secrets management in microservices can be done in many ways. Here GitHub's built-in secrets storage is used.

1. In the repository, select **Settings** from the top navigation, then **Secrets** from the left menu.
2. Select **Add a new secret** and create `AWS_ACCESS_KEY_ID` with the access key ID of the operator user.
3. Repeat to create `AWS_SECRET_ACCESS_KEY` with the secret access key.

#### 4.4.2 Creating the workflow

- **Workflow:** the set of steps run whenever a pipeline is triggered.
- This workflow validates Terraform files and applies them to the sandbox environment, with extra steps before and after.
- **Trigger:** a **Git tag** (a way of giving a name or label to a particular point in the Git repository history).
  - Gives a versioning history of the changes made to the environment.
  - Allows files to be committed to the repository without triggering a build.
- Setup steps before Terraform runs:
  - Install Terraform and AWS tools as in the local environment (the build runs in a virtual machine).
  - Get a copy of the code from the repository.
- Post-provisioning step: make a special configuration file (`kubeconfig`) available for download so the AWS-based microservices environment can be reached from a local machine.
- Workflows are defined in YAML using GitHub Actions workflow commands.
- Workflow files are kept in the hidden directory `.github/workflows`. Editing in the GitHub website allows searching for plug-ins from the marketplace.
- In the Actions page, use **Set up a workflow yourself** (the provided templates are ignored).

Pipeline steps:

| Order | Pipeline step | Description |
|---|---|---|
| Trigger | New Git tag triggers the build | A tag starting with `v` starts the workflow |
| 1 | Provision machine | An Ubuntu virtual machine is created for the job |
| 2 | Get the code | Check out the Terraform code from the repository |
| 3 | Install tools and set up environment | Install dependencies such as the AWS IAM authenticator and set up Terraform |
| 4 | Format code | `terraform fmt` |
| 5 | Validate code | `terraform init` and `terraform validate` |
| 6 | Create plan | `terraform plan` |
| 7 | Apply changes | `terraform apply` (changes the AWS infrastructure) |
| 8 | Publish config files | Upload the `kubeconfig` file as a downloadable artifact |

#### 4.4.3 Configuring the trigger and setup

```yaml
name: Sandbox Environment Build

on:
  create:
    tags:
      - v*
jobs:
  build:
    runs-on: ubuntu-latest
    env:
      AWS_ACCESS_KEY_ID: ${{ secrets.AWS_ACCESS_KEY_ID }}
      AWS_SECRET_ACCESS_KEY: ${{ secrets.AWS_SECRET_ACCESS_KEY }}
    steps:
    - uses: actions/checkout@v2
    # Install Dependencies
```

- `name`: name of the workflow (Sandbox Environment Build).
- `on`: GitHub Actions command that specifies the trigger. Here the workflow runs when a new tag matching the pattern `v*` is created (for example `v1.0`).
- `jobs`: collection of the work GitHub should do when triggered. Here one job called `build`.
- `runs-on`: jobs run in a machine or container. Here an Ubuntu Linux virtual machine (`ubuntu-latest`).
- `env`: adds the AWS secrets configured earlier to the build environment.
- `steps`: the specific steps performed in the environment. The first step checks out the Terraform code from Git using `actions/checkout@v2`, creating a copy of the code inside the Ubuntu build environment.
- **Action:** a modularized library of code that can be called from a workflow. The GitHub Actions marketplace has a large catalog.
  - Be selective: anyone can publish an action, so support, security and quality are not guaranteed.
- Comments (`# Install Dependencies`) describe what is happening and mark where later YAML is added.

#### 4.4.4 Installing dependencies

- Local setup needed Git, AWS and Terraform command-line tools. The pipeline needs a leaner set.
- **Git:** comes free with GitHub Actions.
- **Terraform:** HashiCorp provides a ready-made GitHub Action, so no manual installation.
- **AWS CLI:** not needed, because no change should be made beyond what is specified in Terraform code.
- Therefore no dependencies are strictly needed to create AWS resources, but later infrastructure needs special dependencies for the Kubernetes-based architecture.
- Dependencies added in the workflow:
  - **AWS IAM authenticator:** a command-line tool other tools use to authenticate and access an AWS environment. Needed later to configure access to an AWS-hosted Kubernetes cluster.
  - **Istio CLI:** Istio is a service mesh tool (introduced in the next chapter). Its CLI tool installer is also needed in the pipeline.
- `run` commands run shell commands in the Ubuntu build environment (for example `mkdir`, `curl` to download the authenticator, `chmod +x`, `sudo cp` to `/usr/local/bin`).
- The virtual machine is created at the start of every pipeline run and destroyed at completion, so tools are installed on every run and no state is retained between runs.
- YAML is very particular about indentation. New steps must line up with the earlier `uses` step.
- In practice the pipeline actions go through several iterations as the infrastructure is tested. The book gives a linear sequence for readability.

#### 4.4.5 Applying Terraform files

```yaml
- uses: hashicorp/setup-terraform@v1
  with:
    terraform_version: 0.12.19
- name: Terraform fmt
  run: terraform fmt
- name: Terraform Init
  run: terraform init
- name: Terraform Validate
  run: terraform validate -no-color
- name: Terraform Plan
  run: terraform plan -no-color
- name: Terraform Apply
  run: terraform apply -no-color -auto-approve
```

- `hashicorp/setup-terraform`: HashiCorp's setup action for Terraform. Cleaner and easier to read than command-line installation, so available actions should be used when they fit.
- `run`: calls the Terraform CLI from the Ubuntu shell. Largely the same as the local steps.
- The apply step makes real changes in AWS infrastructure.
- `-auto-approve` flag: removes the need for human interaction when applying.
- `-no-color`: plain output suitable for pipeline logs.

#### 4.4.6 Publishing assets and committing changes

- When a workflow completes, the VM used for the build is destroyed.
- To keep files for later use, GitHub provides the `upload-artifact` action that makes files available for download.
- Next chapter: a Kubernetes cluster is created on AWS. Connecting from a remote machine needs many connection and authentication details, so a final step publishes a `kubeconfig` file for download.
- The step uploads a file called `kubeconfig` from the build environment's working directory to the GitHub Actions repository. It assumes the file exists, and it is created in Chapter 7.

```yaml
# Publish Assets
- name: Upload kubeconfig file
  uses: actions/upload-artifact@v2
  with:
    name: kubeconfig
    path: kubeconfig
```

- The workflow is now complete. GitHub manages workflow files like code, so changes must be committed: click **Start commit**, enter a description, then **Commit new file**. The file is `.github/workflows/main.yml`.
- Possible extension: a production pipeline would include integration testing of the Terraform code, which is omitted here. The Go-based tool **Terratest** from Gruntwork.io is recommended.

### 4.5 Testing the pipeline

To fire the trigger, a Git tag starting with the letter `v` must be created and pushed. It could be created in the browser with GitHub's Releases feature, but here it is created on the local workstation.

1. Update the local clone: `git pull` in the `env-sandbox` directory (pulls the new `.github/workflows/main.yml` file).
2. Create the tag: `git tag -a v0.1 -m "workflow test"` (the tag exists only locally at this point).
3. Push the tag: `git push origin v0.1` (pushing the tag triggers the workflow).
4. Check the run: in GitHub go to **Actions** and view the status.
5. For details, select the workflow (Sandbox Environment Build), then the job (build) to see what happened at each step. Useful for troubleshooting.

This sequence of tagging and pushing a tag is repeated whenever the pipeline should run.

GitHub changes its UI frequently, so exact navigation steps may differ; refer to GitHub documentation.

### 4.6 Chapter 6 summary

- A simple but powerful IaC pipeline was set up using DevOps principles and practices.
- Terraform implements the principles of IaC and immutable infrastructure.
- A GitHub-based repository manages the infrastructure code.
- A GitHub Actions workflow acts as the CI/CD pipeline with automated testing to improve the safety and speed of infrastructure changes.
- No infrastructure resources were created, but the steps of making an infrastructure change were demonstrated:
  1. Create and edit a Terraform file.
  2. Test and run it locally.
  3. Commit it to the repository.
  4. Tag it to start the build and apply pipeline.
- This sequence is the method for immutable infrastructure development and is used often in Chapter 7.

---

# PART 2: BUILDING A MICROSERVICES INFRASTRUCTURE

## 5. Infrastructure Components

### 5.1 Overview

- **Infrastructure:** the set of components that allow a microservices-based application to be deployed, managed and supported.
- It can include hardware, software, networks and tools, so the scope is large.

Approach:

- Use tools and services as much as possible (their number has grown as microservices matured).
- Work with a single cloud platform (AWS) rather than a cloud-agnostic application that can be "lifted and shifted" to other hosts. This makes a feature-rich infrastructure possible in one chapter.

Trade-offs: security, operations controls, and event logging and support are not covered.

Focus: Terraform code to create three components that form the foundation for deploying the example microservices:

1. A working network
2. An AWS managed Kubernetes service
3. A declarative GitOps server

Network design and Kubernetes are deep topics. The instructions allow a first environment to be set up without expert knowledge.

| Layer | Component | AWS / tool used | Purpose |
|---|---|---|---|
| 1 (base) | Network | AWS VPC with four subnets, routing and security | Virtual network on which everything runs |
| 2 | Kubernetes service | AWS EKS (managed Kubernetes) | Orchestrates the containers of the microservices |
| 3 (top) | GitOps deployment server | Argo CD (installed with Helm) | Deploys microservices from Git into the cluster |

### 5.2 The network

- Microservices run on a network. Because services are hosted on AWS, a virtual network is created instead of a physical one.
- No need to deal with physical routers, cables or network devices. The language of AWS network resources is used instead.
- The design is kept as simple as possible, but a few basic resources are needed.

| Network resource | Description |
|---|---|
| Virtual private cloud (VPC) | In AWS, the parent object of a virtual network; created and configured as part of the network design |
| Subnets | Smaller networks into which a VPC is partitioned; they organize network traffic and control access to resources; four subnets are created |
| Routing and security | Objects that dictate how traffic flows in and out of the VPC and subnets; for example, two private subnets accept traffic only from inside the VPC |

The complexity (four subnets and their connections) is driven mainly by the needs of the Kubernetes service that runs on top of the network.

### 5.3 The Kubernetes service

#### Containers and why they are used

- Reducing coordination costs is an important success factor for the system. Containers help teams do more with less coordination.
- Containers give applications a predictable, isolated system configuration without the overhead and heavy lifting of a VM deployment.
- Containers make microservices run predictably across environments as a self-contained unit. Microservices and containers are a natural fit.
- **Limitation:** containers cannot start, scale or heal themselves, and need a lot of operations work in production-like environments.

#### Kubernetes

- **Definition:** a container orchestration tool developed by Google that solves the problems of working with containers at scale.
- Provides a tool-based solution for deploying, scaling, observing and managing container-based applications.

Capabilities:

- Roll out and roll back container deployments.
- Automatically deploy or destroy container deployments based on demand patterns.
- Mount storage systems.
- Manage secrets.
- Help with load balancing and traffic management.

It has become an essential part of the microservices infrastructure stack, but it is complex itself.

| Kubernetes component | Description |
|---|---|
| Cluster | The parent object of a Kubernetes system; installing Kubernetes means installing a cluster; a cluster contains a control plane and a set of nodes |
| Control plane | The "brains" of the cluster; manages the system by deciding on starting, stopping and replicating containers; provides an API to administrate the cluster |
| Nodes | Where the runtime work happens; each node is a physical or virtual machine that runs the container-based workload; nodes run pods; every cluster has at least one node |
| Pod | A deployable unit that contains one or more containers (container images) |

#### Elastic Kubernetes Service (EKS)

- EKS is the AWS managed service for Kubernetes.
- It handles much of Kubernetes' complexity: helps provision the cluster and provides a control plane as a managed service.
- What remains to be done: configure the number and types of nodes and provide a suitable network.

**Key decision:** Use a managed Kubernetes service. AWS EKS is used as a managed service for the Kubernetes cluster.

### 5.4 The GitOps deployment server

Context: the release team is responsible for deploying microservices into production.

- Microservices teams use a CI/CD pipeline to integrate, test, build and deliver their services, but they do not own the actual deployment into the system.
- Reason: production deployments are fairly complex and need special attention.
- To help the release team, a special tool is added to the platform: a **GitOps deployment server**.

| Term | Meaning in this model |
|---|---|
| Continuous Delivery (CD) | Teams automatically and continually ship their finished microservices as containers |
| Continuous Deployment | The containers are released into the production environment by the release team |

Note: "CD" in CI/CD can mean either Continuous Delivery or Continuous Deployment depending on who is asked. The model uses the distinction above.

#### GitOps

- The term was created by a company called WeaveWorks.
- **Definition:** a way of working that uses Git as the source of truth. Whatever is in Git is the target state of the system.
- Like Terraform, GitOps prescribes a declarative approach.
- Responsibilities of GitOps tools:
  - Synchronize system configurations so that they look like the state described in the Git repository.
  - Alert operations teams if the real world has drifted from the state defined in Git.

#### Argo CD

- A GitOps tool that facilitates the work of deploying Kubernetes applications.
- Chosen because of the declarative GitOps approach.
- Without it, a series of Kubernetes calls would have to be automated to deploy an application. With it, Argo CD is pointed at a Git source and keeps the environment up to date.
- Example: Argo CD watches a microservice code repository. When new changes are committed and tested, it automatically deploys the new version into the environment.
- This declarative, continuous deployment capability makes it suitable for release teams.

**Key decision:** Deploy microservices using a GitOps deployment tool. Release teams use Argo CD to manage microservice deployment into production and production-like environments.

End of the chapter: a sandbox environment with an Argo CD server installed on an AWS managed Kubernetes cluster, running on an AWS VPC network, built with a large amount of Terraform code.

---

## 6. Implementing the Infrastructure

- Chapter 6 decided to use Terraform for infrastructure definition and GitHub Actions for testing and applying changes.
- Here the infrastructure design is broken into discrete Terraform modules that are called from the sandbox environment started in Chapter 6.

### 6.1 Installing kubectl

Prerequisites from Chapter 6:

- An AWS instance and a configured operator account.
- Git, Terraform and AWS CLI tools on the workstation.
- A GitHub Actions pipeline for the infrastructure (if missing, a basic sandbox environment can be forked from the book's GitHub repository).

Additional tool: **kubectl**, the command-line application used to test and interact with a Kubernetes server. Install it by following the Kubernetes documentation for the operating system in use.

### 6.2 Setting up the module repositories

- Infrastructure code must be clean and professional like application code. Code that is difficult to understand, maintain or change makes the project costly.
- Not every software development principle is easy to apply to IaC, because tooling and languages are still evolving and changing a live, physical device is a different model.

Three coding practices supported by Terraform modules:

| Practice | Meaning |
|---|---|
| Use modules | Write small functions that do one thing well |
| Encapsulate | Hide internal data structures and implementation details |
| Avoid repetition (DRY) | Don't Repeat Yourself: implement code once, in only one location |

- Infrastructure code is maintained as a set of reusable, encapsulated modules.
- Modules are built for each architecturally significant part: networks, the API gateway and the managed Amazon Kubernetes service (EKS).
- A separate set of Terraform files then uses the modules, with a different file for each environment, without repeating the same infrastructure declarations.

Reusing a network module:

- One Network module is defined once.
- The Sandbox environment code and the Production environment code each contain a reference to the same network module.
- Benefits:
  - New environments are easily created by writing new Terraform files that reuse the modules.
  - A change made in one place (the module) changes the configuration across all environments.

Module structure:

- Each module has its own directory and contains `variables.tf`, `main.tf` and `output.tf` (called `outputs.tf` in the examples).
- A module is defined once and used in a parameterized way to build multiple environments.
- A module is like an application function:
  - `variables.tf` = inputs
  - `main.tf` = logic
  - `outputs.tf` = return values

| Module file | Role | Analogy |
|---|---|---|
| `variables.tf` | Declares all input variables of the module | Function parameters |
| `main.tf` | Declaratively defines the resources | Function body / logic |
| `outputs.tf` | Defines the significant resources/values the module returns | Return values |

Note: do not use the book's configuration files in a production environment. They mirror large-organization setups but cannot cover specific environment, security needs and constraints. A production-grade infrastructure should be designed separately.

Repositories created:

- The sandbox repository from Chapter 6 is reused. In addition, a new repository is created for each module.
- Terraform has built-in support for importing modules managed as GitHub repositories.
- Repositories are made public so they are easier to import. Private repositories can be used but need authentication information in the import command.
- A `.gitignore` is added to each repository (choose the Terraform `.gitignore` in the GitHub GUI) to avoid pushing Terraform working files.

| Repository name | Visibility | Description |
|---|---|---|
| `module-aws-network` | Public | A Terraform module that creates the network |
| `module-aws-kubernetes` | Public | A Terraform module that sets up EKS |
| `module-argo-cd` | Public | A Terraform module that installs Argo CD into a cluster |

### 6.3 The network module

- The virtual network is the foundation of the infrastructure, so it is defined first.
- The module supports a specific Kubernetes and microservices architecture and workload.
- It is written in the `module-aws-network` repository. Once complete, a network is provisioned by giving just a few input values.

#### 6.3.1 Network module outputs (`output.tf`)

```hcl
output "vpc_id" { value = aws_vpc.main.id }
output "subnet_ids" {
  value = [aws_subnet.public-subnet-a.id, aws_subnet.public-subnet-b.id,
           aws_subnet.private-subnet-a.id, aws_subnet.private-subnet-b.id]
}
output "public_subnet_ids" { value = [public-subnet-a.id, public-subnet-b.id] }
output "private_subnet_ids" { value = [private-subnet-a.id, private-subnet-b.id] }
```

- The module creates a VPC representing the software-defined network.
- Within the VPC it creates four logical subnets (bounded parts of the network):
  - Two are public: accessible over the internet.
  - Two are private.
  - All four are later used for the Kubernetes cluster setup and eventually the microservices.
- Outputs: `vpc_id`, `subnet_ids` (all four), `public_subnet_ids` (two), `private_subnet_ids` (two).

#### 6.3.2 Network module main configuration (`main.tf`)

**(a) Provider, locals and VPC**

```hcl
provider "aws" {
  region = var.aws_region
}

locals {
  vpc_name     = "${var.env_name} ${var.vpc_name}"
  cluster_name = "${var.cluster_name}-${var.env_name}"
}

resource "aws_vpc" "main" {
  cidr_block = var.main_vpc_cidr
  tags = {
    "Name" = local.vpc_name,
    "kubernetes.io/cluster/${local.cluster_name}" = "shared",
  }
}
```

- `provider "aws"`: tells Terraform to download and install the libraries needed to communicate with the AWS API and create resources. It uses the credentials configured as environment variables; the region tells Terraform where to work.
- `locals`: two local variables that set a naming standard to tell environment resources apart in the AWS console. Important when several environments exist in the same AWS account (avoids naming collisions).
- `aws_vpc` resource: defines a CIDR block and a set of tags.
- **CIDR (Classless Inter-Domain Routing):** a standard way of describing an IP address range for a network. A shorthand string that defines which IP addresses are allowed inside a network or subnet.
  - Example: `10.0.0.0/16` allows any IP address from `10.0.0.0` to `10.0.255.255` inside the VPC.
- **Tags:** identify groups of resources for administration and automated tasks, and show which resources are managed in specific ways.
  - `Name` tag makes the VPC easier to identify.
  - The Kubernetes tag identifies the VPC as a target for the Kubernetes cluster.
- `var.xxx` and `local.xxx`: references to Terraform variables instead of fixed values (for example `var.main_vpc_cidr`, `local.vpc_name`). Values are given when the module is used, which makes the module reusable.

**(b) Subnets**

- EKS needs subnets in two availability zones.
  - **Availability zone:** a separate physical data center in AWS.
  - Using two zones keeps services working even if one data center goes down.
- Amazon recommends a VPC with both public and private subnets.
  - Public subnets allow traffic from the internet.
  - Private subnets allow traffic only from inside the VPC.
- When EKS runs, it deploys load balancers in the public subnets to manage inbound traffic, which is routed to the containerized microservices deployed in the private subnets.
- Result: four subnets, with public and private subnets split across the two availability zones.

| Subnet | Type | Availability zone | Role tag | Meaning of the role tag |
|---|---|---|---|---|
| `public-subnet-a` | Public | `names[0]` | `kubernetes.io/role/elb = "1"` | EKS may create and deploy an elastic load balancer here |
| `public-subnet-b` | Public | `names[1]` | `kubernetes.io/role/elb = "1"` | EKS may create and deploy an elastic load balancer here |
| `private-subnet-a` | Private | `names[0]` | `kubernetes.io/role/internal-elb = "1"` | Workloads are deployed here and can be load balanced internally |
| `private-subnet-b` | Private | `names[1]` | `kubernetes.io/role/internal-elb = "1"` | Workloads are deployed here and can be load balanced internally |

- Subnet CIDR blocks must be within the VPC IP range and are passed in as variables (not defined in the module).
- Availability zones are not hardcoded. The Terraform data element `data.aws_availability_zones.available.names[0]` and `[1]` is used.
  - `data`: a way of querying the provider for information. Here it asks AWS for the availability zone names in the chosen region.
  - Avoids hardcoded values and makes it easy to create the infrastructure in different regions.
- Every subnet gets a `Name` tag, the Kubernetes cluster tag (`shared`) and the EKS role tag described above.

```hcl
data "aws_availability_zones" "available" { state = "available" }

resource "aws_subnet" "public-subnet-a" {
  vpc_id            = aws_vpc.main.id
  cidr_block        = var.public_subnet_a_cidr
  availability_zone = data.aws_availability_zones.available.names[0]
  tags = {
    "Name" = "${local.vpc_name}-public-subnet-a"
    "kubernetes.io/cluster/${local.cluster_name}" = "shared"
    "kubernetes.io/role/elb" = "1"
  }
}
# public-subnet-b (names[1]), private-subnet-a (names[0]) and
# private-subnet-b (names[1]) follow the same pattern; private subnets use
# the tag "kubernetes.io/role/internal-elb" = "1"
```

**(c) Routing for public subnets**

- Subnets and IP ranges alone do not manage traffic. Routing tables define which traffic sources are allowed into the subnets.
- **Internet gateway:** an AWS network component that connects the private cloud (VPC) to the public internet. Tied to the VPC with `vpc_id`.
- **Route table** (`aws_route_table`): a rule allowing all traffic from the internet (CIDR `0.0.0.0/0`) through the gateway.
- **Route table association** (`aws_route_table_association`): links each public subnet to the route table.

```hcl
resource "aws_internet_gateway" "igw" { vpc_id = aws_vpc.main.id }

resource "aws_route_table" "public-route" {
  vpc_id = aws_vpc.main.id
  route {
    cidr_block = "0.0.0.0/0"
    gateway_id = aws_internet_gateway.igw.id
  }
}

resource "aws_route_table_association" "public-a-association" {
  subnet_id      = aws_subnet.public-subnet-a.id
  route_table_id = aws_route_table.public-route.id
}
# public-b-association is defined the same way for public-subnet-b
```

**(d) Routing for private subnets (NAT gateway)**

- Private subnets need a route out to the internet so that the Kubernetes Pods can talk to the EKS service.
- Nodes in a private subnet cannot reach the internet gateway directly, so a **NAT (Network Address Translation) gateway** is created to give a path out.
- Each NAT gateway needs an **Elastic IP address (EIP)**.
  - An EIP is a real, internet-accessible address, unlike the other virtual addresses that exist only inside AWS.
  - AWS limits the number available. A NAT cannot be created without one, so two EIPs are used (one per NAT).
- Two NAT gateways are created, one in each public subnet, with `depends_on` the internet gateway.
- Each private subnet gets its own route table that sends `0.0.0.0/0` through its NAT gateway (`nat_gateway_id`), plus an association to the private subnet.

```hcl
resource "aws_eip" "nat-a" { vpc = true }                   # nat-b is the same

resource "aws_nat_gateway" "nat-gw-a" {
  allocation_id = aws_eip.nat-a.id
  subnet_id     = aws_subnet.public-subnet-a.id
  depends_on    = [aws_internet_gateway.igw]
}

resource "aws_route_table" "private-route-a" {
  vpc_id = aws_vpc.main.id
  route {
    cidr_block     = "0.0.0.0/0"
    nat_gateway_id = aws_nat_gateway.nat-gw-a.id
  }
}

resource "aws_route_table_association" "private-a-association" {
  subnet_id      = aws_subnet.private-subnet-a.id
  route_table_id = aws_route_table.private-route-a.id
}
# nat-gw-b, private-route-b and private-b-association follow the same pattern
```

Summary of network resources:

| Resource (Terraform type) | Count | Purpose |
|---|---|---|
| VPC (`aws_vpc`) | 1 | Parent object of the virtual network; has a CIDR block (`10.10.0.0/16` in the sandbox) and tags |
| Subnet (`aws_subnet`) | 4 | 2 public + 2 private, spread over two availability zones |
| Internet gateway (`aws_internet_gateway`) | 1 | Connects the VPC to the public internet |
| Public route table (`aws_route_table`) and associations | 1 table, 2 associations | Routes `0.0.0.0/0` from the internet gateway to the public subnets |
| Elastic IP (`aws_eip`) | 2 | Real public IPs required by the NAT gateways |
| NAT gateway (`aws_nat_gateway`) | 2 | Lets private subnets reach the internet (outbound) |
| Private route tables and associations | 2 tables, 2 associations | Route `0.0.0.0/0` from each private subnet through its NAT gateway |

| Feature | Public subnet | Private subnet |
|---|---|---|
| Accessibility | Accessible over the internet | Accepts traffic only from inside the VPC |
| Gateway used | Internet gateway (route `0.0.0.0/0`) | NAT gateway (route `0.0.0.0/0` for outbound access) |
| EKS role tag | `kubernetes.io/role/elb` | `kubernetes.io/role/internal-elb` |
| What runs there | EKS load balancers for inbound traffic and the NAT gateways | Containerized microservices / Kubernetes worker nodes |

#### 6.3.3 Network module variables (`variables.tf`)

- Terraform modules require all input variables to be declared in `variables.tf`. Without this, values cannot be passed into the module.
- Each variable has a name, an optional description and a type. Some have a default so they need not be set for every environment.
- All variables in this module are of type `string`.
- Good practice: include a description for every variable to improve maintainability and usability (omitted in the book to save space).

| Variable | Type | Default | Purpose |
|---|---|---|---|
| `env_name` | string | none | Name of the environment (for example `sandbox`) |
| `aws_region` | string | none | AWS region to work in |
| `vpc_name` | string | `"ms-up-running"` | Name used for the VPC |
| `main_vpc_cidr` | string | none | CIDR range of the VPC |
| `public_subnet_a_cidr` | string | none | CIDR of public subnet A |
| `public_subnet_b_cidr` | string | none | CIDR of public subnet B |
| `private_subnet_a_cidr` | string | none | CIDR of private subnet A |
| `private_subnet_b_cidr` | string | none | CIDR of private subnet B |
| `cluster_name` | string | none | Kubernetes cluster name, used for tagging |

Validating and publishing the module:

- Final module directory contains `main.tf`, `variables.tf`, `outputs.tf` (and `README.md`, `.git`).
- `terraform fmt`: lints and formats the code and lists updated files.
- `terraform init`: installs the AWS provider libraries (AWS credentials are needed).
- `terraform validate`: checks syntax; prints `Success! The configuration is valid.`
- Debugging: set environment variable `TF_LOG` to `INFO` or `DEBUG` to send logging information to standard output.
- Publish:
  ```bash
  git add .
  git commit -m "network module created"
  git push
  ```

#### 6.3.4 Creating a sandbox network

- Using modules, environments are created in a repeatable way. Apart from the values in `variables.tf`, every environment has a network infrastructure that is known and understood, so microservices behave predictably as they move through test and release environments (reduced variation).
- To use the module, the sandbox `main.tf` (from Chapter 6) calls it with a special Terraform resource called `module`.
  - The `source` property tells Terraform where to find the code.
  - For a GitHub-hosted module the source starts with `github.com` followed by the repository path (for example `github.com/implementing-microservices/module-aws-network`).
  - Values are passed for the module's variables.

```hcl
# Network Configuration
module "aws-network" {
  source = "github.com/{YOUR_NETWORK_MODULE_REPO_PATH}"

  env_name              = local.env_name
  vpc_name              = "msur-VPC"
  cluster_name          = local.k8s_cluster_name
  aws_region            = local.aws_region
  main_vpc_cidr         = "10.10.0.0/16"
  public_subnet_a_cidr  = "10.10.0.0/18"
  public_subnet_b_cidr  = "10.10.64.0/18"
  private_subnet_a_cidr = "10.10.128.0/18"
  private_subnet_b_cidr = "10.10.192.0/18"
}
```

Steps to build the network:

1. Run `terraform fmt`, `terraform init` and `terraform validate` locally.
2. Run `terraform plan` (always do a dry run and examine the changes before changing the environment). It lists resources to be created, deleted and updated. The first plan lists many new resources.
3. Push the code and the tag:
   ```bash
   git add .
   git commit -m "initial network release"
   git push origin
   git tag -a v1.0 -m "network build"
   git push origin v1.0
   ```
4. The GitHub Actions pipeline builds the network. Check the Actions tab of the sandbox repository.
5. Verify the VPC:
   ```bash
   aws ec2 describe-vpcs --filters Name=cidr,Values=10.10.0.0/16
   ```
   A JSON description of the VPC means the network is running.

- Two `git push` commands are needed: the first pushes the code changes, the second pushes only the tag.
- If the network module is changed during debugging, run `terraform get -update` in the sandbox directory to pull the latest version of the module from GitHub.

### 6.4 The Kubernetes module

- The Kubernetes layer orchestrates the container-based services. Set up correctly, it gives an automated solution for resiliency, scaling and fault tolerance and a dependable foundation for deployment.
- An Istio service mesh on top gives a powerful way of managing traffic and improving communication between microservices.
- Steps are the same as for the network module: outputs, then main configuration, then variables, in its own repository (`module-aws-kubernetes`).
- A managed service (EKS) is used to hide some setup and management complexity.
- Managed Kubernetes configuration is vendor specific. The examples need reworking for Google Cloud, Azure or other hosts.
- An EKS cluster has two parts:
  - **Control plane:** hosts the Kubernetes system software (created automatically by AWS).
  - **Node group:** hosts the VMs on which the microservices run.
- When the module finishes, an EKS cluster identifier is returned so the cluster can be inspected or extended by other modules.

#### 6.4.1 Kubernetes module outputs (`outputs.tf`)

| Output | Value returned |
|---|---|
| `eks_cluster_id` | ID of the EKS cluster (main value returned) |
| `eks_cluster_name` | Name of the cluster |
| `eks_cluster_certificate_data` | Certificate authority data of the cluster |
| `eks_cluster_endpoint` | Endpoint (server address) of the cluster |
| `eks_cluster_nodegroup_id` | ID of the node group |

The extra values allow the cluster to be accessed from other modules. For example, the endpoint and certificate data are needed when Argo CD is installed into the cluster.

#### 6.4.2 Kubernetes module main configuration (`main.tf`)

**(a) Provider and cluster access management (IAM role and policy)**

```hcl
provider "aws" { region = var.aws_region }

locals { cluster_name = "${var.cluster_name}-${var.env_name}" }

resource "aws_iam_role" "ms-cluster" {
  name = local.cluster_name
  assume_role_policy = <<POLICY
{ "Version": "2012-10-17",
  "Statement": [{ "Effect": "Allow",
    "Principal": { "Service": "eks.amazonaws.com" },
    "Action": "sts:AssumeRole" }] }
POLICY
}

resource "aws_iam_role_policy_attachment" "ms-cluster-AmazonEKSClusterPolicy" {
  policy_arn = "arn:aws:iam::aws:policy/AmazonEKSClusterPolicy"
  role       = aws_iam_role.ms-cluster.name
}
```

- EKS must create and modify AWS resources on the user's behalf, so permissions are needed at the cluster level and for the nodes (VMs).
- The IAM role defines a trust policy that allows the AWS EKS service (`eks.amazonaws.com`) to act on the user's behalf (`sts:AssumeRole`).
- The attached policy `AmazonEKSClusterPolicy` is defined by AWS and gives EKS the permissions to create VMs and make network changes as part of Kubernetes management.
- A local variable holds the cluster name and is used throughout the module.

**(b) Network security policy (security group)**

```hcl
resource "aws_security_group" "ms-cluster" {
  name   = local.cluster_name
  vpc_id = var.vpc_id
  egress {
    from_port   = 0
    to_port     = 0
    protocol    = "-1"
    cidr_blocks = ["0.0.0.0/0"]
  }
  tags = { Name = "ms-up-running" }
}
```

- A VPC **security group** restricts the kind of traffic going into and out of the network.
- This definition has an egress rule allowing unrestricted outbound traffic.
- There is no ingress rule, so no inbound traffic is allowed.
- It is applied to a VPC identified by an input variable. The VPC ID produced by the network module is supplied when the module is used.

**(c) EKS cluster definition**

```hcl
resource "aws_eks_cluster" "ms-up-running" {
  name     = local.cluster_name
  role_arn = aws_iam_role.ms-cluster.arn
  vpc_config {
    security_group_ids = [aws_security_group.ms-cluster.id]
    subnet_ids         = var.cluster_subnet_ids
  }
  depends_on = [aws_iam_role_policy_attachment.ms-cluster-AmazonEKSClusterPolicy]
}
```

- References the name, role, policy and security group defined earlier, and a set of subnets the cluster manages (the subnets from the network module, passed in as a variable).
- `depends_on`: ensures the policy attachment exists before the cluster is created.
- When AWS creates an EKS cluster it automatically sets up all management components. This is the control plane, the "brain" of the Kubernetes system.
- Microservices also need nodes (physical or virtual machines) to run on.
- A managed service offloads the creation, removal and updating of nodes. A managed EKS node group is defined and AWS provisions resources and interacts with Kubernetes.

**(d) EKS node group (IAM role, policies and node group)**

```hcl
resource "aws_iam_role" "ms-node" {
  name = "${local.cluster_name}.node"
  assume_role_policy = <<POLICY   # trusts "ec2.amazonaws.com"
  ...
}
# Policies attached to the node role:
#   AmazonEKSWorkerNodePolicy, AmazonEKS_CNI_Policy,
#   AmazonEC2ContainerRegistryReadOnly

resource "aws_eks_node_group" "ms-node-group" {
  cluster_name     = aws_eks_cluster.ms-up-running.name
  node_group_name  = "microservices"
  node_role_arn    = aws_iam_role.ms-node.arn
  subnet_ids       = var.nodegroup_subnet_ids
  scaling_config {
    desired_size = var.nodegroup_desired_size
    max_size     = var.nodegroup_max_size
    min_size     = var.nodegroup_min_size
  }
  disk_size      = var.nodegroup_disk_size
  instance_types = var.nodegroup_instance_types
  depends_on = [ ...the three node policy attachments... ]
}
```

- The node role (trusting the EC2 service `ec2.amazonaws.com`) and its policies allow nodes to communicate with Amazon's container registries and VM services.
- Nodes need these policies to provision compute resources and access containers in order to run services.
- A managed node group must specify the types of compute and storage it uses and limits on the number of nodes/VMs created automatically.
- The limits are important because EKS automatically provisions and scales nodes. Without limits a massive AWS bill could result.
- Size limits, disk size and CPU (instance) types are input variables rather than hardcoded values, so the same module creates different environments (minimal development environment, robust production environment).

| Node policy | Purpose |
|---|---|
| `AmazonEKSWorkerNodePolicy` | Allows worker nodes to connect to EKS clusters |
| `AmazonEKS_CNI_Policy` | Networking (CNI plug-in) permissions for the nodes |
| `AmazonEC2ContainerRegistryReadOnly` | Lets nodes pull container images from Amazon's container registry |

**(e) Generating the kubeconfig file**

- The module outputs return connection values, but a configuration file for `kubectl` is also useful because most operators use it for Kubernetes management.
- A Terraform `local_file` resource creates a file named `kubeconfig` and fills it with YAML connection parameters.
- The YAML includes: cluster certificate authority data, server endpoint, cluster name (ARN), contexts, current context and users. The user entry runs `aws-iam-authenticator token -i <cluster name>`.
- All values come from the EKS resources created in the module.
- When Terraform runs, `kubeconfig` is created in a local directory. The pipeline step from Chapter 6 (`upload-artifact`) makes it downloadable so the cluster can be reached from a local machine.

#### 6.4.3 Kubernetes module variables (`variables.tf`)

| Variable | Type | Default | Meaning |
|---|---|---|---|
| `aws_region` | string | `eu-west-2` | AWS region |
| `env_name` | string | none | Environment name |
| `cluster_name` | string | none | Kubernetes cluster name |
| `ms_namespace` | string | `"microservices"` | Namespace for the microservices |
| `vpc_id` | string | none | VPC in which the cluster is created (from the network module) |
| `cluster_subnet_ids` | list(string) | none | Subnets managed by the cluster |
| `nodegroup_subnet_ids` | list(string) | none | Subnets in which the nodes are placed |
| `nodegroup_desired_size` | number | 1 | Desired number of nodes |
| `nodegroup_min_size` | number | 1 | Minimum number of nodes |
| `nodegroup_max_size` | number | 5 | Maximum number of nodes |
| `nodegroup_disk_size` | string | none | Disk size of the nodes |
| `nodegroup_instance_types` | list(string) | none | Instance (CPU) types of the nodes |

Then run `terraform fmt`, `terraform init`, `terraform validate`, and commit and push to GitHub:

```bash
git add .
git commit -m "kubernetes module complete"
git push origin
```

#### 6.4.4 Creating a sandbox Kubernetes cluster

- The sandbox `main.tf` calls the module after the `# EKS Configuration` comment.
- Default values are used where possible. The network module's outputs are passed in so the cluster is installed on the network just created.
- `aws_region` must match the region used for the network module and the backend. `source` points to the GitHub-hosted module.

```hcl
# EKS Configuration
module "aws-eks" {
  source = "github.com/{YOUR_EKS_MODULE_PATH}"

  ms_namespace       = "microservices"
  env_name           = local.env_name
  aws_region         = local.aws_region
  cluster_name       = local.k8s_cluster_name
  vpc_id             = module.aws-network.vpc_id
  cluster_subnet_ids = module.aws-network.subnet_ids

  nodegroup_subnet_ids     = module.aws-network.private_subnet_ids
  nodegroup_disk_size      = "20"
  nodegroup_instance_types = ["t3.medium"]
  nodegroup_desired_size   = 1
  nodegroup_min_size       = 1
  nodegroup_max_size       = 3
}
```

- Cluster subnets = all four subnets (`subnet_ids`). Node group subnets = private subnets only (`private_subnet_ids`).
- Commit, push and tag to start the pipeline:
  ```bash
  git add .
  git commit -m "initial k8s release"
  git push
  git tag -a v1.1 -m "k8s build"
  git push origin v1.1
  ```
- Provisioning an EKS cluster can take 10 to 15 minutes.
- **Cost warning:** the EKS cluster accrues charges even when idle, so the environment should be destroyed when not in use.
- Test: `aws eks list-clusters` should list `ms-cluster-sandbox`.

### 6.5 Setting up Argo CD

- The infrastructure is completed with a GitOps server used later in the book.
- The module pattern is followed: a Terraform module for Argo CD that bootstraps the server in the sandbox environment.
- Unlike the earlier modules, Argo CD is installed on the Kubernetes system just created, not on the AWS platform.
- So Terraform uses the **Kubernetes provider** (to issue Kubernetes commands and install the application on the cluster) and the **Helm provider**, instead of the AWS provider.

#### 6.5.1 Providers (`main.tf` of `module-argo-cd`)

```hcl
provider "kubernetes" {
  load_config_file       = false
  cluster_ca_certificate = base64decode(var.kubernetes_cluster_cert_data)
  host                   = var.kubernetes_cluster_endpoint
  exec {
    api_version = "client.authentication.k8s.io/v1alpha1"
    command     = "aws-iam-authenticator"
    args        = ["token", "-i", "${var.kubernetes_cluster_name}"]
  }
}

provider "helm" {
  kubernetes {   # same connection settings as above
    ...
  }
}
```

- The Kubernetes provider is configured with the properties of the provisioned EKS cluster: endpoint (`host`), certificate and the AWS authenticator command used to connect.
- **Helm:** a popular way of describing a Kubernetes deployment and of distributing Kubernetes applications as packages. Similar to package managers such as `apt-get` in Linux. Makes installation of Kubernetes applications simple.
- A Helm deployment is called a **chart**. A chart provided by the Argo CD community is used to install the Argo CD server.
- The Helm provider needs only a few Kubernetes connection parameters.

#### 6.5.2 Helm installation of Argo CD

```hcl
resource "kubernetes_namespace" "example" {
  metadata { name = "argo" }
}

resource "helm_release" "argocd" {
  name       = "msur"
  chart      = "argo-cd"
  repository = "https://argoproj.github.io/argo-helm"
  namespace  = "argo"
}
```

- Creates a namespace (`argo`) for the Argo CD installation.
- Uses the Helm provider (`helm_release`) to install the `argo-cd` chart from the Argo project's Helm repository.

#### 6.5.3 Variables for Argo CD (`variables.tf`)

| Variable (all string) | Source |
|---|---|
| `kubernetes_cluster_id` | Output `eks_cluster_id` of the Kubernetes module |
| `kubernetes_cluster_cert_data` | Output `eks_cluster_certificate_data` |
| `kubernetes_cluster_endpoint` | Output `eks_cluster_endpoint` |
| `kubernetes_cluster_name` | Output `eks_cluster_name` |
| `eks_nodegroup_id` | Output `eks_cluster_nodegroup_id` |

- These variables configure the Kubernetes and Helm providers. Their values come from the Kubernetes module's outputs when it is called from the sandbox's Terraform file.
- Then format, validate (`terraform fmt`, `init`, `validate`), commit and push the module:
  ```bash
  git commit -m "ArgoCD module init"
  ```

#### 6.5.4 Installing Argo CD in the sandbox

```hcl
# GitOps Configuration
module "argo-cd-server" {
  source = "github.com/{YOUR_ARGOCD_MODULE_PATH}"

  kubernetes_cluster_id        = module.aws-eks.eks_cluster_id
  kubernetes_cluster_name      = module.aws-eks.eks_cluster_name
  kubernetes_cluster_cert_data = module.aws-eks.eks_cluster_certificate_data
  kubernetes_cluster_endpoint  = module.aws-eks.eks_cluster_endpoint
  eks_nodegroup_id             = module.aws-eks.eks_cluster_nodegroup_id
}
```

- Added at the end of the sandbox `main.tf` so Argo CD is installed as part of sandbox bootstrapping.
- Tag, commit and push to start the pipeline:
  ```bash
  git add .
  git commit -m "initial ArgoCD release"
  git push origin
  git tag -a v1.2 -m "ArgoCD build"
  git push origin v1.2
  ```
- Wait for the EKS build to complete before tagging. Otherwise there is no Kubernetes cluster to deploy Argo CD to.
- After the pipeline finishes there is a GitOps server that makes deploying microservices easier and more reliable. The sandbox environment is now defined and provisioned.

### 6.6 Testing the environment

- Aim: verify the environment was provisioned as expected. Being able to reach Argo CD proves the entire stack is running (network, EKS, Argo CD).
- `kubectl` must first be set up using the `kubeconfig` file created by the Kubernetes module.

1. In the sandbox GitHub repository open the Actions tab, select the latest build and click the `kubeconfig` artifact to download it (packaged as a ZIP, so decompress it).
2. Point `kubectl` to the file with the environment variable:
   ```bash
   export KUBECONFIG=~/Downloads/kubeconfig
   ```
   Alternatively copy it to `~/.kube/config`, taking care not to overwrite an existing Kubernetes configuration.
3. Test the cluster:
   ```bash
   kubectl get svc
   ```
   It lists the `kubernetes` service (type ClusterIP, port 443/TCP), showing the network and EKS were provisioned and the connection to the cluster works (`kubectl` makes an API call to the cluster).
4. Check Argo CD:
   ```bash
   kubectl get pods -n "argo"
   ```
   The Argo CD pods should be in Running status.

| Argo CD pod (prefix `msur-argocd-`) | Count / Status |
|---|---|
| application-controller | 1/1 Running |
| dex-server | 1/1 Running |
| redis | 1/1 Running |
| repo-server | 1/1 Running |
| server | 1/1 Running |

- **Pod:** a deployable unit consisting of one or more container images.
- After testing, the whole environment can be torn down. With the code written it is easy to create the environment again.

### 6.7 Cleaning up the infrastructure

- Reason: avoid costs when the infrastructure is not in use. In particular the elastic IP addresses can be costly if left running.
- Destroying is low risk because the environment is completely defined in Terraform files and can be re-created whenever needed.
- Terraform destroys resources in the correct order because it internally builds a dependency graph.

Steps:

1. Go to the sandbox working directory (the same one used to install Argo CD).
2. Pull the latest code: `git pull`. Then install the providers used by the code: `terraform init`.
3. Run `terraform destroy`.
4. Terraform lists the resources to be destroyed. Answer `yes` to continue. It takes about five minutes.
5. On success the message is `Destroy complete! Resources: 29 destroyed.`
6. Verify the EKS resources are removed: `aws eks list-clusters` should return an empty list of clusters.

Extra checks for other billable resources (optional if `terraform destroy` succeeded):

```bash
aws ec2 describe-vpcs --filters Name=cidr,Values=10.10.0.0/16
aws elbv2 describe-load-balancers
```

- Destroy works from the local machine because the AWS access and secret keys are stored in a local credentials file. This should not be the case for a production or secured environment.
- If something goes wrong, remove the resources manually in the AWS console (see AWS documentation).

### 6.8 Chapter 7 summary

- A Terraform module for the software-defined network was created, spanning two availability zones in a single region.
- A module that instantiates an AWS EKS cluster for Kubernetes was created.
- An Argo CD GitOps server was implemented in the cluster using a Helm package.
- A sandbox environment was implemented as code that uses all the modules in a declarative, immutable way.
- Detail was given on the Terraform code to show what it takes to define an environment with IaC, immutability and a CI/CD pipeline, and to teach the module pattern and infrastructure design decisions.
- Additional infrastructure modules may be needed as more is learned about the microservices. Later in the book pre-written hosted code is used.
- Next (Chapter 8): development of the example microservices, which will be released into this infrastructure.

---

## Unit 3 Revision

### Key definitions

| Term | Definition |
|---|---|
| DevOps | Pursuit of improvements in how software is developed, released and supported, across organizational design, culture, process and tooling |
| Immutable infrastructure | Infrastructure components that are never changed after creation; changes are made by destroying and re-creating them |
| Configuration (server) drift | Gradual divergence of server and environment states caused by inconsistent manual changes |
| Infrastructure as Code (IaC) | Representing all infrastructure changes as machine-readable files (code) that define the target state and can be reapplied to re-create an environment |
| Terraform | HashiCorp's declarative IaC tool that manages and applies infrastructure changes and tracks state |
| HCL | HashiCorp Configuration Language; JSON-like format in which Terraform files are written |
| Declarative approach | Declaring the desired end state; the tool works out how to reach it |
| CI (Continuous Integration) | Continuously integrating small changes into the repository and testing them |
| CD (Continuous Delivery) | Automatically and continually shipping finished microservices as containers |
| Continuous Deployment | Releasing the containers into the production environment (by the release team) |
| Pipeline | A tool that defines and manages the steps of a CI/CD process |
| GitHub Actions | GitHub's built-in CI/CD tool; workflows are written in YAML |
| Workflow | The set of steps run whenever a pipeline is triggered |
| Git tag | A name or label given to a particular point in a Git repository's history; used here as the pipeline trigger |
| Action | A modularized library of code that can be called from a GitHub Actions workflow |
| IAM | AWS Identity and Access Management: service that manages users, groups and permissions |
| ARN | Amazon Resource Name: unique identifier of every AWS resource |
| S3 | Simple Storage Service: AWS object storage; used as the Terraform state backend |
| Terraform backend | Location of the Terraform state file |
| Terraform state file | JSON file recording the current state of the environment and last operations |
| Terraform resource | Object representing a thing whose state is declared |
| Terraform provider | Packaged library of resources for a platform (for example AWS) |
| Terraform module | Reusable, encapsulated block of HCL code, like a function |
| Sandbox environment | Test environment used to try out IaC modules and pipelines; destroyed afterwards |
| Monorepo | A single repository holding all environment configurations |
| VPC | Virtual private cloud: parent object of a virtual network in AWS |
| Subnet | A smaller network into which a VPC is partitioned |
| CIDR | Classless Inter-Domain Routing: notation for an IP address range (for example `10.0.0.0/16`) |
| Availability zone | A separate physical data center in AWS |
| Internet gateway | AWS component that connects the VPC to the public internet |
| NAT gateway | Gateway that lets resources in private subnets reach the internet; needs an Elastic IP |
| Elastic IP (EIP) | A real, internet-accessible IP address allocated in AWS |
| Security group | VPC rule set that restricts traffic into (ingress) and out of (egress) the network |
| Container | Predictable, isolated application package without the overhead of a VM |
| Kubernetes | Google-developed container orchestration tool for deploying, scaling, observing and managing containers |
| Cluster / Control plane / Node / Pod | Parent Kubernetes object / brain of the cluster / machine running workloads / deployable unit with one or more containers |
| EKS | Elastic Kubernetes Service: AWS managed Kubernetes service |
| kubectl | Command-line application for interacting with a Kubernetes cluster |
| kubeconfig | File with the connection details needed to connect to a Kubernetes cluster |
| GitOps | Working method that uses Git as the source of truth for the target system state |
| Argo CD | Declarative GitOps tool that deploys Kubernetes applications from a Git source |
| Helm / chart | Package manager for Kubernetes / a Helm package deployment |
| Istio | Service mesh tool for managing traffic and communication between microservices |

### Comparisons

Immutable vs traditional (mutable) infrastructure:

| Basis | Immutable infrastructure | Traditional infrastructure |
|---|---|---|
| Change method | Destroy and re-create the component | Patch, alter configuration, stop and start processes in place |
| Predictability | High; easy to reproduce and test | Low; states drift over time |
| Manual effort | Low (automated) | High, needs operator expertise |
| Self-service platform | Supported | Difficult |
| Trade-off | Cost of destroying and re-creating (reduced by the cloud and IaC) | Inconsistent environments |

Continuous Delivery vs Continuous Deployment (as used in the model):

| Basis | Continuous Delivery | Continuous Deployment |
|---|---|---|
| Done by | Microservices teams | Release team |
| Activity | Automatically and continually ship finished microservices as containers | Release the containers into the production environment |
| Tool | CI/CD pipeline | GitOps deployment server (Argo CD) |

Local state vs remote state (S3 backend):

| Basis | Local state file | Remote state in S3 |
|---|---|---|
| Location | Local filesystem (default) | AWS S3 bucket |
| Sharing | Difficult to share across machines and users | Shared by everyone using the tool |
| Risk | State conflicts and synchronization issues | Bucket is private by default; permissions needed |

One repository per environment vs monorepo:

| Basis | One repository per environment (chosen) | Monorepo |
|---|---|---|
| Independence | Teams manage their own environments independently | Shared by all environments |
| Pipeline and code | Bundled together in the repository | Often use specialized CI/CD tools such as Jenkins |
| Sharing and consistency | Less sharing of components | Easier sharing of libraries, components and actions; more consistency |

Internet gateway vs NAT gateway:

| Basis | Internet gateway | NAT gateway |
|---|---|---|
| Purpose | Connects the VPC to the public internet | Gives private-subnet resources a path out to the internet |
| Used by | Public subnets (route `0.0.0.0/0`) | Private subnets (route `0.0.0.0/0` for outbound) |
| Placed in | Attached to the VPC | A public subnet; needs an Elastic IP |

### Command cheat sheet

| Command | What it does |
|---|---|
| `terraform version` | Checks the Terraform installation |
| `terraform fmt` | Formats (lints) the code |
| `terraform init` | Installs providers/modules and initializes the backend |
| `terraform validate` | Checks syntax |
| `terraform plan` | Dry run: shows what would change |
| `terraform apply -auto-approve` | Applies the changes without human confirmation (used in the pipeline) |
| `terraform destroy` | Destroys the environment's resources in dependency order |
| `terraform get -update` | Pulls the latest version of the modules from GitHub |
| `aws configure` | Stores access key, secret, region and output format for the CLI |
| `aws iam list-users` | Lists IAM users (tests the CLI configuration) |
| `aws iam create-group` / `add-user-to-group` / `attach-group-policy` | Create the Ops-Accounts group, add the user, attach policies |
| `aws iam create-policy` | Creates a custom policy (EKS-Management) from a JSON file |
| `aws s3api create-bucket` | Creates the S3 bucket for Terraform state |
| `aws ec2 describe-vpcs --filters Name=cidr,Values=10.10.0.0/16` | Checks that the VPC exists |
| `aws eks list-clusters` | Lists EKS clusters (confirms creation or removal) |
| `git tag -a v0.1 -m "message"` | Creates an annotated tag locally |
| `git push origin v0.1` | Pushes the tag, which triggers the pipeline |
| `export KUBECONFIG=<path>` | Tells kubectl where the kubeconfig file is |
| `kubectl get svc` | Lists services; proves the cluster connection works |
| `kubectl get pods -n "argo"` | Lists the Argo CD pods |

### Files at a glance

| File | Where | Purpose |
|---|---|---|
| `main.tf` | Sandbox repository and each module | Defines resources, providers, backend and module calls |
| `variables.tf` | Each module | Declares input variables |
| `outputs.tf` (`output.tf`) | Network and Kubernetes modules | Defines values returned by the module |
| `.gitignore` (Terraform) | Every repository | Keeps Terraform working files out of Git |
| `.github/workflows/main.yml` | Sandbox repository | GitHub Actions workflow (pipeline definition) |
| `custom-eks-policy.json` | Local working directory | Custom IAM policy for EKS permissions |
| `kubeconfig` | Generated by the Kubernetes module; uploaded as artifact | Connection details for kubectl |

### Likely questions and answer outlines

**1. Explain immutable infrastructure and why the cloud enables it.**
- Definition: components cannot be changed after creation; update by destroy and re-create.
- Mutable systems cause manual work, drift and unpredictability.
- Cloud makes components virtual, so creation and destruction are cheap and infrastructure behaves like software.
- IaC then defines the target state.

**2. What is IaC? Why Terraform?**
- Machine-readable files define all infrastructure changes.
- Terraform is declarative, supports immutability, tracks state, and works with many providers.
- Managing Terraform = managing state, configuration files and solution quality.

**3. Why does the infrastructure need a CI/CD pipeline?**
- IaC makes changes predictable, not safe.
- Pipeline gives consistent, automated testing and delivery of small changes.
- GitHub Actions is used because GitHub already hosts the code.

**4. List the steps to configure the AWS operations account.**
- Create IAM user `ops-account` with programmatic access.
- Attach `IAMFullAccess`.
- Create `Ops-Accounts` group, add user, attach the policy set.
- Create and attach the custom `EKS-Management` policy.
- Configure the AWS CLI with `aws configure`.
- Create the S3 state bucket.

**5. Why is the Terraform state stored in S3?**
- State must be shared and consistent across machines and users.
- Local state causes conflicts and synchronization problems.
- S3 is a supported backend; only a private bucket and permissions are needed.

**6. Describe the infrastructure pipeline steps.**
- Trigger on Git tag `v*`.
- Provision Ubuntu VM, check out code, install tools.
- `fmt`, `init`, `validate`, `plan`, `apply`.
- Upload `kubeconfig` artifact.

**7. Explain the three infrastructure components.**
- Network: VPC, four subnets across two AZs, IGW, NAT gateways and routing.
- Kubernetes: EKS control plane plus managed node group.
- GitOps: Argo CD installed with Helm, deploys from Git.

**8. Why four subnets and two availability zones?**
- EKS requires subnets in at least two AZs for resilience.
- Public subnets host load balancers and NAT gateways.
- Private subnets host the worker nodes and microservices.
- Two AZs keep the system running if one data center fails.

**9. Differentiate internet gateway and NAT gateway.**
- IGW connects the VPC to the internet and serves public subnets.
- NAT lets private-subnet resources reach the internet outbound and needs an Elastic IP.

**10. How does the network module get reused across environments?**
- Module exposes `vpc_id`, `subnet_ids`, public and private subnet IDs.
- Environment `main.tf` calls the module with different variables and CIDRs.
- One change in the module propagates to every environment.

**11. Explain the EKS module's IAM roles.**
- Cluster role trusts `eks.amazonaws.com` and attaches `AmazonEKSClusterPolicy`.
- Node role trusts `ec2.amazonaws.com` and attaches worker node, CNI and ECR read-only policies.
- Security group allows outbound traffic only.

**12. How is Argo CD installed?**
- Kubernetes and Helm providers are configured with the EKS endpoint, certificate and authenticator.
- A namespace `argo` is created.
- `helm_release` installs the `argo-cd` chart from the Argo Helm repository.

**13. How do you verify and clean up the environment?**
- Download `kubeconfig` artifact, `export KUBECONFIG`, `kubectl get svc`, `kubectl get pods -n argo`.
- `terraform destroy` removes resources in dependency order.
- `aws eks list-clusters` should be empty.

---

# PART 3: UNIT 4 - DEVELOPER WORKSPACE

## 7. Coding Standards and the Developer's Setup

### 7.1 Why the developer workspace matters

- A microservices architecture is most beneficial when applied to complex systems.
- In any reasonably complex system, the only sustainable way to get positive and predictable collaboration from well-intentioned participants is to make the right behaviours the easiest and most intuitive ones.
- If doing the "right thing" is hard, most people take the path of least resistance, which leads them the wrong way.
- Therefore, invest early in repeatable, predictable, standardized development processes that avoid unnecessary complexity and give developers an intuitively comfortable structure.
- **Key principle:** an exceptional developer experience (a consistent, intuitive approach that lets every developer easily "do the right thing") is one of the most underappreciated prerequisites of a successful microservices culture.

Role of CI/CD:

- Robust CI/CD pipelines (for both code and infrastructure) are a key enabler of microservices.
- Because of modularity and independent deployability, there will be many pipelines (one per microservice).
- Avoid: every team building its pipeline in its own way, with no consistency with other microservices' codebases.
- Creating a new microservice should be quick and predictable, ideally a templated process in which most things are fully automated.

Role of the local development workspace:

- Most software engineers spend most of their time writing code on their laptops.
- Providing the right guidance and tooling at this stage early brings large benefits later.
- Do not dictate every aspect of a developer's workflow (for example, forcing one code editor on a large team creates opposition and achieves nothing).
- Instead, declare only fundamental principles that the team must follow.

Chapter roadmap:

- 10 opinionated rules/guidelines for a developer workspace.
- Setting up local containerized environments on multiple platforms (vanilla Docker and a lightweight Kubernetes).
- Advanced example: installing a local Cassandra database in the Docker setup.
- Outcome: a fully functioning containerized infrastructure ready for microservices code, plus the principles used to lay out code in Chapter 9.

### 7.2 Three high-level goals

Before introducing organizational standards, clarify and agree on goals, so people understand the "why" before the "how" and "what".

| # | Goal | Explanation |
|---|---|---|
| 1 | Code can be set up in a short time frame | Joining a new team should not mean spending a week or more getting an environment running. Being stuck on "How do I even run it?" kills excitement and momentum. Target: a new developer unfamiliar with the code can set up a microservice, or a collection of microservices forming a logical subsystem, in under one hour. |
| 2 | New microservices can be created quickly, easily and predictably | Starting a service involves boilerplate: code templates for each tech stack (Java, Go, Node, Python, etc.), automated testing and data-management setup, configured dependencies (for example data storage), and a skeleton pipeline. Worst case: a developer works out all of this from scratch every time. Even worse: different developers varying these aspects unnecessarily for similar services. Remedy: well-thought-out templates for each standard tech stack give consistency, familiarity, high quality and higher speed. |
| 3 | Quality control must be automated | Enforcement of the company's software quality standards must be automated and not left to human error. |

### 7.3 The 10 workspace guidelines

These guidelines are opinionated and based on the authors' experience. Use them as a starting point and modify them to fit your needs.

#### Guideline 1: Make Docker the only dependency

- Aim: avoid the "works for me" syndrome. Anybody should be able to create the same environment easily.
- Elaborate manual setups should be banned.
- Only the Docker runtime and Docker Compose should be expected on the host machine, nothing else.
- OS (Windows, macOS, Linux) and installed libraries should not matter.
- No expectations about a specific version of Python, Go, Java, etc. on the developer's machine.
- Setup instructions must be automated, not codified in README files.

#### Guideline 2: Remote or local should not matter

- Setup must work whether the developer runs code on their own laptop or on a cloud server (via an IDE's remote development / SFTP plug-ins).
- This is the default. Any exception must be justified and documented.

#### Guideline 3: Ensure a heterogeneous-ready workspace

- Setup must accommodate multiple microservices in multiple programming languages, using multiple data storage systems.
- Microservices architecture assumes the ability to combine heterogeneous services. It does not mean one codebase in one container or one technology stack.
- If 100% of services are in one language (for example Java, or even Go), something is wrong with the setup.
- Caution: this does not mean every team picks any language or database it likes. When uncertain, use two, at most three, stacks.
- The point is that a new stack can be introduced if genuinely needed. The example setup should demonstrate this by implementing more than one stack.

**The Rule of Twos:**

- Practise heterogeneity proactively.
- For any critical component, use at least two alternatives in production at the same time, even if only one is needed.
- Have infrastructure that supports the two alternatives as easily as a single one.
- Example: if most APIs are in Node.js (good for I/O-bound work), implement some in Go, Java or Rust (for CPU-bound work, where Node is weaker).
- Limit languages and database systems across the whole application to two or three, otherwise there is too much choice and serious maintenance overhead.

#### Guideline 4: Running a single microservice and/or a subsystem of several should be equally easy

- Example: an airline reservation system built from three microservices.
- A developer should be able to check out one microservice and work on it, or check out the entire subsystem of interacting microservices.
- Both tasks must be very easy.

#### Guideline 5: Run databases locally, if possible

- For isolation, provide a local, Dockerized alternative for every database system.
- Switching to cloud services (for example AWS) should be trivial, via a configuration change.
- Example: MinIO can act locally as a drop-in replacement for S3. Many AWS service alternatives are available as local installs.

#### Guideline 6: Implement containerization guidelines

Not all containerization approaches are equal. Making a containerized coding environment developer-friendly takes effort.

Principles:

- **(a) Edit on host, run in container:** developers must be able to edit code on the host (laptop, EC2 dev server) with any code editor, but a full run/test/debug must execute in a container.
- **(b) Dockerfile vs Docker Compose:** they are easily confused since Compose can do much of what a Dockerfile does, so distinguish them:
  - Dockerfile: building a container image.
  - Docker Compose: running things locally, including complex integrations.
  - An image built from a Dockerfile should be directly runnable on Kubernetes, AWS ECR, Swarm or any production-grade runtime.
  - The local/dev image need not be identical to the production image. Teams optimise the former for usability and the latter for security and performance (for example via multistage builds).
- **(c) Multistage builds must be used in Dockerfiles:** slim images for production and fuller-featured images for local development.
- **(d) Developer user experience is critical:** provide hot-reloading of code and/or the ability to attach a debugger out of the box.

#### Guideline 7: Establish rules for painless database migrations

Goal: manage databases and data in a way that supports team collaboration. Schema changes must be codified and applied without manual steps.

- (a) Every schema change is codified as a database migration script. Migration files are named and ordered by date.
- (b) Migrations support both schema changes and sample-data insertion.
- (c) Running migrations is part of project launch (via `make start`) and is enforced.
- (d) Migrations are automated and part of every build (integration builds, feature-branch builds for PRs, etc.).
- (e) It must be possible to indicate which migrations run in which environments (or can be skipped), for example skip sample-data migrations in production.
- (f) Rules apply to all data stores: relational, columnar, NoSQL, etc.
- (g) Examples/tools cited: Flyway (introduction to migrations), a blog on Cassandra migrations, Node's `db-migrate-sql` for MySQL.

#### Guideline 8: Determine a pragmatic automated testing practice

The authors advocate a measured, pragmatic approach that balances developer experience with quality metrics and personal preferences (some teams abandon automated testing, others over-do test-driven development).

- (a) Test-first, test-as-you-code and test-after-code are all acceptable, provided all code has a reasonable amount of meaningful tests before merging to main.
- (b) Use a testing approach/framework idiomatic to the stack (for example JUnit for Java). Services in the same language must use a uniform approach, not vary by author or time.
- (c) External tools (for example Cucumber for acceptance/performance testing) are fine with justification, but must be fully integrated in the service's own repository and as easy to run as a native solution (for example `make test-all`) with no extra setup.
- (d) Tests that span multiple microservices need special care. Apply them at a higher level (an API invoking services, or a UI), or use a dedicated repository for test orchestration/automation.
- (e) Set up linting / static analysis with a consistent configuration matching the organization's style.

#### Guideline 9: Branching and merging

Core principles of good branching hygiene:

- (a) All development happens on feature and bug branches.
- (b) Merging to the main branch is not allowed unless all tests pass (including integration tests in a temporary integration cluster spun up for the branch).
- (c) Test-run status (after each commit/push) must be readily visible to reviewers during pull requests.
- (d) Linting / static-analysis errors should prevent pushing to a branch and/or merging to main.

#### Guideline 10: Common targets should be codified in a makefile

- Every repository (generally one repository per microservice) has a makefile that makes it easy to work with the code regardless of language stack.
- The makefile has standard targets, so in any codebase `make run` / `make start` brings it up and `make test` runs the tests.
- The book provides example microservices (Go and Node) following this pattern, and a sample multi-microservice workspace on GitHub, plus a Markdown template of the guidelines.

Standard makefile targets:

| Target | Purpose |
|---|---|
| `start` | Run the code |
| `stop` | Stop the code |
| `build` | Build the code (typically a container image) |
| `clean` | Clean all caches and run from scratch |
| `add-module` | Add a module (dependency) |
| `remove-module` | Remove a module (dependency) |
| `dependencies` | Ensure all modules declared in dependency management are installed |
| `test` | Run all tests and produce a coverage report |
| `tests-unit` | Run only unit tests |
| `tests-at` | Run only acceptance tests |
| `lint` | Run a linter to ensure coding style conforms to defined standards |
| `migrate` | Run database migrations |
| `add-migration` | Create a new database migration |
| `logs` | Show logs (from within the container) |
| `exec` | Execute a custom command inside the code's container |

Summary of this section:

- Developer experience is paramount for happy, productive, autonomous teams.
- 3 goals lead to 10 principles, which give a repeatable blueprint for user-friendly workspaces, independent of technology stack or tools.
- One enabling principle is containerization with Docker.

---

## 8. Setting Up a Containerized Environment Locally

- Only Docker (and possibly `make`) should be expected on a developer machine. Everything else is installed from it.
- On Linux, installing Docker is straightforward. The question is how to get it on macOS or Windows.

### 8.1 Options for macOS / Windows

| Option | Points |
|---|---|
| Docker4Mac / Docker4Windows | Revolutionary when released: brought Docker to everyday desktops; later added Kubernetes support. Still the easiest way to get Docker and Kubernetes on macOS/Windows. Drawbacks: day-to-day experience is hit-and-miss; high CPU usage and battery drain even on powerful hardware; allows only one Docker instance and one Kubernetes (limits experimentation and "freedom to break things"). |
| VirtualBox (or commercial alternatives) | Own VMs; in the authors' experience even heavier than Docker4Mac/Win. |
| Multipass (by Canonical, creators of Ubuntu) | Quickly launches Ubuntu-based Docker hosts on macOS, Windows (or even Linux). Supports several underlying hypervisors; by default uses HyperKit on macOS and Hyper-V on Windows (Windows Pro required), which are more lightweight. |

### 8.2 Installing Multipass

- Download installers for each platform from the Multipass website. Usable on macOS or Windows Subsystem for Linux.
- Launch a new Ubuntu environment (default image in the example: Ubuntu 20.04 LTS):

```bash
multipass launch -n docker
Launched: docker
multipass list
```

```text
Name     State       IPv4             Image
docker   Running     192.168.64.3     Ubuntu 20.04 LTS
```

- Default allocation: 1 GB RAM, 5 GB disk, 1 CPU core.
- Node.js or Python with MySQL: 1 GB may be OK. Heavy Java apps with Java-based databases such as Cassandra need more memory.
- Override at launch: `-m 4G` sets memory to 4 GB and `-n` sets the name.

```bash
multipass launch -m 4G -n dubuntu
multipass exec dubuntu -- bash
ubuntu@dubuntu:~$ free -m        # shows ~3945 MB total memory
```

Warning: Multipass allows more than one CPU (for example `-c 2`), but for the authors this produced broken containers on macOS (possibly a hypervisor limitation). Proceed with caution. Increasing memory has been no problem.

### 8.3 Increasing memory of an existing instance

Possible without reinstalling, but the process is fragile.

1. Stop the Multipass daemon via `launchctl` (otherwise it overwrites your changes).
2. Edit the configuration JSON file `multipassd-vm-instances.json`.
3. Relaunch the daemon.

```bash
sudo launchctl unload /Library/LaunchDaemons/com.canonical.multipassd.plist
sudo vi "/var/root/Library/Application Support/multipassd/multipassd-vm-instances.json"
sudo launchctl load /Library/LaunchDaemons/com.canonical.multipassd.plist
```

- JSON file fields per instance: `deleted`, `disk_space`, `mac_addr`, `mem_size`, `metadata`, `mounts`, `num_cores`, `ssh_username`, `state`.
- Edit `mem_size` (value is in bytes).
- Use a number divisible by 1 GB: 1 GB = 1024 x 1024 x 1024 = 1,073,741,824 bytes.
- Example: 8 GB = 1073741824 x 8 = 8,589,934,592.

### 8.4 Entering the container and mapping folders

| Command | Purpose |
|---|---|
| `multipass exec <name> -- <command>` | Run any command inside the instance (for example `free -m`, `bash`) |
| `multipass set client.primary-name=dubuntu` | Mark an instance as the primary one |
| `multipass shell` | Open a shell in the primary instance |
| `multipass mount $HOME dubuntu:/home/ubuntu/mac` | Map the macOS home folder to a folder in the instance |
| `multipass info dubuntu` | Show name, state, IPv4, release, load, disk usage, memory usage, mounts |
| `multipass restart` | Restart the instance (for example after group changes) |

Once a virtualized Linux works via Multipass, installing Docker (or even local Kubernetes) becomes easy.

---

## 9. Installing Docker

### 9.1 Installation steps (inside the Multipass Ubuntu instance)

1. Open shell: `multipass shell`.
2. Update system:
   ```bash
   sudo apt-get update && sudo apt-get upgrade -y
   ```
3. Install build tools:
   ```bash
   sudo apt-get install build-essential -y
   ```
4. Sanity check: remove any old Docker packages:
   ```bash
   sudo apt-get remove docker docker-ce-cli docker-engine docker.io containerd runc
   ```
5. Install Docker via snap:
   ```bash
   sudo snap install docker
   ```
6. Add snap binaries to PATH:
   ```bash
   echo 'export PATH=/snap/bin:$PATH' >> ~/.bashrc
   source ~/.bashrc
   ```

### 9.2 Run Docker without sudo (non-root)

- After the steps above, Docker works only as root (via `sudo`), which is insecure and inconvenient.
- Fix: give the default non-privileged user (`ubuntu`) group access to Docker.

```bash
sudo groupadd docker
sudo usermod -aG docker $USER
exit
multipass restart
multipass shell
docker ps
docker version            # Client Version 19.03.11, API 1.40
docker-compose --version  # 1.25.5
```

- You must log out and log back in for the group change to take effect.

### 9.3 Testing Docker with MySQL (Docker Compose)

Create `mysql-stack.yml`:

```yaml
version: '3.1'
services:
  db:
    image: mysql
    restart: always
    environment:
      MYSQL_ROOT_PASSWORD: rootPass
    ports:
      - 33060:3306
```

- `services`: defines one service named `db`.
- `image: mysql`: uses the official MySQL image.
- `restart: always`: container restarts automatically.
- `environment`: sets the MySQL root password.
- `ports: 33060:3306`: maps host port 33060 to container port 3306.
- The default file name is `docker-compose.yml`. A custom name requires the `-f` flag.

```bash
docker-compose -f mysql-stack.yml up -d
docker ps
```

```text
CONTAINER ID   STATUS         IMAGE   PORTS
e08f6f072c89   Up 3 seconds   mysql   33060/tcp, 0.0.0.0:33060->3306/tcp
```

- `up -d` starts the stack in detached (background) mode. This confirms a working Docker + Docker Compose setup.

---

## 10. Advanced Local Docker Usage: Installing Cassandra

- A more complex example than MySQL: running Cassandra in a container.
- Cassandra is popular and versatile, so it is likely to be needed in cloud-native microservices work.
- **Memory:** Cassandra needs more than the default 1 GB RAM. Give the Multipass instance 6 to 8 GB.

Create `docker-compose.yml` anywhere in the instance:

```yaml
version: '3'
services:
  cassandra-seed:
    container_name: cassandra-seed
    image: cassandra:3.11
    ports:
      - "9042:9042"   # Native protocol clients
      # - "7199:7199" # JMX
      # - "9160:9160" # Thrift clients
    volumes:
      - local_cassandra_data_seed:/var/lib/cassandra

volumes:
  local_cassandra_data_seed:
```

| Element | Meaning |
|---|---|
| `cassandra-seed` | Service name (the seed node) |
| `container_name` | Fixed container name `cassandra-seed` |
| `image: cassandra:3.11` | Official Cassandra image, version 3.11 |
| Port 9042 | Native protocol clients (enabled) |
| Port 7199 | JMX (commented out) |
| Port 9160 | Thrift clients (commented out) |
| `volumes` | Named volume `local_cassandra_data_seed` mounted at `/var/lib/cassandra` so data persists beyond the container |

Run and verify:

```bash
docker-compose up -d
Creating network "cassandra_default" with the default driver
Creating cassandra-seed ... done
docker-compose ps
docker exec -it cassandra-seed cqlsh
Connected to Test Cluster at 127.0.0.1:9042.
cqlsh> DESCRIBE keyspaces;
```

- `docker exec -it cassandra-seed cqlsh` opens the CQL shell inside the container.
- `DESCRIBE keyspaces;` lists all existing keyspaces in the new installation.
- Result: a fully working local Cassandra setup.

---

## 11. Installing Kubernetes

- For most cases Docker Compose provides ample capability for orchestrating the components of an application.
- Kubernetes has a much wider range of functionality but proportionally higher complexity.

### 11.1 Avoid using Kubernetes locally unless you must

- Not recommended for everyday coding.
- Docker and Docker Compose complete most containerization tasks more easily and have more straightforward tooling for building images.
- Kubernetes excels at orchestrating a runtime fleet of containers. It is rarely needed in dev, but crucial for production, preproduction, staging and performance testing.
- Local Kubernetes may be used in some circumstances, especially for targeted testing.

### 11.2 Single-machine Kubernetes options

- The official Kubernetes distribution cannot simply be installed on one machine. It is designed for a cluster of servers.
- Side projects bypass this requirement to give a single-machine Kubernetes.

| Tool | Created by | Remarks |
|---|---|---|
| Minikube | The Kubernetes community | The default one |
| k3s | Rancher | Favourite for simplicity and reliability |
| MicroK8s | Canonical | Favourite for simplicity and reliability; installed via snap |

### 11.3 Installing k3s

```bash
curl -sfL https://get.k3s.io | sh -
[INFO] Using v1.17.4+k3s1 as release
[INFO] Installing k3s to /usr/local/bin/k3s
sudo k3s kubectl get nodes
```

```text
NAME      STATUS   ROLES     AGE   VERSION
dubuntu   Ready    master    104s  v1.17.4+k3s1
```

### 11.4 Installing MicroK8s

- Steps similar to k3s, but the current user must also be added to a specific group and you must log in again (as with Docker).

```bash
sudo snap install microk8s --classic
sudo usermod -a -G microk8s $USER
sudo chown -f -R $USER ~/.kube
exit
multipass shell
microk8s.kubectl get services --all-namespaces
```

```text
NAMESPACE NAME         TYPE       CLUSTER-IP     PORT(S)               AGE
default    kubernetes ClusterIP 10.152.183.1     443/TCP               3m22s
```

### 11.5 Limitation of Kubernetes for development and Skaffold

- An out-of-the-box Kubernetes environment has no tooling to build container images. It needs the URI of a pre-built image from a registry.
- So there is no obvious build-run-test cycle, making Kubernetes cumbersome for active development.
- Kubernetes is better for sophisticated orchestration in non-development environments (QA, staging, preprod, prod).
- **Skaffold** (open-source toolset, developed several years after Kubernetes) makes building container images pluggable into Kubernetes life cycles.
- The book does not use local Kubernetes in most examples. A sample project is the Skaffold microservices repository.

### 11.6 Chapter 8 summary

- Goals for a developer-friendly workspace lead to 10 principles.
- Containerized foundation demonstrated for macOS, Windows and Ubuntu Linux.
- Result: easier onboarding of developers and promotion of good coding practices.

---

## Unit 4 Revision

### Likely questions and answer outlines

**1. Why is a good developer workspace important in microservices? State the three goals.**
- People take the path of least resistance. The right behaviour must be the easiest.
- Goals: (1) set up code in a short time (under an hour); (2) create new microservices quickly, easily, predictably using templates; (3) automate quality control.

**2. Explain the 10 workspace guidelines.**
- (1) Docker the only dependency; (2) remote or local should not matter; (3) heterogeneous-ready workspace (Rule of Twos); (4) single microservice or subsystem equally easy; (5) run databases locally; (6) containerization guidelines; (7) painless database migrations; (8) pragmatic automated testing; (9) branching and merging; (10) common makefile targets.

**3. What is the Rule of Twos?**
- For any critical component, use at least two alternatives in production at the same time, even if one would do.
- Maintain infrastructure supporting both as easily as one. Limit total languages/databases to two or three.

**4. Differentiate Dockerfile and Docker Compose. Why multistage builds?**
- Dockerfile builds an image. Compose runs things locally, including multi-container integrations.
- Multistage builds give slim images for production and fuller images for development.

**5. List and explain the standard makefile targets.**
- `start`, `stop`, `build`, `clean`, `add-module`, `remove-module`, `dependencies`, `test`, `tests-unit`, `tests-at`, `lint`, `migrate`, `add-migration`, `logs`, `exec`.

**6. How can Docker be set up on macOS/Windows? Explain Multipass.**
- Docker4Mac/Windows: easy but CPU/battery heavy, one Docker and one Kubernetes.
- VirtualBox: heavy.
- Multipass: Canonical; Ubuntu VMs; HyperKit/Hyper-V.
- Defaults 1 GB RAM / 5 GB disk / 1 CPU. Override with `-m`. Do not raise CPUs on macOS.
- Commands: `launch`, `list`, `exec`, `shell`, `mount`, `info`.

**7. Describe the steps to install Docker in a Multipass instance and run it without sudo.**
- `apt update/upgrade`, `build-essential`, remove old packages, `snap install docker`, PATH export.
- `groupadd docker`, `usermod -aG docker $USER`, log out/in.
- Verify with `docker ps` / `docker version`; test with the MySQL Compose file.

**8. Explain how to install Cassandra using Docker Compose.**
- Needs 6 to 8 GB RAM.
- Compose file with image `cassandra:3.11`, port 9042, named volume.
- `docker-compose up -d`, `docker exec -it cassandra-seed cqlsh`, `DESCRIBE keyspaces;`.

**9. Compare Docker Compose with Kubernetes for local development. Name local Kubernetes options.**
- Compose: lower complexity, straightforward image building, best for everyday development.
- Kubernetes: higher complexity, no built-in image building, best for production-like orchestration.
- Local options: Minikube, k3s, MicroK8s; Skaffold for build integration.

### Quick comparisons

Dockerfile vs Docker Compose:

| Aspect | Dockerfile | Docker Compose |
|---|---|---|
| Use | Building a container image | Running things locally, including complex integrations |
| Output | Image runnable on Kubernetes, AWS ECR, Swarm or any production runtime | Local multi-container setup (services, ports, volumes, env) |
| Related feature | Multistage builds (slim prod image, full-featured dev image) | `docker-compose up -d`; `-f` for a custom file name |

Docker Compose vs Kubernetes (local development):

| Aspect | Docker / Docker Compose | Kubernetes |
|---|---|---|
| Complexity | Lower | Proportionally higher |
| Image building | Straightforward tooling | No build tooling; needs a pre-built image URI from a registry |
| Best for | Everyday development | Production, preprod, staging, performance testing (fleet orchestration) |
| Local use | Recommended | Avoid unless needed (targeted testing); use k3s / MicroK8s / Minikube; Skaffold helps with builds |

Ports used in this unit:

| Port | Used for |
|---|---|
| 33060 (host) to 3306 (container) | MySQL in the Docker Compose test |
| 9042 | Cassandra native protocol clients (7199 = JMX, 9160 = Thrift, commented out) |

---

# Appendix: Chapter 9 Outline (check if it is in your syllabus)

Unit 4's source material also includes Chapter 9 (Developing Microservices). The syllabus topics you listed stop at the developer workspace, so this is only a compact outline in case the exam includes it.

## A.1 Designing microservice endpoints

- Bounded contexts from an Event Storming session: flights management and reservations management. The first two services are `ms-flights` and `ms-reservations`.
- **SEED(S) design flow:** identify actors, write business JTBDs, write technical JTBDs, draw a sequence diagram, translate JTBDs into actions and queries, write the OpenAPI Specification, then implement.
- Actors: customer, airline app, BFF (backends for frontends) API, `ms-flights`, `ms-reservations`.
- **Key decision:** avoid microservices calling each other directly. The BFF API orchestrates the calls. This follows the Chapter 3 recommendation.
- Sequence diagram (Figure 9-1) has two parts:
  - Steps 1 to 7: render the seating chart. BFF calls `ms-flights` for the seat map and `ms-reservations` for reserved seats, then combines the results.
  - Steps 8 to 11: reserve a seat. BFF authenticates and calls `ms-reservations.reserveSeat()`.
- Actions and queries:
  - `ms-flights`: get flight details (`flight_no`, departure date-time, returns `flight_id`); get flight seating (`flight_id`, returns a Seat Map JSON).
  - `ms-reservations`: query reserved seats (`flight_id`, returns seat numbers); reserve a seat (`flight_id`, `customer_id`, `seat_num`, returns 200 success or 403 Forbidden).
- Endpoints are described with an **OpenAPI Specification (OAS)** and rendered with Swagger Editor or ReDoc.
  - `ms-flights`: `GET /flights`, `GET /flights/{flight_no}/seat_map`.
  - `ms-reservations`: `GET /reservations`, `PUT /reservations`.

## A.2 Implementing the data

- Data independence: the two services share no data space.
- `ms-reservations` uses **Redis**:
  - One hash per `flight_id`; fields are seat numbers; values are `customer_id`.
  - `HSETNX` sets a field only if it does not exist, which prevents double-booking.
  - `HKEYS` returns all occupied seats; `HGETALL` returns seats and customers.
- `ms-flights` uses **MySQL**:
  - Native JSON type supports validation, binary storage and nested lookups.
  - Table `seat_maps` (`flight_no` primary key, `seat_map` JSON, origin and destination codes).
  - Table `flights` (`flight_id` primary key, `flight_no` foreign key, `flight_date`).
  - Relational lookup by flight number and date-time is natural.
- Choosing different databases demonstrates heterogeneity and data independence.

## A.3 Implementing the code

- **Key decision:** start microservices with reusable templates.
- `ms-flights` uses **Node.js / Express.js** with the NodeBootstrap template.
- `ms-reservations` uses **Python / Flask** with a GitHub template repository.
- Only Docker and GNU Make are prerequisites. Edit on the host, run in containers.
- `ms-flights` steps:
  1. Create the project from the NodeBootstrap template.
  2. Put the OAS in `docs/api.yml` and render it with `make start` (ReDoc on port 3939).
  3. Rename the `users` module to `flights`, delete `homedoc`, update `appConfig.js`.
  4. Add routes and validation in `mappings.js` (validate `flight_no` and `departure_date_time`).
  5. Create database migrations (`seat-maps`, `flights`, `sample-data`) with `make migration-create`.
  6. Implement the endpoints and hook them to MySQL.
  7. Test at `http://0.0.0.0:5501/flights` and `/flights/AA2532/seat_map`.
- **Health checks:** Kubernetes needs endpoints to manage the container life cycle.
  - **Liveness** `/ping`: "Am I live?" Checks that the service process is up.
  - **Readiness** `/health`: "Is the database ready? Can I do useful work?" Checks the service and its dependencies.
  - The DB check is cached (`minCacheMs: 10000`) so it runs at most every 10 seconds.
- `ms-reservations` implementation:
  - Route in `service.py`: `PUT /reservations` reads the JSON body and calls the handler; returns success JSON or HTTP 403.
  - Handler in `handlers.py`: calls `model.save_reservation(json_body)`.
  - Model in `models.py`: calls `redis_conn.hsetnx(prefix + flight_id, seat_num, customer_id)`.
  - Result 1 means success; result 0 means the seat is already reserved, returning a 403 "Seat already reserved. Cannot double-book".
  - `GET /reservations?flight_id=...` uses `hgetall` and returns the seat-to-customer map.
  - Port 5000 inside the container is mapped to 7701 on the host.
  - Configuration comes from the environment, following Twelve-Factor App Factor 3.

## A.4 Umbrella project

- **Key decision:** use **Faux Git Submodules** to check out individual microservice repositories under the umbrella repository. They are simpler and more predictable than regular Git submodules.
- List the repositories in the workspace config (`fgs.json` / `repos.json`), then run `make update` to clone them.
- `bin/start.sh` and `bin/stop.sh`:
  - `set -eu` exits on error or unset variable.
  - `COMPOSE_PROJECT_NAME=msupandrunning` gives all services a common namespace and network.
  - `pushd` / `popd` enters each microservice folder and runs its `make start` or `make stop`.
  - `make proxystarts` / `make proxystop` starts or stops the proxy.
- **Traefik** edge router handles routing. Each service gets Traefik labels with a path prefix.
- The workspace `make start` brings up both services. Traefik listens on port 9080, so the base URI is `http://0.0.0.0:9080/`.
- Result: two separate-stack microservices (Node + MySQL, Python + Redis) running as one project, while still being easy to run individually.

---

# Final Checklist Before the Exam

- Be able to define: DevOps, immutable infrastructure, drift, IaC, Terraform, HCL, pipeline, GitOps, VPC, CIDR, subnet, NAT gateway, security group, EKS, pod, Argo CD, Helm.
- Know the nine key decisions of Chapter 6 and the two of Chapter 7.
- Be able to draw: target pipeline (Figure 6-1), pipeline steps (Figure 6-11), network module reuse (Figure 7-1), subnet design across two AZs (Figure 7-2).
- Know the IAM setup sequence and the policy list for the `Ops-Accounts` group.
- Know the network module contents: VPC, four subnets, IGW, two NAT gateways, two EIPs, route tables and associations.
- Know the EKS module contents: cluster IAM role, node IAM role, security group, cluster, node group, kubeconfig.
- Know the Argo CD module: Kubernetes and Helm providers, `argo` namespace, `helm_release`.
- Know the commands in the cheat sheet and when each is used.
- Know the 3 goals, the 10 workspace guidelines, the Rule of Twos and the makefile targets.
- Know the Docker installation sequence, the non-root fix, the MySQL test and the Cassandra compose file.
- Know why local Kubernetes is discouraged, the three single-machine options, and what Skaffold solves.
