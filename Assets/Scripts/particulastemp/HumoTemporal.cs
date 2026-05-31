using UnityEngine;
using System.Collections;

public class HumoTemporal : MonoBehaviour
{
    public ParticleSystem[] humos;
    public float duracion = 103f; 
    public float tiempoApagado = 8f;

    void Start()
    {
        StartCoroutine(ApagarHumo());
    }

    IEnumerator ApagarHumo()
    {
        yield return new WaitForSeconds(duracion);

        foreach (ParticleSystem humo in humos)
        {
            var emission = humo.emission;
            emission.rateOverTime = 0;
        }

        yield return new WaitForSeconds(tiempoApagado);

        foreach (ParticleSystem humo in humos)
        {
            humo.Stop();
        }
    }
}