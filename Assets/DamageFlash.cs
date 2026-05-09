using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DamageFlashUI : MonoBehaviour
{
    public Image flashImage;
    public float flashAlpha = 0.45f;
    public float fadeSpeed = 2.5f;

    private Coroutine flashCoroutine;

    public void ShowFlash()
    {
        if (flashCoroutine != null)
        {
            StopCoroutine(flashCoroutine);
        }

        flashCoroutine = StartCoroutine(FlashRoutine());
    }

    IEnumerator FlashRoutine()
    {
        if (flashImage == null) yield break;

        Color color = flashImage.color;
        color.a = flashAlpha;
        flashImage.color = color;

        while (flashImage.color.a > 0.01f)
        {
            color = flashImage.color;
            color.a = Mathf.Lerp(color.a, 0f, fadeSpeed * Time.deltaTime);
            flashImage.color = color;
            yield return null;
        }

        color.a = 0f;
        flashImage.color = color;
    }
}