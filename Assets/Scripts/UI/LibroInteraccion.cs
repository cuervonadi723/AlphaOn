using UnityEngine;

public class LibroInteraccion : MonoBehaviour
{
    public GameObject textoE;

    public LibroUI libroUI;
    public MapaUI mapaUI;
    public NotaUI notaUI;
    public PensamientoJugador pensamiento;
    public UnlockObstacle obstaculo;

    public AudioSource audioSource;
    public AudioClip sonidoLibro;


    private bool jugadorCerca = false;
    private bool yaLeido = false;

    void Start()
    {
        if (textoE != null)
            textoE.SetActive(false);

        if (obstaculo != null)
            obstaculo.enabled = false;
    }

    void Update()
    {
        if (jugadorCerca && Input.GetKeyDown(KeyCode.E) && !yaLeido)
        {
            if (audioSource != null && sonidoLibro != null)
            {
                audioSource.pitch = Random.Range(0.95f, 1.05f);
                audioSource.PlayOneShot(sonidoLibro);
            }
            
            if (libroUI != null)
                libroUI.DesbloquearLibro();

            if (mapaUI != null)
                mapaUI.DesbloquearMapa();

            if (notaUI != null)
                notaUI.MostrarNota();

            if (pensamiento != null)
                pensamiento.MostrarPensamiento(
                    "Parece que hay algo dentro. Debería encontrar una forma de derribar esa puerta."
                );

            yaLeido = true;

            if (textoE != null)
                textoE.SetActive(false);

            if (obstaculo != null)
                obstaculo.enabled = true;

            Destroy(gameObject, 0.4f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = true;

            if (!yaLeido && textoE != null)
                textoE.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = false;

            if (textoE != null)
                textoE.SetActive(false);
        }
    }
}