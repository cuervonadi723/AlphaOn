using UnityEngine;

public class RadioInteraccion : MonoBehaviour
{
    public GameObject textoE;
    public CraftingSystem crafting;

    public bool antenaReparada = false;
    public bool yaEscuchoMuelle = false;

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
            crafting.MostrarMensaje(
                "Radio: ssszzzz... Ignacio, fui a revisar la antena. Sin señal no vamos a salir de acá."
            );
            return;
        }

        if (!yaEscuchoMuelle)
        {
            crafting.MostrarMensaje("Radio: szzszz... señal débil... el muelle... hay algo en la casa del muelle...");

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

        if (crafting != null)
            crafting.MostrarMensaje("Está oscureciendo... necesito hacer una fogata y preparar un lugar para dormir. Mañana voy a poder seguir hasta el muelle.");
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