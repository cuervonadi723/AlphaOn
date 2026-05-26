using UnityEngine;
using TMPro;

public class PlayerInput : MonoBehaviour
{
    public HotbarUI hotbar;
    public CraftingSystem crafting;
    public float rango = 5f;

    public TextMeshProUGUI interactuarTexto;

    RaycastHit hit;

    void Update()
    {
        if (MochilaUI.MochilaAbiertaGlobal)
            return;

        Detectar();

        if (Input.GetKeyDown(KeyCode.E))
        {
            Interactuar();
        }

        if (crafting == null) return;

        if (crafting.estaConstruyendo)
        {
            if (Input.GetKeyDown(KeyCode.H))
            {
                crafting.CancelarColocacion();
            }

            return;
        }

        for (int i = 0; i < crafting.recetas.Length; i++)
        {
            CraftingSystem.Crafteos crafteo = (CraftingSystem.Crafteos)i;

            if (Input.GetKeyDown(crafting.TeclaDeCrafteo(crafteo)))
            {
                crafting.Craftear(crafteo);
            }
        }

        if (Input.GetMouseButtonDown(0))
            UsarHerramienta();
    }

    void Detectar()
    {
        if (interactuarTexto == null) return;

        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        if (Physics.Raycast(ray, out hit, rango))
        {
            Recolectable r = hit.collider.GetComponentInParent<Recolectable>();
            if (r != null)
            {
                interactuarTexto.gameObject.SetActive(true);
                interactuarTexto.text = "E: recolectar";
                return;
            }

            Tree arbol = hit.collider.GetComponentInParent<Tree>();
            if (arbol != null)
            {
                interactuarTexto.gameObject.SetActive(true);
                interactuarTexto.text = "Click: " + arbol.GetTexto(crafting).Replace("E: ", "");
                return;
            }

            RockNode roca = hit.collider.GetComponentInParent<RockNode>();
            if (roca != null)
            {
                interactuarTexto.gameObject.SetActive(true);
                interactuarTexto.text = "Click: " + roca.GetTexto(crafting).Replace("E: ", "");
                return;
            }

            UnlockObstacle obstaculo = hit.collider.GetComponentInParent<UnlockObstacle>();
            if (obstaculo != null)
            {
                interactuarTexto.gameObject.SetActive(true);

                interactuarTexto.text = "E: despejar ("
                    + obstaculo.maderaNecesaria + " madera, "
                    + obstaculo.piedraNecesaria + " piedra)";

                return;
            }

            FoodItem comida = hit.collider.GetComponentInParent<FoodItem>();
            if (comida != null)
            {
                interactuarTexto.gameObject.SetActive(true);
                interactuarTexto.text = "E: comer";
                return;
            }

            WaterSource agua = hit.collider.GetComponentInParent<WaterSource>();
            if (agua != null)
            {
                interactuarTexto.gameObject.SetActive(true);
                interactuarTexto.text = "E: beber";
                return;
            }
        }

        interactuarTexto.gameObject.SetActive(false);
    }

    void Interactuar()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        if (Physics.Raycast(ray, out hit, rango))
        {
            Recolectable r = hit.collider.GetComponentInParent<Recolectable>();
            if (r != null)
            {
                r.Recolectar(crafting);
                return;
            }

            UnlockObstacle obstaculo = hit.collider.GetComponentInParent<UnlockObstacle>();
            if (obstaculo != null)
            {
                obstaculo.IntentarDesbloquear(crafting);
                return;
            }

            FoodItem comida = hit.collider.GetComponentInParent<FoodItem>();
            if (comida != null)
            {
                comida.Comer(GetComponent<PlayerStats>());
                return;
            }

            WaterSource agua = hit.collider.GetComponentInParent<WaterSource>();
            if (agua != null)
            {
                agua.Beber(GetComponent<PlayerStats>());
                return;
            }
        }
    }

    void UsarHerramienta()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        if (Physics.Raycast(ray, out hit, rango))
        {
            RockNode roca = hit.collider.GetComponentInParent<RockNode>();
            if (roca != null)
            {
                if (hotbar != null && hotbar.SlotTieneItem(MochilaUI.ItemID.Pico))
                    roca.Golpear(crafting, hit);
                else
                    crafting.MostrarMensaje("Necesito tener el pico en la mano");

                return;
            }

            Tree arbol = hit.collider.GetComponentInParent<Tree>();
            if (arbol != null)
            {
                if (hotbar != null && hotbar.SlotTieneItem(MochilaUI.ItemID.Hacha))
                    arbol.Golpear(crafting, hit);
                else
                    crafting.MostrarMensaje("Necesito tener el hacha en la mano");

                return;
            }

            Animal animal = hit.collider.GetComponentInParent<Animal>();
            if (animal != null)
            {
                if (crafting.EstaCrafteado(CraftingSystem.Crafteos.Lanza))
                    animal.RecibirGolpe(GetComponent<PlayerInventory>());

                return;
            }
        }
    }
}