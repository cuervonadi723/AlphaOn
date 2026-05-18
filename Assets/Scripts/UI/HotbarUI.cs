using UnityEngine;
using UnityEngine.UI;

public class HotbarUI : MonoBehaviour
{
    public Image[] slots;

    [Header("Iconos de Items")]
    public GameObject iconoHacha;
    public GameObject iconoPico;
    public GameObject iconoLanza;

    [Header("Crafting")]
    public CraftingSystem crafting;

    public Color normalColor = new Color32(60, 60, 60, 180);
    public Color selectedColor = new Color32(180, 160, 90, 220);

    public int selectedSlot = 0;

    void Start()
    {
        UpdateHotbarVisual();
        UpdateItemsVisual();
    }

    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll > 0f)
        {
            selectedSlot--;
            if (selectedSlot < 0)
                selectedSlot = slots.Length - 1;

            UpdateHotbarVisual();
        }

        if (scroll < 0f)
        {
            selectedSlot++;
            if (selectedSlot >= slots.Length)
                selectedSlot = 0;

            UpdateHotbarVisual();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedSlot = 0;
            UpdateHotbarVisual();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && slots.Length > 1)
        {
            selectedSlot = 1;
            UpdateHotbarVisual();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) && slots.Length > 2)
        {
            selectedSlot = 2;
            UpdateHotbarVisual();
        }

        if (Input.GetKeyDown(KeyCode.Alpha4) && slots.Length > 3)
        {
            selectedSlot = 3;
            UpdateHotbarVisual();
        }

        if (Input.GetKeyDown(KeyCode.Alpha5) && slots.Length > 4)
        {
            selectedSlot = 4;
            UpdateHotbarVisual();
        }

        if (Input.GetKeyDown(KeyCode.Alpha6) && slots.Length > 5)
        {
            selectedSlot = 5;
            UpdateHotbarVisual();
        }

        UpdateItemsVisual();
    }

    void UpdateHotbarVisual()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].color = (i == selectedSlot) ? selectedColor : normalColor;
        }
    }

    void UpdateItemsVisual()
    {
        if (crafting == null)
            return;

        iconoHacha.SetActive(crafting.EstaCrafteado(CraftingSystem.Crafteos.Hacha));
        iconoPico.SetActive(crafting.EstaCrafteado(CraftingSystem.Crafteos.Pico));
        iconoLanza.SetActive(crafting.EstaCrafteado(CraftingSystem.Crafteos.Lanza));
    }

    public int GetSelectedSlot()
    {
        return selectedSlot;
    }
}