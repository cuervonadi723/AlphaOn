using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorInteraccion : MonoBehaviour
{
    public void Revisar(CraftingSystem crafting)
    {
        if (!ProgresoAntena.instance.generadorConCombustible)
        {
            if (!ProgresoAntena.instance.tieneCombustible)
            {
                crafting.MostrarMensaje("El tanque está vacío.");
                return;
            }

            ProgresoAntena.instance.generadorConCombustible = true;
            ProgresoAntena.instance.tieneCombustible = false;

            crafting.inventory.RemoveResource(PlayerInventory.TipoRecurso.BidonLleno, 1);

            MochilaUI mochila = FindObjectOfType<MochilaUI>();
            if (mochila != null)
                mochila.ActualizarUI();

            crafting.MostrarMensaje("Cargaste combustible en el generador.");
            return;
        }

        if (!ProgresoAntena.instance.fusiblesInstalados)
        {
            crafting.MostrarMensaje("El generador tiene combustible, pero el tablero sigue sin fusibles.");
            return;
        }

        if (!ProgresoAntena.instance.generadorEncendido)
        {
            ProgresoAntena.instance.generadorEncendido = true;
            crafting.MostrarMensaje("Generador encendido. La antena ya debería tener energía.");
            return;
        }

        crafting.MostrarMensaje("El generador ya está funcionando.");
    }

    public string GetTexto()
    {
        if (!ProgresoAntena.instance.generadorConCombustible)
        {
            if (ProgresoAntena.instance.tieneCombustible)
                return "E: cargar combustible";

            return "E: revisar generador";
        }

        if (!ProgresoAntena.instance.fusiblesInstalados)
            return "E: revisar generador";

        if (!ProgresoAntena.instance.generadorEncendido)
            return "E: encender generador";

        return "E: revisar generador";
    }
}