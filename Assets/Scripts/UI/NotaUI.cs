using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NotaUI : MonoBehaviour
{
    public GameObject notaPanel;
    public CanvasGroup canvasGroup;

    public float velocidadFade = 4f;

    private bool notaAbierta = false;
    private bool puedeCerrar = false;

    void Start()
    {
        if (notaPanel != null)
            notaPanel.SetActive(false);

        if (canvasGroup != null)
            canvasGroup.alpha = 0;
    }

    void Update()
    {
        if (notaAbierta && puedeCerrar &&
            (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Escape)))
        {
            StartCoroutine(CerrarNota());
        }
    }

    public void MostrarNota()
    {
        StartCoroutine(AbrirNota());
    }

    IEnumerator AbrirNota()
    {
        notaPanel.SetActive(true);
        notaAbierta = true;
        puedeCerrar = false;

        while (canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += Time.deltaTime * velocidadFade;
            yield return null;
        }

        canvasGroup.alpha = 1;

        yield return new WaitForSeconds(0.5f);
        puedeCerrar = true;
    }

    IEnumerator CerrarNota()
    {
        puedeCerrar = false;

        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= Time.deltaTime * velocidadFade;
            yield return null;
        }

        canvasGroup.alpha = 0;
        notaPanel.SetActive(false);
        notaAbierta = false;
    }
}