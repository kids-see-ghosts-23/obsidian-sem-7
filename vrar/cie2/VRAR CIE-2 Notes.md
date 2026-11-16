---
tags: [vrar, cie-2, notes]
share_link: https://share.note.sx/ncymn5et#5LPvbJESg+AMQwa5CEVrgtNjbv0gvR72d9Xuq0QHmoE
share_updated: 2026-11-16T01:22:24+05:30
---
# VR & AR (IS372IA) — CIE-2 Detailed Notes

> **Coverage:** Unit II from **Prefabs** to **The Asset Store** + Unit III (**Geometry of Virtual Worlds** and **Motion in Real and Virtual Worlds**).
> **Companion file:** [[VRAR PYQ CIE-2]] — every question in the CIE-2 paper solved in exam format.
> **Built from:** `cie2.html`, `vrar.md`, `VRAR CIE-1 Notes.md`, `VRAR PYQ CIE-1.md`, `sample progs.md`, plus standard Unity / real-time graphics references.

**Jump to:** [[#1. Debugging conditional and looping statements|1. Debugging]] · [[#2. Prefabs|2. Prefabs]] · [[#3. Working with objects|3. Objects]] · [[#4. Working with scripts|4. Scripts]] · [[#5. Player movement|5. Player movement]] · [[#6. Designing levels|6. Level design]] · [[#7. Menu and UI|7. Menu & UI]] · [[#8. Camera movement|8. Camera]] · [[#9. Advanced 3D movement|9. Advanced movement]] · [[#10. The Unity Asset Store|10. Asset Store]] · [[#11. Geometry of virtual worlds|11. Geometry]] · [[#12. Transforming models|12. Transforms]] · [[#13. 2D and 3D rotation — yaw, pitch, roll|13. Rotation]] · [[#14. Viewing transformations|14. Viewing]] · [[#15. Chaining the transformations|15. Chaining]] · [[#16. Motion in real and virtual worlds|16. Motion]] · [[#17. Formula sheet (quick revision)|17. Formulas]] · [[#18. Exam strategy and question map|18. Exam strategy]]

---

## 0. Syllabus map — what CIE-2 covers

| Unit | Syllabus line | Where in this file |
|---|---|---|
| **II** | Debugging conditional and looping statements | §1 |
| **II** | **Prefabs** | §2 |
| **II** | Working with objects | §3 |
| **II** | Working with scripts | §4 |
| **II** | Player movement | §5 |
| **II** | Designing levels | §6 |
| **II** | Menu and UI | §7 |
| **II** | Camera movement | §8 |
| **II** | Advanced 3D movement | §9 |
| **II** | Further learning for Unity: **The Asset Store** | §10 |
| **III** | Geometry of virtual worlds: **geometric models** | §11 |
| **III** | **Transforming models** | §12 |
| **III** | **2D and 3D rotation — yaw, pitch, roll** | §13 |
| **III** | **Viewing transformations** | §14 |
| **III** | **Chaining the transformations** | §15 |
| **III** | Motion in real and virtual worlds: **velocities and acceleration** | §16.1–16.2 |
| **III** | **Physics in the virtual world** | §16.3–16.5 |
| **III** | **Mismatched motion and vection** | §16.6–16.8 |

**Exam pattern (from the paper):** Part A is short-answer (1–2 marks each); Part B is long-answer (4, 6 or 10 marks). The CIE-2 paper seen so far asks **8 Part-B questions**: Q1a, Q1b, Q2a, Q2b, Q3a, Q3b, Q4, Q5.

---

## 1. Debugging conditional and looping statements

### 1.1 Conditionals — making the program choose

A **conditional statement** executes a block only when a Boolean expression is true.

```csharp
// if / else if / else
if (health <= 0)
{
    Die();
}
else if (health < 25)
{
    ShowLowHealthWarning();
}
else
{
    Regenerate();
}

// switch — cleaner for many discrete cases
switch (interactionType)
{
    case InteractionType.Grab:    Grab();    break;
    case InteractionType.Teleport: Teleport(); break;
    case InteractionType.Press:   Press();   break;
    default:                      Ignore();  break;
}
```

Key points:

- The condition must evaluate to `bool` (`true` / `false`), not an integer (unlike C/C++).
- `&&` (AND), `||` (OR), `!` (NOT) combine conditions; `==`, `!=`, `<`, `>`, `<=`, `>=` compare values.
- **Short-circuit evaluation:** in `a && b`, `b` is not evaluated if `a` is false; in `a || b`, `b` is not evaluated if `a` is true. This matters when `b` has side effects or would throw a null-reference exception.
- Use `switch` when comparing one variable against many constant values; use `if` for ranges and compound conditions.
- In Unity, conditionals commonly gate **input**, **state transitions**, **collision response**, **UI visibility** and **spawning**.

### 1.2 Loops — repeating work

| Loop | When to use | Syntax sketch |
|---|---|---|
| `for` | Known number of iterations (arrays, spawn counts) | `for (int i = 0; i < n; i++) { ... }` |
| `while` | Repeat while a condition holds (may run zero times) | `while (condition) { ... }` |
| `do … while` | Must run **at least once** | `do { ... } while (condition);` |
| `foreach` | Iterate every element of a collection | `foreach (var item in list) { ... }` |

```csharp
// Spawn 10 enemies in a row
for (int i = 0; i < 10; i++)
{
    Vector3 pos = new Vector3(i * 2f, 0f, 0f);
    Instantiate(enemyPrefab, pos, Quaternion.identity);
}

// Keep moving while the user holds the trigger
while (Input.GetButton("Fire1"))
{
    FireOnce();
}

// Iterate all child objects
foreach (Transform child in transform)
{
    child.gameObject.SetActive(false);
}
```

Loop control:

- `break` — leave the loop immediately.
- `continue` — skip the rest of this iteration and go to the next one.
- **Never write an infinite loop in `Update()`** — it freezes the main thread and the whole game/VR view. If a `while` depends on a condition that never changes, the editor appears to hang.

### 1.3 Debugging tools in Unity

| Tool | What it does |
|---|---|
| **Console** | Shows `Debug.Log`, warnings, errors and stack traces. |
| `Debug.Log()` / `LogWarning()` / `LogError()` | Print values, confirm a branch ran, inspect object state. |
| **Breakpoints** | Pause execution at a line in Visual Studio / Rider / VS Code; inspect locals, watch expressions, call stack. |
| **Step Over / Step Into / Step Out** | Execute one line, enter a method, or finish the current method. |
| **Debug.DrawLine / DrawRay** | Draw a line or ray in the Scene view for a frame — excellent for checking raycasts, gaze direction and ground checks. |
| **Profiler** | Find frame-time spikes, GC allocations, rendering and physics cost. |
| **Frame Debugger** | Inspect draw calls and see why an object is not rendered. |
| **Physics Debugger** | Visualise colliders, contacts and rigid-body sleep state. |

Typical debugging workflow:

1. **Reproduce** the bug reliably.
2. **Localise** it — which script, which object, which frame?
3. **Instrument** — log the input values, state variables and branch taken.
4. **Form a hypothesis** and test one change at a time.
5. **Fix the cause**, not the symptom.
6. **Regression-test** the fix in the headset / on the device.

### 1.4 Common conditional / loop bugs (and VR symptoms)

| Bug | Example | Symptom |
|---|---|---|
| Assignment instead of comparison | `if (isGrabbing = true)` | Condition is always true; object is always grabbed. |
| Missing braces | `if (x) DoA(); DoB();` | `DoB()` runs unconditionally. |
| Off-by-one | `i <= array.Length` | `IndexOutOfRangeException`. |
| Wrong Boolean logic | `if (!a || !b)` instead of `if (!(a || b))` | Interaction fires in the wrong state. |
| Infinite loop | `while (x < 10)` with `x` never incremented | Editor/headset freezes. |
| Checking input in `FixedUpdate` | `Input.GetKeyDown` in `FixedUpdate` | Some presses are missed because `FixedUpdate` runs at 50 Hz, not every frame. |
| Null reference | Accessing a destroyed object or unassigned prefab | `NullReferenceException`, interaction silently stops. |
| Comparing floats with `==` | `if (speed == 5f)` | Rarely true because of floating-point error; use a tolerance. |
| Stale state | `isGrounded` never reset | Player cannot jump again, or jumps forever. |

**VR-specific debugging checklist**

- Is the input action map enabled and the correct controller bound?
- Is the raycast hitting the intended **layer** and within `maxDistance`?
- Is the object **active** and does it have a collider?
- Is the script running on the right **GameObject** (not a child without the component)?
- Is the code running in `Update` (input) versus `FixedUpdate` (physics)?
- Does the bug happen only at low frame rate? Check `Time.deltaTime` usage.
- Does it happen only in the headset? Check stereo rendering, tracking origin and comfort settings.

---

## 2. Prefabs

### 2.1 What is a Prefab?

A **Prefab** is a **reusable template of a GameObject** stored as an asset in the Project. It records the object's:

- **Components** (Transform, Mesh Filter, Mesh Renderer, Collider, Rigidbody, scripts, AudioSource, etc.),
- **Property values** (position, rotation, scale, material, colours, script fields),
- **Child objects** and their components,
- **Asset references** (meshes, materials, textures, audio clips).

Think of a Prefab as a **stamp**: design the object once, then press as many copies as needed. Every copy is linked to the master, so a change to the master updates all copies.

```
Prefab Asset (Project window)
        │  drag / Instantiate()
        ├── Instance 1 (Scene A)
        ├── Instance 2 (Scene A)
        └── Instance 3 (Scene B)
```

### 2.2 Prefab Asset vs Prefab Instance vs plain GameObject

| Basis | **Prefab Asset** | **Prefab Instance** | **Plain GameObject** |
|---|---|---|---|
| **Lives in** | Project window (`Assets/…`) | Hierarchy / Scene | Hierarchy / Scene |
| **Nature** | Master template on disk | Linked copy of the template | Independent object |
| **Changes** | Editing it updates all instances | Can override locally | Affects only itself |
| **Reuse** | Across many scenes and projects | Many copies of one asset | Must be duplicated manually |
| **Icon** | Blue cube / prefab icon | Blue cube in Hierarchy | Grey/white object icon |
| **Creation** | Drag from Hierarchy to Project | Drag asset into Scene, or `Instantiate()` | `GameObject → Create …` |

> **Exam one-liner:** *The asset is the master copy; the instance is a placed copy that stays linked to it. A plain GameObject has no master, so changing it never affects anything else.*

### 2.3 Creating and placing a Prefab — step by step

**Method A — build in the scene, then drag to Project (the normal workflow)**

1. **Create the object:** `GameObject → 3D Object → Cube` (or Empty, Sphere, etc.).
2. **Configure it:** rename it, set position/rotation/scale, add a material, add components (`Box Collider`, `Rigidbody`, `AudioSource`) and attach scripts.
3. **Build children if needed:** e.g. a `Grabbable` root with a mesh child and an attach-point child.
4. **Drag the object from the Hierarchy into the Project window** (ideally into an `Assets/Prefabs` folder).
5. Unity creates a **Prefab Asset** with a blue icon. The Hierarchy object becomes a **Prefab Instance** (blue).
6. **Place instances:** drag the asset from the Project into the Scene or Hierarchy. Repeat as many times as needed.
7. **Edit the master:** double-click the asset (or click **Open** in the Inspector) to enter **Prefab Mode** — an isolated editing scene with a blue background. Changes are saved to the asset; use **Apply** / **Revert** when working on an instance.
8. **Verify:** change a property on the asset (e.g. material colour) and confirm every instance updates.

**Method B — create from the Project menu**

- Right-click in the Project window → `Create → Prefab` (creates an empty prefab), then drag a GameObject onto it.
- Or right-click a GameObject in the Hierarchy → `Create → Prefab Variant` (see §2.6).

**Method C — at runtime (code)**

```csharp
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemyPrefab;      // assign in Inspector
    public Transform spawnPoint;

    void Start()
    {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
```

`Instantiate()` creates a **runtime instance** of the prefab. It is the standard way to spawn bullets, enemies, pickups, UI popups, particles and AR trackables.

**Method D — editor script (preserves the prefab link)**

```csharp
#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public static class PrefabSpawner
{
    [MenuItem("Tools/Spawn Prefab")]
    static void Spawn()
    {
        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Enemy.prefab");
        PrefabUtility.InstantiatePrefab(prefab);   // editor-only, keeps prefab connection
    }
}
#endif
```

### 2.4 Prefab Mode, overrides, Apply and Revert

**Prefab Mode** is an isolated editing environment for one prefab. You can edit the prefab without other scene objects distracting you. Unity may auto-save; you can also save manually.

An **override** is a property on an instance that differs from the asset. Examples:

- moved / rotated / scaled instance,
- changed material or colour,
- added or removed a component,
- changed a script field,
- added a child object.

Unity marks overridden fields in **bold** and lists them in the **Overrides** dropdown in the Inspector.

| Action | Effect |
|---|---|
| **Apply** (Apply All) | Copies the instance's changes back to the asset → **all** instances update. |
| **Revert** (Revert All) | Discards the instance's overrides → it returns to the asset values. |
| **Apply single property** | Copies only that property to the asset. |
| **Revert single property** | Restores only that property on the instance. |

> **Rule of thumb:** if the change should apply to every copy, edit the **asset** (or Apply). If it is a one-off variation, keep it as an **override** on that instance.

### 2.5 Nested Prefabs

A **nested prefab** is a prefab placed **inside another prefab**.

Example: a `Character` prefab contains a `Sword` prefab and a `Shield` prefab as children. Editing the `Sword` asset updates every character that contains it. Overrides on the nested instance affect only that parent prefab.

**Uses:** characters with equipment, vehicles with wheels, UI panels with buttons, modular level kits, weapons with attachments.

**Caution:** deep nesting makes it harder to see where a value comes from; the Overrides panel helps, but debugging can be confusing.

### 2.6 Prefab Variants

A **Prefab Variant** is a prefab that **inherits** another prefab and selectively overrides it.

- Base prefab: `Enemy` (health 100, speed 3, red material).
- Variant: `EnemyBoss` (health 500, speed 2, purple material, extra shield child).
- Variant: `EnemyFast` (health 60, speed 6, yellow material).

Changes to the base propagate to variants **except** where the variant overrides them. Variants are ideal for enemy types, weapon skins, UI themes and platform-specific versions.

**Create:** right-click a prefab in Project → `Create → Prefab Variant`, or drag a prefab into the scene and use the Variant workflow.

### 2.7 Runtime instantiation and object pooling

**Instantiate** is fine for occasional objects, but creating and destroying hundreds of bullets per second causes **garbage-collection spikes** and frame drops — fatal in VR where a stable 90/120 Hz matters.

**Object pooling** reuses a fixed set of inactive objects:

```csharp
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public GameObject bulletPrefab;
    public int poolSize = 50;

    private readonly Queue<GameObject> pool = new Queue<GameObject>();

    void Awake()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject b = Instantiate(bulletPrefab, transform);
            b.SetActive(false);
            pool.Enqueue(b);
        }
    }

    public GameObject Get(Vector3 pos, Quaternion rot)
    {
        if (pool.Count == 0) return null;          // or grow the pool
        GameObject b = pool.Dequeue();
        b.transform.SetPositionAndRotation(pos, rot);
        b.SetActive(true);
        return b;
    }

    public void Return(GameObject b)
    {
        b.SetActive(false);
        pool.Enqueue(b);
    }
}
```

Pooling benefits: no repeated allocation, predictable performance, less GC, better comfort in VR.

### 2.8 UI Prefabs

Buttons, panels, menus, dialog boxes, HUD elements and tooltips are designed once as prefabs and reused.

- A `MenuButton` prefab holds a `Button`, `TextMeshProUGUI`, background image, hover/pressed animations and a click handler.
- A `PauseMenu` prefab holds the panel, buttons and event wiring.
- A `Tooltip` prefab is spawned near the controller when the user points at an object.

UI prefabs keep the visual language consistent and make a global change (font, colour, corner radius) a one-asset edit.

### 2.9 Prefabs in VR/AR — why they matter

| Use case | How prefabs help |
|---|---|
| **XR rig / player** | One `XROrigin` prefab with camera offset, controllers, locomotion provider; drop into every scene. |
| **Interactables** | `Grabbable`, `Socket`, `Lever`, `Door`, `Button` prefabs reused across levels with consistent physics and haptics. |
| **Controllers / hands** | Left and right controller models, ray interactors and hand visuals are prefab instances. |
| **Spawners** | Enemies, projectiles, confetti, AR anchors and UI popups are instantiated from prefabs. |
| **Networked objects** | Network frameworks spawn the same prefab on every client; a prefab registry keeps IDs consistent. |
| **AR trackables** | A `PlacementIndicator` prefab and an `ARObject` prefab are instantiated on detected planes/images. |
| **Variants** | Low-poly / high-poly LOD variants, Quest / PC / mobile variants, colour skins — all inherit one base. |
| **Scene organisation** | A `Prefabs` folder plus nested prefabs turns a messy Hierarchy into a modular kit. |

### 2.10 Advantages and disadvantages of prefabs

**Advantages**

1. **Reuse** — build once, place many times, across scenes and projects.
2. **Consistency** — every copy has the same structure and behaviour.
3. **Single source of truth** — fix a bug in the asset; all instances update.
4. **Faster iteration** — tune values in Prefab Mode and see the change everywhere.
5. **Better scene organisation** — the Hierarchy holds instances, not hundreds of duplicated objects.
6. **Team collaboration** — different people can work on different prefabs.
7. **Runtime spawning** — `Instantiate()` is the foundation of dynamic content.
8. **Memory efficiency** — meshes, materials and textures are shared assets, not copied per instance.
9. **Variants and nesting** — manage families of objects without duplicating everything.
10. **Safer refactoring** — adding a component to the base updates all variants/instances.

**Disadvantages / cautions**

1. **Too many overrides** make it hard to tell the base value from the instance value.
2. **Nested prefabs** can make debugging and ownership confusing.
3. **Accidental Apply** can push a local experiment to every instance.
4. **Poor folder structure** creates prefab clutter and broken references.
5. **Large prefabs** with many children can be slow to open and edit.
6. **Runtime prefab edits** do not persist (changes in play mode are lost unless applied in edit mode).
7. **Discipline is required** — naming, folders, variants and documentation.

### 2.11 Prefab exam points (memorise)

- Prefab = **reusable GameObject template** stored as an **asset**.
- **Asset** (master, Project) vs **instance** (copy, Scene).
- Create: configure GameObject → drag Hierarchy → Project → drag asset into Scene.
- **Prefab Mode** for isolated editing; **Apply/Revert** for overrides.
- **Nested prefab** = prefab inside prefab; **variant** = inherited prefab with overrides.
- **Instantiate()** for runtime spawning; **pooling** for performance.
- Benefits: reuse, consistency, maintainability, organisation, collaboration, runtime spawning.
- VR/AR: XR rigs, interactables, controllers, UI, spawners, AR trackables, LOD/platform variants.

---

## 3. Working with objects

### 3.1 Class, object and instance

- A **class** is a blueprint: it defines the **data** (fields) and **behaviour** (methods) that objects of that type have.
- An **object** is a concrete entity in memory created from a class.
- An **instance** is one particular object made from a class. `john`, `sarah` and `mike` are three instances of `Person`.

```csharp
public class Person
{
    public string firstName;
    public string lastName;
    public int age;

    public void Introduce()
    {
        Debug.Log($"Hi, I'm {firstName} {lastName}, age {age}.");
    }
}

Person john = new Person();
john.firstName = "John";
john.lastName = "Doe";
john.age = 30;
john.Introduce();
```

Naming conventions: **classes and methods PascalCase**, **variables and parameters camelCase**.

### 3.2 Fields, local variables and defaults

| Kind | Where declared | Lifetime |
|---|---|---|
| **Instance field** | In the class block | As long as the object exists |
| **Static field** | In the class block with `static` | As long as the application runs |
| **Local variable** | Inside a method | Until the method returns |
| **Parameter** | In the method signature | Until the method returns |

Default values when a field is not initialised:

| Type | Default |
|---|---|
| `int`, `float`, `double` | `0` |
| `bool` | `false` |
| `char` | `'\0'` |
| `string`, object references | `null` |

Local variables are **not** automatically initialised — the compiler requires assignment before use.

### 3.3 Methods

A **method** packages behaviour so it can be called repeatedly.

```csharp
public class Item
{
    public string name;
    public int worth;
    public bool canBeSold;

    public void LogInfo()
    {
        Debug.Log($"{name} is worth {worth} gold. Sellable: {canBeSold}");
    }

    public int SellPrice(int taxPercent)
    {
        return worth - (worth * taxPercent / 100);
    }
}
```

- Methods are called through an instance: `item.LogInfo()`.
- **DRY** (Don't Repeat Yourself): write the logic once; fix it in one place.
- **Return type** `void` means no value is returned.
- **Parameters** pass data in; `return` passes a value out.
- Method **overloading**: same name, different parameter lists.

### 3.4 Constructors and `this`

A **constructor** is a special method that runs when an object is created. It has the same name as the class and no return type.

```csharp
public class Item
{
    public string name;
    public int worth;
    public bool canBeSold;

    public Item(string name, int worth, bool canBeSold)
    {
        this.name = name;          // this.name = field, name = parameter
        this.worth = worth;
        this.canBeSold = canBeSold;
    }
}

Item sword = new Item("Iron Sword", 120, true);
```

- `this` refers to the **current instance**; it resolves name clashes between a field and a parameter.
- If you declare **any** constructor, C# no longer generates the automatic **parameterless** constructor. `new Item()` then becomes a compile error unless you also write `public Item() { }`.

### 3.5 Access modifiers and encapsulation

| Modifier | Visible from |
|---|---|
| `public` | Anywhere; also shown in the Unity Inspector |
| `private` | Only inside the class (default for class members) |
| `protected` | The class and its subclasses |
| `internal` | The same assembly |
| `protected internal` | Same assembly or subclass |

**Encapsulation** means hiding internal data and exposing only what is needed. In Unity, `public` fields appear in the Inspector; `private` fields do not. Use `[SerializeField] private` when you want Inspector editing without public access.

```csharp
public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;

    public int CurrentHealth => currentHealth;   // read-only property

    public void TakeDamage(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth - amount, 0, maxHealth);
        if (currentHealth == 0) Die();
    }

    private void Die() { /* ... */ }
}
```

### 3.6 Static members

A **static** member belongs to the **class**, not to any instance. There is exactly one copy shared by all objects.

```csharp
public class Item
{
    public static int NumberOfInstances;

    public Item()
    {
        NumberOfInstances++;
    }
}

Debug.Log(Item.NumberOfInstances);   // class name, not instance name
```

- A static method has no `this`, so it can access only static members.
- Static fields persist for the application's lifetime; they can leak state between scenes, so reset them deliberately.
- Unity example: `Time.deltaTime`, `Mathf.Clamp`, `Vector3.zero`, `Input.GetAxis`.

### 3.7 Value types vs reference types

| Value type (`struct`) | Reference type (`class`) |
|---|---|
| Holds the data directly | Holds a reference (address) to the data |
| Copied on assignment | Reference copied; both variables point to the same object |
| Examples: `int`, `float`, `bool`, `Vector3`, `Quaternion`, `Color` | Examples: `string`, arrays, `List<T>`, `GameObject`, `Transform`, custom classes |
| Stored on the stack or inline | Stored on the managed heap |

```csharp
Vector3 a = new Vector3(1, 2, 3);
Vector3 b = a;          // copy — changing b does not change a
b.x = 99;

Person p1 = new Person();
Person p2 = p1;         // same object — changing p2 changes p1
p2.age = 40;
```

`Vector3` is a struct, so it is copied by value. This is why `transform.position.x = 5` does not compile — you must read the struct, modify it, and assign it back:

```csharp
Vector3 p = transform.position;
p.x = 5f;
transform.position = p;
```

### 3.8 GameObject vs Component vs C# object in Unity

- A **GameObject** is a lightweight container. It has a name, tag, layer, active state and a **Transform**.
- **Components** add functionality: `MeshFilter`, `MeshRenderer`, `Collider`, `Rigidbody`, `AudioSource`, `Camera`, `Light`, custom scripts.
- A **script** is a C# class; when attached, it becomes a **component instance** on a GameObject.
- `gameObject` refers to the GameObject the script is attached to; `transform` is its Transform component.
- `GetComponent<T>()` finds a component; `GetComponentInChildren<T>()` searches children; `TryGetComponent<T>(out var c)` is the safe, allocation-free version.

```csharp
Rigidbody rb = GetComponent<Rigidbody>();
if (rb != null) rb.AddForce(Vector3.up * 5f, ForceMode.Impulse);

if (TryGetComponent<Renderer>(out var rend))
    rend.material.color = Color.red;
```

### 3.9 Inheritance, polymorphism and composition (brief)

- **Inheritance:** `class Enemy : MonoBehaviour` inherits members and can override `virtual` methods.
- **Polymorphism:** a base reference can point to a derived object; the derived override runs.
- **Composition:** a GameObject "has" components rather than "is" a subclass. Unity strongly favours composition: build behaviour by attaching small components.
- Prefer **composition** for gameplay features; use inheritance for genuine "is-a" relationships and shared interfaces.

### 3.10 Useful collection types

| Type | Use |
|---|---|
| `T[]` | Fixed-size array; fastest, no resizing. |
| `List<T>` | Resizable list; `Add`, `Remove`, `Contains`, indexing. |
| `Dictionary<K,V>` | Key → value lookup; fast `TryGetValue`. |
| `Queue<T>` | FIFO — object pools, event queues. |
| `Stack<T>` | LIFO — undo history, matrix stack. |
| `HashSet<T>` | Unique items, fast membership tests. |

---

## 4. Working with scripts

### 4.1 What is a Unity script?

A Unity script is a C# class that inherits from **`MonoBehaviour`**. This base class gives it Unity-specific abilities:

- lifecycle callbacks (`Awake`, `Start`, `Update`, …),
- access to `transform`, `gameObject`, `enabled`,
- coroutines,
- the ability to be attached to a GameObject,
- serialization of public / `[SerializeField]` fields into the Inspector.

```csharp
using UnityEngine;

public class HelloUnity : MonoBehaviour
{
    public float speed = 5f;

    void Start()
    {
        Debug.Log("Hello from " + gameObject.name);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
```

A plain C# class is created with `new`; a `MonoBehaviour` is created by **attaching** it to a GameObject (or by `Instantiate` on a prefab that has it). You should **not** call `new` on a MonoBehaviour.

### 4.2 File and class rules

- The **file name must match the class name exactly** (case-sensitive, no spaces): `PlayerController.cs` contains `public class PlayerController`.
- One MonoBehaviour per file is the convention; the class must be `public` to be attachable.
- The file must be inside the project's `Assets` folder.
- Namespace is optional; if used, the class name still must match the file name.
- If the names do not match, Unity cannot attach the script ("Can't add script component…").

### 4.3 Lifecycle / event methods

Unity calls these automatically; you just declare them.

| Method | When it runs | Typical use |
|---|---|---|
| `Awake()` | Once, when the object is loaded, before `Start` | Cache references, initialise self-contained state |
| `OnEnable()` | Every time the component/object becomes enabled | Subscribe to events, reset state |
| `Start()` | Once, before the first `Update`, after all `Awake`s | Setup that depends on other objects being awake |
| `FixedUpdate()` | At a fixed physics timestep (default 0.02 s = 50 Hz) | Rigidbody forces, physics movement |
| `Update()` | Once per rendered frame | Input, non-physics movement, timers, UI |
| `LateUpdate()` | Once per frame, after all `Update`s | Camera follow, IK, procedural correction |
| `OnGUI()` | Several times per frame (IMGUI) | Legacy/debug UI only |
| `OnDisable()` | When the component/object is disabled | Unsubscribe, stop effects |
| `OnDestroy()` | When the object is destroyed | Cleanup, save data |
| `OnApplicationPause/Quit()` | Mobile/app lifecycle | Save state, release resources |
| `OnCollisionEnter/Stay/Exit` | Physics collisions | Damage, impact sound, bounce |
| `OnTriggerEnter/Stay/Exit` | Trigger volumes | Pickups, zones, proximity |
| `OnControllerColliderHit` | CharacterController hit | Push objects, detect walls |
| `OnParticleCollision` | Particle hit | Fire/acid damage |
| `OnDrawGizmos` / `OnDrawGizmosSelected` | Editor Scene view | Draw debug shapes |

**Order of execution (simplified):**

```
Awake → OnEnable → Start → [FixedUpdate → physics] → Update → LateUpdate → rendering
                                      ↑
                            repeats every frame
```

Important details:

- All `Awake`s run before any `Start`, so a script can safely use a reference set in another script's `Awake`.
- `FixedUpdate` may run zero, one or several times per rendered frame. Put **physics** there, not input.
- `Update` timing varies with frame rate; use `Time.deltaTime` for frame-rate-independent motion.
- `LateUpdate` is where a camera should follow the player, so it reads the player's final position for the frame.
- Script execution order can be changed in `Project Settings → Script Execution Order` or with `[DefaultExecutionOrder(100)]`.

### 4.4 Public variables, the Inspector and attributes

```csharp
public class Turret : MonoBehaviour
{
    [Header("Targeting")]
    [SerializeField] private float range = 10f;
    [SerializeField] private float fireRate = 2f;

    [Header("Visuals")]
    public GameObject muzzleFlash;
    [Range(0f, 1f)] public float flashDuration = 0.1f;
    [Tooltip("Degrees per second")]
    public float turnSpeed = 90f;

    [HideInInspector] public bool debugMode;
}
```

| Attribute | Effect |
|---|---|
| `[SerializeField]` | Shows a private field in the Inspector without making it public |
| `[HideInInspector]` | Hides a public field from the Inspector (it stays public in code) |
| `[Header("…")]` | Adds a bold heading above a group of fields |
| `[Tooltip("…")]` | Shows help text on hover |
| `[Range(min, max)]` | Slider instead of a number field |
| `[Space]` | Adds vertical space |
| `[TextArea]` | Multi-line text box |
| `[RequireComponent(typeof(Rigidbody))]` | Automatically adds a required component |
| `[ExecuteAlways]` | Runs in edit mode too |
| `[CreateAssetMenu]` | Adds a menu item for a `ScriptableObject` |

Public fields **of serializable types** are **serialized**: their values are saved with the scene/prefab and can be edited per instance. Changes made in Play mode are **reverted** when play stops.

### 4.5 `Vector3`, `Quaternion` and the Transform

- `Vector3` stores `x`, `y`, `z` — used for positions, directions, velocities, scales and Euler angles.
- `Quaternion` stores a 3D rotation without gimbal lock — used for orientations.
- `transform` exposes `position`, `rotation`, `localPosition`, `localRotation`, `localScale`, `forward`, `right`, `up`.
- `transform.Translate()` moves in local or world space; `transform.Rotate()` rotates; `transform.LookAt()` points at a target.

```csharp
Vector3 direction = target.position - transform.position;
direction.y = 0f;                          // keep it horizontal
transform.rotation = Quaternion.LookRotation(direction);
transform.position += direction.normalized * speed * Time.deltaTime;
```

### 4.6 Time: `deltaTime`, `fixedDeltaTime`, `timeScale`

| Property | Meaning |
|---|---|
| `Time.deltaTime` | Seconds since the previous **frame** — use in `Update` |
| `Time.fixedDeltaTime` | Fixed physics step (default 0.02 s) — use in `FixedUpdate` |
| `Time.unscaledDeltaTime` | Like `deltaTime` but ignores `timeScale` — use for pause menus |
| `Time.time` | Seconds since the game started |
| `Time.timeScale` | Global speed multiplier; `0` pauses; `0.5` slow motion |
| `Time.frameCount` | Number of frames rendered |

```csharp
// Frame-rate independent movement
transform.Translate(Vector3.forward * speed * Time.deltaTime);

// Physics force
rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
```

Without `Time.deltaTime`, an object moves `speed` units **per frame** — twice as fast at 120 FPS as at 60 FPS. With it, the speed is **units per second** on every machine.

### 4.7 Input

**Old Input Manager (classic):**

```csharp
float h = Input.GetAxis("Horizontal");      // smoothed -1..1
float v = Input.GetAxisRaw("Vertical");     // -1, 0, 1
bool jump = Input.GetButtonDown("Jump");
bool fire = Input.GetButton("Fire1");
Vector3 mouse = Input.mousePosition;
float mx = Input.GetAxis("Mouse X");        // per-frame mouse delta
```

**New Input System (package):**

```csharp
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public void OnMove(InputValue value)
    {
        Vector2 move = value.Get<Vector2>();
        // use move.x, move.y
    }

    public void OnJump(InputValue value)
    {
        if (value.isPressed) Jump();
    }
}
```

Input System pros: action maps, rebinding, multiple devices, better XR/controller support. Cons: more setup, different API.

### 4.8 Coroutines

A coroutine pauses and resumes across frames.

```csharp
using System.Collections;
using UnityEngine;

public class Fader : MonoBehaviour
{
    IEnumerator FadeOut()
    {
        for (float t = 1f; t > 0f; t -= Time.deltaTime)
        {
            // set alpha = t
            yield return null;              // resume next frame
        }
    }

    void Start()
    {
        StartCoroutine(FadeOut());
    }
}
```

- `yield return null` waits one frame.
- `yield return new WaitForSeconds(1f)` waits scaled time; `WaitForSecondsRealtime` ignores `timeScale`.
- `yield return new WaitUntil(() => condition)` waits for a condition.
- `StopCoroutine` / `StopAllCoroutines` cancel.
- Coroutines are not threads; they run on the main thread.

### 4.9 Script communication

| Technique | When to use |
|---|---|
| `GetComponent<T>()` | Direct reference to a component on the same object |
| Serialized reference (`public Transform target`) | Link objects in the Inspector |
| `FindObjectOfType<T>()` / `FindFirstObjectByType<T>()` | Rare, slow; avoid in `Update` |
| `SendMessage()` | Legacy, string-based, slow; avoid |
| C# events / `UnityEvent` | Decoupled communication (health changed, score updated) |
| ScriptableObject event channels | Global events without singletons |
| Singleton / service locator | One global manager (use sparingly) |

```csharp
public class Health : MonoBehaviour
{
    public event System.Action<int> OnDamaged;

    public void TakeDamage(int amount)
    {
        OnDamaged?.Invoke(amount);
    }
}

public class HealthBar : MonoBehaviour
{
    public Health health;

    void OnEnable()  => health.OnDamaged += UpdateBar;
    void OnDisable() => health.OnDamaged -= UpdateBar;

    void UpdateBar(int amount) { /* update UI */ }
}
```

Always **unsubscribe** in `OnDisable`/`OnDestroy` to avoid leaks and callbacks on destroyed objects.

### 4.10 Common script errors

| Error | Cause | Fix |
|---|---|---|
| "Can't add script component" | File name ≠ class name, or compile error | Match names, fix errors |
| `NullReferenceException` | Unassigned reference, destroyed object | Assign in Inspector, null-check |
| Variable not in Inspector | Not `public`, not `[SerializeField]` | Make public or serialize |
| Object moves faster on a better PC | Missing `Time.deltaTime` | Multiply by `deltaTime` |
| Jittery physics | Moving a Rigidbody in `Update` / setting `transform.position` | Use `FixedUpdate` + `rb.MovePosition` / `AddForce` |
| Input missed | `GetButtonDown` in `FixedUpdate` | Read input in `Update`, apply in `FixedUpdate` |
| Script runs before another | Execution order undefined | Set Script Execution Order or use `Awake`/events |
| Values reset after Play | Play-mode changes are not saved | Edit in Edit mode or copy component values |

---

## 5. Player movement

### 5.1 Coordinate spaces

| Space | Meaning | Unity API |
|---|---|---|
| **Local / object** | Relative to the object's own origin and axes | `transform.localPosition`, `transform.forward` |
| **World** | Global scene coordinates | `transform.position`, `Vector3.forward` |
| **Camera / view** | Relative to the camera | `Camera.main.transform`, `worldToCameraMatrix` |
| **Screen** | Pixels on the display | `Camera.main.WorldToScreenPoint` |
| **Viewport** | Normalised 0–1 screen coordinates | `Camera.main.WorldToViewportPoint` |

Convert directions with `transform.TransformDirection(localDir)` (local → world) and `transform.InverseTransformDirection(worldDir)` (world → local). Convert points with `TransformPoint` / `InverseTransformPoint`.

### 5.2 Three ways to move a character

| Method | Physics / collision | Best for |
|---|---|---|
| `transform.Translate` / `transform.position` | None (can pass through walls) | Simple kinematic objects, cameras, non-physical props |
| **Rigidbody** (`AddForce`, `velocity`, `MovePosition`) | Full physics, collisions, gravity | Physical objects, vehicles, ragdolls, thrown props |
| **CharacterController** (`Move`) | Capsule collision, slopes, steps; no automatic physics | First-person and third-person characters |

**Transform movement:**

```csharp
void Update()
{
    float h = Input.GetAxis("Horizontal");
    float v = Input.GetAxis("Vertical");
    Vector3 dir = new Vector3(h, 0f, v).normalized;
    transform.Translate(dir * speed * Time.deltaTime, Space.World);
}
```

**Rigidbody movement:**

```csharp
public class RigidbodyMove : MonoBehaviour
{
    public float speed = 6f;
    public float jumpForce = 5f;
    private Rigidbody rb;

    void Awake() => rb = GetComponent<Rigidbody>();

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 dir = new Vector3(h, 0f, v).normalized;
        rb.MovePosition(rb.position + dir * speed * Time.fixedDeltaTime);

        if (Input.GetButtonDown("Jump"))
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}
```

**CharacterController movement:**

```csharp
public class ControllerMove : MonoBehaviour
{
    public float speed = 5f;
    public float gravity = -20f;
    public float jumpHeight = 1.2f;

    private CharacterController cc;
    private Vector3 velocity;
    private bool grounded;

    void Awake() => cc = GetComponent<CharacterController>();

    void Update()
    {
        grounded = cc.isGrounded;
        if (grounded && velocity.y < 0f) velocity.y = -2f;   // stick to ground

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 move = transform.right * h + transform.forward * v;
        cc.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && grounded)
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        velocity.y += gravity * Time.deltaTime;
        cc.Move(velocity * Time.deltaTime);
    }
}
```

### 5.3 First-person vs third-person controller

| Basis | **First Person (FPC)** | **Third Person (TPC)** |
|---|---|---|
| Camera | At the character's eyes | Behind and above the character |
| Avatar | Usually invisible (or arms only) | Fully visible |
| Input | WASD + mouse look | WASD relative to camera; mouse orbits |
| Immersion | High | Medium |
| Spatial awareness | Lower (no view of body) | Higher (see surroundings and character) |
| Aiming | Direct crosshair | Indirect / over-shoulder |
| Motion sickness | Higher in VR | Lower in VR (if camera is stable) |
| Best for | Shooters, horror, simulation, VR | Platformers, action-adventure, social VR |

### 5.4 Mouse look (FPS camera)

```csharp
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public Transform playerBody;        // yaw goes on the body
    public float sensitivity = 2.5f;
    public float minPitch = -85f;
    public float maxPitch = 85f;

    private float pitch;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        // Yaw: rotate the whole body around world Y
        playerBody.Rotate(Vector3.up * mouseX);

        // Pitch: rotate only the camera around local X, clamped
        pitch = Mathf.Clamp(pitch - mouseY, minPitch, maxPitch);
        transform.localRotation = Quaternion.Euler(pitch, 0f, 0f);
    }
}
```

Why split yaw and pitch?

- Yaw on the **body** turns the movement direction too.
- Pitch on the **camera** lets the player look up/down without tilting the body.
- Clamping pitch prevents the camera from flipping over at ±90°.
- Roll stays 0 for a normal FPS camera.

> **Nuance:** `Input.GetAxis("Mouse X")` is already a **per-frame delta**. Do **not** multiply it by `Time.deltaTime` for direct mouse look — that makes sensitivity depend on frame rate. Use `deltaTime` for smoothing/acceleration, not for the raw delta. (Some course slides multiply by `deltaTime`; in an exam, state your convention.)

### 5.5 A complete movement model (momentum + gravity + jump)

A good character controller is not "position += input * speed". It has:

1. **Input** — read axes, build a direction, **normalise** so diagonal is not faster.
2. **Local → world** — WASD is relative to where the player faces; convert once.
3. **Acceleration** — velocity approaches the target gradually (momentum).
4. **Friction** — when there is no input and the player is grounded, velocity decays.
5. **Gravity** — vertical velocity decreases every frame; clamp to terminal velocity.
6. **Jump** — set vertical velocity to a positive value, not add; mark not grounded.
7. **Collision** — `CharacterController.Move` resolves walls, floors, steps.
8. **Air control** — reduced acceleration while airborne, but keep horizontal momentum.
9. **Slope / step handling** — `slopeLimit`, `stepOffset`, `skinWidth`.
10. **Camera** — follow in `LateUpdate`, avoid clipping through walls.

The full code is in §9 (Advanced 3D movement).

### 5.6 VR locomotion

VR player movement is a **comfort problem**, not just a coding problem. The vestibular system feels no acceleration when the screen moves, so smooth motion can cause sickness.

| Technique | How it works | Comfort | Notes |
|---|---|---|---|
| **Room-scale walking** | User physically walks; tracking maps real motion | Best | Limited by play area; guardian boundary |
| **Teleport** | Point a ray, release to jump to the spot | Very comfortable | Breaks continuous motion; use fade/blink |
| **Smooth locomotion** | Joystick moves the camera continuously | Can be uncomfortable | Add vignette, slow acceleration, avoid strafing |
| **Snap turn** | 30°/45° instant rotation | Comfortable | Avoid smooth turning for new users |
| **Arm swinger** | Swing controllers to "walk" | Moderate | Adds proprioceptive feedback |
| **Redirected walking** | Subtly curve the virtual path to keep the user in the play area | High | Requires space and careful tuning |
| **Seated mode** | User sits; cockpit/vehicle metaphor | High | Good for simulators |

Implementation notes:

- Use an **XR Origin** (`XROrigin` / `XRRig`) with a **Camera Offset**; move the origin, not the camera directly.
- Keep the **head tracking** authoritative; never override the camera's local pose with gameplay animation.
- Use **CharacterController** or a **Rigidbody** on the origin for collisions; do not let the headset pass through walls.
- Add a **comfort vignette** during smooth motion; fade to black on teleport.
- Provide **snap turn** and **teleport** as defaults; make smooth locomotion optional.
- Respect the **guardian / boundary**; warn before the user hits a wall.
- Avoid **forced camera motion** (shakes, cutscenes, head bobbing) in VR.

---

## 6. Designing levels

### 6.1 What level design is

**Level design** is the craft of building the **space, flow, challenge and pacing** of a game. It combines architecture, gameplay, art, lighting, audio and UX. A level tells the player where to go, what to do, and what is dangerous — without a tutorial paragraph.

### 6.2 The level-design pipeline

```
Concept → Paper map / mood board → Greybox (blockout) → Playtest → Iterate
        → Art pass (models, materials, lighting) → Optimization → Final test
```

1. **Concept** — what is the level for? Tutorial, combat, puzzle, hub, boss?
2. **Paper map** — top-down sketch of routes, rooms, objectives and sightlines.
3. **Greybox / blockout** — build with simple primitives (cubes, planes, ramps) to test **scale, flow and gameplay** before art.
4. **Playtest** — can players find the exit? Is the jump possible? Is the cover usable?
5. **Iterate** — move walls, widen doors, add landmarks, remove confusion.
6. **Art pass** — replace greyboxes with models, materials, props, decals, vegetation.
7. **Lighting** — bake or real-time; guide the eye; create mood; ensure readability.
8. **Optimization** — occlusion culling, LODs, batching, lightmap resolution, draw distance.
9. **Final test** — on target hardware (especially the VR headset / phone).

### 6.3 Scale and metrics (critical in VR)

VR is judged at **human scale**; a door that is 1.2 m tall feels wrong immediately.

| Real-world element | Approximate size |
|---|---|
| Eye height (standing) | 1.6–1.7 m |
| Eye height (seated) | 1.1–1.2 m |
| Door width / height | 0.8–0.9 m / 2.0–2.1 m |
| Step height | 0.15–0.20 m |
| Stair riser / tread | 0.17 m / 0.28 m |
| Table height | 0.75 m |
| Counter height | 0.9 m |
| Chair seat height | 0.45 m |
| Corridor width (single) | 1.0–1.2 m |
| Reach (comfortable) | 0.5–0.7 m from shoulder |
| Comfortable interaction distance | 0.3–0.8 m (direct), 1.5–5 m (ray) |

Unity convention: **1 unit = 1 metre**. Keep the player's `CharacterController` height around 1.8 m with the camera at ~1.6 m.

### 6.4 Principles of good level design

- **Affordances** — a ledge looks climbable, a door looks openable, a red barrel looks explosive.
- **Landmarks** — distinct silhouettes and colours help navigation.
- **Sightlines** — show the objective, or hide it deliberately; use framing.
- **Guidance without hand-holding** — lighting, composition, leading lines, audio, enemy placement.
- **Pacing** — alternate tension and rest; vary intensity.
- **Choice** — multiple routes, risk/reward, optional areas.
- **Gating** — locks, keys, abilities, one-way drops.
- **Readability** — the player can tell what is interactive, what is dangerous, and where the floor is.
- **Composition** — rule of thirds, contrast, focal points, colour hierarchy.
- **Loop** — teach, test, twist, reward.

### 6.5 Lighting and atmosphere

- **Directional light** for the sun; **point/spot lights** for interiors; **area lights** for soft illumination.
- **Baked lighting** (lightmaps) for static geometry — cheap and beautiful, but no dynamic changes.
- **Real-time lighting** for moving objects and dynamic time of day — expensive.
- **Mixed lighting** combines both.
- **Reflection probes** for shiny surfaces; **light probes** for dynamic objects.
- **Post-processing**: bloom, colour grading, vignette, depth of field, motion blur (avoid motion blur in VR).
- **Fog** for depth and mood; can hide draw distance.
- **Contrast** matters more than brightness: players must see paths, ledges and hazards.

### 6.6 Navigation and gameplay space

- **NavMesh** for AI pathfinding (`Window → AI → Navigation`); bake walkable areas, add off-mesh links for jumps/ladders.
- **Cover** at player height; **choke points** for combat; **arenas** for bosses.
- **Verticality** — platforms, balconies, vents; in VR, be careful with heights and drops.
- **Traversal** — doors, lifts, moving platforms, teleporters.
- **Checkpoints** and **respawn** — respect the player's time.
- **Audio zones** — reverb, ambience, music transitions.

### 6.7 Optimization

| Technique | What it saves |
|---|---|
| **Frustum culling** | Objects outside the camera view are not drawn (automatic). |
| **Occlusion culling** | Objects hidden behind walls are not drawn (bake). |
| **LOD Group** | Distant objects use fewer triangles. |
| **Static batching** | Combines static meshes into fewer draw calls. |
| **Dynamic batching** | Combines small dynamic meshes (limited). |
| **GPU instancing** | One draw call for many copies of the same mesh/material. |
| **Texture atlases** | Fewer material switches. |
| **Lightmap resolution** | Lower texel density on unimportant surfaces. |
| **Draw distance / fog** | Fewer visible objects. |
| **Object pooling** | Avoids instantiate/destroy spikes. |

### 6.8 VR-specific level design

1. **Comfort first** — no forced camera motion, no head bob, no sudden acceleration.
2. **Human scale** — doors, furniture, stairs and props must match real dimensions.
3. **Readable interaction** — highlight grabbable objects, show controller hints, use consistent colours.
4. **Reach zones** — place important objects between waist and chest height, within 0.5–0.7 m.
5. **Teleport anchors** — design clear landing spots; avoid teleporting into walls or off ledges.
6. **Play area** — respect the guardian; keep critical objects inside the tracked space.
7. **Seated option** — design a seated mode for users who cannot stand.
8. **Avoid stairs/slopes** for smooth locomotion; use ramps, lifts or teleport.
9. **Performance** — 90/120 FPS, stereo rendering, high resolution; every draw call counts.
10. **Accessibility** — comfort options, subtitles, colour-blind palettes, one-handed mode, adjustable height.

### 6.9 AR-specific level design

- **Plane detection** — place content on horizontal/vertical surfaces; show a reticle.
- **Anchors** — keep virtual objects stable as the phone moves.
- **Scale** — AR content is often table-top; design for 0.2–2 m viewing distance.
- **Occlusion** — virtual objects should be hidden by real objects where possible.
- **Lighting estimation** — match virtual light direction/intensity to the room.
- **Markers/images** — design trackable images with high contrast and detail.
- **Safety** — users walk around while looking at a phone; avoid requiring them to stare at the screen while moving.

---

## 7. Menu and UI

### 7.1 Unity UI systems

| System | Status | Use |
|---|---|---|
| **uGUI** (Unity UI) | Mature, built-in | Canvas-based menus, HUD, world-space UI; most VR UI |
| **UI Toolkit** | Modern, recommended for editor/runtime | Complex data-driven UI, responsive layouts |
| **IMGUI** | Legacy | Editor tools, quick debug UI |
| **TextMeshPro** | Essential add-on | Crisp text at any distance; SDF rendering |

### 7.2 Canvas fundamentals

A **Canvas** is the root of a uGUI hierarchy. It controls how UI is rendered.

| Render Mode | Behaviour | Best for |
|---|---|---|
| **Screen Space – Overlay** | UI drawn on top of everything, in screen pixels | Normal 2D HUD/menus |
| **Screen Space – Camera** | UI placed at a distance from a camera, still screen-sized | UI that can be affected by camera settings |
| **World Space** | UI lives in the 3D world with a Transform | VR/AR menus, in-world panels, health bars over characters |

**Canvas Scaler** controls resolution adaptation:

- `Constant Pixel Size` — fixed pixels; UI size changes with resolution.
- `Scale With Screen Size` — reference resolution; UI scales proportionally (most common).
- `Constant Physical Size` — uses physical units (cm/inches).

**EventSystem** is required for interaction: it routes clicks, touches, gaze and controller input to UI elements. It needs an **Input Module** (Standalone Input Module, Input System UI Input Module, XR UI Input Module).

**Graphic Raycaster** turns UI graphics into raycast targets. Without it, buttons cannot be clicked.

### 7.3 Building a menu

```
Canvas (Screen Space - Overlay, Canvas Scaler: Scale With Screen Size)
├── EventSystem (Input Module)
├── Background (Image)
├── Title (TextMeshProUGUI)
├── ButtonsPanel (Vertical Layout Group)
│   ├── PlayButton (Button + TMP text)
│   ├── SettingsButton
│   └── QuitButton
└── SettingsPanel (inactive by default)
```

**Anchors and pivots** keep UI in the right place on different aspect ratios. Anchor a corner element to that corner; stretch a background to fill the canvas; use `Content Size Fitter` and `Layout Group` for automatic sizing.

**Button handler:**

```csharp
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject settingsPanel;

    public void PlayGame()
    {
        SceneManager.LoadScene("Level01");
    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
```

Wire the button's `OnClick()` in the Inspector, or add the listener in code.

### 7.4 HUD design

- **Minimal** — only what the player needs right now: health, ammo, objective, compass, interaction prompt.
- **Consistent placement** — health bottom-left, ammo bottom-right, objective top-left, crosshair centre (genre conventions).
- **Contrast** — use outlines, shadows or panels so text is readable over any background.
- **Scale** — test at 1080p, 4K and in the headset.
- **Colour-blind safe** — do not rely on red/green alone; add icons/shapes.
- **Subtitles** — size, background, speaker labels, on/off option.

### 7.5 VR UI design

VR UI is **spatial**: it lives in 3D space, at a comfortable distance and angle.

| Pattern | How it works | Best for |
|---|---|---|
| **World-space panel** | A Canvas in the scene, usually curved, at 1.5–3 m | Menus, dashboards, instructions |
| **Laser pointer** | Controller ray + line renderer + hit point | Distant UI, large menus |
| **Gaze + click / dwell** | Look at a button for a set time or press a button | Hands-free, accessibility |
| **Direct hand poke** | Finger touches a physical-looking button | Close-up, tactile UI |
| **Diegetic UI** | UI exists in the world (wristwatch, cockpit, sign) | Immersion |
| **Non-diegetic UI** | Floating overlay (health bar, subtitle) | Essential information |
| **Head-locked UI** | Attached to the camera | Avoid except tiny reticles; can cause discomfort |

VR UI rules:

1. **Distance** — 1.5–3 m for menus; 0.3–0.8 m for direct interaction.
2. **Angular size** — text should subtend enough angle to read; minimum ~1° of visual angle for body text.
3. **Curvature** — a slight cylindrical curve keeps all points equidistant from the eye.
4. **Contrast** — dark panel with light text (or vice versa); avoid thin fonts.
5. **Feedback** — hover, press, click sound, haptic pulse.
6. **No head-locked menus** — they feel claustrophobic and cause discomfort.
7. **Comfort** — do not move the UI with the head; let the user's gaze move.
8. **Accessibility** — seated height, one-handed mode, adjustable scale, subtitles.
9. **Performance** — one canvas per panel; avoid many small canvases; use TMP.
10. **Ray length** — 1–10 m; show a clear cursor; highlight the hit object.

**XR Interaction Toolkit ray interactor (concept):**

```csharp
// Attach XRRayInteractor to the controller.
// It emits a ray, hits UI / interactables, and sends select events.
// Add XRInteractorLineVisual for the visible beam and XRRayInteractor reticle.
```

**Gaze dwell button (concept):**

```csharp
public class GazeButton : MonoBehaviour
{
    public float dwellTime = 1.5f;
    private float timer;
    private bool gazing;

    public void OnGazeEnter() => gazing = true;
    public void OnGazeExit() { gazing = false; timer = 0f; }

    void Update()
    {
        if (!gazing) return;
        timer += Time.deltaTime;
        if (timer >= dwellTime)
        {
            timer = 0f;
            GetComponent<UnityEngine.UI.Button>().onClick.Invoke();
        }
    }
}
```

### 7.6 AR UI design

- **Screen-space UI** is normal for phones: buttons, sliders, instructions.
- **World-anchored labels** sit next to real objects; keep them readable and avoid clutter.
- **Touch input** is primary; support tap, drag, pinch-to-scale and two-finger rotate.
- **Safe areas** — avoid notches and rounded corners (`Screen.safeArea`).
- **Occlusion** — labels behind real objects should fade or hide.
- **Battery/performance** — UI updates every frame are expensive on mobile; cache references.

### 7.7 Menu flow and state

```
Boot → Main Menu → [Play → Level → Pause → Resume/Options/Quit]
                 → [Options → Audio/Video/Controls/Accessibility → Back]
                 → [Credits]
```

- Use a **GameManager** or **UI state machine** to avoid overlapping menus.
- Pause with `Time.timeScale = 0` and `AudioListener.pause = true`; use `unscaledDeltaTime` for the pause menu animation.
- Remember the **last selected button** for controller/gamepad navigation.
- Support **Back** (Escape/B) consistently.

---

## 8. Camera movement

### 8.1 The camera component

| Property | Meaning |
|---|---|
| **Projection** | Perspective (3D) or Orthographic (2D/isometric) |
| **Field of View (FOV)** | Vertical viewing angle; 60° is common, 90–110° in VR |
| **Near / Far Clipping Plane** | Anything closer than near or farther than far is not rendered |
| **Culling Mask** | Which layers the camera renders |
| **Depth** | Render order for multiple cameras |
| **Target Texture** | Render to a texture (mirrors, security cameras, UI) |
| **HDR / MSAA** | High dynamic range / anti-aliasing |
| **Physical Camera** | Focal length, sensor size, aperture (cinematic) |

Camera basics:

- The camera is the **eye** of the viewer. Its transform defines position and orientation.
- The **view matrix** transforms world space into camera space; the **projection matrix** transforms camera space into clip space.
- `Camera.main` finds the camera tagged `MainCamera` (cache it; `Camera.main` is a lookup).
- `WorldToScreenPoint` / `ScreenToWorldPoint` convert between spaces.

### 8.2 Camera styles

| Style | Description | Use |
|---|---|---|
| **First-person** | Camera at the character's eyes | FPS, horror, simulation |
| **Third-person follow** | Camera behind/above, follows with smoothing | Action-adventure, platformers |
| **Orbit** | Camera rotates around a target | Character viewers, strategy, product showcases |
| **Top-down / isometric** | Fixed angle, often orthographic | RTS, ARPG, city builders |
| **Rail / spline** | Camera follows a predefined path | Cutscenes, on-rails shooters |
| **Free / fly** | WASD + mouse, no character | Editor, strategy, cinematics |
| **Shoulder** | Offset behind one shoulder | Cover shooters |
| **Cockpit** | Inside a vehicle | Racing, flight, mech sims |

### 8.3 Smooth follow camera

```csharp
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0f, 3f, -6f);
    public float followSpeed = 8f;
    public float rotationSpeed = 10f;

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPos = target.position + target.rotation * offset;
        transform.position = Vector3.Lerp(transform.position, desiredPos,
                                          1f - Mathf.Exp(-followSpeed * Time.deltaTime));

        Quaternion desiredRot = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRot,
                                               1f - Mathf.Exp(-rotationSpeed * Time.deltaTime));
    }
}
```

Why `LateUpdate`? The target moves in `Update`; the camera must read the **final** position after all gameplay updates, or it lags one frame and jitters.

Why `1 - exp(-k·dt)`? It is a **frame-rate-independent** smoothing factor. A plain `Lerp(a, b, 0.1f)` is faster at high frame rates because it is applied more often.

### 8.4 Camera collision

A third-person camera can clip through walls. Cast from the target to the desired camera position and pull the camera in if something is hit.

```csharp
public class CameraCollision : MonoBehaviour
{
    public Transform target;
    public float distance = 6f;
    public LayerMask collisionMask;
    public float collisionRadius = 0.3f;

    void LateUpdate()
    {
        Vector3 dir = (transform.position - target.position).normalized;
        if (Physics.SphereCast(target.position, collisionRadius, dir,
                               out RaycastHit hit, distance, collisionMask))
        {
            transform.position = target.position + dir * (hit.distance - 0.1f);
        }
    }
}
```

### 8.5 Camera shake

```csharp
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float duration = 0.3f;
    public float magnitude = 0.2f;

    private float timer;
    private Vector3 originalLocalPos;

    void Awake() => originalLocalPos = transform.localPosition;

    public void Shake() => timer = duration;

    void LateUpdate()
    {
        if (timer <= 0f)
        {
            transform.localPosition = originalLocalPos;
            return;
        }

        timer -= Time.deltaTime;
        float t = timer / duration;
        transform.localPosition = originalLocalPos +
            Random.insideUnitSphere * magnitude * t;
    }
}
```

Use **Perlin noise** for smoother shake; use **trauma-based** shake (magnitude = trauma²) for better feel. **Do not shake the camera in VR** — it causes discomfort; shake the controller/haptics or the world instead.

### 8.6 Cinemachine

Cinemachine is Unity's procedural camera system:

- **Virtual Cameras** define shots (position, aim, FOV, noise, confiner).
- **Brain** blends between virtual cameras with configurable blends.
- **Follow** and **Aim** components (Transposer, Framing Transposer, Composer, Orbital Transposer).
- **Noise** for handheld shake; **Impulse** for hits/explosions.
- **Confiner** keeps the camera inside a polygon/collider.
- **Clear Shot** picks the best camera with line of sight.

Typical setup: a `CinemachineBrain` on the Main Camera, a `CM vcam` following the player, a second vcam for a cutscene, and a blend when switching.

### 8.7 VR camera specifics

- **The headset owns the camera pose.** Do not animate the camera's local position/rotation; animate the **XR Origin** instead.
- **Stereo rendering:** each eye has a slightly different view; the projection matrices are asymmetric. Use **Single Pass Instanced** for performance.
- **IPD:** interpupillary distance (~63 mm average); the headset reports it; do not hard-code it.
- **FOV:** VR needs a wide FOV (~90–110° per eye); a narrow FOV breaks presence.
- **Refresh rate:** 90 Hz minimum (Quest 2/3, Index); 120 Hz on some devices. Missed frames cause judder and sickness.
- **Late latching / reprojection:** the runtime adjusts the image to the latest head pose; keep frame time low.
- **No camera shake, no head bob, no motion blur, no forced cutscene camera.**
- **Comfort:** stable horizon, no acceleration, optional vignette during movement.
- **Tracking origin:** floor-level vs eye-level; set it correctly or the player will be a giant or a dwarf.

---

## 9. Advanced 3D movement

This section expands the momentum-based first-person controller from the course slides. It is the model behind the `Advanced 3D movement` syllabus line.

### 9.1 Goals

- Move with **WASD** relative to where the player faces.
- Build **momentum**; do not stop instantly.
- Apply **friction** only when grounded and not actively accelerating.
- Apply **gravity**; support **jumping**.
- Keep **air control** reduced but present.
- Handle **collisions**, slopes and steps.
- Keep **one world-space velocity** so jumping and turning do not change direction mid-air.

### 9.2 Local vs world space — the key insight

WASD is **local** to the player: W means "forward relative to my facing". `CharacterController.Move` expects a **world-space** displacement.

If you store velocity in local space, then jump forward, spin 180° in the air, and the velocity rotates with you — you suddenly fly backward. That is wrong. The fix:

1. Read input locally.
2. Convert **once** to world space with `transform.TransformDirection`.
3. Store **worldVelocity** and never rotate it with the player.

```csharp
Vector3 inputLocal = new Vector3(h, 0f, v);
Vector3 inputWorld = transform.TransformDirection(inputLocal.normalized);
```

### 9.3 Dot product — detect reversing direction

`Vector3.Dot(a, b)` returns:

- `1` if the vectors point the same way,
- `0` if perpendicular,
- `-1` if opposite.

Use it to give **extra acceleration when the player reverses** (so turning around feels responsive instead of sliding):

```csharp
float dot = Vector3.Dot(currentVelocity.normalized, targetDirection);
if (dot < 0f)
    acceleration *= reverseMultiplier;   // e.g. 2x
```

### 9.4 Clamp magnitude — limit speed correctly

`Vector3.ClampMagnitude(v, maxSpeed)` limits the **length** of the vector. Clamping X and Z separately gives a **square** speed limit (diagonal is faster); clamping the magnitude gives a **circular** limit.

```csharp
worldVelocity = Vector3.ClampMagnitude(worldVelocity, maxSpeed);
```

If an external force (explosion, moving platform) leaves the player above max speed, clamp to the **current** magnitude so they are not instantly slowed:

```csharp
if (worldVelocity.magnitude > maxSpeed)
    worldVelocity = Vector3.ClampMagnitude(worldVelocity, worldVelocity.magnitude);
```

### 9.5 Friction and snap-to-zero

Friction acts **opposite** to velocity, only when:

- the player is grounded, and
- there is no input **or** the player is above max speed.

```csharp
Vector3 friction = -worldVelocity.normalized * frictionAmount * Time.deltaTime;
worldVelocity += friction;

// Snap to zero: if friction would overshoot, set exactly zero.
if (worldVelocity.magnitude < 0.05f)
    worldVelocity = Vector3.zero;
```

Without snap-to-zero, friction can flip the velocity negative and the player jitters back and forth.

### 9.6 Gravity, jump and ground check

```csharp
if (!grounded)
    yVelocity += gravity * Time.deltaTime;   // gravity is negative
else if (yVelocity < 0f)
    yVelocity = -2f;                          // small downward force keeps contact

if (Input.GetButtonDown("Jump") && grounded)
{
    yVelocity = jumpPower;                    // SET, not +=
    grounded = false;                         // leave the ground immediately
}
```

Why `=` and not `+=`? If you add to the current downward velocity, a falling player's jump is weaker. Set the jump velocity explicitly.

Why `grounded = false` immediately? Otherwise the next frame still thinks you are grounded and resets `yVelocity`, cancelling the jump.

### 9.7 Collision flags

`CharacterController.Move` returns `CollisionFlags`:

| Flag | Meaning | Response |
|---|---|---|
| `Below` | Hit something below | Grounded = true; zero downward velocity |
| `Sides` | Hit a wall | Slide along it; optionally bounce |
| `Above` | Hit a ceiling | Zero upward velocity |

```csharp
CollisionFlags flags = controller.Move(displacement);
grounded = (flags & CollisionFlags.Below) != 0;

if ((flags & CollisionFlags.Above) != 0 && yVelocity > 0f)
    yVelocity = 0f;
```

### 9.8 Full controller code

```csharp
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class AdvancedPlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float maxSpeed = 6f;
    public float acceleration = 40f;
    public float airControl = 0.35f;
    public float reverseMultiplier = 2f;
    public float friction = 30f;

    [Header("Jump / Gravity")]
    public float jumpPower = 7f;
    public float gravity = -25f;
    public float maxGravity = -50f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundRadius = 0.25f;
    public LayerMask groundMask;

    private CharacterController controller;
    private Vector3 worldVelocity;   // horizontal, world space
    private float yVelocity;         // vertical
    private bool grounded;

    void Awake() => controller = GetComponent<CharacterController>();

    void Update()
    {
        // 1. Ground check
        grounded = Physics.CheckSphere(groundCheck.position, groundRadius, groundMask);
        if (grounded && yVelocity < 0f) yVelocity = -2f;

        // 2. Input (local) -> world
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 inputLocal = new Vector3(h, 0f, v);
        if (inputLocal.sqrMagnitude > 1f) inputLocal.Normalize();
        Vector3 inputWorld = transform.TransformDirection(inputLocal);

        // 3. Target horizontal velocity
        Vector3 targetVelocity = inputWorld * maxSpeed;

        // 4. Acceleration (extra when reversing)
        float accel = acceleration;
        if (worldVelocity.sqrMagnitude > 0.01f &&
            Vector3.Dot(worldVelocity.normalized, inputWorld) < 0f)
        {
            accel *= reverseMultiplier;
        }

        // 5. Move velocity toward target
        if (inputLocal.sqrMagnitude > 0.01f)
        {
            float control = grounded ? 1f : airControl;
            worldVelocity = Vector3.MoveTowards(
                worldVelocity, targetVelocity, accel * control * Time.deltaTime);
        }
        else if (grounded)
        {
            // 6. Friction when no input
            worldVelocity = Vector3.MoveTowards(
                worldVelocity, Vector3.zero, friction * Time.deltaTime);
        }

        // 7. Clamp speed
        worldVelocity = Vector3.ClampMagnitude(worldVelocity, maxSpeed);

        // 8. Jump
        if (Input.GetButtonDown("Jump") && grounded)
        {
            yVelocity = jumpPower;
            grounded = false;
        }

        // 9. Gravity
        yVelocity += gravity * Time.deltaTime;
        yVelocity = Mathf.Max(yVelocity, maxGravity);

        // 10. Combine and move
        Vector3 displacement = worldVelocity + Vector3.up * yVelocity;
        CollisionFlags flags = controller.Move(displacement * Time.deltaTime);

        if ((flags & CollisionFlags.Below) != 0) grounded = true;
        if ((flags & CollisionFlags.Above) != 0 && yVelocity > 0f) yVelocity = 0f;
    }
}
```

### 9.9 Slopes, steps and moving platforms

- **Slope limit** — `CharacterController.slopeLimit` (default 45°); steeper surfaces are walls.
- **Step offset** — `stepOffset` (default 0.3 m); lets the controller climb small steps.
- **Skin width** — small overlap that prevents jitter; keep ~10% of radius.
- **Moving platforms** — parent the player to the platform, or add the platform's velocity to the player's displacement each frame.
- **One-way platforms** — disable the collider when moving upward through it, re-enable when landing.
- **Ladders** — disable gravity, move along the ladder axis, exit at the top.
- **Water/swimming** — reduce gravity, add buoyancy and drag, change controls.

### 9.10 VR advanced movement

- Use the same momentum model, but **scale it down** and add comfort options.
- **Snap turn** instead of smooth yaw for new users.
- **Teleport** with a parabolic ray and a valid-landing check (NavMesh, slope, clearance).
- **Vignette** during smooth motion; fade on teleport.
- **Physical crouch** should be respected; do not force crouch animations.
- **Hand/controller tracking** is separate from locomotion; never let locomotion detach the hands from the body.
- **Collision** — the player's capsule should match the real body; the head should not pass through walls.
- **Comfort settings** — speed, turn mode, vignette strength, seated/standing, height calibration.

---

## 10. The Unity Asset Store

### 10.1 What it is

The **Unity Asset Store** is an online marketplace for reusable Unity assets: 3D models, textures, animations, audio, shaders, tools, templates, complete projects and XR packages. It lets small teams ship faster by buying or downloading work instead of building everything from scratch.

### 10.2 Types of assets

| # | Type | Examples / notes |
|---|---|---|
| 1 | **3D models** | Characters, vehicles, props, buildings, environments, vegetation; FBX/glTF; PBR materials; import scale and collider generation matter. |
| 2 | **2D art & textures** | Sprites, sprite sheets, UI skins, icons, tileable textures, normal maps. |
| 3 | **Audio** | Music, SFX, ambience, UI sounds, voice, 3D/ambisonic audio. |
| 4 | **Shaders, materials, VFX** | Shader Graph, URP/HDRP shaders, post-processing, particles, VFX Graph. |
| 5 | **Scripts, tools & editor extensions** | C# frameworks, custom inspectors, build tools, debugging/profiling, code generators. |
| 6 | **Animation** | Clips, humanoid/generic rigs, Animator controllers, blend trees, mocap. |
| 7 | **Physics** | Physics materials, joint presets, cloth, collision shapes. |
| 8 | **GUI / UI kits** | HUDs, menus, VR/AR UI kits, icons, fonts. |
| 9 | **VR/AR/XR tools** | XR Interaction Toolkit samples, OpenXR/Oculus/ARKit packages, hand tracking, gestures, teleport/locomotion, MRTK. |
| 10 | **Fonts & text** | Fonts, SDF fonts, 3D text. |
| 11 | **Templates & full projects** | Complete games, starter kits, boilerplates. |
| 12 | **Services & support** | Cloud Build, Analytics, Asset Store Tools, networking, AI/ML packages. |

### 10.3 Importing assets

1. **Download** from the Asset Store window (`Window → Asset Store`) or the website.
2. **Import** the `.unitypackage` through `Assets → Import Package → Custom Package`, or use the **Package Manager** for official packages (XR Interaction Toolkit, Input System, TextMeshPro, Cinemachine).
3. **Inspect** the imported folder: models, materials, textures, prefabs, demo scenes.
4. **Set import settings** — scale factor, `Read/Write`, `Generate Colliders`, material creation, texture compression, audio load type.
5. **Test performance** on the target device, especially VR/mobile.

### 10.4 Advantages

- **Speed** — production-ready models, animations and systems in minutes.
- **Quality** — professional art and code, often cheaper than hiring.
- **Variety** — styles from realistic to stylised, sci-fi to medieval.
- **Learning** — complete projects show how systems are built.
- **Prototyping** — greyboxing with free assets tests gameplay before custom art.
- **XR ecosystem** — official XR packages and samples accelerate VR/AR development.
- **Community** — ratings, reviews, forums and updates.

### 10.5 Limitations

- **Quality varies** — check reviews, screenshots, demos and compatibility.
- **Style mismatch** — mixing assets from different packs can look inconsistent.
- **Performance** — high-poly models, 4K textures and heavy shaders can destroy VR frame rate.
- **Licensing** — some assets are single-seat, team, or restricted; read the license.
- **Dependencies** — packages may require a specific Unity version or render pipeline.
- **Support** — some publishers abandon assets; no guarantee of updates.
- **Security** — only import trusted packages; code can run in the editor.
- **Bloat** — importing a huge pack adds unused files; clean up.

### 10.6 Asset Store best practices

1. **Check the Unity version and render pipeline** (Built-in / URP / HDRP) before buying.
2. **Read reviews** and look for recent updates.
3. **Test in a blank project** before integrating.
4. **Use a `ThirdParty` folder** to keep imported assets separate.
5. **Set up an asset budget** — triangle count, texture size, draw calls, audio memory.
6. **Prefer modular kits** for environments; they are easier to optimize.
7. **Use LODs and texture atlases** from the pack where available.
8. **Keep a license file** and record what was purchased.
9. **Bake lighting** after import; imported demos may use expensive real-time lights.
10. **Profile** on the target headset/phone, not just the editor.

### 10.7 Free alternatives and companions

- **Unity Learn / Starter Assets** — official tutorials and samples.
- **XR Interaction Toolkit samples** — hands, controllers, locomotion, UI.
- **Kenney.nl** — free low-poly game assets.
- **Quixel Megascans** (now part of Fab) — scanned materials and models.
- **Sketchfab** — downloadable models (check licenses).
- **OpenGameArt** — free 2D/3D/audio assets.
- **Blender** — create or modify models yourself.

---

# UNIT III — GEOMETRY OF VIRTUAL WORLDS

## 11. Geometry of virtual worlds

### 11.1 3D Euclidean space and coordinates

The virtual world is modelled as a **3D Euclidean space**. Every point has three coordinates:

$$
P = (x, y, z)
$$

- **x** — left/right
- **y** — up/down
- **z** — forward/back

A **vector** has direction and magnitude but no fixed position: $\vec{v} = (v_x, v_y, v_z)$. A **point** is a location. The difference of two points is a vector; adding a vector to a point gives another point.

**Coordinate systems used in the pipeline:**

| Space | Origin / axes | What lives here |
|---|---|---|
| **Object / local / model** | The object's own pivot | Vertices as modelled in Blender/Maya |
| **World** | Scene origin | All objects placed in the scene |
| **Camera / view / eye** | The camera, looking down its axis | What the camera sees |
| **Clip** | After projection | Homogeneous coordinates before divide |
| **NDC** (normalised device coordinates) | After perspective divide | x, y, z in a canonical range |
| **Screen / viewport** | Pixel grid | Final image |

**Handedness:**

- **Right-handed:** x right, y up, z **toward the viewer** (out of the screen). OpenGL, mathematics.
- **Left-handed:** x right, y up, z **into the screen**. Unity, DirectX.

The handedness changes the sign of some rotation and projection terms, but the concepts are identical. In an exam, state which convention you use.

### 11.2 Geometric models

A **geometric model** is a mathematical description of a shape that a computer can store, transform and render. It defines the **surface** (and sometimes the interior) of an object.

A model must answer:

1. **Where is the surface?** — vertices, curves, implicit equations.
2. **What does it look like?** — material, colour, texture, normal.
3. **How is it transformed?** — model matrix, skeleton, morph targets.
4. **How is it rendered efficiently?** — mesh, LOD, culling.

### 11.3 Geometric primitives

A **geometric primitive** is a **basic building-block shape** from which more complex models are constructed. Primitives are finite, simple and mathematically well-defined.

| Primitive | Dimensions | Unity equivalent | Typical use |
|---|---|---|---|
| **Point** | 0D | (vertex) | Particles, point clouds |
| **Line / segment** | 1D | Line Renderer | Wires, paths, debug rays |
| **Triangle** | 2D (planar) | Mesh triangle | Every rendered surface |
| **Quad** | 2D | Quad | Screens, cards, floors |
| **Polygon** | 2D | (triangulated) | Modelling base |
| **Cube / box** | 3D | Cube | Blockout, crates, walls |
| **Sphere** | 3D | Sphere | Balls, planets, LOD proxy |
| **Capsule** | 3D | Capsule | Characters, limbs, colliders |
| **Cylinder** | 3D | Cylinder | Pipes, pillars, barrels |
| **Cone** | 3D | (mesh) | Cones, spikes, spotlights |
| **Plane** | 2D surface | Plane | Ground, walls |
| **Torus** | 3D | (mesh) | Rings, tyres, donuts |

**Why primitives matter in VR modelling:**

- **Greyboxing / blockout** — build the level with cubes and planes to test scale and flow before art.
- **Colliders** — a box or capsule collider is far cheaper than a mesh collider.
- **LOD proxies** — a sphere or box can stand in for a complex object at a distance.
- **Procedural generation** — primitives can be combined and instanced at runtime.
- **Teaching / prototyping** — quick shapes to test interaction and physics.
- **Base meshes** — sculpt or subdivide a cube/sphere into a detailed model.

### 11.4 Triangle meshes — anatomy

A **triangle mesh** is a collection of triangles that together approximate a surface.

```
Vertices:  v0, v1, v2, v3, ...
Triangle:  (v0, v1, v2)
Mesh:      { vertices, triangles, normals, UVs, tangents, ... }
```

**Components of a mesh:**

| Component | Meaning |
|---|---|
| **Vertex** | A point in 3D space (position). |
| **Edge** | A line connecting two vertices. |
| **Face** | A closed loop of edges — usually a triangle. |
| **Index buffer** | Lists which vertices form each triangle (saves memory vs repeating vertices). |
| **Winding order** | Clockwise or counter-clockwise order of vertices; defines the **front face** (back-face culling). |
| **Normal** | Unit vector perpendicular to the surface; used for lighting. |
| **UV coordinates** | 2D texture coordinates per vertex. |
| **Tangent / bitangent** | Basis for normal mapping. |
| **Submesh** | A group of triangles with one material. |
| **Bounding volume** | AABB / sphere used for culling and collision. |

**Triangle strips and fans** reduce the index count by sharing vertices, but modern GPUs are optimized for indexed triangle lists, so strips are less important than they used to be.

**Doubly Connected Edge List (DCEL):** a mesh data structure that stores, for each edge, its two endpoints, its two adjacent faces, and the next/previous edges around each face. It makes adjacency queries (neighbours, boundary, traversal) efficient — useful for mesh editing, simplification and collision.

### 11.5 Why triangles are used

1. **Three points are always coplanar.** A triangle is guaranteed to be flat, so its normal is well-defined and shading is unambiguous.
2. **A triangle is always convex.** No self-intersection, no concave fill rules; rasterization is simple.
3. **Any polygon can be triangulated.** A quad, pentagon or arbitrary n-gon can be split into triangles (ear clipping, fan, etc.).
4. **GPUs are built for triangles.** The rasterization pipeline, hardware tessellation and vertex processing are optimized for triangle primitives.
5. **Affine interpolation is exact.** Barycentric coordinates interpolate UVs, colours, normals and depth linearly across a triangle.
6. **Simple, fast intersection tests.** Ray–triangle, triangle–triangle and sphere–triangle tests are well understood and cheap.
7. **Robust and unambiguous.** A triangle has a clear inside/outside, a single plane, and a well-defined area.
8. **Parallelism.** Millions of independent triangles can be processed in parallel by the GPU.
9. **Easy to transform.** Transforming the vertices transforms the whole mesh; edges and faces follow.
10. **Universal support.** Every engine, file format, GPU and modelling tool supports triangles.

**Limitations:**

- **Curved surfaces need many triangles** — a sphere is an approximation; smooth silhouettes require high tessellation.
- **File size and memory** grow with triangle count.
- **Aliasing / faceting** if normals are not smoothed.
- **Not ideal for CAD** — NURBS and parametric surfaces represent exact curves; meshes are approximations.
- **Thin geometry** can cause z-fighting and intersection artifacts.

> **Exam line:** *Triangles are the simplest planar, convex polygon; every polygon can be decomposed into them, and GPUs rasterize them directly. That is why VR models are triangle meshes, even though the underlying design may start as quads, NURBS or sculpted surfaces.*

### 11.6 Other geometric representations

| Representation | Description | Strengths | Weaknesses |
|---|---|---|---|
| **Polygon / triangle mesh** | Vertices + faces | Universal, fast, GPU-native | Approximates curves; memory |
| **Parametric surface** (Bézier, NURBS) | Control points + basis functions | Exact smooth curves; compact | Tessellation needed; complex |
| **Implicit surface** (sphere, plane, metaballs) | $f(x,y,z)=0$ | Exact, easy inside/outside test | Hard to render directly; blending |
| **Voxel grid** | 3D array of occupancy | Easy boolean ops, destruction | Memory-heavy; blocky |
| **Point cloud** | Set of points | Captures scanned detail | No connectivity; heavy |
| **CSG** (constructive solid geometry) | Boolean union/difference/intersection of primitives | Intuitive modelling | Expensive; topology issues |
| **Height map / terrain** | 2D grid of heights | Efficient large terrain | No overhangs |
| **Skeletal / skinned mesh** | Mesh + bones + weights | Animation | More complex pipeline |

### 11.7 VR modelling constraints

VR renders **twice** (stereo) at **90–120 Hz**, with a wide FOV and high resolution. The budget is tight:

- **Triangle count** — keep it low; use LODs and normal maps for detail.
- **Draw calls** — batch, instance, atlas; each draw call costs CPU.
- **Texture size** — 2K is often enough; 4K only for hero assets.
- **Overdraw** — transparent effects are expensive; avoid full-screen layers.
- **Shadows** — real-time shadows are costly; bake where possible.
- **Materials** — fewer unique materials per object; use atlases.
- **Level of detail** — swap to low-poly meshes at distance.
- **Occlusion culling** — bake and use it.
- **Single-pass stereo** — render both eyes in one pass.
- **Comfort** — stable frame time matters more than raw polygon count.

### 11.8 From model to screen — the whole pipeline

```
Vertices (local space)
   │  Model matrix  M
   ▼
World space
   │  View matrix  V
   ▼
Camera / eye space
   │  Projection matrix  P
   ▼
Clip space
   │  Perspective divide (÷ w)
   ▼
NDC
   │  Viewport transform
   ▼
Screen pixels
   │  Rasterization + depth test + shading
   ▼
Final image
```

---

## 12. Transforming models

### 12.1 Why transform?

A model is authored once in **local space**. To place it in a scene, animate it, attach it to a parent, or view it from a camera, we apply **transformations**:

- **Translation** — move.
- **Rotation** — turn.
- **Scale** — resize.
- **Shear** — slant.
- **Reflection** — mirror.

### 12.2 Rigid-body, affine and projective transformations

| Class | Preserves | Includes | Example |
|---|---|---|---|
| **Rigid body** (isometry) | Distances, angles, handedness | Translation + rotation | Moving a chair |
| **Similarity** | Angles, ratios of lengths | Rigid + uniform scale | Scaling a model uniformly |
| **Affine** | Parallel lines, ratios along a line | Translation, rotation, scale, shear, reflection | Non-uniform scaling, skew |
| **Projective** | Straight lines, cross-ratios | Affine + perspective | Camera projection |

A **rigid body** does not deform: its motion is **translation + rotation**. This is why a physics rigid body has mass, velocity and angular velocity but no shape change.

### 12.3 Homogeneous coordinates

We use **4×4 matrices** and **homogeneous coordinates** $(x, y, z, 1)$ so that translation can be a matrix multiplication.

- A 3×3 matrix can represent rotation, scale, shear and reflection, but **not translation**.
- A 4×4 matrix can represent all affine transformations, and a 4×4 projective matrix can represent perspective.
- The fourth component $w$ is 1 for points and 0 for directions (so translation does not affect directions).
- After projection, $w$ carries depth; dividing by $w$ produces perspective.

**Point:** $(x, y, z, 1)$ — translation applies.
**Direction / vector:** $(x, y, z, 0)$ — translation does not apply.

### 12.4 The basic matrices

**Translation** by $(t_x, t_y, t_z)$:

$$
T =
\begin{bmatrix}
1 & 0 & 0 & t_x \\
0 & 1 & 0 & t_y \\
0 & 0 & 1 & t_z \\
0 & 0 & 0 & 1
\end{bmatrix}
$$

**Scale** by $(s_x, s_y, s_z)$:

$$
S =
\begin{bmatrix}
s_x & 0 & 0 & 0 \\
0 & s_y & 0 & 0 \\
0 & 0 & s_z & 0 \\
0 & 0 & 0 & 1
\end{bmatrix}
$$

**Rotation about Z** by $\theta$:

$$
R_z(\theta) =
\begin{bmatrix}
\cos\theta & -\sin\theta & 0 & 0 \\
\sin\theta & \cos\theta & 0 & 0 \\
0 & 0 & 1 & 0 \\
0 & 0 & 0 & 1
\end{bmatrix}
$$

**Shear** in x by y:

$$
H =
\begin{bmatrix}
1 & k & 0 & 0 \\
0 & 1 & 0 & 0 \\
0 & 0 & 1 & 0 \\
0 & 0 & 0 & 1
\end{bmatrix}
$$

**Reflection** across the YZ plane (negate x):

$$
M =
\begin{bmatrix}
-1 & 0 & 0 & 0 \\
0 & 1 & 0 & 0 \\
0 & 0 & 1 & 0 \\
0 & 0 & 0 & 1
\end{bmatrix}
$$

### 12.5 Composition order

Matrix multiplication is **associative** but **not commutative**:

$$
(T \cdot R) \neq (R \cdot T)
$$

With **column vectors** and the convention $v' = M v$, transformations are applied **right to left**:

$$
v_{world} = T \cdot R \cdot S \cdot v_{local}
$$

means **scale first, then rotate, then translate**. This is the intuitive order: resize the object in its own space, spin it, then place it in the world.

With **row vectors** and $v' = v M$, the order is reversed: $v_{world} = v_{local} \cdot S \cdot R \cdot T$.

> **Always state your convention.** Unity uses column vectors with `Matrix4x4` multiplication `T * R * S`.

### 12.6 Inverse transformations

The inverse undoes a transformation:

- $T^{-1}(t) = T(-t)$
- $R^{-1}(\theta) = R(-\theta) = R^T(\theta)$ for a rotation
- $S^{-1}(s_x,s_y,s_z) = S(1/s_x, 1/s_y, 1/s_z)$
- $(A B)^{-1} = B^{-1} A^{-1}$

The **world-to-local** matrix is the inverse of the local-to-world matrix. Use it to convert a world-space hit point into the object's local space (e.g. to apply a decal or check a local bounds).

### 12.7 Transforming normals

Normals are **directions**, so translation does not affect them. But **non-uniform scale** changes the direction of a normal incorrectly if you use the same matrix.

Correct rule: transform normals by the **inverse transpose** of the upper-left 3×3:

$$
n' = (M^{-1})^T n
$$

Then **normalise** $n'$. For a pure rotation, the inverse transpose equals the rotation, so you can use $R$ directly. For uniform scale, the normal direction is unchanged (only length), so normalise. For non-uniform scale or shear, use the inverse transpose.

### 12.8 Unity transform API

| API | Meaning |
|---|---|
| `transform.position` | World position |
| `transform.localPosition` | Position relative to parent |
| `transform.rotation` | World rotation (Quaternion) |
| `transform.localRotation` | Rotation relative to parent |
| `transform.localScale` | Scale relative to parent |
| `transform.localToWorldMatrix` | Local → world 4×4 matrix |
| `transform.worldToLocalMatrix` | World → local 4×4 matrix |
| `transform.TransformPoint(p)` | Local point → world point |
| `transform.InverseTransformPoint(p)` | World point → local point |
| `transform.TransformDirection(d)` | Local direction → world direction |
| `transform.TransformVector(d)` | Local vector → world vector (includes scale) |
| `Matrix4x4.TRS(pos, rot, scale)` | Build a translation-rotation-scale matrix |
| `Matrix4x4.MultiplyPoint3x4(p)` | Apply a matrix to a point (no projection divide) |

**Worked example — TRS:**

Let $S = \text{diag}(2,2,1)$, $R = R_z(90°)$, $T = (3,4,0)$, and $v = (1,0,0)$.

1. Scale: $S v = (2,0,0)$.
2. Rotate 90° about Z: $(2,0,0) \to (0,2,0)$.
3. Translate: $(0,2,0) \to (3,6,0)$.

So $M = T R S$ maps $(1,0,0)$ to $(3,6,0)$.

---

## 13. 2D and 3D rotation — yaw, pitch, roll

### 13.1 2D rotation

A 2D rotation about the origin by angle $\theta$ (counter-clockwise) maps:

$$
\begin{aligned}
x' &= x\cos\theta - y\sin\theta \\
y' &= x\sin\theta + y\cos\theta
\end{aligned}
$$

Matrix form:

$$
R(\theta) =
\begin{bmatrix}
\cos\theta & -\sin\theta \\
\sin\theta & \cos\theta
\end{bmatrix}
$$

**Derivation:** the point $(1,0)$ rotates to $(\cos\theta, \sin\theta)$; the point $(0,1)$ rotates to $(-\sin\theta, \cos\theta)$. These are the **columns** of the matrix.

### 13.2 Properties of a valid rotation matrix

A 2×2 matrix $R = [c_1 \; c_2]$ is a rotation iff:

1. **Unit columns:** $\|c_1\| = \|c_2\| = 1$ — no stretching.
2. **Perpendicular columns:** $c_1 \cdot c_2 = 0$ — no shearing.
3. **Determinant +1:** $\det R = +1$ — no mirroring (a reflection has det −1).

Equivalently, $R^T R = I$ and $\det R = 1$; $R$ is **orthonormal**. Its inverse is its transpose: $R^{-1} = R^T$.

### 13.3 Rotation about an arbitrary pivot

To rotate about a point $p$ (not the origin):

$$
v' = T(p) \cdot R(\theta) \cdot T(-p) \cdot v
$$

i.e. **translate the pivot to the origin, rotate, translate back**. The same idea works in 3D for rotating about a joint or a custom pivot.

### 13.4 3D rotation matrices

**Rotation about X (pitch):**

$$
R_x(\alpha) =
\begin{bmatrix}
1 & 0 & 0 \\
0 & \cos\alpha & -\sin\alpha \\
0 & \sin\alpha & \cos\alpha
\end{bmatrix}
$$

**Rotation about Y (yaw):**

$$
R_y(\beta) =
\begin{bmatrix}
\cos\beta & 0 & \sin\beta \\
0 & 1 & 0 \\
-\sin\beta & 0 & \cos\beta
\end{bmatrix}
$$

**Rotation about Z (roll):**

$$
R_z(\gamma) =
\begin{bmatrix}
\cos\gamma & -\sin\gamma & 0 \\
\sin\gamma & \cos\gamma & 0 \\
0 & 0 & 1
\end{bmatrix}
$$

Each matrix rotates around one axis and leaves that axis unchanged. The signs follow the **right-hand rule**: point your right thumb along the positive axis; your fingers curl in the positive rotation direction.

### 13.5 Yaw, pitch and roll

| Term | Axis | Motion | Intuitive example |
|---|---|---|---|
| **Yaw** | **Y** (vertical) | Turn left/right | Shaking your head "no"; turning a car |
| **Pitch** | **X** (horizontal, left–right) | Look up/down | Nodding "yes"; raising/lowering a gun |
| **Roll** | **Z** (line of sight) | Tilt sideways | Tilting your head to your shoulder; banking a plane |

Together with position $(x,y,z)$, these three angles give the **6 degrees of freedom (6 DOF)** of a rigid body.

**In Unity:** the Inspector shows rotation as **X = pitch, Y = yaw, Z = roll** (degrees). Internally, Unity stores a **Quaternion**.

**Sign conventions vary.** In a right-handed system with the right-hand rule, positive pitch about +X tilts the nose **down** or **up** depending on whether the camera looks down −Z or +Z. Always define the convention in an exam answer.

### 13.6 Order matters — and gimbal lock

Rotations about different axes **do not commute**:

$$
R_x(90°) R_y(90°) \neq R_y(90°) R_x(90°)
$$

The order is called the **Euler order**. Common conventions:

- **Aerospace / yaw-pitch-roll:** $R = R_z(\text{yaw}) R_y(\text{pitch}) R_x(\text{roll})$ (varies by field).
- **Unity `Quaternion.Euler(x,y,z)`:** applies **Z, then X, then Y** (intrinsic ZXY).
- **OpenGL / many graphics texts:** $R = R_z R_y R_x$ for column vectors.

**Gimbal lock** happens when two rotation axes align, so one degree of freedom is lost. For a yaw-pitch-roll sequence, it occurs at **pitch = ±90°**: yaw and roll then rotate about the same axis. Symptoms: the object cannot be oriented in a certain direction; interpolation flips or spins.

**Solutions:**

- Use **quaternions** (Unity's internal representation) — no gimbal lock, smooth interpolation.
- Use **rotation matrices** and compose them.
- Use **axis-angle** representation.
- Avoid hitting ±90° pitch in camera code (clamp to ±85–89°).

### 13.7 Quaternions

A **quaternion** represents a 3D rotation as four numbers:

$$
q = w + xi + yj + zk
$$

with $i^2 = j^2 = k^2 = ijk = -1$. For a unit quaternion, $w^2 + x^2 + y^2 + z^2 = 1$.

**Axis-angle to quaternion:** for a unit axis $(a_x, a_y, a_z)$ and angle $\theta$:

$$
q = \left(\cos\frac{\theta}{2},\; a_x\sin\frac{\theta}{2},\; a_y\sin\frac{\theta}{2},\; a_z\sin\frac{\theta}{2}\right)
$$

**Rotating a vector:** $v' = q v q^{-1}$ (where $v$ is treated as a pure quaternion $(0, v_x, v_y, v_z)$).

**Why quaternions are used:**

- **No gimbal lock.**
- **Compact** (4 floats vs 9 for a matrix).
- **Smooth interpolation:** **SLERP** (spherical linear interpolation) gives constant angular velocity and the shortest path.
- **Efficient composition:** $q_{total} = q_2 q_1$.
- **Numerically stable** when normalised.

**Unity API:**

```csharp
Quaternion q = Quaternion.Euler(pitch, yaw, roll);   // Euler -> quaternion
Vector3 e = q.eulerAngles;                            // quaternion -> Euler
Quaternion r = Quaternion.LookRotation(forward, up);
Quaternion s = Quaternion.Slerp(a, b, t);
transform.rotation = q;
```

### 13.8 Axis-angle and Rodrigues' formula

Any 3D rotation can be described by a **unit axis** $\hat{k}$ and an **angle** $\theta$. Rodrigues' formula:

$$
R = I + \sin\theta [\hat{k}]_\times + (1-\cos\theta)[\hat{k}]_\times^2
$$

where $[\hat{k}]_\times$ is the skew-symmetric cross-product matrix. This is useful for rotating about an arbitrary axis (e.g. a joint axis, a custom turret axis).

### 13.9 2D vs 3D rotation

| Basis | **2D rotation** | **3D rotation** |
|---|---|---|
| Axis | One axis perpendicular to the plane (Z) | Three axes (X, Y, Z) |
| Parameters | One angle $\theta$ | Three angles (yaw, pitch, roll) or a quaternion |
| Matrix size | 2×2 (or 3×3 homogeneous) | 3×3 (or 4×4 homogeneous) |
| DOF | 1 rotational DOF | 3 rotational DOF |
| Representation | Angle, complex number | Euler angles, quaternion, axis-angle, matrix |
| Issues | Simple, no gimbal lock | Order matters, gimbal lock with Euler angles |
| Example | 2D sprite spinning | Head turning, camera orbit, aircraft banking |

### 13.10 Why rotation matters in VR

1. **Head tracking** — the HMD reports yaw, pitch and roll; the camera must match them every frame.
2. **Presence** — a mismatch between real head rotation and rendered rotation breaks immersion immediately.
3. **Comfort** — wrong or laggy rotation causes sickness; snap turn is more comfortable than smooth turn for many users.
4. **Interaction** — controllers are 6-DOF; grabbing, aiming and throwing use orientation.
5. **Locomotion** — yaw rotates the player; pitch aims; roll is used in vehicles/flight.
6. **Object manipulation** — rotating a virtual object to inspect it requires 3D rotation.
7. **Animation** — bones rotate in 3D; quaternions avoid gimbal lock.
8. **Camera** — the view matrix is built from the camera's rotation.

---

## 14. Viewing transformations

### 14.1 The problem

A model is defined in **local coordinates**. To draw it, we must answer:

> Where does each vertex appear on the screen, given a camera at some position and orientation?

The **viewing transformation** converts **world coordinates → camera/eye coordinates**, and then the **projection** converts **eye coordinates → clip/NDC → screen**. Together with the model transform, this is the **MVP pipeline**.

### 14.2 The virtual camera

A camera is defined by:

- **Eye position** $e = (e_x, e_y, e_z)$ — where the camera is.
- **Look-at point** $c = (c_x, c_y, c_z)$ — what it points at.
- **Up vector** $u$ — which way is "up" (usually $(0,1,0)$).

From these we build an **orthonormal basis**:

**Standard graphics convention (camera looks down −Z):**

$$
\hat{z} = \frac{-(c-e)}{\|c-e\|}, \qquad
\hat{x} = \frac{u \times \hat{z}}{\|u \times \hat{z}\|}, \qquad
\hat{y} = \hat{z} \times \hat{x}
$$

- $\hat{z}$ points **backward** (out of the screen toward the viewer), because the camera looks down **−Z**.
- $\hat{x}$ points **right**.
- $\hat{y}$ points **up**.

**Alternative (forward-first) formulation:**

$$
f = \frac{c-e}{\|c-e\|}, \quad
s = \frac{f \times u}{\|f \times u\|}, \quad
u' = s \times f
$$

Then the camera axes are $(\hat{x}, \hat{y}, \hat{z}) = (s, u', -f)$.

> **Unity note:** Unity is **left-handed** and the camera looks down **+Z**. Unity's `Camera.worldToCameraMatrix` handles the sign flip internally. In an exam, use the standard right-handed −Z convention and mention the Unity difference.

### 14.3 The view matrix

The view matrix transforms a world point $p$ into camera space:

$$
p_{eye} = V p_{world}
$$

If the camera basis is $(\hat{x}, \hat{y}, \hat{z})$ and eye is $e$, then:

$$
V =
\begin{bmatrix}
\hat{x}_x & \hat{x}_y & \hat{x}_z & -\hat{x}\cdot e \\
\hat{y}_x & \hat{y}_y & \hat{y}_z & -\hat{y}\cdot e \\
\hat{z}_x & \hat{z}_y & \hat{z}_z & -\hat{z}\cdot e \\
0 & 0 & 0 & 1
\end{bmatrix}
$$

**Derivation:** the rotation part $R$ has the camera axes as **rows** (because we are transforming world vectors into the camera basis). The translation is $-R e$, because we first translate the camera to the origin and then rotate.

Equivalently:

$$
V = \begin{bmatrix} R & 0 \\ 0 & 1 \end{bmatrix}
    \begin{bmatrix} I & -e \\ 0 & 1 \end{bmatrix}
  = \begin{bmatrix} R & -R e \\ 0 & 1 \end{bmatrix}
$$

where $R = [\hat{x}; \hat{y}; \hat{z}]$ as rows.

### 14.4 Perspective projection

After the view transform, the camera looks down −Z. A **perspective projection** makes distant objects smaller.

Given:

- vertical field of view $\text{fov}_y$,
- aspect ratio $a = W/H$,
- near plane $n$,
- far plane $f$,

define:

$$
t = \frac{1}{\tan(\text{fov}_y/2)}
$$

The projection matrix (OpenGL-style, NDC z ∈ [−1, 1]) is:

$$
P =
\begin{bmatrix}
\frac{t}{a} & 0 & 0 & 0 \\
0 & t & 0 & 0 \\
0 & 0 & \frac{f+n}{n-f} & \frac{2fn}{n-f} \\
0 & 0 & -1 & 0
\end{bmatrix}
$$

For a point in front of the camera, $z_{eye} < 0$, so:

$$
w_{clip} = -z_{eye} > 0
$$

and the **perspective divide** gives:

$$
x_{ndc} = \frac{x_{clip}}{w_{clip}}, \quad
y_{ndc} = \frac{y_{clip}}{w_{clip}}, \quad
z_{ndc} = \frac{z_{clip}}{w_{clip}}
$$

**What each term does:**

- $t/a$ and $t$ set the horizontal and vertical FOV.
- The third row maps the near/far planes to the NDC depth range.
- The $-1$ in the fourth row puts $-z_{eye}$ into $w$, creating the perspective divide.
- **Orthographic projection** replaces the perspective terms with a linear mapping; objects do not shrink with distance (used for 2D, isometric, CAD).

**DirectX / Unity depth range:** NDC z ∈ [0, 1] instead of [−1, 1]; the third row changes. Unity's `GL.GetGPUProjectionMatrix` handles the platform difference.

### 14.5 NDC, viewport and screen

After the divide, NDC is typically:

$$
x_{ndc}, y_{ndc} \in [-1, 1], \qquad z_{ndc} \in [-1, 1] \text{ (OpenGL)}
$$

**Viewport transform** to pixels:

$$
x_{screen} = \frac{x_{ndc}+1}{2} W, \qquad
y_{screen} = \frac{1-y_{ndc}}{2} H
$$

The $1-y_{ndc}$ flips y because screen coordinates usually have the origin at the **top-left**, while NDC has y up. If your window system uses bottom-left, use $(y_{ndc}+1)/2 \cdot H$.

**Depth:** $z_{ndc}$ is stored in the **depth buffer**; the GPU keeps the nearest fragment. Near/far precision is non-linear — most precision is close to the near plane, so do not set the near plane too small.

### 14.6 Clipping and rasterization

Before rasterization, the GPU **clips** primitives against the view frustum:

$$
-w \le x_{clip} \le w, \quad
-w \le y_{clip} \le w, \quad
-w \le z_{clip} \le w
$$

- Triangles completely inside are kept.
- Triangles completely outside are discarded.
- Triangles crossing a plane are clipped and re-triangulated.

Then the GPU **rasterizes** each triangle into fragments (pixels), interpolates attributes (UV, colour, normal, depth), runs the fragment shader, and performs the **depth test** and blending.

### 14.7 Worked example

Camera at $e=(0,0,5)$, looking at $c=(0,0,0)$, up $u=(0,1,0)$. FOV 90°, aspect 1, near 0.1, far 100.

1. $f = (0,0,-1)$; $\hat{z} = (0,0,1)$; $\hat{x} = (1,0,0)$; $\hat{y} = (0,1,0)$.
2. View matrix translates by $(0,0,-5)$: a world point $(0,0,0)$ becomes $(0,0,-5)$.
3. $t = 1/\tan(45°) = 1$.
4. For a point $(1,0,0)$: eye space $(1,0,-5)$.
5. Clip: $x_{clip} = 1 \cdot 1 = 1$, $w_{clip} = 5$.
6. NDC: $x_{ndc} = 1/5 = 0.2$, $y_{ndc}=0$.
7. Screen (1920×1080): $x = (0.2+1)/2 \cdot 1920 = 1152$, $y = (1-0)/2 \cdot 1080 = 540$.

So a point 1 unit to the right of the camera axis, 5 units away, appears 20% of the way from centre to the right edge — exactly what perspective should do.

### 14.8 Stereoscopic viewing (VR)

VR renders two images, one per eye:

- The eyes are separated by the **interpupillary distance (IPD)**, about **63 mm** on average.
- For the **left eye**, shift the camera left by $\text{IPD}/2$; for the **right eye**, shift right by $\text{IPD}/2$.
- The projection matrices are **asymmetric frusta** because each eye looks through a slightly offset window.

$$
v_{clip}^{left} = P_{left} \cdot V \cdot M \cdot v, \qquad
v_{clip}^{right} = P_{right} \cdot V \cdot M \cdot v
$$

The **parallax** between the two images is interpreted by the brain as depth. This is the basis of stereoscopic 3D.

**VR rendering optimizations:**

- **Single-pass instanced stereo** — render both eyes in one pass with instancing.
- **Multiview / view instancing** — hardware support for two views.
- **Lens distortion correction** — pre-distort the image for the headset lenses.
- **Chromatic aberration correction** — separate colour channels.
- **Late latching / reprojection** — adjust for the latest head pose.
- **Foveated rendering** — high resolution where the eye looks, lower in the periphery.

### 14.9 Unity equivalents

| Step | Unity API |
|---|---|
| Model matrix | `transform.localToWorldMatrix` |
| View matrix | `Camera.worldToCameraMatrix` |
| Projection | `Camera.projectionMatrix` / `GL.GetGPUProjectionMatrix` |
| MVP | `camera.projectionMatrix * camera.worldToCameraMatrix * transform.localToWorldMatrix` |
| World → screen | `Camera.WorldToScreenPoint` |
| Screen → world | `Camera.ScreenToWorldPoint` |
| World → viewport | `Camera.WorldToViewportPoint` |

```csharp
Matrix4x4 mvp = camera.projectionMatrix
              * camera.worldToCameraMatrix
              * transform.localToWorldMatrix;

Vector3 screen = camera.WorldToScreenPoint(transform.position);
```

---

## 15. Chaining the transformations

### 15.1 The idea

A vertex may need many transformations:

1. scale it in local space,
2. rotate it,
3. translate it to its world position,
4. transform it into camera space,
5. project it,
6. map it to the viewport.

Instead of applying each matrix separately to every vertex, we **multiply the matrices together once** into a single combined matrix, then apply that one matrix to every vertex. This is **chaining the transformations**.

$$
v_{screen} = T_{vp} \cdot T_{can} \cdot T_{eye} \cdot T_{rb} \cdot v_{model}
$$

where:

- $T_{rb}$ — rigid-body / model transform (local → world),
- $T_{eye}$ — view transform (world → camera),
- $T_{can}$ — canonical / projection transform (camera → clip),
- $T_{vp}$ — viewport transform (NDC → screen).

The combined matrix is:

$$
T = T_{vp} \cdot T_{can} \cdot T_{eye} \cdot T_{rb}
$$

### 15.2 Matrix multiplication

For two 4×4 matrices $A$ and $B$, the product $C = A B$ has:

$$
C_{ij} = \sum_{k=1}^{4} A_{ik} B_{kj}
$$

Each element is the **dot product of row $i$ of $A$ and column $j$ of $B$**.

Properties:

- **Associative:** $(A B) C = A (B C)$ — we can group the chain however we like.
- **Not commutative:** $A B \neq B A$ — order matters.
- **Identity:** $I A = A I = A$.
- **Inverse:** $(A B)^{-1} = B^{-1} A^{-1}$.

### 15.3 The chaining algorithm

**Given:** a list of matrices in the order they must be **applied** to a column vector, e.g. $[S, R, T]$ (scale first, then rotate, then translate).

**Goal:** compute $M = T R S$.

**Algorithm (left-multiply while iterating in application order):**

```
M = Identity
for each transform X in [S, R, T]:     // application order
    M = X * M
return M
```

Trace:

- start: $M = I$
- $X=S$: $M = S$
- $X=R$: $M = R S$
- $X=T$: $M = T R S$ ✓

**Alternative (right-multiply while iterating in reverse order):**

```
M = Identity
for i = n-1 down to 0:                 // reverse application order
    M = M * transforms[i]
return M
```

Trace with $[S,R,T]$:

- $i=T$: $M = T$
- $i=R$: $M = T R$
- $i=S$: $M = T R S$ ✓

**Then apply once per vertex:**

```
for each vertex v:
    v' = M * v
```

**Complexity:** building the chain is $O(n)$ matrix multiplications (each 4×4 multiplication is a constant 64 multiply-adds). Applying it is **one** matrix–vector multiply per vertex instead of $n$. For a mesh with $V$ vertices and $n$ transforms, the naive cost is $O(V n)$; the chained cost is $O(V + n)$.

### 15.4 Worked numerical example

Let:

$$
S = \begin{bmatrix}2&0&0&0\\0&2&0&0\\0&0&1&0\\0&0&0&1\end{bmatrix}, \quad
R = R_z(90°) = \begin{bmatrix}0&-1&0&0\\1&0&0&0\\0&0&1&0\\0&0&0&1\end{bmatrix}, \quad
T = \begin{bmatrix}1&0&0&3\\0&1&0&4\\0&0&1&0\\0&0&0&1\end{bmatrix}
$$

**Chain:** $M = T R S$.

First $R S$:

$$
R S = \begin{bmatrix}0&-2&0&0\\2&0&0&0\\0&0&1&0\\0&0&0&1\end{bmatrix}
$$

Then $T (R S)$:

$$
M = \begin{bmatrix}0&-2&0&3\\2&0&0&4\\0&0&1&0\\0&0&0&1\end{bmatrix}
$$

Apply to $v=(1,0,0,1)$:

$$
M v = (3, 6, 0, 1)
$$

Check step by step: scale → $(2,0,0)$; rotate 90° → $(0,2,0)$; translate → $(3,6,0)$. ✓

### 15.5 Scene graphs and hierarchical transforms

A scene is a **tree** of transforms:

```
World
└── Player
    ├── Camera
    ├── LeftHand
    │   └── Sword
    └── RightHand
```

Each node has a **local** transform relative to its parent. The **world** matrix is the product of all ancestors:

$$
M_{world} = M_{root} \cdot \cdots \cdot M_{parent} \cdot M_{local}
$$

A vertex in the Sword's local space goes to world space by:

$$
v_{world} = M_{Player} \cdot M_{LeftHand} \cdot M_{Sword} \cdot v_{local}
$$

This is why moving the player moves the hands and the sword automatically. The **matrix stack** (push/pop) is the classic implementation: push the parent matrix, multiply by the child's local matrix, draw the child, pop.

**Unity:** `transform.localToWorldMatrix` already includes the parent chain; `transform.parent` gives the parent.

### 15.6 The full render pipeline chain

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

**Stereo:**

$$
v_{clip}^{eye} = P_{eye} \cdot V \cdot M_{model} \cdot v_{local}
$$

where $P_{eye}$ uses the left or right eye offset and asymmetric frustum.

### 15.7 Unity code

```csharp
using UnityEngine;

public class MatrixChainDemo : MonoBehaviour
{
    public Vector3 translation = new Vector3(3f, 4f, 0f);
    public Vector3 eulerAngles = new Vector3(0f, 0f, 90f);   // pitch, yaw, roll
    public Vector3 scale = new Vector3(2f, 2f, 1f);

    void Start()
    {
        // 1. Build the individual matrices
        Matrix4x4 T = Matrix4x4.Translate(translation);
        Matrix4x4 R = Matrix4x4.Rotate(Quaternion.Euler(eulerAngles));
        Matrix4x4 S = Matrix4x4.Scale(scale);

        // 2. Chain: scale first, then rotate, then translate
        Matrix4x4 M = T * R * S;

        // 3. Apply the single matrix to a vertex
        Vector3 local = new Vector3(1f, 0f, 0f);
        Vector3 world = M.MultiplyPoint3x4(local);

        Debug.Log($"Local {local} -> World {world}");   // (1,0,0) -> (3,6,0)

        // 4. Compare with Unity's own local-to-world matrix
        Debug.Log(transform.localToWorldMatrix);
    }
}
```

### 15.8 Why chaining matters

1. **Performance** — one matrix–vector multiply per vertex instead of $n$. For static objects, compute the matrix once and reuse it; for instancing, upload it to the GPU.
2. **Correct order** — the chain encodes the intended order (scale → rotate → translate), which is not obvious from separate operations.
3. **Hierarchies** — a child inherits its parent's transform by multiplying matrices; this is the foundation of scene graphs, skeletal animation and VR rigs.
4. **Camera and projection** — the view and projection matrices are just two more links in the chain.
5. **Stereo** — swap the projection matrix per eye without changing the model or view chain.
6. **Reuse** — a single matrix can be stored, interpolated, inverted and passed to shaders.
7. **GPU efficiency** — the vertex shader receives one MVP matrix; the CPU does the chain once per object, not once per vertex.

### 15.9 Common mistakes

| Mistake | Result | Fix |
|---|---|---|
| Wrong order (`S R T` instead of `T R S`) | Object scales in world space or rotates around the wrong point | Remember right-to-left application |
| Using row-vector order with column vectors | Transposed result | Match convention |
| Forgetting the perspective divide | No depth/size perspective | Divide by $w$ |
| Transforming normals with the model matrix under non-uniform scale | Wrong lighting | Inverse transpose |
| Using world position as local | Double transform | Use `localToWorldMatrix` correctly |
| Recomputing the chain per vertex | Slow | Compute once per object |
| Ignoring parent transforms | Child detaches from parent | Multiply by parent world matrix |
| Mixing degrees and radians | Wild rotations | Convert with `Mathf.Deg2Rad` / `Rad2Deg` |

---

## 16. Motion in real and virtual worlds

### 16.1 Position, velocity and acceleration

Let position be $\mathbf{r}(t) = (x(t), y(t), z(t))$.

**Velocity** is the rate of change of position:

$$
\mathbf{v}(t) = \frac{d\mathbf{r}}{dt} = \left(\frac{dx}{dt}, \frac{dy}{dt}, \frac{dz}{dt}\right)
$$

**Acceleration** is the rate of change of velocity:

$$
\mathbf{a}(t) = \frac{d\mathbf{v}}{dt} = \frac{d^2\mathbf{r}}{dt^2}
$$

**Speed** is the magnitude of velocity:

$$
v = \|\mathbf{v}\| = \sqrt{v_x^2 + v_y^2 + v_z^2}
$$

**Units:** position in metres (m), velocity in m/s, acceleration in m/s².

**Average vs instantaneous:**

$$
\mathbf{v}_{avg} = \frac{\Delta \mathbf{r}}{\Delta t}, \qquad
\mathbf{v}_{inst} = \lim_{\Delta t \to 0} \frac{\Delta \mathbf{r}}{\Delta t}
$$

### 16.2 Equations of motion (constant acceleration)

For constant acceleration $\mathbf{a}$:

$$
\mathbf{v} = \mathbf{v}_0 + \mathbf{a}t
$$

$$
\mathbf{r} = \mathbf{r}_0 + \mathbf{v}_0 t + \frac{1}{2}\mathbf{a}t^2
$$

$$
v^2 = v_0^2 + 2\mathbf{a}\cdot(\mathbf{r}-\mathbf{r}_0)
$$

**Free fall / projectile** with gravity $g = 9.8$ m/s² downward:

$$
v_y = v_{0y} - gt, \qquad
y = y_0 + v_{0y}t - \frac{1}{2}gt^2
$$

**Projectile range** (launched from ground at speed $v_0$, angle $\theta$):

$$
R = \frac{v_0^2 \sin(2\theta)}{g}
$$

**Time of flight:**

$$
T = \frac{2v_0\sin\theta}{g}
$$

These equations are used for ballistics, jumping, throwing and physics prediction in VR.

### 16.3 Circular motion

For an object moving in a circle of radius $r$ with angular velocity $\omega$ (rad/s):

$$
v = r\omega
$$

**Centripetal acceleration** points toward the centre:

$$
a_c = \frac{v^2}{r} = r\omega^2
$$

**Angular acceleration** $\alpha = d\omega/dt$; angular position $\theta = \theta_0 + \omega t + \frac{1}{2}\alpha t^2$.

In 3D, angular velocity is a **vector** along the rotation axis; the linear velocity of a point is:

$$
\mathbf{v} = \boldsymbol{\omega} \times \mathbf{r}
$$

This is how a rotating rigid body's points move, and how a VR headset's angular velocity relates to the changing orientation.

### 16.4 Physics in the virtual world

A **physics engine** simulates motion and collisions using numerical integration and constraint solving.

**Core concepts:**

| Concept | Meaning |
|---|---|
| **Rigid body** | An object that does not deform; has mass, position, orientation, velocity, angular velocity |
| **Mass** | Resistance to acceleration ($F = ma$) |
| **Force** | Push/pull; changes momentum |
| **Impulse** | Instantaneous change in momentum ($J = F\Delta t$) |
| **Momentum** | $p = mv$ |
| **Torque** | Rotational force; changes angular momentum |
| **Inertia tensor** | Resistance to angular acceleration |
| **Friction** | Force opposing relative motion at contact |
| **Restitution** | Bounciness (0 = inelastic, 1 = perfectly elastic) |
| **Damping** | Energy loss over time (linear/angular drag) |
| **Constraint / joint** | Restricts relative motion (hinge, ball, fixed, spring) |
| **Collider** | Shape used for collision detection |
| **Trigger** | Collider that detects overlap without physical response |

**Newton's laws:**

1. An object stays at rest or in uniform motion unless acted on by a force.
2. $F = ma$ (or $F = dp/dt$).
3. Every action has an equal and opposite reaction.

**Simulation loop:**

```
for each fixed timestep:
    apply forces (gravity, user input, springs)
    integrate velocity and position
    detect collisions
    resolve collisions / constraints
    update transforms
```

### 16.5 Numerical integration

The engine advances the state by a time step $\Delta t$.

**Explicit Euler:**

$$
v_{k+1} = v_k + a_k \Delta t, \qquad
x_{k+1} = x_k + v_k \Delta t
$$

Simple, but can gain energy and become unstable at large $\Delta t$.

**Semi-implicit (symplectic) Euler:**

$$
v_{k+1} = v_k + a_k \Delta t, \qquad
x_{k+1} = x_k + v_{k+1} \Delta t
$$

Uses the **new** velocity for the position update — more stable, standard in games.

**Verlet:**

$$
x_{k+1} = 2x_k - x_{k-1} + a_k \Delta t^2
$$

Good for cloth, ropes and particles; position-based, no explicit velocity.

**Runge–Kutta (RK4):** evaluates the derivative four times per step; more accurate, more expensive. Used when accuracy matters (orbital mechanics, high-speed simulation).

**Timestep issues:**

- **Too large** → instability, tunneling, jitter.
- **Too small** → expensive; many substeps.
- **Fixed timestep** → deterministic physics; Unity's `FixedUpdate` (default 0.02 s).
- **Variable timestep** → frame-rate dependent; avoid for physics.
- **Interpolation** → smooth rendering between physics steps.

### 16.6 Collision detection and response

**Detection** answers *if* and *when* objects touch. **Response** decides *what happens*: bounce, slide, stop, deform, vibrate, damage.

**Two-phase detection:**

| Phase | Purpose | Techniques |
|---|---|---|
| **Broad phase** | Quickly reject far-apart pairs | Bounding spheres, AABBs, OBBs, sweep-and-prune, spatial hashing, octrees, BVH |
| **Narrow phase** | Precise contact test on remaining pairs | Triangle–triangle, sphere–sphere, capsule–capsule, GJK, SAT, MPR |

**Bounding volumes:**

- **Sphere** — cheapest, rotation-invariant, loose fit.
- **AABB** — axis-aligned box, cheap, loose fit.
- **OBB** — oriented box, tighter, more expensive.
- **Convex hull** — tightest convex fit, most expensive.
- **Mesh collider** — exact but expensive; use for static geometry only.

**Distance and penetration:**

- $d(A,B)$ = smallest gap; 0 if touching.
- **Penetration depth** = how deep they overlap; used to push objects apart and prevent sinking.

**Discrete vs continuous:**

- **Discrete** — checks each frame; fast objects can **tunnel** through thin walls.
- **Continuous (CCD)** — sweeps the shape along its motion; prevents tunneling at the cost of performance.

**Response:**

- **Impulse-based** — apply an impulse along the contact normal, with restitution and friction.
- **Position-based** — push objects apart to resolve penetration.
- **Constraint-based** — solve contact constraints iteratively (sequential impulses).
- **Soft bodies** — spring-mass, shape matching, FEM.

**Unity physics:**

- `Rigidbody` + `Collider` for dynamic objects.
- `FixedUpdate` for forces; `Update` for input.
- `OnCollisionEnter/Stay/Exit`, `OnTriggerEnter/Stay/Exit`.
- `Physics.Raycast`, `SphereCast`, `OverlapSphere`, `CheckSphere`.
- `PhysX` backend; `Rigidbody.interpolation` for smooth rendering; `collisionDetectionMode = ContinuousDynamic` for fast objects.
- **Layer collision matrix** to avoid unnecessary pairs.
- **Physics materials** for friction and bounciness.

### 16.7 Mismatched motion

**Mismatched motion** is a disagreement between the motion the user **sees** and the motion their **body feels**. In VR it is the main cause of **simulator sickness**.

**Three sensory systems involved:**

1. **Visual** — optic flow, motion parallax, the moving virtual world.
2. **Vestibular** — inner ear; detects head acceleration and orientation.
3. **Proprioceptive** — muscles, joints, skin; body position and movement.

When they disagree, the brain interprets it as poisoning or instability and produces nausea, dizziness, sweating, headache and fatigue.

**Examples of mismatch:**

| Scenario | Visual says | Vestibular says | Result |
|---|---|---|---|
| Seated user moves forward in VR | Moving | Still | Motion sickness |
| Virtual roller coaster | Accelerating/turning | Still | Strong sickness |
| Standing on a real moving platform, virtual world static | Still | Moving | Confusion, imbalance |
| Head turn with tracking lag | Turning slowly | Turning now | Dizziness, lag |
| Virtual world rotates but body does not | Rotating | Still | Circular vection, sickness |
| Real obstacle not represented in VR | No obstacle | Body stops | Disorientation, safety risk |
| Virtual wall with no real wall | Wall | No resistance | Loss of trust/immersion |

**Mismatched obstacles** (real vs virtual):

| Case | Issue | Design response |
|---|---|---|
| **Real only** (table, stairs, pet) | User cannot see it; may trip or hit it | Warn, show a virtual proxy, clear the play area, use guardian |
| **Virtual only** (wall, pit) | No physical resistance; user may walk through or fall | Stop the camera, add haptic/audio warning, or align with a real obstacle |
| **Both match** | Believable and safe | Keep alignment; calibrate the play area |
| **Offset** | Virtual object is not where the real one is | Recalibrate; avoid reaching tasks near real obstacles |

**Mitigation techniques:**

- **Teleport** instead of smooth locomotion.
- **Snap turn** instead of smooth yaw.
- **Comfort vignette** (tunnel vision) during movement.
- **Stable horizon** — no camera roll.
- **Reduce acceleration** — constant velocity is more comfortable than speeding up/slowing down.
- **Cockpit metaphor** — put the user in a vehicle; the brain accepts motion.
- **Physical motion platform** — move the body to match the visuals.
- **Latency reduction** — low motion-to-photon latency (< 20 ms).
- **Frame rate** — maintain 90/120 FPS; reprojection helps but is not a substitute.
- **User control** — let the user choose speed, turn mode, vignette.
- **Short sessions / breaks** — gradually build tolerance.

### 16.8 Vection

**Vection** is the **illusion of self-motion** produced by visual stimuli — usually **optic flow** — when the observer is actually stationary (or moving differently).

It is a **visually induced** sense of movement. The classic example: sitting in a stationary train and seeing the train next to you move; you feel your own train moving in the opposite direction.

**Why it happens:** the visual system dominates spatial orientation. Large, coherent, peripheral optic flow is interpreted as self-motion, even when the vestibular system reports no acceleration.

**Types of vection:**

| Type | Axis / motion | What the user sees | Role in VR |
|---|---|---|---|
| **Linear vection** (translational) | Forward/backward, left/right, up/down | Optic flow radiating from a focus of expansion (forward) or contracting (backward) | Simulates walking, driving, flying; gives speed and direction cues; foundation of smooth locomotion |
| **Circular vection** (rotational / yaw) | Rotation about the vertical (Y) axis | Environment rotating around the user | Simulates turning; used in turntables, swivel chairs, smooth turn; strong sickness trigger |
| **Roll vection** | Rotation about the line of sight (Z) | World tilting/rolling | Simulates banking, tilting, aircraft roll; can be very uncomfortable |
| **Curvilinear vection** | Combination of linear + circular | Curved optic flow | Simulates turning while moving (driving around a corner, running along a curve) |
| **Vertical vection** | Up/down translation | Floor/ceiling moving | Elevators, falling, flying; height sensation |

**Factors that increase vection:**

- **Wide field of view**, especially **peripheral vision**.
- **Coherent, textured** optic flow (not random noise).
- **Depth cues** — motion parallax, stereo, occlusion.
- **High contrast** and **luminance**.
- **Low latency** and **high frame rate**.
- **Large moving area** — a full-screen environment is stronger than a small window.
- **Attention** — looking at the flow increases it.

**Factors that reduce vection:**

- A **static frame** (cockpit, dashboard, virtual nose) — gives a stable reference.
- **Narrow FOV** or a small moving window.
- **Low texture** / low contrast.
- **Incongruent motion** — different parts of the scene move differently.
- **Physical motion** that matches the visual motion (then it is not illusory).

**Role of vection in VR:**

1. **Self-motion perception** — it lets a stationary user feel they are moving through the virtual world.
2. **Locomotion** — smooth locomotion relies on vection; without it, the user feels like a floating camera.
3. **Presence** — coherent optic flow increases the sense of "being there".
4. **Speed and direction cues** — the focus of expansion tells the user where they are heading.
5. **Comfort vs sickness** — strong vection without vestibular match causes sickness; designers tune it.
6. **Vehicle simulation** — cockpit + vection gives a convincing driving/flying experience.
7. **Redirected walking** — vection can subtly steer the user's real walking path.
8. **Illusion research** — vection is used to study perception, balance and presence.

> **Exam distinction:** *Vection is the **illusion** of self-motion from vision; **mismatched motion** is the **conflict** between visual and vestibular/proprioceptive motion. Vection can cause mismatch (and sickness) when the body is not actually moving.*

### 16.9 The vestibular system in VR

The **vestibular system** is the sensory system in the **inner ear** that detects **head acceleration and orientation** and helps control **balance and eye movements**.

**Anatomy:**

| Part | What it detects | Role |
|---|---|---|
| **Semicircular canals** (3, roughly orthogonal) | **Angular acceleration** (rotation) | Detect yaw, pitch, roll of the head |
| **Utricle** (otolith organ) | **Linear acceleration** (horizontal) and head tilt relative to gravity | Detect translation and tilt |
| **Saccule** (otolith organ) | **Linear acceleration** (vertical) and gravity | Detect up/down motion and tilt |
| **Hair cells** | Deflection of stereocilia | Convert motion into nerve signals |
| **Vestibulo-ocular reflex (VOR)** | — | Stabilises the eyes during head movement |

**How it works:** when the head accelerates, fluid in the canals lags behind and bends the **cupula**, deflecting hair cells. Otoliths (tiny calcium carbonate crystals) lag behind and deflect hair cells in the utricle/saccule. The brain combines these signals with vision and proprioception to estimate motion and orientation.

**In VR:**

- The vestibular system is **physically stationary** when the user sits/stands with a headset. A screen cannot directly accelerate the inner ear.
- If the virtual world moves, the **eyes** report motion but the **vestibular system** reports none → **sensory conflict** → **simulator sickness**.
- If the user physically turns their head, the visual and vestibular signals **agree** → comfortable and immersive.
- The **VOR** means the eyes automatically counter-rotate during head turns; if the rendered image lags, the user notices blur and discomfort.

**Suitable example:** A user sits in a chair wearing an HMD. The virtual scene shows them moving forward through a tunnel (linear vection). Their eyes see strong optic flow, but their semicircular canals and otoliths detect no acceleration. The brain receives contradictory signals, and the user feels dizzy, sweaty and nauseated. If instead the user physically walks in a tracked room (or the scene is a stationary cockpit), the visual and vestibular signals match and the experience is comfortable.

**Implications for design:**

- Prefer **room-scale walking**, **teleport** and **snap turn**.
- Avoid **forced acceleration**, **roller-coaster** motion and **camera shake**.
- Use a **cockpit** or **stable frame** to anchor the visual field.
- Keep **latency low** and **frame rate high**.
- Provide **comfort settings** and **breaks**.
- Use **galvanic vestibular stimulation (GVS)** only in research; it is not consumer-ready.

### 16.10 Presence, immersion and sickness

| Term | Meaning |
|---|---|
| **Immersion** | The objective capability of the system to deliver sensory fidelity (FOV, resolution, audio, haptics, tracking). |
| **Presence** | The subjective feeling of "being there" in the virtual world. |
| **Simulator sickness / cybersickness** | Nausea, dizziness, sweating, headache caused by sensory conflict, latency, flicker or poor ergonomics. |
| **Vection** | Illusion of self-motion from visual flow. |
| **Sensory conflict** | Mismatch between visual, vestibular and proprioceptive signals. |

**Ways to increase presence:**

- High frame rate, low latency, high resolution.
- Wide FOV and correct IPD.
- 6-DOF tracking and accurate hand/controller tracking.
- Spatial audio.
- Haptic feedback.
- Coherent physics and interaction.
- A believable, consistent world.

**Ways to reduce sickness:**

- Comfortable locomotion (teleport, snap turn).
- Vignette during movement.
- Stable horizon, no camera roll.
- No forced camera motion.
- Low latency, high frame rate.
- Correct scale and IPD.
- User control and breaks.

---

## 17. Formula sheet (quick revision)

### 17.1 Rotation

**2D:**

$$
x' = x\cos\theta - y\sin\theta, \qquad
y' = x\sin\theta + y\cos\theta
$$

**3D:**

$$
R_x(\alpha) =
\begin{bmatrix}1&0&0\\0&\cos\alpha&-\sin\alpha\\0&\sin\alpha&\cos\alpha\end{bmatrix}, \quad
R_y(\beta) =
\begin{bmatrix}\cos\beta&0&\sin\beta\\0&1&0\\-\sin\beta&0&\cos\beta\end{bmatrix}, \quad
R_z(\gamma) =
\begin{bmatrix}\cos\gamma&-\sin\gamma&0\\\sin\gamma&\cos\gamma&0\\0&0&1\end{bmatrix}
$$

**Rotation about a pivot $p$:**

$$
v' = T(p) R T(-p) v
$$

**Quaternion from axis-angle:**

$$
q = \left(\cos\frac{\theta}{2}, a_x\sin\frac{\theta}{2}, a_y\sin\frac{\theta}{2}, a_z\sin\frac{\theta}{2}\right)
$$

### 17.2 Viewing

**Look-at basis (camera looks −Z):**

$$
\hat{z} = \frac{e-c}{\|e-c\|}, \quad
\hat{x} = \frac{u \times \hat{z}}{\|u \times \hat{z}\|}, \quad
\hat{y} = \hat{z} \times \hat{x}
$$

**View matrix:**

$$
V = \begin{bmatrix}
\hat{x}_x & \hat{x}_y & \hat{x}_z & -\hat{x}\cdot e \\
\hat{y}_x & \hat{y}_y & \hat{y}_z & -\hat{y}\cdot e \\
\hat{z}_x & \hat{z}_y & \hat{z}_z & -\hat{z}\cdot e \\
0&0&0&1
\end{bmatrix}
$$

**Perspective projection:**

$$
P = \begin{bmatrix}
\frac{t}{a}&0&0&0\\
0&t&0&0\\
0&0&\frac{f+n}{n-f}&\frac{2fn}{n-f}\\
0&0&-1&0
\end{bmatrix}, \qquad t = \frac{1}{\tan(\text{fov}_y/2)}
$$

**Perspective divide:**

$$
x_{ndc} = \frac{x_{clip}}{w_{clip}}, \quad
y_{ndc} = \frac{y_{clip}}{w_{clip}}, \quad
z_{ndc} = \frac{z_{clip}}{w_{clip}}
$$

**Viewport:**

$$
x_s = \frac{x_{ndc}+1}{2}W, \qquad
y_s = \frac{1-y_{ndc}}{2}H
$$

### 17.3 Chaining

$$
v_{clip} = P \cdot V \cdot M_{model} \cdot v_{local}
$$

$$
M_{world} = M_{parent} \cdot M_{local}
$$

$$
M = T \cdot R \cdot S \quad \text{(column vectors, right-to-left)}
$$

### 17.4 Motion

$$
v = \frac{dr}{dt}, \quad a = \frac{dv}{dt}, \quad v = v_0 + at, \quad r = r_0 + v_0t + \frac{1}{2}at^2
$$

$$
v^2 = v_0^2 + 2a(r-r_0), \quad v = r\omega, \quad a_c = \frac{v^2}{r} = r\omega^2
$$

**Semi-implicit Euler:**

$$
v_{k+1} = v_k + a_k\Delta t, \qquad x_{k+1} = x_k + v_{k+1}\Delta t
$$

### 17.5 Key numbers

| Quantity | Value |
|---|---|
| Gravity | $9.8$ m/s² |
| Average IPD | ~63 mm |
| VR frame rate | 90 / 120 Hz |
| Motion-to-photon latency target | < 20 ms |
| Unity physics timestep | 0.02 s (50 Hz) |
| Unity unit | 1 m |
| Comfortable VR interaction distance | 0.3–0.8 m direct; 1.5–5 m ray |

---

## 18. Exam strategy and question map

### 18.1 How to structure a long answer

1. **Definition** — one or two precise sentences.
2. **Diagram** — a labelled block diagram or matrix chain; examiners reward it.
3. **Explanation** — numbered points, each with a "because".
4. **Example** — a VR/AR scenario (headset, controller, AR phone).
5. **Advantages / limitations** — a short balanced list.
6. **Conclusion** — one sentence linking back to VR/AR.

### 18.2 Likely CIE-2 questions and where to look

| Question | Section |
|---|---|
| Creating/placing prefabs; reuse & scene organisation | §2.3, §2.9, §2.10 |
| Geometric primitives; why triangles | §11.3, §11.5 |
| Types of vection; self-motion | §16.8 |
| Vestibular system with example | §16.9 |
| Viewing transformation world → screen | §14.1–14.7 |
| Importance of DOF | §13.5, §5.6, §16.9 |
| Mouse rotation program | §5.4, §8.3, and [[VRAR PYQ CIE-2]] Q4 |
| Matrix multiplication / chaining | §15.2–15.8 |
| 2D vs 3D rotation / yaw-pitch-roll | §13.1–13.10 |
| Physics in the virtual world | §16.4–16.6 |
| Mismatched motion | §16.7 |
| Level design / UI / camera movement | §6, §7, §8 |
| Asset Store | §10 |

### 18.3 One-mark / two-mark rapid-fire

| Question | Short answer |
|---|---|
| What is a prefab? | Reusable GameObject template stored as an asset. |
| Prefab asset vs instance? | Asset = master in Project; instance = placed linked copy in Scene. |
| What is a geometric primitive? | A basic building-block shape (cube, sphere, plane, triangle). |
| Why triangles? | Planar, convex, simplest, GPU-native, easy to interpolate and collide. |
| What is vection? | Illusion of self-motion caused by visual optic flow. |
| Types of vection? | Linear, circular (yaw), roll, curvilinear, vertical. |
| What is the vestibular system? | Inner-ear system detecting angular/linear acceleration and orientation. |
| What is DOF? | Number of independent motions; 3 position + 3 rotation = 6. |
| What is a viewing transformation? | Converts world coordinates to camera/screen coordinates. |
| What is chaining? | Multiplying transforms into one matrix, then applying it to vertices. |
| What is the Asset Store? | Unity's marketplace for models, textures, audio, tools and templates. |
| What is `Time.deltaTime`? | Seconds since the last frame; makes motion frame-rate independent. |
| What is a CharacterController? | A capsule-based mover with collision, slopes and steps but no physics. |
| What is gimbal lock? | Loss of a rotational DOF when two Euler axes align (pitch ±90°). |
| What is a quaternion? | A 4-number rotation representation without gimbal lock. |
| What is IPD? | Interpupillary distance; the eye separation used for stereo. |
| What is presence? | The subjective feeling of being inside the virtual world. |
| What is simulator sickness? | Nausea/dizziness from sensory conflict, latency or poor ergonomics. |

### 18.4 Common mistakes to avoid

- Writing "prefab is a copy" — it is a **template/asset**; an **instance** is the copy.
- Saying "triangles are used because they are easy" without **why** (planar, convex, GPU-native, interpolatable).
- Confusing **vection** (illusion of self-motion) with **mismatch** (conflict between senses).
- Forgetting the **perspective divide** in the viewing pipeline.
- Writing the matrix chain in the wrong order — state the convention.
- Mixing **yaw/pitch/roll** axes — yaw = Y, pitch = X, roll = Z.
- Claiming quaternions have no disadvantages — they are less intuitive and need normalisation.
- Ignoring **VR comfort** in level/UI/camera answers — it earns marks.
- Writing code without explaining the **why** (deltaTime, normalisation, clamping).
- Forgetting **stereo/IPD** when discussing VR viewing.

---

*End of CIE-2 notes. Companion: [[VRAR PYQ CIE-2]] — full solutions to the CIE-2 paper.*

---

## Related notes

- [[VRAR MOC]] — subject index and reading order.
- [[VRAR PYQ CIE-2]] — solved CIE-2 paper for these topics.
- [[VRAR CIE-1 Notes]] — previous exam: Unit I and early Unit II.
- [[VRAR PYQ CIE-1]] — previous exam: solved CIE-1 paper.
- [[vrar]] — raw lecture notes.
- [[sample progs]] — Unity C# scripts for movement and rotation.
