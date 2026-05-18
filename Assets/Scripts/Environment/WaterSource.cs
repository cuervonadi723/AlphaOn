using UnityEngine;

public class WaterSource : MonoBehaviour
{
    public float aguaQueDa = 40f;

    public void Beber(PlayerStats stats)
    {
        if (stats == null || stats.water == null) return;

        stats.water.Add(aguaQueDa);
    }
}