---
tags: [vrar, lab, code]
---
# Program 1 - Transformations on Cube, Plane and Sphere

## Aim

Develop a scene in Unity that includes a Cube, a Plane and a Sphere, and apply transformations on the three game objects.

## What the program does

- The Plane acts as the ground.
- The Cube and the Sphere sit on the Plane.
- One short script moves, rotates and scales whichever object it is attached to.
- The same script is attached to both objects with different `moveSpeed` values, so they move at different speeds.

## Scene setup

1. Create a new **3D** project in Unity.
2. `GameObject -> 3D Object -> Plane`. Set Position `(0, 0, 0)` and Scale `(2, 1, 2)`.
3. `GameObject -> 3D Object -> Cube`. Set Position `(0, 0.5, 0)`.
4. `GameObject -> 3D Object -> Sphere`. Set Position `(2, 0.5, 0)`.
5. In the Project window: `Create -> C# Script`, name it `TransformController`.
6. Paste the code below and save.
7. Drag `TransformController` onto the Cube and onto the Sphere.
8. Select the Cube, set `Move Speed = 3`. Select the Sphere, set `Move Speed = 6`.
9. Press **Play**, click inside the Game view, and use the keys below.

The script can be attached to any GameObject. The Plane is only the ground, so it does not need the script.

## Program file

`TransformController.cs`

```csharp
using UnityEngine;

public class TransformController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotateSpeed = 60f;
    public float scaleSpeed = 2f;

    void Update()
    {
        // Move on the XZ plane with WASD / arrow keys
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(x, 0f, z) * moveSpeed * Time.deltaTime);

        // Rotate around the Y axis with Q and E
        if (Input.GetKey(KeyCode.Q))
            transform.Rotate(Vector3.up, -rotateSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.E))
            transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);

        // Scale with R and F
        if (Input.GetKey(KeyCode.R))
            transform.localScale += Vector3.one * scaleSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.F))
            transform.localScale -= Vector3.one * scaleSpeed * Time.deltaTime;
    }
}
```

## How the code works

| Code | Meaning |
|---|---|
| `public float moveSpeed = 5f;` | Speed of movement. It is `public`, so it can be changed in the Inspector for each object. |
| `Input.GetAxis("Horizontal")` | Returns -1 for A/Left, +1 for D/Right, and 0 when nothing is pressed. |
| `Input.GetAxis("Vertical")` | Returns -1 for S/Down, +1 for W/Up. |
| `new Vector3(x, 0f, z)` | Movement direction on the ground. Y is 0, so the object does not fly. |
| `transform.Translate(...)` | Moves the object by the given offset. |
| `Time.deltaTime` | Time since the last frame. Multiplying by it makes the speed independent of the frame rate. |
| `transform.Rotate(Vector3.up, angle)` | Rotates around the Y (vertical) axis. |
| `transform.localScale` | Size of the object relative to its parent. |
| `Vector3.one` | Shorthand for `(1, 1, 1)`. Adding it increases all three scale axes together. |

The lab-manual version splits the code into `HandleMovement()`, `HandleRotation()` and `HandleScaling()`. This version calls the same logic directly from `Update()` to keep the program short.

## Controls

| Key | Action |
|---|---|
| W / S | Move forward / backward |
| A / D | Move left / right |
| Q / E | Rotate left / right around Y |
| R / F | Increase / decrease size |

## Expected output

- The Cube and the Sphere move on the Plane with WASD.
- Q and E rotate them around the vertical axis.
- R makes them bigger and F makes them smaller.
- Because the two objects have different Move Speed values, they move at different speeds.
- The data-sheet sketch shows a cube at the centre of the X, Y and Z axes; this is the transformed object in the scene.

## Quick viva

1. **What is a transformation?** A change in an object's position, rotation or scale.
2. **Difference between `Translate` and `position`?** `Translate` adds an offset to the current position; `position` sets an absolute world position.
3. **Why multiply by `Time.deltaTime`?** To keep movement smooth and independent of the frame rate.
4. **What is `localScale`?** Scale relative to the parent. `lossyScale` is the final world scale.
5. **Which axis is `Vector3.up`?** The positive Y axis.
6. **How do you move an object in all three directions?** Include a Y value in the movement vector, for example `new Vector3(x, y, z)`.
