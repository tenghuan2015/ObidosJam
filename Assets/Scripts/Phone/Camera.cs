using UnityEngine;
using UnityEngine.UI;

public class MonitorViewController : MonoBehaviour
{
    public RawImage displayImage;
    public float stepSize = 100f; // 每次移动的步长
    public int maxSteps = 3; // 每个方向最大移动次数

    private Vector2 imagePosition;
    private Vector2Int currentSteps = Vector2Int.zero; // 当前在每个方向上移动的步数

    void Start()
    {
        if (displayImage == null)
        {
            Debug.LogError("Display Image not set in MonitorViewController");
            return;
        }

        imagePosition = displayImage.rectTransform.anchoredPosition;
    }

    void Update()
    {
        HandleInput();
        UpdateImageTransform();
    }


    void HandleInput()
    {
        string direction = PhoneInputController.Instance.GetLastMsg();

        if (direction.Equals("2") && currentSteps.y > -maxSteps)
        {
            PhoneInputController.Instance.PlayAudio("Assets/Sounds/Effect/ding.mp3");
            imagePosition.y -= stepSize;
            currentSteps.y--;
        }
        else if (direction.Equals("8") && currentSteps.y < maxSteps)
        {
            PhoneInputController.Instance.PlayAudio("Assets/Sounds/Effect/ding.mp3");
            imagePosition.y += stepSize;
            currentSteps.y++;
        }

        if (direction.Equals("4") && currentSteps.x > -maxSteps)
        {
            PhoneInputController.Instance.PlayAudio("Assets/Sounds/Effect/ding.mp3");
            imagePosition.x += stepSize;
            currentSteps.x--;
        }
        else if (direction.Equals("6") && currentSteps.x < maxSteps)
        {
            PhoneInputController.Instance.PlayAudio("Assets/Sounds/Effect/ding.mp3");
            imagePosition.x -= stepSize;
            currentSteps.x++;
        }
    }

    void UpdateImageTransform()
    {
        displayImage.rectTransform.anchoredPosition = imagePosition;
    }

}
