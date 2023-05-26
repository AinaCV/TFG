using SaveLoadSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //public GameObject menuReference;
    public static GameManager instance; //static--> para los demás componentes puedan acceder al GM
    public bool hasItemsInInventory;
    void Awake()
    {
        if (instance == null)
        {
            instance = this; //this = new GM que unity ha hecho por nosotros
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); //si hay más de un GM se queda el primero que se instancia, el otro se elimina y se queda entre escenas
        }
    }

    private void Update()
    {

        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    if (!menuReference.activeInHierarchy)
        //    {
        //        menuReference.SetActive(true);
        //    }
        //    else if (menuReference.activeInHierarchy)
        //    {
        //        menuReference.SetActive(false);
        //    }
        //}

        Inventory i = FindObjectOfType<Inventory>();
        if (i.inventorySlots.Count > 0)
        {
            hasItemsInInventory = true;
        }
        else if (i.inventorySlots.Count <= 0)
        {
            hasItemsInInventory = false;
        }

        if (hasItemsInInventory)
        {
            int numberOfItems = ((Ink.Runtime.IntValue)DialogueManager.GetInstance().GetVariableState("numberOfItems")).value;

            switch (numberOfItems)
            {
                case > 1:
                    foreach (InventorySlot slot in i.inventorySlots)
                    {
                        numberOfItems++;
                    }
                    break;
                case <= 0:
                    foreach (InventorySlot slot in i.inventorySlots)
                    {
                        numberOfItems--;
                    }
                    break;
                default:
                    Debug.LogWarning("items count not handled by switch stament: " + numberOfItems);
                    break;
            }
        }
    }
}