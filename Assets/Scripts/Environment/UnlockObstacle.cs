using UnityEngine;

public class UnlockObstacle : MonoBehaviour
{
    [Header("Libro")]
    public LibroUI libroUI;

    [Header("Golpes")]
    public int golpesNecesarios = 3;
    private int golpesActuales = 0;

    [Header("Audio")]
    public AudioClip sonidoGolpe;
    public AudioSource audioSource;

    [Header("Particulas")]
    public ParticleSystem particulasMadera;

    

    public void IntentarDesbloquear(CraftingSystem crafting, RaycastHit hit)
    {
        if (libroUI != null && !libroUI.libroDesbloqueado)
        {
            if (crafting != null)
                crafting.MostrarMensaje("Primero debería revisar el libro de supervivencia.");

            return;
        }

        if (crafting == null)
            return;

        if (!crafting.EstaCrafteado(CraftingSystem.Crafteos.Hacha))
        {
            crafting.MostrarMensaje("Necesito un hacha para romper esto.");
            return;
        }

        if (audioSource != null && sonidoGolpe != null)
        {
            audioSource.pitch = Random.Range(0.7f, 1.2f);
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

        golpesActuales++;

        if (golpesActuales < golpesNecesarios)
        {
            crafting.MostrarMensaje("Golpeando bloqueo... (" + golpesActuales + "/" + golpesNecesarios + ")");
            return;
        }

        crafting.MostrarMensaje("Rompiste el bloqueo con el hacha.");
        Destroy(gameObject, 0.3f);
    }

    public string GetTexto(CraftingSystem crafting)
    {
        if (crafting == null || !crafting.EstaCrafteado(CraftingSystem.Crafteos.Hacha))
            return "Necesitás hacha";

        return "Romper bloqueo (" + golpesActuales + "/" + golpesNecesarios + ")";
    }
}