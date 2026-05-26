using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsignarParticulasArboles : MonoBehaviour
{
    [Header("Particulas")]
    public ParticleSystem particulasMadera;
    public ParticleSystem particulasHojas;

    [Header("Audio")]
    public AudioClip sonidoGolpe;

    [ContextMenu("Asignar efectos a todos los arboles")]
    void Asignar()
    {
        Tree[] arboles = FindObjectsOfType<Tree>();

        for (int i = 0; i < arboles.Length; i++)
        {
            arboles[i].particulasMadera = particulasMadera;
            arboles[i].particulasHojas = particulasHojas;
            arboles[i].sonidoGolpe = sonidoGolpe;
        }

        Debug.Log("Efectos asignados a " + arboles.Length + " arboles");
    }
}