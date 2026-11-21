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
