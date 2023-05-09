using System.Collections.Generic;
using UnityEngine;

namespace SaveLoadSystem
{
    [System.Serializable]
    public class PlayerData
    {
        public List<string> inventory = new List<string>();
        public Vector3 position;
        //public string characterName;
        public List<DecisionData> decisions = new List<DecisionData>();
    }
}

/*Lee los siguientes códigos y dame soluciones para los errores que dan entre códigos: 
1.  
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
2.  
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
3. 
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Player Data")]
    public PlayerData playerData;

    [Header("Inventory")]
    public Inventory inventory;

    [Header("Decision Manager")]
    public DecisionManager decisionManager;

    [Header("Dialog Manager")]
    public DialogueManager dialogueManager;

    [Header("Action Manager")]
    public Action actionManager;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        decisionManager = new DecisionManager();
        actionManager = new ActionManager();
    }

    private void Start()
    {
        LoadGameData();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SaveGameData();
            Application.Quit();
        }
    }

    public void SaveGameData()
    {
        playerData.position = GetPlayerPosition();
        playerData.inventory = inventory.GetInventoryData();
        playerData.decisions = decisionManager.GetDecisionData();

        SaveLoadManager.currentSaveData = playerData;
        SaveLoadManager.Save();
    }

    public void LoadGameData()
    {
        SaveLoadManager.Load();
        inventory.SetInventoryData(playerData.inventory);

        decisionManager.SetDecisionData(playerData.decisions);

        if (playerData.position != Vector3.zero) //Modificar la comparación
        {
            SetPlayerPosition(playerData.position);
        }
    }

    private Vector3 GetPlayerPosition()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            return player.transform.position;
        }
        return Vector3.zero;
    }

    private void SetPlayerPosition(Vector3 position)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.transform.position = position;
        }
    }

    public void StartNewGame()
    {
        playerData = new PlayerData(); //Crea nuevos datos del jugador
        inventory.ResetInventory(); //Reinicia el inventario
        decisionManager.ResetDecisions(); //Reinicia las decisiones tomadas
        SaveLoadManager.currentSaveData = playerData; //Establece los nuevos datos del jugador en la clase SaveLoadManager
        SaveLoadManager.Save(); //Guarda los nuevos datos del jugador en el archivo de guardado
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //reinicia la escena actual
    }

    public void EndGame()
    {
        SaveGameData();
        Application.Quit();
    }

    public void ShowDialogue(string dialogID)
    {
        dialogueManager.ShowDialogue(dialogID);
    }

    public void StartAction(string actionID)
    {
        Action action = actionManager.GetAction(actionID);
        if (action != null && CanAffordAction(action))
        {
            action.startAction();
        }
    }

    private bool CanAffordAction(Action action)
    {
        if (action.cost == null)
        {
            return true;
        }
        return inventory.HasResources(action.cost);
    }

} 
4.  
[System.Serializable]
public class DecisionData
{
    public string decisionID;
    public bool isTaken;

    public DecisionData(string id, bool taken)
    {
        decisionID = id;
        isTaken = taken;
    }
}

[System.Serializable]
public class DecisionManagerData
{
    public List<DecisionData> decisions = new List<DecisionData>();

    public void MakeDecision(string decisionID)
    {
        DecisionData decision = decisions.Find(d => d.decisionID == decisionID);
        if (decision != null)
        {
            decision.isTaken = true;
        }
        else
        {
            decisions.Add(new DecisionData(decisionID, true));
        }
    }

    public bool CheckDecision(string decisionID)
    {
        DecisionData decision = decisions.Find(d => d.decisionID == decisionID);
        return decision != null && decision.isTaken;
    }

    public void ResetDecisions()
    {
        foreach (DecisionData decision in decisions)
        {
            decision.isTaken = false;
        }
    }
}
public class DecisionManager
{
    private DecisionManagerData decisionManagerData;

    public DecisionManager(PlayerData playerData)
    {
        decisionManagerData = new DecisionManagerData();
        decisionManagerData.decisions = playerData.decisions;
    }

    public void MakeDecision(string decisionID)
    {
        decisionManagerData.MakeDecision(decisionID);
    }

    public bool CheckDecision(string decisionID)
    {
        return decisionManagerData.CheckDecision(decisionID);
    }

    public void ResetDecisions()
    {
        decisionManagerData.ResetDecisions();
    }
} 
5. 
public class DialogueManager : MonoBehaviour
{
    public GameObject dialogueBox;
    public TextMeshProUGUI characterNameText;
    public TextMeshProUGUI dialogueText;
    public float textDelay = 0.1f;
    public float dialogDelay = 1f;
    public GameObject dialogPanel;
    public TMP_InputField dialogTextInputField;

    private Queue<DialogData> dialogQueue = new Queue<DialogData>();
    private Coroutine currentCoroutine;
    private Dictionary<string, DialogData> dialogDictionary;

    // Lista de diálogos
    public List<DialogData> dialogList;

    private void Awake()
    {
        // Crear el diccionario dialogDictionary y poblarlo con los datos de diálogo
        dialogDictionary = new Dictionary<string, DialogData>();
        foreach (DialogData dialogData in dialogList)
        {
            dialogDictionary.Add(dialogData.dialogID, dialogData);
        }
        dialogueBox.SetActive(false);
    }

    public void StartDialogue(string dialogue)
    {
        dialogueBox.SetActive(true);
        dialogueText.text = dialogue;
    }

    public void EndDialogue()
    {
        dialogueBox.SetActive(false);
    }

    public void AddDialogue(string characterName, string dialogText)
    {
        DialogData dialog = new DialogData();
        dialog.characterName = characterName;
        dialog.dialogText = dialogText;
        dialogQueue.Enqueue(dialog);
    }

    public void ShowNextDialogue()
    {
        if (dialogQueue.Count == 0)
        {
            return;
        }

        DialogData dialog = dialogQueue.Dequeue();
        characterNameText.text = dialog.characterName;
        dialogueText.text = "";

        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }

        currentCoroutine = StartCoroutine(TypeText(dialog.dialogText));
    }

    IEnumerator TypeText(string text)
    {
        foreach (char c in text)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(textDelay);
        }

        yield return new WaitForSeconds(dialogDelay);

        ShowNextDialogue();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && dialogueBox.activeSelf)
        {
            HideDialogue();
        }
    }

    public void ShowDialogue(string dialogID)
    {
        dialogueBox.SetActive(true);
        dialogue = DialogueDatabase.GetDialogueByID(dialogID);
        dialogueText.text = dialogue;
    }

    public void HideDialogue()
    {
        dialogueBox.SetActive(false);
        dialogue = "";
        dialogueText.text = "";
    }
6.  
public class ForestAction : Action
{
    private bool hasMetNpc = false;
    private bool hasGivenItem = false;
    public DialogueManager dialogueManager;

    public override void LateUpdate()
    {
        if (!activated)
            return;

        if (!hasMetNpc)
        {
            // Encuentra al NPC
            NPC npc = FindObjectOfType<NPC>();
            if (npc != null)
            {
                hasMetNpc = true;
                npc.StartDialogue();
            }
        }
        else if (!hasGivenItem)
        {
            // Si el jugador tiene al menos un objeto en su inventario, 
            // muestra un mensaje para pedir uno y espera la respuesta del jugador.
            InventorySlot[] inventorySlots = FindObjectOfType<Inventory>().slots;
            bool hasItem = false;
            foreach (InventorySlot slot in inventorySlots)
            {
                if (slot.item != null)
                {
                    hasItem = true;
                    break;
                }
            }

            if (hasItem)
            {
                // Muestra el mensaje y espera la respuesta del jugador
                dialogueManager.ShowDialogue("NPC: Hola, ¿tendrías un objeto para prestarme? Tengo algo que ofrecerte a cambio.",
                                                       new List<string>() { "Sí", "No" },
                                                       new List<DialogueAction>() { GiveItemToNpc, IgnoreNpc });
            }
            else
            {
                // Si el jugador no tiene ningún objeto, informa al jugador y termina el diálogo.
                dialogueManager.ShowDialogue("NPC: Parece que no tienes nada que prestarme... Vuelve cuando tengas algo que ofrecer.",
                                                       new List<string>() { "Salir" },
                                                       new List<DialogueAction>() { EndDialogue });
                hasMetNpc = false;
            }
        }
        else
        {
            // Muestra la recompensa y termina el diálogo.
            dialogueManager.ShowDialogue("NPC: ¡Muchas gracias por tu ayuda! Aquí tienes tu recompensa.",
                                                   new List<string>() { "Aceptar" },
                                                   new List<DialogueAction>() { EndDialogue });
            // Otorga la recompensa al jugador
            //FindObjectOfType<Player>().AddExperiencePoints(100);
            hasMetNpc = false;
            hasGivenItem = false;
        }
    }

    private void GiveItemToNpc()
    {
        // Busca el primer objeto que encuentre en el inventario y se lo da al NPC
        InventorySlot[] inventorySlots = FindObjectOfType<Inventory>().slots;
        foreach (InventorySlot slot in inventorySlots)
        {
            if (slot.item != null)
            {
                slot.item = null;
                hasGivenItem = true;
                break;
            }
        }
    }

    private void IgnoreNpc()
    {
        // Si el jugador ignora al NPC, termina el diálogo.
        dialogueManager.ShowDialogue("NPC: Vaya... Bueno, gracias de todas formas.",
                                               new List<string>() { "Salir" },
                                               new List<DialogueAction>() { EndDialogue });
        hasMetNpc = false;
    }

    private void EndDialogue()
    {
        // Termina el diálogo y desactiva esta acción
        activated = false;
        dialogueManager.HideDialogue();
    }
} 
7.  
public class Action : DecisionTreeNode
{
    public bool activated = false;
    private List<Action> _actions;
    private string ID;

    public override DecisionTreeNode MakeDecision() //override es necesario para ampliar o modificar la implementación abstracta o virtual de un método, propiedad, indexador o evento heredado.
    {
        return this;
    }
    public virtual void LateUpdate()
    {
        if (!activated)
            return;
        // Implememntar los comportamientos aquí :D
    }

    public Action(List<Action> actions)
    {
        _actions = actions;
    }

    public Action GetAction(string actionID)
    {
        foreach (Action action in _actions)
        {
            if (action.ID == actionID)
            {
                return action;
            }
        }
        return null; // si no se encontró una acción con el ID dado, devuelve null
    }
} 
8. 
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
}*/