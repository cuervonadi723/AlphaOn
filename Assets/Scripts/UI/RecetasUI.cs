using TMPro;
using UnityEngine;

public class RecetasUI : MonoBehaviour
{
    public CraftingSystem crafting;
    public TextMeshProUGUI texto;

    void Update()
    {
        if (crafting == null || texto == null) return;

        string resultado = "";

        for (int i = 0; i < crafting.recetas.Length; i++)
        {
            CraftingSystem.Crafteos crafteo = (CraftingSystem.Crafteos)i;
            CraftingSystem.Receta receta = crafting.recetas[i];

            if (crafting.EstaCrafteado(crafteo))
            {
                resultado += "* " + receta.nombre + "\n";
            }
            else
            {
                string materiales = "";

                for (int j = 0; j < receta.recursos.Length; j++)
                {
                    materiales += receta.cantidades[j] + " " + receta.recursos[j];

                    if (j < receta.recursos.Length - 1)
                        materiales += ", ";
                }

                resultado += crafting.TeclaDeCrafteo(crafteo) + ": "
                    + receta.nombre + " (" + materiales + ")\n";
            }
        }

        texto.text = resultado;
    }
}