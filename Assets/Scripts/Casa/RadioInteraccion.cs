using UnityEngine;

public class RadioInteraccion : MonoBehaviour
{
    public GameObject textoE;
    public CraftingSystem crafting;

    public bool antenaReparada = false;
    public bool yaEscuchoMuelle = false;

    public PensamientoJugador pensamiento;
    private bool yaPensoAntena = false;

    private bool jugadorCerca = false;

    [Header("Audios")]
    public AudioSource audioSource;
    public AudioClip radioRota;
    public AudioClip radioReparada;
    public AmbienteNoche ambienteNoche;

    [Header("Noche")]
    public Light luzSol;
    public float intensidadNoche = 0.15f;

    void Start()
    {
        if (textoE != null)
            textoE.SetActive(false);
    }

    void Update()
    {
        if (jugadorCerca && Input.GetKeyDown(KeyCode.E))
        {
            UsarRadio();
        }
    }

    void UsarRadio()
    {
        if (!antenaReparada)
        {
            ReproducirAudio(radioRota);

            if (crafting != null)
            {
                crafting.MostrarMensaje(
                    "Sin señal... solo se escucha estática..."
                );
            }

            if (!yaPensoAntena && pensamiento != null)
            {
                pensamiento.MostrarPensamiento(
                    "Tal vez Ignacio tenía razón. Debería revisar la antena."
                );

                yaPensoAntena = true;
            }

            return;
        }

        if (!yaEscuchoMuelle)
        {
            ReproducirAudio(radioReparada);

            if (crafting != null)
            {
                crafting.MostrarMensaje(
                    "¿Me recibe?... aquí central de rescate..."
                );
            }

            yaEscuchoMuelle = true;

            if (ProgresoAntena.instance != null)
                ProgresoAntena.instance.debeDormir = true;

            Invoke(nameof(MensajeRadioParte2), 5f);
            Invoke(nameof(MensajeNoche), 10f);

            return;
        }

        ReproducirAudio(radioRota);

        if (crafting != null)
        {
            crafting.MostrarMensaje(
                "Radio: Solo se escucha estática..."
            );
        }
    }

    void MensajeRadioParte2()
    {
        if (crafting != null)
        {
            crafting.MostrarMensaje(
                "Señal recuperada... manténgase en la zona... enviaremos ayuda al amanecer."
            );
        }
    }

    void ReproducirAudio(AudioClip clip)
    {
        if (audioSource == null || clip == null)
            return;

        audioSource.Stop();
        audioSource.PlayOneShot(clip);
    }

    void MensajeNoche()
    {
        if (luzSol != null)
            luzSol.intensity = intensidadNoche;

        if (ambienteNoche != null)
            ambienteNoche.ActivarNoche();


        if (pensamiento != null)
        {
            pensamiento.MostrarPensamiento(
                "Está oscureciendo... necesito hacer una fogata y preparar un lugar para dormir."
            );
        }
    }

    public void ActivarSenal()
    {
        antenaReparada = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = true;

            if (textoE != null)
                textoE.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = false;

            if (textoE != null)
                textoE.SetActive(false);
        }
    }
}