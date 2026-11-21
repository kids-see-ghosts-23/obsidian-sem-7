---
tags: [vrar, lab, code]
---
# Program 6 - Immersive Environment with Video and Audio

## Aim

Create an immersive environment with only static game objects. 3D game objects can be created using Blender or taken from available 3D models, and a video and audio source are added.

## What the program does

- The scene is built from static objects: a floor, walls, props and an imported 3D model.
- A Quad acts as a video screen and plays a video clip.
- An AudioSource plays looping background audio.
- The player can walk and look around.
- Space pauses or resumes the video; H mutes or unmutes the audio.

## Scene setup

1. Create a new 3D project.
2. Build the environment:
   - `GameObject -> 3D Object -> Plane` for the floor, scale `(5, 1, 5)`.
   - Add Cubes as walls, pillars or props.
   - Import a 3D model from the Asset Store or a Blender FBX export and place it in the scene.
3. Create the video screen:
   - `GameObject -> 3D Object -> Quad`, position it on a wall.
   - Import a video file (for example MP4) into Assets.
   - Add a `Video Player` component to the Quad.
   - Assign the video clip.
   - Set `Render Mode = Material Override` and drag the Quad's `Mesh Renderer` into the Renderer field.
   - If the video has its own sound, set `Audio Output Mode = Audio Source` and assign an AudioSource. The separate AudioSource below is for background audio.
4. Create the audio:
   - Add an `AudioSource` to any object.
   - Assign an audio clip.
   - Leave `Play On Awake` unchecked; the script starts it.
5. Create the player:
   - `GameObject -> 3D Object -> Capsule`, position `(0, 1, 0)`.
   - Make the Main Camera a child of the Capsule, local position `(0, 0.7, 0)`.
6. Create `ImmersiveScene.cs` and attach it to the Capsule.
7. Drag the `VideoPlayer` and `AudioSource` into the script fields.
8. Press Play. Use WASD and the mouse. Press Space to pause/resume the video and H to mute/unmute the audio.

## Program file

`ImmersiveScene.cs`

```csharp
using UnityEngine;
using UnityEngine.Video;

public class ImmersiveScene : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float lookSpeed = 2f;
    public VideoPlayer videoPlayer;
    public AudioSource audioSource;

    float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        videoPlayer.Play();
        audioSource.loop = true;
        audioSource.Play();
    }

    void Update()
    {
        // Move with WASD
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(x, 0f, z) * moveSpeed * Time.deltaTime);

        // Look with the mouse
        float mouseX = Input.GetAxis("Mouse X") * lookSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * lookSpeed;

        xRotation = Mathf.Clamp(xRotation - mouseY, -80f, 80f);
        Camera.main.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

        // Space: play or pause the video
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (videoPlayer.isPlaying)
                videoPlayer.Pause();
            else
                videoPlayer.Play();
        }

        // H: mute or unmute the audio
        if (Input.GetKeyDown(KeyCode.H))
            audioSource.mute = !audioSource.mute;
    }
}
```

## How the code works

| Code | Meaning |
|---|---|
| `using UnityEngine.Video;` | Required for the `VideoPlayer` class. |
| `public VideoPlayer videoPlayer;` | Reference to the Video Player component on the screen. |
| `public AudioSource audioSource;` | Reference to the audio source. |
| `videoPlayer.Play();` | Starts video playback. |
| `audioSource.loop = true;` | Repeats the audio clip forever. |
| `audioSource.Play();` | Starts the audio. |
| `Input.GetAxis("Horizontal")` / `"Vertical"` | WASD or arrow-key movement. |
| `transform.Translate(...)` | Moves the player. |
| `Camera.main.transform.localRotation` | Up/down mouse look. |
| `transform.Rotate(Vector3.up * mouseX)` | Left/right turning of the player. |
| `videoPlayer.isPlaying` | True while the video is running. |
| `videoPlayer.Pause()` | Pauses the video. |
| `audioSource.mute = !audioSource.mute;` | Toggles mute on and off. |

The lab-manual version puts movement, look and media handling in `HandleMovement()`, `HandleLook()` and `HandleMedia()`. This version keeps the same logic inside `Update()` so it is shorter.

## Controls

| Key | Action |
|---|---|
| W / S | Move forward / backward |
| A / D | Move left / right |
| Mouse | Look around |
| Space | Pause / resume the video |
| H | Mute / unmute the audio |
| Esc | Release the cursor in the Editor |

## Expected output

- The player can walk through the static environment and look around.
- The video plays on the Quad screen.
- The background audio plays in a loop.
- Space pauses and resumes the video.
- H mutes and unmutes the audio.
- The data-sheet sketch shows a cube/model, a video screen, an audio source and a ball in the scene.

## Quick viva

1. **What is an immersive environment?** A 3D world that surrounds the player and makes him feel present in it.
2. **What are static game objects?** Objects that do not move or change at runtime; they form the scenery.
3. **What is a VideoPlayer?** A Unity component that plays video clips on a camera, a render texture or a material.
4. **What are the VideoPlayer render modes?** Camera Near Plane, Render Texture, Material Override and API Only.
5. **How is audio added?** With an `AudioSource` and an `AudioClip`; `Spatial Blend` controls 2D vs 3D sound.
6. **How do you pause a video?** `videoPlayer.Pause();` and resume with `videoPlayer.Play();`.
7. **Why use `loop = true`?** To keep the background audio playing continuously.
8. **What is the difference between static and dynamic objects?** Static objects do not move; dynamic objects have movement, physics or animation.
