using UnityEngine;

public class FoodItem : MonoBehaviour
{
    public int cantidad = 1;

    public void Recolectar(CraftingSystem crafting)
    {
        if (crafting == null || crafting.inventory == null)
            return;

        bool agregado = crafting.inventory.AddResource(PlayerInventory.TipoRecurso.LataComida, cantidad);

        if (agregado)
        {
            crafting.MostrarMensaje("Agarraste una lata de comida.");

            MochilaUI mochila = FindObjectOfType<MochilaUI>();
            if (mochila != null)
                mochila.ActualizarUI();

            Destroy(gameObject);
        }
        else
        {
            crafting.MostrarMensaje("No puedo cargar más latas.");
        }
    }
}