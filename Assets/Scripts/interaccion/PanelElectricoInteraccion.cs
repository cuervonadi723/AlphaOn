using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelElectricoInteraccion : MonoBehaviour
{
    public RadioInteraccion radio;
    public MesaTrabajoInteraccion mesaTrabajo;
    public PensamientoJugador pensamiento;
    public ParticleSystem chispasPanel;
    public Light focoPanel;

    private bool sistemaActivado = false;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip sonidoPanelRoto;
    public AudioClip sonidoInstalarFusibles;
    public AudioClip sonidoActivarSistema;

    public void Revisar(CraftingSystem crafting)
    {
        if (!ProgresoAntena.instance.fusiblesInstalados)
        {
            if (!ProgresoAntena.instance.tieneFusibles)
            {
                if (audioSource != null && sonidoPanelRoto != null)
                    audioSource.PlayOneShot(sonidoPanelRoto);
                if (chispasPanel != null)
                    chispasPanel.Play();

                crafting.MostrarMensaje("Los fusibles están quemados.");
                return;
            }

            if (audioSource != null && sonidoInstalarFusibles != null)
                audioSource.PlayOneShot(sonidoInstalarFusibles);

            ProgresoAntena.instance.fusiblesInstalados = true;
            ProgresoAntena.instance.tieneFusibles = false;

            crafting.inventory.RemoveResource(
                PlayerInventory.TipoRecurso.Fusibles,
                1
            );

            MochilaUI mochila = FindObjectOfType<MochilaUI>();

            if (mochila != null)
                mochila.ActualizarUI();

            crafting.MostrarMensaje("Fusibles instalados.");
            return;
        }

        if (!sistemaActivado)
        {
            if (audioSource != null && sonidoActivarSistema != null)
                audioSource.PlayOneShot(sonidoActivarSistema);

            sistemaActivado = true;

            if (focoPanel != null)
                focoPanel.enabled = true;

            if (radio != null)
                radio.ActivarSenal();

            if (mesaTrabajo != null)
                mesaTrabajo.ActivarEnergia();

            if (pensamiento != null)
            {
                pensamiento.MostrarPensamiento(
                    "Ahora que la antena parece funcionar, tal vez se solucionó lo de la radio."
                );
            }

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