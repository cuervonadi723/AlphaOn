using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MesaTrabajoInteraccion : MonoBehaviour
{
    public GameObject textoE;

    public CraftingSystem crafting;

    public bool tieneEnergia = false;

    private bool jugadorCerca = false;

    void Start()
    {
        if (textoE != null)
            textoE.SetActive(false);
    }

    void Update()
    {
        if (jugadorCerca && Input.GetKeyDown(KeyCode.E))
        {
            UsarMesa();
        }
    }

    void UsarMesa()
    {
        if (crafting == null)
        {
            Debug.LogWarning("Falta asignar CraftingSystem en MesaTrabajoInteraccion");
            return;
        }

        if (!tieneEnergia)
        {
            crafting.MostrarMensaje("No tiene energía...");
            return;
        }

        crafting.MostrarMensaje("La mesa de trabajo ya funciona.");
    }

    public void ActivarEnergia()
    {
        tieneEnergia = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = true;

            if (textoE != null)
                textoE.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = false;

            if (textoE != null)
                textoE.SetActive(false);
        }
    }
}