using UnityEngine;

public class AnimalHostil : MonoBehaviour
{
    public Transform jugador;
    public float velocidad = 3f;
    public float rangoAtaque = 5f;

    void Update()
    {
        if (jugador == null) return;

        float distancia = Vector3.Distance(transform.position, jugador.position);

        if (distancia < rangoAtaque)
        {
            transform.LookAt(jugador);
            transform.position += transform.forward * velocidad * Time.deltaTime;
        }
    }
}