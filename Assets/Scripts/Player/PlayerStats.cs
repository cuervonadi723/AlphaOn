using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [Header("Stamina")]
    public float stamina = 100f;
    public float staminaDrain = 20f;
    public float staminaRegen = 15f;
    public bool isRunning;

    [Header("Stats")]
    public Stat food;
    public Stat water;

    [Header("Vida")]
    public float health = 100f;
    public float maxHealth = 100f;
    public Image barraVida;
    public Image pantallaDanio;

    [Header("Regeneracion")]
    public float healthRegen = 5f;

    void Update()
    {
        HandleStamina();
        HandleHealth();
        UpdateUI();
    }

    void HandleHealth()
    {
        if (food != null && food.current <= 0)
            health -= food.healthDrain * Time.deltaTime;

        if (water != null && water.current <= 0)
            health -= water.healthDrain * Time.deltaTime;

        if (food != null && water != null && food.current > 80f && water.current > 80f)
        {
            health += healthRegen * Time.deltaTime;
        }

        health = Mathf.Clamp(health, 0, maxHealth);
    }

    void UpdateUI()
    {
        if (barraVida != null)
            barraVida.fillAmount = health / maxHealth;

        if (pantallaDanio != null)
        {
            float healthPercent = health / maxHealth;
            float healthDanger = 0f;

            if (healthPercent <= 0.05f)
            {
                healthDanger = 1f - (healthPercent / 0.05f);
            }

            pantallaDanio.color = new Color(0.6f, 0f, 0f, healthDanger * 0.6f);
        }
    }

    void HandleStamina()
    {
        if (Input.GetKey(KeyCode.LeftShift) && stamina > 0)
        {
            isRunning = true;
            stamina -= staminaDrain * Time.deltaTime;
        }
        else
        {
            isRunning = false;
            stamina += staminaRegen * Time.deltaTime;
        }

        stamina = Mathf.Clamp(stamina, 0, 100);
    }
}