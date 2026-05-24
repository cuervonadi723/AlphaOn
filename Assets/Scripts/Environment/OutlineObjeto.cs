using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineObjeto : MonoBehaviour
{
    private Outline outline;

    void Start()
    {
        outline = GetComponent<Outline>();

        if (outline != null)
            outline.enabled = false;
    }

    void OnMouseEnter()
    {
        if (outline != null)
            outline.enabled = true;
    }

    void OnMouseExit()
    {
        if (outline != null)
            outline.enabled = false;
    }
}