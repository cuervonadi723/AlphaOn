using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [Header("Paneles")]
    public GameObject Opciones;
    public GameObject Botonera;
    public GameObject MenuPartida;

    [Header("Canvas Groups")]
    public CanvasGroup opcionesCanvasGroup;
    public CanvasGroup partidaCanvasGroup;

    [Header("Botones")]
    public Button botonCargarPartida;

    [Header("Configuracion")]
    public float velocidadFade = 0.35f;
    public string escenaNuevaPartida = "SampleScene";

    private Coroutine fadeActual;

    void Start()
    {
        Opciones.SetActive(false);
        opcionesCanvasGroup.alpha = 0;
        opcionesCanvasGroup.interactable = false;
        opcionesCanvasGroup.blocksRaycasts = false;

        MenuPartida.SetActive(false);
        partidaCanvasGroup.alpha = 0;
        partidaCanvasGroup.interactable = false;
        partidaCanvasGroup.blocksRaycasts = false;

        RevisarPartidaGuardada();
    }

    public void Jugar()
    {
        AbrirMenuPartida();
    }

    public void NuevaPartida()
    {
        PlayerPrefs.DeleteKey("partidaGuardada");
        SceneManager.LoadScene(escenaNuevaPartida);
    }

    public void CargarPartida()
    {
        if (PlayerPrefs.HasKey("partidaGuardada"))
        {
            SceneManager.LoadScene(escenaNuevaPartida);
        }
    }

    public void Salir()
    {
        Application.Quit();
    }

    public void AbrirOpciones()
    {
        if (fadeActual != null)
            StopCoroutine(fadeActual);

        Opciones.SetActive(true);
        Botonera.SetActive(false);

        fadeActual = StartCoroutine(FadePanel(opcionesCanvasGroup, 1));
    }

    public void CerrarOpciones()
    {
        if (fadeActual != null)
            StopCoroutine(fadeActual);

        Botonera.SetActive(true);

        fadeActual = StartCoroutine(FadePanel(opcionesCanvasGroup, 0, Opciones));
    }

    public void AbrirMenuPartida()
    {
        if (fadeActual != null)
            StopCoroutine(fadeActual);

        MenuPartida.SetActive(true);
        Botonera.SetActive(false);

        RevisarPartidaGuardada();

        fadeActual = StartCoroutine(FadePanel(partidaCanvasGroup, 1));
    }

    public void CerrarMenuPartida()
    {
        if (fadeActual != null)
            StopCoroutine(fadeActual);

        Botonera.SetActive(true);

        fadeActual = StartCoroutine(FadePanel(partidaCanvasGroup, 0, MenuPartida));
    }

    void RevisarPartidaGuardada()
    {
        bool tienePartida = PlayerPrefs.HasKey("partidaGuardada");

        if (botonCargarPartida != null)
            botonCargarPartida.interactable = tienePartida;
    }

    IEnumerator FadePanel(CanvasGroup grupo, float alphaFinal, GameObject panelCerrar = null)
    {
        float alphaInicial = grupo.alpha;
        float tiempo = 0f;

        while (tiempo < velocidadFade)
        {
            tiempo += Time.deltaTime;
            grupo.alpha = Mathf.Lerp(alphaInicial, alphaFinal, tiempo / velocidadFade);
            yield return null;
        }

        grupo.alpha = alphaFinal;

        bool abierto = alphaFinal == 1;

        grupo.interactable = abierto;
        grupo.blocksRaycasts = abierto;

        if (!abierto && panelCerrar != null)
            panelCerrar.SetActive(false);
    }
}