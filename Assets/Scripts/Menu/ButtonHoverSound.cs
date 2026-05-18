using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHoverSound : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    public AudioSource audioSource;
    public AudioClip sonido;

    public float volumenHover = 0.25f;
    public float volumenClick = 0.4f;

    public float pitchHover = 1f;
    public float pitchClick = 0.85f;

    public void OnPointerEnter(PointerEventData eventData)
    {
        audioSource.pitch = pitchHover;
        audioSource.PlayOneShot(sonido, volumenHover);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        audioSource.pitch = pitchClick;
        audioSource.PlayOneShot(sonido, volumenClick);
    }
}