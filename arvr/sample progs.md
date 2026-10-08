# Simple C# Programs for Unity

Three ready-to-attach `MonoBehaviour` scripts: mouse rotation, continuous Y-axis rotation, and smooth arrow-key movement.

---

## 1. Rotate Object with Mouse

```csharp
using UnityEngine;

public class SimpleMouseRotate : MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        transform.Rotate(Vector3.up * mouseX * speed);
        transform.Rotate(Vector3.right * -mouseY * speed);
    }
}
```

**How it works**

- `Input.GetAxis("Mouse X")` → horizontal mouse movement
- `Input.GetAxis("Mouse Y")` → vertical mouse movement
- `transform.Rotate()` → applies the rotation
- `speed` → controls rotation sensitivity

**Steps to run in Unity**

1. Create a Cube or Sphere (`GameObject → 3D Object → Cube`)
2. Create a script `SimpleMouseRotate.cs`
3. Paste the code above and save
4. Attach the script to the object
5. Press **Play**, then move the mouse — the object rotates

---

## 2. Rotate a Cube Continuously on the Y-Axis

```csharp
using UnityEngine;

public class RotateCube : MonoBehaviour
{
    public float speed = 50f; // Rotation speed

    void Update()
    {
        transform.Rotate(0f, speed * Time.deltaTime, 0f);
    }
}
```

**How it works**

- `transform.Rotate()` rotates the object each frame
- Only the Y value changes → rotation around the vertical axis
- `speed * Time.deltaTime` keeps the rotation smooth and frame-rate independent

**Steps to run in Unity**

1. Create a Cube (`GameObject → 3D Object → Cube`)
2. Create a script `RotateCube.cs`
3. Paste the code above and save
4. Attach it to the Cube
5. Press **Play** — the cube spins around its vertical axis

---

## 3. Smooth Movement with Arrow Keys

```csharp
using UnityEngine;

public class SmoothMove : MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal"); // Left / Right arrows
        float moveZ = Input.GetAxis("Vertical");   // Up / Down arrows

        Vector3 movement = new Vector3(moveX, 0f, moveZ);
        transform.Translate(movement * speed * Time.deltaTime);
    }
}
```

**How it works**

- `Input.GetAxis("Horizontal")` → detects Left/Right arrow keys
- `Input.GetAxis("Vertical")` → detects Up/Down arrow keys
- `Vector3` stores the movement direction
- `Translate()` moves the object
- `Time.deltaTime` makes motion smooth and frame-rate independent

**Steps to run in Unity**

1. Create a Cube or Player object
2. Create a script `SmoothMove.cs`
3. Paste the code above and save
4. Attach the script to the object
5. Press **Play**, then use the arrow keys to move
