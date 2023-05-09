using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]

public class Item : ScriptableObject
{
    new public string name = "New Item"; //Nombre del item
    public Sprite icon = null; //Icono del item
    public bool isDefaultItem = false; //Indica si es un item predeterminado para que no pueda ser eliminado

    public Inventory inventory;
    public string itemName;
    public int itemCount;
    public Sprite itemIcon;
    public virtual void Use() //Uso del item
    {
        Debug.Log("Using " + name);
    }

    public void RemoveFromInventory() //Elimina el item del inventario
    {
        inventory.RemoveItem(this);
    }

    // Constructor
    public Item(string itemName, int itemCount, Sprite itemIcon)
    {
        this.itemName = itemName;
        this.itemCount = itemCount;
        this.itemIcon = itemIcon;
    }

    public static Item GetItem(string itemName, List<Item> itemList)
    {
        // Busca el objeto Item correspondiente al nombre dado en la lista de objetos Item
        for (int i = 0; i < itemList.Count; i++)
        {
            if (itemList[i].itemName == itemName)
            {
                return itemList[i];
            }
        }
        return null;
    }
}