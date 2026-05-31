using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BidonInteraccion : MonoBehaviour
{
    public void Tomar(CraftingSystem crafting)
    {
        if (ProgresoAntena.instance.tieneBidon)
            return;

        ProgresoAntena.instance.tieneBidon = true;
        ProgresoAntena.instance.puedeRevisarCamioneta = true;

        crafting.inventory.AddResource(PlayerInventory.TipoRecurso.BidonVacio, 1);
        FindObjectOfType<MochilaUI>().ActualizarUI();

        crafting.MostrarMensaje("Agarraste el bidón vacío. Podrías llenarlo con combustible.");

        Destroy(gameObject);
    }

    public string GetTexto()
    {
        return "E: tomar bidón";
    }
}