using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
public class EndingController : MonoBehaviour
{
    private static EndingController instance; 
    private GameObject ImgGood;
    private GameObject ImgBad;
    private bool isGood;
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
           private void Awake()
       {
           if (instance == null)
           {
               instance = this;
               DontDestroyOnLoad(gameObject); // 不销毁该对象
           }
           else
           {
               Destroy(gameObject); // 如果已经存在实例，则销毁新创建的对象
           }
       }
    private void OnEnable()
    {

    }
    private void Start()
    {
        StartCoroutine(FindImages());
    }
    private IEnumerator FindImages()
{
    // 等待一帧，确保场景完全加载
    yield return null;

    ImgGood = GameObject.Find("bgGood");
    ImgBad = GameObject.Find("bgBad");

    // 继续执行其他逻辑
    if (ImgBad == null || ImgGood == null) yield return null; // 如果任一物体未找到，退出

    Debug.Log(isGood);
    if (isGood)
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
    public void SetEndStatus(bool IsGood)
    {
        isGood = IsGood;
        Debug.Log(isGood);
    }
}
