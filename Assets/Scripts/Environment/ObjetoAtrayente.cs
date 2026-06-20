using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetoAtrayente : MonoBehaviour
{
    private Vector3 posicionInicial;

    public float altura = 0.08f;
    public float velocidad = 2f;

    void Start()
    {
        posicionInicial = transform.position;
    }

    void Update()
    {
        transform.position = posicionInicial + Vector3.up * Mathf.Sin(Time.time * velocidad) * altura;
    }
}