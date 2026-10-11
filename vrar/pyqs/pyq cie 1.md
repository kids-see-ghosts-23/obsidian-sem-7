---
share_link: https://share.note.sx/fep21ahs#bm1wx3ybskiQ2ryaBD9HOe+bomCn0VOl40H7FfCz1RE
share_updated: 2026-10-09T06:23:08+05:30
---
## PART A (QUIZ)
### 1.1 Define Virtual Reality. How does virtual reality differ from augmented reality?

**Virtual Reality (VR):** A computer-generated, fully immersive 3-D simulation of an environment with which a user can interact using special electronic equipment (e.g., VR headsets, haptic gloves).
**Difference from Augmented Reality (AR):**

| Aspect | Virtual Reality (VR) | Augmented Reality (AR) |
|---|---|---|
| Environment | Fully computer-generated / virtual world | Real world + virtual content overlaid |
| Real-world visibility | Real world is replaced/blocked out | Real world remains visible |
| Immersion | Complete immersion | Partial immersion |
| Example devices | Oculus Quest, HTC Vive, PSVR | Pokémon GO, Google Maps AR, HoloLens |

### 1.2 Mention the three I's that form the core of Virtual Reality.

The three I's of Virtual Reality (proposed by Burdea and Coiffet) — also called the **VR Triangle / I³**:

JPG

| I | Meaning |
|---|---|
| **Immersion** | The feeling of physically being inside a simulated 3-D world. |
| **Interaction** | The user's ability to manipulate objects and navigate within the virtual environment in real time. |
| **Imagination** | The capacity of the human mind to perceive and conceive virtual entities as real and meaningful. |

### 1.3 Identify any two significant applications of Mixed Reality.

**1. Medical & Healthcare Training:** Interactive surgical simulation and real-time 3-D anatomical visualization layered over the real patient during procedures (e.g. HoloLens-assisted surgery).


**2. Industrial Design & Manufacturing:** Collaborative 3-D CAD visualisation, interactive remote maintenance instructions anchored on real machinery, and assembly-line training.

### 1.4 Name the various input devices commonly used in Virtual Reality environments.

- **Head-mounted display (HMD) with head tracker** — measures head position/orientation to steer the virtual camera.
- **Data gloves** (e.g. VPL DataGlove) — fibre-optic sensors for finger flex angles + a Polhemus sensor for hand position/orientation.
- **Motion controllers** — 6-DOF handheld controllers with buttons/triggers (HTC Vive wands, Oculus Touch, PS Move).
- **3-D mouse / SpaceBall** — 6-DOF position/orientation input (Logitech 3-D mouse uses three ultrasonic speakers; SpaceBall is force-sensitive).
- **Omnidirectional treadmills / motion platforms** — for natural locomotion.
- **Haptic / force-feedback gloves** — for touch feedback.
- Also: eye trackers, Leap Motion / Kinect depth cameras, Myo EMG armband, voice input, keyboard/mouse for text and UI.

### 1.5 Mention any two default built-in methods used during game development in Unity.

Unity calls these *magic / lifecycle methods* automatically — you just write them and Unity invokes them at the right time.

| Method | When it runs |
|---|---|
| **`Start()`** | Once, just before the first `Update()` — used to set up references and initial state. |
| **`Awake()`** | Once, when the GameObject is created — before `Start()`; use for early initialisation. |
| **`Update()`** | Once per frame — main gameplay loop (input, non-physics movement, timers). |
| **`FixedUpdate()`** | At a fixed physics timestep (default 50 Hz) — used for physics (`AddForce`, Rigidbody motion). |
| **`LateUpdate()`** | Once per frame, after all `Update()` calls — used for camera follow and transform interpolation. |

Most commonly asked pair: **`Start()`** (run once at startup) and **`Update()`** (run every frame).

## PART B (TEST)
### 1a. Illustrate the importance of Augmented Reality (AR) in improving marketing strategies and commercial experiences.

Augmented Reality transforms passive consumer viewing into active engagement — the key idea is that customers **interact with the product in their own space** instead of just looking at a picture.


- **"Try Before You Buy" Virtual Try-Ons:** Customers preview products in real time through the camera — cosmetics, eyewear, hairstyles, apparel (e.g. beauty brand virtual try-on filters).

- **Interactive Spatial Visualisation:** Customers place 3-D furniture, decor or appliances directly in their living room to check size, style and fit before buying (e.g. IKEA Place).

- **Enhanced Packaging & Print Media:** Scanning an AR QR code on packaging unlocks interactive 3-D models, brand storytelling, gamified rewards and promotional content.

- **Engagement & Conversion:** AR reduces buyer uncertainty and product returns while increasing time-on-brand, retention and impulse purchases.

### 1b. Describe the significant features of Unity that make it a preferred platform for creating interactive games.

- **Cross-Platform Deployment:** Build once and deploy to PC, mobile (iOS/Android), consoles (PlayStation, Xbox, Switch) and XR devices (Meta Quest, HoloLens).

- **Component-Based Architecture:** Behaviour is attached to GameObjects as components via C# scripts; flexible and reusable.

- **Integrated all-in-one editor:** scene design, asset import, physics, animation, UI, audio, profiling and scripting all in one IDE.

- **First-class XR support:** OpenXR, XR Interaction Toolkit, AR Foundation, Oculus/SteamVR/HoloLens plugins for VR/AR/MR development.

- **Asset Store & large ecosystem:** thousands of ready-made 3-D models, shaders, audio, scripts and tools; huge community and tutorials.

- **Built-in physics & modern rendering:** PhysX-basedRigidbody/collision/joint system, plus URP and HDRP render pipelines.

### 2a. Explain the working of a Virtual Reality system in detail with emphasis on its five classic components with the help of a neat diagram.

A VR system works as a **closed-loop, real-time interaction cycle**: the user acts, input devices capture the action, the VR engine updates the world model, and output devices present the new sensory feedback — and this repeats many times per second.

```
                          +-------------------+
                          |   HUMAN USER      |
                          | (senses + actions)|
                          +---------+---------+
                                    |
                  sensory feedback   |  motor actions
                                    v
               +-------------+   +-------------+
               | OUTPUT      |   | INPUT       |
               | DEVICES     |   | DEVICES     |
               | (Display,   |   | (sensors,   |
               |  audio,     |   |  controllers|
               |  haptics)   |   |  gloves)    |
               +-----+-------+   +-----+-------+
                     ^                 |
        rendered    |                 |  raw sensor data
        frame       |                 v
               +-----+-----------------------------+
               |        VR ENGINE                  |
               |  +-----------------------------+   |
               |  |  SOFTWARE & DATABASES       |   |
               |  |  (Real-Time Database / World |   |
               |  |   Model + simulation state)  |   |
               |  +-----------------------------+   |
               +-----------------------------------+
```

**Working:**
1. **User actions** (head turn, hand gesture, button press, walk) are sensed by **input devices**.
2. Raw sensor data is sent to the **VR engine**, which updates the **software & databases** — the real-time world model containing geometry, textures, physics and object states.
3. The engine computes the next frame and sends rendered visuals, spatial audio and haptic feedback to the **output devices**.
4. The **human user** perceives this feedback and acts again — forming a continuous closed loop. The faster and more accurately this loop runs, the stronger the sense of presence.

#### Five Classic Components (VR Engine, I/O Devices, Software & Databases, User, Task):

1. **VR Engine (Compute Core / Real-Time Simulation):**
    - The software heart of the system — processes input, runs physics, evaluates interaction rules, updates the world model, and drives every frame in real time.

2. **I/O Devices (Input + Output / Display & Actuators):**
    - **Input:** sensors, tracked controllers, data gloves, eye/head trackers that capture the user's movements and commands.
    - **Output:** Head-Mounted Display, spatial audio, force-feedback / haptic actuators that deliver visual, auditory and tactile feedback.

3. **Software & Databases (Real-Time Database / World Model):**
    - Stores the virtual world: geometry, textures, lighting, physics properties and the positional/state data of every 3-D object; the engine reads and updates this model each frame.

4. **Human User:**
    - The organism at the centre of the loop — perceives the sensory output and continuously responds with motor actions, closing the interaction cycle.

5. **Task / Application:**
    - The purpose or activity the user performs in the virtual environment (training, surgery simulation, gaming, design review, etc.) — what actually drives the design of the whole system.

### 2b. Elaborate on the concept of Telepresence with a suitable example.

**Concept:** Telepresence is the technology and sensory experience that lets a human operator **feel present and exert control** in a **remote physical location**, using robotic actuators, sensors and real-time feedback loops.


**Key mechanism:** Combines stereoscopic video/audio feeds from the remote site with teleoperated robotic hardware and haptic force feedback, so the operator sees, hears and feels the remote environment as if they were there.

**Example — Robotic Remote Surgery (e.g. Da Vinci Surgical System):** A surgeon sits at a console in one room (or city) and manipulates 3-D controls. The system replicates the surgeon's micro-movements in real time on the patient using robotic arms, while feeding back immersive high-definition 3-D visual and haptic (force) feedback. The surgeon is effectively "present" at the operating table despite being elsewhere.

### 3a. Discuss the key windows in the Unity editor and illustrate how their layout and size can be adjusted to enhance the workflow.

#### Key Windows:

| Window | What it does |
|---|---|
| **Scene View** | Interactive 3-D/2-D workspace where you visually build, position, rotate and scale GameObjects. |
| **Game View** | Previews the final rendered view from the active camera — what the player actually sees. |
| **Hierarchy Window** | Lists every GameObject in the active scene in a parent-child tree. |
| **Project Window** | Asset browser showing all project files — scripts, 3-D models, textures, audio, prefabs. |
| **Inspector Window** | Shows and lets you edit the properties, components and attached scripts of the currently selected GameObject. |
| **Console Window** | Shows errors, warnings and debug logs (`Debug.Log`). |


#### Workflow Customization:

- **Docking / floating:** Drag a window by its tab and drop it onto a panel region to dock it, or drop it into empty space to make it a floating window.
- **Resizing:** Hover over a divider between split panels and drag to resize.
- **Preset layouts:** Use the layout dropdown (top-right) to switch among built-in arrangements like *Tall*, *2 by 3*, *4 Split*, or save your own custom layout for a particular task (level design, animation, debugging, etc.).

### 3b. Explain the four basic tracker performance parameters that have to be considered regardless of the technology used.

1. **Accuracy:** How close the tracker’s reported position/orientation is to the true physical location (error range / resolution).

2. **Jitter (noise / precision):** Rapid, unwanted high-frequency fluctuations in the output when the tracked target is actually stationary.

3. **Latency (update delay / lag):** Total time between the physical movement of the object and the corresponding update appearing in the system — high latency breaks presence and causes sickness.

4. **Update rate (sampling rate / frequency):** Number of position/orientation measurements the tracker produces per second (Hz) — higher rates give smoother tracking but demand more processing.


### 4. Develop a program that enables an object to move smoothly in response to arrow-key inputs.

Below is a complete Unity C# script that moves the object smoothly with the arrow keys — frame-rate independent (uses `Time.deltaTime`), diagonal-safe (normalised) and with gradual acceleration (Lerp smoothing).

JPG

```csharp
using UnityEngine;

public class ArrowKeyMover : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 5f;          // units per second
    public float smooth = 10f;        // higher = quicker acceleration

    private Vector3 currentVelocity;

    void Update()
    {
        // Arrow keys via the Horizontal / Vertical input axes
        float h = Input.GetAxisRaw("Horizontal");   // Left / Right
        float v = Input.GetAxisRaw("Vertical");     // Up / Down

        Vector3 direction = new Vector3(h, 0f, v).normalized;

        // Target velocity for this frame
        Vector3 targetVelocity = direction * speed;

        // Smoothly ramp velocity so the object accelerates / decelerates gently
        currentVelocity = Vector3.Lerp(currentVelocity, targetVelocity,
                                        Time.deltaTime * smooth);

        // Move frame-rate independently
        transform.Translate(currentVelocity * Time.deltaTime, Space.World);
    }
}
```

**Why this is smooth:**
1. **`Time.deltaTime`** makes the movement the same speed at any frame rate.
2. **`.normalized`** prevents diagonal movement from being √2× faster.
3. **`Vector3.Lerp`** ramps the velocity gradually instead of snapping to full speed.
4. `Input.GetAxisRaw` returns a continuous -1 / 0 / +1 every frame, so the object keeps moving while a key is held and stops cleanly when released.

### 5. Analyse Stevens' power law and illustrate how it models the relationship between physical stimulus intensity and human perceptual response. Give suitable examples.

#### Mathematical Model

Stevens' Power Law is an empirical relationship between the **physical magnitude of a stimulus (I)** and the **perceived psychological intensity (S):**

JPG

**S = k · Iᵃ**

Where:
- **S** = perceived sensory magnitude
- **I** = physical stimulus intensity
- **k** = scaling constant that depends on the units and measurement method
- **a** = exponent characteristic of the specific sensory modality

    JPG

#### Analysis based on the exponent (a)

The value of **a** tells you whether perception *compresses*, *tracks linearly*, or *expands* relative to the physical stimulus:

1. **Compressive modalities (a < 1):**
    - As physical intensity increases, perceived sensation grows more slowly — diminishing returns.
    - *Example — Luminance (a ≈ 0.33):* Doubling the physical light does **not** make a room feel twice as bright; the perception compresses at high brightness.

        JPG

2. **Linear modalities (a ≈ 1):**
    - Perceived intensity grows proportionally with physical intensity.
    - *Example — Apparent length (a ≈ 1.0):* Doubling the physical length of a line is perceived as roughly double the length.

        JPG

3. **Expansive modalities (a > 1):**
    - Small increases in physical intensity produce disproportionately large rises in subjective sensation — typical of pain/warning systems.
    - *Example — Electric shock (a ≈ 3.5):* A small rise in current causes a very large increase in perceived discomfort/pain.

        JPG

**Conclusion:** Stevens' Power Law shows that human perception is **not a linear copy** of physical reality — different senses map stimulus intensity to perception with different exponents, which is why VR must account for perceptual scaling (e.g. brightness, haptic force, motion) rather than just raw physical values.