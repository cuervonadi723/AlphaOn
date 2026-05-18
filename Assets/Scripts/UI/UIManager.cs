using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public PlayerInventory inventory;

    public TextMeshProUGUI PiedraText;
    public TextMeshProUGUI PaloText;
    public TextMeshProUGUI MaderaText;

    void Update()
    {
        if (inventory == null) return;

        if (PiedraText != null)
            PiedraText.text = "Piedras: " + inventory.GetResource(PlayerInventory.TipoRecurso.Piedra);

        if (PaloText != null)
            PaloText.text = "Ramas: " + inventory.GetResource(PlayerInventory.TipoRecurso.Rama);

        if (MaderaText != null)
            MaderaText.text = "Madera: " + inventory.GetResource(PlayerInventory.TipoRecurso.Madera);
    }
}