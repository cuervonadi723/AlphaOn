using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorVibracion : MonoBehaviour
{
    private Vector3 posicionInicial;

    public float intensidad = 0.005f;

    void Start()
    {
        posicionInicial = transform.localPosition;
    }

    void Update()
    {
        if (ProgresoAntena.instance != null &&
            ProgresoAntena.instance.generadorEncendido)
        {
            transform.localPosition =
                posicionInicial +
                Random.insideUnitSphere * intensidad;
        }
        else
        {
            transform.localPosition = posicionInicial;
        }
    }
}