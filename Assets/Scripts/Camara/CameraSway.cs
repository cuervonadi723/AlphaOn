using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraSway : MonoBehaviour
{
    public float intensidad = 0.015f;
    public float velocidad = 6f;

    private Vector3 posicionInicial;
    private float tiempo;
    private bool iniciado = false;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.3f);

        posicionInicial = transform.localPosition;
        iniciado = true;
    }

    void LateUpdate()
    {
        if (!iniciado)
            return;

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        bool seEstaMoviendo = Mathf.Abs(horizontal) > 0.1f || Mathf.Abs(vertical) > 0.1f;

        if (seEstaMoviendo)
        {
            tiempo += Time.deltaTime * velocidad;

            float movimientoY = Mathf.Sin(tiempo) * intensidad;
            float movimientoX = Mathf.Cos(tiempo * 0.5f) * intensidad * 0.5f;

            Vector3 nuevaPosicion = posicionInicial + new Vector3(movimientoX, movimientoY, 0);

            transform.localPosition = Vector3.Lerp(
                transform.localPosition,
                nuevaPosicion,
                Time.deltaTime * velocidad
            );
        }
        else
        {
            tiempo = 0;

            transform.localPosition = Vector3.Lerp(
                transform.localPosition,
                posicionInicial,
                Time.deltaTime * velocidad
            );
        }
    }
}