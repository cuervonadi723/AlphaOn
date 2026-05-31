using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class MochilaUI : MonoBehaviour
{
    public enum ItemID
    {
        Piedra,
        Rama,
        Madera,
        Tela,
        Aloe,
        Venda,
        Hacha,
        Pico,
        Lanza,
        BidonVacio,
        BidonLleno,
        Fusibles,
        Hojas,
    }

    [System.Serializable]
    public class ItemConfig
    {
        public ItemID id;
        public string nombre;
        public Sprite icono;
        public float peso = 1f;

        public bool esHerramienta;
        public PlayerInventory.TipoRecurso recurso;
        public CraftingSystem.Crafteos herramienta;
    }

    [Header("Referencias")]
    public GameObject mochilaPanel;
    public PlayerInventory inventory;
    public CraftingSystem crafting;

    [Header("Slots Inventario")]
    public SlotMochilaUI[] slotsInventario;

    [Header("Slots Hotbar dentro de la mochila")]
    public SlotMochilaUI[] slotsHotbar;

    [Header("Hotbar viejo / visible")]
    public HotbarUI hotbarUI;

    [Header("Hotbar visible abajo")]
    public SlotMochilaUI[] slotsHotbarVisible;

    [Header("Peso")]
    public TextMeshProUGUI pesoTexto;
    public Image barraPeso;
    public float pesoMaximo = 50f;

    [Header("Items")]
    public ItemConfig[] items;

    [Header("Animaciones herramientas")]
    public MonoBehaviour[] scriptsAnimacionesHerramientas;

    [Header("Control jugador")]
    public MonoBehaviour scriptMovimiento;
    public MonoBehaviour scriptCamara;
    public PlayerInput playerInput;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip sonidoAbrir;
    public AudioClip sonidoCerrar;

    public bool mochilaDesbloqueada = false;
    public static bool MochilaAbiertaGlobal = false;

    private bool mochilaAbierta = false;

    private ItemID?[] inventario;
    private ItemID?[] hotbar;

    private bool haySeleccion = false;
    private bool seleccionDesdeHotbar = false;
    private int indexSeleccionado = -1;
    private ItemID itemSeleccionado;

    private UIFade fadeMochila;

    void Start()
    {
        fadeMochila = mochilaPanel.GetComponent<UIFade>();

        inventario = new ItemID?[12];
        hotbar = new ItemID?[6];

        ConfigurarSlots();

        if (mochilaPanel != null)
            mochilaPanel.SetActive(false);

        ActualizarUI();
    }

    void Update()
    {
        if (mochilaDesbloqueada && Input.GetKeyDown(KeyCode.Tab))
        {
            mochilaAbierta = !mochilaAbierta;
            if (mochilaAbierta)
                ReproducirSonido(sonidoAbrir);
            else
                ReproducirSonido(sonidoCerrar);

            MochilaAbiertaGlobal = mochilaAbierta;

            if (mochilaPanel != null)
            {
                if (fadeMochila != null)
                {
                    if (mochilaAbierta)
                        fadeMochila.Mostrar();
                    else
                        fadeMochila.Ocultar();
                }
                else
                {
                    mochilaPanel.SetActive(mochilaAbierta);
                }
            }

            Cursor.lockState = mochilaAbierta
                ? CursorLockMode.None
                : CursorLockMode.Locked;

            Cursor.visible = mochilaAbierta;

            if (scriptMovimiento != null)
                scriptMovimiento.enabled = !mochilaAbierta;

            if (scriptCamara != null)
                scriptCamara.enabled = !mochilaAbierta;

            if (playerInput != null)
                playerInput.enabled = !mochilaAbierta;

            for (int i = 0; i < scriptsAnimacionesHerramientas.Length; i++)
            {
                if (scriptsAnimacionesHerramientas[i] != null)
                    scriptsAnimacionesHerramientas[i].enabled = !mochilaAbierta;
            }

            if (mochilaAbierta)
                ActualizarUI();
        }

        if (mochilaAbierta && Input.GetKeyDown(KeyCode.Escape))
        {
            mochilaAbierta = false;
            MochilaAbiertaGlobal = false;

            if (fadeMochila != null)
                fadeMochila.Ocultar();
            else if (mochilaPanel != null)
                mochilaPanel.SetActive(false);

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            if (scriptMovimiento != null)
                scriptMovimiento.enabled = true;

            if (scriptCamara != null)
                scriptCamara.enabled = true;

            if (playerInput != null)
                playerInput.enabled = true;

            for (int i = 0; i < scriptsAnimacionesHerramientas.Length; i++)
            {
                if (scriptsAnimacionesHerramientas[i] != null)
                    scriptsAnimacionesHerramientas[i].enabled = true;
            }
        }
    }

    void ConfigurarSlots()
    {
        for (int i = 0; i < slotsInventario.Length; i++)
        {
            if (slotsInventario[i] != null)
                slotsInventario[i].Configurar(this, i, false);
        }

        for (int i = 0; i < slotsHotbar.Length; i++)
        {
            if (slotsHotbar[i] != null)
                slotsHotbar[i].Configurar(this, i, true);
        }
    }

    public void DesbloquearMochila()
    {
        MochilaAbiertaGlobal = false;
        mochilaDesbloqueada = true;
        mochilaAbierta = false;

        if (mochilaPanel != null)
            mochilaPanel.SetActive(false);

        ActualizarUI();
    }

    public void ActualizarUI()
    {
        SincronizarItems();
        DibujarSlots();
        ActualizarPeso();
        ActualizarSeleccionVisual();
    }

    void SincronizarItems()
    {
        LimpiarItemsQueYaNoExisten();

        for (int i = 0; i < items.Length; i++)
        {
            ItemConfig item = items[i];

            if (item == null)
                continue;

            if (ItemDisponible(item) && !ItemYaExiste(item.id))
                AgregarAlPrimerSlotLibre(item.id);
        }
    }

    void LimpiarItemsQueYaNoExisten()
    {
        for (int i = 0; i < inventario.Length; i++)
        {
            if (inventario[i].HasValue)
            {
                ItemConfig item = GetConfig(inventario[i].Value);

                if (!ItemDisponible(item))
                    inventario[i] = null;
            }
        }

        for (int i = 0; i < hotbar.Length; i++)
        {
            if (hotbar[i].HasValue)
            {
                ItemConfig item = GetConfig(hotbar[i].Value);

                if (!ItemDisponible(item))
                    hotbar[i] = null;
            }
        }
    }

    bool ItemDisponible(ItemConfig item)
    {
        if (item == null)
            return false;

        if (item.esHerramienta)
            return crafting != null && crafting.EstaCrafteado(item.herramienta);

        return inventory != null && inventory.GetResource(item.recurso) > 0;
    }

    bool ItemYaExiste(ItemID id)
    {
        for (int i = 0; i < inventario.Length; i++)
        {
            if (inventario[i].HasValue && inventario[i].Value == id)
                return true;
        }

        for (int i = 0; i < hotbar.Length; i++)
        {
            if (hotbar[i].HasValue && hotbar[i].Value == id)
                return true;
        }

        return false;
    }

    void AgregarAlPrimerSlotLibre(ItemID id)
    {
        for (int i = 0; i < inventario.Length; i++)
        {
            if (!inventario[i].HasValue)
            {
                inventario[i] = id;
                return;
            }
        }
    }

    public void ClickSlot(bool esHotbar, int index)
    {
        if (esHotbar && (index < 0 || index >= hotbar.Length))
            return;

        if (!esHotbar && (index < 0 || index >= inventario.Length))
            return;

        ItemID? itemClickeado = esHotbar
            ? hotbar[index]
            : inventario[index];

        if (!haySeleccion)
        {
            if (!itemClickeado.HasValue)
                return;

            haySeleccion = true;
            seleccionDesdeHotbar = esHotbar;
            indexSeleccionado = index;
            itemSeleccionado = itemClickeado.Value;

            ActualizarSeleccionVisual();
            return;
        }

        if (seleccionDesdeHotbar == esHotbar &&
            indexSeleccionado == index)
        {
            CancelarSeleccion();
            ActualizarSeleccionVisual();
            return;
        }

        if (itemClickeado.HasValue)
            return;

        if (esHotbar)
        {
            hotbar[index] = itemSeleccionado;

            if (seleccionDesdeHotbar)
                hotbar[indexSeleccionado] = null;
            else
                inventario[indexSeleccionado] = null;
        }
        else
        {
            inventario[index] = itemSeleccionado;

            if (seleccionDesdeHotbar)
                hotbar[indexSeleccionado] = null;
            else
                inventario[indexSeleccionado] = null;
        }

        CancelarSeleccion();
        ActualizarUI();
    }

    void CancelarSeleccion()
    {
        haySeleccion = false;
        seleccionDesdeHotbar = false;
        indexSeleccionado = -1;
    }

    void DibujarSlots()
    {
        for (int i = 0; i < slotsInventario.Length; i++)
        {
            if (slotsInventario[i] == null)
                continue;

            if (i < inventario.Length && inventario[i].HasValue)
                MostrarItemEnSlot(slotsInventario[i], inventario[i].Value);
            else
                slotsInventario[i].Limpiar();
        }

        for (int i = 0; i < slotsHotbar.Length; i++)
        {
            if (slotsHotbar[i] == null)
                continue;

            if (i < hotbar.Length && hotbar[i].HasValue)
                MostrarItemEnSlot(slotsHotbar[i], hotbar[i].Value);
            else
                slotsHotbar[i].Limpiar();
        }

        for (int i = 0; i < slotsHotbarVisible.Length; i++)
        {
            if (slotsHotbarVisible[i] == null)
                continue;

            if (i < hotbar.Length && hotbar[i].HasValue)
                MostrarItemEnSlot(slotsHotbarVisible[i], hotbar[i].Value);
            else
                slotsHotbarVisible[i].Limpiar();
        }

        if (hotbarUI != null)
            hotbarUI.RecibirHotbar(hotbar);
    }

    void MostrarItemEnSlot(SlotMochilaUI slot, ItemID id)
    {
        if (slot == null)
            return;

        ItemConfig item = GetConfig(id);

        if (item == null)
        {
            slot.Limpiar();
            return;
        }

        string cantidad = "";

        if (!item.esHerramienta && inventory != null)
        {
            int cant = inventory.GetResource(item.recurso);
            cantidad = cant > 1 ? cant.ToString() : "";
        }

        slot.Mostrar(item.icono, cantidad);
    }

    void ActualizarSeleccionVisual()
    {
        for (int i = 0; i < slotsInventario.Length; i++)
        {
            if (slotsInventario[i] != null)
                slotsInventario[i].MarcarSeleccionado(false);
        }

        for (int i = 0; i < slotsHotbar.Length; i++)
        {
            if (slotsHotbar[i] != null)
                slotsHotbar[i].MarcarSeleccionado(false);
        }

        if (!haySeleccion)
            return;

        if (seleccionDesdeHotbar)
        {
            if (indexSeleccionado >= 0 &&
                indexSeleccionado < slotsHotbar.Length &&
                slotsHotbar[indexSeleccionado] != null)
            {
                slotsHotbar[indexSeleccionado].MarcarSeleccionado(true);
            }
        }
        else
        {
            if (indexSeleccionado >= 0 &&
                indexSeleccionado < slotsInventario.Length &&
                slotsInventario[indexSeleccionado] != null)
            {
                slotsInventario[indexSeleccionado].MarcarSeleccionado(true);
            }
        }
    }

    void ActualizarPeso()
    {
        float peso = 0f;

        for (int i = 0; i < items.Length; i++)
        {
            ItemConfig item = items[i];

            if (!ItemDisponible(item))
                continue;

            if (item.esHerramienta)
            {
                peso += item.peso;
            }
            else if (inventory != null)
            {
                int cantidad = inventory.GetResource(item.recurso);
                peso += item.peso * cantidad;
            }
        }

        if (pesoTexto != null)
        {
            pesoTexto.text =
                peso.ToString("0.0") +
                " / " +
                pesoMaximo.ToString("0") +
                " KG";
        }

        if (barraPeso != null)
            barraPeso.fillAmount = Mathf.Clamp01(peso / pesoMaximo);
    }

    ItemConfig GetConfig(ItemID id)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] != null && items[i].id == id)
                return items[i];
        }

        return null;
    }

    void ReproducirSonido(AudioClip clip)
    {
        if (audioSource != null && clip != null)
            audioSource.PlayOneShot(clip);
    }
}