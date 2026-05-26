using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class SlotMochilaUI : MonoBehaviour, IPointerClickHandler
{
    public Image icono;
    public TextMeshProUGUI cantidadTexto;
    public Image marcoSeleccionado;

    [HideInInspector] public int index;
    [HideInInspector] public bool esHotbar;
    [HideInInspector] public MochilaUI mochila;

    public void Configurar(MochilaUI mochilaRef, int nuevoIndex, bool hotbar)
    {
        mochila = mochilaRef;
        index = nuevoIndex;
        esHotbar = hotbar;
    }

    public void Mostrar(Sprite sprite, string cantidad)
    {
        if (icono != null)
        {
            icono.sprite = sprite;
            icono.enabled = sprite != null;
        }

        if (cantidadTexto != null)
            cantidadTexto.text = cantidad;
    }

    public void Limpiar()
    {
        if (icono != null)
        {
            icono.sprite = null;
            icono.enabled = false;
        }

        if (cantidadTexto != null)
            cantidadTexto.text = "";
    }

    public void MarcarSeleccionado(bool activo)
    {
        if (marcoSeleccionado != null)
            marcoSeleccionado.gameObject.SetActive(activo);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (mochila != null)
            mochila.ClickSlot(esHotbar, index);
    }
}