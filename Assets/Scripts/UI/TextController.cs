using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;
using DG.Tweening.Core;
using UnityEngine.SceneManagement;
public class TextController : MonoBehaviour
{
    [SerializeField] private Text text1;
    [SerializeField] private Text text2;
    [SerializeField] private Text text3;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Sequence quence = DOTween.Sequence();
        quence.Append(DOTweenModuleUI.DOText(text1, "It¡¯s a quiet night, and Luna sits alone in her small apartment, ready to enjoy an evening of peace.\n ", 6.0f, true).SetEase(Ease.Linear));
        quence.AppendInterval(0.5f);
        quence.Append(DOTweenModuleUI.DOText(text1, "But that peace is shattered when a stranger begins pounding on her door. <color=red>His voice grows louder, more threatening, as he tries to break in.</color> \n ", 12.0f, true).SetRelative().SetEase(Ease.Linear));
        quence.AppendInterval(0.5f);
        quence.Append(DOTweenModuleUI.DOText(text1, "With no one around, Luna must find a way to signal for help before it¡¯s too late. \n Time is running out, and every decision counts.\n ", 10.0f, true).SetRelative().SetEase(Ease.Linear));
        quence.AppendInterval(0.5f);
        quence.Append(DOTweenModuleUI.DOText(text1,"<color=red>Will you be able to save her?</color>", 5.0f, true).SetRelative().SetEase(Ease.Linear));
        quence.InsertCallback(38.0f, () =>
        {
            SceneManager.LoadScene("dialFriend");
        });
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
