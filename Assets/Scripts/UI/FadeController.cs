using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeController : MonoBehaviour
{
    public Image fadeImage;
    public float velocidadFade = 1.5f;

    void Start()
    {
        StartCoroutine(FadeIn());
    }

    public void CambiarEscena(string nombreEscena)
    {
        StartCoroutine(FadeOut(nombreEscena));
    }

    IEnumerator FadeIn()
    {
        float alpha = 1f;

        while (alpha > 0)
        {
            alpha -= Time.deltaTime * velocidadFade;

            Color color = fadeImage.color;
            color.a = alpha;
            fadeImage.color = color;

            yield return null;
        }

        Color final = fadeImage.color;
        final.a = 0;
        fadeImage.color = final;
    }

    IEnumerator FadeOut(string nombreEscena)
    {
        float alpha = 0f;

        while (alpha < 1)
        {
            alpha += Time.deltaTime * velocidadFade;

            Color color = fadeImage.color;
            color.a = alpha;
            fadeImage.color = color;

            yield return null;
        }

        SceneManager.LoadScene(nombreEscena);
    }
}