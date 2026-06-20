using UnityEngine;
using TMPro;

public class PlayerInput : MonoBehaviour
{
    public HotbarUI hotbar;
    public CraftingSystem crafting;
    public float rango = 7f;

    public TextMeshProUGUI interactuarTexto;
    public GameObject lataVaciaPrefab;
    public GameObject botellaVaciaPrefab;
    public Transform puntoSoltar;

    [Header("Audios")]
    public AudioSource audioSource;
    public AudioClip sonidoComerLata;
    public AudioClip sonidoVenda;
    public AudioClip sonidoTomarAgua;


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
                interactuarTexto.text = "RECOLECTAR";
                return;
            }

            Tree arbol = hit.collider.GetComponentInParent<Tree>();
            if (arbol != null)
            {
                interactuarTexto.gameObject.SetActive(true);
                interactuarTexto.text = "TALAR";
                return;
            }

            RockNode roca = hit.collider.GetComponentInParent<RockNode>();
            if (roca != null)
            {
                interactuarTexto.gameObject.SetActive(true);
                interactuarTexto.text = "PICAR";
                return;
            }

            UnlockObstacle obstaculo = hit.collider.GetComponentInParent<UnlockObstacle>();
            if (obstaculo != null)
            {
                interactuarTexto.gameObject.SetActive(true);
                interactuarTexto.text = "ROMPER";
                return;
            }

            FoodItem comida = hit.collider.GetComponentInParent<FoodItem>();
            if (comida != null)
            {
                interactuarTexto.gameObject.SetActive(true);
                interactuarTexto.text = "TOMAR";
                return;
            }

            BotellaAguaItem botella = hit.collider.GetComponentInParent<BotellaAguaItem>();
            if (botella != null)
            {
                interactuarTexto.gameObject.SetActive(true);
                interactuarTexto.text = "TOMAR";
                return;
            }

            WaterSource agua = hit.collider.GetComponentInParent<WaterSource>();
            if (agua != null)
            {
                interactuarTexto.gameObject.SetActive(true);
                interactuarTexto.text = "BEBER";
                return;
            }

            NotaInteraccion nota = hit.collider.GetComponentInParent<NotaInteraccion>();
            if (nota != null)
            {
                interactuarTexto.gameObject.SetActive(true);
                interactuarTexto.text = "LEER";
                return;
            }

            GeneradorInteraccion generador = hit.collider.GetComponentInParent<GeneradorInteraccion>();
            if (generador != null)
            {
                interactuarTexto.gameObject.SetActive(true);
                interactuarTexto.text = "REVISAR";
                return;
            }

            PanelElectricoInteraccion panel = hit.collider.GetComponentInParent<PanelElectricoInteraccion>();
            if (panel != null)
            {
                interactuarTexto.gameObject.SetActive(true);
                interactuarTexto.text = "REVISAR";
                return;
            }

            BidonInteraccion bidon = hit.collider.GetComponentInParent<BidonInteraccion>();
            if (bidon != null)
            {
                interactuarTexto.gameObject.SetActive(true);
                interactuarTexto.text = "TOMAR";
                return;
            }

            CamionetaInteraccion camioneta = hit.collider.GetComponentInParent<CamionetaInteraccion>();
            if (camioneta != null)
            {
                string texto = camioneta.GetTexto();

                if (texto != "")
                {
                    interactuarTexto.gameObject.SetActive(true);
                    interactuarTexto.text = "REVISAR";
                    return;
                }
            }
            //MIMIR
            DormirInteraccion cama = hit.collider.GetComponentInParent<DormirInteraccion>();
            if (cama != null)
            {
                interactuarTexto.gameObject.SetActive(true);
                interactuarTexto.text = "DORMIR";
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

            FoodItem comida = hit.collider.GetComponentInParent<FoodItem>();
            if (comida != null)
            {
                comida.Recolectar(crafting);
                return;
            }

            BotellaAguaItem botella = hit.collider.GetComponentInParent<BotellaAguaItem>();
            if (botella != null)
            {
                botella.Recolectar(crafting);
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

        if (hotbar != null && hotbar.SlotTieneItem(MochilaUI.ItemID.Venda))
        {
            UsarVenda();
            return;
        }

        if (hotbar != null && hotbar.SlotTieneItem(MochilaUI.ItemID.LataComida))
        {
            UsarLataComida();
            return;
        }

        if (hotbar != null && hotbar.SlotTieneItem(MochilaUI.ItemID.BotellaAgua))
        {
            UsarBotellaAgua();
            return;
        }



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

            UnlockObstacle obstaculo = hit.collider.GetComponentInParent<UnlockObstacle>();

            if (obstaculo != null)
            {
                if (hotbar != null && hotbar.SlotTieneItem(MochilaUI.ItemID.Hacha))
                    obstaculo.IntentarDesbloquear(crafting, hit);
                else
                    crafting.MostrarMensaje("Necesito tener el hacha en la mano");

                return;
            }
        }
    }
    void UsarVenda()
    {
        PlayerStats stats = GetComponent<PlayerStats>();
        PlayerInventory inventario = GetComponent<PlayerInventory>();

        if (stats == null || inventario == null)
            return;

        if (!inventario.HasResource(PlayerInventory.TipoRecurso.Venda, 1))
        {
            crafting.MostrarMensaje("No tengo vendas.");
            return;
        }

        if (stats.health >= stats.maxHealth)
        {
            crafting.MostrarMensaje("Ya estoy bien.");
            return;
        }

        stats.health = stats.maxHealth;

        inventario.RemoveResource(PlayerInventory.TipoRecurso.Venda, 1);

        if (audioSource != null && sonidoVenda != null)
            audioSource.PlayOneShot(sonidoVenda);

        MochilaUI mochila = FindObjectOfType<MochilaUI>();
        if (mochila != null)
            mochila.ActualizarUI();

        crafting.MostrarMensaje("Usaste una venda.");
    }

    void UsarLataComida()
    {
        PlayerStats stats = GetComponent<PlayerStats>();
        PlayerInventory inventario = GetComponent<PlayerInventory>();

        if (stats == null || inventario == null)
            return;

        if (!inventario.HasResource(PlayerInventory.TipoRecurso.LataComida, 1))
        {
            crafting.MostrarMensaje("No tengo comida.");
            return;
        }

        if (stats.food.current >= 100f)
        {
            crafting.MostrarMensaje("No tengo hambre.");
            return;
        }

        stats.food.Add(30f);

        inventario.RemoveResource(PlayerInventory.TipoRecurso.LataComida, 1);

        if (lataVaciaPrefab != null && puntoSoltar != null)
        {
            Instantiate(
                lataVaciaPrefab,
                puntoSoltar.position,
                puntoSoltar.rotation
            );
        }

        if (audioSource != null && sonidoComerLata != null)
            audioSource.PlayOneShot(sonidoComerLata);

        MochilaUI mochila = FindObjectOfType<MochilaUI>();
        if (mochila != null)
            mochila.ActualizarUI();

        crafting.MostrarMensaje("Comiste una lata de comida.");
    }

    void UsarBotellaAgua()
    {
        PlayerStats stats = GetComponent<PlayerStats>();
        PlayerInventory inventario = GetComponent<PlayerInventory>();

        if (stats == null || inventario == null)
            return;

        if (!inventario.HasResource(PlayerInventory.TipoRecurso.BotellaAgua, 1))
        {
            crafting.MostrarMensaje("No tengo agua.");
            return;
        }

        if (stats.water.current >= 100f)
        {
            crafting.MostrarMensaje("No tengo sed.");
            return;
        }

        stats.water.Add(30f);

        inventario.RemoveResource(PlayerInventory.TipoRecurso.BotellaAgua, 1);

        if (botellaVaciaPrefab != null && puntoSoltar != null)
        {
            Instantiate(
                botellaVaciaPrefab,
                puntoSoltar.position,
                puntoSoltar.rotation
            );
        }

        if (audioSource != null && sonidoTomarAgua != null)
            audioSource.PlayOneShot(sonidoTomarAgua);

        MochilaUI mochila = FindObjectOfType<MochilaUI>();
        if (mochila != null)
            mochila.ActualizarUI();

        crafting.MostrarMensaje("Tomaste agua.");
    }
}