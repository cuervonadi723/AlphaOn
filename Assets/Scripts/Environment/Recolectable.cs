using UnityEngine;

public class Recolectable : MonoBehaviour
{
    public PlayerInventory.TipoRecurso tipo;
    public int cantidad = 1;

    public void Recolectar(CraftingSystem crafting)
    {
        PlayerInventory inv = crafting.inventory;

        bool agregado = inv.AddResource(tipo, cantidad);

        if (agregado)
        {
            crafting.MostrarMensaje("Recolectaste " + tipo.ToString());
            Destroy(gameObject);
        }
        else
        {
            crafting.MostrarMensaje("No puedo cargar más " + tipo.ToString());
        }
    }
}