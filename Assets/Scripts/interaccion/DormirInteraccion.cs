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
    public float comidaNecesaria = 100f;

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

        if (fadeNegro != null)
        {
            while (fadeNegro.alpha > 0)
            {
                fadeNegro.alpha -= Time.deltaTime * velocidadFade;
                yield return null;
            }

            fadeNegro.alpha = 0;
        }

        if (crafting != null)
            crafting.MostrarMensaje("Finalmente... voy a salir de aquí.");

        durmiendo = false;
    }
}