using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public ItemData itemData;
    public bool keyPressed = false;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            keyPressed = true;
        }
        else
        {
            keyPressed = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (keyPressed)
        {
            PlayerSaveData.itemPickedUp = true;
            Inventory.Instance.AddToInventory(itemData, itemData.count);
            Inventory.Instance.UpdateInventory();
            Inventory.Instance.CheckID();
            this.gameObject.SetActive(false);
            keyPressed = false;
        }
    }
}