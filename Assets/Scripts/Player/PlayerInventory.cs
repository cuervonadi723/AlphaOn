using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public enum TipoRecurso
    {
        Piedra,
        Rama,
        Madera
    }

    public int[] resources = new int[3];

    [Header("Limites")]
    public int maxPiedra = 8;
    public int maxRama = 10;
    public int maxMadera = 6;

    public int GetResource(TipoRecurso tipo)
    {
        return resources[(int)tipo];
    }

    public bool AddResource(TipoRecurso tipo, int cantidad)
    {
        int index = (int)tipo;
        int limite = GetMaxForType(tipo);

        if (resources[index] >= limite)
            return false;

        resources[index] += cantidad;

        if (resources[index] > limite)
            resources[index] = limite;

        return true;
    }

    public bool HasResource(TipoRecurso tipo, int cantidad)
    {
        return resources[(int)tipo] >= cantidad;
    }

    public void RemoveResource(TipoRecurso tipo, int cantidad)
    {
        resources[(int)tipo] -= cantidad;

        if (resources[(int)tipo] < 0)
            resources[(int)tipo] = 0;
    }

    int GetMaxForType(TipoRecurso tipo)
    {
        switch (tipo)
        {
            case TipoRecurso.Piedra:
                return maxPiedra;
            case TipoRecurso.Rama:
                return maxRama;
            case TipoRecurso.Madera:
                return maxMadera;
        }

        return 0;
    }
}