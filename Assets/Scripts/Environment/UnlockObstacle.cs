using UnityEngine;

public class UnlockObstacle : MonoBehaviour
{
    [Header("Requisitos")]
    public int maderaNecesaria = 3;
    public int piedraNecesaria = 3;

    [Header("Libro")]
    public LibroUI libroUI;

    public void IntentarDesbloquear(CraftingSystem crafting)
    {
        if (libroUI != null && !libroUI.libroDesbloqueado)
        {
            if (crafting != null)
                crafting.MostrarMensaje("Primero debería revisar el libro de supervivencia");

            return;
        }

        if (crafting == null || crafting.inventory == null) return;

        bool tieneMadera = crafting.inventory.HasResource(PlayerInventory.TipoRecurso.Madera, maderaNecesaria);
        bool tienePiedra = crafting.inventory.HasResource(PlayerInventory.TipoRecurso.Piedra, piedraNecesaria);

        if (!tieneMadera || !tienePiedra)
        {
            crafting.MostrarMensaje("Necesitas " + maderaNecesaria + " madera y " + piedraNecesaria + " piedra");
            return;
        }

        crafting.inventory.RemoveResource(PlayerInventory.TipoRecurso.Madera, maderaNecesaria);
        crafting.inventory.RemoveResource(PlayerInventory.TipoRecurso.Piedra, piedraNecesaria);

        Destroy(gameObject);
    }
}