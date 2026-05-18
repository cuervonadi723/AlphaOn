using UnityEngine;
using UnityEngine.UI;

public class OpcionesBrillo : MonoBehaviour
{
    [Header("Referencias")]
    public Image panelBrillo;
    public Slider sliderBrillo;

    [Header("Configuracion")]
    public float oscuridadMaxima = 0.55f;

    private const string KEY_BRILLO = "brillo";

    void Start()
    {
        float brilloGuardado = PlayerPrefs.GetFloat(KEY_BRILLO, 0.5f);

        if (sliderBrillo != null)
            sliderBrillo.SetValueWithoutNotify(brilloGuardado);

        AplicarBrillo(brilloGuardado);
    }

    public void CambiarBrillo(float valor)
    {
        AplicarBrillo(valor);

        PlayerPrefs.SetFloat(KEY_BRILLO, valor);
        PlayerPrefs.Save();
    }

    void AplicarBrillo(float valor)
    {
        if (panelBrillo == null) return;

        float alpha = Mathf.Lerp(oscuridadMaxima, 0f, valor);

        Color color = panelBrillo.color;
        color.a = alpha;
        panelBrillo.color = color;
    }
}