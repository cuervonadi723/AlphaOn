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

    void Detectar()  //coloco los objetos interactuables, medio logico el comentario pero me sirve p futuro. 
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

            NotaInteraccion nota = hit.collider.GetComponentInParent<NotaInteraccion>();
            if (nota != null)
            {
                interactuarTexto.gameObject.SetActive(true);
                interactuarTexto.text = "E: leer nota";
                return;
            }

            GeneradorInteraccion generador = hit.collider.GetComponentInParent<GeneradorInteraccion>();
            if (generador != null)
            {
                interactuarTexto.gameObject.SetActive(true);
                interactuarTexto.text = generador.GetTexto();
                return;
            }

            PanelElectricoInteraccion panel = hit.collider.GetComponentInParent<PanelElectricoInteraccion>();
            if (panel != null)
            {
                interactuarTexto.gameObject.SetActive(true);
                interactuarTexto.text = panel.GetTexto();
                return;
            }

            BidonInteraccion bidon = hit.collider.GetComponentInParent<BidonInteraccion>();
            if (bidon != null)
            {
                interactuarTexto.gameObject.SetActive(true);
                interactuarTexto.text = bidon.GetTexto();
                return;
            }

            CamionetaInteraccion camioneta = hit.collider.GetComponentInParent<CamionetaInteraccion>();
            if (camioneta != null)
            {
                string texto = camioneta.GetTexto();

                if (texto != "")
                {
                    interactuarTexto.gameObject.SetActive(true);
                    interactuarTexto.text = texto;
                    return;
                }
            }
            //MIMIR
            DormirInteraccion cama = hit.collider.GetComponentInParent<DormirInteraccion>();
            if (cama != null)
            {
                interactuarTexto.gameObject.SetActive(true);
                interactuarTexto.text = cama.GetTexto();
                return;
            }


        }

        interactuarTexto.gameObject.SetActive(false);
    }

    void Interactuar() //coloco los objetos interactuables, medio logico el comentario pero me sirve p futuro.
    {
        Debug.Log("APRETE E");
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

            NotaInteraccion nota = hit.collider.GetComponentInParent<NotaInteraccion>();
            if (nota != null)
            {
                nota.Leer();
                return;
            }

            GeneradorInteraccion generador = hit.collider.GetComponentInParent<GeneradorInteraccion>();

            if (generador != null)
            {
                generador.Revisar(crafting);
                return;
            }

            PanelElectricoInteraccion panel = hit.collider.GetComponentInParent<PanelElectricoInteraccion>();
            if (panel != null)
            {
                panel.Revisar(crafting);
                return;
            }

            BidonInteraccion bidon = hit.collider.GetComponentInParent<BidonInteraccion>();
            if (bidon != null)
            {
                bidon.Tomar(crafting);
                return;
            }

            CamionetaInteraccion camioneta = hit.collider.GetComponentInParent<CamionetaInteraccion>();
            if (camioneta != null)
            {
                camioneta.Revisar(crafting);
                return;
            }
            //MIMIR
            DormirInteraccion cama = hit.collider.GetComponentInParent<DormirInteraccion>();
            if (cama != null)
            {
                cama.Dormir();
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