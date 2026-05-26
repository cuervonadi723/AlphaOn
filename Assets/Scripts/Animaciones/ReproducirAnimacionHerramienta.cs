using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReproducirAnimacionHerramienta : MonoBehaviour
{
    public string nombreAnimacion;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            animator.Play(nombreAnimacion, 0, 0f);
        }
    }
}