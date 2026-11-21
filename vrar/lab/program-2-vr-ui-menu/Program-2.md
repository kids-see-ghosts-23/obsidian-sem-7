---
tags: [vrar, lab, code]
---
# Program 2 - VR UI Menu with Images, Canvas, Sprites and Buttons

## Aim

Develop a user interface menu with Images, Canvas, Sprites and Buttons, and interact with the UI menu through a VR trigger button so that each successful trigger interaction is displayed on the scene.

## What the program does

- A Canvas holds an Image, a status Text and three Buttons: Start, Settings and Exit.
- Each button changes the image and updates the status text with an interaction count.
- In VR, the controller ray points at a button and the trigger acts as a click.
- In the Editor, a normal mouse click does the same thing.

## Unity setup (UI)

1. Create a new 3D project.
2. Import three images into Assets. Select each image, set `Texture Type = Sprite (2D and UI)`, and click Apply.
3. `GameObject -> UI -> Canvas`. Set `Render Mode = World Space` for VR. Set Width 800, Height 600 and Scale 0.005 (adjust to taste). Place it about 2 m in front of the camera.
4. Under the Canvas add:
   - `UI -> Image` and rename it to `DisplayImage`
   - `UI -> Text` (Text - Legacy) and rename it to `StatusText`
   - three `UI -> Button` objects named `StartButton`, `SettingsButton` and `ExitButton`
5. Change the button labels to Start, Settings and Exit.
6. Create the script `VRMenuController.cs` and attach it to the Canvas.
7. Drag the references into the script fields in the Inspector. For `Sprites`, set Size = 3 and assign the three sprites (0 = Start, 1 = Settings, 2 = Exit).
8. Press Play and click the buttons with the mouse. The image and the text change.

## VR trigger setup (XR Interaction Toolkit)

1. Install `XR Interaction Toolkit` from the Package Manager.
2. Add an `XR Origin` (or XR Rig) with hand/controller interactors.
3. Make sure the scene has an `EventSystem` (Unity creates one with the Canvas). Replace its `Standalone Input Module` with the `XR UI Input Module`.
4. Add `Tracked Device Graphic Raycaster` to the Canvas.
5. Add an `XR Ray Interactor` to the controller and enable UI interaction.
6. Build to the headset, point the ray at a button and press the trigger.

## Program file

`VRMenuController.cs`

```csharp
using UnityEngine;
using UnityEngine.UI;

public class VRMenuController : MonoBehaviour
{
    public Button startButton;
    public Button settingsButton;
    public Button exitButton;
    public Text statusText;
    public Image displayImage;
    public Sprite[] sprites;

    int count = 0;

    void Start()
    {
        startButton.onClick.AddListener(() => OnPressed("Start", 0));
        settingsButton.onClick.AddListener(() => OnPressed("Settings", 1));
        exitButton.onClick.AddListener(() => OnPressed("Exit", 2));
        statusText.text = "Point and press trigger";
    }

    void OnPressed(string name, int index)
    {
        count++;
        statusText.text = "Interaction " + count + ": " + name;
        displayImage.sprite = sprites[index];
    }
}
```

## How the code works

| Code | Meaning |
|---|---|
| `public Button startButton;` | Reference to a UI Button, assigned in the Inspector. |
| `public Image displayImage;` | The UI Image whose sprite is changed. |
| `public Sprite[] sprites;` | Array of images; index 0, 1, 2 matches Start, Settings and Exit. |
| `Button.onClick.AddListener(...)` | Registers a method to run when the button is clicked or triggered in VR. |
| `() => OnPressed("Start", 0)` | A lambda that calls `OnPressed` with fixed arguments. |
| `count++` | Counts successful interactions. |
| `statusText.text = ...` | Updates the on-screen message. |
| `displayImage.sprite = sprites[index]` | Swaps the displayed image. |

The lab-manual version uses `TextMeshProUGUI` and `using TMPro;`. If your Canvas uses TextMeshPro, replace `public Text statusText;` with `public TMP_Text statusText;` and add `using TMPro;`. The rest of the code is unchanged.

## Expected output

- Start screen text: `Point and press trigger`.
- Press Start: `Interaction 1: Start` and the first sprite appears.
- Press Settings: `Interaction 2: Settings` and the second sprite appears.
- Press Exit: `Interaction 3: Exit` and the third sprite appears.
- The data-sheet sketch shows an Image at the top, a "Display Msg" text and the Start, Settings and Exit buttons below it.

## Quick viva

1. **What is a Canvas?** The container for all UI elements. Render modes: Screen Space Overlay, Screen Space Camera and World Space.
2. **What is a Sprite?** A 2D image used by the UI Image component.
3. **Difference between Image and RawImage?** `Image` displays a Sprite; `RawImage` displays a Texture directly.
4. **How does a VR trigger click a UI button?** The controller has a ray interactor; the Canvas has a Graphic Raycaster; the EventSystem sends the click to the button.
5. **What does `AddListener` do?** It registers a callback that runs when the button is clicked.
6. **Why use a World Space Canvas for VR?** So the menu exists in 3D space and the controller ray can point at it.
