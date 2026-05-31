using UnityEngine;

public class AntenaInteraccion : MonoBehaviour
{
    public GameObject textoE;
    public CraftingSystem crafting;
    public RadioInteraccion radio;

    private bool jugadorCerca = false;
    private bool reparada = false;

    public MesaTrabajoInteraccion mesaTrabajo;

    void Start()
    {
        if (textoE != null)
            textoE.SetActive(false);
    }

    void Update()
    {
        if (jugadorCerca && Input.GetKeyDown(KeyCode.E) && !reparada)
        {
            if (ProgresoAntena.instance.generadorEncendido)
            {
                RepararAntena();
            }
            else
            {
                if (crafting != null)
                    crafting.MostrarMensaje("La antena no tiene energía. Primero necesito encender el generador.");
            }
        }
    }

    void RepararAntena()
    {
        reparada = true;

        if (radio != null)
            radio.ActivarSenal();

        if (mesaTrabajo != null)
            mesaTrabajo.ActivarEnergia();

        if (crafting != null)
            crafting.MostrarMensaje("Creo que ya quedó... Tal vez ahora funcione la mesa de trabajo de la casa.");

        if (textoE != null)
            textoE.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !reparada)
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