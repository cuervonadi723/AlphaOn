using UnityEngine;
public class AnimalPasivo : MonoBehaviour
{
    public float velocidad = 2f;

    void Update()
    {
        transform.Translate(Vector3.forward * velocidad * Time.deltaTime);
    }
}