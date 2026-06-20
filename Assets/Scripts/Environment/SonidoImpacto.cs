using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonidoImpacto : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip sonidoImpacto;

    private bool yaSono = false;
    
    private void OnCollisionEnter(Collision collision)
    {
        if (yaSono)
            return;
        if (audioSource != null && sonidoImpacto != null)
        {
            audioSource.PlayOneShot(sonidoImpacto);
            yaSono = true;
        }
    }
}
