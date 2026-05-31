using UnityEngine;

public class Tree : MonoBehaviour
{
    [Header("Audio")]
    public AudioClip sonidoGolpe;

    [Header("Particulas")]
    public ParticleSystem particulasMadera;
    public ParticleSystem particulasHojas;

    [Header("Recursos")]
    public int golpesNecesarios = 8;
    private int golpesActuales = 0;

    public int maderaQueDa = 4;
    public int hojasQueDa = 5;

    private AudioSource audioSource;

    void Start()
    {
        PlayerInput player = FindObjectOfType<PlayerInput>();

        if (player != null)
            audioSource = player.GetComponent<AudioSource>();
    }

    public void Golpear(CraftingSystem crafting, RaycastHit hit)
    {
        if (crafting == null || crafting.inventory == null)
            return;

        if (!crafting.EstaCrafteado(CraftingSystem.Crafteos.Hacha))
        {
            crafting.MostrarMensaje("Falta hacha");
            return;
        }

        if (audioSource != null && sonidoGolpe != null)
        {
            audioSource.pitch = Random.Range(0.7f, 1.3f);
            audioSource.volume = Random.Range(0.5f, 0.7f);
            audioSource.PlayOneShot(sonidoGolpe);
        }

        if (particulasMadera != null)
        {
            ParticleSystem p = Instantiate(
                particulasMadera,
                hit.point,
                Quaternion.LookRotation(hit.normal)
            );

            Destroy(p.gameObject, 2f);
        }

        if (particulasHojas != null)
        {
            ParticleSystem p2 = Instantiate(
                particulasHojas,
                hit.point,
                Quaternion.identity
            );

            Destroy(p2.gameObject, 2f);
        }

        golpesActuales++;

        if (golpesActuales >= golpesNecesarios)
        {
            bool agregoMadera = crafting.inventory.AddResource(PlayerInventory.TipoRecurso.Madera, maderaQueDa);
            bool agregoHojas = crafting.inventory.AddResource(PlayerInventory.TipoRecurso.Hojas, hojasQueDa);

            if (agregoMadera || agregoHojas)
            {
                crafting.MostrarMensaje("Recolectaste madera y hojas");
            }
            else
            {
                crafting.MostrarMensaje("No puedo cargar más recursos");
            }

            MochilaUI mochila = FindObjectOfType<MochilaUI>();
            if (mochila != null)
                mochila.ActualizarUI();

            Destroy(gameObject);
        }
    }

    public string GetTexto(CraftingSystem crafting)
    {
        if (!crafting.EstaCrafteado(CraftingSystem.Crafteos.Hacha))
            return "Necesitás hacha";

        return "E: talar (" + golpesActuales + "/" + golpesNecesarios + ")";
    }
}