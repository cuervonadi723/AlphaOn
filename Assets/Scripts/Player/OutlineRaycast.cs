using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineRaycast : MonoBehaviour
{
    public Camera camara;
    public float distancia = 4f;
    public LayerMask capasDetectables;

    private Outline outlineActual;

    void Start()
    {
        if (camara == null)
            camara = Camera.main;
    }

    void Update()
    {
        Ray ray = camara.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, distancia, capasDetectables))
        {
            Outline nuevoOutline = hit.collider.GetComponentInParent<Outline>();

            if (nuevoOutline != outlineActual)
            {
                ApagarOutline();

                outlineActual = nuevoOutline;

                if (outlineActual != null)
                    outlineActual.enabled = true;
            }
        }
        else
        {
            ApagarOutline();
        }
    }

    void ApagarOutline()
    {
        if (outlineActual != null)
        {
            outlineActual.enabled = false;
            outlineActual = null;
        }
    }
}