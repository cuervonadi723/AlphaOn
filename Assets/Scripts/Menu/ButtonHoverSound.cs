using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

public class ButtonHoverSound : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip sonido;

    public float volumenHover = 0.25f;
    public float volumenClick = 0.4f;

    public float pitchHover = 1f;
    public float pitchClick = 0.85f;

    [Header("Visual")]
    public Image brillo;

    public float escalaHover = 1.03f;
    public float velocidad = 8f;
    public float alphaHover = 0.18f;

    private Vector3 escalaInicial;
    private Coroutine animacion;

    void Start()
    {
        escalaInicial = transform.localScale;

        if (brillo != null)
        {
            Color c = brillo.color;
            c.a = 0f;
            brillo.color = c;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        audioSource.pitch = pitchHover;
        audioSource.PlayOneShot(sonido, volumenHover);

        Animar(escalaInicial * escalaHover, alphaHover);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Animar(escalaInicial, 0f);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        audioSource.pitch = pitchClick;
        audioSource.PlayOneShot(sonido, volumenClick);
    }

    void Animar(Vector3 escalaDestino, float alphaDestino)
    {
        if (animacion != null)
            StopCoroutine(animacion);

        animacion = StartCoroutine(AnimacionHover(escalaDestino, alphaDestino));
    }

    IEnumerator AnimacionHover(Vector3 escalaDestino, float alphaDestino)
    {
        while (Vector3.Distance(transform.localScale, escalaDestino) > 0.001f)
        {
            transform.localScale = Vector3.Lerp(
                transform.localScale,
                escalaDestino,
                Time.unscaledDeltaTime * velocidad
            );

            if (brillo != null)
            {
                Color c = brillo.color;

                c.a = Mathf.Lerp(
                    c.a,
                    alphaDestino,
                    Time.unscaledDeltaTime * velocidad
                );

                brillo.color = c;
            }

            yield return null;
        }

        transform.localScale = escalaDestino;
    }
}