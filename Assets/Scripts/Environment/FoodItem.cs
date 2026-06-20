using UnityEngine;

public class FoodItem : MonoBehaviour
{
    public int cantidad = 1;

    public AudioSource audioSource;
    public AudioClip sonidoRecoger;

    public void Recolectar(CraftingSystem crafting)
    {
        if (crafting == null || crafting.inventory == null)
            return;

        bool agregado = crafting.inventory.AddResource(PlayerInventory.TipoRecurso.LataComida, cantidad);

        if (agregado)
        {
            if (audioSource != null && sonidoRecoger != null)
                audioSource.PlayOneShot(sonidoRecoger);

            crafting.MostrarMensaje("Agarraste una lata de comida.");

            MochilaUI mochila = FindObjectOfType<MochilaUI>();
            if (mochila != null)
                mochila.ActualizarUI();

            Destroy(gameObject, 0.5f);
        }
        else
        {
            crafting.MostrarMensaje("No puedo cargar más latas.");
        }
    }
}