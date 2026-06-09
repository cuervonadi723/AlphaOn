using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LibroCrafteoUI : MonoBehaviour
{
    [Header("Panel")]
    public GameObject libroPanel;

    [Header("Paginas")]
    public GameObject[] paginas;

    [Header("Recetas avanzadas")]
    public bool recetasAvanzadasDesbloqueadas = false;

    [Header("Crafting")]
    public CraftingSystem crafting;
    public CraftingSystem.Crafteos[] crafteosPorPagina;

    [Header("UI Crear")]
    public Button botonCrear;
    public GameObject imagenYaCreado;
    public TextMeshProUGUI textoEstado;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip sonidoAbrir;
    public AudioClip sonidoCerrar;
    public AudioClip sonidoPagina;
    public AudioClip sonidoCrear;

    [Header("Control jugador")]
    public MonoBehaviour scriptMovimiento;
    public MonoBehaviour scriptCamara;
    public PlayerInput playerInput;

    private UIFade fadeLibro;
    private UIZoom zoomLibro;

    private int paginaActual = 0;
    private bool libroAbierto = false;
    public bool libroDesbloqueado = false;
    

    void Start()
    {
        libroAbierto = false;

        if (libroPanel != null)
        {
            fadeLibro = libroPanel.GetComponent<UIFade>();
            zoomLibro = libroPanel.GetComponent<UIZoom>();
            

            libroPanel.SetActive(false);
        }

        MostrarPagina(0);
    }

    void Update()
    {
        if (libroDesbloqueado && Input.GetKeyDown(KeyCode.B))
        {
            if (libroAbierto)
                CerrarLibro();
            else
                AbrirLibro();
        }

        if (libroAbierto)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void AbrirLibro()
    {
        libroAbierto = true;

        ReproducirSonido(sonidoAbrir);

        if (fadeLibro != null)
            fadeLibro.Mostrar();
        else if (libroPanel != null)
            libroPanel.SetActive(true);

        if (zoomLibro != null)
            zoomLibro.ReproducirZoom();

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (scriptMovimiento != null)
            scriptMovimiento.enabled = false;

        if (scriptCamara != null)
            scriptCamara.enabled = false;

        if (playerInput != null)
            playerInput.enabled = false;

        MostrarPagina(paginaActual);
    }

    public void CerrarLibro()
    {
        libroAbierto = false;

        ReproducirSonido(sonidoCerrar);

        if (fadeLibro != null)
            fadeLibro.Ocultar();
        else if (libroPanel != null)
            libroPanel.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (scriptMovimiento != null)
            scriptMovimiento.enabled = true;

        if (scriptCamara != null)
            scriptCamara.enabled = true;

        if (playerInput != null)
            playerInput.enabled = true;
    }

    public void PaginaSiguiente()
    {
        int nuevaPagina = paginaActual + 1;

        if (nuevaPagina >= paginas.Length)
            nuevaPagina = 0;

        ReproducirSonido(sonidoPagina);
        MostrarPagina(nuevaPagina);
    }

    public void PaginaAnterior()
    {
        int nuevaPagina = paginaActual - 1;

        if (nuevaPagina < 0)
            nuevaPagina = paginas.Length - 1;

        ReproducirSonido(sonidoPagina);
        MostrarPagina(nuevaPagina);
    }

    public void CrearDesdePagina()
    {
        if (crafting == null)
            return;

        if (paginaActual < 0 || paginaActual >= crafteosPorPagina.Length)
            return;

        CraftingSystem.Crafteos crafteo = crafteosPorPagina[paginaActual];
        CraftingSystem.Receta receta = crafting.BuscarReceta(crafteo);
        if (!recetasAvanzadasDesbloqueadas &&
    (crafteo == CraftingSystem.Crafteos.Fogata ||
     crafteo == CraftingSystem.Crafteos.CamaImprovisada))
        {
            if (textoEstado != null)
                textoEstado.text = "RECETA BLOQUEADA";

            return;
        }


        if (receta == null)
            return;

        if (crafting.EstaCrafteado(crafteo))
        {
            ActualizarEstadoPagina();
            return;
        }

        if (!PuedeCrear(receta))
        {
            if (textoEstado != null)
            {
                textoEstado.text = "FALTAN MATERIALES";
                textoEstado.color = Color.red;
            }

            return;
        }

        
        ReproducirSonido(sonidoCrear);

        crafting.Craftear(crafteo);

        if (botonCrear != null)
            botonCrear.interactable = false;

        Invoke(nameof(ActualizarEstadoPagina), 5.2f);
    }

    void MostrarPagina(int index)
    {
        paginaActual = index;

        for (int i = 0; i < paginas.Length; i++)
        {
            if (paginas[i] != null)
                paginas[i].SetActive(i == paginaActual);
        }

        ActualizarEstadoPagina();
    }

    void ActualizarEstadoPagina()
    {
        if (crafting == null)
            return;

        if (paginaActual < 0 || paginaActual >= crafteosPorPagina.Length)
            return;

        CraftingSystem.Crafteos crafteo = crafteosPorPagina[paginaActual];
        CraftingSystem.Receta receta = crafting.BuscarReceta(crafteo);

        if (receta == null)
            return;

        bool yaCreado = crafting.EstaCrafteado(crafteo);
        bool puede = PuedeCrear(receta);

        bool recetaBloqueada =
    !recetasAvanzadasDesbloqueadas &&
    (crafteo == CraftingSystem.Crafteos.Fogata ||
     crafteo == CraftingSystem.Crafteos.CamaImprovisada);

        if (imagenYaCreado != null)
            imagenYaCreado.SetActive(yaCreado);

        if (botonCrear != null)
        {
            botonCrear.interactable = !yaCreado && !recetaBloqueada;

            ColorBlock colors = botonCrear.colors;

            colors.normalColor = (!yaCreado && puede && !recetaBloqueada)
                ? Color.white
                : new Color(0.45f, 0.45f, 0.45f, 1f);

            colors.highlightedColor = (!yaCreado && puede && !recetaBloqueada)
                ? new Color(0.9f, 0.85f, 0.7f, 1f)
                : new Color(0.45f, 0.45f, 0.45f, 1f);

            colors.pressedColor = new Color(0.35f, 0.3f, 0.25f, 1f);

            botonCrear.colors = colors;
        }

        if (textoEstado != null)
            textoEstado.text = "";
    }

    bool PuedeCrear(CraftingSystem.Receta receta)
    {
        if (crafting == null || crafting.inventory == null)
            return false;

        for (int i = 0; i < receta.recursos.Length; i++)
        {
            if (!crafting.inventory.HasResource(receta.recursos[i], receta.cantidades[i]))
                return false;
        }

        return true;
    }

    void ReproducirSonido(AudioClip clip)
    {
        if (audioSource != null && clip != null)
            audioSource.PlayOneShot(clip);
    }

    public void DesbloquearLibro()
    {
        libroDesbloqueado = true;
        libroAbierto = false;

        if (libroPanel != null)
            libroPanel.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void DesbloquearRecetasAvanzadas()
    {
        recetasAvanzadasDesbloqueadas = true;
        ActualizarEstadoPagina();
    }
}