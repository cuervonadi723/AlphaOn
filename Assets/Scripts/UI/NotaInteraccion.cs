using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotaInteraccion : MonoBehaviour
{
    public NotaUI notaUI;

    private bool yaLeida = false;

    public void Leer()
    {
        if (yaLeida)
            return;

        yaLeida = true;

        if (notaUI != null)
            notaUI.MostrarNota();

        gameObject.SetActive(false);
    }

    public string GetTexto()
    {
        return "";
    }
}