using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DormirInteraccion : MonoBehaviour
{
    public CraftingSystem crafting;

    [Header("Referencias")]
    public GameObject fogataEnCasa;

    [Header("Fade opcional")]
    public CanvasGroup fadeNegro;
    public float velocidadFade = 1.5f;
    public float tiempoPantallaNegra = 2f;

    [Header("Luz opcional")]
    public Light luzSol;
    public float intensidadDia = 1f;
    private bool durmiendo = false;

    [Header("Comidaaa")]
    public PlayerStats playerStats;
    public float comidaNecesaria = 90f;

    [Header("Final")]
    public AudioSource audioSource;
    public AudioClip sonidoHelicoptero;
    public GameObject panelFinal;

    public string GetTexto()
    {
        return "E: dormir";
    }

    public void Dormir()
    {
        if (durmiendo)
            return;

        if (ProgresoAntena.instance == null || !ProgresoAntena.instance.debeDormir)
        {
            crafting.MostrarMensaje("Todavía no necesito dormir.");
            return;
        }

        if (fogataEnCasa == null || !fogataEnCasa.activeSelf)
        {
           crafting.MostrarMensaje("Hace demasiado frío. Necesito una fogata antes de dormir.");
            return;
        }

        if (!crafting.EstaCrafteado(CraftingSystem.Crafteos.Lanza))
        {
            crafting.MostrarMensaje("No me siento seguro pasando la noche sin algún tipo de arma.");
            return;
        }

        if (playerStats == null)
            playerStats = FindObjectOfType<PlayerStats>();

        if (playerStats != null && playerStats.food != null && playerStats.food.current < comidaNecesaria)
        {
            crafting.MostrarMensaje("Tengo hambre. Ignacio marcó algunos lugares en el mapa, tal vez encuentre algo útil.");
            return;
        }

        StartCoroutine(DormirRutina());
    }

    IEnumerator DormirRutina()
    {
        

        durmiendo = true;

        if (crafting != null)
            crafting.MostrarMensaje("Voy a descansar un poco...");

        yield return new WaitForSeconds(1.5f);

        

        if (fadeNegro != null)
        {
            

            while (fadeNegro.alpha < 1)
            {
                fadeNegro.alpha += Time.deltaTime * velocidadFade;
                yield return null;
            }

            fadeNegro.alpha = 1;
            
        }
       

        yield return new WaitForSeconds(tiempoPantallaNegra);

        if (luzSol != null)
            luzSol.intensity = intensidadDia;

        ProgresoAntena.instance.yaDurmio = true;
        ProgresoAntena.instance.debeDormir = false;



        if (audioSource != null && sonidoHelicoptero != null)
        {
            audioSource.clip = sonidoHelicoptero;
            audioSource.loop = true;
            audioSource.Play();
        }

        yield return new WaitForSeconds(2f);

        if (crafting != null)
            crafting.MostrarMensaje("Finalmente... voy a salir de aquí.");

        yield return new WaitForSeconds(3f);

        if (panelFinal != null)
            panelFinal.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Time.timeScale = 0f;

        durmiendo = false;
    }
}