using UnityEngine;
using UnityEngine.UI;

public class Stat : MonoBehaviour
{
    public float current = 100f;
    public float max = 100f;
    public float drain = 0.2f;
    public float healthDrain = 1f;
    public Image barra;

    void Update()
    {
        current -= drain * Time.deltaTime;
        current = Mathf.Clamp(current, 0f, max);

        if (barra != null)
            barra.fillAmount = current / max;
    }

    public void Add(float amount)
    {
        current += amount;
        current = Mathf.Clamp(current, 0f, max);

    }
}
