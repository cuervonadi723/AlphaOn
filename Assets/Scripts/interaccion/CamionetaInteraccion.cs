using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamionetaInteraccion : MonoBehaviour
{
    private bool revisada = false;
    public AudioSource audioSource;
    public AudioClip sonidoRevisar;

    public void Revisar(CraftingSystem crafting)
    {
        if (!ProgresoAntena.instance.puedeRevisarCamioneta)
        {
            crafting.MostrarMensaje("No necesito revisar la camioneta ahora.");
            return;
        }

        if (revisada)
        {
            crafting.MostrarMensaje("Ya revisé la camioneta.");
            return;
        }

        if (!ProgresoAntena.instance.tieneBidon)
        {
            crafting.MostrarMensaje("Necesito algo para transportar combustible.");
            return;
        }

        if (audioSource != null && sonidoRevisar != null)
            audioSource.PlayOneShot(sonidoRevisar);

        revisada = true;

        ProgresoAntena.instance.tieneCombustible = true;
        ProgresoAntena.instance.tieneFusibles = true;
        //agregamos los materiales a la mochila.
        crafting.inventory.RemoveResource(PlayerInventory.TipoRecurso.BidonVacio, 1);
        crafting.inventory.AddResource(PlayerInventory.TipoRecurso.BidonLleno, 1);
        crafting.inventory.AddResource(PlayerInventory.TipoRecurso.Fusibles, 1);

        FindObjectOfType<MochilaUI>().ActualizarUI();

        crafting.MostrarMensaje("Encontraste combustible y unos fusibles.");
    }

    public string GetTexto()
    {
        if (!ProgresoAntena.instance.puedeRevisarCamioneta)
            return "";

        return "E: revisar camioneta";
    }
}
