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

    [Header("Noche")] //xD es mucho mas prolijo asi jaja
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
            crafting.MostrarMensaje("Radio: ssszzzz... sin señal... solo se escucha estática...");

            if (!yaPensoAntena && pensamiento != null)
            {
                pensamiento.MostrarPensamiento("Tal vez Ignacio tenía razón. Debería revisar la antena.");
                yaPensoAntena = true;
            }

            return;
        }

        if (!yaEscuchoMuelle)
        {
            crafting.MostrarMensaje("Radio: ...¿me recibe?... aquí central de rescate... señal recuperada... manténgase en la zona... enviaremos ayuda al amanecer...");

            yaEscuchoMuelle = true;

            if (ProgresoAntena.instance != null)
                ProgresoAntena.instance.debeDormir = true;

            Invoke(nameof(MensajeNoche), 5f);

            return;
        }

        crafting.MostrarMensaje("Radio: Solo se escucha estática...");
    }

    void MensajeNoche()
    {
        if (luzSol != null)
            luzSol.intensity = intensidadNoche;

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