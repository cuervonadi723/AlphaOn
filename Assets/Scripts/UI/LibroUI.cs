using UnityEngine;

public class LibroUI : MonoBehaviour
{
    [Header("UI que se desbloquea")]
    public GameObject hotbar;
    public MochilaUI mochilaUI;
    public GameObject textoAyuda;

    [Header("Nuevo libro de crafteo")]
    public LibroCrafteoUI libroCrafteoUI;

    public bool libroDesbloqueado = false;

    void Start()
    {
        if (hotbar != null)
            hotbar.SetActive(false);

        if (mochilaUI != null && mochilaUI.mochilaPanel != null)
            mochilaUI.mochilaPanel.SetActive(false);

        if (textoAyuda != null)
            textoAyuda.SetActive(false);
    }

    public void DesbloquearLibro()
    {
        libroDesbloqueado = true;

        if (hotbar != null)
            hotbar.SetActive(true);

        if (textoAyuda != null)
            textoAyuda.SetActive(true);

        if (mochilaUI != null)
            mochilaUI.DesbloquearMochila();

        if (libroCrafteoUI != null)
            libroCrafteoUI.DesbloquearLibro();
    }
}