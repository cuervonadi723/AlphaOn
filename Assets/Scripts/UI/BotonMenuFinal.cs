using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BotonMenuFinal : MonoBehaviour
{
    public AudioSource audioFinal;

    public void VolverAlMenu()
    {
        Time.timeScale = 1f;

        if (audioFinal != null)
            audioFinal.Stop();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        SceneManager.LoadScene("MainMenu");
    }
}