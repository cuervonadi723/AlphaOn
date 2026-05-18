using UnityEngine;

public class RadioInteraccion : MonoBehaviour
{
    public GameObject textoE;
    public CraftingSystem crafting;

    public bool antenaReparada = false;
    public bool yaEscuchoMuelle = false;

    private bool jugadorCerca = false;

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
            crafting.MostrarMensaje("Radio: ssszzzz... szsszszsz... Matías, fui a revisar la antena. Sin señal no vamos a salir de acá.");
            return;
        }

        if (!yaEscuchoMuelle)
        {
            crafting.MostrarMensaje("Radio: szzszz... señal débil... el muelle... hay algo en la casa del muelle...");
            yaEscuchoMuelle = true;
            return;
        }

        crafting.MostrarMensaje("Radio: Solo se escucha estática...");
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