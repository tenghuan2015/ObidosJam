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
        quence.Append(DOTweenModuleUI.DOText(text1, "It's a quiet night, and Luna stays alone in her small apartment, ", 0.0f, true).SetEase(Ease.Linear).SetAutoKill(true));
        quence.AppendInterval(5.0f);
        quence.AppendCallback(() => text1.text = ""); // 清空 text1 的内容
        quence.Append(DOTweenModuleUI.DOText(text1, "ready to enjoy an evening of peace. ", 0.0f, true).SetEase(Ease.Linear).SetAutoKill(true));
        quence.AppendInterval(3.0f);
        quence.AppendCallback(() => text1.text = ""); // 清空 text1 的内容
        quence.Append(DOTweenModuleUI.DOFade(bg2, 255f, 0f));
        
        
        quence.Append(DOTweenModuleUI.DOText(text1, "But that peace is shattered when a stranger knocks the door.", 0.0f, true).SetEase(Ease.Linear).SetAutoKill(true));
        quence.AppendInterval(6.0f);
        quence.AppendCallback(() => text1.text = ""); // 清空 text1 的内容
        quence.Append(DOTweenModuleUI.DOText(text1, "His voice grows louder, more threatening, as he tries to break in. ", 0.0f, true).SetEase(Ease.Linear).SetAutoKill(true));
        quence.AppendInterval(6.0f);
        quence.AppendCallback(() => text1.text = ""); // 清空 text1 的内容
        quence.Append(DOTweenModuleUI.DOFade(bg3, 255f, 0f));
        
        
        quence.Append(DOTweenModuleUI.DOText(text1, "With no one around, Luna must dial for help before it’s too late. ", 0.0f, true).SetEase(Ease.Linear));
        quence.AppendInterval(6.0f);
        quence.AppendCallback(() => text1.text = ""); // 清空 text1 的内容
        quence.Append(DOTweenModuleUI.DOText(text1, "Time is running out, and every decision counts. ", 0.0f, true).SetEase(Ease.Linear));
        quence.AppendInterval(5.0f);
        quence.AppendCallback(() => text1.text = ""); // 清空 text1 的内容
        quence.Append(DOTweenModuleUI.DOText(text1, "Will you save her?", 0.0f, true).SetEase(Ease.Linear));

        quence.AppendInterval(0.5f);
        //quence.Append(DOTweenModuleUI.DOText(text3,"Will you be able to save her?", 5.0f, true).SetRelative().SetEase(Ease.Linear));
        quence.InsertCallback(41.5f, () =>
        {
            SceneManager.LoadScene("dialFriend");
        });
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
