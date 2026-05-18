using UnityEngine;

public class FoodItem : MonoBehaviour
{
    public float comidaQueDa = 30f;

    public void Comer(PlayerStats stats)
    {
        if (stats == null || stats.food == null) return;

        stats.food.Add(comidaQueDa);

        Destroy(gameObject);
    }
}