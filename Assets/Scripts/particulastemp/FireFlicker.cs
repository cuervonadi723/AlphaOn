using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFlicker : MonoBehaviour
{
    private Light luz;
    public float intensidadBase = 2f;
    public float variacion = 0.4f;

    void Start()
    {
        luz = GetComponent<Light>();
    }

    void Update()
    {
        luz.intensity =
            intensidadBase +
            Random.Range(-variacion, variacion);
    }
}