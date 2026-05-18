using UnityEngine;

public class Animal : MonoBehaviour
{
    public int vida = 3;
    public int cueroQueDa = 2;

    public void RecibirGolpe(PlayerInventory inv)
    {
        vida--;

        if (vida <= 0)
        {
            // Por ahora es el item de piedra pero cuando hago el cuero lo cambio :D
            inv.AddResource(PlayerInventory.TipoRecurso.Piedra, cueroQueDa);

            Destroy(gameObject);
        }
    }
}