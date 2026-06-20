using System.Collections;
using UnityEngine;

public class AmbienteNoche : MonoBehaviour
{
    public AudioSource audioBaseNoche;
    public AudioSource audioEventosNoche;

    public AudioClip sonidoBaseNoche;
    public AudioClip[] sonidosRandomNoche;

    private bool nocheActiva = false;

    public void ActivarNoche()
    {
        if (nocheActiva)
            return;

        nocheActiva = true;

        if (audioBaseNoche != null && sonidoBaseNoche != null)
        {
            audioBaseNoche.clip = sonidoBaseNoche;
            audioBaseNoche.loop = true;
            audioBaseNoche.Play();
        }

        StartCoroutine(SonidosAleatorios());
    }

    IEnumerator SonidosAleatorios()
    {
        while (nocheActiva)
        {
            yield return new WaitForSeconds(Random.Range(8f, 20f));

            if (audioEventosNoche != null && sonidosRandomNoche.Length > 0)
            {
                int random = Random.Range(0, sonidosRandomNoche.Length);
                audioEventosNoche.PlayOneShot(sonidosRandomNoche[random]);
            }
        }
    }
}