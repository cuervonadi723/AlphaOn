using System.Collections;
using UnityEngine;

public class AnimacionHerramienta : MonoBehaviour
{
    [Header("Posiciones")]
    public Vector3 posicionNormal;
    public Vector3 posicionGolpe;

    [Header("Rotaciones")]
    public Vector3 rotacionNormal;
    public Vector3 rotacionGolpe;

    [Header("Velocidades")]
    public float velocidadIda = 14f;
    public float velocidadVuelta = 8f;

    private bool golpeando = false;

    void Start()
    {
        posicionNormal = transform.localPosition;
        rotacionNormal = transform.localEulerAngles;
    }

    void Update()
    {
        // desactivo anim porque hace cosas raras con la mochi abierta
        if (MochilaUI.MochilaAbiertaGlobal)
            return;

        if (Input.GetMouseButtonDown(0) && !golpeando)
        {
            StartCoroutine(Golpe());
        }
    }

    IEnumerator Golpe()
    {
        golpeando = true;

        float tiempo = 0;

        while (tiempo < 1)
        {
            
            if (MochilaUI.MochilaAbiertaGlobal)
            {
                ResetearHerramienta();
                yield break;
            }

            tiempo += Time.deltaTime * velocidadIda;

            transform.localPosition = Vector3.Lerp(
                posicionNormal,
                posicionGolpe,
                tiempo
            );

            transform.localRotation = Quaternion.Lerp(
                Quaternion.Euler(rotacionNormal),
                Quaternion.Euler(rotacionGolpe),
                tiempo
            );

            yield return null;
        }

        tiempo = 0;

        while (tiempo < 1)
        {
            if (MochilaUI.MochilaAbiertaGlobal)
            {
                ResetearHerramienta();
                yield break;
            }

            tiempo += Time.deltaTime * velocidadVuelta;

            transform.localPosition = Vector3.Lerp(
                posicionGolpe,
                posicionNormal,
                tiempo
            );

            transform.localRotation = Quaternion.Lerp(
                Quaternion.Euler(rotacionGolpe),
                Quaternion.Euler(rotacionNormal),
                tiempo
            );

            yield return null;
        }

        ResetearHerramienta();
    }

    void ResetearHerramienta()
    {
        transform.localPosition = posicionNormal;

        transform.localRotation = Quaternion.Euler(rotacionNormal);

        golpeando = false;
    }
}