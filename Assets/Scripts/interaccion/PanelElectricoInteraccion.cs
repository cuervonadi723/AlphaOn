using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelElectricoInteraccion : MonoBehaviour
{
    public RadioInteraccion radio;
    public MesaTrabajoInteraccion mesaTrabajo;

    private bool sistemaActivado = false;

    public void Revisar(CraftingSystem crafting)
    {
        if (!ProgresoAntena.instance.fusiblesInstalados)
        {
            if (!ProgresoAntena.instance.tieneFusibles)
            {
                crafting.MostrarMensaje("Los fusibles están quemados.");
                return;
            }

            ProgresoAntena.instance.fusiblesInstalados = true;
            ProgresoAntena.instance.tieneFusibles = false;

            crafting.inventory.RemoveResource(PlayerInventory.TipoRecurso.Fusibles, 1);

            MochilaUI mochila = FindObjectOfType<MochilaUI>();
            if (mochila != null)
                mochila.ActualizarUI();

            crafting.MostrarMensaje("Fusibles instalados.");
            return;
        }

        if (!ProgresoAntena.instance.generadorEncendido)
        {
            crafting.MostrarMensaje("Los fusibles están instalados, pero el generador todavía no está encendido.");
            return;
        }

        if (!sistemaActivado)
        {
            sistemaActivado = true;

            if (radio != null)
                radio.ActivarSenal();

            if (mesaTrabajo != null)
                mesaTrabajo.ActivarEnergia();

            crafting.MostrarMensaje("Activaste el sistema. Tal vez ahora funcione la radio de la casa.");
            return;
        }

        crafting.MostrarMensaje("El sistema ya está activado.");
    }

    public string GetTexto()
    {
        if (!ProgresoAntena.instance.fusiblesInstalados)
        {
            if (ProgresoAntena.instance.tieneFusibles)
                return "E: instalar fusibles";

            return "E: revisar tablero";
        }

        if (!ProgresoAntena.instance.generadorEncendido)
            return "E: revisar tablero";

        if (!sistemaActivado)
            return "E: activar sistema";

        return "E: revisar tablero";
    }
}