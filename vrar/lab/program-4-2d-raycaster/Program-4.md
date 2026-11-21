---
tags: [vrar, lab, code]
---
# Program 4 - Finding a Target with a 2D Raycaster

## Aim

Develop a program for finding a target using a 2D Raycaster.

## What the program does

- A 2D "Source" object shoots a ray towards the mouse pointer.
- A "Target" sprite with a 2D collider sits in the scene.
- A red line shows the ray in the Scene view.
- When the ray hits the target on a mouse click, the Console prints the target name and distance, and the target turns green.

## Scene setup

1. Create a new **2D** project (or a 3D project with an Orthographic camera).
2. `GameObject -> 2D Object -> Sprites -> Circle`, name it `Source`, position `(-5, 0, 0)`.
3. `GameObject -> 2D Object -> Sprites -> Square`, name it `Target`, position `(3, 0, 0)`.
4. Select `Target` and add a `Box Collider 2D` component.
5. Attach `Raycast2DTarget.cs` to the `Source` object.
6. Press Play. Move the mouse to aim the ray. Click the Target.
7. Open `Window -> General -> Console` to see the hit message.
8. Enable Gizmos in the Scene view to see the red ray.

## Program file

`Raycast2DTarget.cs`

```csharp
using UnityEngine;

public class Raycast2DTarget : MonoBehaviour
{
    public float rayDistance = 10f;

    void Update()
    {
        Vector2 origin = transform.position;
        Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mouse - origin).normalized;

        // Red line in the Scene view
        Debug.DrawRay(origin, direction * rayDistance, Color.red);

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(origin, direction, rayDistance);

            if (hit.collider != null)
            {
                Debug.Log("Hit: " + hit.collider.name + " | Distance: " + hit.distance);
                hit.collider.GetComponent<SpriteRenderer>().color = Color.green;
            }
        }
    }
}
```

## How the code works

| Code | Meaning |
|---|---|
| `Vector2 origin = transform.position;` | The ray starts at the Source object. |
| `Camera.main.ScreenToWorldPoint(Input.mousePosition)` | Converts the mouse position on the screen into a position in the 2D world. |
| `(mouse - origin).normalized` | Direction from the Source to the mouse, reduced to length 1. |
| `Debug.DrawRay(...)` | Draws the ray in the Scene view for debugging. |
| `Input.GetMouseButtonDown(0)` | Runs the check only on the left mouse click. |
| `Physics2D.Raycast(origin, direction, rayDistance)` | Shoots the 2D ray and returns information about the first collider it hits. |
| `hit.collider != null` | True when something was hit. |
| `hit.collider.name` | Name of the hit object. |
| `hit.distance` | Distance from the ray origin to the hit point. |
| `SpriteRenderer.color = Color.green` | Changes the target's colour as the "action" after a successful hit. |

The lab-manual version stores the hit in `RaycastHit2D` and uses `Debug.Log` and `Debug.DrawRay` in the same way. This version calculates the direction towards the mouse so the demo is easier to see.

## Expected output

- A red ray starts at the Source and points towards the mouse.
- Clicking the Target prints `Hit: Target | Distance: ...` in the Console.
- The Target sprite turns green.
- The data-sheet sketch shows a Source, a ray of light and a Target.

## Quick viva

1. **What is raycasting?** Shooting an invisible ray into the scene to detect colliders along its path.
2. **Difference between `Physics.Raycast` and `Physics2D.Raycast`?** `Physics` is for 3D colliders; `Physics2D` is for 2D colliders.
3. **What is `RaycastHit2D`?** A struct that stores the collider, point, distance and normal of the hit.
4. **Why normalize the direction?** So the vector has length 1 and `rayDistance` is measured correctly.
5. **Which component must the target have?** A 2D collider, such as `BoxCollider2D`.
6. **Where does `Debug.DrawRay` appear?** In the Scene view, not in the Game view.
7. **What happens if nothing is hit?** `hit.collider` is `null`, so the `if` block is skipped.
