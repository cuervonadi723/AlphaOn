using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Collections;


public class PensamientoJugador : MonoBehaviour
{
    public TextMeshProUGUI textoPensamiento;
    public float duracion = 5f;

    [Header("Pensamiento inicial")]
    public bool mostrarAlInicio = true;
    public string pensamientoInicial = "Me duele todo... necesito pedir ayuda y encontrar algo para curarme.";
    public float demoraInicial = 2f;

    private Coroutine rutinaActual;

    void Start()
    {
        if (textoPensamiento != null)
            textoPensamiento.text = "";

        if (mostrarAlInicio)
            Invoke(nameof(MostrarInicial), demoraInicial);
    }

    void MostrarInicial()
    {
        MostrarPensamiento(pensamientoInicial);
    }

    public void MostrarPensamiento(string mensaje)
    {
        if (textoPensamiento == null)
            return;

        if (rutinaActual != null)
            StopCoroutine(rutinaActual);

        rutinaActual = StartCoroutine(MostrarTemporal(mensaje));
    }

    IEnumerator MostrarTemporal(string mensaje)
    {
        textoPensamiento.text = mensaje;

        yield return new WaitForSeconds(duracion);

        textoPensamiento.text = "";
    }
}