using SaveLoadSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSaveData : MonoBehaviour
{
    public static PlayerData playerData = new PlayerData();

    public Player player;
    public static bool continueGame;
    public static bool itemPickedUp;
    //public float timeToSave = 0f;

    private void Start()
    {
        if (continueGame)
        {
            LoadPlayer();
        }
    }

    private void Update()
    {
        //timeToSave += Time.deltaTime; 

        if (Input.GetKeyDown(KeyCode.G))
        {
            SavePlayer();
            //timeToSave = 0f;
        }
    }

    public void SavePlayer()
    {
        SaveGameManager.currentSaveData.playerPosition = player.transform.position;
        SaveGameManager.currentSaveData.playerRotation = player.transform.rotation;
        SaveGameManager.currentSaveData.itemsList = new List<ItemData>();

        Inventory i = FindObjectOfType<Inventory>();
        if (i)
        {
            SaveGameManager.currentSaveData.itemsList.Clear();
            foreach (InventorySlot slot in i.inventorySlots)
            {
                ItemData iData = new ItemData();
                iData.icon = slot.itemIcon;
                iData.name = slot.itemName;
                iData.count = slot.itemCount;
                iData.ID = slot.itemID;

                SaveGameManager.currentSaveData.itemsList.Add(iData);
            }
        }

        SaveGameManager.Save();
        Debug.Log("SAVE");
    }

    public void LoadPlayer()
    {
        Debug.Log("LOAD");

        SaveGameManager.Load();
        playerData = SaveGameManager.currentSaveData;
        player.transform.position = playerData.playerPosition;
        player.transform.rotation = playerData.playerRotation;

        Inventory i = FindObjectOfType<Inventory>();
        if (i)
        {
            foreach (ItemData itemD in playerData.itemsList)
            {
                InventorySlot invSlot = new InventorySlot(itemD);
                i.inventorySlots.Add(invSlot);
            }

            i.UpdateInventory();
        }

        //ItemManager itemManager = FindObjectOfType<ItemManager>();

        //foreach (ItemData item in itemManager.availableItems)
        //{
        //    if (!item.collected)
        //    {
        //        Instantiate(item.prefab, item.spawnPosition, item.spawnRotation);
        //    }
        //}

        //public bool collected;

    }

    [System.Serializable]
    public struct PlayerData
    {
        public Vector3 playerPosition;
        public Quaternion playerRotation;
        public List<ItemData> itemsList;
        //public List<DecisionData> decisions;

        public PlayerData(Vector3 position, Quaternion rotation, ItemData items)
        {
            playerPosition = new Vector3(0, 0, 0);
            playerRotation = new Quaternion(0, 0, 0, 0);
            itemsList = new List<ItemData>();
            //decisions = new List<DecisionData>();
        }
    }
}