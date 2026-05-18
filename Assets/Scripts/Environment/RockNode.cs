using UnityEngine;

public class RockNode : MonoBehaviour
{
    public int golpesNecesarios = 3;
    int golpesActuales = 0;

    public int piedrasQueDa = 3;

    public void Golpear(CraftingSystem crafting)
    {
        if (crafting == null || crafting.inventory == null) return;

        if (!crafting.EstaCrafteado(CraftingSystem.Crafteos.Pico))
        {
            crafting.MostrarMensaje("Falta pico");
            return;
        }

        golpesActuales++;

        if (golpesActuales >= golpesNecesarios)
        {
            crafting.inventory.AddResource(PlayerInventory.TipoRecurso.Piedra, piedrasQueDa);
            Destroy(gameObject);
        }
    }

    public string GetTexto(CraftingSystem crafting)
    {
        if (!crafting.EstaCrafteado(CraftingSystem.Crafteos.Pico))
            return "Necesitás pico";

        return "E: picar (" + golpesActuales + "/" + golpesNecesarios + ")";
    }
}