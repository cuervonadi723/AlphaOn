using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MesaTrabajoInteraccion : MonoBehaviour
{
    public GameObject textoE;
    public CraftingSystem crafting;
    public LibroCrafteoUI libroCrafteoUI;
    public PensamientoJugador pensamiento;

    public bool tieneEnergia = false;

    private bool jugadorCerca = false;
    private bool yaActualizoLibro = false;

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

        if (!yaActualizoLibro)
        {
            yaActualizoLibro = true;

            if (libroCrafteoUI != null)
                libroCrafteoUI.DesbloquearRecetasAvanzadas();

            crafting.MostrarMensaje("La mesa de trabajo volvió a funcionar.");

            if (pensamiento != null)
            {
                pensamiento.MostrarPensamiento(
                    "Debería revisar el libro. Tal vez aparecieron nuevas recetas."
                );
            }

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