---
tags: [vrar, lab, code]
---
# Program 3 - First Person Controller

## Aim

Develop a program for a First Person Controller in a scene.

## What the program does

- A capsule represents the player.
- The camera is placed at eye level as a child of the capsule.
- Moving the mouse turns the view and the player.
- WASD moves the player in the direction he is facing.

## Scene setup

1. Create a new 3D project.
2. `GameObject -> 3D Object -> Plane`. Scale it to `(5, 1, 5)` to make a large ground.
3. Add a few Cubes as obstacles or walls.
4. `GameObject -> 3D Object -> Capsule`. Set Position `(0, 1, 0)`.
5. Drag the Main Camera onto the Capsule so it becomes a child. Set its local Position to `(0, 0.7, 0)` and Rotation to `(0, 0, 0)`.
6. Make sure the camera is tagged `MainCamera` (Inspector -> Tag).
7. Create the script `FirstPersonController.cs` and attach it to the Capsule.
8. Press Play, click the Game view, and use the mouse and WASD. Press Esc to release the cursor.

## Program file

`FirstPersonController.cs`

```csharp
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float mouseSensitivity = 2f;
    float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Look with the mouse
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        Camera.main.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

        // Move with WASD / arrow keys
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;
        transform.position += move * moveSpeed * Time.deltaTime;
    }
}
```

## How the code works

| Code | Meaning |
|---|---|
| `Cursor.lockState = CursorLockMode.Locked;` | Hides and locks the cursor so the mouse can look around without leaving the window. |
| `Input.GetAxis("Mouse X")` | Horizontal mouse movement, used to turn the body left and right. |
| `Input.GetAxis("Mouse Y")` | Vertical mouse movement, used to look up and down. |
| `xRotation -= mouseY;` | Mouse up looks up, mouse down looks down. |
| `Mathf.Clamp(xRotation, -80f, 80f)` | Stops the camera from flipping over at the top and bottom. |
| `Camera.main.transform.localRotation = ...` | Rotates only the camera for up/down look. |
| `transform.Rotate(Vector3.up * mouseX);` | Rotates the whole player around the Y axis for left/right look. |
| `transform.right` and `transform.forward` | Local directions of the player, so W always moves where the player is facing. |
| `moveSpeed * Time.deltaTime` | Makes movement smooth and frame-rate independent. |

The lab-manual version uses `Camera.main.transform.localRotation` and `transform.Rotate` in the same way. This version removes the helper methods and keeps the logic in `Update()`.

## Controls

| Key | Action |
|---|---|
| Mouse | Look around |
| W / S | Move forward / backward |
| A / D | Move left / right |
| Esc | Release the cursor in the Editor |

## Expected output

- The player sees the scene from a first-person view.
- Moving the mouse looks around; the view stops at 80 degrees up and down.
- WASD moves the player over the Plane.
- The data-sheet sketch shows a capsule (the player) with a camera and a sun in the scene.

## Quick viva

1. **What is a First Person Controller?** A player object controlled from a first-person viewpoint.
2. **Why is the camera a child of the player?** It follows the player's position and left/right rotation automatically.
3. **Why clamp the X rotation?** To stop the camera from turning upside down.
4. **Difference between `transform.forward` and `Vector3.forward`?** `transform.forward` is the object's own facing direction; `Vector3.forward` is the world's positive Z direction.
5. **Why is the camera tagged `MainCamera`?** `Camera.main` searches for the camera with that tag.
6. **How would you add gravity?** Add a `Rigidbody` or use a `CharacterController` instead of changing `transform.position` directly.
