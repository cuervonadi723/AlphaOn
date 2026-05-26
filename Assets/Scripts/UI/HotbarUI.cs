using UnityEngine;
using UnityEngine.UI;

public class HotbarUI : MonoBehaviour
{
    [System.Serializable]
    public class ItemHotbar
    {
        public MochilaUI.ItemID itemID;
        public GameObject objetoEnMano;
    }

    [Header("Slots visuales")]
    public Image[] slots;

    [Header("Items en mano")]
    public ItemHotbar[] itemsHotbar;

    [Header("Colores")]
    public Color normalColor = new Color32(60, 60, 60, 180);
    public Color selectedColor = new Color32(180, 160, 90, 220);

    public int selectedSlot = 0;

    private MochilaUI.ItemID?[] hotbarItems = new MochilaUI.ItemID?[6];

    void Start()
    {
        UpdateHotbarVisual();
        ActualizarObjetoEnMano();
    }

    void Update()
    {
        if (slots == null || slots.Length == 0)
            return;

        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll > 0f)
        {
            selectedSlot--;

            if (selectedSlot < 0)
                selectedSlot = slots.Length - 1;

            UpdateHotbarVisual();
            ActualizarObjetoEnMano();
        }

        if (scroll < 0f)
        {
            selectedSlot++;

            if (selectedSlot >= slots.Length)
                selectedSlot = 0;

            UpdateHotbarVisual();
            ActualizarObjetoEnMano();
        }

        for (int i = 0; i < slots.Length; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                selectedSlot = i;
                UpdateHotbarVisual();
                ActualizarObjetoEnMano();
            }
        }
    }

    public void RecibirHotbar(MochilaUI.ItemID?[] nuevosItems)
    {
        for (int i = 0; i < hotbarItems.Length; i++)
        {
            if (nuevosItems != null && i < nuevosItems.Length)
                hotbarItems[i] = nuevosItems[i];
            else
                hotbarItems[i] = null;
        }

        ActualizarObjetoEnMano();
    }

    void UpdateHotbarVisual()
    {
        if (slots == null)
            return;

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i] != null)
                slots[i].color = (i == selectedSlot) ? selectedColor : normalColor;
        }
    }

    void ActualizarObjetoEnMano()
    {
        for (int i = 0; i < itemsHotbar.Length; i++)
        {
            if (itemsHotbar[i].objetoEnMano != null)
                itemsHotbar[i].objetoEnMano.SetActive(false);
        }

        if (selectedSlot < 0 || selectedSlot >= hotbarItems.Length)
            return;

        if (!hotbarItems[selectedSlot].HasValue)
            return;

        MochilaUI.ItemID itemSeleccionado = hotbarItems[selectedSlot].Value;

        for (int i = 0; i < itemsHotbar.Length; i++)
        {
            if (itemsHotbar[i].itemID == itemSeleccionado)
            {
                if (itemsHotbar[i].objetoEnMano != null)
                    itemsHotbar[i].objetoEnMano.SetActive(true);

                return;
            }
        }
    }

    public int GetSelectedSlot()
    {
        return selectedSlot;
    }

    public bool SlotTieneItem(MochilaUI.ItemID item)
    {
        if (selectedSlot < 0 || selectedSlot >= hotbarItems.Length)
            return false;

        return hotbarItems[selectedSlot].HasValue &&
               hotbarItems[selectedSlot].Value == item;
    }
}