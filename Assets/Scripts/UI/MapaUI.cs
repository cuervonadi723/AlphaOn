using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MapaUI : MonoBehaviour
{
    public GameObject mapaPanel;
    public CanvasGroup canvasGroup;

    private bool mapaDesbloqueado = false;
    private bool mapaAbierto = false;

    public float velocidadFade = 5f;

    void Start()
    {
        mapaPanel.SetActive(false);
        canvasGroup.alpha = 0;
    }

    void Update()
    {
        if (!mapaDesbloqueado)
            return;

        if (Input.GetKeyDown(KeyCode.M))
        {
            if (mapaAbierto)
                StartCoroutine(FadeOut());
            else
                StartCoroutine(FadeIn());
        }
    }

    public void DesbloquearMapa()
    {
        mapaDesbloqueado = true;
    }

    IEnumerator FadeIn()
    {
        mapaPanel.SetActive(true);

        while (canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += Time.deltaTime * velocidadFade;
            yield return null;
        }

        canvasGroup.alpha = 1;
        mapaAbierto = true;
    }

    IEnumerator FadeOut()
    {
        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= Time.deltaTime * velocidadFade;
            yield return null;
        }

        canvasGroup.alpha = 0;

        mapaPanel.SetActive(false);

        mapaAbierto = false;
    }
}