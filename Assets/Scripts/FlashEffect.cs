using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class ShaderFadeEffect : MonoBehaviour
{
    [SerializeField] private Material fadeMaterial;
    [SerializeField] private float fadeDuration = 2.0f;

    private Coroutine fadeCoroutine;

    // 渐隐效果
    public void FadeOut()
    {
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }

        fadeCoroutine = StartCoroutine(Fade(fadeMaterial, 1.0f, 0.0f, fadeDuration));
    }

    // 渐显效果
    public void FadeIn()
    {
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }

        fadeCoroutine = StartCoroutine(Fade(fadeMaterial, 0.0f, 1.0f, fadeDuration));
    }

    private IEnumerator Fade(Material material, float fromValue, float toValue, float duration)
    {
        float elapsedTime = 0.0f;
        float startValue = fromValue;
        float endValue = toValue;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Lerp(0f, 1f, elapsedTime / duration);
            material.SetFloat("_FadeAmount", Mathf.Lerp(startValue, endValue, t));
            yield return null;
        }

        material.SetFloat("_FadeAmount", endValue);
    }
}