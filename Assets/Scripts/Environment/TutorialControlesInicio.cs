using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TutorialControlesInicio : MonoBehaviour
{
    public UIFade fadeTutorial;
    public float demora = 1f;
    public float duracion = 8f;

    void Start()
    {
        StartCoroutine(MostrarTutorial());
    }

    IEnumerator MostrarTutorial()
    {
        yield return new WaitForSeconds(demora);

        fadeTutorial.Mostrar();

        yield return new WaitForSeconds(duracion);

        fadeTutorial.Ocultar();
    }
}