---
tags: [vrar, lab, moc]
---
# VRAR Lab - Six Programs

Short, exam-friendly versions of the six lab programs. Each folder contains one Unity C# script and one note with the aim, setup steps, code explanation, expected output and viva questions.

| # | Program | Script | Key API | Note |
|---|---|---|---|---|
| 1 | Transformations on Cube, Plane and Sphere | `TransformController.cs` | `Translate`, `Rotate`, `localScale` | [Program 1](program-1-transformations/Program-1.md) |
| 2 | VR UI menu with Images, Canvas, Sprites and Buttons | `VRMenuController.cs` | `Button.onClick`, `Image.sprite` | [Program 2](program-2-vr-ui-menu/Program-2.md) |
| 3 | First Person Controller | `FirstPersonController.cs` | `Input.GetAxis`, `Camera.main` | [Program 3](program-3-first-person-controller/Program-3.md) |
| 4 | Finding a target with a 2D Raycaster | `Raycast2DTarget.cs` | `Physics2D.Raycast` | [Program 4](program-4-2d-raycaster/Program-4.md) |
| 5 | Marker-based AR app | `MarkerModelPlacer.cs` | `ARTrackedImageManager` | [Program 5](program-5-marker-ar/Program-5.md) |
| 6 | Immersive scene with video and audio | `ImmersiveScene.cs` | `VideoPlayer`, `AudioSource` | [Program 6](program-6-immersive-scene/Program-6.md) |

## Ten-minute revision

| # | Remember this one line |
|---|---|
| 1 | Move with `Translate`, turn with `Rotate`, resize with `localScale`. |
| 2 | A button click calls a method that changes `statusText.text` and `displayImage.sprite`. |
| 3 | Mouse X/Y turns the camera; Horizontal/Vertical moves the player. |
| 4 | `Physics2D.Raycast(origin, direction, distance)` returns a `RaycastHit2D`. |
| 5 | `ARTrackedImageManager` detects the marker; place the model at `image.transform.position`. |
| 6 | `VideoPlayer.Play()` and `AudioSource.Play()` bring the static scene to life. |

## Common Unity notes

- The class name must match the file name. For example, `TransformController` must be in `TransformController.cs`.
- Create the script in the Project window, paste the code, save, then drag it onto the GameObject named in the note.
- Public fields appear in the Inspector. Drag the scene objects into those slots before pressing Play.
- If `Input.GetAxis` gives an Input System error, go to `Edit -> Project Settings -> Player -> Active Input Handling` and select `Both`.
- Use `Window -> General -> Console` to see `Debug.Log` output.
- Program 5 needs AR Foundation and an Android phone. The normal Editor does not show a real camera feed without XR Simulation.
- Program 6 needs a video clip and an audio clip imported into the project.

## Folder layout

```text
vrar/lab/
  program-1-transformations/
  program-2-vr-ui-menu/
  program-3-first-person-controller/
  program-4-2d-raycaster/
  program-5-marker-ar/
  program-6-immersive-scene/
```
