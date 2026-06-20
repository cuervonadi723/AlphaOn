using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorInteraccion : MonoBehaviour
{
    private bool cargando = false;

    public ParticleSystem humoEscape;
    

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip sonidoCargarCombustible;
    public AudioSource audioMotor;
    public AudioClip sonidoRevisarGenerador;

    public void Revisar(CraftingSystem crafting)
    {
        if (!ProgresoAntena.instance.generadorConCombustible)
        {
            if (!ProgresoAntena.instance.tieneCombustible)
            {
                if (audioSource != null && sonidoRevisarGenerador != null)

                    audioSource.PlayOneShot(sonidoRevisarGenerador);
                
                crafting.MostrarMensaje("El tanque está vacío.");
                return;
            }

            if (!cargando)
                StartCoroutine(CargarCombustible(crafting));

            return;
        }

        crafting.MostrarMensaje("El generador ya está funcionando.");
    }

    IEnumerator CargarCombustible(CraftingSystem crafting)
    {
        cargando = true;

        crafting.MostrarMensaje("Cargando combustible...");

        if (audioSource != null && sonidoCargarCombustible != null)
            audioSource.PlayOneShot(sonidoCargarCombustible);

        yield return new WaitForSeconds(1.5f);

        ProgresoAntena.instance.generadorConCombustible = true;
        ProgresoAntena.instance.generadorEncendido = true;
        ProgresoAntena.instance.tieneCombustible = false;

        crafting.inventory.RemoveResource(
            PlayerInventory.TipoRecurso.BidonLleno,
            1
        );

        MochilaUI mochila = FindObjectOfType<MochilaUI>();

        if (mochila != null)
            mochila.ActualizarUI();

        crafting.MostrarMensaje(
            "Cargaste combustible y encendiste el generador."
        );

        if (audioMotor != null && ! audioMotor.isPlaying)
        {
            audioMotor.Play();
        }

        if (humoEscape != null)
        {
            humoEscape.Play();
        }

        cargando = false;
        
    }
    

    public string GetTexto()
    {
        if (!ProgresoAntena.instance.generadorConCombustible)
        {
            if (ProgresoAntena.instance.tieneCombustible)
                return "E: cargar combustible";

            return "E: revisar generador";
        }

        return "E: revisar generador";
    }
}