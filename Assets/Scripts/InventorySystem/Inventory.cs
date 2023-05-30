using SaveLoadSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] public List<InventorySlot> inventorySlots;

    public GameObject inventorySlotPrefab;
    public Transform inventoryContent;
    public GameObject inventoryPanel;
    //public GameObject healButton;
    public bool inventoryOnScreen;
    public bool hasGuideonsItem = false;

    public static Inventory Instance { get; private set; }

    private void Awake()
    {
        Instance = this; //el inventario se inicializa en un awake
        inventorySlots = new List<InventorySlot>();
        UpdateInventory(); //las cosas acceden al inventario despúes de inicializarse
    }

    private void Update()
    {
        if (DialogueManager.GetInstance().dialogueIsPlaying)
        {
            inventoryOnScreen = false;
            return;
        }

        if (inventoryOnScreen)
        {
            inventoryPanel.SetActive(true);
            //AudioManager.Instance.PlayAudio(clip.name);
        }

        if (!inventoryOnScreen)
        {
            inventoryPanel.SetActive(false);
            //AudioManager.Instance.PlayAudio(clip.name);
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            ChangeInventoryState();
        }

    }

    public bool AddToInventory(ItemData itemToAdd, int amountToAdd)
    {
        int i = 0;
        while (i < inventorySlots.Count)
        {
            if (inventorySlots[i].itemName == itemToAdd.name)
            {
                inventorySlots[i].itemCount = (inventorySlots[i].itemCount + amountToAdd);
                //(int.Parse(inventorySlots[i].itemCount.text) + amountToAdd).ToString();
                return true;
            }
            i++;
        }
        inventorySlots.Add(new InventorySlot(itemToAdd));
        return true;
    }
    public void RemoveFromInventory(int ID)
    {
        //int i = 0;

        //while (i < inventorySlots.Count)
        //{
        //    if (inventorySlots[i].itemName == itemToRemove)
        //    {
        //        inventorySlots[i].itemCount = (inventorySlots[i].itemCount - 1);
        //        UpdateInventory();
        //        break;
        //    }
        //}
        //if (i <= 0)
        //{
        //    inventorySlots.RemoveAt(i);
        //}
        //i++;

        for (int i = 0; i < inventorySlots.Count; i++)
        {
            if (inventorySlots[i].itemID == ID)
            {
                inventorySlots[i].itemCount = (inventorySlots[i].itemCount - 1);
                if (inventorySlots[i].itemCount <= 0)
                {
                    inventorySlots.Remove(inventorySlots[i]);
                }
                UpdateInventory();
            }
        }
    }

    public void ChangeInventoryState()
    {
        inventoryOnScreen = !inventoryOnScreen; //comprueba en que estado se encuentra el bool y lo cambia al pulsar la tecla
    }

    public void UpdateInventory()
    {
        //Limpia
        for (int i = 0; i < inventoryContent.childCount; i++)
        {
            Destroy(inventoryContent.GetChild(i).gameObject);
        }
        //Añade
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            GameObject slotObject = Instantiate(inventorySlotPrefab, inventoryContent);
            InventorySlotRef refer = slotObject.GetComponent<InventorySlotRef>();

            refer.image.sprite = inventorySlots[i].itemIcon;
            refer.itemNameText.text = inventorySlots[i].itemName;
            refer.itemCountText.text = inventorySlots[i].itemCount.ToString();
        }
    }

    public void CheckID()
    {
        for (int i = 0; i < inventorySlots.Count; i++) //recorre cada elemento de la lista
        {
            if (inventorySlots[i].itemID == 1) //si tenemos el objeto de gieon en el inventario
            {
                hasGuideonsItem = true;
            }
            else
            {
                hasGuideonsItem = false;
            }
        }
    }
}