using System.Collections;
using UnityEngine;

public class PuertaInteraccion : MonoBehaviour
{
    public GameObject textoE;

    public Transform puerta;

    public float velocidad = 2f;
    public float anguloAbierta = 90f;

    private bool jugadorCerca = false;
    private bool puertaAbierta = false;
    private bool moviendoPuerta = false;

    private Quaternion rotacionCerrada;
    private Quaternion rotacionAbierta;

    public AudioSource audioSource;
    public AudioClip[] sonidosPuerta;

    void Start()
    {
        rotacionCerrada = puerta.localRotation;

        rotacionAbierta = rotacionCerrada * Quaternion.Euler(0, anguloAbierta, 0);

        textoE.SetActive(false);
    }

    void Update()
    {
        if (jugadorCerca && Input.GetKeyDown(KeyCode.E) && !moviendoPuerta)
        {
            if (audioSource != null && sonidosPuerta.Length > 0)
            {
                int random = Random.Range(0, sonidosPuerta.Length);

                audioSource.pitch = Random.Range(0.95f, 1.05f);
                audioSource.PlayOneShot(sonidosPuerta[random]);
            }

            if (puertaAbierta)
            {
                StartCoroutine(MoverPuerta(rotacionCerrada));
            }
            else
            {
                StartCoroutine(MoverPuerta(rotacionAbierta));
            }

            puertaAbierta = !puertaAbierta;
        }
    }

    IEnumerator MoverPuerta(Quaternion destino)
    {
        moviendoPuerta = true;

        while (Quaternion.Angle(puerta.localRotation, destino) > 0.5f)
        {
            puerta.localRotation = Quaternion.Lerp(
                puerta.localRotation,
                destino,
                Time.deltaTime * velocidad
            );

            yield return null;
        }

        puerta.localRotation = destino;

        moviendoPuerta = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = true;

            textoE.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = false;

            textoE.SetActive(false);
        }
    }
}