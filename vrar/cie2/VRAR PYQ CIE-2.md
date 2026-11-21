---
tags: [vrar, cie-2, pyq]
share_link: https://share.note.sx/qux0hrrs#GhFNustg9EsMHOh3WAEHCWqru4a4OWfOqS/8Nt5Tf6g
share_updated: 2026-11-16T01:24:34+05:30
---
# VR & AR (IS372IA) — CIE-2 PYQ Solutions

> **Paper:** CIE-2, Part B (TEST) — Unit II from *Prefabs* + Unit III (*Geometry of Virtual Worlds* and *Motion in Real and Virtual Worlds*).
> **Questions solved:** Q1a, Q1b, Q2a, Q2b, Q3a, Q3b, Q4, Q5.
> **Marks:** 6 + 4 + 6 + 4 + 6 + 4 + 10 + 10 = **50**.
> **Companion notes:** [[VRAR CIE-2 Notes]].

**How to use this file:** each answer is written the way it should appear in the answer booklet — definition → diagram → numbered explanation → example → conclusion. The "Examiner expects" box tells you what to make sure is visible on the page.

---

## Q1a. Illustrate the process of creating and placing Prefabs in Unity. How does the use of Prefabs improve asset reuse and scene organization in VR/AR applications?

**M: 06 | CO: 2 | BT: 2**

> **Examiner expects:** a clear definition of a Prefab, the creation/placement steps, and a separate paragraph on **reuse** and **scene organisation** with VR/AR examples.

### Definition

A **Prefab** is a **reusable template of a GameObject** stored as an asset in the Project window. It saves the object's **components, property values, child objects and asset references** (meshes, materials, scripts, audio). Instances placed in a scene remain **linked** to the prefab asset, so a change to the asset can update every instance.

```
Prefab Asset (Project)
      │  drag / Instantiate()
      ├── Instance 1 (Scene A)
      ├── Instance 2 (Scene A)
      └── Instance 3 (Scene B)
```

| Basis | **Prefab Asset** | **Prefab Instance** |
|---|---|---|
| Lives in | Project window (`Assets/…`) | Hierarchy / Scene |
| Nature | Master template on disk | Linked copy of the template |
| Change it | Updates all instances (unless overridden) | Creates an override for that instance only |
| Creation | Drag a GameObject from Hierarchy to Project | Drag the asset into a scene, or `Instantiate()` |

### Process of creating and placing a Prefab

**Step 1 — Create the GameObject.**
Use `GameObject → 3D Object → Cube` (or Empty, Sphere, etc.) in the scene. Give it a meaningful name, e.g. `GrabbableCube`.

**Step 2 — Configure the object.**
Set its transform (position, rotation, scale), assign a material, add the required components — e.g. `Box Collider`, `Rigidbody`, `XR Grab Interactable`, `AudioSource` — and attach any scripts. Add child objects if the prefab needs them (a mesh child, an attach point, a highlight object).

**Step 3 — Test it in the scene.**
Press Play and confirm the object behaves correctly before turning it into a template. It is much easier to fix a prefab before it is used in twenty places.

**Step 4 — Drag it into the Project window.**
Drag the GameObject from the **Hierarchy** into the **Project** window, preferably into an `Assets/Prefabs` folder. Unity creates a **Prefab Asset** with a blue prefab icon. The Hierarchy object becomes a **Prefab Instance** (also shown in blue).

**Step 5 — Place instances.**
Drag the prefab asset from the Project window into the **Scene** or **Hierarchy**. Repeat for as many copies as needed. Each copy is an **instance** linked to the asset.

**Step 6 — Edit the master (Prefab Mode).**
Double-click the prefab asset (or click **Open** in the Inspector) to enter **Prefab Mode** — an isolated editing scene with a blue background. Make changes there; they are saved to the asset and propagate to all instances.

**Step 7 — Handle overrides.**
If you change an instance in the scene, Unity records an **override** (shown in bold, listed in the **Overrides** dropdown). Use:

- **Apply** — push the change back to the asset → all instances update.
- **Revert** — discard the local change → the instance returns to the asset values.
- **Apply/Revert single property** — do it for one field only.

**Step 8 — Use it at runtime if needed.**
Spawn copies with `Instantiate()`:

```csharp
public GameObject enemyPrefab;

void SpawnEnemy(Vector3 position)
{
    Instantiate(enemyPrefab, position, Quaternion.identity);
}
```

### Diagram — the prefab workflow

```
┌─────────────────────┐
│ 1. Create GameObject│
│    + components     │
│    + scripts        │
└──────────┬──────────┘
           │ drag Hierarchy → Project
           ▼
┌─────────────────────┐
│ 2. Prefab Asset     │  (master copy, Project window)
│    blue icon        │
└──────────┬──────────┘
           │ drag Project → Scene / Hierarchy
           ▼
┌─────────────────────┐
│ 3. Prefab Instances │  (linked copies in scenes)
│    Scene A, B, C    │
└──────────┬──────────┘
           │ edit asset / Apply / Revert
           ▼
┌─────────────────────┐
│ 4. All instances    │
│    update together  │
└─────────────────────┘
```

### How Prefabs improve asset reuse

1. **Single source of truth.** The prefab asset defines the object once. Every instance uses the same structure, components and default values, so there is no duplication of work.
2. **Reuse across scenes.** A `Player`, `Enemy`, `Grabbable`, `Door` or `UIButton` prefab can be dropped into any scene without rebuilding it.
3. **Consistency.** All copies look and behave the same. A bug fix or balance change in the asset reaches every instance automatically.
4. **Faster iteration.** Designers tune values in Prefab Mode and immediately see the result everywhere; no need to edit dozens of objects.
5. **Runtime spawning.** `Instantiate()` creates bullets, enemies, pickups, particles, UI popups and AR objects from the same template.
6. **Memory efficiency.** Meshes, materials and textures are shared assets, not copied per instance.
7. **Variants.** A base prefab can spawn specialised **variants** (boss enemy, fast enemy, low-poly LOD, Quest/mobile version) that inherit the base and override only what differs.
8. **Team collaboration.** Different developers can own different prefabs without conflicting edits in one scene file.

### How Prefabs improve scene organization

1. **The Hierarchy stays clean.** Instead of hundreds of duplicated objects, the scene contains a small number of prefab instances; the complexity lives inside the prefab.
2. **Modular scenes.** Levels are assembled from prefab "kits" — rooms, props, doors, lights, spawners — like Lego bricks.
3. **Nested prefabs.** A `Character` prefab can contain a `Sword` prefab and a `Shield` prefab; editing the sword updates every character that carries it.
4. **Folder structure.** `Assets/Prefabs/Environment`, `/Characters`, `/UI`, `/Interactables`, `/XR` keeps the project navigable.
5. **Prefab Mode isolation.** You can edit one object without accidentally moving or deleting scene geometry.
6. **Overrides are visible.** The Overrides dropdown tells you exactly what is customised on an instance, so the scene stays understandable.
7. **Safer refactoring.** Adding a component or child to the base prefab updates all instances and variants in a controlled way.
8. **Build and version control.** Prefabs are separate assets, so changes are easier to review, merge and revert than edits buried in a large scene file.

### Why this matters specifically in VR/AR

| VR/AR need | Prefab solution |
|---|---|
| **XR rig / player** | One `XROrigin` prefab with camera offset, controllers and locomotion; drop into every scene. |
| **Interactables** | `Grabbable`, `Socket`, `Lever`, `Door`, `Button` prefabs with consistent physics, haptics and highlights. |
| **Controllers / hands** | Left/right controller models, ray interactors and hand visuals are prefab instances. |
| **Spawners** | Enemies, projectiles, confetti and UI popups are instantiated from prefabs. |
| **Networked objects** | Every client spawns the same prefab; a prefab registry keeps network IDs consistent. |
| **AR trackables** | A placement indicator and AR object prefab are instantiated on detected planes/images. |
| **Performance variants** | Low-poly / high-poly LOD variants and Quest/PC variants inherit one base prefab. |
| **Scene assembly** | Modular room/prop prefabs let a small team build large VR environments quickly. |

### Limitations (mention briefly for balance)

- Too many **overrides** make it hard to tell the base value from the instance value.
- **Nested prefabs** can make debugging and ownership confusing.
- An accidental **Apply** can push a local experiment to every instance.
- Poor folder structure creates **prefab clutter** and broken references.
- Large prefabs with many children can be slow to open and edit.

### Conclusion

A Prefab is a **reusable GameObject template**; creating one means configuring an object and dragging it into the Project, and placing one means dragging the asset into a scene or calling `Instantiate()`. Prefabs give VR/AR projects **reuse, consistency, maintainability and a clean scene structure**, which is essential when the same XR rig, interactable or UI panel must appear in many scenes and on many devices.

---

## Q1b. Explain the concept of geometric primitives and describe why triangles are commonly used in VR modelling.

**M: 04 | CO: 2 | BT: 2**

> **Examiner expects:** definition + examples of primitives, then at least 5 solid reasons for triangles, ideally with a VR performance angle.

### Geometric primitives

A **geometric primitive** is a **basic building-block shape** used to construct more complex 3D models. Primitives are simple, finite and mathematically well-defined; larger models are built by combining, scaling, rotating and editing them.

**Common primitives:**

| Primitive | Dimension | Example use |
|---|---|---|
| Point | 0D | Particle, point cloud, vertex |
| Line / segment | 1D | Wire, path, debug ray |
| **Triangle** | 2D (planar) | Every rendered surface |
| Quad | 2D | Screen, card, floor tile |
| Polygon | 2D | Modelling base (triangulated before rendering) |
| Cube / box | 3D | Blockout, crate, wall |
| Sphere | 3D | Ball, planet, LOD proxy |
| Capsule | 3D | Character, limb, collider |
| Cylinder | 3D | Pipe, pillar, barrel |
| Cone | 3D | Spike, spotlight volume |
| Plane | 2D surface | Ground, wall |
| Torus | 3D | Ring, tyre |

**In Unity:** `GameObject → 3D Object → Cube / Sphere / Capsule / Cylinder / Plane / Quad`. These are ready-made primitive meshes.

**In VR modelling, primitives are used for:**

- **Greyboxing / blockout** — build the level with cubes and planes to test scale, flow and gameplay before art.
- **Colliders** — a box or capsule collider is much cheaper than a mesh collider.
- **LOD proxies** — a sphere or box stands in for a complex object at a distance.
- **Procedural generation** — primitives are combined and instanced at runtime.
- **Base meshes** — sculpt or subdivide a cube/sphere into a detailed model.

### Why triangles are commonly used

A **triangle** is a polygon with three vertices and three edges. It is the simplest possible 2D surface primitive.

1. **Three points are always coplanar.**
   Any three non-collinear points define exactly one plane. A triangle is therefore guaranteed to be flat, so its **normal** is well-defined and lighting is unambiguous. A quad with four non-coplanar vertices has no single plane.

2. **A triangle is always convex.**
   There is no concave fill rule, no self-intersection and no ambiguity about what is inside. Rasterization is simple and robust.

3. **Any polygon can be triangulated.**
   A quad, pentagon or arbitrary n-gon can be split into triangles (fan, ear clipping, Delaunay). So a triangle is a **universal** representation: if you can model it, you can triangulate it.

4. **GPUs are built for triangles.**
   The graphics pipeline — vertex processing, clipping, rasterization, interpolation, depth testing — is optimized for triangles. Hardware processes millions of triangles in parallel.

5. **Affine interpolation is exact.**
   Barycentric coordinates interpolate UVs, vertex colours, normals and depth **linearly** across a triangle. This gives correct texture mapping and smooth shading.

6. **Simple, fast intersection tests.**
   Ray–triangle, triangle–triangle, sphere–triangle and frustum–triangle tests are well understood and cheap. Collision detection and picking are faster than with splines or NURBS.

7. **Well-defined front and back faces.**
   The **winding order** (clockwise or counter-clockwise) defines the front face, enabling **back-face culling** — a major performance saving.

8. **Easy to transform and animate.**
   Transforming the vertices transforms the whole mesh; edges and faces follow. Skinning, morph targets and procedural deformation all operate on vertices.

9. **Universal tool and format support.**
   Every engine, GPU, modelling tool and file format (FBX, glTF, OBJ) supports triangles. There is no compatibility risk.

10. **Good approximation with enough triangles.**
    A curved surface (sphere, car body, character) can be approximated arbitrarily well by increasing the triangle count — a simple **level-of-detail** knob.

### Diagram — a quad is two triangles

```
Quad                          Two triangles
v0 ─────── v1                 v0 ─────── v1
 │         │        ──►        │  ╲      │
 │         │                   │    ╲    │
v3 ─────── v2                 v3 ─────── v2
                              (v0,v1,v2) and (v0,v2,v3)
```

A quad may be non-planar (the four vertices do not lie in one plane), which makes its normal and rasterization ambiguous. Splitting it into two triangles removes the ambiguity.

### Limitations of triangles

- **Curved surfaces need many triangles** — a smooth sphere is an approximation; silhouettes can look faceted.
- **File size and memory** grow with triangle count.
- **Not ideal for CAD** — NURBS and parametric surfaces represent exact curves; meshes are approximations.
- **Thin geometry** can cause z-fighting and intersection artifacts.
- **High triangle counts** hurt VR performance, where the scene is rendered twice (stereo) at 90–120 Hz.

### Conclusion

Geometric primitives are the simple shapes from which 3D models are built. The **triangle** is the preferred primitive because it is **planar, convex, universal, GPU-native, easy to interpolate and cheap to test**, and any surface can be approximated with enough of them. In VR, this simplicity is also a performance necessity: fewer triangles, fewer draw calls and stable frame rate are critical for comfort.

---

## Q2a. Identify the types of vection and explain the role each type plays in helping users perceive self-motion within a VR environment.

**M: 06 | CO: 2 | BT: 2**

> **Examiner expects:** a definition of vection, a clear classification (at least linear, circular and roll), and the role of each type in self-motion perception. Add the VR comfort implication for full marks.

### Definition

**Vection** is the **illusion of self-motion** produced by visual stimuli — usually **optic flow** — when the observer is actually stationary or moving differently. The visual system interprets large, coherent motion of the environment as movement of the self.

**Classic example:** sitting in a stationary train and seeing the train on the next track move; you feel your own train moving in the opposite direction.

**Optic flow:** the pattern of apparent motion of surfaces, edges and textures across the visual field. Forward motion produces flow **radiating outward** from a **focus of expansion**; backward motion produces flow **contracting inward**; rotation produces a **global sweep**; tilt produces a **rotating flow field**.

### Types of vection

| Type | Axis / motion | What the user sees | Role in self-motion perception |
|---|---|---|---|
| **Linear vection** (translational) | Forward/backward, left/right, up/down | Optic flow radiating from a focus of expansion (forward) or contracting (backward) | Simulates walking, driving, flying; gives **speed and direction** cues; foundation of smooth locomotion |
| **Circular vection** (rotational / yaw) | Rotation about the vertical (Y) axis | Environment sweeping horizontally around the user | Simulates **turning**; used in swivel chairs, turntables, smooth turn; strong sickness trigger |
| **Roll vection** | Rotation about the line of sight (Z) | World tilting/rolling around the view axis | Simulates **banking/tilting**; aircraft roll, motorcycle lean; can be very uncomfortable |
| **Curvilinear vection** | Combination of linear + circular | Curved optic flow | Simulates **turning while moving** — driving around a corner, running along a curve |
| **Vertical vection** | Up/down translation | Floor/ceiling moving vertically | Simulates **elevators, falling, flying**; contributes to height sensation |

Some texts also distinguish:

- **Self-vection** — the observer feels they are moving.
- **Object-relative vection** — a moving object induces a sense of self-motion (e.g. a large moving train).
- **Illusory vection** — vection without any physical motion (the normal VR case).

### Role of each type in VR

**1. Linear vection — the basis of locomotion.**
When the user presses forward on a joystick, the virtual world flows toward them. The **focus of expansion** tells them where they are heading, and the **rate of flow** tells them how fast. This is what makes smooth locomotion feel like walking or driving rather than sliding a camera. It is the most common form of vection in VR.

**2. Circular vection — turning and orientation.**
When the world rotates around the user, they feel they are turning. This supports **smooth turning**, turntables, vehicle steering and standing rotations. It also helps the user maintain a sense of which way is "forward" after a turn. Because it strongly stimulates the vestibular system's expectation of rotation, it is a major cause of **motion sickness** when the body is still.

**3. Roll vection — tilt and banking.**
When the horizon rotates, the user feels they are tilting or banking. This is useful in **flight simulators**, motorcycle/racing games and zero-gravity experiences. It is also one of the most nauseating forms of vection because the vestibular system is very sensitive to head tilt, and a mismatch with gravity is especially disturbing.

**4. Curvilinear vection — natural curved paths.**
Real walking and driving rarely follow a perfect straight line. Curvilinear flow combines translation and rotation, giving a more natural sense of **moving along a curve**. It is used in rail shooters, racing lines and cinematic camera paths.

**5. Vertical vection — height and elevators.**
Vertical flow gives a sense of rising or falling. It is used in lifts, drops, flying and platforming. It can be strong and disorienting, especially if the visual acceleration does not match the body.

### What increases or reduces vection

**Increases:**

- Wide **field of view**, especially **peripheral vision**.
- Coherent, **textured** optic flow (not random noise).
- **Depth cues** — motion parallax, stereo, occlusion.
- High **contrast** and **luminance**.
- **Low latency** and **high frame rate**.
- A **large moving area** — a full-screen environment is stronger than a small window.
- **Attention** — looking at the flow increases it.

**Reduces:**

- A **static frame** (cockpit, dashboard, virtual nose) — gives a stable reference.
- **Narrow FOV** or a small moving window.
- **Low texture** / low contrast.
- **Incongruent motion** — different parts of the scene move differently.
- **Physical motion** that matches the visual motion (then it is not illusory).

### VR design implications

- **Vection is necessary** for smooth locomotion — without it, the user feels like a floating camera.
- **Strong vection without vestibular match causes sickness.** The eyes say "moving", the inner ear says "still".
- **Comfort techniques** modulate vection: teleport removes it; snap turn replaces continuous circular vection with a discrete jump; a **vignette** reduces peripheral flow; a **cockpit** provides a static frame.
- **Presence** improves when vection is coherent and matches the user's intention (e.g. pushing forward really moves forward).
- **Vehicle simulators** exploit vection heavily; a cockpit frame makes it tolerable.

### Conclusion

Vection is the **visually induced illusion of self-motion**. Its main types — **linear, circular, roll, curvilinear and vertical** — correspond to translation, yaw rotation, tilt, curved paths and vertical motion. In VR they provide the **speed, direction and orientation cues** that make locomotion feel real, but they must be designed carefully because strong vection without matching physical motion is a primary cause of **simulator sickness**.

---

## Q2b. Elaborate on the concept of Vestibular System in VR with a suitable example.

**M: 04 | CO: 2 | BT: 2**

> **Examiner expects:** what the vestibular system is, its parts and what they detect, one concrete VR example, and the sensory-conflict/sickness implication.

### What is the vestibular system?

The **vestibular system** is the sensory system in the **inner ear** that detects **head acceleration and orientation** and helps control **balance, posture and eye movements**. It works together with **vision** and **proprioception** (muscle/joint feedback) to tell the brain where the body is and how it is moving.

### Anatomy and function

| Part | Location | What it detects | Role |
|---|---|---|---|
| **Semicircular canals** (3, roughly orthogonal) | Inner ear | **Angular acceleration** (rotation) | Detect yaw, pitch and roll of the head |
| **Utricle** (otolith organ) | Inner ear | **Linear acceleration** (horizontal) and head tilt relative to gravity | Detect sideways/forward motion and tilt |
| **Saccule** (otolith organ) | Inner ear | **Linear acceleration** (vertical) and gravity | Detect up/down motion and tilt |
| **Hair cells** | Inside the canals and otolith organs | Deflection of stereocilia | Convert mechanical motion into nerve signals |
| **Vestibulo-ocular reflex (VOR)** | Brainstem / eyes | — | Stabilises the eyes during head movement so the image stays sharp |

**How it works:**

1. When the head rotates, fluid in the **semicircular canals** lags behind and bends the **cupula**, deflecting hair cells.
2. When the head accelerates linearly or tilts, tiny calcium-carbonate crystals (**otoconia**) in the **utricle and saccule** lag behind and deflect hair cells.
3. The brain combines these signals with vision and proprioception to estimate **self-motion and orientation**.
4. The **VOR** automatically counter-rotates the eyes during head turns, keeping the visual world stable.

### The problem in VR

In VR, the user's **body is physically stationary** (sitting or standing), but the **virtual world may move**. A screen cannot directly accelerate the inner ear. So:

- The **eyes** see motion (optic flow, vection).
- The **vestibular system** reports no acceleration.
- The **proprioceptive system** reports no body movement.

This is **sensory conflict**. The brain interprets the contradiction as a sign of poisoning or instability and produces **simulator sickness / cybersickness**: nausea, dizziness, sweating, headache, fatigue and loss of balance.

### Suitable example

**Example — seated VR roller coaster / forward motion:**

A user sits in a chair and wears a head-mounted display. The virtual scene shows them moving forward through a tunnel at high speed (strong **linear vection**). Their eyes receive a wide, coherent optic flow that says "I am moving forward". However, their **semicircular canals** detect no rotation and their **otolith organs** detect no linear acceleration — the body is still. The brain receives contradictory signals, and after a short time the user feels dizzy, sweaty and nauseated. This is a classic **visual–vestibular mismatch**.

**Contrast — comfortable case:**

The same user physically turns their head. The headset updates the view immediately. The **visual** motion and the **vestibular** rotation match, so the user feels comfortable and immersed. Similarly, **room-scale walking** moves the body and the view together, so there is no conflict.

**Another example — real moving platform vs virtual world:**

If the user stands on a real motion platform that tilts/moves while the virtual scene shows a matching tilt, the vestibular and visual signals agree and the experience is convincing. If the platform moves but the scene stays still (or vice versa), the mismatch causes disorientation and imbalance.

### Implications for VR design

1. **Prefer room-scale walking** — real motion matches virtual motion.
2. **Use teleport** — it avoids continuous vection and the associated conflict.
3. **Use snap turn** — a discrete rotation is less provocative than continuous yaw.
4. **Avoid forced acceleration**, roller-coaster motion and camera shake.
5. **Add a stable frame** — a cockpit, dashboard or virtual nose gives the visual system a static reference.
6. **Reduce latency** and **maintain 90/120 FPS** — lag between head motion and image is itself a mismatch.
7. **Provide comfort settings** — speed, turn mode, vignette strength, seated/standing.
8. **Allow breaks** and gradual exposure — users build tolerance over time.

### Conclusion

The vestibular system is the inner-ear sense of **rotation, linear acceleration and orientation**, working with vision and proprioception to keep us balanced. In VR it is the system that **cannot be fooled by a screen**: when the visuals move but the body does not, the result is **sensory conflict and motion sickness**. Understanding it is the key to designing comfortable locomotion, camera motion and interactions.

---

## Q3a. Explain how viewing transformation converts objects from world coordinates to screen coordinates in VR and describe the main steps involved.

**M: 06 | CO: 2 | BT: 2**

> **Examiner expects:** the full pipeline (model → world → camera → clip → NDC → screen), the look-at/view matrix, projection and perspective divide, and a diagram.

### Overview

A model is authored in **local coordinates**. To draw it, the renderer must find where each vertex appears on the screen. The **viewing transformation** is the part that converts **world coordinates → camera/eye coordinates**; the **projection** then converts **eye coordinates → clip/NDC**, and the **viewport transform** maps NDC to pixels.

```
Local coordinates
   │  Model matrix M (T·R·S)
   ▼
World coordinates
   │  View matrix V (look-at)
   ▼
Camera / eye coordinates
   │  Projection matrix P
   ▼
Clip coordinates
   │  Perspective divide (÷ w)
   ▼
NDC
   │  Viewport transform
   ▼
Screen pixels
```

The combined transform is:

$$
v_{clip} = P \cdot V \cdot M \cdot v_{local}
$$

### Step 1 — Model transform: local → world

Each object has a **model matrix** $M$ built from its translation, rotation and scale:

$$
M = T \cdot R \cdot S
$$

Applying $M$ to a local vertex places it in the world:

$$
v_{world} = M \cdot v_{local}
$$

For a child object, $M$ includes the parent chain: $M_{world} = M_{parent} \cdot M_{local}$.

### Step 2 — View transform: world → camera

The camera is defined by:

- **eye position** $e$ — where the camera is,
- **look-at point** $c$ — what it points at,
- **up vector** $u$ — which way is up.

Build an **orthonormal basis** (standard graphics convention, camera looks down −Z):

$$
\hat{z} = \frac{-(c-e)}{\|c-e\|}, \qquad
\hat{x} = \frac{u \times \hat{z}}{\|u \times \hat{z}\|}, \qquad
\hat{y} = \hat{z} \times \hat{x}
$$

- $\hat{z}$ points **backward** (out of the screen).
- $\hat{x}$ points **right**.
- $\hat{y}$ points **up**.

The **view matrix** is:

$$
V =
\begin{bmatrix}
\hat{x}_x & \hat{x}_y & \hat{x}_z & -\hat{x}\cdot e \\
\hat{y}_x & \hat{y}_y & \hat{y}_z & -\hat{y}\cdot e \\
\hat{z}_x & \hat{z}_y & \hat{z}_z & -\hat{z}\cdot e \\
0 & 0 & 0 & 1
\end{bmatrix}
$$

It first translates the camera to the origin and then rotates the world into the camera's axes:

$$
p_{eye} = V \cdot p_{world}
$$

After this, the camera is at the origin looking down −Z; everything in front of it has **negative z**.

> **Unity note:** Unity is left-handed and its camera looks down **+Z**. `Camera.worldToCameraMatrix` handles the sign flip internally. State your convention in the exam.

### Step 3 — Projection: camera → clip

The **perspective projection** makes distant objects smaller. Given vertical FOV $\text{fov}_y$, aspect ratio $a = W/H$, near plane $n$ and far plane $f$:

$$
t = \frac{1}{\tan(\text{fov}_y/2)}
$$

$$
P =
\begin{bmatrix}
\frac{t}{a} & 0 & 0 & 0 \\
0 & t & 0 & 0 \\
0 & 0 & \frac{f+n}{n-f} & \frac{2fn}{n-f} \\
0 & 0 & -1 & 0
\end{bmatrix}
$$

$$
v_{clip} = P \cdot v_{eye}
$$

**What the matrix does:**

- $t/a$ and $t$ set the horizontal and vertical field of view.
- The third row maps the near and far planes to the depth range.
- The $-1$ in the fourth row puts $-z_{eye}$ into $w$, creating the **perspective divide**.
- **Orthographic projection** replaces the perspective terms with a linear mapping; objects do not shrink with distance (used for 2D, isometric, CAD).

### Step 4 — Perspective divide: clip → NDC

The GPU divides by $w$:

$$
x_{ndc} = \frac{x_{clip}}{w_{clip}}, \qquad
y_{ndc} = \frac{y_{clip}}{w_{clip}}, \qquad
z_{ndc} = \frac{z_{clip}}{w_{clip}}
$$

For a point in front of the camera, $w_{clip} = -z_{eye} > 0$. Distant points have larger $w$, so their $x$ and $y$ shrink toward the centre — this is **perspective foreshortening**.

NDC is typically:

$$
x_{ndc}, y_{ndc} \in [-1, 1], \qquad z_{ndc} \in [-1, 1] \text{ (OpenGL)}
$$

(DirectX/Unity use $z_{ndc} \in [0,1]$.)

### Step 5 — Viewport transform: NDC → screen

$$
x_{screen} = \frac{x_{ndc}+1}{2} W, \qquad
y_{screen} = \frac{1-y_{ndc}}{2} H
$$

The $1-y_{ndc}$ flips y because screen coordinates usually have the origin at the **top-left**, while NDC has y up.

**Depth:** $z_{ndc}$ is stored in the **depth buffer**; the GPU keeps the nearest fragment. Near/far precision is non-linear, so do not set the near plane too small.

### Step 6 — Clipping and rasterization

Before rasterization, the GPU **clips** primitives against the view frustum:

$$
-w \le x_{clip} \le w, \quad
-w \le y_{clip} \le w, \quad
-w \le z_{clip} \le w
$$

Triangles completely inside are kept; those outside are discarded; those crossing a plane are clipped and re-triangulated. Then the GPU **rasterizes** each triangle into fragments, interpolates attributes (UV, colour, normal, depth), runs the fragment shader and performs the **depth test** and blending.

### Worked example

Camera at $e=(0,0,5)$, looking at $c=(0,0,0)$, up $u=(0,1,0)$. FOV 90°, aspect 1, near 0.1, far 100.

1. Forward $f=(0,0,-1)$; $\hat{z}=(0,0,1)$; $\hat{x}=(1,0,0)$; $\hat{y}=(0,1,0)$.
2. View matrix translates by $(0,0,-5)$: world point $(0,0,0)$ becomes eye point $(0,0,-5)$.
3. $t = 1/\tan(45°) = 1$.
4. For a point $(1,0,0)$: eye space $(1,0,-5)$.
5. Clip: $x_{clip}=1$, $w_{clip}=5$.
6. NDC: $x_{ndc}=1/5=0.2$, $y_{ndc}=0$.
7. Screen (1920×1080): $x = (0.2+1)/2 \cdot 1920 = 1152$, $y = (1-0)/2 \cdot 1080 = 540$.

So a point 1 unit to the right of the camera axis, 5 units away, appears 20% of the way from centre to the right edge — exactly what perspective should do.

### VR-specific points

- **Stereo:** each eye has a slightly different view. The **IPD** (~63 mm) offsets the cameras left/right, and the projection matrices are **asymmetric frusta**. The parallax between the two images is read as depth.
- **Two projections per frame:** $P_{left}$ and $P_{right}$; the model and view chain is shared.
- **Single-pass instanced stereo** renders both eyes in one pass.
- **Lens distortion / chromatic aberration correction** is applied after projection.
- **Late latching / reprojection** adjusts for the latest head pose.
- **Wide FOV** (~90–110°) and **high frame rate** (90/120 Hz) are required for presence and comfort.

### Conclusion

The viewing transformation is the **world → camera** step of the rendering pipeline. Together with the **model transform**, **projection**, **perspective divide** and **viewport transform**, it converts a local vertex into a screen pixel. In VR the same chain is executed **twice per frame**, once per eye, with a small IPD offset and asymmetric projection to produce stereoscopic depth.

---

## Q3b. Illustrate the importance of Degree of Freedom in VR.

**M: 04 | CO: 2 | BT: 2**

> **Examiner expects:** a definition, the 3-DOF vs 6-DOF distinction, and 6–8 concrete reasons why DOF matters in VR.

### Definition

**Degrees of Freedom (DOF)** are the **independent parameters** that define the **position and orientation** of an object in space. In VR, DOF is the number of independent movements of the user's body (or controller) that the tracking system can measure and reproduce in the virtual world.

- **3 DOF** — **orientation only**: yaw, pitch, roll. The system knows which way the head is facing but not where it is. Rotational tracking only.
- **6 DOF** — **position + orientation**: $x, y, z$ translation plus yaw, pitch, roll rotation. The system knows both where the head is and which way it faces.

$$
\underbrace{x, y, z}_{\text{position}} + \underbrace{\text{yaw, pitch, roll}}_{\text{orientation}} = 6 \text{ DOF}
$$

**Tracking hardware:**

- A **3-DOF HMD** uses gyroscopes/accelerometers to track rotation only (early mobile VR, Google Cardboard).
- A **6-DOF HMD** adds positional tracking via cameras, base stations, inside-out SLAM or magnetic/ultrasonic sensors (Quest, Vive, Index, PSVR2).
- **6-DOF controllers** track hand position and orientation; **3-DOF controllers** track rotation only.
- A **9-DOF IMU** has 3 accelerometer + 3 gyroscope + 3 magnetometer axes, but the **motion** it describes is still 6 DOF (3 position + 3 orientation); the extra axes are sensor channels, not additional motion DOF.

### Importance of DOF in VR

1. **Determines the level of immersion.**
   More DOF means more of the user's real body movement is reflected in the virtual world. With 6 DOF, leaning, crouching, stepping and peeking all work; with 3 DOF, the world feels like a panoramic video rather than a place.

2. **Enables natural, intuitive interaction.**
   With 6 DOF the user reaches, grabs, pushes, ducks and walks naturally. With 3 DOF they must use artificial locomotion (joystick/teleport) because real head translation is ignored, which feels unnatural.

3. **Correct size, scale and depth perception.**
   Position tracking lets the user move their head to get **motion parallax** — the relative shift of near and far objects. This is a powerful depth cue and is essential for judging distance and scale. Without it, objects can look flat or the wrong size.

4. **Enables accurate 3D manipulation.**
   Selecting, grabbing, rotating and placing objects in 3D requires both **position** (where the hand is) and **orientation** (how the hand is turned). 6-DOF controllers make this possible.

5. **Reduces VR sickness.**
   A mismatch between the real head movement (6 DOF) and the rendered motion (3 DOF) creates **visual–vestibular conflict** → cybersickness. Adequate DOF keeps the visual and vestibular signals consistent. Positional tracking also lets the user physically move instead of using artificial locomotion, which is more comfortable.

6. **Enables freedom of navigation.**
   The user can physically walk, look up/down, lean around corners and peek behind objects. This improves spatial understanding and makes the environment feel solid.

7. **Supports precise, high-value tasks.**
   Surgical simulation, CAD review, assembly training and teleoperation need millimetre-accurate hand position and orientation. 6 DOF is the minimum for these tasks.

8. **Improves tracking robustness and calibration.**
   Sensor fusion cross-checks optical and inertial DOFs, reducing **drift**. Independent DOFs can be measured directly (e.g. 3 translation axes from 3 cameras); dependent DOFs are derived (e.g. roll from an accelerometer + magnetometer in a 3-DOF IMU).

9. **Enables believable avatars and gestures.**
   High-DOF hand, head and body tracking make sign language, pointing, waving and social presence believable. Avatars can mirror the user's real pose.

10. **Defines the input device and interaction vocabulary.**
    A 3-DOF controller can only point/rotate; a 6-DOF controller can grab, throw, swing, aim and manipulate. The DOF of the hardware determines what interactions a VR app can offer.

### 3-DOF vs 6-DOF — comparison

| Basis | **3 DOF** | **6 DOF** |
|---|---|---|
| Tracks | Rotation only (yaw, pitch, roll) | Rotation + position (x, y, z) |
| Head translation | Ignored | Tracked |
| Depth cues | Weak (no motion parallax) | Strong (motion parallax) |
| Interaction | Pointing, looking | Grabbing, reaching, throwing, manipulating |
| Comfort | Can be comfortable (no artificial translation) but limited | More natural; can be uncomfortable if locomotion is artificial |
| Example devices | Google Cardboard, early mobile VR, 3-DOF trackers | Meta Quest, HTC Vive, Valve Index, PSVR2, 6-DOF controllers |
| Immersion | Limited | High |

### Conclusion

DOF is the **number of independent motions** a VR system can track and reproduce. **6 DOF** — three translations plus three rotations — is the standard for immersive VR because it gives **natural movement, correct depth and scale, accurate manipulation, better comfort and stronger presence**. A VR system with fewer DOF than the user's real movement inevitably feels limited and can cause sensory mismatch.

---

## Q4. Write a program that enables an object to rotate smoothly in response to mouse inputs.

**M: 10 | CO: 3 | BT: 3**

> **Examiner expects:** a complete C# `MonoBehaviour`, `Input.GetAxis("Mouse X"/"Mouse Y")`, rotation application, smoothing, and an explanation of each line. Add pitch clamping, cursor lock and the reason for `Time.deltaTime`/smoothing for full marks.

### 4.1 Requirement

The object should:

1. Read horizontal and vertical mouse movement.
2. Rotate around the **Y axis** (yaw) and **X axis** (pitch).
3. Respond **smoothly**, not in jerky steps.
4. Avoid rolling or flipping over.
5. Be frame-rate independent.

### 4.2 Simple version (the core idea)

```csharp
using UnityEngine;

public class SimpleMouseRotate : MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        transform.Rotate(Vector3.up, mouseX * speed, Space.World);
        transform.Rotate(Vector3.right, -mouseY * speed, Space.Self);
    }
}
```

**How it works:**

- `Input.GetAxis("Mouse X")` returns the horizontal mouse delta for this frame.
- `Input.GetAxis("Mouse Y")` returns the vertical delta.
- `transform.Rotate(axis, angle, space)` applies the rotation.
- `Space.World` for yaw keeps the object turning around the world's vertical axis.
- `Space.Self` for pitch tilts around the object's local right axis.
- The minus sign on `mouseY` makes moving the mouse up look up (screen y increases downward).

**Limitations:** repeated `Rotate` calls can accumulate unwanted **roll**, and there is no pitch clamp. The robust version below fixes both.

### 4.3 Smooth, robust version (recommended for 10 marks)

```csharp
using UnityEngine;

public class SmoothMouseRotate : MonoBehaviour
{
    [Header("Sensitivity")]
    [Tooltip("Degrees of rotation per unit of mouse movement")]
    public float sensitivity = 3.0f;

    [Header("Smoothing")]
    [Range(1f, 30f)]
    [Tooltip("Higher = snappier, lower = smoother")]
    public float smoothing = 12f;

    [Header("Pitch Limits")]
    public float minPitch = -80f;
    public float maxPitch = 80f;

    private float targetYaw;
    private float targetPitch;
    private float currentYaw;
    private float currentPitch;

    void Start()
    {
        // Start from the object's current orientation
        Vector3 e = transform.eulerAngles;
        targetYaw = currentYaw = e.y;
        targetPitch = currentPitch = NormalizeAngle(e.x);

        // Lock the cursor to the game window
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // 1. Read mouse input (already a per-frame delta)
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        // 2. Update the target angles
        targetYaw += mouseX;
        targetPitch = Mathf.Clamp(targetPitch - mouseY, minPitch, maxPitch);

        // 3. Smoothly move the current angles toward the target
        float t = 1f - Mathf.Exp(-smoothing * Time.deltaTime);
        currentYaw = Mathf.Lerp(currentYaw, targetYaw, t);
        currentPitch = Mathf.Lerp(currentPitch, targetPitch, t);

        // 4. Apply the rotation (no roll)
        transform.rotation = Quaternion.Euler(currentPitch, currentYaw, 0f);
    }

    void OnDisable()
    {
        // Release the cursor when the object/script is disabled
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Convert 0..360 to -180..180 so pitch starts correctly
    static float NormalizeAngle(float angle)
    {
        angle %= 360f;
        if (angle > 180f) angle -= 360f;
        return angle;
    }
}
```

### 4.4 Line-by-line explanation

| Line / field | Purpose |
|---|---|
| `sensitivity` | How many degrees the object turns per unit of mouse movement. Higher = faster. |
| `smoothing` | How quickly the current rotation catches up with the target. Higher = snappier. |
| `minPitch` / `maxPitch` | Prevent the object from flipping over at ±90°. |
| `targetYaw` / `targetPitch` | The orientation the mouse is asking for. |
| `currentYaw` / `currentPitch` | The orientation actually applied this frame. |
| `Input.GetAxis("Mouse X")` | Horizontal mouse delta (per frame). |
| `targetYaw += mouseX` | Accumulate yaw so the object can keep turning. |
| `targetPitch = Mathf.Clamp(...)` | Accumulate pitch but keep it within limits. |
| `Mathf.Exp(-smoothing * Time.deltaTime)` | Frame-rate-independent smoothing factor. |
| `Mathf.Lerp(current, target, t)` | Move a fraction `t` of the way to the target each frame. |
| `Quaternion.Euler(pitch, yaw, 0)` | Build the final rotation with **zero roll**. |
| `Cursor.lockState = Locked` | Keeps the mouse inside the window and hides the cursor. |
| `OnDisable()` | Releases the cursor when the script/object is disabled. |

### 4.5 Why this is smooth

1. **Separate target and current angles.**
   The mouse updates the **target** instantly; the object moves toward it gradually. This removes jitter and gives a natural "weight" to the rotation.

2. **Frame-rate-independent smoothing.**
   `t = 1 - exp(-k·dt)` produces the same visual smoothing at 30, 60, 90 or 120 FPS. A plain `Lerp(a, b, 0.1f)` would be faster at high frame rates because it is applied more often.

3. **No `Time.deltaTime` on the raw mouse delta.**
   `Input.GetAxis("Mouse X")` is already a per-frame delta. Multiplying it by `deltaTime` again makes sensitivity depend on frame rate. Use `deltaTime` for the **smoothing**, not for the input.

4. **Yaw and pitch are separated.**
   Yaw accumulates around the world Y axis; pitch is clamped around the local X axis. This prevents **roll** and the "camera flip" that happens when you rotate around both axes with `transform.Rotate` repeatedly.

5. **Quaternion output.**
   `Quaternion.Euler` produces a clean orientation without gimbal-lock artifacts in the applied transform, and Unity stores it internally as a quaternion.

6. **Clamped pitch.**
   The object can look up and down but never past vertical, so the view never inverts.

### 4.6 Steps to run in Unity

1. Create an object: `GameObject → 3D Object → Cube` (or use the Main Camera).
2. Create a script named **`SmoothMouseRotate.cs`** (the file name must match the class name).
3. Paste the code and save.
4. Drag the script onto the object (or `Add Component`).
5. Press **Play**.
6. Move the mouse — the object rotates smoothly; horizontal movement yaws, vertical movement pitches.
7. In the editor, press **Esc** to release the cursor; the `OnDisable()` method also releases it when the object/script is disabled.

### 4.7 Alternative — rotate a Rigidbody with torque

If the object is physical and should respond with inertia:

```csharp
public class PhysicsMouseRotate : MonoBehaviour
{
    public float torque = 10f;
    private Rigidbody rb;

    void Awake() => rb = GetComponent<Rigidbody>();

    void FixedUpdate()
    {
        float mx = Input.GetAxis("Mouse X");
        float my = Input.GetAxis("Mouse Y");

        rb.AddTorque(Vector3.up * mx * torque, ForceMode.VelocityChange);
        rb.AddTorque(Vector3.right * -my * torque, ForceMode.VelocityChange);
    }
}
```

Use this for a floating object in space, not for a first-person camera (the camera should be kinematic).

### 4.8 Alternative — new Input System

```csharp
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseRotateNewInput : MonoBehaviour
{
    public float sensitivity = 0.2f;

    private Vector2 look;

    public void OnLook(InputValue value)
    {
        look = value.Get<Vector2>();
    }

    void Update()
    {
        transform.Rotate(Vector3.up, look.x * sensitivity, Space.World);
        transform.Rotate(Vector3.right, -look.y * sensitivity, Space.Self);
    }
}
```

Bind the `Look` action to `<Mouse>/delta` in the Input Actions asset.

### 4.9 Alternative — touch drag (mobile AR/VR)

```csharp
void Update()
{
    if (Input.touchCount == 1)
    {
        Touch t = Input.GetTouch(0);
        if (t.phase == TouchPhase.Moved)
        {
            Vector2 delta = t.deltaPosition;
            transform.Rotate(Vector3.up, delta.x * sensitivity, Space.World);
            transform.Rotate(Vector3.right, -delta.y * sensitivity, Space.Self);
        }
    }
}
```

### 4.10 Common mistakes

| Mistake | Problem | Fix |
|---|---|---|
| Multiplying mouse delta by `Time.deltaTime` | Sensitivity depends on frame rate | Do not multiply the raw delta; use `deltaTime` only for smoothing |
| Rotating around both axes with repeated `transform.Rotate` | Accumulates roll / flips | Use yaw + pitch state and `Quaternion.Euler` |
| No pitch clamp | Object flips upside down at ±90° | `Mathf.Clamp` |
| Using `Input.GetAxis` in `FixedUpdate` | Missed input | Read in `Update` |
| Forgetting `Cursor.lockState` | Mouse leaves the window | Lock and hide the cursor |
| Using `transform.rotation` on a Rigidbody | Physics jitter | Use `MoveRotation` / `AddTorque` |

### Conclusion

The program reads `Mouse X` and `Mouse Y`, accumulates **yaw** and **pitch**, clamps the pitch, smoothly interpolates the current orientation toward the target with a frame-rate-independent factor, and applies the result as a **quaternion with zero roll**. The result is a natural, smooth mouse-controlled rotation suitable for inspecting objects, first-person cameras and VR/AR interaction previews.

---

## Q5. Illustrate the matrix multiplication algorithm iterated over the chain of matrices to produce the Chaining of Transformations.

**M: 10 | CO: 2 | BT: 2**

> **Examiner expects:** the concept of chaining, the matrix multiplication rule, the iterative algorithm (loop over the chain), a worked example, and the rendering pipeline / importance.

### 5.1 Concept

A vertex usually needs several transformations: **scale → rotate → translate → view → project → viewport**. Applying each matrix separately to every vertex is expensive:

$$
v' = T_{vp}(T_{can}(T_{eye}(T_{rb} v)))
$$

Instead, multiply the matrices **once** into a single combined matrix, then apply that one matrix to every vertex:

$$
T = T_{vp} \cdot T_{can} \cdot T_{eye} \cdot T_{rb}
$$

$$
v' = T v
$$

This is **chaining the transformations**. For a mesh with $V$ vertices and $n$ transforms, the naive cost is $O(Vn)$; the chained cost is $O(V+n)$.

### 5.2 Matrix multiplication rule

For two 4×4 matrices $A$ and $B$, the product $C = AB$ is:

$$
C_{ij} = \sum_{k=1}^{4} A_{ik} B_{kj}
$$

i.e. the **dot product of row $i$ of $A$ and column $j$ of $B$**.

**Properties:**

- **Associative:** $(AB)C = A(BC)$ — grouping does not matter.
- **Not commutative:** $AB \neq BA$ — order matters.
- **Identity:** $IA = AI = A$.
- **Inverse:** $(AB)^{-1} = B^{-1}A^{-1}$.

With **column vectors** and $v' = Mv$, transformations are applied **right to left**. So:

$$
v_{world} = T \cdot R \cdot S \cdot v_{local}
$$

means **scale first, then rotate, then translate**.

### 5.3 The iterative algorithm

**Input:** a list of matrices in the order they must be **applied** to a column vector, e.g. $[S, R, T]$ (scale first, then rotate, then translate).

**Goal:** compute $M = T R S$.

**Algorithm A — left-multiply in application order:**

```
M = Identity
for each transform X in [S, R, T]:     // application order
    M = X * M
return M
```

Trace:

| Step | X | M |
|---|---|---|
| 0 | — | $I$ |
| 1 | $S$ | $S$ |
| 2 | $R$ | $R S$ |
| 3 | $T$ | $T R S$ ✓ |

**Algorithm B — right-multiply in reverse order:**

```
M = Identity
for i = n-1 down to 0:                 // reverse application order
    M = M * transforms[i]
return M
```

Trace:

| Step | i | transform | M |
|---|---|---|---|
| 0 | — | — | $I$ |
| 1 | 2 | $T$ | $T$ |
| 2 | 1 | $R$ | $T R$ |
| 3 | 0 | $S$ | $T R S$ ✓ |

**Then apply once per vertex:**

```
for each vertex v:
    v' = M * v
```

**Pseudocode (complete):**

```
function buildAndApply(transforms, vertices):
    M = Identity
    for i = 0 to transforms.length - 1:
        M = transforms[i] * M        // left-multiply
    for each v in vertices:
        v' = M * v
    return vertices
```

**Complexity:** $O(n)$ matrix multiplications to build the chain (each 4×4 multiplication is a constant 64 multiply-adds), then **one** matrix–vector multiply per vertex.

### 5.4 Worked numerical example

Let:

$$
S = \begin{bmatrix}2&0&0&0\\0&2&0&0\\0&0&1&0\\0&0&0&1\end{bmatrix}, \quad
R = R_z(90°) = \begin{bmatrix}0&-1&0&0\\1&0&0&0\\0&0&1&0\\0&0&0&1\end{bmatrix}, \quad
T = \begin{bmatrix}1&0&0&3\\0&1&0&4\\0&0&1&0\\0&0&0&1\end{bmatrix}
$$

**Step 1 — $R \cdot S$:**

$$
R S =
\begin{bmatrix}
0 & -2 & 0 & 0 \\
2 & 0 & 0 & 0 \\
0 & 0 & 1 & 0 \\
0 & 0 & 0 & 1
\end{bmatrix}
$$

**Step 2 — $T \cdot (R S)$:**

$$
M = T R S =
\begin{bmatrix}
0 & -2 & 0 & 3 \\
2 & 0 & 0 & 4 \\
0 & 0 & 1 & 0 \\
0 & 0 & 0 & 1
\end{bmatrix}
$$

**Step 3 — apply to $v = (1,0,0,1)^T$:**

$$
M v =
\begin{bmatrix}
0\cdot1 + (-2)\cdot0 + 0\cdot0 + 3\cdot1 \\
2\cdot1 + 0\cdot0 + 0\cdot0 + 4\cdot1 \\
0\cdot1 + 0\cdot0 + 1\cdot0 + 0\cdot1 \\
1
\end{bmatrix}
=
\begin{bmatrix}
3 \\ 6 \\ 0 \\ 1
\end{bmatrix}
$$

**Check step by step:**

1. Scale: $(1,0,0) \to (2,0,0)$.
2. Rotate 90° about Z: $(2,0,0) \to (0,2,0)$.
3. Translate by $(3,4,0)$: $(0,2,0) \to (3,6,0)$. ✓

### 5.5 C# implementation in Unity

```csharp
using UnityEngine;

public class MatrixChainDemo : MonoBehaviour
{
    public Vector3 translation = new Vector3(3f, 4f, 0f);
    public Vector3 eulerAngles = new Vector3(0f, 0f, 90f);   // pitch, yaw, roll
    public Vector3 scale = new Vector3(2f, 2f, 1f);

    void Start()
    {
        // 1. Build individual matrices
        Matrix4x4 T = Matrix4x4.Translate(translation);
        Matrix4x4 R = Matrix4x4.Rotate(Quaternion.Euler(eulerAngles));
        Matrix4x4 S = Matrix4x4.Scale(scale);

        // 2. Chain them (scale first, then rotate, then translate)
        Matrix4x4 M = T * R * S;

        // 3. Apply the single matrix to a local vertex
        Vector3 local = new Vector3(1f, 0f, 0f);
        Vector3 world = M.MultiplyPoint3x4(local);

        Debug.Log($"Local {local} -> World {world}");   // (1,0,0) -> (3,6,0)

        // 4. Unity's own local-to-world matrix does the same thing
        Debug.Log(transform.localToWorldMatrix);
    }
}
```

**What `MultiplyPoint3x4` does:** multiplies the 4×4 matrix by the point $(x,y,z,1)$ and returns the first three components, without the perspective divide. For a full render pipeline, use the clip-space result and divide by $w$.

### 5.6 Chaining in the full render pipeline

```
v_local
  │  M_model = T · R · S          (local → world)
  ▼
v_world
  │  V = worldToCameraMatrix     (world → camera)
  ▼
v_eye
  │  P = projectionMatrix        (camera → clip)
  ▼
v_clip
  │  ÷ w                          (perspective divide)
  ▼
v_ndc
  │  viewport transform          (NDC → screen)
  ▼
v_screen
```

Combined:

$$
v_{clip} = P \cdot V \cdot M_{model} \cdot v_{local}
$$

$$
v_{ndc} = \frac{v_{clip}}{w_{clip}}, \qquad
v_{screen} = \text{Viewport}(v_{ndc})
$$

**Stereo (VR):**

$$
v_{clip}^{eye} = P_{eye} \cdot V \cdot M_{model} \cdot v_{local}
$$

where $P_{eye}$ uses the left/right eye offset and asymmetric frustum.

### 5.7 Chaining in a scene graph

A scene is a tree of transforms:

```
World
└── Player
    ├── Camera
    ├── LeftHand
    │   └── Sword
    └── RightHand
```

Each node has a **local** transform. The **world** matrix is the product of all ancestors:

$$
M_{world} = M_{root} \cdots M_{parent} \cdot M_{local}
$$

A vertex in the Sword's local space goes to world space by:

$$
v_{world} = M_{Player} \cdot M_{LeftHand} \cdot M_{Sword} \cdot v_{local}
$$

This is why moving the player moves the hands and the sword automatically. The **matrix stack** (push/pop) is the classic implementation: push the parent matrix, multiply by the child's local matrix, draw the child, pop.

### 5.8 Why chaining matters

1. **Performance** — one matrix–vector multiply per vertex instead of $n$; for static objects the matrix is computed once and reused.
2. **Correct order** — the chain encodes the intended order (scale → rotate → translate), which is not obvious from separate operations.
3. **Hierarchies** — child objects inherit parent transforms; the foundation of scene graphs, skeletal animation and VR rigs.
4. **Camera and projection** — the view and projection matrices are just two more links in the chain.
5. **Stereo** — swap the projection matrix per eye without changing the model or view chain.
6. **GPU efficiency** — the vertex shader receives one MVP matrix; the CPU does the chain once per object.
7. **Reuse** — a single matrix can be stored, interpolated, inverted and passed to shaders.
8. **Instancing** — one matrix per instance lets the GPU draw thousands of copies efficiently.

### 5.9 Common mistakes

| Mistake | Result | Fix |
|---|---|---|
| Wrong order (`S R T` instead of `T R S`) | Object scales in world space or rotates around the wrong point | Remember right-to-left application |
| Using row-vector order with column vectors | Transposed result | Match convention |
| Forgetting the perspective divide | No depth/size perspective | Divide by $w$ |
| Transforming normals with the model matrix under non-uniform scale | Wrong lighting | Inverse transpose |
| Recomputing the chain per vertex | Slow | Compute once per object |
| Ignoring parent transforms | Child detaches from parent | Multiply by parent world matrix |
| Mixing degrees and radians | Wild rotations | Convert with `Mathf.Deg2Rad` / `Rad2Deg` |

### Conclusion

Chaining the transformations means **multiplying the chain of matrices into one combined matrix** and applying it to every vertex. The iterative algorithm left-multiplies each transform in application order (or right-multiplies in reverse), producing $M = T R S$ for scale-then-rotate-then-translate. This reduces the per-vertex cost from $n$ multiplications to one, preserves the correct order, and makes **scene hierarchies, camera transforms, projection and stereo rendering** possible. It is the mathematical backbone of the entire real-time rendering pipeline.

---

## Quick revision — one-line answers

| Question | One-line answer |
|---|---|
| **Q1a** | A Prefab is a reusable GameObject template; create it by configuring an object and dragging it into the Project, place it by dragging the asset into a scene or calling `Instantiate()`; it gives reuse, consistency, maintainability and clean scene organisation in VR/AR. |
| **Q1b** | Geometric primitives are basic building-block shapes; triangles are used because they are planar, convex, universal, GPU-native, easy to interpolate and cheap to collide. |
| **Q2a** | Vection is the illusion of self-motion from optic flow; types are linear, circular, roll, curvilinear and vertical, each providing translation, turning, tilt, curved-path or height cues. |
| **Q2b** | The vestibular system is the inner ear's sense of rotation and linear acceleration; in VR, visual motion without body motion causes sensory conflict and sickness. |
| **Q3a** | Viewing transformation converts world → camera (look-at view matrix), then projection → clip → NDC → screen; the main steps are model, view, projection, perspective divide and viewport. |
| **Q3b** | DOF is the number of independent motions; 3 DOF is orientation only, 6 DOF adds position; more DOF gives natural interaction, depth cues, accurate manipulation, comfort and presence. |
| **Q4** | Read `Mouse X/Y`, accumulate yaw and clamped pitch, smooth with a frame-rate-independent Lerp, apply `Quaternion.Euler(pitch, yaw, 0)`. |
| **Q5** | Multiply the transform chain into one matrix ($M = T R S$), then apply it once per vertex; this is the basis of the MVP pipeline, scene graphs and stereo rendering. |

---

*End of CIE-2 PYQ solutions. Companion notes: [[VRAR CIE-2 Notes]].*

---

## Related notes

- [[VRAR MOC]] — subject index and reading order.
- [[VRAR CIE-2 Notes]] — full notes for every answer.
- [[VRAR CIE-1 Notes]] — previous exam: Unit I and early Unit II.
- [[VRAR PYQ CIE-1]] — previous exam: solved CIE-1 paper.
- [[sample progs]] — Unity C# scripts for the programming questions.
