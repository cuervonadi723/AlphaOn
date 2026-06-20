using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReproducirAnimacionHerramienta : MonoBehaviour
{
    public string nombreAnimacion;

    private Animator animator;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip sonidoSwing;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (MochilaUI.MochilaAbiertaGlobal)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            if (animator != null)
                animator.Play(nombreAnimacion, 0, 0f);

            if (audioSource != null && sonidoSwing != null)
            {
                audioSource.pitch = Random.Range(0.95f, 1.05f);
                audioSource.PlayOneShot(sonidoSwing);
            }
        }
    }
}