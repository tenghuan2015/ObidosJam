using UnityEngine;
using UnityEngine.UI;
public class EndingController : MonoBehaviour
{
    private static EndingController instance; // µ¥ÀýÊµÀý
    [SerializeField] private GameObject ImgGood;
    [SerializeField] private GameObject ImgBad;
    public static EndingController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<EndingController>();
                if (instance == null)
                {
                    GameObject obj = new GameObject("EndingController");
                    instance = obj.AddComponent<EndingController>();
                }
            }
            return instance;
        }
    }
    public void ShowEnding(bool IsGoodEnd)
    {
        if (IsGoodEnd)
        {
            ImgGood.SetActive(true);
            ImgBad.SetActive(false);
        }
        else
        {
            ImgGood.SetActive(false);
            ImgBad.SetActive(true);
        }
    }
}
