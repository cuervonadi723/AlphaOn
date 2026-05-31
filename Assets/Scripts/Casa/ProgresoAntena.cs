using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgresoAntena : MonoBehaviour
{
    public static ProgresoAntena instance;

    public bool tieneBidon = false;
    public bool tieneCombustible = false;
    public bool tieneFusibles = false;

    public bool generadorConCombustible = false;
    public bool fusiblesInstalados = false;
    public bool generadorEncendido = false;
    public bool puedeRevisarCamioneta = false;
    public bool debeDormir = false;
    public bool yaDurmio = false;

    void Awake()
    {
        instance = this;
    }
}