using UnityEngine;
using TMPro;

public class MochilaUI : MonoBehaviour
{
    [Header("Referencias")]
    public GameObject mochilaPanel;
    public PlayerInventory inventory;
    public CraftingSystem crafting;

    [Header("Textos Recursos")]
    public TextMeshProUGUI piedraText;
    public TextMeshProUGUI ramaText;
    public TextMeshProUGUI maderaText;
    public TextMeshProUGUI telaText;
    public TextMeshProUGUI aloeText;
    public TextMeshProUGUI vendaText;

    [Header("Textos Herramientas")]
    public TextMeshProUGUI hachaText;
    public TextMeshProUGUI picoText;
    public TextMeshProUGUI lanzaText;

    public bool mochilaDesbloqueada = false;

    private bool mochilaAbierta = false;

    void Start()
    {
        if (mochilaPanel != null)
            mochilaPanel.SetActive(false);
    }

    void Update()
    {
        if (mochilaDesbloqueada && Input.GetKeyDown(KeyCode.Tab))
        {
            mochilaAbierta = !mochilaAbierta;

            if (mochilaPanel != null)
                mochilaPanel.SetActive(mochilaAbierta);

            if (mochilaAbierta)
                ActualizarUI();
        }
    }

    public void DesbloquearMochila()
    {
        mochilaDesbloqueada = true;
        mochilaAbierta = false;

        if (mochilaPanel != null)
            mochilaPanel.SetActive(false);
    }

    void ActualizarUI()
    {
        if (inventory != null)
        {
            if (piedraText != null)
                piedraText.text = "Piedras: " +
                    inventory.GetResource(PlayerInventory.TipoRecurso.Piedra) +
                    " / " + inventory.GetMaxResource(PlayerInventory.TipoRecurso.Piedra);

            if (ramaText != null)
                ramaText.text = "Ramas: " +
                    inventory.GetResource(PlayerInventory.TipoRecurso.Rama) +
                    " / " + inventory.GetMaxResource(PlayerInventory.TipoRecurso.Rama);

            if (maderaText != null)
                maderaText.text = "Madera: " +
                    inventory.GetResource(PlayerInventory.TipoRecurso.Madera) +
                    " / " + inventory.GetMaxResource(PlayerInventory.TipoRecurso.Madera);

            if (telaText != null)
                telaText.text = "Tela: " +
                    inventory.GetResource(PlayerInventory.TipoRecurso.Tela) +
                    " / " + inventory.GetMaxResource(PlayerInventory.TipoRecurso.Tela);

            if (aloeText != null)
                aloeText.text = "Aloe: " +
                    inventory.GetResource(PlayerInventory.TipoRecurso.Aloe) +
                    " / " + inventory.GetMaxResource(PlayerInventory.TipoRecurso.Aloe);

            if (vendaText != null)
                vendaText.text = "Vendas: " +
                    inventory.GetResource(PlayerInventory.TipoRecurso.Venda) +
                    " / " + inventory.GetMaxResource(PlayerInventory.TipoRecurso.Venda);
        }

        if (crafting != null)
        {
            if (hachaText != null)
                hachaText.text = "Hacha: " +
                    (crafting.EstaCrafteado(CraftingSystem.Crafteos.Hacha) ? "Sí" : "No");

            if (picoText != null)
                picoText.text = "Pico: " +
                    (crafting.EstaCrafteado(CraftingSystem.Crafteos.Pico) ? "Sí" : "No");

            if (lanzaText != null)
                lanzaText.text = "Lanza: " +
                    (crafting.EstaCrafteado(CraftingSystem.Crafteos.Lanza) ? "Sí" : "No");
        }
    }
}