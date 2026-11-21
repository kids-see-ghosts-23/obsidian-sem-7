---
tags: [vrar, lab, code]
---
# Program 5 - Marker-Based AR App (Model on a Marker)

## Aim

Create a marker-based application that places a model on a plane by real-time detection.

## What the program does

- The phone camera looks for a printed marker image.
- AR Foundation reports the detected image through `ARTrackedImageManager`.
- When the marker is tracked, the 3D model is shown at the marker's position and rotation.
- The model stays on the marker while the marker is visible.

## Unity setup

1. Create a new 3D project.
2. `File -> Build Settings -> Android -> Switch Platform`.
3. Open `Window -> Package Manager`. Install:
   - `AR Foundation`
   - `Google ARCore XR Plugin` (use `ARKit XR Plugin` for iOS)
4. `Edit -> Project Settings -> XR Plug-in Management`. On the Android tab, enable `ARCore`.
5. Delete the Main Camera from the scene.
6. `GameObject -> XR -> AR Session`.
7. `GameObject -> XR -> XR Origin (Mobile AR)` (older versions call it XR Rig).
8. Create the marker library:
   - `Assets -> Create -> XR -> AR Foundation -> Reference Image Library`
   - Click `Add Image`, assign a high-contrast image (college logo, book cover, poster).
   - Give it a name and a physical width, for example 0.2 m.
9. Select `XR Origin`, add the `AR Tracked Image Manager` component, and assign the reference image library.
10. Create the 3D model in the scene (a Cube or an imported FBX). Keep it as a scene object, not a Project prefab asset, because the script moves that object directly. Drag it into the `Model` field of the script.
11. Create an empty GameObject named `ARManager`, attach `MarkerModelPlacer.cs`, and assign:
    - `Image Manager` = the AR Tracked Image Manager
    - `Model` = your 3D model
12. In `Player Settings` for Android, set Minimum API Level 24, Scripting Backend IL2CPP and Target Architectures ARM64.
13. Build and Run on an Android phone. Point the camera at the marker.

## Program file

`MarkerModelPlacer.cs`

```csharp
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class MarkerModelPlacer : MonoBehaviour
{
    public ARTrackedImageManager imageManager;
    public GameObject model;

    void Awake()
    {
        model.SetActive(false);
    }

    void OnEnable()
    {
        imageManager.trackedImagesChanged += OnImagesChanged;
    }

    void OnImagesChanged(ARTrackedImagesChangedEventArgs args)
    {
        foreach (ARTrackedImage image in args.added)
            Place(image);

        foreach (ARTrackedImage image in args.updated)
            Place(image);
    }

    void Place(ARTrackedImage image)
    {
        if (image.trackingState != TrackingState.Tracking)
            return;

        model.SetActive(true);
        model.transform.position = image.transform.position;
        model.transform.rotation = image.transform.rotation;
    }
}
```

## How the code works

| Code | Meaning |
|---|---|
| `ARTrackedImageManager` | AR Foundation component that detects images from the reference library. |
| `public GameObject model;` | The 3D model that should appear on the marker. |
| `model.SetActive(false);` | Hides the model until a marker is detected. |
| `trackedImagesChanged += OnImagesChanged;` | Subscribes to the event that fires when images are added or updated. |
| `args.added` | Images detected for the first time. |
| `args.updated` | Images whose position or tracking state changed. |
| `TrackingState.Tracking` | The image pose is valid, so the model can be placed. |
| `image.transform.position` | World position of the detected marker. |
| `image.transform.rotation` | World rotation of the detected marker. |

The lab-manual version checks `imageManager.trackables` inside `Update()`. This version uses the `trackedImagesChanged` event, which is shorter and only runs when AR reports a change.

## Expected output

- The phone shows the live camera feed.
- When the camera sees the marker, the 3D model appears on the marker.
- Moving the marker moves the model with it.
- The data-sheet sketch shows a marker image on a table with a 3D object above it.

## Notes

- A normal Unity Editor does not show a real camera feed. Use a physical Android phone, or Unity's XR Simulation for a rough test.
- Print the marker on paper, or show it on another screen.
- If the model does not appear, check that the reference image library is assigned and that the marker has enough contrast.

## Quick viva

1. **What is marker-based AR?** AR that uses a recognized image or pattern to trigger digital content.
2. **What is AR Foundation?** A Unity package that provides a common API for ARCore (Android) and ARKit (iOS).
3. **What is a reference image library?** A set of images that the AR app can recognize.
4. **What is an `ARTrackedImage`?** A detected image with its pose and tracking state.
5. **What does `TrackingState.Tracking` mean?** The image is currently visible and its pose is valid.
6. **Why hide the model at the start?** So it does not appear in the scene before a marker is detected.
7. **Marker-based vs markerless AR?** Marker-based uses a known image/pattern; markerless uses planes and the environment.
8. **Could Vuforia be used instead?** Yes. The manual mentions Vuforia, but AR Foundation is free and uses the same idea.
