using System.Collections;
using UnityEngine;

public class UIZoom : MonoBehaviour
{
    public float escalaInicial = 0.92f;
    public float escalaFinal = 1f;
    public float velocidad = 10f;

    Coroutine animacion;

    public void ReproducirZoom()
    {
        if (animacion != null)
            StopCoroutine(animacion);

        animacion = StartCoroutine(Zoom());
    }

    IEnumerator Zoom()
    {
        transform.localScale = Vector3.one * escalaInicial;

        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * velocidad;
            float escala = Mathf.Lerp(escalaInicial, escalaFinal, t);

            transform.localScale = Vector3.one * escala;

            yield return null;
        }

        transform.localScale = Vector3.one * escalaFinal;
    }
}