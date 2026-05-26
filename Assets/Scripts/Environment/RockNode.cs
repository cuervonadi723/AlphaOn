using UnityEngine;

public class RockNode : MonoBehaviour
{
    [Header("Audio")]
    public AudioClip sonidoGolpe;

    [Header("Particulas")]
    public ParticleSystem particulasPiedra;
    public ParticleSystem particulasPolvo;

    [Header("Recursos")]
    public int golpesNecesarios = 3;
    private int golpesActuales = 0;

    public int piedrasQueDa = 3;

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

        if (!crafting.EstaCrafteado(CraftingSystem.Crafteos.Pico))
        {
            crafting.MostrarMensaje("Falta pico");
            return;
        }

        // sonido
        if (audioSource != null && sonidoGolpe != null)
        {
            audioSource.pitch = Random.Range(0.7f, 1.3f);
            audioSource.volume = Random.Range(0.5f, 0.7f);
            audioSource.PlayOneShot(sonidoGolpe);
        }

        // particulkas de piedra
        if (particulasPiedra != null)
        {
            ParticleSystem p = Instantiate(
                particulasPiedra,
                hit.point,
                Quaternion.LookRotation(hit.normal)
            );

            Destroy(p.gameObject, 2f);
        }

        // particulas de polvo
        if (particulasPolvo != null)
        {
            ParticleSystem p2 = Instantiate(
                particulasPolvo,
                hit.point,
                Quaternion.identity
            );

            Destroy(p2.gameObject, 2f);
        }

        golpesActuales++;

        if (golpesActuales >= golpesNecesarios)
        {
            crafting.inventory.AddResource(
                PlayerInventory.TipoRecurso.Piedra,
                piedrasQueDa
            );

            Destroy(gameObject);
        }
    }

    public string GetTexto(CraftingSystem crafting)
    {
        if (!crafting.EstaCrafteado(CraftingSystem.Crafteos.Pico))
            return "Necesitás pico";

        return "E: picar (" + golpesActuales + "/" + golpesNecesarios + ")";
    }
}