# VR / AR (IS372IA) — PYQ Solutions & Quiz Bank

> **Every question from both real question papers, in paper order, question followed by a complete exam-ready answer.** Followed by a full **quiz-question bank** (MCQs + short answers) built from the same syllabus.
>
> | Paper | Code | Session | Total |
> |---|---|---|---|
> | **PAPER 1** | 21IS72 | Mar/Apr 2025 | 100 marks — Part A (10 Qs) + Part B (Q2–Q10) |
> | **PAPER 2** | IS372IA | Feb/March 2026 | 100 marks — Part A (19 Qs) + Part B (Q2–Q10) |
>
> **Syllabus pattern:** Unit I (Introduction + Bird's-eye view) · Unit II (Programming with Unity) + the later-unit topics that keep appearing in Part B (tracking, 3-D modelling/Blender, WebXR, transforms & meshes, controllers, physics).
>
> **Format of each answer:** the question exactly as printed → **Answer** written the way you should write it in the exam. `M` = marks, `CO` = course outcome, `BT` = Bloom's taxonomy level as printed in the QP.

---
---

# 📄 PAPER 1 — QP 21IS72, Mar/Apr 2025 (RVCE, VII Sem ISE)

## PART A (10 × 02 marks) — answer in the first three pages only

---

### 1.1 — Differentiate between Augmented Reality and Virtual Reality.
**M: 02 | CO: 2 | BT: 1**

**Answer:**

| Basis | **Virtual Reality (VR)** | **Augmented Reality (AR)** |
|---|---|---|
| **Environment** | Fully **computer-generated / virtual** environment | **Real + virtual** — it **enhances** the real world |
| **Real world** | **Blocked / replaced** by the virtual world | **Visible**; awareness of surroundings is maintained |
| **Immersion** | **Complete** immersion | **Partial** immersion |
| **Senses** | Senses are **under the control of the system** | User **keeps a sense of presence in the real world** |
| **Devices** | VR headset (HMD), CAVE, BOOM | Phone, tablet, AR smart glasses |
| **Mechanism needed** | A mechanism to **feed** the virtual world to the user (display + tracking) | A mechanism to **combine** virtual and real worlds (registration, tracking) |
| **How it works** | Information is placed **right in front of the eyes**, eliminating interaction with the real world | Graphics, audio and other enhancements are **superimposed over a real-world scene in real time**, changing to follow the user's hand/head movement |
| **Reality spectrum** | Near **pure virtuality** | Between pure reality and pure virtuality (Milgram's continuum) |
| **Suited for** | **Replacing** surroundings — training, games, simulation | **Real-world task support** — maintenance, navigation, medical |
| **Examples** | Oculus Rift, HTC Vive, PSVR | Pokémon GO, Google Maps AR, Google Glass |

**Key line:** *In VR the real world is completely replaced by a synthetic world; in AR the real world remains and digital information is added on top of it.*

---

### 1.2 — List the different input devices used for Virtual Reality applications.
**M: 02 | CO: 1 | BT: 1**

**Answer:**

1. **Head-mounted display (HMD) with head tracker** — measures head position and orientation to control the virtual camera.
2. **Data gloves** (e.g. **VPL DataGlove**) — fibre-optic sensors with an LED and phototransistor; bending the cable loses light, giving the **finger flex angle**; a **Polhemus sensor** gives hand position/orientation.
3. **Motion controllers** (e.g. HTC Vive wands, Oculus Touch, PS Move) — 6-DOF handheld input with buttons and triggers.
4. **3-D mouse / SpaceBall** — the Logitech 3-D mouse uses **three ultrasonic speakers** in a triangle to track position and gives output in all **6 DOF (X, Y, Z, pitch, yaw, roll)**; the SpaceBall is 6-DOF **force-sensitive** and produces homogeneous transformation matrices.
5. **MIDI keyboard** — 88 keys transmitting keystroke **velocity and pressure**; a general way to enter many DOFs of data simultaneously for real-time animation.
6. **Gesture interfaces** — **Pinch Glove** (conductive fabric + fingertip electrodes, RS232 interface) and **5DT Data Glove** (fibre-optic sensors, wireless transmitter).
7. **Trackers** — **mechanical** (ceiling arm), **optical** (IR cameras + markers), **magnetic/electromagnetic** (base station + sensor), **inertial** (IMU), **ultrasonic**.
8. **Omni-directional treadmills** (Virtix, Cyberith) and **force-feedback / haptic gloves** for locomotion and touch.
9. Also: **eye trackers**, **Leap Motion / Kinect depth cameras**, **Myo EMG armband**, **voice input**, and **keyboard/mouse** for text entry and UI.

*(Note: input devices must sense **finger flex angles** and the **position/orientation of the wrist in real time**.)*

---

### 1.3 — Write any two applications of Mixed Reality.
**M: 02 | CO: 1 | BT: 1**

**Answer — any two:**

**1. Medical training and surgery support.** MR headsets such as Microsoft HoloLens let surgeons **rehearse anatomy and procedures** and display a **virtual patient layered over the real patient** during an actual operation, improving surgical precision without any risk to the patient.

**2. Engineering / product design review.** Engineers can walk through a **full-scale virtual prototype** of an engine or building that is anchored in the physical design space — real parts correctly **occlude** the hologram, and virtual content receives **real-world lighting**, so design reviews can be done before fabrication.

**Other acceptable answers:** remote assistance / field service (a remote expert annotates the local technician's real workspace); manufacturing (animated step-by-step assembly instructions anchored on the real machine); education (holographic 3-D models on a desk); digital twins with live IoT sensor data; remote collaboration (shared holographic workspaces); architecture (full-scale holographic walk-through of a building before construction); entertainment (HoloLens/Minecraft — game characters in the player's living room).

---

### 1.4 — What are the default methods while working with Unity?
**M: 02 | CO: 2 | BT: 1**

**Answer:**

**Default (built-in / magic) methods** are methods whose names Unity recognises and calls **automatically** at the right moment — you do not call them yourself. In order of execution:

| # | Method | When Unity calls it |
|---|---|---|
| 1 | **`Awake()`** | **Once**, when the GameObject is created/instantiated — before `Start()`. Use to initialise variables that do not depend on other objects. |
| 2 | **`OnEnable()`** | When the object becomes **enabled and active**. |
| 3 | **`Start()`** | **Once**, just **before the first frame's `Update()`**, after all `Awake()` calls. Use to set up references and initial state. |
| 4 | **`FixedUpdate()`** | At a **fixed interval** (default **50 times/second**, the physics timestep). Use for **physics** — `AddForce`, Rigidbody movement. |
| 5 | **`Update()`** | **Once per frame** — the main gameplay logic loop (input, non-physics movement, timers). |
| 6 | **`LateUpdate()`** | **Once per frame, after all `Update()`s** — used for **camera follow** and interpolating transform changes. |
| 7 | **`OnGUI()`** | For drawing immediate-mode GUI; runs multiple times per frame. |
| 8 | **`OnDisable()`** | When the object becomes disabled/inactive. |
| 9 | **`OnDestroy()`** | Just before the GameObject is destroyed — clean up, unsubscribe, stop coroutines. |

**Other default (message) methods:** collision/trigger messages — `OnCollisionEnter/Stay/Exit`, `OnTriggerEnter/Stay/Exit`, `OnControllerColliderHit`, `OnParticleCollision`, `OnMouseDown/Up/Over/Exit`, `OnDrawGizmos`, `OnApplicationPause/Quit`, `OnValidate`; and coroutine methods — `StartCoroutine()`, `StopCoroutine()`.

**Example:**
```csharp
using UnityEngine;
public class Example : MonoBehaviour
{
    void Start()   { Debug.Log("Start: called once at startup"); }
    void Update()  { Debug.Log("Update: called every frame"); }
    void LateUpdate() { Debug.Log("LateUpdate: after Update"); }
    void OnDestroy() { Debug.Log("OnDestroy: cleaning up"); }
}
```
**Most commonly asked pair: `Start()` (once at startup) and `Update()` (every frame).**

---

### 1.5 — Compare Edit Mode and Object Mode in Blender.
**M: 02 | CO: 2 | BT: 2**

**Answer:**

In Blender, a **mesh object** can be worked on at two levels — the **whole object** or its **individual parts (vertices, edges, faces)**. The mode is toggled with the **Tab** key (or by clicking the mode selector at the top-left of the 3-D viewport).

| Basis | **Object Mode** (default) | **Edit Mode** |
|---|---|---|
| **What is selected** | The **entire mesh object** as one unit | **Individual vertices, edges and faces** |
| **Editing level** | **Object level** | **Sub-object / vertex level** |
| **Transforms** | Moving/rotating/scaling applies to the **whole object** | Transforms apply to the **selected vertices/edges/faces only** |
| **Operations** | Add, duplicate, rename, apply modifiers, parent, set origin | Move, extrude, inset, bevel, subdivide, merge, delete faces, extrude loops |
| **Use for** | Placing/moving the model in the scene, applying **Sculpting**, **Rigging** and **Armature** work, adding modifiers | **Detailed shaping** of the model — moving specific vertices, refining topology, retopology, UV unwrapping |
| **Typical uses** | Adding a modifier, moving a character, applying **Sculpt** or **Rig** to the whole mesh | Pulling one vertex to fix a bump, cutting a face, extruding, welding |
| **Multiple objects** | Can select and transform **many objects at once** | Edits **one mesh** at a time |
| **Selection display** | Whole object outlined (orange) | Vertices as dots, edges as lines, faces as shaded regions |

**Extra points (for higher marks):**
- **Edit Mode has three sub-modes** — **Vertex**, **Edge**, **Face** (toggled with **1 / 2 / 3** keys).
- **Sculpt Mode** and **Weight Paint / Pose Mode** are separate modes reached from Object Mode.
- Both modes share the same **N-panel / Tool Shelf** on the left, but only Edit Mode gives access to **vertex/edge/face** operators.
- **Common mistake:** pressing **Tab** accidentally and wondering why the model turns into dots — fix by pressing **Tab** again.

---

### 1.6 — Mention different types of Sensor Based Tracking in AR.
**M: 02 | CO: 1 | BT: 1**

**Answer:**

**Sensor-based tracking** (also called *device-based* tracking) uses the **onboard sensors of the AR device** — plus sometimes external data — to estimate the device's **position and orientation**. It is the technique used by **marker-less mobile AR** (ARCore, ARKit, Pokémon GO).

**Types:**

| # | Type | Principle |
|---|---|---|
| 1 | **Optical tracking** | **IR cameras** detect IR LEDs or reflective markers; triangulation gives 3-D position. |
| 2 | **Magnetic tracking** | An **electromagnetic field** from a base station; a sensor in the device measures field strength and orientation. |
| 3 | **Acoustic / ultrasonic tracking** | **Ultrasonic pulses** and receivers measure distance from echo time. |
| 4 | **Inertial tracking** | **Accelerometer + gyroscope (+ magnetometer)** = IMU; rotation and acceleration are integrated (**dead reckoning**). |
| 5 | **Vision-based tracking** | The **camera** detects natural features or markers; **computer vision / SLAM** builds a map and computes the pose. |
| 6 | **Hybrid / sensor-fusion tracking** | All of the above combined by an **Extended Kalman Filter (EKF)** — this is what ARCore/ARKit actually use. |
| 7 | **GPS/GNSS + compass tracking** | Satellites give **global position**; magnetometer gives heading; used for **location-based outdoor AR**. |

**Closing line:** *In practice AR uses **sensor fusion (EKF)** that blends camera (visual), IMU, GPS and compass data to obtain a robust, low-latency 6-DOF pose. Optical tracking gives accuracy, inertial gives high update rate, and vision-based (SLAM) removes drift.*

---

### 1.7 — Illustrate the importance of Degree of Freedom in VR.
**M: 02 | CO: 3 | BT: 3**

**Answer:**

**Definition:** *Degrees of Freedom (DOF) are the independent parameters that define the position and orientation of an object in space. In VR, DOF means the number of independent movements of the user's body that the tracking system can measure.*

- **3 DOF** = **position only** → x, y, z (left–right, up–down, forward–back)
- **6 DOF** = **3 position (x, y, z) + 3 orientation (yaw, pitch, roll)** ← the VR ideal

The coordinates of a moving 3-D object are its position along **X, Y, Z** plus rotation angles **roll, pitch, yaw** — six degrees of freedom. Independent DOFs are measured directly (e.g. 3 translation axes from 3 cameras); **dependent DOFs are derived** from them (e.g. roll computed from accelerometer + magnetometer in a 3-DOF IMU).

**Importance in VR:**

1. **Controls the amount of immersion** — DOF determines how much of the user's real body movement is reflected in the virtual world. More DOF → the world moves *with* the user → higher **presence**.
2. **Gives natural, intuitive interaction** — with 6 DOF the user leans, crouches and reaches naturally; with 3 DOF they must use virtual locomotion (teleport/joystick) because real head movement is ignored, which feels unnatural.
3. **Correct size and scale perception** — position tracking lets the user touch an object at the correct distance and judge depth and scale correctly.
4. **Enables accurate 3-D manipulation** — selecting, grabbing, dragging and placing objects in 3-D requires both position and orientation.
5. **Reduces VR sickness** — a mismatch between the real head movement (6 DOF) and the rendered motion (3 DOF) creates a **vestibular–visual sensory conflict** → cybersickness. Adequate DOF keeps the two systems consistent.
6. **Enables freedom of navigation** — the user can physically walk, look up and down, and peek behind objects, improving spatial understanding.
7. **Supports precise tasks** — eye–hand coordination and millimetre-accurate manipulation needed for surgical simulation, CAD and assembly.
8. **Allows drift correction and calibration** — sensor fusion cross-checks optical and inertial DOFs, reducing **drift**.
9. **Enables believable gestures and avatars** — high-DOF hand and whole-body tracking make sign language and social interaction believable.

**Conclusion:** *6-DOF is the minimum acceptable standard for immersive VR; if the tracking DOF is less than the visual DOF offered by the display, the user perceives a mismatch between what they see and how their body moves, which breaks the illusion of presence and causes discomfort.*

---

### 1.8 — Outline the importance of Box collider and Rigid body in Unity software.
**M: 02 | CO: 2 | BT: 1**

**Answer:**

**Box Collider** — an invisible **rectangular (cuboid) collision volume** attached to a GameObject.
1. It defines the object's **collision boundary**, so Unity knows *when and where* the object collides.
2. It is **very fast / computationally cheap** compared with a Mesh Collider (only simple box maths), which helps real-time performance.
3. It is ideal for **cube-like / boxy objects** — walls, floors, platforms, crates, boxes.
4. It works **with or without a Rigidbody**: on a **static** object (no Rigidbody) it acts as a **static collider**; with a Rigidbody it allows dynamic interaction.
5. It generates **collision events** — `OnCollisionEnter`, `OnTriggerEnter` — used for gameplay logic (score, damage, pickup detection).
6. A single Box Collider can be used as a **cheap rough collider** in place of an expensive mesh collider on a complex model.

**Rigidbody** — the component that gives a GameObject **physical properties**.
1. It attaches **mass and physical behaviour** (gravity, inertia, friction, restitution/bounce) so the object behaves realistically.
2. It **responds to collisions and forces**; without it objects simply pass through each other.
3. It is **essential for realistic VR/AR interaction** — objects can be picked up, dropped, thrown and stacked.
4. It lets you control motion from a script via `AddForce`, `AddTorque`, `velocity`, `MovePosition`, and `isKinematic`.
5. It provides **collision-detection modes** (Discrete, Continuous, Continuous Dynamic, Continuous Speculative) and **constraints** (freeze position/rotation on chosen axes).
6. It supports **joints** (Hinge, Fixed, Spring, Configurable) for mechanisms, vehicles and ragdolls, and makes **gravity** work.

**Together:** *the Collider says **where** the object is solid; the Rigidbody says **how it moves** when something touches it. The two together drive the PhysX physics engine and make object interaction, collision events and realistic behaviour possible in VR/AR.*

---

### 1.9 — List any four 3-D modeling tools.
**M: 02 | CO: 1 | BT: 1**

**Answer:**

| # | Tool | Key features |
|---|---|---|
| **1** | **Blender** | **Free and open-source**, full 3-D suite — modelling, **sculpting**, **rigging**, animation, UV, **EEVEE/Cycles** rendering, compositing, and a game engine. Runs on Windows, macOS, Linux. |
| **2** | **Autodesk Maya** | Industry-standard **film, TV and game** package; powerful **nurbs/sub-D modelling**, rigging, animation, **Arnold** renderer. |
| **3** | **Autodesk 3ds Max** | Widely used in **architecture, game and VFX** workflows; strong modifier stack, extensive plugin ecosystem. |
| **4** | **ZBrush** | Specialist **digital sculpting** tool with millions of polygons; creates highly detailed organic creatures and characters; excellent retopology tools. |

**Other acceptable tools:** **Cinema 4D** (motion graphics), **Autodesk Mudbox** (sculpting/texturing), **Houdini** (procedural/VFX), **SketchUp** (simple architectural 3-D), **Tinkercad** (beginner CAD), **Google SketchUp**, **Blender's Grease Pencil**, **Rhino**.

---

### 1.10 — List the 4 key elements in experiencing virtual reality.
**M: 02 | CO: 1 | BT: 1**

**Answer:**

| # | Element | Explanation |
|---|---|---|
| **1** | **Targeted behaviour** | The experience is **designed by the creator**; the behaviour induced is **intentional, not random** — e.g. walking or flying, learning or training, social interaction. |
| **2** | **Artificial sensory stimulation** | VR **replaces or enhances** natural sensory input; **at least one sensory modality is partially controlled artificially** — vision, hearing, touch, motion and balance. |
| **3** | **Unawareness** | The **organism is unaware of the interference** — it accepts the virtual environment and feels "being there"; this is a **perceptual illusion**, like an optical illusion. |
| **4** | **Organism** | The **entity that experiences** the VR — a **human user, or even an animal** (rodents, insects, primates). VR is widely used in neuroscience experiments on animals, so **VR is not limited to humans**. |

**Related framework (mention for extra marks):** The **"VR Triangle" / Three I's** — **Immersion · Interaction · Imagination (I³)**; and the **five classic components of a VR system** — **VR Engine · I/O Devices · Software & Databases · User · Task**.
*(Alternative "key elements of experiencing VR" framing used by many textbooks: Immersion · Presence · Interaction · Navigation.)*

---
---

## PART B — 100 marks pattern: Q2 compulsory (3 parts) + any **one** full question from each of (3,4), (5,6), (7,8), (9,10)

---

### 2(a) — List out the application of Virtual Reality in different fields and its benefits in the field of education.
**M: 06 | CO: 1 | BT: 2**

**Answer:**

### (i) Applications of VR in different fields

| Field | Application |
|---|---|
| **Education** | **Immersive learning** — students explore concepts and environments interactively; better understanding and retention through **experiential learning**; **virtual labs**; virtual field trips; interactive training systems. Also helps non-traditional learners (physically disabled, rehabilitation). |
| **Healthcare** | **Medical training, therapy and rehabilitation** in controlled virtual environments; **surgery simulation** on virtual patients; **virtual colonoscopy / bronchoscopy** (virtual endoscopy); **3-D anatomy atlases**; diagnostics via 3-D organ reconstruction from radiological cross-sections; **virtual hospital** for practising emergency/accident handling; exposure therapy for phobias. Reduces risk, improves precision. |
| **Defence & military** | Battle and flight simulation, mission rehearsal, urban-warfare drills, live-fire training **without live ammunition**. |
| **Architecture & construction** | Architects and clients **visualise and experience designs before construction**; identifies design issues early; improves decision-making. |
| **Gaming** | Highly immersive, interactive; players **feel physically present** in the game world. |
| **Entertainment & media** | Virtual concerts, movies, theme-park simulations; audience **actively experiences rather than passively consumes** (e.g. Star Trek TNG holodeck, TRON, The Matrix). |
| **Social interaction** | Users meet in **shared virtual spaces using avatars**; remote collaboration and social presence beyond physical distance. |
| **Engineering & product design** | Walk through prototypes before building; virtual assembly and inspection; automotive design (Ford). |
| **Industry** | **Safety training**, machinery installation and assembly practice, ergonomic studies. |
| **Science & research** | Scientific visualisation of huge or microscopic datasets; astronomy simulation; motion capture for sports science. |
| **Tourism** | Virtual tours of destinations, museums and heritage sites before travelling. |
| **Sports** | Technique analysis and rehearsal of competition. |
| **Professional fields** | Robotics, medicine, aviation, business, architectural and interior design, city planning, product design, law enforcement, visual arts, music and dance. |

### (ii) Benefits of VR in the field of education

1. **Safe and risk-free learning** — students practise high-risk tasks (surgery, aircraft landing, chemical handling) repeatedly **with no real danger** to people or equipment.
2. **Cost-effective and repeatable** — one expensive simulator can be shared by **hundreds of students**; an experiment can be repeated any number of times without consuming chemicals or breaking parts.
3. **Safe mistake-making with instant feedback** — learners can fail, repeat and receive immediate performance feedback, which accelerates learning.
4. **Realistic, immersive 3-D understanding** — abstract and microscopic/macro-scale concepts (an atom, the heart, the solar system, an engine) become **visibly concrete** — far better than 2-D diagrams.
5. **Presence and engagement** — full immersion keeps students focused, **raises retention**, and makes learning an experience rather than a lecture.
6. **Availability and accessibility** — remote and online learners get access to the same labs; **no geographical limits**, works 24×7.
7. **Individualised and paced learning** — adaptive VR adjusts difficulty per learner and tracks each student's performance.
8. **Teamwork and collaboration** — multi-user VR classrooms let distributed students work on the same virtual model together.
9. **Better learning outcomes** — VR improves skill retention, spatial understanding and exam scores compared with traditional 2-D teaching.
10. **Supports inclusive education** — students who are physically disabled can participate in experiments and field trips they could never attend.

*(For balance: limitations are high initial cost, need for trained faculty, slow and expensive content development, headset discomfort and motion sickness.)*

---

### 2(b) — Explain the importance of aural representation in VR and how it is used to achieve a complete VR experience.
**M: 06 | CO: 2 | BT: 1**

**Answer:**

**Definition:** *Aural representation (spatial / 3-D sound)* is the rendering of sound so that the user **perceives it as coming from a specific point, direction and distance in the virtual space**. Humans locate sound using **binaural hearing**, and the auditory system processes **sound direction and distance**.

### Importance of aural representation in VR

1. **Completes the VR experience (main answer).** VR stimulates **multiple senses**; **vision alone gives only part of the perception**. Aural representation supplies the **auditory half**, so the user perceives a world that has both sight and sound — the experience becomes **complete and believable**, which is the definition of a convincing VE (Ellis: a VE in at least one sensory modality; a complete one needs several consistent ones).
2. **Creates presence and immersion.** Consistent **audio-visual correspondence** — a sound coming from a visible source — greatly increases the **sense of being there** and embodiment.
3. **Provides spatial awareness and navigation.** The user can locate objects that are **behind or above** them, which is essential because in VR they cannot turn a monitor to look around — so sound is often the fastest way to find things.
4. **Gives essential gameplay and safety feedback** — footsteps, engine noise, warnings, enemy sounds, UI clicks. A large part of the information in a game is audio.
5. **Makes the environment feel like a real place** — ambience, reverberation, echo, occlusion (sound muffled through walls) and outdoor openness.
6. **Reduces sensory conflict and cybersickness.** Correctly localised, low-latency audio keeps auditory information consistent with the visual and vestibular systems.
7. **Improves memory and learning** — audio cues make VR training and educational experiences more memorable.
8. **Accessibility** — audio alerts help users who cannot see a visual cue.

### How aural representation is used to achieve a complete VR experience

1. **Spatial audio sources in the scene** — every virtual object gets an audio source **placed at its virtual location**; the **AudioListener** (on the camera/HMD) follows the user's head.
2. **Binaural / HRTF filtering** — a **Head-Related Transfer Function** describes how sound is filtered by the head, torso and pinnae for a given direction. The renderer applies **different filters to the left and right ear** using **interaural time differences (ITD)** — a sound on the right reaches the right ear first — and **interaural level differences (ILD)** — a sound on the right is louder in the right ear.
3. **Head-tracking synchronisation** — as the user turns their head, the filters update instantly so the sound **stays locked** at its world position. This is what ties aural representation to 6-DOF tracking.
4. **Distance attenuation** — loudness falls with distance following the **inverse-square law** (with rolloff curves).
5. **Occlusion / obstruction filtering** — sound behind a wall or obstacle is **low-pass filtered** and attenuated.
6. **Reverb / acoustic simulation** — reverb zones and reflected sound give a sense of space and room size.
7. **Doppler effect** — pitch changes as source and listener approach or separate.
8. **Directional audio** — for private listening in headphones, sound is directed only toward the user's ears.
9. **Realistic sound design and interactive audio** — natural recorded/synthesised sounds, footstep cadence matched to walking speed, gunshots on firing.
10. **Engines used** — Unity `AudioSource` + `AudioListener` with 3-D settings (Spatial Blend, Min/Max Distance, Rolloff, Doppler) and **HRTF**; **Wwise**, **FMOD**, **Resonance Audio** (Google), **Microsoft Spatial Sound**.

**Conclusion:** *Aural representation makes the auditory system a full participant in the illusion. By spatially localising and filtering sound relative to the user's head using HRTF and head tracking, the user *perceives* sounds as coming from real positions in the virtual world. This audio-visual consistency completes the multisensory perceptual illusion, dramatically increases presence and realism, aids navigation, and supplies essential feedback — thereby completing the VR experience.*

---

### 2(c) — Discuss the important features of Unity that makes it a popular game engine.
**M: 04 | CO: 2 | BT: 2**

**Answer:**

Unity is a **cross-platform, general-purpose real-time 3-D (and 2-D) game development engine** by Unity Technologies. Its popularity comes from:

1. **Cross-platform deployment** — a single project builds for **20+ platforms**: Windows, macOS, Linux, Android, iOS, WebGL, Xbox, PlayStation, Switch, and VR/AR headsets.
2. **Integrated all-in-one IDE** — scene design, asset import, physics, animation, UI, audio, profiling and scripting all in **one editor**; no need to juggle many external tools.
3. **Powerful C# scripting with a rich API** — the full .NET class library plus built-in APIs for physics, audio, UI, animation, particles and XR.
4. **First-class VR/AR/MR support** — **OpenXR**, **XR Interaction Toolkit**, Oculus, SteamVR, ARCore, ARKit, HoloLens, WebXR.
5. **Huge ecosystem and Asset Store** — thousands of ready-made models, animations, sounds, shaders and tools, plus a very large developer community and abundant tutorials.
6. **Powerful 2-D engine** — Tilemap, Sprites, 2-D physics and 2-D animation make it excellent for mobile 2-D games.
7. **Built-in advanced physics** — PhysX-based **rigidbody / collision / joint** system with easy primitive colliders and a layer collision matrix.
8. **Fast iteration** — live script reload while playing, quick build times.
9. **Modern rendering** — URP and HDRP render pipelines, real-time GI, GPU instancing, post-processing stack.
10. **Affordable/free tier** — the Personal edition is free for students and small studios.

**Note:** the course material adds that Unity is the **most widely used VR/AR/MR development platform**, and states that **over 91% of Vuforia and HoloLens experiences** are made with Unity.

---
---

### 3(a) — Write a program for smooth movement of the object along the movement of arrow keys.
**M: 10 | CO: 3 | BT: 3**

**Answer:**

```csharp
using UnityEngine;

public class ArrowKeyMover : MonoBehaviour
{
    public float speed = 5f;                 // units per second
    public float rotationSpeed = 90f;         // degrees per second (optional)

    void Update()
    {
        // Build a direction vector from the arrow keys
        float horizontal = 0f;
        float vertical   = 0f;

        if (Input.GetKey(KeyCode.LeftArrow))  horizontal -= 1f;
        if (Input.GetKey(KeyCode.RightArrow)) horizontal += 1f;
        if (Input.GetKey(KeyCode.UpArrow))    vertical   += 1f;
        if (Input.GetKey(KeyCode.DownArrow))  vertical   -= 1f;

        Vector3 direction = new Vector3(horizontal, 0f, vertical);

        // 'transform' is relative to the object, so it moves along its own forward/right
        if (direction.magnitude > 0f)
        {
            // --- SMOOTHNESS ---
            // 1. Normalise so diagonals are not faster
            // 2. Lerp between current and desired velocity for gradual acceleration
            Vector3 desiredVelocity = direction.normalized * speed;

            Vector3 currentVelocity =
                Vector3.Lerp(transform.position, desiredVelocity, Time.deltaTime * 5f);

            transform.position = currentVelocity;
        }
    }
}
```

### Simpler version (the core answer)

```csharp
using UnityEngine;

public class ArrowKeyMover : MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");   // Left/Right arrows or A/D
        float z = Input.GetAxisRaw("Vertical");     // Up/Down arrows or W/S

        Vector3 direction = new Vector3(x, 0f, z);

        // Multiply by Time.deltaTime so speed is the SAME at any frame rate (smooth)
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }
}
```

### Using an Input Action (new Input System)

```csharp
using UnityEngine;
using UnityEngine.InputSystem;

public class ArrowKeyMover : MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {
        Vector2 input = Vector2.zero;

        if (Keyboard.current.upArrowKey.isPressed)    input.y += 1f;
        if (Keyboard.current.downArrowKey.isPressed)  input.y -= 1f;
        if (Keyboard.current.leftArrowKey.isPressed)  input.x -= 1f;
        if (Keyboard.current.rightArrowKey.isPressed) input.x += 1f;

        Vector3 direction = new Vector3(input.x, 0f, input.y);
        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
    }
}
```

**Why this is *smooth*:**
1. **`Time.deltaTime`** multiplies the movement so speed is **frame-rate independent** — at 60 fps or 144 fps the object moves at the same speed. Without it, motion is jerky and speed depends on FPS.
2. **Normalising** `direction` so that pressing two arrows (a diagonal) is not √2 times faster.
3. **Smoothing/acceleration** — instead of jumping straight to full speed, ramp up using `Mathf.Lerp` (position) or `Vector3.Lerp` / `SmoothDamp` (velocity), so the object accelerates and decelerates gradually.
4. `Input.GetAxisRaw`/`Input.GetKey` returns a **continuous** -1/0/+1 value every frame rather than a single key-press event.

**Extra features you can add for the remaining marks:**
- Optional **rotation** to face the direction of travel: `transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);`
- Normalised diagonal movement; **speed clamping** with `Mathf.Clamp`.
- Stop the object when the key is released (velocity decays with `Vector3.MoveTowards`).

---

### 3(b) — Analyze the importance of yaw, pitch and roll in 3-D rotation.
**M: 06 | CO: 3 | BT: 4**

**Answer:**

**Yaw, pitch and roll (YPR)** are the three **Tait–Bryan angles** used to describe the **orientation** of an object in 3-D space — i.e., the **rotational degrees of freedom**. Together with x, y, z they give the **6 DOF** of a pose.

| Angle | Axis | Direction | Everyday example |
|---|---|---|---|
| **Yaw** | **Vertical (Y)** axis | **Left ↔ right** rotation | Turning your head / a car "left or right", like saying **"no"** |
| **Pitch** | **Horizontal (X)** axis | **Up ↔ down** rotation | Nodding your head, like saying **"yes"**; raising a gun barrel |
| **Roll** | **Z** axis (line of sight) | **Sideways tilt** rotation | Tilting your head towards your shoulder; banking an aeroplane |

### Importance

1. **They completely describe orientation.** Position (x, y, z) tells you *where* an object is; **without rotation the object is not fully placed**. Together they give the **3 rotational DOF** needed for a full **6-DOF pose**.
2. **They are exactly what a 6-DOF tracker must measure.** A VR HMD must report **yaw, pitch and roll** so that turning or tilting your head produces the matching change of view. This is the core of **head tracking** and therefore of **presence**.
3. **Yaw is the most important axis in VR.** Turning the head sideways is the most common movement, and it is the one that must be **perfectly tracked** — yaw error is the most noticeable and most sickness-inducing error.
4. **They define the virtual camera orientation.** The renderer uses yaw/pitch/roll (as Euler angles or, better, **quaternions**) to build the camera's view matrix. Getting them right is what makes the virtual world appear **anchored** to the user's head.
5. **They control interaction and aim.** In a first-person shooter, **yaw and pitch of the controllers** determine where the player is aiming; **roll** is used for actions like reloading a cylinder or tilting a steering wheel.
6. **They enable locomotion and navigation.** Yaw rotates the user (turning); pitch can pitch the camera or drive the flight angle; roll is used for banking in vehicles.
7. **They are the basis of object manipulation.** Rotating a 3-D object, placing furniture, or orienting a machine part all use yaw/pitch/roll.
8. **They are used in animation and rigging.** Bone rotations in skeletal animation are expressed in these three angles; **Animation Curves / Euler curves** in Unity are stored as x, y, z (pitch, yaw, roll).
9. **Order matters.** Rotations about different axes **do not commute** — rotating yaw-then-pitch is not the same as pitch-then-yaw. This must be handled consistently or objects get unexpected orientations.
10. **Euler angles vs quaternions.** Euler (YPR) angles are intuitive but suffer from **gimbal lock** (when pitch = ±90°, yaw and roll become dependent and one axis loses effect). **Quaternions** avoid this and interpolate smoothly, so Unity exposes rotation in Euler angles for the Inspector but stores it as a quaternion internally.
11. **In the Unity Inspector**, rotation is displayed as **X = pitch, Y = yaw, Z = roll** (in degrees, 0–360), matching the axis convention used throughout VR.

**Conclusion:** *Yaw, pitch and roll are the three rotational degrees of freedom that, together with x, y, z, define the complete pose of an object. In VR they are essential because the virtual camera must reproduce the exact orientation of the user's head in every frame — a mismatch in any of the three immediately destroys the illusion of presence and causes discomfort or motion sickness.*

---
---

### 4(a) — Illustrate the matrix multiplication algorithm iterated over the chain of matrices to produce "Chaining the Transformations".
**M: 10 | CO: 3 | BT: 3**

*(OR of 3(a) — attempt either)*

**Answer:**

### Concept

To place an object in the world, you must apply, in order, a chain of **transformations**: **scale → rotate → translate** (and then **project** the result into camera/clip/screen space). Instead of transforming every vertex once per transformation (which would require many multiplications per vertex), we **multiply the transformation matrices together once**, producing a single combined matrix, and then multiply each vertex by that one matrix. This is called **"chaining the transformations"**.

**Unity convention:** with column vectors and row-vector application `v' = M · v`, the combined local-to-world matrix is:

```
M = T · R · S          (translate, then rotate, then scale is read right-to-left)
```

so that `v_world = T · R · S · v_local`. (Scaling happens **first** in the local space, then rotation, then translation — which is the intuitive behaviour: scale a cube, spin it, then move it.)

### The algorithm

```csharp
using UnityEngine;

public class MatrixChaining : MonoBehaviour
{
    // The chain of transformations, in the order they must be applied
    public Vector3    translation = new Vector3(5f, 2f, -3f);
    public Vector3    rotationEuler = new Vector3(0f, 45f, 0f);   // pitch, yaw, roll
    public Vector3    scale = new Vector3(2f, 2f, 2f);

    void Start()
    {
        // ---- STEP 1: build each individual matrix -------------------------------
        Matrix4x4 T = Matrix4x4.Translate(translation);   // local  -> world (position)
        Matrix4x4 R = Matrix4x4.Rotate(Quaternion.Euler(rotationEuler)); // orientation
        Matrix4x4 S = Matrix4x4.Scale(scale);             // size

        // ---- STEP 2: CHAIN them into ONE matrix by successive multiplication ----
        Matrix4x4 chained = T * R * S;          // S first, then R, then T

        // (The intermediate results, useful for debugging)
        Matrix4x4 rs = R * S;                    // scale + rotate
        Debug.Log("Scale then Rotate:\n" + rs);
        Debug.Log("Full chain (T*R*S):\n" + chained);

        // ---- STEP 3: use the single chained matrix on every vertex ---------------
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        MeshFilter mf = cube.GetComponent<MeshFilter>();
        Mesh mesh = mf.mesh;                       // work on a copy

        Vector3[] vertices = mesh.vertices;
        for (int i = 0; i < vertices.Length; i++)
        {
            // One matrix multiplication per vertex does scale + rotate + translate
            vertices[i] = chained.MultiplyPoint3x4(vertices[i]);
        }
        mesh.vertices = vertices;
        mf.mesh = mesh;

        // Unity itself does exactly this for us:
        Debug.Log("Unity's own localToWorldMatrix:\n" + transform.localToWorldMatrix);
        Debug.Log("Multiplying a point by Unity's matrix: " +
                  transform.TransformPoint(new Vector3(0, 0, 0)));
    }
}
```

### Step-by-step matrix maths

**1. Scale matrix `S`** (uniform `s`):

```
| s  0  0  0 |
| 0  s  0  0 |
| 0  0  s  0 |
| 0  0  0  1 |
```

**2. Rotation matrix `R`** — Unity composes from quaternions. Pitch (about X), then yaw (about Y), then roll (about Z); `Quaternion.Euler(x, y, z)` builds `q = qY * qX * qZ`. For a **yaw of θ about Y** alone:

```
| cosθ  0  sinθ  0 |
|  0    1   0    0 |
| -sinθ 0  cosθ  0 |
|  0    0   0    1 |
```

**3. Translation matrix `T`**:

```
| 1  0  0  tx |
| 0  1  0  ty |
| 0  0  1  tz |
| 0  0  0  1  |
```

**4. Chain them:** `M = T · R · S`. For the vertex column vector `v = (x, y, z, 1)ᵀ`:

```
| |   |   |       |   |        |   |   |       |   |     |
| 1  0  0  tx    | cosθ 0 sinθ 0   | s 0 0 0   |   | x |
| 0  1  0  ty  · |  0   1  0  0  · | 0 s 0 0  · |   | y |
| 0  0  1  tz    | -sinθ0 cosθ 0   | 0 0 s 0   |   | z |
| 0  0  0  1     |  0   0  0  1    | 0 0 0 1   |   | 1 |
| |   |   |       |   |        |   |   |       |   |     |
 =  the single combined local-to-world matrix
```

`v_world = M · v` — one multiplication per vertex instead of three.

### Full render-pipeline chain (world → screen)

```
Model (object)      M_model      = T · R · S              (local → world)
View  (camera)      M_view       = Camera.worldToCameraMatrix
Projection           M_proj       = GL.GetGPUProjectionMatrix(camera.projectionMatrix, true)
GPU projection       M_gpuProj
─────────────────────────────────────────────────────────
Chain                M_chain      = M_gpuProj · M_view · M_model
v_clip   = M_chain  ·  v_local
v_ndc    = (x/w, y/w, z/w)
v_screen = ((ndc.x*0.5+0.5)*W, (ndc.y*0.5+0.5)*H)     (NDC is [-1,1] → pixels [0,W]×[0,H])
```

For **stereoscopic VR** a **projection matrix** is pre-multiplied for each eye (asymmetric frusta using the left and right eye positions and the IPD):

```
v_eye = P_left/right · V · M · v_local
```

### Why the technique matters (importance)

1. **Performance** — instead of *n* multiplications per vertex for *n* transformations, you do **one**; for static objects the combined matrix can be computed once and even uploaded to the GPU as a **constant buffer**, so the vertex shader performs a single matrix multiply.
2. **Correct order** — chaining guarantees the transformations are applied in the intended order (scale in local space, then rotate, then translate), which is not obvious from the individual matrices.
3. **Hierarchies** — a scene graph is a tree of transforms. Each object's world matrix is `M_parent · M_local`, so chaining is what makes a child of a rotated parent inherit the parent's orientation correctly.
4. **Simplicity & reuse** — one matrix object replaces several; camera motion is just a view-matrix update, so the same chained pipeline serves every frame.
5. **Correctness for stereo/perspective** — projection cannot be folded into translation, so the chain makes explicit which transforms are linear (T, R, S) and which is projective (P).
6. **Gimbals avoided** — building `R` from a **quaternion** avoids Euler-angle gimbal lock that a naive Euler matrix chain would suffer from.

**Note on Unity's equivalent:** you rarely build this by hand — `transform.localToWorldMatrix` and `transform.TransformPoint()` already perform the chain, and the `Graphics.DrawMeshInstanced`/SRP Batcher paths exploit exactly this idea.

---
---

### 4(b) — Analyze the importance of triangle meshes for virtual objects.
**M: 06 | CO: 3 | BT: 4**

**Answer:**

**What they are:** A **triangle mesh** (triangulated mesh) represents an object as a set of **vertices** connected by **edges** to form **faces**, where every face is a **triangle**. Two triangles sharing an edge form a **quadrilateral (quad)**, and quads are always split into two triangles because a GPU can only rasterise triangles efficiently.

### Importance

1. **The GPU's native primitive.** Rasterisation, texture mapping, interpolation of normals and vertex shaders all operate on triangles. Using triangles means **no triangulation step at render time**, so meshes are the fastest, most hardware-friendly way to draw geometry.
2. **Any surface can be represented.** Triangles can approximate **any** surface, curved or flat — flat, smooth, hard-edged. A cube = 12 triangles; a sphere = many thin triangles. So one representation covers spheres, terrain, characters and machinery.
3. **Exact modelling of planar polygons.** Flat polygons (quads, pentagons) are represented exactly by triangles, whereas N-gons can render incorrectly. Splitting a quad along a diagonal also avoids the **"cracks"** that appear when two triangles of a quad are rasterised with different diagonal conventions.
4. **Efficient rendering and GPU pipeline.** Vertex and index buffers, **indexed drawing** (shared vertices), and **hardware instancing** all work on triangle lists. Fewer, larger triangles mean far fewer vertex-shader invocations, which is critical because VR must render **stereoscopically (twice) at 90–120 fps**.
5. **Physics collision.** Unity builds **convex hulls** from mesh vertices for **Mesh Colliders** so that fast collision detection algorithms can run. A mesh must be **convex for dynamic (moving) objects**; for static objects a full concave mesh collider is allowed.
6. **Skinning and animation.** Character animation **binds** a mesh to a skeleton by assigning each vertex to bones and **weights**. Because a triangle deforms as a unit, interpolating vertex positions gives smooth bending — this is how **skinned meshes** are posed by the animation/rigging system.
7. **Deformation and procedural geometry.** Height-map terrain, cloth simulation, water surfaces, cave systems and explosion deformation all operate by **moving vertices**, which deforms the triangles with them.
8. **Level of Detail (LOD).** Meshes can be simplified by **decimating** triangles while keeping the silhouette — the basis of LOD groups, which trade triangle count for distance and are a primary VR performance tool.
9. **Storage, compression and streaming.** Index buffers and compressed vertex formats make meshes compact; **mesh simplification / quantization** is an explicit MR research direction.
10. **Normals, UVs and tangents** are carried per vertex or per triangle corner, which is what makes shading, texturing and normal mapping work.
11. **Collision detection of the render vs physics** — the **visual mesh** can be high-detail while a **separate, simpler convex physics mesh** is used for collision, keeping performance high.

**Conclusion:** *Triangle meshes are important because they are the only polygon primitive the GPU can process natively, they can approximate any surface, they keep rendering fast enough for real-time VR, and they also serve as the geometry for physics collision and skeletal animation. Managing triangle count is therefore the single most important optimisation lever in real-time 3-D.*

---
---

### 5(a) — Differentiate between First Person Controller and Third Person Controller.
**M: 08 | CO: 3 | BT: 3**

**Answer:**

**First Person Controller (FPC)** — the camera is placed **at the character's own eyes**, so the player sees the world **through the character's eyes** and normally **cannot see the character itself**. Input is typically **WASD + mouse look**.

**Third Person Controller (TPC)** — the camera is placed **behind and above the character**, so the player **sees the character's body/avatar** in front of the camera, and the character moves relative to the camera direction.

| Basis | **First Person Controller** | **Third Person Controller** |
|---|---|---|
| **Camera position** | At the character's **eyes**, inside the head | **Behind and above** the character |
| **Avatar visibility** | Character is **not visible** (no body on screen) | Character and **full avatar/animation are visible** |
| **Feel** | **Maximum immersion** — "you *are* the character" | **Detached, observational** feel — you watch the character |
| **Spatial awareness** | **Poor** — you cannot see your own body or your position in the level | **Good** — you always see where the character is relative to objects |
| **Navigation** | Harder — you cannot see around you; rely on a compass/minimap | Easier — you see the surroundings and plan the route |
| **Typical input** | **WASD + mouse look** (mouse controls yaw/pitch directly) | **WASD** relative to the **camera**, mouse orbits the camera behind the character |
| **Typical genres** | Shooters, horror, immersive sims, FPS, flight/vehicle sims | Third-person shooters, platformers, adventure, racing, action-adventure |
| **Aim / shooting** | **Crosshair-based**; aiming is direct and intuitive — the screen centre is where you shoot | Aim uses a **screen-space offset** from the character to the target; needs a **reticle** offset; slightly less direct |
| **Obstruction handling** | Objects near the camera clip awkwardly; less common problem | **Camera collision** is a real issue — walls/objects push the camera in; must be handled (spring arm, raycast) |
| **Character design** | Only hands/arms/weapons usually modelled — **cheaper to build** | Full character model, rig and animations required — **more expensive** |
| **Occlusion awareness** | You may be hidden behind a wall without knowing | You can see the character, so **you always know where you are** |
| **Animation complexity** | Little visible body animation needed | Needs full skeletal animation: idle, walk, run, jump, fall, turn |
| **Motion sickness** | Higher risk — no external visual reference frame; forced camera motion | Lower risk — the visible character provides a **reference frame** |
| **Multiplayer** | Hard to tell who is who (only a name tag) | Easy — you recognise other players' characters |
| **Examples** | Half-Life: Alyx, Beat Saber, Superhot VR, Doom VFR | Hellblade: Senua's Sacrifice, Skyrim VR, Mario Odyssey VR, Rez Infinite |

**Conclusion:** *The FPC maximises immersion and makes aiming direct, which is why it dominates shooters and horror; the TPC gives better spatial awareness and navigation clarity, which is why it dominates platformers and adventure games. The choice depends on whether the design goal is "you are inside the world" or "you are watching a character act in the world".*

---

### 5(b) — Analyze the need for 3-D software in Unity. Explain Blender software along with its important features.
**M: 08 | CO: 4 | BT: 4**

**Answer:**

### (i) Why 3-D software is needed alongside Unity

1. **Unity is an engine, not a modeller.** Unity can place, animate, light and render objects, but it **cannot create detailed geometry**. 3-D software is needed to **create the assets** that Unity then uses.
2. **Unity's primitives are blockouts.** The built-in Cube, Sphere, Capsule, Cylinder, Plane and Quad are **placeholder geometry**. Real content needs modelled meshes.
3. **Everything visual depends on it** — characters, vehicles, buildings, props, terrain, weapons, environments. Without modelling there is nothing to place in a scene.
4. **Physics needs collision meshes.** Colliders (Mesh Collider, convex hulls) are generated from real geometry, and the render mesh and physics mesh are often different — modelling lets you make a **simple convex physics hull** for a complex visual object.
5. **Skins, rigs and animation clips** for characters are produced in 3-D software and **imported as FBX** into Unity, which only plays back animations.
6. **UV mapping and texturing** — UVs are laid out in the modelling package; Unity uses them to map textures.
7. **Topology quality matters for real-time.** Low-poly, **optimised and retopologised** meshes are needed for mobile and VR — professional 3-D software provides decimation, LOD generation and normal-map baking.
8. **Terrain, sculpting and organic shapes** are hard or impossible to do properly in an engine; sculpting tools (ZBrush, Blender Sculpt Mode) are designed for it.
9. **Pipeline standardisation** — the industry exchange format is **FBX/GLTF**, which 3-D packages write and Unity reads.
10. **Saves time and money** — modelling once and reusing the asset across platforms (PC, mobile, VR) is far cheaper than building geometry by code.

### (ii) Blender — introduction

**Blender** is a **free and open-source 3-D creation suite** developed by the **Blender Foundation** and released under the **GPL**. It is a **complete, professional, all-in-one package** available for **Windows, macOS and Linux** — modelling, sculpting, texturing, rigging, animation, simulation, rendering, compositing, video editing and even a game engine — **completely free**, with no seat limits. It is the industry-standard choice for independent game developers, animation and architectural visualisation, and is widely used for **VR-ready assets** because it is free and produces clean, optimised meshes.

### (iii) Important features of Blender

| # | Feature | Description |
|---|---|---|
| **1** | **Free & open source** | Completely free under the GPL; huge community; no licence cost — ideal for students |
| **2** | **Polygon modelling** | Full **mesh modelling** — vertices, edges, faces; **Edit Mode** (Tab) for sub-object editing; primitives, extrude, inset, bevel, subdivide, boolean, mirror |
| **3** | **Sculpt Mode** | High-vertex-density **organic modelling** with brushes (draw, smooth, inflate, crease); dynamic topology; **voxel remesh**; excellent for creatures and characters |
| **4** | **Retopology** | Rebuild a clean, low-poly, quad-dominant mesh over the sculpted result — **essential for game/VR meshes** |
| **5** | **UV unwrapping & texturing** | Smart UV project, seams, islands; paint textures; **image textures, procedural textures, node-based shader editor** |
| **6** | **Rigging & animation** | **Armature/bones**, **weight painting** for smooth skin deformation, pose mode, IK, constraints; keyframe and **NLA animation**, motion capture import |
| **7** | **Modifiers (non-destructive)** | Array, Mirror, Subdivision Surface, Solidify, Bevel, Boolean, Skin, Remesh, Displace — **procedural and editable** |
| **8** | **Rendering engines** | **EEVEE** (fast real-time, GPU, used for VR previews) and **Cycles** (physically-based, path tracing, offline quality) |
| **9** | **Two view modes** | **Object Mode** (whole object) and **Edit Mode** (individual vertices/edges/faces) — toggled with **Tab**; plus Sculpt, Weight Paint, Pose, Texture Paint modes |
| **10** | **Particle & physics simulation** | Particles, cloth, soft body, rigid body, fluid, smoke and fire simulations |
| **11** | **Node-based compositor & sequencer** | Compositing, colour grading, and a **video sequencer** — so Blender is a full post-production tool |
| **12** | **Compositing / tracking** | Motion tracking, camera solving — useful for **match-moving and AR/VR compositing** |
| **13** | **File formats** | Reads/writes **FBX, OBJ, glTF/GLB, COLLADA, USD, STL, SVG, Alembic** — so assets move smoothly into **Unity/Unreal** |
| **14** | **Scripting & add-ons** | Built-in **Python** console/scripting and a large add-on library for automation and pipeline customisation |
| **15** | **Cross-platform & lightweight** | Runs on Windows, macOS and Linux; relatively low system requirements |
| **16** | **Built-in game engine** | Blender Eevee-based game engine for real-time prototyping before moving to Unity |

**Conclusion:** *3-D software is needed because Unity is an engine rather than a modeller — it renders and animates assets but cannot author them; and 3-D packages also produce UVs, skins, rigs, animations and optimised collision meshes. Blender meets this need for free, being a complete, cross-platform, professional suite with polygon modelling, sculpting, retopology, rigging and animation, node-based texturing, the EEVEE/Cycles renderers, physics simulation, a compositor and sequencer, Python scripting, and full FBX/glTF/USD interchange with Unity.*

---
---

### 6(a) — Illustrate the need for Sculpting and Rigging features in Blender.
**M: 08 | CO: 3 | BT: 3**

**Answer:**

### Sculpting — the need

**Sculpting** is Blender's **free-form digital clay** mode: instead of moving a handful of vertices, you push and pull the **surface** with brushes to shape it.

**Why it is needed:**
1. **Organic shapes are impossible with box modelling.** Characters, creatures, rocks, cloth folds, tree bark and food are **organic and irregular**; they cannot be built from straight edges, so vertex-by-vertex editing is unworkable. Sculpting lets the artist **add and remove material intuitively**, like working with clay.
2. **Much faster for adding detail.** A single brush stroke that adds surface detail can replace hundreds of manual vertex edits — this is the single biggest reason sculpting exists.
3. **A high-resolution sculpt acts as the "master" form.** You sculpt at very high polygon density (millions of polygons), then **retopologise** to build a clean low-poly game mesh over it. So sculpting defines the **shape**, retopology defines the **topology**.
4. **Supports dynamic topology and voxel remeshing,** so detail can be added without worrying about edge loops.
5. **Combines with modifiers** — you can keep the sculpt as a base and layer **procedural noise/displacement** on top for skin pores, wrinkles and weathering.
6. **Bakes to normal/bump maps.** The sculpted high-detail surface is **baked down into normal maps** and applied to the low-poly mesh, giving detailed appearance at very low cost — exactly what mobile and VR need.
7. **Base meshes** (e.g. a head) can be sculpted over to create many different characters quickly.
8. **Other 3-D packages depend on it** — **ZBrush** and **Mudbox** are dedicated sculpting applications, and Unity has no sculpting at all.

### Rigging — the need

**Rigging** is building a **skeleton (armature)** inside the mesh and **skinning** it so the mesh deforms and moves.

**Why it is needed:**
1. **Animation requires a skeleton.** A model must be able to **bend** — an elbow cannot stay rigid. Rigging inserts **bones** and defines how much each vertex follows each bone.
2. **Weight painting / skinning distributes the deformation.** Each vertex is assigned **weights** to nearby bones; where weights blend, the mesh **bends smoothly** instead of creasing. Without proper skinning, characters look like robots made of rigid plates.
3. **One rig → many animations.** A single rig can be posed, keyframed and animated **hundreds of times** (walk, run, jump, attack, die) without remodelling — this is the **DRY** principle applied to animation.
4. **Animation retargeting.** The same rig can be driven by motion capture, which is how realistic human movement is transferred to a game character.
5. **Performance.** An animated character needs to be a **skinned mesh with a small bone hierarchy**, not millions of independent vertices; the GPU transforms the bones and the vertices follow. Rigging is what makes real-time character animation feasible.
6. **IK (Inverse Kinematics)** — the rig can make the hand reach a point, or the foot stay on the ground, rather than rotating joints directly, which is essential for believable motion.
7. **Facial rig / blendshapes** for lip-sync and expressions.
8. **Unity cannot create a rig** — Unity only **imports and plays back** animations (from FBX) and blends them with Mecanim/Animator. Rigging and skinning must be done in Blender/Maya/3ds Max/ZBrush, then exported.
9. **Procedural/constraint-driven motion** — IK, constraints and drivers can generate motion automatically (e.g. a foot-plant, a chain hanging), and that logic lives in the rig.
10. **Correct light and physics interactions** — rigged characters collide correctly and interact with the VR world, which is what makes them feel real in immersive applications.

**Conclusion:** *Sculpting is needed because organic, high-detail shape cannot be achieved by polygon editing, and it provides the master form from which efficient game meshes are retopologised and normal maps baked. Rigging is needed because nothing can animate without a skeleton: it enables smooth skin deformation, allows one mesh to carry many animations, supports IK and motion capture, and is what makes real-time character animation possible. Every production character in Unity passes through both stages — sculpt in Blender, rig and skin in Blender, export FBX, then animate and blend in Unity.*

---

### 6(b) — Consider a shooting game which has to be developed. Analyze its advantages and disadvantages if developed using First Person Controller and Third Person Controller.
**M: 08 | CO: 3 | BT: 4**

*(OR of 5 — attempt either)*

**Answer:**

### Using a First Person Controller (FPC) — advantages
1. **Maximum immersion** — the player *is* the character; the game feels more intense and realistic, which is ideal for a shooter.
2. **Direct and accurate aiming** — the camera centre is the aim point, so a **crosshair** at screen centre maps exactly to the bullet path. No screen-space offset calculation; aiming feels **natural and twitchy**.
3. **Better peripheral vision** — the screen shows almost exactly what the character sees, so awareness of threats in the environment is high.
4. **Faster, simpler controls** — WASD + mouse; no need to manage a character facing and a following camera.
5. **Cheaper to develop** — only hands/weapon need modelling, not a full character rig with full-body animations → saves modelling, rigging and animation cost.
6. **Less camera management** — no camera-collision, camera-clipping or "camera goes through walls" problems.
7. **Naturally suits weapon handling** — recoil, sway and reload animations are shown on the weapon in view, giving strong feedback.
8. **Works well in VR** — matches head-tracked first-person view.

### Using FPC — disadvantages
1. **Poor spatial awareness** — the player cannot see their own character, so they may not know their position relative to objects, or may be hidden behind cover/walls.
2. **Navigation is harder** — finding objectives is difficult with no external reference; mini-maps/compass are often required.
3. **Restricted characterisation** — little of the character is visible, so personality, emotion and body language are hard to convey; cinematic cut-scenes are hard to compose.
4. **Motion sickness** is higher in VR because there is **no visible body as a reference frame**.
5. **Very limited feedback on the character** — you cannot see your own injuries, reload progress on the body, or team-mate identification.
6. **Difficulty in showing enemies approaching from behind** without sudden turns.
7. **Unrealistic movement** — sprinting and turning in first person can feel disorienting and breaks the brain's expectation of moving a body.
8. **Harder to show group/team situations** — teammates are rarely visible.

### Using a Third Person Controller (TPC) — advantages
1. **Better spatial awareness** — the player always sees the character and surroundings, so positioning, cover and navigation are clear.
2. **Full character presentation** — the **full model, rig and animations** (walk, run, aim, reload, death) give a richer, more believable experience.
3. **Camera control is flexible** — the camera can orbit, zoom, swing out in aim mode, and can be pulled in on obstruction (**spring-arm / collision**), giving cinematic framing.
4. **Natural movement** — the visible character moves like a real body, so locomotion looks and feels more natural.
5. **Cover and positioning mechanics work well** — the third-person camera makes cover systems (peek, lean, blind fire) visible and readable.
6. **Easier to show character identity and emotion** — animation and face can convey personality.
7. **Better for team play and identification** — you recognise teammates and enemies.
8. **Lower sickness risk in VR** — the visible body provides a **reference frame**.

### Using TPC — disadvantages
1. **Aiming is less direct** — the crosshair sits at a **screen-space offset** from the character; translating that to a world aim point adds complexity and can feel imprecise or "loose", especially at long range.
2. **Camera collision problems** — the camera clips into walls, objects and the character's own body; needs raycasts, spring arms and fade-out logic.
3. **Reduced immersion** — you watch the character act rather than being the character; weaker emotional engagement for a shooter.
4. **More expensive** — a full character, rig, skin, and a complete animation set must be created.
5. **Peripheral awareness is reduced** — the screen shows less of the environment than FPC because the character occupies part of the view.
6. **Animation/attack system complexity** — aiming, strafing, firing and locomotion must all be blended correctly, or the animation looks wrong.
7. **Not ideal for VR realism** — many players find the "doll" view breaks immersion in an immersive medium.

### Conclusion / recommendation
*For a competitive or realism-focused shooter, the **First Person Controller** is preferred for its maximum immersion, direct crosshair aiming, better peripheral vision and lower development cost — despite poorer spatial awareness and higher motion-sickness risk. The **Third Person Controller** is better when spatial awareness, cinematic camera control, character identity, cover mechanics and readable team play matter more than raw immersion, at the cost of indirect aiming and higher asset complexity. Many modern shooters offer **both** — a switchable first/third-person camera — to satisfy both audiences.*

---
---

### 7(a) — Explain the advantage of Mobile Sensors in Tracking.
**M: 08 | CO: 2 | BT: 1**

**Answer:**

**Context:** Almost every smartphone and tablet carries a rich **onboard sensor suite**, so mobile AR (ARCore, ARKit, Pokémon GO, Layar) uses these instead of custom tracking hardware.
**Typical mobile sensors:** 3-axis **accelerometer**, 3-axis **gyroscope**, **magnetometer (compass)**, **GPS/GNSS**, **barometer**, **camera** (plus **depth/ToF/structured-light** on flagships), proximity and ambient-light sensors, and **biometric** sensors.

### Advantages

1. **Ubiquitous and zero extra cost.** Every phone already has these sensors, so **no tracking hardware, dongles, beacons or base stations need to be bought** — an enormous cost saving, and the reason mass-market AR was possible at all.
2. **Compact and lightweight.** Everything fits in the hand or pocket — truly **mobile**, no tether, no fixed installation.
3. **Instant to use — zero setup.** No camera calibration, no base-station placement, no marker printing. Launch the app and point the phone.
4. **Very wide hardware base.** Billions of devices → a huge developer and user community, abundant tutorials, and a large talent pool.
5. **Sufficient accuracy for coarse tracking.** GPS gives **metre-level outdoor position**; compass + gyroscope give good orientation. That is enough for **location-based and marker-less AR** experiences.
6. **Sensor fusion is powerful.** Combining **camera (CV/SLAM) + IMU + GPS + compass** through an **Extended Kalman Filter** produces a robust, smooth **6-DOF pose** with far better stability than any single sensor alone — gyroscope for smooth high-rate rotation, accelerometer for gravity/tilt, magnetometer for absolute heading, camera to correct drift, GPS for absolute position.
7. **Depth and plane detection on modern phones.** ARKit/ARCore and LiDAR-equipped devices give **plane detection, occlusion, people occlusion and light estimation** — enabling real MR-like behaviour.
8. **Rich contextual sensors.** Barometer (altitude/indoor floor), ambient light (auto-exposure), proximity, and magnetometer let the system infer **indoor position, orientation and lighting** even when GPS fails.
9. **Low power and duty-cycled.** Sensor hubs let the sensors run intermittently, preserving battery when the app is in the background.
10. **Turnkey platform support.** ARCore/ARKit/Samsung AR give tracking, plane detection, anchors and **cloud anchors** for multi-user sharing — no in-house CV work needed.
11. **No occlusion problem.** Sensors are **inside the device**, so the user's own body cannot block them — a problem for external optical rigs.
12. **Economically scalable** — near-zero hardware cost per user makes AR viable for **large deployments** in retail, education and marketing.
13. **No fixed infrastructure** — works in any environment, indoors or outdoors, with no site preparation.

### Limitations (mention for balance)
Low accuracy compared with optical tracking; **gyroscope drift**; GPS does not work indoors; **battery drain** from continuous GPS + camera + IMU + rendering; **thermal throttling**; small screen and limited FOV; handheld **instability** makes precise interaction hard; weak performance in **textureless or dark environments** (SLAM struggles); **device fragmentation** in sensor quality; and it cannot replace lighthouse/optical systems for **room-scale, precision 6-DOF VR**.

---
---

### 7(b) — Identify the problems which has to be solved to make an effective tracking system.
**M: 08 | CO: 2 | BT: 2**

**Answer:**

For a tracking system to be **effective**, it must satisfy a set of requirements. Each is a "problem" that must be solved:

| # | Problem | Requirement / solution needed |
|---|---|---|
| **1** | **Latency (motion-to-photon)** | The delay between body movement and the updated pixels must be **below 20 ms**. Needs fast sensors, hardware time-stamping, pose **prediction** and asynchronous render threading. |
| **2** | **Update / frame rate** | Tracking must update at **90–120 Hz** and rendering must run at **≥ 90 fps**, matched to the display refresh rate, otherwise there is judder/jitter. |
| **3** | **Occlusion and out-of-view loss** | When markers or features are hidden, the pose must still be recovered — use **redundant markers/sensors**, sensor fusion and body models, and **predict** the pose outside the volume. |
| **4** | **Drift (long-term stability)** | Inertial dead reckoning accumulates error. Fuse with an **absolute reference** — optical, magnetic, GPS, or **vision-based SLAM** anchors — so the pose does not wander. |
| **5** | **Accuracy and precision** | Millimetre-level accuracy is needed for precise manipulation — requires good **calibration** (intrinsics + extrinsics), high resolution and careful filtering. |
| **6** | **Jitter and noise** | Sensor output is noisy. Apply **Kalman / complementary / One-Euro filters** and smooth orientation using **quaternions** rather than raw Euler angles. |
| **7** | **Audio-visual synchronisation** | Audio, video and haptic latencies must be matched; otherwise the brain receives mismatched cues → sickness. |
| **8** | **Comfort and physical safety** | HMD weight, heat and cable management; avoid sudden acceleration/deceleration that conflicts with the vestibular system; prevent neck and collision injuries. |
| **9** | **Calibration and ease of setup** | Calibration must be **quick or automatic**, and per-user (**IPD**, lens distance), or users will not use the system. |
| **10** | **Environmental robustness** | Must work in varied light, in the dark, near metal objects, and in bright sunlight; must handle reflective, transparent and patternless surfaces. |
| **11** | **Ambiguity / correspondence errors** | Solve **temporal continuity** and **left–right ambiguity** — the two classic failure modes of optical and marker-based tracking. |
| **12** | **Pose representation (gimbal lock)** | Use **quaternions / rotation matrices** for orientation; Euler angles suffer **gimbal lock** at pitch = ±90° and interpolate badly. |
| **13** | **Range / tracking volume** | Enough volume for natural walking; if the user leaves it, the system must **predict** rather than teleport the camera. |
| **14** | **Cost and accessibility** | The system must be affordable so AR/VR reaches classrooms and industry, not only research labs. |
| **15** | **Battery and thermal budget** | Especially on mobile and standalone HMDs, the system must sustain the frame rate **without overheating**. |
| **16** | **Graceful failure** | On tracking loss the system must **freeze or fade** rather than jump, warn the user, and prompt re-calibration — a sudden camera jump is nauseating. |

**Conclusion:** *An effective tracking system must be **accurate, precise, low-latency (< 20 ms), high update rate (≥ 90 Hz), drift-free, jitter-free, occlusion-tolerant, well synchronised with audio and haptics, comfortable, quick to calibrate, robust to the environment, safe on failure, and affordable**. Every one of these is a problem the designer must solve — and any one of them, if unsolved, breaks immersion and causes cybersickness.*

---
---

### 8(a) — Explain Optical Tracking in detail along with its advantages and disadvantages.
**M: 08 | CO: 2 | BT: 1**

*(OR of 7 — attempt either)*

**Answer:**

**Definition:** Optical tracking is a **camera-based tracking technique** that measures the position and orientation of a tracked target (headset, controller, or marker) by **detecting the light emitted or reflected by it using cameras**, and computing its 3-D pose from the 2-D images — usually by **triangulation**.

### Working in detail

1. **Attaching markers/targets.** A marker is attached to the tracked object.
   - **Active markers** contain **IR LEDs** that emit light (HTC Vive Lighthouse, PS Move's glowing sphere) — bright and robust, but need power.
   - **Passive markers** are **retro-reflective** spheres or tape (OptiTrack, PhaseSpace) that bounce camera IR light — cheap and battery-free, but dependent on camera quality.
   - **Marker-less** variants track **natural features** of the environment, needing no artificial markers.
2. **IR cameras capture images.** Cameras fitted with **IR filters and IR illuminators** take pictures of the markers/environment. **Infrared is used because it is invisible to humans and immune to visible-light interference.**
3. **Image processing detects the marker** in **each camera's 2-D image** by thresholding, blob/corner detection or feature detection, giving its **2-D centroid or corners**.
4. **Triangulation gives 3-D position.** The same marker is seen by **at least two cameras** separated by a known **baseline**. The 2-D positions are back-projected into rays and their **intersection** gives the 3-D **(x, y, z)** position. More cameras and better angular coverage improve accuracy.
5. **Orientation is computed from multiple markers.** Markers arranged rigidly on the target define a known shape; the algorithm solves a **rigid-body fit (Kabsch/Horn)** to get the full **6-DOF pose**. Alternatively, the motion of a **three-marker pattern** gives rotation via the cross product of the marker vectors.
6. **Redundancy improves robustness.** Using **several markers** allows the pose to be recovered even if one marker is occluded, and gives **sub-millimetre accuracy**.
7. **Calibration is essential.** **Camera intrinsics** (focal length, principal point, lens distortion) and **extrinsics** (camera positions/orientations) are solved — typically by moving a **calibration wand** through many poses — before use.
8. **Filtering, fusion and prediction.** Data is passed through a **Kalman/complementary filter**, usually **fused with IMU data** in hybrid trackers to remove frame-drop glitches and dropouts, and the pose is **predicted forward** to hide display latency.

### Key variants
- **Outside-in optical** — cameras in the room track markers on the headset (**OptiTrack, HTC Vive Lighthouse, PS Move**).
- **Inside-out optical** — cameras **on the headset** track the environment (**Quest, SteamVR, ARCore/ARKit, HoloLens**).
- **Lighthouse / IR sweep** — base stations sweep IR lasers; sensors on the headset detect the sweep timing to compute the pose (**Vive, Valve Index**).
- **Structured light** — a projected IR dot pattern plus a camera (**Kinect v1, early HoloLens**).
- **Time-of-Flight (ToF)** — IR emitter plus a sensor array measures return time (**Kinect v2, Azure Kinect**).

### Advantages

1. **Very high accuracy** — sub-millimetre to millimetre positional accuracy with good marker/camera setup.
2. **Low latency and high update rate** (60–120+ Hz) → a smooth, responsive view.
3. **Full 6-DOF pose** (position **and** orientation) → strong immersion and natural interaction.
4. **Immune to magnetic/electromagnetic interference** — it works beside metal objects, motors and speakers, which break magnetic tracking.
5. **No drift over time** — it measures absolute position rather than integrating motion, so accuracy holds through long sessions (unlike IMU-only tracking).
6. **Redundancy and reliability** — multiple markers and cameras give graceful degradation under partial occlusion.
7. **Works in complete darkness** — with IR illumination it is independent of ambient visible light, which matters in VR.
8. **Scalable** — many users and many objects can be tracked simultaneously in a shared volume.
9. **Small, lightweight passive targets** — no batteries or wiring on the marker.
10. **Deterministic** — no probabilistic failure mode like visual SLAM, making it predictable for safety-critical applications.

### Disadvantages

1. **Occlusion** — if markers are blocked by the user's body, a wall or a large object, the pose cannot be computed and **tracking is lost or paused**.
2. **Needs clear line of sight** and a **fixed, carefully placed camera/base-station rig**, so it requires a dedicated room and setup time.
3. **Not portable** — the tracked volume is **bounded by camera coverage**; the user cannot walk outside it.
4. **Cost** — high-end optical systems (OptiTrack, Vive Lighthouse, multi-camera rigs) are expensive.
5. **Calibration is mandatory** — intrinsic and extrinsic calibration must be done and **redone if the cameras move**.
6. **Light and environmental sensitivity** — passive markers can be confused by **sunlight or IR-emitting** sources, and reflective surfaces can confuse detection.
7. **Frame drops and jitter** — occlusion, motion blur or lost markers cause **glitches and jitter** in the pose.
8. **Space and rigging** — needs mount points, power and cabling for the cameras.
9. **Outdoor/sunlight limits** — some IR schemes do not work in bright sunlight, which restricts AR use.
10. **Markers get dirty or damaged** — sweat, dust or obstruction silently degrades accuracy.

---
---

### 8(b) — Differentiate between Marker-based and Marker-less tracking along with its application.
**M: 08 | CO: 2 | BT: 2**

*(OR of 7 — attempt either)*

**Answer:**

**Marker-based tracking** uses a **known, high-contrast, machine-readable pattern (a *marker* or *fiducial*) deliberately placed in the real world**, which the camera detects; the marker's pose is recovered and used as a reference frame to **register** virtual content precisely on it.
**Marker-less tracking** uses **no artificial marker** — the camera detects **natural features** already present (corners, edges, texture) and applies **computer vision / SLAM** to build a map and compute the device's 6-DOF pose.

### Differences

| Basis | **Marker-Based Tracking** | **Marker-Less Tracking** |
|---|---|---|
| **Reference object** | A **man-made printed or LED marker** placed in the world (QR code, AprilTag, ArUco, IR reflective sphere) | **Natural features** of the environment — texture, corners, lines. **No artificial marker** |
| **Method** | **Pattern detection** + decode ID + **corner-based homography / PnP** | **Feature detection & matching** (SIFT/SURF/ORB), **SLAM**, **visual odometry**, **plane detection** |
| **Accuracy** | **Very high, stable, deterministic** (sub-centimetre), robust to lighting variation | **Moderate and variable** — depends on scene texture and lighting; **drifts** over time |
| **Setup** | Requires **printing and placing markers** | **Zero setup** — just point the device |
| **Hardware cost** | Very low (printed markers); moderate for IR/LED markers | Needs a **camera** (ideally depth/ToF) + a powerful CPU/GPU for SLAM |
| **Lighting** | Needs **consistent, adequate illumination**; fails in dark or glare | Also light-sensitive but **adaptive** (auto-exposure, feature re-detection) |
| **Range / area** | **Limited** to within the camera's view and the marker's size; tracks one or a few known markers | Tracks **large open areas**, many objects, whole rooms |
| **Environment** | Must be **set up** with markers; unsuitable for unknown or moving places | Works in **any real environment** — streets, outdoors, indoors |
| **Scalability** | Limited — each object/target needs its **own unique marker**; clutter if many | **Scales to many objects** with no extra markers |
| **Proportion of real content** | Markers are **extra clutter** added to the scene | Scene stays **completely unmodified** |
| **Computational load** | Low — corner detection is cheap | High — SLAM/feature matching is expensive |
| **Applications** | Education (scan a marker → 3-D molecule/monument), marketing & interactive packaging, museum exhibits, **assembly/maintenance instructions**, printed-map navigation, medical body-marker registration, tracking-volume calibration, AR toys | Mobile AR (**Pokémon GO**, Layar, Wikitude), ARCore/ARKit apps, **IKEA Place** furniture placement, virtual try-on, **Google Maps AR** navigation, industrial AR assembly without markers, **HoloLens / Quest room-scale MR**, face tracking, environment reconstruction |
| **Best for** | **Controlled, static, precise** setups — labs, exhibits, training rooms, calibration | **Open, real-world, large-scale, mass-consumer** AR |

### Applications

**Marker-based**
- **Education** — scanning a marker in a textbook or placard shows the 3-D molecule, historical monument or animated diagram; AR flashcards and interactive posters.
- **Marketing & packaging** — a marker on a brochure or product box launches a product demo, video or 3-D model.
- **Museums & heritage** — markers near exhibits trigger 3-D reconstructions and audio guides.
- **Industrial maintenance** — a marker on a machine panel makes the AR overlay draw the exact part and its repair steps.
- **Medical** — markers on the patient's skin for vein mapping, surgical registration and radiotherapy planning.
- **Navigation** — markers on printed maps or signposts give 3-D directions and highlighted POIs.
- **Construction** — markers on drawings or sites overlay the 3-D model on the real structure for progress checks.
- **Research/labs** — markers are used to **calibrate camera volumes** for optical tracking systems.
- **Toys and games** — AR toys and card-based AR board games.
- **Rapid prototyping** — printing a marker is trivial, so it is the cheapest entry point for AR development.

**Marker-less**
- **Location-based outdoor AR** — Pokémon GO, Ingress, Wikitude, Layar, Google Street View AR: content is tied to real places using GPS + compass + camera.
- **Mobile consumer AR (ARCore/ARKit)** — IKEA Place furniture placement, virtual make-up and clothing try-on, AR measuring tools, 3-D photo capture, face filters and stickers.
- **Industrial & enterprise AR** — hands-free assembly and maintenance guidance on real equipment with **no markers at all**; warehouse picking.
- **Mixed Reality headsets** — HoloLens and Meta Quest use room-scale, plane-aware, marker-less SLAM in any room.
- **Retail** — virtual product visualisation on the real shop floor; interactive displays.
- **Education** — virtual lab apparatus placed on a real desk without markers.
- **Medical** — AR endoscopy and navigation overlaying on the patient's own anatomy.
- **Cultural heritage** — reconstructing monuments on site with no markers, only cameras.
- **Entertainment** — AR filters and face tracking.

---
---

### 9(a) — Explain WebXR and the challenges associated with it.
**M: 08 | CO: 2 | BT: 1**

**Answer:**

**Definition:** **WebXR** is an **open W3C standard set of APIs that lets web browsers expose virtual reality and augmented reality devices to web pages.** A browser that implements WebXR is a *WebXR User Agent*; a web page written with WebXR is a *WebXR Application*. It is the successor to **WebVR** and is the **only cross-vendor, cross-platform way to publish immersive content on the open web** — no app-store approval, no install, and one URL works on a phone, a PC headset and a standalone headset.

**WebXR API surface (from `navigator.xr`):**
- `XRSystem` — entry point; `isSessionSupported('immersive-vr' | 'immersive-ar')`
- `XRSession` — a running immersive session; `requestAnimationFrame()`, `end()`, `updateRenderState()`, `inputSources`
- `XRSpace` / `XRReferenceSpace` — the coordinate space in which poses are reported (`local-floor`, `local`, `viewer`, `bounded-floor`, `unbounded`)
- `XRFrame` + `XRViewerPose` + `XRView` — per-frame head pose and per-eye view
- `XRInputSource` + `XRInputSourceArray` — controllers, hands and gaze; `XRGamepad` for buttons/triggers
- `XRSessionEvent` (`sessionstart`, `sessionend`, `inputsourceschange`)
- Supported alongside the **WebGL/WebGPU** graphics API, **Web Audio API** for spatial audio and **Gamepad/WebXR Input** profiles.

### Why it matters
One web page can run on **Oculus, HTC Vive, PlayStation VR, Quest, Apple Vision Pro and non-VR browsers**; content is **instantly deployable and linkable** (no store), easy to share, indexable and updateable, and it can blend 2-D web content with immersive 3-D.

### Challenges

1. **Performance.** Web pages share the main thread with the browser's own work; the renderer is competing with page scripting, layout, GC and the browser UI. Achieving **90–120 fps** with low latency in WebGL/WebGPU is much harder than in a native engine.
2. **No low-level GPU access.** The browser deliberately withholds direct GPU/driver APIs, so low-level optimisation (custom shaders, tight memory control, direct VRAM access) available natively is not possible; only WebGL/WebGPU abstractions are given.
3. **Security & privacy model.** WebXR requires the session to be started inside a **user gesture** (a click/tap); **cross-origin isolation** (`COOP`/`COEP` headers) is needed for `SharedArrayBuffer` and low-latency features; **camera/sensor data access is permission-gated**.
4. **Precision tracking is limited.** Browser WebXR typically provides only **device/local pose with limited 6-DOF fidelity**; **room-scale** tracking needs separate lighthouse/inside-out infrastructure, and there is no access to raw sensor data for custom fusion.
5. **Device fragmentation.** Headsets use different **input profiles** (`xr-standard` gamepad maps, touch vs. buttons), different FOV/IPD behaviour and different performance ceilings; **fallback and adaptive quality** must be implemented.
6. **Thermal and battery limits on mobile.** Phones and standalone headsets throttle under sustained GPU load and drain batteries, so long immersive sessions suffer.
7. **Mobile FOV and form factor.** Handheld AR gives a small viewing window and is held in the hand — inherently less immersive than a headset.
8. **Rendering quality gap.** Browser texture compression support, MSAA, shadow and lighting techniques and post-processing are limited or slower than native engines, so console-quality visuals are hard to reach.
9. **Browser inconsistencies.** Chrome-based browsers are best supported; Safari/Firefox support differs, and there have been long-standing gaps (e.g. iOS Safari WebXR availability), so **fallback to non-immersive mode** is essential.
10. **Standard evolution.** The API is still maturing — deprecations (e.g. `XRDevice`, WebVR's `getDevice/pose`, the layers API) and the **WebGPU transition** require ongoing maintenance, and the **immersive-ar** feature is still not universally supported.
11. **Security of spatial data.** WebXR applications can request room-scale and camera/sensor data, raising **privacy** concerns about mapping private spaces.
12. **Content download size.** High-poly immersive worlds must be streamed and compressed over the network, so **bandwidth and CDN** become quality constraints.
13. **No screen-space "layer" UI** in the same way as native engines, so UI must be built inside the 3-D scene.

---
---

### 9(b) — Analyze the different ways in which the VR/AR session may end and suggest the appropriate steps to be followed when the session has to end.
**M: 08 | CO: 3 | BT: 4**

**Answer:**

### Ways a VR/AR session can end

| # | Cause | Explanation |
|---|---|---|
| **1** | **User removes the HMD** | The proximity/headset-present sensor reports the device is off the face. If the user removes it for more than a few seconds, the session is normally **suspended or ended** so the user can safely take a break. |
| **2** | **User presses the system/exit button** | The headset's platform button, controller menu button, or a system-menu **"Quit / Exit"** option. |
| **3** | **Application calls `session.end()`** | The app itself requests termination (e.g. the user taps a "Quit" button in the app, the app completes its experience). |
| **4** | **Device disconnects / goes out of range** | Headset battery dies, USB cable is unplugged, Bluetooth link drops, wireless signal is lost, or the device leaves the tracking volume. |
| **5** | **The browser tab is closed, refreshed, or backgrounded** | On the web, closing or navigating away the tab terminates the immersive session; on mobile, backgrounding the app suspends it. |
| **6** | **The application or page is terminated** | The OS kills the process (low memory), a crash occurs, or a **watchdog** kills a hung process. |
| **7** | **The browser/process is suspended** | System suspend/sleep, or the mobile OS suspends the app to save power. |
| **8** | **Session timeout / user inactivity** | The runtime ends the session after a period of no input, or on a schedule (timed exhibit, classroom session limit). |
| **9** | **Runtime failure** | An unrecoverable error, loss of graphics context, driver failure, or failure to start the display. |
| **10** | **Safety/comfort intervention** | The runtime ends the session because of **cybersickness safety**, thermal shutdown, or overheating; or because the user is outside the tracked play area for too long. |
| **11** | **Explicit remote/system command** | An administrator ends the session, or the platform receives a "quit" from a companion/companion-app controller. |
| **12** | **Forced exit after a fatal error in network sync** | In multiplayer/telepresence, a required server connection drops and the app cannot continue. |

### Steps to follow when the session has to end

1. **Give the user warning and time to finish.** Show an in-VR **notification/exit prompt** (or dim/blur the scene) a few seconds before exit so the user is not surprised — an abrupt cut is disorienting.
2. **Pause input and interaction.** Stop accepting controller, hand, gaze and voice input so the user cannot interact with a dying scene.
3. **Stop the render and tracking loops.** Cancel the pending `requestAnimationFrame` / `Update` loop and **stop sending tracking frames** to the headset, so the headset does not keep displaying a frozen or stale image.
4. **Pause or stop audio** — stop spatial audio playback so the user is not left with a looping sound in the real world; unmute before returning to the desktop.
5. **Save state before exiting.** Persist the **player position, score, progress and any user data**, so the user can resume exactly where they left off. (Do this **before** releasing anything.)
6. **Detach event listeners and release resources.** Remove `sessionend`, `visibilitychange`, `devicechange`, `inputsourceschange` and button handlers; **cancel animation frames, timers, coroutines and pending promises**; null out references to `XRReferenceSpace`, `XRViewerPose`, buffers, textures and audio nodes so the GPU/JS heap can be freed.
7. **Call the formal end API.** Call `XRSession.end()`, which fires the **`end` event on the XRSession and a `sessionend` event on `XRSystem`**. Handle `sessionend` to run the same cleanup, and handle `end` if you need to know when it actually finished.
8. **Reset the display state.** Remove the immersive-mode UI/`xr-present` classes from the document, and restore any normal 2-D page content so the user lands back on a sensible screen rather than a blank page.
9. **Handle `visibilitychange`/`devicechange` gracefully** — if the device is merely **suspended** (tab hidden, headset removed briefly), **pause** the loop and resume it on the matching `visible`/`sessionresume` event instead of destroying the session.
10. **Exit the immersive loop cleanly** and only then hand control back to the normal page/app loop.

**JavaScript example (WebXR):**

```javascript
const xr = navigator.xr;

// 1. Application decides to quit
document.getElementById('quit').addEventListener('click', async () => {
  showExitNotification();
  saveState();                       // 2. persist progress
  stopRenderLoop();                  // 3. cancel requestAnimationFrame
  pauseAudio();                      // 4
  removeEventListeners();            // 6. detach listeners

  if (xrSession) {
    await xrSession.end();           // 7. formal end -> fires 'end' + 'sessionend'
  }
  restore2DContent();                // 8. restore the normal page
});

// React to any end reason
xrSession.addEventListener('end', onSessionEnded);
navigator.xr.addEventListener('sessionend', (e) => {
  cleanupResources();                // release GL/JS resources
  restore2DContent();
});

document.addEventListener('visibilitychange', () => {
  if (document.hidden) pauseEverything();     // 9. suspend, don't destroy
  else if (xrSession) resumeEverything();
});

// 11. device lost
navigator.xr.addEventListener('devicechange', (e) => {
  if (!e.session) cleanupResources();
});
```

**Conclusion:** *A session can end for many reasons — deliberate user action, application request, device/browser termination, suspension, failure, timeout or a safety intervention. Because the end is often unexpected and happens while the user is in an immersive state, the correct procedure is: **warn the user, pause input, stop the render loop and audio, save state, detach listeners, release resources, call `session.end()`, handle the resulting `end`/`sessionend` events, restore the 2-D view, and treat mere suspension (`visibilitychange`) as a pause rather than an exit** — so the user is never left in a broken or blank headset state.*

---
---

### 10(a) — Explain the Life cycle of WebXR application in detail.
**M: 08 | CO: 2 | BT: 1**

*(OR of 9 — attempt either)*

**Answer:**

**Definition:** The **WebXR application lifecycle** is the sequence of events and API calls that takes a web page from a normal **2-D web page**, through **entering immersive mode**, running the **render loop** with per-frame tracking input, to **exiting** back to the 2-D page — including **suspend and resume**.

### Stage-by-stage lifecycle

**Stage 0 — 2-D page (the default state)**
The user sees an ordinary web page. The app checks capability:
```javascript
if (navigator.xr) {
  const vrOK = await navigator.xr.isSessionSupported('immersive-vr');
  const arOK = await navigator.xr.isSessionSupported('immersive-ar');
  // show "Enter VR" / "Enter AR" buttons accordingly
}
```
*(Older WebVR used `navigator.getVRDisplays()`.)*

**Stage 1 — Feature detection**
Before offering immersive mode, the app detects: `navigator.xr` support, `isSessionSupported()` for the mode, and loads the **WebXR polyfill** if needed. The **Enter** button is only shown if immersive mode is actually available — this is how the app avoids promising something it cannot deliver.

**Stage 2 — User gesture & session request** (must be inside a user gesture)
```javascript
enterButton.addEventListener('click', async () => {
  xrSession = await navigator.xr.requestSession('immersive-vr', {
    optionalFeatures: ['local-floor', 'bounded-floor', 'hand-tracking', 'layers']
  });
});
```
`requestSession(mode, init)` is the **single point where the app asks to take over the display**. Optional features are **requested** here (reference spaces, hand tracking, layers) and become available only if the device grants them. If the user or platform refuses, the promise rejects and the app stays in 2-D.

**Stage 3 — Session start event**
```javascript
navigator.xr.addEventListener('sessionstart', onSessionStart);
```
The `sessionstart` event on `XRSystem` fires when an immersive session actually begins.

**Stage 4 — Setup within the session**
- **Reference space:** obtain a tracking space and ask for a pose.
```javascript
refSpace = await xrSession.requestReferenceSpace('local-floor');
const viewerPose = await refSpace.requestReferenceSpace; // and later frame.getViewerPose(refSpace)
```
- **Render state:** set the base layer for rendering.
```javascript
const layer = new XRWebGLLayer(xrSession, gl);
await xrSession.updateRenderState({ baseLayer: layer });
```
(or `XRWebGLBinding` / `projection-layer` in newer revisions.)
- **Input sources:** read `xrSession.inputSources` for controllers, hands and gaze; attach `selectstart`/`selectend`/`squeezestart`/`squeezestart` handlers and map them using the **standard gamepad profile**.
- **Feature events:** listen for `inputsourceschange`, `visibilitychange` (page hidden/shown) and `reset` (user recentres/re-centres).
- **Create the WebGL context** sized to the XR framebuffer and start the WebGL/WebGPU pipeline.

**Stage 5 — The render/animation loop** (the core of the lifecycle)
```javascript
function onXRFrame(t, frame) {
  xrSession.requestAnimationFrame(onXRFrame);   // re-register FIRST
  const pose = frame.getViewerPose(refSpace);   // head pose, 6-DOF
  if (pose) {
    for (const view of pose.views) {            // one per eye (stereo)
      render(view.transform.matrix, view.projectionMatrix);
    }
  }
  updateInputSources(frame, xrSession.inputSources);
}
xrSession.requestAnimationFrame(onXRFrame);
```
- `requestAnimationFrame` fires **in step with the headset's display refresh** (typically 72/90/120 Hz) — this is the correct timing source, not `window.requestAnimationFrame`.
- `frame.getViewerPose(refSpace)` returns an **`XRViewerPose`** with `transform` (head pose) and one or more **`XRView`**s (per eye, each with its own `eye`, `transform`, `projectionMatrix`, `viewport`).
- **Pose prediction** is done by the UA, and **pose may be `null`** briefly (tracking not ready) — the app must handle that.
- **Head pose → camera:** the renderer's view matrix is the inverse of `view.transform.matrix`; projection comes from `view.projectionMatrix` (already asymmetric/stereo-correct).
- Per frame the app also **updates physics, input, audio and scene state**.
- If the **visibility state** is `visible-blurred` (system UI showing) the app should **pause heavy work**.

**Stage 6 — Suspend / resume** (session preserved but paused)
```javascript
xrSession.addEventListener('visibilitychange', () => {
  if (xrSession.visibilityState === 'hidden') suspendEverything();
  else resumeEverything();     // keep the same session
});
```
The UA may also **pause the frame rate** (or stop calling rAF) when the headset is not being worn. The **session object stays valid**, so the app resumes rather than re-requesting.

**Stage 7 — Reference-space reset**
```javascript
xrSession.addEventListener('reset', () => { /* user recentred or bounds changed */ });
```
If the user recentres or the play-area bounds change, `reset` fires and the app must **re-obtain the reference space**.

**Stage 8 — Session end**
```javascript
xrSession.addEventListener('end', onEnded);
navigator.xr.addEventListener('sessionend', onEnded);
```
Ends because of: the user pressing exit / removing the HMD, the app calling `session.end()`, the tab closing, device disconnect, page navigation, or a system-initiated end. The app must **save state, cancel the rAF loop, remove listeners, release WebGL/audio resources** and **restore the 2-D page**.

### Lifecycle summary diagram

```
2-D page  ──isSessionSupported()──▶  "Enter VR" button shown
    │ user gesture + requestSession('immersive-vr')
    ▼
sessionstart  ──▶ setup: updateRenderState(baseLayer), requestReferenceSpace('local-floor'),
    │            read inputSources, create WebGL context
    ▼
requestAnimationFrame loop   ◀── resume ── visibilitychange
    │  frame.getViewerPose(refSpace)          (session preserved)
    │  render per XRView (left + right eye)
    │  poll inputSources / gamepad buttons
    ▼  (repeated at display refresh rate)
  suspend  ◀── visibilitychange / headset removed ── resume
    │
    ▼  end  (user exit, session.end(), tab closed, device lost, error)
sessionend  ──▶ save state, cancel loop, release resources, restore 2-D page
```

### Unity note
In **Unity**, the equivalent lifecycle is handled by the **XR Interaction Toolkit**: enable **XR Plug-in Management**, choose **OpenXR/ Oculus/ SteamVR**, then Unity manages `requestSession`, the render loop, the tracking origin, input, and `end`/`sessionend` for you, plus **scene transitions via `SceneManager`**.

---
---

### 10(b) — Explain the functionality for WebXR request and session life cycle.
**M: 08 | CO: 2 | BT: 4**

*(OR of 9 — attempt either)*

**Answer:**

### 1. `navigator.xr.requestSession()` — the request API

The **request** step is the app asking the browser to hand over the display.

```javascript
async function enterVR() {
  // (a) Feature-detect first
  if (!navigator.xr) return showFallback2D();

  // (b) MUST be called from a user gesture (click / tap) — browser security rule
  try {
    const xrSession = await navigator.xr.requestSession('immersive-vr', {
      optionalFeatures: ['local-floor', 'bounded-floor', 'hand-tracking']
    });
    await onSessionStarted(xrSession);
  } catch (err) {
    // RequestAnimationFrame / NotSupportedError / SecurityError
    console.error('Session request failed:', err);
    showFallback2D();          // graceful degradation
  }
}
```

**Signature:** `requestSession(mode, XRSessionInit)` → `Promise<XRSession>`

| Part | Meaning |
|---|---|
| **`mode`** | `'immersive-vr'` (headset) or `'immersive-ar'` (see-through / handheld). Must be one of the `XRSessionMode` enum values. |
| **`XRSessionInit`** | An options dictionary of **required** (`requiredFeatures`) and **optional** (`optionalFeatures`) features. Omitting a feature that the app truly needs causes failure; optional features are best-effort. |
| **Returns** | A `Promise` resolving to an **`XRSession`**. |
| **Must be user-activated** | The browser requires a **user gesture**. A programmatic call from `setTimeout` or on page load will be **rejected** — this is a deliberate anti-abuse/security measure. |
| **Rejection cases** | User refused, no compatible device, the session was already active, insecure (non-HTTPS) context, or required features unavailable. |
| **Secure context** | WebXR requires **HTTPS** (or `localhost`). |

**Common features requested:** `viewer`, `local`, `local-floor`, `bounded-floor`, `unbounded`, `hand-tracking`, `layers`, `dom-overlay`, `anchors`, `light-estimation`, `hit-test`.

### 2. `XRSession` — the session object

`XRSession` represents one immersive session and is the **centre of the lifecycle API**.

| Member | Functionality |
|---|---|
| `requestAnimationFrame(cb)` | Registers a callback to run **once per display refresh** of the headset. The callback receives `(time, XRFrame)`. The app **must re-register itself every frame**. |
| `cancelAnimationFrame(handle)` | Cancels a scheduled frame callback (used when pausing/exiting). |
| `requestReferenceSpace(type)` | Returns a `Promise<XRSpace>` for a tracking space — `'viewer'`, `'local'`, `'local-floor'`, `'bounded-floor'`, `'unbounded'`. `'local-floor'` puts y = 0 on the real floor. |
| `updateRenderState(init)` | Configures **how the session renders** — e.g. `{ baseLayer: new XRWebGLLayer(session, gl) }`, `depthNear`, `depthFar`, `ignoreDepthValues`, `layers`. Returns a Promise. |
| `renderState` | Read-only snapshot of the current render state. |
| `inputSources` | A live `XRInputSourceArray` of tracked **controllers, hands and gaze sources**, each with `handedness`, `targetRaySpace`, `gripSpace`, `gamepad` and `profiles`. |
| `end()` | **Programmatically ends** the session → fires `end`. Returns a Promise. |
| `visibilityState` | `'visible'`, `'visible-blurred'` (system UI showing) or `'hidden'`. |
| `renderState`, `environmentBlendMode` | `'opaque'` (VR), `'additive'` or `'alpha-blend'` (AR). |
| `domOverlay` | (Optional) lets 2-D HTML DOM be shown over/in the immersive view. |
| `depthUsage`, `useDepthValues` | Depth-buffer configuration for correct occlusion. |
| **Events** | `end`, `visibilitychange`, `inputsourceschange`, `select`, `selectstart`, `selectend`, `squeezestart`, `squeeze`, `squeezeend`, `reset`. |

**`XRFrame`** (the per-frame object): `getViewerPose(refSpace)` → `XRViewerPose`; `getPose(space, baseSpace)` → `XRPose`; `getHitTestResults(source)`; `getLightEstimate()`; `getJointPose` / `getJointPose` for hand tracking.

**`XRViewerPose`**: `.transform` (head pose = XRTransform with `position` and `orientation`), `.views` (array of **`XRView`** — one per eye for stereo VR, one for AR).
**`XRView`**: `.eye` (`'left'`/`'right'`/`'none'`), `.transform`, `.projectionMatrix`, `.viewport`, `.renderTarget`.

### 3. Working request + session lifecycle example

```javascript
const xr = navigator.xr;
let xrSession, refSpace, gl, running = false;

xr.addEventListener('sessionstart', () => console.log('immersive session started'));

async function enterVR() {
  if (!xr) return fallback();

  xrSession = await xr.requestSession('immersive-vr', {
    optionalFeatures: ['local-floor', 'bounded-floor', 'hand-tracking']
  });

  // --- session-level events ---
  xrSession.addEventListener('visibilitychange', onVisibility);
  xrSession.addEventListener('inputsourceschange', onInputsChanged);
  xrSession.addEventListener('reset', onReset);
  xrSession.addEventListener('end', onEnded);
  xrSession.addEventListener('select', onSelect);       // 'trigger' on controller
  xrSession.addEventListener('squeezestart', onGrip);

  // --- setup the session ---
  gl = canvas.getContext('webgl2', { xrCompatible: true });
  const layer = new XRWebGLLayer(xrSession, gl);
  await xrSession.updateRenderState({ baseLayer: layer, depthNear: 0.1, depthFar: 100 });

  refSpace = await xrSession.requestReferenceSpace('local-floor');

  running = true;
  xrSession.requestAnimationFrame(onFrame);           // start the loop
}

function onFrame(t, frame) {
  if (!running) return;
  xrSession.requestAnimationFrame(onFrame);            // ALWAYS re-register

  const pose = frame.getViewerPose(refSpace);          // may be null
  if (pose) {
    for (const view of pose.views) {                   // left + right eye
      gl.bindFramebuffer(gl.FRAMEBUFFER, layer.framebuffer);
      renderView(view.transform.matrix, view.projectionMatrix, view.viewport);
    }
  }
  for (const src of xrSession.inputSources) {
    if (src.gamepad) readButtons(src.gamepad);
    if (src.hand)    for (const j of src.hand.values) trackJoint(j);
  }
}

function onVisibility() {
  if (xrSession.visibilityState === 'hidden') { cancelAnimationFrame(h); pauseAudio(); saveState(); }
  else                                             { xrSession.requestAnimationFrame(onFrame); }
}

async function exitVR() {
  running = false;
  saveState();
  await xrSession.end();          // -> fires 'end' -> 'sessionend'
}

function onEnded() {
  cleanupGLResources();           // delete buffers, textures, audio nodes
  removeListeners();
  running = false;
  showFallback2D();               // restore the normal 2-D page
}

xr.addEventListener('sessionend', onEnded);   // fired on the XRSystem
```

### 4. Lifecycle summary

| Phase | API / event | Purpose |
|---|---|---|
| **Feature detection** | `navigator.xr`, `isSessionSupported(mode)` | Decide whether to show immersive entry |
| **Request** | `requestSession(mode, init)` **inside a user gesture** | Ask for the display; resolves to `XRSession` or rejects |
| **Start** | `sessionstart` on `XRSystem` | Immersive mode really began |
| **Configure** | `updateRenderState({baseLayer})`, `requestReferenceSpace('local-floor')`, WebGL context, `inputSources` | Prepare render state, tracking space and input |
| **Loop** | `requestAnimationFrame(cb)` + `frame.getViewerPose(refSpace)` + per-`XRView` rendering | Per-refresh-rate render & input at 90/120 Hz |
| **Suspend / resume** | `visibilitychange` (`hidden` / `visible-blurred` / `visible`) | Pause and resume **without** destroying the session |
| **Re-centre** | `reset` | Reference space changed; re-obtain it |
| **End** | `session.end()` or user/browser/system | Request or force termination |
| **Teardown** | `end` on session, `sessionend` on system | Save state, cancel loop, release resources, restore 2-D |

**Key points to emphasise in the exam:**
- `requestSession` **must be called from a user gesture** and **requires a secure (HTTPS) context**.
- It returns a **Promise** and **can reject** — the app must degrade gracefully to 2-D.
- **Optional vs required features** decide whether the app can start on a given device.
- The **frame loop is driven by `XRSession.requestAnimationFrame`**, not `window.requestAnimationFrame`, and it must **re-register every frame**.
- **Per-eye rendering** uses `pose.views`, each with its own transform and projection matrix — this is how stereo VR is produced.
- **`end` / `sessionend` / `visibilitychange`** are what let an app pause, resume and shut down cleanly without breaking the user.

---
---

# 📄 PAPER 2 — QP IS372IA, Feb/March 2026 (RVCE, VII Sem ISE, QP ID 630675)

## PART A (19 questions, 01 mark each except 1.9 = 02 marks)

---

### 1.1 — What is telepresence in VR?
**M: 01 | CO: 1 | BT: 1**

**Answer:** *Telepresence is a VR application that **immerses the viewer in a real world captured by distant cameras** and allows **remote manipulation of real objects via robot arms**. It gives the user the feeling of being physically present at a remote location and directly interacting with it.* Used in **telemedicine** (tele-surgery) and **telerobotics**.

---

### 1.2 — State one medical application of VR.
**M: 01 | CO: 1 | BT: 1**

**Answer:** *Surgical training and simulation — medical students and surgeons **practise operations on virtual patients** in a controlled virtual environment, reducing risk to real patients and improving precision.* *(Other acceptable: virtual colonoscopy/bronchoscopy for diagnosis; exposure therapy for phobias; 3-D anatomy atlases; stroke rehabilitation.)*

---

### 1.3 — What is the function of scripts in Unity?
**M: 01 | CO: 2 | BT: 1**

**Answer:** *User-written code is added to a GameObject as **script components**. Scripts are **reusable and mixable**, and Unity's component system is a **building-block system** that allows flexible feature combinations. They are what give a GameObject its **custom behaviour** — responding to events like `Start`, `Update`, collisions and input, and modifying other objects.*

---

### 1.4 — Mention any two default built-in methods used during game development in Unity.
**M: 01 | CO: 2 | BT: 1**

**Answer:**
1. **`Start()`** — called **once at startup**, just before the first frame's `Update()`. Used to initialise variables and set up references.
2. **`Update()`** — called **once per frame** (every frame while the game is playing). Used for the main gameplay logic such as input and non-physics movement.

*(Others: `Awake()`, `OnEnable()`, `FixedUpdate()` — for physics at a fixed rate, `LateUpdate()` — after all Updates e.g. camera follow, `OnGUI()`, `OnDisable()`, `OnDestroy()`.)*

---

### 1.5 — What are degrees of freedom (DOF) in VR systems?
**M: 01 | CO: 1 | BT: 1**

**Answer:** *Degrees of Freedom are the **independent parameters that define the position and orientation of an object in space**. In VR, DOF is the number of independent movements of the user's body that the tracking system can measure. **3 DOF = position (x, y, z); 6 DOF = 3 position + 3 orientation (yaw, pitch, roll)** — the standard required for immersive VR.*

---

### 1.6 — Identify the role of physics in the virtual world, and why is it important in VR/AR applications?
**M: 01 | CO: 1 | BT: 1**

**Answer:** *The **physics engine (PhysX in Unity)** makes objects obey **realistic, consistent laws** — gravity, momentum, friction, collision and response — so the virtual world behaves believably. It is important in VR/AR because the user already has real physical expectations: when the tracked motion, the visual result and the physical response do not match, a **sensory conflict** occurs that breaks **immersion and presence** and causes **cybersickness**. Correct physics also enables genuine **interaction** — objects can be picked up, dropped and stacked — and is essential for **training simulations** (surgical, engineering, military) where the correct feel and force must be learned.*

---

### 1.7 — What is Third Person Controller?
**M: 01 | CO: 3 | BT: 1**

**Answer:** *A **Third Person Controller (TPC)** is a player controller in which the **camera is positioned behind and above the character**, so the player **sees the character's body/avatar** on screen along with the environment. Movement (usually WASD) is relative to the **camera direction**, and mouse movement **orbits the camera**. It gives **better spatial awareness and navigation clarity** but less immersion and slightly less direct aiming than a first-person controller. Typical in **platformers, racing and third-person shooters** (e.g. Hellblade, Skyrim VR).*

---

### 1.8 — What is First Person Controller?
**M: 01 | CO: 3 | BT: 1**

**Answer:** *A **First Person Controller (FPC)** is a player controller in which the **camera is placed at the character's own eyes**, so the player sees the world **through the character's eyes** and normally **cannot see the character itself**. It uses **WASD for movement and mouse look (yaw/pitch) for viewing**, aiming with a **crosshair at the screen centre**. It provides **maximum immersion** and direct, accurate aiming, typical of **shooters, horror and immersive sims** (e.g. Half-Life: Alyx, Superhot VR).*

---

### 1.9 — Mention any two applications of Tracking.
**M: 02 | CO: 3 | BT: 1**

**Answer:**
1. **Gaming** — head, hand and body tracking drives the virtual camera and the player's avatar, and is the **input mechanism** for pointing, grabbing, shooting and menu navigation. It also enables **locomotion** (room-scale walking, teleportation, treadmill tracking) and gaze-aiming with eye tracking.
2. **Medical imaging and surgery** — tracking **registers 3-D CT/MRI/PET scan data onto the patient's body**, so the surgeon sees the organs and vessels **overlaid on the patient** during a procedure, and provides **navigation guidance**.

*(Others: CAD/product design — track a real model part and overlay the CAD/digital twin; robotics & teleoperation — track the operator's hand to control a robot arm in a hazardous environment; military, aviation and industrial training; **navigation & wayfinding** in AR; assistive technology; construction safety; sports motion analysis.)*

---

### 1.10 — What is marker-based tracking?
**M: 01 | CO: 3 | BT: 2**

**Answer:** *Marker-based tracking is a tracking technique in which a **known, high-contrast, machine-readable pattern (the marker or fiducial — QR code, AprilTag, ArUco, or an IR/LED marker) is placed in the real world**, and the AR device's **camera detects it**, recovers its **6-DOF pose (position and orientation) from its image**, and uses that as a **reference frame to register virtual content precisely on top of the marker**.*

---

### 1.11 — Give one industrial application of AR.
**M: 01 | CO: 1 | BT: 1**

**Answer:** *Overlaying **CAD models onto the real physical structure** so that the as-built site can be compared with the "as-planned" model, improving accuracy, inspection speed and decision-making.* *(Also acceptable: step-by-step **maintenance and assembly instructions displayed directly on the equipment**, reducing dependence on manuals.)*

---

### 1.12 — Mention one entertainment application of VR.
**M: 01 | CO: 1 | BT: 1**

**Answer:** *Immersive VR **gaming**, where the player feels **physically present** in the game world (e.g. Half-Life: Alyx, Beat Saber).* *(Also acceptable: virtual concerts, VR movies and 360° video, and theme-park simulator rides.)*

---

### 1.13 — Compare a Game Object with its Prefab instance in Unity.
**M: 01 | CO: 2 | BT: 2**

**Answer:**

| Basis | **GameObject** | **Prefab Instance** |
|---|---|---|
| **Definition** | The **basic object in a scene** — a container holding components | An **instance created from a Prefab asset (template)** placed in the scene |
| **Where it lives** | In the **Hierarchy** window | In the **Hierarchy**, **linked** to the Prefab in the **Project/Assets** window |
| **Relationship** | **Independent**; not linked to any template | **Connected** to the source Prefab asset |
| **Updates** | Changing it does **not** affect other objects | Changes to the **source Prefab propagate to all instances automatically** |
| **Creation** | `GameObject → Create Empty / 3D Object`, or in code | **Drag the Prefab from Assets into the Hierarchy**, or `Instantiate(prefab)` in code |
| **Purpose** | A **unique** object placed in the level (e.g. the Player) | **Reusable, consistent copies** (e.g. 100 identical Enemies, Bullets, Pickups) |

---

### 1.14 — Differentiate between 2D rotation and 3D rotation.
**M: 01 | CO: 3 | BT: 1**

**Answer:** *In **2-D rotation**, an object rotates about a **single axis perpendicular to the screen** — effectively one angle (clockwise/anticlockwise) — so it stays flat and never appears to turn in depth. In **3-D rotation**, an object rotates about **three independent axes** — **yaw (Y), pitch (X) and roll (Z)** — so it changes its orientation in depth, showing its different sides. 3-D rotation is described by Euler angles or **quaternions** (Euler angles risk **gimbal lock**).*

---

### 1.15 — What is mouse sensitivity?
**M: 01 | CO: 3 | BT: 1**

**Answer:** *Mouse sensitivity is the **setting that determines how much the camera/view rotates in response to a given amount of physical mouse movement** — usually expressed in **cm/360°** or counts per pixel. **High sensitivity** gives small physical movements a large turn (fast, twitchy, precise at short range but prone to **overshoot**), while **low sensitivity** requires large physical movements (**slower, smoother, but tiring and harder to turn quickly**). It should be tuned to the player's **mouse DPI and polling rate**, to their **arm/wrist room**, and to the game's required **turn speed** (slower turn = more time to react to threats in shooters).*

---

### 1.16 — List the types of tracking used in AR/VR.
**M: 01 | CO: 2 | BT: 1**

**Answer:**
- **Sensor-based tracking:** (1) **optical**, (2) **magnetic**, (3) **acoustic (ultrasonic)**, (4) **inertial**
- **Vision-based tracking** (camera/image-based)
- **Hybrid tracking** (sensor fusion of the above, e.g. EKF)
- **Outside-in vs inside-out** architectures; **GPS/GNSS + compass** for outdoor AR
- **Marker-based** and **marker-less** tracking
- **Eye tracking**, **hand tracking** and **full-body tracking**

---

### 1.17 — What is tracking in VR?
**M: 01 | CO: 2 | BT: 1**

**Answer:** *Tracking is the process of **continuously measuring the position, orientation and movement of the user (or a real-world object) in physical space**, so that the computer can **synchronise the virtual world with the user's real body movement** and so the virtual view and interactions respond correctly. Tracking must be accurate, **low latency (< 20 ms)** and high update rate (**≥ 90 Hz**).*

---

### 1.18 — Mention two limitations of marker-based tracking.
**M: 01 | CO: 2 | BT: 1**

**Answer:**
1. It **requires markers to be printed and placed in the environment**, so it cannot work in unknown, moving or uncontrolled outdoor locations without setup.
2. It is **susceptible to occlusion and limited by range and viewing angle** — the marker must be visible to the camera, and works only within a limited distance and angle.

*(Others: sensitive to lighting — poor/uneven illumination breaks detection; accuracy limited by marker size; visual clutter with many markers; markers can be damaged or lost; not scalable to large environments.)*

---

### 1.19 — What is computer vision?
**M: 01 | CO: 2 | BT: 1**

**Answer:** *Computer vision is the branch of AI and image processing that **enables computers to acquire, process, interpret and understand information from images and video** — so they can detect objects, recognise patterns, track motion and infer 3-D structure and depth — performing tasks that normally require human visual perception. In VR/AR it is the enabler of **marker-less tracking, SLAM, inside-out tracking, plane detection and occlusion, and hand/face/body tracking**.*

---
---

## PART B — same pattern: Q2 compulsory + one from each pair

---

### 2(a) — Explain the working and importance of Head-Mounted Displays (HMDs) in VR.
**M: 08 | CO: 3 | BT: 2**

**Answer:**

### What an HMD is
A **Head-Mounted Display** is a device **worn on the head** containing **display(s), optics/lenses and head-tracking sensors**, connected to a computer (or with its own processor). It is the **primary output device** of a VR system, giving a **private, wide-field, immersive** view.
The HMD was the **first device to give an immersive experience** — **Evans & Sutherland demonstrated a head-mounted stereo display in 1965**, and **VPL Research's commercial "EyePhone"** came in **1989**.

### Structure
Headgear · **two small LCD colour screens** · **special optics** for a wide field of view · a **motion tracker** for precise head location · stereo earphones/mic · adjustable head straps with **IPD** and lens-distance dials · (in standalone units) a mobile SoC and cameras for inside-out tracking.

### Working
1. The HMD is **designed to exploit binocular vision**. Each screen is **optically channelled to one eye** and the two screens show **slightly different images** → **stereoscopic 3-D**.
2. Because the display is only a few centimetres from the eyes with special wide-angle optics, it gives a **wide field of view (~100°–120°)** and **excludes the real world entirely**.
3. A **motion tracker continuously measures head position (x, y, z) and orientation (yaw, pitch, roll)** — ideally **6 DOF** — at high rate (90–120 Hz).
4. The measured pose is sent to the image-generating computer, which **updates the scene from the new viewpoint**. The viewer can therefore **look around and walk through the VE**, and the virtual scene appears **locked to the head**.
5. The computer renders the scene **twice**, once per eye, with the two viewpoints offset by the **interpupillary distance (IPD)**, and sends the pair of images to the two displays.
6. **Positional tracking** (present in modern HMDs) additionally tracks the headset's position so the user can **physically move** in the play area; rotational-only tracking allows looking around but not moving within the volume.
7. Simultaneously, **spatial (3-D) audio** and optionally **haptic feedback** are delivered, completing the multisensory illusion.

### Importance
1. **Provides true immersion** — the display is very close to the eye so the FOV is wide and the surrounding real world is excluded; nothing distracts the user and nobody sees their screen.
2. **Provides stereoscopic 3-D** — binocular disparity gives genuine depth perception, essential for virtual objects to look real.
3. **Enables 6-DOF head tracking** — the virtual view follows the head exactly, which is the strongest single driver of **presence**.
4. **Corrects the vergence–accommodation conflict** — the lens optics place the virtual image at a comfortable focal plane, largely removing the eye-strain that flat screens cause.
5. **Combines output, input and sensing in one device** — many HMDs also carry controllers, cameras, microphones and haptics.
6. **Enables the "window into a virtual world"** used in education, training, therapy, design review and entertainment.
7. **Powers standalone, untethered VR** — headsets such as Oculus Quest do the rendering **on-device**, removing the tether and allowing the user to **walk freely** → *room-scale VR*.
8. **Advances in HMDs drove the whole consumer VR industry** from the 2012 Oculus Kickstarter to Rift/Vive/PSVR.

### Limitations
HMDs are **intrusive and uncomfortable** (weight on the neck, heat, cables); **FOV (~110°) is much less than human vision (~200°)** → eye strain and sickness; **expensive**; battery/thermal limits; the "**screen-door**" effect and limited resolution; social isolation; and **motion sickness**. Because of these, **alternative large-display systems were developed — the BOOM (head-coupled box on a multi-link arm) and the CAVE (room-sized multi-person projection environment)**.

---
---

### 2(b) — Explain real-time rendering and scene management in VR software.
**M: 08 | CO: 3 | BT: 2**

**Answer:**

### 1. Real-time rendering

**Definition:** Real-time rendering is the process of **automatically generating the final image — the 3-D scene, textures, lighting, shading and stereo views — from scene data and the user's camera pose, fast enough to keep up with the display's refresh rate**, so the user perceives smooth, continuous motion.

**Why it is critical in VR:** the image must be **stereoscopic**, **low latency** and rendered at **≥ 90 fps** (ideally matching the headset's 90/120 Hz refresh) with **motion-to-photon latency below 20 ms**. Miss this and the user sees judder and suffers **cybersickness**. Real-time rendering is far harder than offline film rendering because the **camera pose changes every frame according to the user's head**.

**Elements of a real-time renderer:**
1. **Scene graph / scene data** — the 3-D objects (meshes, materials, textures, lights) in a hierarchy with transforms.
2. **Transform pipeline** — model → world → view (camera) → projection → clip → screen, using **matrix multiplication** to chain the transforms; the virtual camera's view matrix is taken **directly from the tracked head pose** every frame.
3. **Stereo rendering** — the scene is rendered **twice**, once per eye, with a small horizontal **IPD** offset (or in a single pass using stereo/multiview techniques to halve the cost).
4. **Culling and LOD** — **frustum culling** (skip objects outside the view), **occlusion culling** (skip hidden objects) and **level of detail** (simpler meshes for distant objects).
5. **Lighting and shading** — per-vertex/per-pixel lighting, **real-time shadow maps**, specular highlights, ambient/image-based lighting; **shaders** (HLSL/GLSL/ShaderLab) define surface appearance.
6. **Materials, textures and post-processing** — **MSAA anti-aliasing**, tone mapping, bloom, vignette, colour grading. Motion blur is normally avoided in VR.
7. **Performance optimisations** — **draw-call batching**, **GPU instancing**, **SRP batching**, texture compression, **foveated rendering** (render the periphery at lower resolution using eye tracking), and **pose prediction** to hide latency.

**The per-frame render loop:**
```
1. Read the latest tracked head/controller poses (filtered + predicted)
2. Update the scene via scripts (Update()) and physics (FixedUpdate())
3. Cull to visible objects; choose LODs
4. For each eye: set camera pose -> transform pipeline -> shade pixels
5. Post-process (anti-aliasing, tone map)
6. Submit to the headset display
```

**Challenges:** stereo rendering doubles the cost; high polygon and texture budgets; the ≥ 90 fps target; the strict latency budget; thermal limits on standalone headsets and phones.

### 2. Scene management

**Definition:** Scene management is the **organisation, loading and control of the scenes (levels/worlds) in the application** — how they are structured and ordered, how they are loaded and unloaded at runtime, and how the active scene and camera are switched — while managing memory and performance.

**In Unity:** a **Scene** is a collection of GameObjects, their components and scene settings (lighting, physics, camera), saved as a `.unity` file. It is the unit of level design and resource loading.

**Aspects:**
- **Single-scene vs multi-scene** — all in one scene, or **additive loading** where scenes are loaded on top of each other (e.g. a persistent UI/audio scene plus a level scene).
- **Loading and unloading** — `SceneManager.LoadScene(...)` / `SceneManager.UnloadSceneAsync(...)`; **asynchronous loading** avoids frame hitches.
- **Scene flow and transitions** — fades, loading screens, teleporting the player to a spawn point, transition audio.
- **Persistent objects** — audio sources and managers marked `DontDestroyOnLoad` so they survive scene changes.
- **Managing active cameras** — enabling/disabling or switching cameras (e.g. first-person vs third-person view).
- **Lighting and environment per scene** — baked lightmaps, reflection probes, skybox, fog.
- **Trigger volumes** — collider volumes whose `OnTriggerEnter` loads the next scene, giving level progression.
- **Resource and memory management** — **object pooling** (reuse instead of create/destroy to avoid GC and stutter) and **Addressables** for memory-managed asset loading.
- **Build settings** — scenes added to the build list in the correct order.

```csharp
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string nextSceneName;              // set in the Inspector

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            SceneManager.LoadScene(nextSceneName);   // switch level
    }
}
```

**Why scene management matters in VR:** VR applications are large (many environments) but must load **fast and seamlessly** or immersion breaks; the **runtime memory budget is limited** on standalone HMDs and phones; and the scene must **update the camera instantly to the tracked pose**. Poor scene management produces **loading hitches, memory spikes and stutter**, which the VR user feels immediately as **discomfort and sickness**.

---
---

### 3(a) — Develop a program that Rotate a Cube Continuously on Y-axis.
**M: 08 | CO: 3 | BT: 3**

**Answer:**

```csharp
using UnityEngine;

public class CubeRotation : MonoBehaviour
{
    public float degreesPerSecond = 90f;      // speed; editable in the Inspector

    void Update()
    {
        // Rotate 90 degrees per second around the Y (vertical) axis
        transform.Rotate(0f, degreesPerSecond * Time.deltaTime, 0f);
    }
}
```

### Version 2 — using a Vector3 (matches the course slide)

```csharp
using UnityEngine;

public class SimpleRotation : MonoBehaviour
{
    public Vector3 rotationPerSecond;          // e.g. (0, 45, 0)

    void Update()
    {
        // Multiply by Time.deltaTime so the speed is the SAME at any frame rate
        transform.Rotate(rotationPerSecond * Time.deltaTime);
    }
}
```
**Steps:** create the script in the Project window → drag the `.cs` onto the Cube in the Hierarchy (or `Add Component`) → in the Inspector set **X = 0, Y = 45, Z = 0** → press **Play**.

### Version 3 — smooth acceleration/deceleration

```csharp
using UnityEngine;

public class CubeRotationSmooth : MonoBehaviour
{
    public float maxDegreesPerSecond = 90f;
    public float smoothTime = 0.5f;
    float currentSpeed;

    void Update()
    {
        currentSpeed = Mathf.SmoothDamp(currentSpeed, maxDegreesPerSecond,
                                        ref currentSpeed, smoothTime);
        transform.Rotate(0f, currentSpeed * Time.deltaTime, 0f);
    }
}
```

### Version 4 — rotate around a pivot (not the object's own centre)

```csharp
using UnityEngine;

public class CubeRotateAround : MonoBehaviour
{
    public Transform pivot;
    public float degreesPerSecond = 90f;

    void Update()
    {
        pivot.Rotate(0f, degreesPerSecond * Time.deltaTime, 0f);
    }
}
```

### Version 5 — new Input System (rotate only while the user holds a button)

```csharp
using UnityEngine;
using UnityEngine.InputSystem;

public class CubeRotationInput : MonoBehaviour
{
    public float degreesPerSecond = 90f;
    public InputActionProperty rotateAction;

    void Update()
    {
        float value = rotateAction.action.ReadValue<float>();
        if (value != 0f)
            transform.Rotate(0f, value * degreesPerSecond * Time.deltaTime, 0f);
    }
}
```

**Key points for the exam:**
- **`transform.Rotate(x, y, z)`** — the three arguments are **degrees about X, Y, Z respectively**. The **Y argument is the Y-axis**, so continuous Y rotation is `transform.Rotate(0f, speed * Time.deltaTime, 0f)`.
- **`Time.deltaTime`** makes the rotation **frame-rate independent** — the cube turns at the same degrees per second whether the frame rate is 60, 90 or 144 fps. Without it the speed depends on FPS and the rotation becomes jerky.
- In **Euler angles**, Y is **yaw**; `Vector3 rotationPerSecond = new Vector3(0, 45, 0)` means **45° per second about Y** (360° = one full rotation per 8 seconds).
- **Negative values** rotate the opposite way.
- Use **`Update()`** for continuous rotation; use **`FixedUpdate()`** only if the rotation is being driven by rigidbody physics.

---
---

### 3(b) — Describe the types of assets available in the Unity Asset Store.
**M: 08 | CO: 3 | BT: 2**

*(OR of 3 — attempt either)*

**Answer:**

The **Unity Asset Store** is an online marketplace where developers and artists publish reusable assets for Unity projects. Assets are mainly of the following types:

| # | Asset type | Description / examples |
|---|---|---|
| **1** | **3-D models** | Characters, vehicles, props, buildings, environments, **vegetation**, weapons, furniture, **Furniture Kit**, **Stylized/Realistic packs**. Usually in **FBX/glTF**, with **PBR materials and textures**; import settings control **scale, compression** and whether to **generate colliders**. |
| **2** | **2-D sprites, textures and artwork** | **Sprites, sprite sheets, UI skins, icons, particle textures, tileable textures, materials, normal maps**, for 2-D or UI work. |
| **3** | **Audio** | **Music, sound effects, ambience, UI clicks, voice, footsteps, 3-D/ambisonic audio**, and complete audio systems. |
| **4** | **Shaders, materials and visual effects (VFX)** | **Shader graphs, toon/realistic shaders, post-processing stacks, particle systems, VFX Graph shaders**, and **URP/HDRP shader libraries**. |
| **5** | **Scripts, tools and editor extensions** | **C# frameworks and utilities, editor tools, build tools, code generators, debugging/profiling tools, custom inspectors, ScriptableObject frameworks**. |
| **6** | **Animation assets** | **Animation clips, humanoid and generic rigs, controllers (Animator state machines), blend trees, motion-capture data** for Mecanim. |
| **7** | **Physics assets** | **Collision/physics materials, joint presets, cloth, particle collision shapes, PhysX settings presets**. |
| **8** | **GUI / UI kits** | **UI toolkits, HUD templates, menu systems, VR/AR UI kits** (buttons, panels, reticles, interaction UI). |
| **9** | **VR/AR and XR tools** | **XR Interaction Toolkit, XR Interaction Toolkit Starter Assets, OpenXR/Oculus/ARKit packages, hand-tracking, gestures, teleport/locomotion systems, MRTK, interaction feedback/haptics.** |
| **10** | **Fonts and text** | **Fonts, SDF fonts, font shaders** for UI and 3-D text. |
| **11** | **Full projects, templates and starter assets** | Complete **sample projects, complete games, templates, boilerplates** — a whole working project to start from or learn from. |
| **12** | **Services and support** | **Cloud Build, Analytics, Asset Store Tools, Unity Learn/Game Templates, In-app purchases, Burst/Jobs packages, AI (Sentis / ML), networking (Netcode, Photon)**. |

**Other things found in the store:** **Frameworks**, **Sprite Editor/2D packages**, **Font assets**, **Plugins for specific platforms**, **Editor extensions**, and **paid/free "Asset Store Tools"**.

**Notes for the exam:**
- Assets come as **packages imported into `Assets/`** (or the Package Manager for official packages); third-party assets are usually imported as **`.unitypackage`** files.
- They may be **free or paid**; a **paid asset is a one-time purchase with a seat/license**, and some are **restricted to a single seat or a team licence**.
- Import settings control texture **compression and max size**, model **Read/Write and Generate Colliders/Materials**, and audio **Load Type (Decompress on Load / Compressed in Memory / Streaming)**, **Force To Mono**, and **3-D Sound** — these settings strongly affect VR performance.

---
---

### 4(a) — Describe the significant features of Unity that make it a preferred platform for creating interactive games.
**M: 08 | CO: 2 | BT: 2**

*(OR of 3 — attempt either)*

**Answer:** *(Expanded version of Paper 1, Q2(c) — write in full for 8 marks.)*

**Unity** is a **cross-platform, general-purpose real-time 3-D (and 2-D) game development engine** from Unity Technologies, and it is preferred because:

1. **Cross-platform deployment** — one project builds for **20+ platforms**: Windows, macOS, Linux, Android, iOS, WebGL, Xbox, PlayStation, Switch and VR/AR headsets. Huge reach for a small team, and a single codebase serves PC, mobile and console.
2. **Integrated all-in-one IDE** — scene design, asset import and management, physics, animation, UI, audio, profiling, packaging and scripting all in **one editor**. No need to juggle many external tools, which cuts pipeline complexity.
3. **Powerful C# scripting with a rich API** — the full .NET class library plus built-in APIs for physics, audio, UI, animation, particles, AI and XR. C# is a mature, readable, well-documented language, so it is easy to learn and to hire for.
4. **First-class VR/AR/MR support** — built-in **XR Plug-in Management**, the **XR Interaction Toolkit**, and support for **OpenXR, Oculus/Meta, SteamVR, ARCore, ARKit, HoloLens and WebXR**. VR can be enabled from Project Settings in minutes. The course notes state Unity is the **most widely used VR/AR/MR platform**, with **over 91% of Vuforia and HoloLens experiences** built with Unity.
5. **Huge ecosystem and Asset Store** — thousands of ready-made **models, animations, sounds, shaders, scripts, VFX, UI kits and complete projects**, so teams do not rebuild common systems. Also an **In-app purchase and analytics** ecosystem.
6. **Massive community and learning resources** — millions of developers, extensive documentation, **Unity Learn**, tutorials, forums and conference talks. Easier to find help, tutorials and collaborators, and to hire talent.
7. **Powerful 2-D engine** — Tilemap, Sprite, 2-D physics and 2-D animation make Unity excellent for mobile 2-D games as well as 3-D.
8. **Advanced built-in physics** — a PhysX-based **rigidbody, collision, joints and raycast** system with easy primitive colliders (Box, Sphere, Capsule, Terrain, Wheel, Mesh), **layer collision matrix** and multiple **collision detection modes**.
9. **Prefab and Asset system** — reusable **Prefab templates** with automatic propagation to instances, organised **Assets folder** structure, and **Addressables** for memory-managed loading. This directly supports efficient content pipelines.
10. **Modern graphics and rendering** — **Universal Render Pipeline (URP)** for mobile/VR and **HDRP** for high-end, real-time global illumination, **GPU instancing**, SRP batching, shadow techniques and a post-processing stack. **Foveated rendering** for XR performance.
11. **Powerful animation system** — **Mecanim/Animator** with humanoid and generic rigs, **IK**, blend trees, state machines and root motion, plus motion-capture import.
12. **Built-in UI, audio and AI** — **UI/UX Toolkit (uGUI)** and UI Toolkit for menus/HUDs, a full **audio system** with 3-D/spatial sound and reverb zones, and built-in **pathfinding/NavMesh AI**.
13. **Fast iteration and live reloading** — scripts can be reloaded while playing in the editor, giving a very short edit-test loop.
14. **Good profiling and debugging** — **Unity Profiler** (CPU/GPU frame timing), **Memory Profiler**, **Frame Debugger**, **Console** with stack traces, and **development builds** with an on-screen profiler — essential for hitting 90 fps in VR.
15. **Efficient enough for mobile and XR** — URP, dynamic resolution, GPU instancing, occlusion culling and good battery behaviour on standalone headsets and phones.
16. **Scripting backends for every target** — **Mono** for fast iteration and **IL2CPP** for AOT, performance and deep profiling on consoles and mobile.
17. **Affordable and accessible** — a **free Personal edition** for students, educators and small studios, with professional features and source-code access at higher tiers.
18. **Strong asset and package interoperability** — imports **FBX, OBJ, glTF, PSD, WAV** etc., and exports to major engine and 3-D formats.

**Conclusion:** *Unity is preferred because it combines a single integrated editor, powerful C# scripting, first-class XR support, a vast asset ecosystem, an enormous community, cross-platform deployment from one codebase, and the performance and profiling tools needed to hit 90 fps on standalone headsets and phones — all at a very low or zero licence cost.*

---
---

### 4(b) — Discuss the advantages and limitations of using the Unity Asset Store.
**M: 08 | CO: 2 | BT: 2**

*(OR of 3 — attempt either)*

**Answer:**

### Advantages

1. **Saves a great deal of time and money** — ready-made models, animations, shaders, scripts and complete systems do not have to be built from scratch. This shortens development time dramatically.
2. **Large, high-quality library** — thousands of assets covering almost every category: 3-D models, 2-D sprites, textures, audio, VFX, UI kits, shaders, tools and frameworks, from both established studios and individuals.
3. **Professional quality** — many assets are produced by professional studios with **PBR materials, LODs, proper topology, animations and documentation**, and are often **better than what a small team could produce**.
4. **Tested and maintained** — popular assets are **updated for new Unity versions**, and many come with **documentation, sample scenes and support**.
5. **Proven reliability** — widely used assets are **battle-tested** by the community, so bugs and Unity-version incompatibilities have usually already been found and fixed.
6. **Reduces the need for specialists** — a small team can get high-end character rigging, particle VFX, dialogue systems or networking without hiring an expert.
7. **Enables rapid prototyping** — a prototype or a proof-of-concept can be assembled in hours, which is vital for pitching and for game jams.
8. **Integrated workflow** — assets import straight into `Assets/` as `.unitypackage` files, and the store also offers **free and trial versions**, plus **official packages** via the Package Manager.
9. **Learning resource** — the store contains many **complete sample projects and templates**, so developers can study how a full game is structured.
10. **Long-tail availability and customisation** — niche requirements (a specific aircraft, a specific shader effect, an AR tracking package) can usually be found rather than built.
11. **Affordable relative to the alternative** — professional assets are far cheaper than the staff time needed to create the same content.

### Limitations / disadvantages

1. **Cost** — quality assets can be expensive, and a project can need many of them, so total spend adds up quickly.
2. **Licence restrictions** — licences may limit the number of **seats**, prevent use in a **published game**, or restrict redistribution; **Asset Store Terms of Service must be checked**.
3. **Quality is inconsistent** — the store is **crowded**; there is no guarantee of correctness, optimisation or support. Many assets are amateur-made or abandoned.
4. **Version incompatibility and dependency conflicts** — assets built for an old Unity version may break, and different assets may depend on **conflicting versions of the same package**, requiring manual dependency resolution.
5. **Third-party code quality and security** — you are importing **unreviewed code**; poorly written code can cause **memory leaks, GC spikes and frame drops**, which are especially damaging in VR. Code is also hard to maintain once the original author stops supporting it.
6. **Performance overhead** — heavy assets (high-poly models, unoptimised shaders, large textures) can **blow the memory and GPU budget** on mobile and standalone headsets, causing stutter or thermal throttling. **Default import settings are often wrong** for VR and must be corrected per asset.
7. **Hidden coupling and "dependency hell"** — relying on a third-party framework can make upgrading Unity or removing the asset very difficult.
8. **Redundancy and bloat** — unused sample assets, editor scripts and dependencies remain in the project, increasing build size and clutter.
9. **Learning curve for third-party frameworks** — a purchased framework has its own conventions, APIs and documentation, so developers must learn it before they can customise it.
10. **Support may lapse** — the original developer may stop updating the asset, so the project becomes stuck on an old version and may be abandoned when a new Unity release arrives.
11. **No guarantee of supply-chain integrity** — an asset could be removed, or the developer account could disappear.
12. **Creative/identity concerns** — using off-the-shelf assets can make a project look generic or infringe third-party IP if licences are not respected.

### Conclusion
*The Asset Store is a huge productivity multiplier — it provides professional, tested, well-documented content that saves months of work and lets small teams build games they could not otherwise afford. But it is not risk-free: budget for licences, **verify Unity-version compatibility and import settings**, inspect the asset's documentation and reviews, and **profile it in a build** before committing, because version conflicts, poor-quality code and performance overhead can cost more time than the asset saved.*

---
---

### 5(a) — Analyze the importance of triangle meshes for virtual objects.
**M: 08 | CO: 3 | BT: 3**

*(OR of 3 — attempt either)*

**Answer:** *(Expanded for 8 marks.)*

**Triangle mesh** = a set of **vertices** joined by **edges** to form **faces**, where every face is a **triangle**. A quadrilateral is two triangles sharing an edge; quads are split into triangles because a GPU can only rasterise triangles efficiently.

### Importance

1. **The GPU's native primitive.** Rasterisation, texture mapping, **normal interpolation** and vertex shaders all operate on triangles. Triangles are therefore the **fastest, most hardware-friendly way to draw geometry** — no triangulation at render time.
2. **Any surface can be represented.** Triangles approximate **any** surface, flat or curved: a cube is 12 triangles, a sphere is many thin triangles, terrain and characters are irregular meshes. One representation covers everything.
3. **Exact representation of flat polygons.** Any flat polygon can be represented exactly by triangles, whereas N-gons (quads, pentagons) can render incorrectly. Splitting consistently along a diagonal also prevents the **"cracks"** that appear when adjacent quads choose different diagonals — which is why Unity splits quads into **two triangles** and offers **Quad/Tri topology toggle** in model import settings.
4. **Efficient rendering for real-time VR.** An **index buffer** stores vertex indices, so shared vertices are transformed once and reused — a quad grid costs half the vertex work of two independent quads. **Hardware instancing** also works on triangle lists. Reducing triangle count is the single most important lever for hitting **stereoscopic 90–120 fps** in VR.
5. **Physics collision.** Unity generates **convex hulls** from mesh vertices for **Mesh Colliders** so collision detection can be computed quickly. A mesh must be **convex for dynamic (moving) objects**; static objects may use a full concave mesh collider. This lets a virtual object have a real physical presence.
6. **Skeletal skinning and animation.** Character animation **binds** a mesh to a skeleton, assigning each **vertex** to bones with **weights**. Vertices are then deformed by the bones, and because a triangle is deformed as a unit, smooth bending is obtained. Without triangle meshes, rigged character animation would be impossible.
7. **Deformation and procedural geometry.** **Height-map terrain, cloth simulation, water surfaces, caves, morphing and explosion deformation** all work by **moving vertices**, and the triangles stretch with them. This is what makes terrain and soft-body effects possible at all.
8. **Level of Detail (LOD).** Meshes can be **decimated** — removing triangles while preserving the silhouette — which is the basis of **LOD groups** and automatic LOD generation. LOD is a primary VR optimisation technique.
9. **Separate render and physics meshes.** The **visible** mesh can be detailed while a **separate simplified convex collider** is used for physics, keeping both visual quality and performance high. This is impossible with single-sided geometry.
10. **Normals, UVs and tangents.** Normals (for lighting), **UV coordinates** (for texturing) and tangents (for **normal mapping**) are carried per vertex or per triangle corner — the data shading needs. **Smoothing groups / normals recalculation** is a triangle-mesh operation.
11. **Storage, compression and streaming.** Index buffers, half-float/quantised vertex formats and **mesh simplification/compression** keep meshes small enough to download and hold in memory — an explicit MR research direction in the course notes.
12. **Terrain and LOD tiles.** Large worlds are built from **mesh tiles/chunks**, each its own mesh, which can be loaded, generated and culled independently.

### Conclusion
*Triangle meshes are important because they are the only polygon primitive the GPU processes natively, they can approximate any surface exactly enough, index buffers and instancing make them cheap to draw — essential because VR renders them **twice per frame at 90–120 fps** — and they double as the geometry for **physics collision** and **skeletal animation**. Managing triangle count and topology quality is therefore the most important optimisation and art task in real-time 3-D.*

---
---

### 5(b) — Analyze the importance of yaw, pitch and roll in 3D rotation.
**M: 08 | CO: 3 | BT: 3**

*(OR of 5 — answer either)*

**Answer:** *(Expanded 8-mark version.)*

**Yaw, pitch, roll (YPR)** are the three **Tait–Bryan angles** describing an object's **orientation** in 3-D space — the three **rotational degrees of freedom**. With x, y, z they give a full **6-DOF pose**.

| Angle | Axis | Motion | Everyday example |
|---|---|---|---|
| **Yaw** (ψ) | **Vertical Y** | **Left ↔ right** | Turning the head or a steering wheel; saying "no" |
| **Pitch** (θ) | **Horizontal X** | **Up ↔ down** | Nodding ("yes"); raising a gun barrel |
| **Roll** (φ) | **Z** (line of sight) | **Sideways tilt** | Tilting the head to the shoulder; banking an aircraft |

### Importance

1. **They completely specify orientation.** Position (x, y, z) says *where* an object is; without rotation it is not fully placed. YPR provide the **3 rotational DOF** needed for a full **6-DOF pose**.
2. **They are exactly what a 6-DOF tracker measures.** The VR HMD must report **yaw, pitch and roll** so head turns and tilts produce the matching change of view. This is the core of **head tracking** and therefore of **presence**.
3. **Yaw is the most frequently used and most critical axis in VR.** Sideways head rotation is the most common movement, so yaw error is the most noticeable and the most sickness-inducing; yaw must be tracked with very low error.
4. **They define the virtual camera orientation.** The renderer uses yaw/pitch/roll to build the camera's **view matrix**, so getting them right is what makes the virtual world appear **anchored to the user's head** rather than drifting.
5. **They control interaction and aim.** In a first-person shooter, the **yaw and pitch of the controller** determine the aim direction; **roll** is used for actions like reloading a revolver's cylinder or tilting a steering wheel.
6. **They enable locomotion and navigation.** Yaw turns the user; pitch can tilt the camera or set the flight/climb angle; roll banks the view in vehicles.
7. **They are essential for object manipulation.** Rotating a 3-D model, orienting a placed component, or arranging furniture all use yaw/pitch/roll, and a 6-DOF controller lets the user do this naturally.
8. **They are the basis of animation and rigging.** Every bone rotation in skeletal animation is expressed in these angles; Unity's **Animation Curves store rotation as x (pitch), y (yaw), z (roll) in degrees**.
9. **Rotation order matters and must be consistent.** Rotations about different axes **do not commute** — yaw-then-pitch is not pitch-then-yaw. An inconsistent order gives objects unexpected orientations, so a fixed convention must always be used.
10. **Euler angles vs quaternions.** YPR are intuitive to author but suffer **gimbal lock** (at pitch = ±90°, yaw and roll become dependent and one axis loses effect) and interpolate badly. Unity therefore **displays rotation as Euler angles in the Inspector but stores it internally as a quaternion**, and `Quaternion.Euler(x, y, z)` builds the rotation from them.
11. **They are the axis convention used throughout VR.** Unity's Inspector shows **X = pitch, Y = yaw, Z = roll**, matching the axes of `Vector3` — so scripts such as `transform.Rotate(0, 45 * Time.deltaTime, 0)` are directly yaw rotations.

### Conclusion
*Yaw, pitch and roll are the three rotational degrees of freedom that, together with x, y, z, define the complete pose of an object. In VR they are essential because the virtual camera must reproduce the user's head orientation **exactly, in every frame** — any mismatch in yaw, pitch or roll immediately destroys the illusion of presence and causes discomfort, so accurate, low-latency orientation tracking is a core hardware and software requirement.*

---
---

### 6(a) — Analyze how collision detection contributes to interaction realism in VR training simulations such as surgery, engineering, or military applications.
**M: 08 | CO: 2 | BT: 3**

*(OR of 5 — attempt either)*

**Answer:**

### What collision detection does
Collision detection is the process of determining **when and where two colliders intersect**, and collision response then computes the physical reaction (impulse, friction, bounce). In Unity this runs in the **PhysX** engine in two stages — a **broad phase** (cheap bounding-volume test to find possible pairs) and a **narrow phase** (exact geometry test producing contact points and normals).

### How it contributes to interaction realism

1. **Objects behave as solid things.** Collision makes a virtual instrument **stop at a surface** instead of passing through it, so objects appear to have **mass, thickness and form** rather than being flat images.
2. **It produces force and haptic feedback.** A collider + Rigidbody collision generates the **impulse** that drives the **haptic feedback** in the controller/glove. The surgeon therefore *feels* the instant the scalpel meets tissue — this **feeling of resistance is the single most important learning signal** in surgical simulation.
3. **Correct depth and force control.** In surgery, penetrating too deep is the critical error. Collision lets the system report **contact depth, contact force and penetration**, so the trainee learns to control force and depth — the skill that determines patient safety.
4. **Collision events drive the training logic.** `OnCollisionEnter`, `OnCollisionStay`, `OnTriggerEnter` and `OnTriggerExit` let the simulator detect that an instrument touched a target, that a tool was inserted into a port, or that a vessel was cut — so the scenario can **score performance and branch**.
5. **Multi-entity interaction.** Multiple instruments, retractors, needles, bones, or a vehicle chassis must interact with each other and with the environment; without collision they pass through one another and all realism is lost.
6. **It provides the boundaries of the task.** Triggers and colliders define the **valid working volume** — where a drill may be used, which structures are fragile, where the safe corridor lies — teaching spatial constraints.
7. **Physics realism creates presence.** Consistent, believable physical responses keep the user's internal model of the world valid, so the user **sustains presence and immersion** rather than feeling a technical demo.
8. **It prevents nausea and builds confidence.** Jittering or missing collisions break the illusion and cause visual/vestibular conflict, so reliable collision improves **comfort and confidence**.
9. **It enables safe, repeatable practice.** Because collisions are resolved by equations rather than by chance, the same procedure can be repeated **thousands of times** with identical, measurable outcomes — the basis of a **valid, standardised assessment**.

### Application-specific value

**Surgery / medicine**
- Virtual scalpel, forceps and needle interact with virtual tissue; the trainee feels the tissue give way, sees bleeding, and hears the tool.
- The wrong path (e.g. cutting an artery) can be detected and scored, so the trainee learns the consequences **without risking a real patient**.
- Enables rehearsal of rare, dangerous or complex procedures before the operation.

**Engineering**
- Virtual assembly of an engine or a pipeline: parts must be inserted in the correct order, orientation and depth; a part inserted wrongly simply **will not fit**, exactly as in reality.
- Interference detection (part clashes) and force/torque limits model tightening a bolt to the correct specification.
- Validates clearances, maintenance access and ergonomics before fabrication.

**Military and defence**
- Realistic weapon handling, recoil, sight alignment and ballistics; hitting or missing a target is determined by geometry, not by a scripted animation.
- Vehicle and aircraft simulation — landing gear contact with the ground, collisions with terrain and structures.
- Blast, fragmentation and cover mechanics; mission rehearsal with **correct, repeatable physics**.

### Conclusion
*Collision detection is what converts a passive 3-D model into a believable, interactive object. In VR training it supplies the **contact feedback, force and depth cues**, enforces the **spatial and procedural constraints** of the task, generates the **events** that score and branch the scenario, and makes practice **safe and repeatable**. Because the user's brain judges realism largely by whether objects push back correctly, effective collision detection is a direct determinant of skill transfer from the simulator to the real world.*

---
---

### 6(b) — Compare 2D rotation and 3D rotation with suitable examples.
**M: 08 | CO: 2 | BT: 2**

*(OR of 5 — attempt either)*

**Answer:**

**Definition:** **2-D rotation** is rotation about a **single axis perpendicular to the plane of the object** (in 2-D space, the Z axis), so the object turns **within its own plane**. **3-D rotation** is rotation about **three mutually perpendicular axes** — **yaw (Y), pitch (X) and roll (Z)** — so the object changes its orientation in **depth**, exposing its different sides.

| Basis | **2-D Rotation** | **3-D Rotation** |
|---|---|---|
| **Axis / DOF** | **One axis** (perpendicular to the screen) — **1 rotational DOF** | **Three axes** (X, Y, Z) — **3 rotational DOF** |
| **Parameters** | A **single angle** (θ), clockwise or anticlockwise | **Three angles**: yaw (Y), pitch (X), roll (Z) |
| **Representation** | A scalar angle; matrix `[[cosθ, −sinθ], [sinθ, cosθ]]` | Euler angles (x, y, z) or a **quaternion** (x, y, z, w) |
| **Direction of effect** | The object **stays flat**; its area and outline are unchanged | The object appears to **turn in depth**, showing different faces |
| **Effect on the object** | Only **orientation in-plane** changes; no face becomes visible that was not already visible | Different faces, edges and surfaces come into view |
| **Order dependence** | No order problem — a single axis | **Order matters**: yaw-then-pitch ≠ pitch-then-yaw; rotations **do not commute** |
| **Interpolation** | Simple, always valid | Euler angles suffer **gimbal lock** (pitch = ±90° → yaw and roll become dependent, one axis loses effect); **quaternions avoid this** and interpolate smoothly |
| **Composition** | Angles simply add | Requires matrix multiplication or quaternion multiplication |
| **Cost** | Trivial | Three times the work (three matrices/quaternons), though on GPU it is usually merged into one matrix |
| **Where used** | 2-D UI, sprite rotation, top-down/isometric games, radar/PPI displays, clock hands, 2-D sprite animation | **Cameras and VR headsets**, aircraft/vehicle attitude, spacecraft, robots and robotic arms, 3-D object manipulation, ball-and-socket joints, first/third-person controller camera orientation, character animation |
| **Data in Unity** | `Quaternion.Euler(0, 0, θ)` | `Quaternion.Euler(x, y, z)` — stored as **X = pitch, Y = yaw, Z = roll** |

### Suitable examples

**2-D rotation**
- A **clock hand** sweeping around a dial.
- A **radar/PPI display** rotating a sweep line.
- A **top-down 2-D game** where a tank or a character sprite turns clockwise to face a new direction.
- A **2-D UI icon or loading spinner** rotating in place.
- **Isometric/2.5-D games** where characters are constrained to the XZ plane.

**3-D rotation**
- **A VR headset / first-person camera** — the user looks left/right (**yaw**), up/down (**pitch**) and tilts their head sideways (**roll**); all three are tracked so the virtual view matches the head exactly.
- **A pilot's attitude** — yaw = heading, pitch = climb/dive, roll = bank.
- **A spacecraft** — the same three angles define its orientation, plus translation along the axes.
- **A robot arm / 6-axis industrial robot** — each joint is a rotation; the tool-point pose is computed from the six joint angles.
- **Rotating a 3-D object in a CAD viewer** — dragging with the mouse rotates it about all three axes.
- **A human head or a bone in skeletal animation** — orientations stored as Euler angles (pitch, yaw, roll) or quaternions.
- **A ball-and-socket joint or a steering wheel/joystick** used in flight simulators.

### Conclusion
*2-D rotation concerns a **single angle about one axis**, keeps the object flat, and is simple and cheap — sufficient for top-down games and UI. 3-D rotation concerns **three angles about three orthogonal axes**, lets an object turn in depth and reveal its sides, is required for **cameras, VR headsets, vehicles and robots**, and must be handled carefully because **order matters** and **Euler angles risk gimbal lock** — which is why Unity stores rotation internally as a **quaternion**.*

---
---

### 7(a) — Discuss the advantages and limitations of mouse-aimed camera systems.
**M: 08 | CO: 3 | BT: 3**

*(OR of 7 — attempt either)*

**Answer:**

### What a mouse-aimed camera system is
A mouse-aimed camera system is a **camera (and player) control scheme in which the aim direction is determined by the position of the mouse cursor on screen** rather than by mouse *movement relative to the camera*. The camera performs a **raycast from the camera through the screen point under the cursor** (or uses a screen-space offset) to find what the player is aiming at and to set the view direction.

Common examples: point-and-click adventure games, **twin-stick shooters**, most **desktop RTS/FPS hybrids**, and **VR "desktop theatre"/mirror-mode** setups.

### Advantages

1. **Simple and intuitive** — the cursor is a visible, absolute indicator of intent; a new player immediately understands "put the pointer on the thing".
2. **No ambiguity about the target** — the cursor is *on* the object, so the player never has to guess where they are aiming; there is **no crosshair drift**.
3. **Precise selection** — clicking exactly on a small object is accurate; useful for **inventory, UI, dialogue, puzzle and strategy** interactions where precision matters more than speed.
4. **Works naturally with the UI** — menu items, buttons and text are clicked directly; no separate interaction mode is needed, and **right-click can be bound to a second action** (aim, alt-fire).
5. **Low hardware cost and high availability** — a mouse is included with every PC; no extra tracking hardware, no battery, no calibration, and it works on any display resolution.
6. **Comfortable for extended play** — a desk surface supports the arm and permits large, quick movements, so long sessions are **far less fatiguing** than mid-air or handheld input.
7. **Absolute, position-based control** — the cursor does not accumulate error over time, unlike relative mouse-look which drifts if limits are hit; the aim point is always exactly where the pointer is.
8. **Precise depth/scale selection** — because the camera can be positioned and the FOV changed, the player can zoom in on a specific object and click it precisely (useful in **tower-defence, RTS and building games**).
9. **Keeps the world visually stable** — since the camera does not swing wildly, the scene is easier to read and less nausea-inducing.
10. **Two-handed freedom** — the left hand can be on **WASD** for movement while the right hand aims, giving simultaneous movement and aiming with one device.
11. **Suited to long-distance and careful aiming** — for sniping or for tasks needing deliberate, careful aim, a rested arm on a desk is much steadier.
12. **Easy to add secondary inputs** — scroll wheel for zoom, middle-click for pan, modifier keys for modifiers, all natural.

### Limitations

1. **There is no mouse in VR.** This is the decisive limitation: inside an HMD the user has **no cursor and no desk surface**, so a mouse-aimed camera **cannot be used at all**. Mouse-aimed schemes are inherently **non-immersive, 2-D-desktop schemes** and are the main reason VR ports of PC games use a **mirror/dummy-desktop mode** that feels clunky and disconnected.
2. **Constrained by the screen plane (2-D aiming).** The aim point lies on the screen plane, so aiming at objects **above, below or behind the view** is awkward — the player must move the camera or character to reach off-screen targets.
3. **Very limited field of view for interaction.** The user can only choose a target within the current view; there is no way to aim "behind you" or "at the floor near your feet" without turning.
4. **Requires a desk and physical space.** It cannot be used standing up, while walking, or in a living-room-style setup, so it is unusable for room-scale VR.
5. **Lethargy / stopping problem.** Moving the cursor to a distant target takes time; **the player stops moving while aiming**, which breaks the flow of action games and slows combat.
6. **Poor for fast, continuous aiming.** Real-time shooters need **fast, sweeping, analogue aiming**; cursor repositioning is jerky and imprecise for tracking a moving target.
7. **Occlusion and depth ambiguity.** The raycast hits the **nearest** object along the ray, so a target **behind** another object cannot be selected unless the game provides an outline or a layer toggle — frustrating for cover-based play.
8. **Not immersive / breaks presence.** A cursor on a flat monitor destroys the sense of embodiment; the player is looking *at a screen*, not being *inside* the world.
9. **Cognitive translation.** The player must map from **cursor space** to **world space**, adding a small mental step, whereas in first-person view the view *is* the aim.
10. **Needs a fixed viewing position.** It ties the player to a desk and a screen; it does not scale to living-room or projected displays.
11. **Motion sickness risk in VR emulation.** Faking camera rotation with a mouse in a headset produces **artificial, discontinuous view motion** that conflicts with the vestibular system.

### Conclusion
*Mouse-aimed camera systems are **simple, precise, cheap, comfortable and excellent for UI, dialogue, strategy and point-and-click interaction**, and they remain the best scheme for desktop-only input. They are, however, **fundamentally a 2-D-desktop paradigm**: they require a **desk, a cursor and a screen plane**, they cannot aim outside the current view, they slow down fast action, and — decisively — **they cannot be used in VR at all**, because an HMD provides neither a cursor nor a desk surface. Hence VR uses head, hand/eye tracking and motion controllers instead.*

---
---

### 7(b) — Discuss the role of modern tools such as Blender, ZBrush, Unity, and Unreal Engine in creating immersive AR/VR experiences.
**M: 08 | CO: 3 | BT: 2**

*(OR of 7 — attempt either)*

**Answer:**

Immersive AR/VR experiences are produced by a **pipeline** in which different tools are used at different stages. No single tool does everything, so modern content creation deliberately combines them.

### 1. Blender — 3-D modelling, sculpting, rigging, animation and rendering
- **Free and open-source, cross-platform** full 3-D suite — the backbone of independent AR/VR content.
- **Polygon modelling** (Object Mode / **Edit Mode**, Tab) and **Sculpt Mode** for organic, high-resolution shapes.
- **Retopology** to produce clean, **low-poly, quad-dominant meshes optimised for real-time** and **normal-map baking** from the sculpt.
- **Rigging and skinning** (armature, bones, **weight painting**, IK) and animation — **Unity cannot create rigs**, so characters are rigged here and exported.
- **UV unwrapping** and texture painting in the **node-based shader editor**.
- **EEVEE** (fast real-time render — ideal for VR previews) and **Cycles** (path-traced offline quality).
- **Physics simulation** (cloth, rigid body, fluids), **compositor**, **Python scripting** and add-ons for pipeline automation.
- Exports **FBX / glTF / USD** straight into Unity and Unreal.

### 2. ZBrush — specialist high-detail sculpting and texturing
- Built specifically for **digital sculpting**; handles **millions/billions of polygons**, so extremely fine surface detail (skin pores, wrinkles, fabric weave, scales) is achievable that no polygon modeller could reach.
- **ZRemesher / retopology tools** generate clean game-ready topology from the sculpt, and **UV/projecting tools** prepare it for texturing.
- Creates **creature, character and environment assets** that are then retopologised and baked into normal maps, giving **rich appearance at very low polygon cost** — exactly what mobile and VR require.
- **ZBrushCore, Pixologic/ Maxon ecosystem** and **3D Coat / Marmoset** integrate into a texturing pipeline.
- **Role in AR/VR:** produces the **high-detail form and texture** from which efficient real-time assets are derived.

### 3. Unity — the real-time engine, and the dominant AR/VR platform
- **Cross-platform real-time 3-D/2-D engine** using **C#**; the course notes state it is the most widely used VR/AR/MR platform, with **over 91% of Vuforia and HoloLens experiences** built with it.
- **First-class XR support** — **OpenXR**, **XR Interaction Toolkit**, Oculus/Quest, SteamVR, ARCore, ARKit, HoloLens, **WebXR**.
- **Scene composition and gameplay logic** — GameObjects, components, prefabs, scripts (`Start`, `Update`, `FixedUpdate`), **input handling**, camera control, **state machines and interaction logic**.
- **PhysX physics**, **NavMesh AI**, **UI (uGUI/UI Toolkit)**, **3-D spatial audio**, **particle systems**, **Mecanim animation** (blending imported clips with IK and root motion).
- **Rendering** with **URP** (mobile/VR) and **HDRP**, real-time shadows, **GPU instancing**, occlusion culling, LOD, **foveated rendering**.
- **Debugging and profiling** — Console, **Unity Profiler**, Frame Debugger, Memory Profiler, development builds — essential to hold **≥ 90 fps** and low latency in VR.
- **Deployment** to PC, mobile, consoles, headsets and the web.

### 4. Unreal Engine — photoreal real-time alternative, and the leading **VR training/simulation** platform
- **Photorealistic rendering** — Lumen global illumination, nanite virtualised geometry, high-quality materials, volumetrics and cinematic post-processing. VR training, architecture/automotive visualisation and enterprise simulations often choose Unreal for visual quality.
- **Blueprints** — **visual, node-based scripting** that lets designers, engineers and **subject-matter experts (surgeons, technicians, soldiers) build interactive training logic without writing code**, which is crucial for VR training content.
- **Strong physics and Chaos solver**, **Niagara VFX**, **MetaHuman** characters (photoreal humans for training and simulation), **motion capture** pipelines.
- **Full VR template** and mature **OpenXR/SteamVR/Quest** support; also **UE4/UE5 VR Pawn** and packaged VR training projects.
- Used for **architectural visualisation**, **automotive design review**, and **military simulation**.

### How the tools work together (pipeline)

```
Concept → ZBrush (sculpt high detail, skin detail textures)
       → Blender/Maya (retopology for real-time, UV, rig, skin, animate, FBX export)
       → Unity / Unreal (import FBX, build scene, write C#/Blueprints for interaction,
                        physics, audio, XR support)
       → Platform SDKs (OpenXR, ARCore, ARKit, HoloLens, Vuforia, WebXR)
       → Build & test on device with profiling tools → Ship to headset
```

### Conclusion
*The four tools cover complementary parts of the pipeline: **ZBrush** produces **high-detail organic form and textures**; **Blender** turns those into **optimised, rigged, UV-mapped, animated assets** for free; **Unity** is the dominant **real-time engine** with the best-integrated XR support, C# scripting and profiling tools; and **Unreal Engine** delivers **photorealism** and **no-code Blueprint training logic** for simulation and visualisation. Together they let small teams build immersive experiences that are visually convincing, interactive and — crucially — fast enough to run at 90+ frames per second with latency below 20 ms on a headset.*

---
---

### 8(a) — Analyse the advantages and disadvantages of developing a shooting game using First Person Controller and Third Person Controller.
**M: 08 | CO: 4 | BT: 3**

*(OR of 7 — attempt either)*

**Answer:** *(This is the same question as Paper 1 Q6(b); full answer given there. Re-stated for this paper.)*

### First Person Controller — advantages
1. **Maximum immersion** — the player *is* the character, giving the intensity and realism a shooter needs.
2. **Direct, accurate aiming** — camera centre = aim point, so the **crosshair** maps exactly to the bullet path; aiming is fast, natural and twitchy.
3. **Better peripheral vision** — the screen shows nearly what the character sees, raising awareness of threats.
4. **Simple, fast controls** — WASD + mouse look; no separate character facing or following camera to manage.
5. **Cheaper to develop** — only hands/weapon need modelling and animating; no full character rig → saves modelling, rigging and animation cost.
6. **No camera-management problems** — no camera clipping, no camera through walls, no spring-arm code.
7. **Strong weapon feedback** — recoil, sway and reload are shown on the weapon in view.
8. **Suits VR naturally** — matches the head-tracked first-person view.

### First Person Controller — disadvantages
1. **Poor spatial awareness** — the player cannot see their own body, so position relative to objects is unclear and they may be unknowingly hidden behind cover.
2. **Harder navigation** — finding objectives and orienting is difficult; mini-map or compass needed.
3. **Little characterisation** — the face and body are invisible, so personality and emotion are hard to convey; cinematic framing is difficult.
4. **Higher motion sickness in VR** — no visible body to act as a reference frame.
5. **Harder to show group play** — teammates are rarely visible, so identification is awkward.
6. **No feedback on the character** — injuries, reload state and body cues are invisible.
7. **Unrealistic movement** — sprinting and rapid turning in first person can feel disorienting and break the brain's expectation of moving a body.

### Third Person Controller — advantages
1. **Better spatial awareness** — the character and surroundings are always visible, so positioning, cover and navigation are clear.
2. **Full character presentation** — full model, rig and animation set (idle, walk, run, aim, reload, death) for a richer experience.
3. **Flexible, cinematic camera** — can orbit, zoom, swing out in aim mode, and pull in on obstruction via a spring arm with collision.
4. **Natural movement** — the visible character moves like a real body.
5. **Cover mechanics work well** — the third-person camera makes cover systems (peek, lean, blind fire) visible and readable.
6. **Character identity and emotion** can be conveyed.
7. **Easier team identification** — you recognise other players.
8. **Lower sickness risk in VR** — the visible body provides a reference frame.

### Third Person Controller — disadvantages
1. **Less direct aiming** — the crosshair is at a **screen-space offset** from the character, so converting it to a world aim point adds complexity and can feel imprecise at long range.
2. **Camera collision problems** — the camera clips into walls, objects and the character's body, requiring raycasts, spring arms and fade-out logic.
3. **Reduced immersion** — you watch the character act rather than being the character; weaker emotional engagement.
4. **More expensive** — a full character, rig, skin and complete animation set must be produced.
5. **Reduced peripheral awareness** — the character occupies part of the view.
6. **Animation complexity** — blending aiming, strafing, firing and locomotion must be correct or the animation looks wrong.
7. **Less suited to VR realism** — many players find the "doll" view breaks immersion in an immersive medium.

### Conclusion
*Choose the **First Person Controller** for a competitive or realism-focused shooter: maximum immersion, direct crosshair aiming, better peripheral vision and lower cost — accepting poorer spatial awareness and higher sickness risk. Choose the **Third Person Controller** when spatial awareness, cinematic camera control, character identity, cover mechanics and readable team play matter more than raw immersion — accepting indirect aiming and higher asset complexity. Modern shooters often ship **both** via a switchable camera.*

---
---

### 8(b) — Illustrate the need for Sculpting and Rigging features in Blender.
**M: 08 | CO: 3 | BT: 2**

*(OR of 7 — attempt either)*

**Answer:** *(Full answer given at Paper 1, Q6(a); restated here.)*

### Sculpting — the need
**Sculpting** is Blender's free-form "digital clay" mode: the surface is pushed and pulled with brushes instead of moving individual vertices.
1. **Organic shapes are impossible with box modelling.** Characters, creatures, rocks, cloth folds and bark are irregular and cannot be built from straight edges, so vertex-by-vertex editing is unworkable; sculpting lets the artist add and remove material intuitively.
2. **Far faster for adding detail** — one brush stroke replaces hundreds of manual vertex edits, which is the main reason sculpting exists.
3. **It creates the high-resolution "master" form.** You sculpt at very high polygon density, then **retopologise** to build a clean low-poly game mesh over it — sculpting defines **shape**, retopology defines **topology**.
4. **Dynamic topology and voxel remeshing** let detail be added without worrying about edge loops.
5. **It layers with modifiers** — procedural **noise/displacement** on top of the sculpt gives skin pores, wrinkles and weathering.
6. **It bakes to normal/bump maps**, putting detail on a low-poly mesh — exactly what mobile and VR need for performance.
7. **Base meshes** (e.g. a head) can be sculpted over to create many different characters quickly.
8. **Unity has no sculpting at all** — dedicated applications **ZBrush** and **Mudbox** exist solely for this.

### Rigging — the need
**Rigging** builds a **skeleton (armature)** inside the mesh and **skins** it so the mesh deforms and moves.
1. **Animation requires a skeleton** — an elbow cannot stay rigid; bones provide the joints that bend.
2. **Weight painting / skinning distributes the deformation.** Each vertex is assigned **weights** to nearby bones; where weights blend, the mesh **bends smoothly** instead of creasing. Without it characters look like rigid robot plates.
3. **One rig, many animations** — the same rig can be posed and keyframed for walking, running, jumping, attacking and dying without remodelling (DRY applied to animation).
4. **Animation retargeting** — motion capture can drive the rig, transferring realistic human movement to a game character.
5. **Performance** — an animated character must be a **skinned mesh with a small bone hierarchy**, not millions of independent vertices; the GPU transforms the bones and the vertices follow. Rigging is what makes real-time character animation feasible.
6. **IK (Inverse Kinematics)** — the rig can place the hand on a target or keep the foot on the ground rather than rotating joints directly, which is essential for believable motion.
7. **Facial rigs and blendshapes** for lip-sync and expression.
8. **Unity only imports and plays back animations (FBX) and blends them with Mecanim/Animator** — rigs and skins must be created in Blender/Maya/3ds Max/ZBrush and exported.
9. **Correct light and physics interaction** — rigged characters collide correctly with the VR world, which makes them feel real.

### Conclusion
*Sculpting is needed because organic, high-detail shape cannot be produced by polygon editing, and it supplies the master form from which efficient real-time meshes are retopologised and normal maps baked. Rigging is needed because nothing can animate without a skeleton: it enables smooth skin deformation, allows one mesh to carry many animations, supports IK and motion capture, and makes real-time character animation possible. Every production character passes through both stages — sculpt and rig in Blender, export FBX, then animate and blend in Unity.*

---
---

### 9(a) — Explain the role of tracking in gaming applications of AR/VR
**M: 08 | CO: 4 | BT: 2**

**Answer:**

Tracking is the process of **continuously measuring the position, orientation and movement of the user (or real-world objects) so the computer can synchronise the virtual world with the user's real body movement**. In AR/VR gaming it is the foundation of the whole experience. Its roles:

1. **Defines the player's viewpoint.** Head tracking maps the user's real head pose to the **virtual camera**, so looking around feels natural and the world appears **anchored in space**. Without it the view is a fixed screen.
2. **Maps player movement to the avatar.** Hand, controller and full-body tracking drive the player's avatar, so the user **sees their own actions** — essential for **embodiment, ownership and presence**.
3. **Provides the input mechanism.** Controller, hand, gaze and voice tracking *is* the input: pointing, grabbing, throwing, shooting, menu selection, gesture commands. **No tracking means no interaction.**
4. **Enables locomotion (navigation).** Room-scale walking, **teleportation**, omnidirectional treadmill tracking and turning allow the player to move through a large game world **physically** rather than by joystick only.
5. **Supports collision and physics interaction.** Tracking the hands lets the player pick up and manipulate objects with correct spatial relationships, and contact with tracked geometry produces collision and haptic response.
6. **Determines aiming accuracy and fairness.** Aim precision, reaction time and end-to-end latency all depend on tracking quality; **latency > 20 ms** or jitter makes the game feel unresponsive and unfair.
7. **Provides spatial awareness of the real world in AR.** **SLAM/plane detection** lets the game place content on the user's real floor or table, detect surfaces, and **occlude virtual content behind real objects** correctly.
8. **Enables social and multiplayer presence.** Tracking each player's pose lets remote players see each other's avatars, gestures and gaze, which is the basis of **social presence** and communication in multiplayer and social VR.
9. **Supports gaze/eye-tracking features** — **foveated rendering** (huge GPU saving, longer battery life, needed for standalone headsets) and **gaze-based aiming and UI** without controllers.
10. **Enables new interaction genres** — room-scale, seated, standing and full-body AR/VR; hand-tracking games; motion-based minigames; motion capture for character animation.
11. **Calibration of the play area** — the user walks the boundary to define the room-scale volume, so the virtual world matches the real room's dimensions.
12. **Prevents sickness and protects safety** — accurate, low-latency tracking keeps the visual scene consistent with the vestibular system, avoiding the **sensory conflict** that causes **cybersickness**, and correct tracking prevents the user from walking into real objects.

**Conclusion:** *Tracking is what turns a VR/AR game from a screen into a world. It sets the viewpoint, drives the avatar, provides all input, enables physical navigation and object interaction, grounds AR content in the real world, enables social presence and gaze features, and — critically — determines whether the experience feels **responsive and comfortable** or **laggy and nauseating**. Its two hard requirements are **latency below 20 ms** and an **update rate of at least 90 Hz**.*

---
---

### 9(b) — Distinguish between Marker-Based and Marker-less Tracking.
**M: 08 | CO: 3 | BT: 3**

**Answer:** *(Full answer given at Paper 1, Q8(b); restated.)*

**Marker-based tracking** uses a **known, high-contrast, machine-readable pattern deliberately placed in the real world**, which the camera detects; the marker's pose is recovered and used as a reference frame to **register** virtual content precisely on it. **Marker-less tracking** uses **no artificial marker** — the camera detects **natural features** and applies **computer vision / SLAM** to build a map and compute the device's pose.

| Basis | **Marker-Based Tracking** | **Marker-Less Tracking** |
|---|---|---|
| **Reference object** | Man-made **printed or LED marker** (QR code, AprilTag, ArUco, IR sphere) | **Natural features** — texture, corners, lines; no artificial marker |
| **Method** | Pattern detection + decode ID + **homography / PnP** corner-based pose | **Feature detection & matching** (SIFT/SURF/ORB), **SLAM**, visual odometry, plane detection |
| **Accuracy** | **Very high, stable, deterministic** (sub-centimetre) | **Moderate, variable**; depends on scene texture and lighting; **drifts** |
| **Setup** | Must **print and place** markers | **Zero setup** — just point the device |
| **Hardware cost** | Very low (printed markers); moderate for IR/LED | Needs camera (ideally depth/ToF) + CPU/GPU for SLAM |
| **Lighting** | Needs consistent illumination; fails in dark/glare | Also light-sensitive but **adaptive** |
| **Range / area** | **Limited** to the camera's view and marker size | **Large open areas**, whole rooms |
| **Environment** | Must be set up with markers; unsuitable for unknown/moving places | Works in **any** real environment |
| **Scalability** | Each target needs its **own unique marker**; clutter | **Scales to many objects** with no extra markers |
| **Scene clutter** | Adds visible markers to the scene | Scene stays **completely unmodified** |
| **Compute load** | Low | **High** |
| **Applications** | Education, marketing/packaging, museums, maintenance instructions, printed-map navigation, medical body markers, calibration | Mobile AR (Pokémon GO, Layar), ARCore/ARKit apps, IKEA Place, virtual try-on, Google Maps AR, industrial AR assembly, HoloLens/Quest MR, face tracking |
| **Best for** | **Controlled, static, precise** setups | **Open, real-world, large-scale, mass-consumer** AR |

---
---

### 10(a) — Explain the concept of marker-based tracking in Augmented Reality.
**M: 08 | CO: 4 | BT: 2**

**Answer:** *(Full answer given at Paper 1, Q6/8 on marker-based tracking; restated with extra depth.)*

### Concept
**Marker-based tracking** (also called **fiducial-based** or **pattern-based tracking**) is a tracking technique in which a **known, high-contrast, machine-readable pattern — the *marker* — is deliberately placed in the real world**, and the AR device's **camera detects it**. The device **recognises the marker, estimates its 6-DOF position and orientation from its 2-D image**, and uses that as a **reference frame to register virtual content precisely on top of it**, correctly scaled and oriented.

*Definition:* *"Marker-based tracking in AR is a technique in which pre-defined visual markers (QR codes, AprilTags, ArUco, ARToolKit markers, IR reflective spheres) are placed in the real environment; the AR camera detects them, recovers their 6-DOF pose by corner detection with homography/PnP, and registers virtual objects onto them with sub-centimetre accuracy."*

### Types of markers
- **1-D barcode markers** (BluxAR) — fastest, simplest.
- **2-D square/rectangular markers** — **QR codes**, **ARToolKit markers**, the **AprilTag** family, **ArUco**, **SONY markers**; the ID encoded in the data matrix tells the app *which* content to show.
- **Non-square rectangular markers** — for elongated objects.
- **Infrared / LED active markers** — glowing spheres (PS Move), IR reflective spheres (OptiTrack), IR LED arrays (Vive trackers).
- **3-D / volumetric markers** — an object with known geometry whose pose is estimated by model matching (e.g. a hand-shaped marker).

### How it works, step by step
1. **A marker with a unique, known pattern** is printed or placed on a real surface (a table, box, wall, tool) or fixed to an object.
2. The AR device's **camera captures** the image; software **segments** the marker by **thresholding and contour/quadrilateral detection**, and **decodes its ID** for data-matrix markers.
3. The **four corners** of the marker are located; with the **known camera intrinsics**, a **homography** (2-D) and then **perspective-n-point (PnP)** solve give the marker's **position (x, y, z) and orientation (yaw, pitch, roll)** — its **6-DOF pose relative to the camera**.
4. The application's **virtual model is placed in that pose** (the model's local frame is anchored to the marker's frame) and rendered **on top of the marker**, correctly scaled and oriented.
5. As the **device or the marker moves**, the pose updates every frame, so the content stays **locked to the marker** — this is *registration*.
6. **Stability improvements:** occlusion handling, **shadow and lighting compensation**, and smoothing filters (One-Euro/Kalman) on the pose.

### Why it is used / advantages
1. **Very high accuracy and stability** — corner detection is a deterministic geometric problem, so the pose is accurate to sub-centimetre and reproducible.
2. **Cheap and trivial to deploy** — a marker is just a **printed piece of paper**; the total hardware cost is near zero.
3. **Fast and robust detection** — the distinctive black-and-white pattern is found quickly even with modest processing power, which suits **mobile devices**.
4. **Low computational cost** — no SLAM, no heavy feature matching, so it runs on low-end phones.
5. **Precise registration** — virtual content is **accurately aligned** with the real surface, so the AR looks convincing.
6. **Unambiguous identification** — the encoded ID means one marker can trigger different content, so **many products/targets can be supported**.
7. **Easy for non-programmers and for rapid prototyping** — museums, schools and small businesses can deploy AR quickly.
8. **Useful for calibration** — markers are used to calibrate the camera volume of optical tracking systems in research labs.

### Limitations
1. **Requires markers to be placed and maintained** — cannot work in unknown, moving or uncontrolled outdoor locations.
2. **Limited range and viewing angle** — the marker must be seen by the camera; best results are at short distances and near-normal angles.
3. **Susceptible to occlusion** — a partially covered marker loses the pose.
4. **Lighting sensitive** — poor, uneven or strong backlighting breaks detection.
5. **Accuracy limited by marker size** — small markers give poor positional precision.
6. **Clutter in large scenes** — many markers confuse detection; each target needs a unique ID.
7. **Durability** — markers get damaged, lost or defaced and need re-printing.
8. **Not scalable to large environments** — poor for covering big outdoor areas or many simultaneous targets.
9. **Visual pollution** — markers must remain visible, which is unacceptable in many real settings.

### Applications
- **Education** — scan a marker in a textbook/placard → 3-D molecule, historical monument, animated diagram; AR flashcards and interactive posters.
- **Marketing and packaging** — a marker on a brochure or product box launches a demo, video or 3-D model.
- **Museums and heritage** — markers near exhibits trigger 3-D reconstructions and audio guides.
- **Industrial maintenance** — a marker on a machine panel makes the overlay show the exact part and its repair steps.
- **Medical** — markers on the patient's skin for vein mapping (AccuVein), surgical registration and radiotherapy planning.
- **Navigation** — markers on printed maps or signposts give 3-D directions and highlighted POIs.
- **Architecture and construction** — markers on drawings or sites overlay the 3-D model on the real structure for progress checking.
- **Toys and games** — AR toys, card-based AR games and board games.
- **Research and calibration** — markers used to calibrate camera volumes and as test targets for AR systems.

### Conclusion
*Marker-based tracking is the simplest, cheapest and most accurate tracking technique: a known printed or illuminated pattern is detected by the camera, its 6-DOF pose is recovered geometrically by corner detection with homography/PnP, and virtual content is registered exactly onto it. Because it is deterministic and computationally light it is ideal for **controlled, static, precision setups** — education, exhibits, marketing packaging, maintenance instructions and medical registration — but its dependence on placed markers, limited range and occlusion and lighting sensitivity prevent its use in open, large-scale and mass-consumer AR, where marker-less SLAM is used instead.*

---
---

### 10(b) — Describe the technologies and sensors used for tracking in AR/VR
**M: 08 | CO: 3 | BT: 2**

**Answer:**

### A. Tracking technologies (by principle)

| # | Technology | Principle | Advantages | Disadvantages |
|---|---|---|---|---|
| **1** | **Optical tracking** | **IR cameras** detect IR LEDs (active) or reflective markers (passive); **triangulation** from ≥ 2 cameras gives 3-D position; multiple markers give orientation by rigid-body fit | Very accurate (mm level), low latency, high update rate, full 6-DOF, **immune to EM interference**, **no drift**, works in darkness, scalable | **Occlusion**, needs **line of sight** and a fixed camera rig, **not portable**, costly, needs **calibration**, light-sensitive |
| **2** | **Magnetic / electromagnetic tracking** | An **EM base station** emits a magnetic field; a sensor measures field strength/orientation | Works **through occluders**, cheap, small, easy to make, no camera calibration | **Distorted by metal objects and electronics**, limited range (~few m), lower absolute accuracy, latency, needs re-calibration |
| **3** | **Inertial tracking (IMU)** | **Gyroscopes** (angular velocity) + **accelerometers** (linear acceleration) + **magnetometer** (absolute heading), combined by **sensor fusion**; **dead reckoning** | Very **low latency**, very high update rate (100–1000 Hz), cheap, tiny, works anywhere, no occlusion | **Drift** over time, no absolute position, must be corrected by another reference |
| **4** | **Hybrid tracking** | **Optical + inertial** fused (HTC Vive Lighthouse, Quest inside-out + IMU, Kinect + IMU) | Optical accuracy + inertial smoothness and high rate, drift corrected, robust | More complex and costly, needs calibration |
| **5** | **Acoustic / ultrasonic tracking** | Ultrasonic pulses and receivers; echo time gives distance | Cheap, simple, low power | Very short range, noisy, multipath reflections, low accuracy |
| **6** | **GPS / GNSS tracking** | Satellite **trilateration** gives global position | Works **outdoors at large scale**, no extra infrastructure | Does not work indoors or in urban canyons, a few metres accuracy, not suitable for head-level precision |
| **7** | **Vision-based / marker-less tracking (SLAM)** | Camera detects natural features; **feature detection/matching, optical flow, SLAM** build a map and compute the pose | **No markers needed**, works in any environment, mobile, low hardware cost | Needs texture and light, **drift**, computationally heavy, fails on blank walls |
| **8** | **Marker-based tracking** | Known **fiducial** (QR, AprilTag, IR sphere) detected by camera; **homography/PnP** | Very accurate, deterministic, cheap, robust | Needs markers placed, limited range/angle, occlusion and lighting sensitive |
| **9** | **Eye tracking** | **IR cameras** measure pupil/corneal reflection | **Foveated rendering**, gaze interaction, real presence | Cost, calibration, needs eye cameras |
| **10** | **Hand & full-body tracking** | Depth cameras / optical + **ML** infer joint positions | Natural interaction, no controllers | Finger occlusion, latency, cost |
| **11** | **Outside-in / inside-out architectures** | Outside-in: cameras in the room track the headset. Inside-out: cameras on the headset track the environment | Outside-in very accurate with a fixed volume; inside-out portable, no setup, quick | Outside-in needs space and calibration; inside-out needs feature-rich rooms and SLAM |

### B. Sensors used

| Sensor | Measures | Principle | Typical use |
|---|---|---|---|
| **Gyroscope** | **Angular velocity** (rotation rate) about 3 axes | **Coriolis effect** in a spinning mass | Head orientation, fast rotation (part of every IMU) |
| **Accelerometer** | **Linear acceleration**, including gravity | Change in capacitance / piezoresistive mass | Tilt, gravity direction, motion (part of every IMU) |
| **Magnetometer** | **Magnetic field** → absolute heading (yaw) | **Hall effect** | Corrects gyroscope yaw **drift** |
| **IMU** | Gyro + accel + magnetometer | **Sensor fusion / Kalman filter** | Core of every 6-DOF HMD |
| **IR camera** | Image of IR LEDs or reflective markers | IR reflection and triangulation | High-accuracy optical tracking (Lighthouse, OptiTrack) |
| **Depth camera** (Kinect, RealSense, Azure Kinect, ARKit) | 3-D point cloud / depth | **Time-of-Flight (ToF)** or **structured light** | Inside-out tracking, hand tracking, **plane detection**, room mapping, occlusion |
| **Structured-light projector** | Depth | Projected IR dot pattern + camera | Room-scale mapping, surface reconstruction |
| **LIDAR** | Distance / point cloud | Time-of-flight of laser | Precise outdoor mapping, autonomous vehicles |
| **Ultrasonic sensor** | Distance | Echo reflection time | Simple short-range tracking |
| **GPS / GNSS receiver** | Global position | Satellite trilateration | **Location-based outdoor AR**, geofencing |
| **Barometer** | Altitude | Air pressure | Indoor floor/height estimation |
| **Compass / magnetometer** | Heading | Magnetic field | Outdoor AR heading |
| **Video camera (RGB)** | Image stream | Optical flow, feature detection | Marker-less tracking, **SLAM**, face tracking, object recognition |
| **Eye tracker** | Gaze point | IR reflection off the cornea | Foveated rendering, gaze interaction |
| **Radar / mmWave** | Distance and velocity | Radio waves | Gesture recognition, vital-sign monitoring |
| **Proximity sensor** | Object nearness | IR/EM field | Detecting the HMD is being worn (session start/end) |
| **Haptic actuators** | Force/vibration | Vibration motors, force-feedback, ultrasound | Touch feedback to accompany tracking |
| **Networking** | Data | Wi-Fi/5G | Multi-user position sharing, cloud anchors |

### C. How the technologies combine in practice

- **Every 6-DOF HMD** contains an **IMU** (gyroscope + accelerometer + magnetometer) for smooth, high-rate orientation.
- **Absolute position** is supplied by **optical tracking** (outside-in, e.g. Vive Lighthouse; or inside-out via **depth/RGB cameras and SLAM**, e.g. Quest, HoloLens) or by **magnetic tracking**.
- A **Kalman/complementary filter** fuses them: the IMU gives smooth high-frequency motion, the absolute source prevents drift.
- **Mobile AR** fuses **camera (CV/SLAM) + IMU + GPS + compass** with an **Extended Kalman Filter**.
- **Controllers** combine IMU + optical or magnetic tracking for 6-DOF hand input; **hand tracking** replaces them with depth/RGB cameras and ML.
- **Eye tracking** adds gaze data for foveated rendering and gaze interaction.
- Requirements for all of them: **latency below 20 ms**, **update rate ≥ 90 Hz**, **millimetre accuracy**, **no drift**, and **graceful failure** when tracking is lost.

### Conclusion
*Tracking in AR/VR is built from four technology families — **optical, magnetic, inertial and acoustic/GPS** — combined with **vision-based (SLAM)** techniques, in either **outside-in** or **inside-out** architectures. These are realised physically by **gyroscopes, accelerometers and magnetometers (IMU), IR and RGB/depth cameras, structured-light projectors, LIDAR, ultrasonic and GPS receivers, barometers and eye trackers**, plus haptic actuators and networking. No single sensor is sufficient: **inertial sensors are smooth but drift, optical and GPS sensors are absolute but low-rate or occlusion-prone, magnetic sensors work through occluders but are metal-sensitive** — so an effective system always uses **sensor fusion** to combine accuracy, low latency and no drift within a **< 20 ms latency and ≥ 90 Hz update rate** budget.*

---
---

# 🧠 QUIZ-QUESTION BANK

> **Section A — MCQs (1 mark each).** One line of reasoning/answer after each.

## Unit I — VR, AR, MR, Applications

**Q1.** VR stands for —
(a) Virtual Rendering (b) **Virtual Reality** (c) Vivid Reality (d) Virtual Reconnaissance
**Ans:** (b) Virtual Reality — a fully computer-generated artificial environment the user enters.

**Q2.** The formal definition "inducing targeted behaviour in an organism using artificial sensory stimulation with little or no awareness of the interference" describes —
(a) AR (b) **VR** (c) MR (d) XR
**Ans:** (b) VR.

**Q3.** Which is NOT one of the four key elements of VR in the course notes?
(a) Targeted behaviour (b) Artificial sensory stimulation (c) **Registration** (d) Unawareness
**Ans:** (c) Registration is an AR concept; the four are targeted behaviour, artificial sensory stimulation, unawareness and organism.

**Q4.** The Three I's of VR are —
(a) Immersion, Interaction, Imagination (b) **Input, Imaging, Interaction** (c) Immersion, Illusion, Indexing (d) Information, Interaction, Intention
**Ans:** (a) Immersion, Interaction, Imagination (I³).

**Q5.** The five classic components of a VR system are —
(a) Engine, Display, Sensor, User, Task (b) **VR Engine, I/O Devices, Software & Databases, User, Task** (c) GPU, CPU, RAM, Screen, Cable (d) Scene, Camera, Light, Mesh, Script
**Ans:** (b).

**Q6.** Azuma's three requirements for an AR system include registration in —
(a) one dimension (b) **three dimensions** (c) 2D only (d) colour space
**Ans:** (b) three dimensions — an AR system must combine real and virtual, be interactive in real time, and be registered in 3-D.

**Q7.** The most popular and basic AR device type is —
(a) HUD (b) holographic display (c) smart glasses (d) **handheld (phone/tablet)**
**Ans:** (d) handheld.

**Q8.** The virtuality continuum was proposed by —
(a) Azuma (b) **Milgram & Kishino (1994)** (c) Sutherland (d) Heilig
**Ans:** (b) Milgram & Kishino, 1994.

**Q9.** "Augmented Virtuality" is best described as —
(a) real world with digital overlays (b) **mostly virtual content with a small amount of real input** (c) fully computer-generated (d) physical reality only
**Ans:** (b) — e.g. projecting a virtual forest onto real trees.

**Q10.** Which term is an **umbrella** for all realities (VR, AR, MR)?
(a) SIM (b) **XR** (c) UAV (d) HMD
**Ans:** (b) XR.

**Q11.** "Sword of Damocles" was built by —
(a) Morton Heilig (b) **Ivan Sutherland (1968)** (c) Jaron Lanier (d) Tom Gruber
**Ans:** (b) Ivan Sutherland with student Bob Sproull, 1968 — so heavy it hung from the ceiling.

**Q12.** The **EyePhone** was a commercial HMD from —
(a) Microsoft (b) **VPL Research (1989)** (c) Sony (d) Meta
**Ans:** (b) VPL Research, 1989.

**Q13.** Sensorama was created by —
(a) Ivan Sutherland (b) **Morton Heilig** (c) Philco (d) Myron Krueger
**Ans:** (b) Morton Heilig, early 1960s — mechanical, predates digital computing.

**Q14.** **Virtual Fixtures**, the earliest functional AR system, was developed at —
(a) MIT (b) **U.S. Air Force Armstrong Laboratory (1992)** (c) UNC (d) Cambridge
**Ans:** (b).

**Q15.** The term "Augmented Reality" was coined by **Tom Gruber** at —
(a) NASA (b) **Boeing (1993)** (c) Microsoft (d) Sony
**Ans:** (b) Boeing, 1993.

**Q16.** ARToolKit, the first **open-source** AR platform, was released in —
(a) 1989 (b) 1992 (c) **1999** (d) 2005
**Ans:** (c) 1999.

**Q17.** The **Aspen Movie Map** (1977) was created at —
(a) MIT (b) **USC** (c) UNC (d) NASA
**Ans:** **(b) USC (University of Southern California)** — a crass, noisy, wire-frame fly-through of Aspen, Colorado. *(Note: the course notes state MIT; the historically documented origin is USC. Check your notes before writing.)*

**Q18.** The **CAVE** (Cave Automatic Virtual Environment) was invented at —
(a) MIT (b) **UNC Chapel Hill** (c) University of Illinois (d) VPL
**Ans:** (b) UNC Chapel Hill. *(University of Illinois = BOOM by Fakespace.)*

**Q19.** BOOM stands for —
(a) Binocular Omni-Oriented Monitor (b) **Binocular Omni-Orientation Monitor** (c) Basic Optical Output Module (d) Broadband Omni-Optical Mechanism
**Ans:** (b).

**Q20.** Four projectors are thrown onto **three walls and the floor** in a —
(a) BOOM (b) HMD (c) **CAVE** (d) data glove
**Ans:** (c) CAVE.

**Q21.** A **360° VR video** is an example of —
(a) closed-loop interactivity (b) **open-loop** (c) full immersion (d) shared VE
**Ans:** (b) open-loop — sensory output with no response to user actions.

**Q22.** In the VPL **DataGlove**, a fibre-optic cable with an **LED at one end and a phototransistor at the other** measures —
(a) hand weight (b) **finger bend angle** (c) hand position (d) grip force
**Ans:** (b) finger bend angle — bending loses light, so less light is received.

**Q23.** The sensor on the back of the hand in the VPL DataGlove that measures position and orientation is the —
(a) Polhemus (b) Kinect (c) LiDAR (d) ultrasonic array
**Ans:** (a) Polhemus sensor.

**Q24.** The Logitech **3-D mouse** uses an array of **three ultrasonic speakers** to give output in —
(a) 3 DOF (b) **6 DOF (X, Y, Z, pitch, yaw, roll)** (c) 2 DOF (d) 9 DOF
**Ans:** (b).

**Q25.** The **SpaceBall** is a 6-DOF device that is —
(a) **force-sensitive** and produces homogeneous transformation matrices (b) optically tracked (c) a haptic exoskeleton (d) an eye tracker
**Ans:** (a).

**Q26.** Six degrees of freedom mean —
(a) x, y, z only (b) **x, y, z + yaw, pitch, roll** (c) x, y, z, roll, pitch (d) six linear axes
**Ans:** (b) — 3 position + 3 orientation.

**Q27.** The minimum acceptable DOF for immersive VR is —
(a) 2 (b) 3 (c) **6** (d) 9
**Ans:** (c) 6.

**Q28.** In the UNIT1 axis diagram, the **Y axis corresponds to** —
(a) yaw (b) **pitch** (c) roll (d) translation
**Ans:** (b) pitch.

**Q29.** **Drift** in a tracker means —
(a) random rapid noise when stationary (b) **steady increase in tracking error with time** (c) total sensor failure (d) screen flicker
**Ans:** (b) steady increase in tracking error with time. *(Jitter = random rapid fluctuations when stationary; accuracy = difference from actual position; latency = time delay.)*

**Q30.** The first tracker was a —
(a) optical camera rig (b) **mechanical arm supporting a CRT-based HMD** (c) magnetic base station (d) ultrasonic array
**Ans:** (b).

**Q31.** Mechanical trackers have very low jitter and the **lowest latency**, but their drawbacks are —
(a) low accuracy (b) **limited range and reduced freedom of motion** (c) high cost (d) drift
**Ans:** (b).

**Q32.** Relative sensors return —
(a) absolute coordinates (b) **zeros if not acted upon** (c) 6 DOF always (d) no data
**Ans:** (b).

**Q33.** Sensor-based tracking techniques include optical, magnetic, acoustic and —
(a) radar (b) **inertial** (c) thermal (d) optical-circular
**Ans:** (b) inertial.

**Q34.** Vision-based tracking uses —
(a) EM field (b) **the camera and computer vision** (c) ultrasonics (d) GPS
**Ans:** (b).

**Q35.** Optical tracking is **immune** to which problem?
(a) occlusion (b) **magnetic/electromagnetic interference** (c) drift (d) cost
**Ans:** (b) — it works beside metal objects, motors and speakers.

**Q36.** The big disadvantage of optical tracking is —
(a) cost of magnetic field (b) **occlusion and the need for line of sight and a fixed rig** (c) drift (d) low update rate
**Ans:** (b).

**Q37.** To compute 3-D position by triangulation, a marker must be seen by —
(a) one camera (b) **at least two cameras at a known baseline** (c) three markers only (d) an IMU
**Ans:** (b).

**Q38.** VR sickness is primarily caused by —
(a) low resolution (b) **sensory conflict between vision and the vestibular system** (c) loud audio (d) small FOV
**Ans:** (b).

**Q39.** The **vestibular system** is located in the —
(a) eyes (b) **inner ear** (c) cerebellum cortex (d) cochlea only
**Ans:** (b) inner ear.

**Q40.** Latency for VR tracking should be —
(a) under 200 ms (b) **under 20 ms** (c) under 5 ms (d) under 1 second
**Ans:** (b) under 20 ms.

**Q41.** Human horizontal field of view is approximately —
(a) 100° (b) 120° (c) **200°** (d) 360°
**Ans:** (c) ~200°.

**Q42.** The five sensory systems listed in the course notes are visual, auditory, vestibular, proprioceptive and —
(a) olfactory (b) **tactile** (c) gustatory (d) thermal
**Ans:** (b) tactile.

**Q43.** **Stereoscopic** display works by presenting —
(a) the same image to both eyes (b) **slightly different images to each eye** (c) a single flat image (d) sequential sound
**Ans:** (b) — exploiting binocular vision to give depth.

**Q44.** In VR software, the categories of software are —
(a) hardware, firmware, drivers (b) **system, application, development** (c) VR, AR, MR (d) open, closed, hybrid
**Ans:** (b).

**Q45.** **Vuforia Engine** is a platform for —
(a) VR games (b) **creating AR apps with computer vision** (c) 3-D modelling (d) audio
**Ans:** (b).

**Q46.** **MRTK** stands for —
(a) Mixed Reality Tool Kit (b) **Mixed Reality Toolkit** (c) Mobile Rendering Toolkit (d) Multi-Runtime Tool Kit
**Ans:** (b).

**Q47.** Which is NOT a core technical pillar of MR?
(a) Environmental perception (b) Advanced input systems (c) **Blockchain storage** (d) Cloud & edge computing
**Ans:** (c).

**Q48.** **Visual-inertial odometry (VIO)** combines —
(a) GPS and compass (b) **camera data + inertial readings** (c) audio and touch (d) magnetometer and barometer
**Ans:** (b).

**Q49.** **Time-of-flight (ToF)** sensors measure distance by —
(a) sound echo (b) **emitting and detecting reflected light** (c) magnetic field (d) satellite signal
**Ans:** (b).

**Q50.** **Foveated rendering** renders —
(a) the centre of view in low resolution (b) **the fovea in high resolution and the periphery in lower detail** (c) only one eye (d) no audio
**Ans:** (b).

**Q51.** Ultraleap provides —
(a) full-body capture (b) **infrared hand tracking and mid-air haptics** (c) GPS tracking (d) eye tracking only
**Ans:** (b). *(Kinect = full-body movement capture.)*

**Q52.** The **digital twin** application of MR involves —
(a) copying a game (b) **a real-time 3-D replica of a physical system with live IoT sensor data overlaid** (c) 2-D scanning (d) video compression
**Ans:** (b).

**Q53.** Telemedicine/telerobotics is an example of —
(a) synthetic VR (b) **telepresence** (c) augmented virtuality (d) HUD
**Ans:** (b) telepresence.

**Q54.** In shared virtual environments, each user is represented by a(n) —
(a) cursor (b) **avatar** (c) icon (d) texture
**Ans:** (b) avatar.

**Q55.** **VRML 2.0** corresponds to which international standard?
(a) ISO/IEC 14496 (b) **VRML97** (c) IEEE 1394 (d) ITU-T H.264
**Ans:** (b) VRML97.

**Q56.** HTML gives "home pages"; VRML gives —
(a) home spaces (b) home movies (c) home screens (d) home movies
**Ans:** (a) home spaces.

**Q57.** Full immersion vs partial immersion — Second Life and the Nintendo **Wii remote** are examples of —
(a) full immersion (b) **partial immersion** (c) no VR (d) MR
**Ans:** (b) partial immersion — full immersion is not compulsory.

**Q58.** In the **MRTK** workflow, **in-editor simulation** means —
(a) deploying to a device each time (b) **prototyping in the editor without deploying to a device** (c) offline rendering (d) cloud rendering
**Ans:** (b).

## Unit II — Unity, Code, Debugging, Objects, Scripts

**Q59.** The four main Unity editor windows are —
(a) Project, Scene, Hierarchy, Inspector (b) **Project, Scene, Hierarchy, Inspector** (c) Assets, View, Tools, Help (d) Console, Assets, Layout, Menu
**Ans:** (b). *(Console is also important but the "four main" are Project, Scene, Hierarchy, Inspector.)*

**Q60.** The default scene asset name in a new Unity project is —
(a) Level1 (b) **SampleScene** (c) Main (d) Untitled
**Ans:** (b) SampleScene.

**Q61.** A **GameObject** is —
(a) a script (b) **any object in a scene — a named point in space** (c) only a mesh (d) only a camera
**Ans:** (b).

**Q62.** Every GameObject always has a —
(a) Collider (b) **Transform** (c) Rigidbody (d) Mesh Renderer
**Ans:** (b) Transform (position, rotation, scale) — it cannot be removed without deleting the GameObject.

**Q63.** A **Component** —
(a) is a separate scene (b) **is a single piece of functionality attached to a GameObject and cannot exist without one** (c) must be a script (d) is stored in the Project window
**Ans:** (b).

**Q64.** Which component **renders the scene to the player's screen** during gameplay?
(a) Light (b) Audio Source (c) **Camera** (d) Collider
**Ans:** (c) Camera.

**Q65.** A **Cube** created in Unity additionally has which two components besides Transform?
(a) Collider, Rigidbody (b) **Mesh Filter and Mesh Renderer** (c) Light, Camera (d) Animator, Audio
**Ans:** (b) Mesh Filter holds the 3-D model; Mesh Renderer displays it.

**Q66.** Unity's six transform tools in key order are —
(a) Q W E R T Y (b) **Q W E R T Y** (c) W Q E R T Y (d) Q E W R T Y
**Ans:** (b) Q = Hand, W = Move, E = Rotate, R = Scale, T = Rect, Y = Transform.

**Q67.** The **Rect (T)** tool is mainly used for —
(a) rotating 3-D (b) **2-D work, adjusting position and scale together with corner handles** (c) flying the camera (d) scaling uniformly
**Ans:** (b).

**Q68.** The **Transform (Y)** tool combines —
(a) Hand and Move (b) **Move, Rotate and Scale together** (c) Rect and Scale (d) Move and Rect
**Ans:** (b).

**Q69.** In Unity, the axes are coloured —
(a) X blue, Y red, Z green (b) **X red, Y green, Z blue** (c) X green, Y blue, Z red (d) all white
**Ans:** (b).

**Q70.** The **world origin** in Unity is —
(a) (1, 1, 1) (b) **(0, 0, 0)** (c) (0.5, 0.5, 0.5) (d) (10, 10, 10)
**Ans:** (b) (0, 0, 0) — the centre of the scene.

**Q71.** Which direction is **positive Z** in Unity?
(a) backward (b) **forward** (c) up (d) down
**Ans:** (b) forward.

**Q72.** Which direction is **positive Y** in Unity?
(a) down (b) **up** (c) forward (d) left
**Ans:** (b) up.

**Q73.** A **Plane** primitive in Unity is —
(a) a thick slab (b) **a thin flat surface with no thickness, one-sided (invisible from below)** (c) a sphere (d) a 3-D cube
**Ans:** (b).

**Q74.** **Scale ≠ size** because —
(a) scale is absolute (b) **scale is a multiplier and every mesh has its own dimensions, so a Plane looks larger than a Cube at the same scale** (c) scale cannot be changed (d) scale affects only sprites
**Ans:** (b).

**Q75.** Unity **does not enforce** real-world units — a unit is —
(a) always 1 metre (b) **a design decision, so consistency matters more than the unit itself** (c) fixed by Unity (d) always 1 foot
**Ans:** (b).

**Q76.** Every C# statement must end with —
(a) `:` (b) **`;`** (c) `{}` (d) `//`
**Ans:** (b) a semicolon.

**Q77.** A **code block** is a group of statements written inside —
(a) `[ ]` (b) **`{ }`** (c) `( )` (d) `< >`
**Ans:** (b) curly braces.

**Q78.** A **single-line** comment in C# is —
(a) `/* */` (b) `//` (c) `#` (d) `--`
**Ans:** (b) `//`; multi-line uses `/* */`.

**Q79.** Which type stores decimal numbers such as 3.5?
(a) int (b) **float** (c) bool (d) string
**Ans:** (b) float.

**Q80.** A method that returns a value must use the keyword —
(a) print (b) **return** (c) yield (d) out
**Ans:** (b) return.

**Q81.** The correct method structure is —
(a) `Name { returnType (params) }` (b) **`returnType Name(parameters) { statements; }`** (c) `void Name { }` (d) `Name: returnType;`
**Ans:** (b).

**Q82.** A **method does nothing until it is —**
(a) compiled (b) **called** (c) saved (d) attached
**Ans:** (b) called — using its name followed by parentheses.

**Q83.** Which is NOT a common beginner mistake in C#?
(a) forgetting the semicolon (b) missing curly braces (c) **declaring a public variable** (d) not calling methods
**Ans:** (c).

**Q84.** AR/VR logic typically runs at —
(a) 10–20 fps (b) **60–120 fps** (c) 5 fps (d) 500 fps
**Ans:** (b) 60–120 frames per second.

**Q85.** An **infinite loop** in VR code will —
(a) slowly degrade graphics (b) **freeze the application and force the user to restart the headset** (c) be ignored (d) increase immersion
**Ans:** (b).

**Q86.** The most common conditional bug is using **=** instead of —
(a) `!=` (b) **`==`** (c) `>=` (d) `<=`
**Ans:** (b) `==`.

**Q87.** Which logical operators are `&&`, `||` and `!`?
(a) arithmetic (b) **logical (AND, OR, NOT)** (c) relational (d) assignment
**Ans:** (b).

**Q88.** Which loop runs **at least once** before the condition is checked?
(a) for (b) while (c) **do–while** (d) foreach
**Ans:** (c) do–while.

**Q89.** Which loop is used in Unity for **continuous per-frame updates**?
(a) for (b) while (c) do–while (d) **Update()**
**Ans:** (d) Update().

**Q90.** Excessive loops in AR/VR cause —
(a) better graphics (b) **lag and frame drops, and can lead to motion sickness** (c) lower cost (d) no effect
**Ans:** (b).

**Q91.** A **class** in C# is —
(a) a running program (b) **a blueprint/template defining structure and behaviour** (c) an instance (d) a component
**Ans:** (b).

**Q92.** An **instance** of a class is —
(a) the blueprint (b) **an individual copy holding its own data** (c) a static member (d) a namespace
**Ans:** (b).

**Q93.** An **instance variable** is —
(a) declared inside a method and destroyed when it returns (b) **declared in the class block and lives as long as the object** (c) always static (d) private
**Ans:** (b).

**Q94.** The **default access modifier** if none is written is —
(a) public (b) protected (c) **private** (d) internal
**Ans:** (c) private.

**Q95.** A variable declared `public` — where does it appear?
(a) nowhere (b) **in the Inspector** (c) in the Project window only (d) in the Console
**Ans:** (b) in the Inspector, editable per GameObject.

**Q96.** Inside a method, `if (canBeSold)` does not need `== true` because —
(a) it is a string (b) **it is already a bool** (c) Unity requires it (d) it is an int
**Ans:** (b).

**Q97.** A **constructor** is —
(a) a static method (b) **a special method that initialises an object when it is created with `new`** (c) an instance variable (d) a component
**Ans:** (b).

**Q98.** `this` refers to —
(a) the class (b) **the current instance** (c) the parent class (d) the GameObject
**Ans:** (b) the current instance — it separates a field from a same-named parameter.

**Q99.** If a class declares a **custom constructor**, the **default parameterless constructor** —
(a) is still generated (b) **disappears, so `new Item()` gives a compile error** (c) becomes static (d) becomes private
**Ans:** (b).

**Q100.** A **static** variable is —
(a) separate on each instance (b) **shared by the entire class and accessed through the class name** (c) private only (d) always a string
**Ans:** (b).

**Q101.** `Debug.Log()` is itself —
(a) an instance method (b) **a static method of the `Debug` class** (c) a component (d) an attribute
**Ans:** (b).

**Q102.** `MonoBehaviour` is —
(a) a namespace (b) **the Unity base class that makes a script a component attachable to a GameObject** (c) a variable type (d) a renderer
**Ans:** (b).

**Q103.** A **script class** (vs a regular class) is instantiated —
(a) with `new` in code (b) **by attaching it to a GameObject in the Editor** (c) by the compiler (d) by the Package Manager
**Ans:** (b).

**Q104.** **Critical rule:** the script **file name must** —
(a) match the scene name (b) **exactly match the class name (case-sensitive, PascalCase, .cs)** (c) be all lowercase (d) end with "_Script"
**Ans:** (b) — otherwise the script cannot be attached to a GameObject.

**Q105.** Inheriting `MonoBehaviour` gives free access to —
(a) `Camera` and `Light` (b) **`transform` and `gameObject`** (c) `Renderer` only (d) `Physics`
**Ans:** (b) — both lowercase and never null.

**Q106.** `[Header("Movement")]` adds —
(a) a comment (b) **a bold group title in the Inspector** (c) a class (d) a namespace
**Ans:** (b).

**Q107.** `[HideInInspector]` hides a **public** variable —
(a) from other scripts (b) **from the Inspector only — it stays public in code** (c) completely (d) from the compiler
**Ans:** (b).

**Q108.** `Vector3` holds —
(a) two values (b) **three values X, Y, Z representing a position or direction** (c) a rotation only (d) a colour
**Ans:** (b).

**Q109.** We multiply movement by `Time.deltaTime` to make motion —
(a) faster (b) **frame-rate independent** (c) jittery (d) networked
**Ans:** (b).

**Q110.** `public Vector3 rotationPerSecond = new Vector3(0, 45, 0);` rotates —
(a) 45° total (b) **45° per second about the Y axis** (c) 45° about X (d) 45° per frame
**Ans:** (b).

**Q111.** The correct Unity property for changing an object's **size** is —
(a) `transform.scale` (b) **`transform.localScale`** (c) `transform.size` (d) `transform.magnitude`
**Ans:** (b) `transform.localScale`. *(The course slide shows `transform.scale`, which is an error — see the errors section.)*

**Q112.** DRY stands for —
(a) Do Really Young (b) **Don't Repeat Yourself** (c) Debug Runtime Your Yield (d) Data Reference Yes
**Ans:** (b) — write the code once and call it many times.

**Q113.** Which statement about `public`/`private` is correct?
(a) private variables show in the Inspector (b) **private is the default and supports encapsulation** (c) public cannot be accessed outside (d) both show in the Inspector
**Ans:** (b).

**Q114.** A **namespace** in C# is —
(a) a folder on disk (b) **a container that organises code; a `using` statement gives direct access without the full path** (c) a class (d) a variable
**Ans:** (b).

**Q115.** To see the value of a variable at runtime you use —
(a) `Print()` (b) **`Debug.Log()`** (c) `Console.Write()` (d) `WriteLine()`
**Ans:** (b) `Debug.Log()` prints to the Unity Console window.

**Q116.** String concatenation in C# uses —
(a) `&` (b) **`+`** (c) `|` (d) `,`
**Ans:** (b).

**Q117.** In Unity, which method is called **once at startup**, before the first `Update()`?
(a) Update (b) FixedUpdate (c) **Start** (d) OnDestroy
**Ans:** (c) Start (Awake is also called once but before Start).

**Q118.** Which Unity method runs at a **fixed physics rate** (default 50/sec)?
(a) Update (b) **FixedUpdate** (c) LateUpdate (d) OnGUI
**Ans:** (b) FixedUpdate.

**Q119.** `LateUpdate()` is best used for —
(a) input (b) **camera follow, after all Updates** (c) physics forces (d) initialisation
**Ans:** (b).

**Q120.** The correct execution order is —
(a) Start → Update → Awake (b) **Awake → OnEnable → Start → FixedUpdate/Update → LateUpdate** (c) Update → Start → Awake (d) Awake → Update → LateUpdate → Start
**Ans:** (b).

**Q121.** An **asset** in Unity is —
(a) a scene only (b) **any piece of work used in a game — art, sound, code, levels** (c) a component (d) a collider
**Ans:** (b).

**Q122.** `GameObject → 3D Object →` creates which primitive?
(a) a light (b) **a Cube** (c) an empty (d) a prefab
**Ans:** (b) a Cube.

**Q123.** To attach a script to a GameObject you can —
(a) only use code (b) **drag the .cs from the Project window onto the GameObject, or use Add Component in the Inspector** (c) only use the console (d) only use the Hierarchy menu
**Ans:** (b).

**Q124.** `Instantiate(prefab)` in code is equivalent to —
(a) deleting the prefab (b) **dragging the prefab from Assets into the Hierarchy** (c) renaming (d) saving the scene
**Ans:** (b) — both create a prefab instance.

**Q125.** `OnTriggerEnter` fires when —
(a) a solid collision happens (b) **a trigger collider region is entered** (c) the scene loads (d) a frame is drawn
**Ans:** (b).

**Q126.** Unity's built-in physics engine is —
(a) Havok (b) **PhysX** (c) Bullet (d) Ode
**Ans:** (b) PhysX.

**Q127.** Which collider is **default for characters** in Unity?
(a) Box (b) Sphere (c) **Capsule** (d) Mesh
**Ans:** (c) Capsule — it accurately models a standing person.

**Q128.** A **Mesh Collider** on a **moving** object must be —
(a) concave (b) **convex** (c) hidden (d) disabled
**Ans:** (b) convex — fast collision algorithms require convex geometry.

**Q129.** Setting a collider's **Is Trigger** option means —
(a) it is destroyed (b) **it only detects entry/exit and produces no physical response** (c) it becomes invisible (d) it is static
**Ans:** (b).

**Q130.** To move a Rigidbody by script you would use —
(a) `transform.Translate` only (b) **`AddForce`, `AddTorque`, `velocity` or `MovePosition`** (c) `Debug.Log` (d) `Instantiate`
**Ans:** (b).

---

## Section B — Short-answer / 2-mark quiz questions (with model answers)

**Q1.** Differentiate between **open-loop and closed-loop** interactivity in VR, with one example of each.
**Ans:** *In **open-loop**, sensory output is produced **without responding to user actions** — e.g. watching a **360° VR video** (System → User). In **closed-loop**, the output is **continuously updated based on user actions (feedback)** — e.g. **VR games, flight or driving simulators and training simulations** (System → User → Feedback → System). Closed-loop systems are interactive; open-loop systems are not.*

**Q2.** Differentiate between **synthetic** and **captured** virtual worlds.
**Ans:** *A **synthetic** virtual world is **fully computer-generated** — nothing in it exists in the real world. A **captured** virtual world is built by **scanning and reconstructing a real environment**, so it corresponds to an actual place. Synthetic worlds give unlimited creative freedom; captured worlds give high realism and accuracy but are limited by what was captured.*

**Q3.** Differentiate between **immersive, non-immersive and shared** VR.
**Ans:** * **Immersive VR** fully immerses the user — **head-referenced, stereoscopic viewing** at full scale with realistic interaction via data gloves, and networked shared worlds. **Non-immersive VR** is not fully immersive — mouse-controlled navigation of a 3-D environment on a monitor with stereo glasses or stereo projection, e.g. **Apple QuickTime VR**. **Shared VR** allows users at **different locations** to meet in the **same virtual world** — one using a BOOM, one a CAVE, one an HMD — each seeing the VE from their own viewpoint, each represented by an **avatar**, interacting as a team.*

**Q4.** Differentiate between **absolute sensors** and **relative sensors**.
**Ans:** *An **absolute sensor (tracker)** returns position/orientation **with respect to a fixed coordinate system**, and its data is **never a zero set** even when the receiver is at rest. A **relative sensor** returns **zeros if not acted upon**, and is another way to control a VR object's position. Absolute sensors suit measuring real-world pose (tracking); relative sensors suit continuous incremental control (panning, moving).*

**Q5.** Differentiate between **Object Mode** and **Edit Mode** in Blender.
**Ans:** *In **Object Mode** the **whole mesh object** is selected as a single unit and transforms apply to the entire object — used for placing the model, adding modifiers, sculpting and rigging. In **Edit Mode** (toggled with **Tab**) **individual vertices, edges and faces** are selected and edited — used for detailed shaping, retopology and UV unwrapping. Edit Mode has three sub-modes: **Vertex (1), Edge (2), Face (3)**.*

**Q6.** What are the **four AR device types** and which is most popular?
**Ans:** * **HUD (heads-up display)** — a transparent display that presents data without the user looking away from their usual viewpoint. **Holographic displays** — extract people/objects from the real world in real time and immerse them into a virtual environment where they interact naturally with digital elements. **Smart glasses** — wearable transparent devices generating AR content in the user's viewpoint; the user sees surroundings as with ordinary glasses. **Handheld** — most popular, with minimal hardware requirements; touch-screen interaction is the common method (phones, tablets).*

**Q7.** Why does **AR reduce cognitive load**?
**Ans:** *Traditional digital interfaces present information on a screen **disconnected from the physical surroundings**, so users must **mentally map** it to real objects — this adds **extraneous cognitive load** and increases cognitive effort. **AR creates automatic, direct and intuitive links** between the real world and digital data by overlaying information in physical context. This **reduces extraneous load**, keeps **germane load** (linking new to current information) high, and delivers **context-aware information**, improving accuracy and task performance.*

**Q8.** List the **advantages and challenges of AR**.
**Ans:** * **Advantages:** reduces cognitive load; provides context-aware information; improves accuracy and task performance; enhances learning and training; natural, intuitive interaction. **Challenges:** accurate tracking and registration; sensor inaccuracies and environmental variability; hardware limitations and latency; usability and ergonomics; scalability and robustness in real-world environments.*

**Q9.** What is the difference between **foveated rendering** and **plane detection**?
**Ans:** * **Foveated rendering** is a **rendering optimisation**: the area the user is looking at (the **fovea**) is rendered in **high resolution** while the **periphery** is rendered at **lower detail**, saving GPU computation and battery — it requires **eye tracking**. **Plane detection** is a **spatial understanding** capability: using depth cameras, LIDAR or structured light the system detects **flat surfaces** (floors, walls, tabletops) so that virtual content can be **anchored correctly** to them. One is about rendering cost; the other is about understanding the environment.*

**Q10.** Why is **full immersion not compulsory** for VR?
**Ans:** *Because VR does not require the user to be locked away in a machine. **Partial immersion** — where the person can manipulate a VR environment without being isolated — is also VR. **Second Life** and the **Nintendo Wii remote** are VR-based products with partial immersion. Only full immersion requires the user to be shielded from the real world.*

**Q11.** Differentiate between **System software, Application software and Development software** in VR/AR.
**Ans:** * **System software** is the backbone that runs the VR/AR hardware and manages resources — it controls hardware devices, manages sensors and displays, and bridges hardware and applications; without it the hardware cannot function. Examples: device drivers, OS support libraries, device operating systems (VR headsets, AR glasses), hardware drivers (motion sensors, cameras, controllers). **Application software** is what end users interact with — education, entertainment, training; examples: VR games, AR navigation apps, medical simulators. **Development software** is used by programmers, designers and developers to build VR/AR apps, providing tools for 3-D modelling, animation and physics; examples: **Unity, Unreal Engine**.*

**Q12.** What is the role of **AudioListener** and **AudioSource** in VR spatial audio?
**Ans:** An **AudioSource** is placed on a virtual object at its **virtual position** and plays a clip with **3-D sound settings** (Spatial Blend, Min/Max Distance, Rolloff, Doppler, HRTF). The **AudioListener** is attached to the **camera/HMD** and **follows the user's head**. As the user turns their head, the HRTF filters update so the sound stays correctly located in world space — this is what makes aural representation complete the VR experience.

**Q13.** Differentiate between **Instance variables** and **Local variables**.
**Ans:** *An **instance variable** is declared **in the class block**, lives for the **entire lifetime of the object**, is accessible by **all methods of the class**, and each instance has its own copy. A **local variable** is declared **inside a method**, exists **only while the method runs** and is destroyed when the method returns, and its scope is **limited to its code block** so other methods cannot access it.*

**Q14.** What happens if you access a `private` member from outside the class?
**Ans:** It produces a **compiler error** — for example `item.name = "Goblin Tooth";` fails if `name` is `private`. Adding `public` fixes it. This is how **encapsulation** is enforced: the class hides its details and exposes only what it chooses.

**Q15.** What are the **five functions of program logic** in AR/VR?
**Ans:** * **1. User input handling** — captures controller, gesture, gaze, voice and touch input; checks which button is pressed, for how long, and whether the user points at a valid object; produces grab, teleport and select responses. **2. Object interaction control** — how objects react to the user and environment (touch → highlight; grabbed → disable gravity) and prevents invalid actions such as grabbing a locked object. **3. Animation flow management** — when animations start, stop, loop or transition, so motion is smooth with no abrupt transitions. **4. Scene transitions** — movement between scenes/levels based on task completion, timer expiry or correct interaction. **5. Backbone: conditionals and loops** — conditionals decide based on user action/system state, and loops run continuous per-frame updates (collision checks, position tracking, animation updates), keeping real-time responsiveness at **60–120 FPS**.*

**Q16.** What are the **most common errors in conditional statements** and their effects in AR/VR?
**Ans:** * **Assignment `=` instead of comparison `==`** → controller buttons do not respond. **Incorrect logical operators (`&&`, `||`, `!`)** → objects behave unexpectedly or incorrectly. **Condition value never updates at runtime** → scenes change without user intent. **Conditions placed outside the update loop** → interactions feel delayed or broken. **Wrong variable used in the check** → **loss of immersion**.*

**Q17.** Give the four **Unity best practices** for code.
**Ans:** * **Naming** — classes PascalCase, variables camelCase, and the **file name must equal the class name**. **Organisation** — variables at the top, methods below, logically grouped. **Encapsulation** — private by default, public only if needed. **DRY** — extract repeated code into methods and classes. Also: **comment complex logic** and use **`[Header]`** to organise the Inspector.*

**Q18.** Why must you multiply movement by `Time.deltaTime`?
**Ans:** *`Time.deltaTime` is the **time between frames**. Without it (`pos += Vector3.right * 5;`) the speed **depends on the frame rate**, so the object moves faster on a 144 Hz monitor than on a 60 Hz one, and the motion looks different on different machines. With it (`pos += Vector3.right * 5 * Time.deltaTime;`) the movement per **second** is constant, so the speed is **frame-rate independent** and looks smooth and identical on all hardware.*

**Q19.** What happens to the **default constructor** when a custom constructor is declared?
**Ans:** The automatic **default parameterless constructor disappears**. So if you declare `public Item(string name, int worth, bool canBeSold)`, then `new Item()` gives a **compile error** — you must call `new Item("Goblin Tooth", 4, true)`. Also, the **argument order, count and types must match** the constructor signature exactly, or the compiler reports an error.

**Q20.** Differentiate between **Static members** and **Instance members**.
**Ans:** *A **static** member belongs to the **class**: there is only **one shared copy**, it is accessed through the **class name** (`Item.NumberOfInstances`, not `item.NumberOfInstances`), it has **no `this`** (no instance context) and it can access **only other static members**, never instance variables like `name` or `worth`. A **static method** therefore cannot use per-object data. **Instance members** belong to **each object separately** — each instance has its own copy of the variable, and they are accessed via the instance (`item.name`). `Debug.Log()` is itself a static method of the `Debug` class.*

**Q21.** What is a **navigation and manipulation interface** in VR?
**Ans:** *A device that allows **interactive change of the view** of the virtual environment and **exploration** through **selection and manipulation** of a virtual object of interest. Examples: trackers, 3-D mice, SpaceBall, data gloves, and gesture interfaces such as the Pinch Glove and 5DT Data Glove.*

**Q22.** What are the **four types of VR-related technologies** mentioned in the notes?
**Ans:** * **Motion trackers** — monitor dancers/athletes for later study in immersive VR. **Augmented reality** — real environments with superimposed virtual objects. **Telepresence** (telemedicine, telerobotics) — immerses a viewer in a real world captured by distant cameras and allows remote manipulation of real objects via robot arms. (Also **VRML** — brings 3-D worlds with integrated hyperlinks to the Web.)*

**Q23.** What is **human horizontal FOV** and why does it matter for HMDs?
**Ans:** *Human horizontal field of view is approximately **200°**. HMDs typically provide only **100°–120°** because of lens and display constraints. A **narrow FOV reduces immersion**, and a **wide FOV increases realism but may cause discomfort**; headsets therefore aim for a **balanced FOV**. The mismatch between the eye's natural FOV and the display's FOV is one cause of eye strain and discomfort in VR.*

**Q24.** Explain the **five functions of program logic** vs **the decision core** — why is logic critical for real-time VR?
**Ans:** *(See Q15 for the five functions.)* Program logic is the **decision-making core** that determines *what happens, when and why*. It is critical because in AR/VR the logic executes **every frame at 60–120 FPS** with **no tolerance** for incorrect or inefficient code: **wrong conditionals** make objects vanish or flicker, an **infinite loop** freezes the headset forcing a restart, **failed interaction checks** mean actions do not execute even when the user does the right thing, and **poorly optimised loops** cause frame drops, lag, stutter and ultimately **motion sickness**. Hence debugging conditionals and loops is a core skill.*

**Q25.** What is **open vs closed loop**, and give a VR example of each.
**Ans:** *(See Q1.)*

---

## Section C — Long-answer / 8-mark quiz questions (question + full-answer pointer)

These are questions that have **not yet appeared** in either paper but follow the same pattern and syllabus — high probability for the next exam. Model answers are in the PYQ sections above.

| # | Likely question | Where the full answer is |
|---|---|---|
| 1 | Explain the **five classic components of a VR system** with a labelled diagram. | Notes §5; short answer in Section B above |
| 2 | Explain **virtuality continuum** with a labelled diagram. | Notes §10.1 |
| 3 | Explain **Azuma's three requirements** for AR with an optical see-through schematic. | Notes §9.3 |
| 4 | Describe the **four types of AR devices** with figures. | Notes §9.5 |
| 5 | Explain **immersive vs non-immersive vs shared VR** with examples. | Notes §8; Section B Q3 |
| 6 | Explain **HMD** — working, importance and limitations — with BOOM/CAVE alternatives. | Paper 2 Q2(a) |
| 7 | Explain **input devices** in detail (DataGlove, 3-D mouse, SpaceBall, MIDI keyboard, Pinch Glove, 5DT). | Paper 1 Q1.2; Notes §12.4, §12.7 |
| 8 | Explain **tracker performance parameters** — accuracy, jitter, drift, latency. | Paper 1 Q1.7; Notes §12.5 |
| 9 | Explain **human sensory systems** and **sensory conflict theory** in VR. | Notes §14; Paper 1 Q2(b) |
| 10 | Explain **importance of aural representation** and how it achieves a complete VR experience. | Paper 1 Q2(b) |
| 11 | Explain **advantages and challenges of AR**. | Notes §9.7; Section B Q8 |
| 12 | Explain **MR's four core technical pillars** and **spatial mapping**. | Notes §10.4–10.5 |
| 13 | Explain **types of VR/AR software** and name the SDKs. | Notes §13; Section B Q11 |
| 14 | Explain **Unity's features** making it popular for interactive games. | Paper 2 Q4(a) |
| 15 | Describe **types of assets** in the Unity Asset Store + advantages/limitations. | Paper 2 Q3(b), Q4(b) |
| 16 | Write a program to **rotate a cube continuously on the Y-axis**. | Paper 2 Q3(a) |
| 17 | Write a program for **smooth movement using arrow keys**. | Paper 1 Q3(a) |
| 18 | Explain **2-D vs 3-D rotation** with examples. | Paper 2 Q6(b) |
| 19 | Explain **importance of triangle meshes** for virtual objects. | Paper 2 Q5(a) |
| 20 | Explain **matrix chaining** — T · R · S — and its importance. | Paper 1 Q4(a) |
| 21 | Explain **optical tracking** with advantages and disadvantages. | Paper 1 Q8(a) |
| 22 | Differentiate **marker-based vs marker-less tracking** with applications. | Paper 1 Q8(b), Paper 2 Q9(b), Q10(a) |
| 23 | Explain **advantages of mobile sensors** in tracking. | Paper 1 Q7(a) |
| 24 | Identify **problems to solve for an effective tracking system**. | Paper 1 Q7(b) |
| 25 | Explain **WebXR** and its challenges. | Paper 1 Q9(a) |
| 26 | Explain the **WebXR application lifecycle** in detail. | Paper 1 Q10(a) |
| 27 | Explain **WebXR request and session lifecycle functionality**. | Paper 1 Q10(b) |
| 28 | Analyse **how a VR/AR session may end** and the steps to follow. | Paper 1 Q9(b) |
| 29 | Explain **real-time rendering and scene management** in VR software. | Paper 2 Q2(b) |
| 30 | Differentiate **FPC vs TPC** + shooting game pros and cons. | Paper 1 Q5(a), Q6(b); Paper 2 Q1.7, Q1.8, Q8(a) |
| 31 | Discuss **advantages/limitations of mouse-aimed camera systems**. | Paper 2 Q7(a) |
| 32 | Explain **importance of sculpting and rigging** in Blender. | Paper 1 Q6(a); Paper 2 Q8(b) |
| 33 | Explain the **need for 3-D software in Unity** and describe **Blender's features**. | Paper 1 Q5(b) |
| 34 | Explain **role of Blender, ZBrush, Unity, Unreal Engine** in immersive AR/VR. | Paper 2 Q7(b) |
| 35 | Explain **collision detection's contribution to realism** in VR training. | Paper 2 Q6(a) |
| 36 | Compare **GameObject vs Prefab instance** + explain Prefabs. | Paper 2 Q1.13; Notes §17.6 |
| 37 | Explain **Box Collider and Rigidbody** importance in Unity. | Paper 1 Q1.8 |
| 38 | Explain **role of physics** in the virtual world. | Paper 2 Q1.6 |
| 39 | Explain **history of VR and AR** with a timeline. | Notes §15 |
| 40 | What is **computer vision** and its role in VR/AR? | Notes §9.6; Paper 2 Q1.19 |

---
---

# 📌 FINAL REVISION CHECKLIST

**Fully solved PYQs:** P1 — 29 questions (Part A 1.1–1.10, Part B 2a–10b) · P2 — 37 questions (Part A 1.1–1.19, Part B 2a–10b) · **Total 66 PYQs with full answers.**

**Most-repeated PYQ clusters (memorise these first):**
1. **Marker-based vs marker-less tracking** — appears **5 times** (P1 8b; P2 1.10, 1.18, 9b, 10a)
2. **DOF / 6 DOF** — **3 times** (P1 1.7; P2 1.5, and inside 2a)
3. **FPC vs TPC** — **4 times** (P1 5a, 6b; P2 1.7, 1.8, 8a)
4. **Unity built-in methods (`Start`/`Update`)** — **3 times** (P1 1.4; P2 1.3, 1.4)
5. **Yaw, pitch, roll** — **2 full 8-mark questions** (P1 3b; P2 5b) + 2-D vs 3-D (P2 1.14, 6b)
6. **Triangle meshes** — **2 full 8-mark questions** (P1 4b; P2 5a)
7. **Unity features** — **2 full questions** (P1 2c; P2 4a)
8. **Sculpting & Rigging** — **2 full questions** (P1 6a; P2 8b)
9. **HMD** — **1 full 8-mark + repeated short** (P2 2a; P1 1.2 related)
10. **4 key elements of VR** — **1 short** (P1 1.10)

**Highest-frequency 1-mark answers:**
MR applications · VR medical application · AR industrial application · VR entertainment application · telepresence · scripts' function · two Unity default methods · DOF · physics role · Third Person Controller · First Person Controller · tracking applications · marker-based tracking · GameObject vs Prefab instance · 2-D vs 3-D rotation · mouse sensitivity · types of tracking · tracking in VR · limitations of marker-based tracking · computer vision · differ between AR and VR · input devices for VR · sensor-based tracking types · 3-D modeling tools · 4 key elements in experiencing VR.

**Code you must be able to write from memory:**
1. Continuous Y-axis rotation (`transform.Rotate` + `Time.deltaTime`)
2. Smooth arrow-key movement (`Input.GetAxisRaw` / `Input.GetKey` + `Vector3` + `Time.deltaTime` + smoothing)
3. A scene switch (`SceneManager.LoadScene`)
4. A coroutine with `WaitForSeconds`
5. Object pooling
6. An `Item` class with a constructor, `this`, instance method, `public`/`private`, and `Debug.Log` concatenation
7. A static counter + static method
8. A `foreach`/`for`/`while`/`do–while` demonstration
9. A conditional (if/else if/else and switch) demonstration
10. Matrix chaining (`Matrix4x4.T * R * S`)

**Where to revise the theory:**
→ See `VRAR CIE-1 Notes.md` (Unit I complete + Unit II Programming with Unity, built from the course notes PDF, with PYQ mapping and a diagram index).

---

**End of PYQ Solutions & Quiz Bank.**
*Sources: QP 21IS72 (Mar/Apr 2025), QP IS372IA (Feb/March 2026), and `AR_VR (IS372IA) - CIE-1 & Quiz-1 Exam Notes.pdf`.*
