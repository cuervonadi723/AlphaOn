using UnityEngine;

public class Tree : MonoBehaviour
{
    public int golpesNecesarios = 3;
    int golpesActuales = 0;

    public int maderaQueDa = 3;

    public void Golpear(CraftingSystem crafting)
    {
        if (crafting == null || crafting.inventory == null) return;

        if (!crafting.EstaCrafteado(CraftingSystem.Crafteos.Hacha))
        {
            crafting.MostrarMensaje("Falta hacha");
            return;
        }

        golpesActuales++;

        if (golpesActuales >= golpesNecesarios)
        {
            crafting.inventory.AddResource(PlayerInventory.TipoRecurso.Madera, maderaQueDa);
            Destroy(gameObject);
        }
    }

    public string GetTexto(CraftingSystem crafting)
    {
        if (!crafting.EstaCrafteado(CraftingSystem.Crafteos.Hacha))
            return "Necesitás hacha";

        return "E: talar (" + golpesActuales + "/" + golpesNecesarios + ")";
    }
}