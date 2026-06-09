using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PensamientoTrigger : MonoBehaviour
{
    public PensamientoJugador pensamientoJugador;

    [TextArea] //es premiun esto :D
    public string mensaje = "Podría pedir ayuda en esa casa abandonada.";

    private bool yaMostrado = false;

    private void OnTriggerEnter(Collider other)
    {
        if (yaMostrado)
            return;

        if (other.CompareTag("Player"))
        {
            yaMostrado = true;

            if (pensamientoJugador != null)
                pensamientoJugador.MostrarPensamiento(mensaje);

            gameObject.SetActive(false);
        }
    }
}