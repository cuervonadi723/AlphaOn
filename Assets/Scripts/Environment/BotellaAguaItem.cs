using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotellaAguaItem : MonoBehaviour
{
    public int cantidad = 1;

    public AudioSource audioSource;
    public AudioClip sonidoRecoger;

    public void Recolectar(CraftingSystem crafting)
    {
        if (crafting == null || crafting.inventory == null)
            return;

        bool agregado = crafting.inventory.AddResource(
            PlayerInventory.TipoRecurso.BotellaAgua,
            cantidad
        );

        if (agregado)
        {
            if (audioSource != null && sonidoRecoger != null)
                audioSource.PlayOneShot(sonidoRecoger);

            crafting.MostrarMensaje("Agarraste una botella de agua.");

            MochilaUI mochila = FindObjectOfType<MochilaUI>();

            if (mochila != null)
                mochila.ActualizarUI();

            Destroy(gameObject, 0.5f);
        }
        else
        {
            crafting.MostrarMensaje("No puedo cargar más botellas.");
        }
    }
}