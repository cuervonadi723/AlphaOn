using UnityEngine;

public class MovimientoAgua : MonoBehaviour
{
    public Renderer renderAgua;

    [Header("Movimiento textura")]
    public float velocidadX = 0.02f;
    public float velocidadY = 0.01f;

    [Header("Movimiento agua")]
    public float velocidadSubida = 0.5f;
    public float alturaMovimiento = 0.05f;

    private Vector3 posicionInicial;

    void Start()
    {
        posicionInicial = transform.position;
    }

    void Update()
    {
        
        if (renderAgua != null)
        {
            Vector2 offset = renderAgua.material.mainTextureOffset;

            offset.x += velocidadX * Time.deltaTime;
            offset.y += velocidadY * Time.deltaTime;

            renderAgua.material.mainTextureOffset = offset;
        }

        
        float movimiento = Mathf.Sin(Time.time * velocidadSubida) * alturaMovimiento;

        transform.position = new Vector3(
            posicionInicial.x,
            posicionInicial.y + movimiento,
            posicionInicial.z
        );
    }
}