using UnityEngine;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    [Header("Referencias Audio")]
    public AudioSource musica;
    public AudioSource efectos;

    [Header("Sliders")]
    public Slider sliderMusica;
    public Slider sliderEfectos;

    [Header("Multiplicadores")]
    public float multiplicadorMusica = 2f;
    public float multiplicadorEfectos = 1f;

    private const string KEY_MUSICA = "volumenMusica";
    private const string KEY_EFECTOS = "volumenEfectos";

    void Start()
    {
        float volumenMusicaGuardado = PlayerPrefs.GetFloat(KEY_MUSICA, 0.4f);
        float volumenEfectosGuardado = PlayerPrefs.GetFloat(KEY_EFECTOS, 0.5f);

        musica.volume = volumenMusicaGuardado * multiplicadorMusica;
        efectos.volume = volumenEfectosGuardado * multiplicadorEfectos;

        if (sliderMusica != null)
            sliderMusica.SetValueWithoutNotify(volumenMusicaGuardado);

        if (sliderEfectos != null)
            sliderEfectos.SetValueWithoutNotify(volumenEfectosGuardado);
    }

    public void CambiarVolumenMusica(float valor)
    {
        musica.volume = valor * multiplicadorMusica;

        PlayerPrefs.SetFloat(KEY_MUSICA, valor);
        PlayerPrefs.Save();
    }

    public void CambiarVolumenEfectos(float valor)
    {
        efectos.volume = valor * multiplicadorEfectos;

        PlayerPrefs.SetFloat(KEY_EFECTOS, valor);
        PlayerPrefs.Save();
    }
}