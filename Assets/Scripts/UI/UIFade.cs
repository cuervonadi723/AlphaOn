using System.Collections;
using UnityEngine;

public class UIFade : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public float velocidadFade = 6f;

    Coroutine fadeActual;

    void Awake()
    {
        if (canvasGroup == null)
            canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Mostrar()
    {
        gameObject.SetActive(true);

        if (fadeActual != null)
            StopCoroutine(fadeActual);

        fadeActual = StartCoroutine(Fade(1f));
    }

    public void Ocultar()
    {
        if (fadeActual != null)
            StopCoroutine(fadeActual);

        fadeActual = StartCoroutine(Fade(0f));
    }

    IEnumerator Fade(float objetivo)
    {
        float inicio = canvasGroup.alpha;
        float tiempo = 0f;

        while (tiempo < 1f)
        {
            tiempo += Time.deltaTime * velocidadFade;

            canvasGroup.alpha = Mathf.Lerp(
                inicio,
                objetivo,
                tiempo
            );

            yield return null;
        }

        canvasGroup.alpha = objetivo;

        if (objetivo == 0f)
            gameObject.SetActive(false);
    }
}