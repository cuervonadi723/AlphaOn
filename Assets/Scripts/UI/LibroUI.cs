using TMPro;
using UnityEngine;

public class LibroUI : MonoBehaviour
{
    public GameObject libroPanel;
    public TextMeshProUGUI texto;

    public CraftingSystem crafting;

    [Header("UI que se desbloquea")]
    public GameObject hotbar;
    public MochilaUI mochilaUI;
    public GameObject textoAyuda;

    public bool libroDesbloqueado = false;

    private bool abierto = false;
    private bool desbloqueado = false;

    void Start()
    {
        if (libroPanel != null)
            libroPanel.SetActive(false);

        if (hotbar != null)
            hotbar.SetActive(false);

        if (mochilaUI != null && mochilaUI.mochilaPanel != null)
            mochilaUI.mochilaPanel.SetActive(false);

        if (textoAyuda != null)
            textoAyuda.SetActive(false);
    }

    void Update()
    {
        if (desbloqueado && Input.GetKeyDown(KeyCode.B))
        {
            abierto = !abierto;

            if (libroPanel != null)
                libroPanel.SetActive(abierto);

            if (abierto)
                ActualizarLibro();
        }
    }

    public void DesbloquearLibro()
    {
        libroDesbloqueado = true;
        desbloqueado = true;

        if (hotbar != null)
            hotbar.SetActive(true);

        if (textoAyuda != null)
            textoAyuda.SetActive(true);

        abierto = false;

        if (libroPanel != null)
            libroPanel.SetActive(false);

        if (mochilaUI != null)
            mochilaUI.DesbloquearMochila();
    }

    void ActualizarLibro()
    {
        if (crafting == null || texto == null)
            return;

        string resultado = "LIBRO DE SUPERVIVENCIA\n\n";

        for (int i = 0; i < crafting.recetas.Length; i++)
        {
            CraftingSystem.Crafteos crafteo = (CraftingSystem.Crafteos)i;
            CraftingSystem.Receta receta = crafting.recetas[i];

            resultado += "* " + receta.nombre + "\n";

            if (crafting.EstaCrafteado(crafteo))
            {
                resultado += "Ya creado\n\n";
                continue;
            }

            resultado += "Materiales:\n";

            for (int j = 0; j < receta.recursos.Length; j++)
            {
                int tengo = crafting.inventory.GetResource(receta.recursos[j]);
                int necesito = receta.cantidades[j];

                resultado += "- " + receta.recursos[j] + " (" + tengo + "/" + necesito + ")\n";
            }

            resultado += "\n";
        }

        texto.text = resultado;
    }
}