using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [Header("Audio Source")]
    public AudioSource audioSource;

    [Header("Pasos")]
    public AudioClip[] sonidosPasos;

    [Header("Recolectar")]
    public AudioClip[] sonidosRecolectar;

    [Header("Configuracion")]
    public float intervaloPasos = 0.45f;
    public float volumenPasos = 0.18f;
    public float volumenRecolectar = 0.35f;

    private CharacterController controller;
    private float timerPasos;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        ManejarPasos();
    }

    void ManejarPasos()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        bool caminando =
            (horizontal != 0 || vertical != 0) &&
            controller.isGrounded;

        if (caminando)
        {
            timerPasos -= Time.deltaTime;

            if (timerPasos <= 0)
            {
                ReproducirPaso();
                timerPasos = intervaloPasos;
            }
        }
        else
        {
            timerPasos = 0;
        }
    }

    void ReproducirPaso()
    {
        if (sonidosPasos.Length == 0)
            return;

        if (audioSource.isPlaying)
            return;

        AudioClip clip = sonidosPasos[Random.Range(0, sonidosPasos.Length)];

        audioSource.pitch = Random.Range(0.95f, 1.05f);
        audioSource.PlayOneShot(clip, volumenPasos);
    }

    public void ReproducirRecolectar()
    {
        if (sonidosRecolectar.Length == 0)
            return;

        AudioClip clip =
            sonidosRecolectar[
                Random.Range(0, sonidosRecolectar.Length)
            ];

        audioSource.pitch = Random.Range(0.95f, 1.05f);

        audioSource.PlayOneShot(clip, volumenRecolectar);
    }
}