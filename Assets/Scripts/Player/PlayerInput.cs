using UnityEngine;
using TMPro;

public class PlayerInput : MonoBehaviour
{
    public CraftingSystem crafting;
    public float rango = 5f;

    public TextMeshProUGUI interactuarTexto;

    RaycastHit hit;

    void Update()
    {
        Detectar();

        if (Input.GetKeyDown(KeyCode.E))
        {
            Interactuar();
        }

        if (crafting == null) return;

        // H cancela construccion
        if (crafting.estaConstruyendo)
        {
            if (Input.GetKeyDown(KeyCode.H))
            {
                crafting.CancelarColocacion();
            }

            return;
        }

        // Craft con tecla a designar en inspectr
        for (int i = 0; i < crafting.recetas.Length; i++)
        {
            CraftingSystem.Crafteos crafteo = (CraftingSystem.Crafteos)i;

            if (Input.GetKeyDown(crafting.TeclaDeCrafteo(crafteo)))
            {
                crafting.Craftear(crafteo);
            }
        }

        // Ataque
        if (Input.GetMouseButtonDown(0))
            Atacar();
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
                interactuarTexto.text = arbol.GetTexto(crafting);
                return;
            }

            RockNode roca = hit.collider.GetComponentInParent<RockNode>();
            if (roca != null)
            {
                interactuarTexto.gameObject.SetActive(true);
                interactuarTexto.text = roca.GetTexto(crafting);
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
            RockNode roca = hit.collider.GetComponentInParent<RockNode>();
            if (roca != null)
            {
                roca.Golpear(crafting);
                return;
            }

            Recolectable r = hit.collider.GetComponentInParent<Recolectable>();
            if (r != null)
            {
                r.Recolectar(crafting);
                return;
            }

            Tree t = hit.collider.GetComponentInParent<Tree>();
            if (t != null)
            {
                if (crafting.EstaCrafteado(CraftingSystem.Crafteos.Hacha))
                    t.Golpear(crafting);
                else
                    crafting.MostrarMensaje("Falta hacha");

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

    void Atacar()
    {
        if (!crafting.EstaCrafteado(CraftingSystem.Crafteos.Lanza)) return;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 2f))
        {
            Animal animal = hit.collider.GetComponentInParent<Animal>();

            if (animal != null)
            {
                animal.RecibirGolpe(GetComponent<PlayerInventory>());
            }
        }
    }
}