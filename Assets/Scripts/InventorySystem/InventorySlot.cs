using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class InventorySlot
{
    public Item item;
    public Image icon;
    public GameObject slotUI;
    public Button removeButton;
    public Inventory inventory;
    public void AddItem(Item newItem)
    {
        item = newItem;

        icon.sprite = item.itemIcon;
        icon.enabled = true;
        removeButton.interactable = true;
    }

    public void ClearSlot()
    {
        item = null;

        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
    }

    public void OnRemoveButton()
    {
        inventory.RemoveItem(item);
    }

    public void UseItem()
    {
        if (item != null)
        {
            item.Use();
        }
    }
    public void UpdateSlotUI()
    {
        // actualizar la interfaz de usuario de la ranura de inventario
        if (slotUI != null)
        {
            // busca los componentes de imagen y texto que representan el icono y el nombre del objeto de inventario
            Image slotImage = slotUI.transform.Find("ItemImage").GetComponent<Image>();
            Text slotText = slotUI.transform.Find("ItemText").GetComponent<Text>();

            if (item != null)
            {
                slotImage.sprite = item.itemIcon;
                slotImage.enabled = true;
                slotText.text = item.itemName;
            }
            else
            {
                slotImage.sprite = null;
                slotImage.enabled = false;
                slotText.text = "";
            }
        }
    }
}