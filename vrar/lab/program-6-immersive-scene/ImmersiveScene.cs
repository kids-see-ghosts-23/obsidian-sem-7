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
