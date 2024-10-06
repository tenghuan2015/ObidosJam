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
    [SerializeField] private Image bg1;
    [SerializeField] private Image bg2;
    [SerializeField] private Image bg3;
    [SerializeField] private Transform bg;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Sequence quence = DOTween.Sequence();
        quence.Append(DOTweenModuleUI.DOText(text1, "It¡¯s a quiet night, and Luna sits alone in her small apartment, ready to enjoy an evening of peace.\n ", 6.0f, true).SetEase(Ease.Linear).SetAutoKill(true));
        quence.Append(DOTweenModuleUI.DOFade(text1, 0f, 0.5f));
        //quence.Append(bg.DOMoveX(-19, 2f));
        quence.Append(DOTweenModuleUI.DOFade(bg2, 255f, 1f));
        quence.AppendInterval(0.5f);
        quence.Append(DOTweenModuleUI.DOText(text2, "But that peace is shattered when a stranger begins pounding on her door. His voice grows louder, more threatening, as he tries to break in. \n ", 12.0f, true).SetRelative().SetEase(Ease.Linear).SetAutoKill(true));
        quence.Append(DOTweenModuleUI.DOFade(text2, 0f, 0.5f));
        //quence.Append(bg.DOMoveX(-19, 2f));
        quence.Append(DOTweenModuleUI.DOFade(bg3, 255f, 1f));
        quence.AppendInterval(0.5f);
        quence.Append(DOTweenModuleUI.DOText(text3, "With no one around, Luna must find a way to signal for help before it¡¯s too late. \n Time is running out, and every decision counts.\n ", 10.0f, true).SetRelative().SetEase(Ease.Linear));
        //quence.Append(DOTweenModuleUI.DOFade(text3, 0f, 0.5f));
        //quence.Append(bg.DOMoveX(-19, 2f))
        quence.AppendInterval(0.5f);
        quence.Append(DOTweenModuleUI.DOText(text3,"Will you be able to save her?", 5.0f, true).SetRelative().SetEase(Ease.Linear));
        quence.InsertCallback(44.0f, () =>
        {
            SceneManager.LoadScene("dialFriend");
        });
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
