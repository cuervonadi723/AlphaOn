using UnityEngine;
using TMPro;
using System.Collections;

public class CraftingSystem : MonoBehaviour
{
    public PlayerInventory inventory;
    
    
    //enum paso 1
    public enum Crafteos
    {
        Hacha,
        Pico,
        Lanza,
        Fogata
    }

    [System.Serializable]
    public class Receta
    {
        public string nombre;
        public PlayerInventory.TipoRecurso[] recursos;
        public int[] cantidades;
        public bool crafteado;
        public KeyCode craftingKey;
    }

    [Header("Recetas")]
    public Receta[] recetas;

    [Header("UI")]
    public TextMeshProUGUI mensajeTexto;

    [Header("Fogata")]
    public GameObject fogataPrefab;
    public GameObject fogataPreviewPrefab;
    public LayerMask groundMask;
    public Vector3 offsetFogata = Vector3.zero;

    public bool modoConstruccion = false;
    public bool estaConstruyendo = false;

    private Vector3 posicionConstruccion;
    private GameObject previewActual;


    //un solo metodo para todo. paso 2
    public void Craftear(Crafteos crafteo)
    {
        Receta receta = BuscarReceta(crafteo);

        if (receta == null)
        {
            MostrarMensaje("No existe la receta");
            return;
        }

        if (receta.crafteado && crafteo != Crafteos.Fogata)
        {
            MostrarMensaje("Ya tenés " + receta.nombre);
            return;
        }

        if (!PuedeCraftear(receta))
        {
            MostrarMensaje("Faltan materiales");
            return;
        }

        GastarRecursos(receta);

        if (crafteo == Crafteos.Fogata)
        {
            EmpezarColocarFogata();
            return;
        }

        DarResultado(receta);
        MostrarMensaje(receta.nombre + " creada!");
    }


    // cambie esta parte para buscar por enum y no por nombre como lo tenia antes
    public Receta BuscarReceta(Crafteos crafteo)
    {
        return recetas[(int)crafteo];
    }

    public KeyCode TeclaDeCrafteo(Crafteos crafteo)
    {
        return recetas[(int)crafteo].craftingKey;
    }

    bool PuedeCraftear(Receta receta)
    {
        for (int i = 0; i < receta.recursos.Length; i++)
        {
            if (!inventory.HasResource(receta.recursos[i], receta.cantidades[i]))
                return false;
        }

        return true;
    }

    void GastarRecursos(Receta receta)
    {
        for (int i = 0; i < receta.recursos.Length; i++)
        {
            inventory.RemoveResource(receta.recursos[i], receta.cantidades[i]);
        }
    }

    void DarResultado(Receta receta)
    {
        receta.crafteado = true;
    }


    // bool agregado como era el paso 5
    public bool EstaCrafteado(Crafteos crafteo)
    {
        return recetas[(int)crafteo].crafteado;
    }

    public void MostrarMensaje(string mensaje)
    {
        StopAllCoroutines();
        StartCoroutine(MostrarMensajeTemporal(mensaje));
    }

    IEnumerator MostrarMensajeTemporal(string mensaje)
    {
        mensajeTexto.text = mensaje;
        yield return new WaitForSeconds(5f);
        mensajeTexto.text = "";
    }

    public void EmpezarColocarFogata()
    {
        if (modoConstruccion)
            return;

        if (fogataPrefab == null)
        {
            MostrarMensaje("Falta asignar prefab");
            return;
        }

        if (fogataPreviewPrefab == null)
        {
            MostrarMensaje("Falta asignar preview");
            return;
        }

        modoConstruccion = true;
        estaConstruyendo = true;

        previewActual = Instantiate(
            fogataPreviewPrefab,
            Camera.main.transform.position + Camera.main.transform.forward * 3f,
            Quaternion.identity
        );

        previewActual.SetActive(true);
    }

    void ColocarFogata()
    {
        Instantiate(fogataPrefab, posicionConstruccion, Quaternion.identity);

        if (previewActual != null)
            Destroy(previewActual);

        modoConstruccion = false;
        estaConstruyendo = false;

        MostrarMensaje("Fogata colocada!");
    }

    public void CancelarColocacion()
    {
        if (previewActual != null)
            Destroy(previewActual);

        modoConstruccion = false;
        estaConstruyendo = false;

        MostrarMensaje("Construcción cancelada");
    }

    void Update()
    {
        if (modoConstruccion && previewActual != null)
        {
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 10f, groundMask))
            {
                posicionConstruccion = hit.point + offsetFogata;
                previewActual.transform.position = posicionConstruccion;
            }

            if (Input.GetMouseButtonDown(0))
                ColocarFogata();

            if (Input.GetMouseButtonDown(1))
                CancelarColocacion();
        }
    }
}