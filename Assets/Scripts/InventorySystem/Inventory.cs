using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventory
{
    public List<Item> items = new List<Item>(); // Lista de items en el inventario
    public List<InventorySlot> inventorySlots = new List<InventorySlot>(); //Lista de los slots disponibles

    public void AddItem(Item item) // Añade
    {
        items.Add(item);
    }

    public void RemoveItem(Item item) // Quita
    {
        items.Remove(item);
    }

    public bool HasItem(Item item) // Comprueba si un item está en el inventario
    {
        return items.Contains(item);
    }

    public int GetItemCount(Item item) // Obtiene la cantidad de un item en el inventario
    {
        int count = 0;
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i] == item)
            {
                count++;
            }
        }
        return count;
    }

    public void Clear() // Limpia el inventario
    {
        items.Clear();
    }
    public bool Contains(Item item)
    {
        return items.Contains(item);
    }

    public List<string> GetInventoryData() // Obtiene los datos del inventario en forma de una lista de strings
    {
        List<string> itemNames = new List<string>();
        foreach (InventorySlot slot in inventorySlots)
        {
            if (slot.item != null)
            {
                itemNames.Add(slot.item.itemName); // Añade el nombre del item al inventario
            }
            else
            {
                itemNames.Add("");
            }
        }
        return itemNames;
    }

    public void SetInventoryData(List<string> itemNames, List<Item> itemList) // Establece los datos del inventario a partir de una lista de strings
    {
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            if (itemNames[i] != "")
            {
                InventorySlot slot = inventorySlots[i];
                slot.item = Item.GetItem(itemNames[i], itemList); // Obtiene el item a partir de su nombre
                slot.UpdateSlotUI();
            }
        }
    }

    public void ResetInventory()
    {
        foreach (InventorySlot slot in inventorySlots)
        {
            slot.item = null;
            slot.UpdateSlotUI();
        }
    }
}