using UnityEngine;
using TMPro;
using System.Collections;

public class CraftingSystem : MonoBehaviour
{
    public PlayerInventory inventory;
    public UIFade mensajeFade;
    public AudioSource audioSource;
    public AudioClip sonidoCraftCompleto;

    public enum Crafteos
    {
        Hacha,
        Pico,
        Lanza,
        Fogata,
        CamaImprovisada,
        Venda,
    }

    [System.Serializable]
    public class Receta
    {
        public string nombre;
        public PlayerInventory.TipoRecurso[] recursos;
        public int[] cantidades;
        public bool crafteado;
        public KeyCode craftingKey;
    }

    [Header("Recetas")]
    public Receta[] recetas;

    [Header("UI")]
    public TextMeshProUGUI mensajeTexto;

    [Header("Tiempo de crafteo")]
    public float tiempoCrafteo = 5f;
    public TextMeshProUGUI textoContadorCrafteo;

    [Header("Objetos en casa")] //cambie de ponerlo libre por el mundo porque era muy complejo para mi y tenia muchos errores y deje asi de que esten en la casa, sigue esa idea de craftear pero ms controlado.
    public GameObject fogataEnCasa;
    public GameObject camaEnCasa;
    private bool estaCrafteando = false;

    public void Craftear(Crafteos crafteo)
    {
        if (estaCrafteando)
        {
            MostrarMensaje("Ya estás creando algo");
            return;
        }

        StartCoroutine(CraftearConTiempo(crafteo));
    }

    IEnumerator CraftearConTiempo(Crafteos crafteo)
    {
        Receta receta = BuscarReceta(crafteo);

        if (receta == null)
        {
            MostrarMensaje("No existe la receta");
            yield break;
        }

        if (receta.crafteado && crafteo != Crafteos.Fogata)
        {
            MostrarMensaje("Ya tenés " + receta.nombre);
            yield break;
        }

        if (!PuedeCraftear(receta))
        {
            MostrarMensaje("Faltan materiales");
            yield break;
        }

        estaCrafteando = true;

        float tiempo = tiempoCrafteo;

        while (tiempo > 0)
        {
            if (textoContadorCrafteo != null)
                textoContadorCrafteo.text = "Creando... " + Mathf.CeilToInt(tiempo);

            tiempo -= Time.deltaTime;
            yield return null;
        }

        if (textoContadorCrafteo != null)
            textoContadorCrafteo.text = "";

        GastarRecursos(receta);

        if (audioSource != null && sonidoCraftCompleto != null)
        {
            audioSource.PlayOneShot(sonidoCraftCompleto);
        }

        if (crafteo == Crafteos.Venda)
        {
            inventory.AddResource(PlayerInventory.TipoRecurso.Venda, 1);

            MochilaUI mochila = FindObjectOfType<MochilaUI>();
            if (mochila != null)
                mochila.ActualizarUI();

            estaCrafteando = false;
            MostrarMensaje("Venda creada!");
            yield break;
        }

        if (crafteo == Crafteos.Fogata)
        {
            DarResultado(receta);

            if (fogataEnCasa != null)
                fogataEnCasa.SetActive(true);

            estaCrafteando = false;
            MostrarMensaje("Fogata preparada!");
            yield break;
        }

        if (crafteo == Crafteos.CamaImprovisada)
        {
            DarResultado(receta);

            if (camaEnCasa != null)
                camaEnCasa.SetActive(true);

            estaCrafteando = false;
            MostrarMensaje("Cama improvisada preparada!");
            yield break;
        }

        DarResultado(receta);

        estaCrafteando = false;

        MostrarMensaje(receta.nombre + " creada!");
    }

    public Receta BuscarReceta(Crafteos crafteo)
    {
        if (recetas == null)
            return null;

        int index = (int)crafteo;

        if (index < 0 || index >= recetas.Length)
            return null;

        return recetas[index];
    }

    public KeyCode TeclaDeCrafteo(Crafteos crafteo)
    {
        return recetas[(int)crafteo].craftingKey;
    }

    bool PuedeCraftear(Receta receta)
    {
        if (inventory == null)
            return false;

        if (receta.recursos.Length != receta.cantidades.Length)
        {
            MostrarMensaje("Error en receta: recursos y cantidades no coinciden");
            return false;
        }

        for (int i = 0; i < receta.recursos.Length; i++)
        {
            if (!inventory.HasResource(receta.recursos[i], receta.cantidades[i]))
                return false;
        }

        return true;
    }

    void GastarRecursos(Receta receta)
    {
        for (int i = 0; i < receta.recursos.Length; i++)
        {
            inventory.RemoveResource(receta.recursos[i], receta.cantidades[i]);
        }
    }

    void DarResultado(Receta receta)
    {
        receta.crafteado = true;
    }

    public bool EstaCrafteado(Crafteos crafteo)
    {
        Receta receta = BuscarReceta(crafteo);

        if (receta == null)
            return false;

        return receta.crafteado;
    }

    public void MostrarMensaje(string mensaje)
    {
        if (mensajeTexto == null)
            return;

        StopCoroutineSafe();
        StartCoroutine(MostrarMensajeTemporal(mensaje));
    }

    void StopCoroutineSafe()
    {
        StopAllCoroutines();

        if (estaCrafteando)
            estaCrafteando = false;

        if (textoContadorCrafteo != null)
            textoContadorCrafteo.text = "";
    }

    IEnumerator MostrarMensajeTemporal(string mensaje)
    {
        if (mensajeFade != null)
            mensajeFade.Mostrar();

        mensajeTexto.text = mensaje;

        yield return new WaitForSeconds(5f);

        if (mensajeFade != null)
            mensajeFade.Ocultar();
        else
            mensajeTexto.text = "";
    }
}