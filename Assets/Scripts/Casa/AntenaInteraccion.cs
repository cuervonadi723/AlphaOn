using UnityEngine;

public class AntenaInteraccion : MonoBehaviour
{
    public GameObject textoE;
    public CraftingSystem crafting;
    public RadioInteraccion radio;

    private bool jugadorCerca = false;
    private bool reparada = false;

    void Start()
    {
        if (textoE != null)
            textoE.SetActive(false);
    }

    void Update()
    {
        if (jugadorCerca && Input.GetKeyDown(KeyCode.E) && !reparada)
        {
            RepararAntena();
        }
    }

    void RepararAntena()
    {
        reparada = true;

        if (radio != null)
            radio.ActivarSenal();

        if (crafting != null)
            crafting.MostrarMensaje("Creo que ya quedó. Tal vez la radio ahora reciba algo.");

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