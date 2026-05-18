using UnityEngine;

public class LibroInteraccion : MonoBehaviour
{
    public GameObject textoE;

    public LibroUI libroUI;

    public UnlockObstacle obstaculo;

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
            libroUI.DesbloquearLibro();

            yaLeido = true;

            if (textoE != null)
                textoE.SetActive(false);

            if (obstaculo != null)
                obstaculo.enabled = true;

            gameObject.SetActive(false);
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