using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public enum TipoRecurso
    {
        Piedra,
        Rama,
        Madera,
        Tela,
        Aloe,
        Venda,
        BidonVacio,
        BidonLleno,
        Fusibles,
        Hojas,
        LataComida,
        BotellaAgua,
    }

    [System.Serializable]
    public class RecursoInventario
    {
        public TipoRecurso tipo;
        public int cantidad;
        public int maximo = 10;
    }

    [Header("Recursos")]
    public RecursoInventario[] recursos;

    public int GetResource(TipoRecurso tipo)
    {
        RecursoInventario recurso = BuscarRecurso(tipo);

        if (recurso == null)
            return 0;

        return recurso.cantidad;
    }

    public bool AddResource(TipoRecurso tipo, int cantidad)
    {
        RecursoInventario recurso = BuscarRecurso(tipo);

        if (recurso == null)
            return false;

        if (recurso.cantidad >= recurso.maximo)
            return false;

        recurso.cantidad += cantidad;

        if (recurso.cantidad > recurso.maximo)
            recurso.cantidad = recurso.maximo;

        return true;
    }

    public bool HasResource(TipoRecurso tipo, int cantidad)
    {
        return GetResource(tipo) >= cantidad;
    }


    public int GetMaxResource(TipoRecurso tipo)
    {
        RecursoInventario recurso = BuscarRecurso(tipo);

        if (recurso == null)
            return 0;

        return recurso.maximo;
    }

    public void RemoveResource(TipoRecurso tipo, int cantidad)
    {
        RecursoInventario recurso = BuscarRecurso(tipo);

        if (recurso == null)
            return;

        recurso.cantidad -= cantidad;

        if (recurso.cantidad < 0)
            recurso.cantidad = 0;
    }

    RecursoInventario BuscarRecurso(TipoRecurso tipo)
    {
        for (int i = 0; i < recursos.Length; i++)
        {
            if (recursos[i].tipo == tipo)
                return recursos[i];
        }

        return null;
    }
}