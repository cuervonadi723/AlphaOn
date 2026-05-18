using UnityEngine;

public class Fogata : MonoBehaviour
{
    public float curacionPorSegundo = 10f;

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        PlayerStats stats = other.GetComponent<PlayerStats>();
        if (stats == null) return;

        if (stats.health < stats.maxHealth)
        {
            stats.health += curacionPorSegundo * Time.deltaTime;
            stats.health = Mathf.Clamp(stats.health, 0, stats.maxHealth);
        }
    }
}