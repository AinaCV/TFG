using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.EventSystems;

public class DialogueManager : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private float typingSpeed = 0.04f;

    [Header("Load Globals JSON")]
    [SerializeField] private TextAsset loadGlobalsJSON;

    [SerializeField] private int itemCount;

    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;

    [SerializeField] private TextMeshProUGUI dialogueText; //using TMPro;

    [SerializeField] private GameObject continueIcon;

    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices; //array de elecciones

    private TextMeshProUGUI[] choicesText; //array de cada texto para cada elecci�n

    private Story currentStory; //using Ink.Runtime;

    Inventory inventory;
    public bool dialogueIsPlaying { get; private set; }//read only

    public bool canContinueToNextLine = false;

    public bool hasGivenItem = false;

    private Coroutine displayTextCoroutine;

    private DialogueVariables dialogueVar;

    private static DialogueManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one Dialogue Manager in the scene");
        }
        instance = this;

        dialogueVar = new DialogueVariables(loadGlobalsJSON);
    }
    public static DialogueManager GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        //playerAnim = Player.Instance.GetComponent<AnimationController>();
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);

        //inicializa choicesText
        choicesText = new TextMeshProUGUI[choices.Length]; //iguala el array de choicesText con la cantidad del array de choices
        int index = 0;
        foreach (GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++; //incrementa el indice despues de cada loop
        }

        inventory = FindObjectOfType<Inventory>();
    }

    private void Update()
    {
        if (!dialogueIsPlaying)
        {
            return;
        }
        if (currentStory.currentChoices.Count == 0 && canContinueToNextLine && Input.GetMouseButtonDown(0))
        {
            ContinueStory();
        }
    }

    void ChangeItemVar(Story story)
    {
        //hasGivenItem = ((BoolValue)GetInstance().GetVariableState("hasGivenItem")).value;
        itemCount = ((IntValue)GetInstance().GetVariableState("itemCount")).value;

        //if (hasGivenItem)
        //{
        //    itemCount--;
        //    Inventory.Instance.RemoveFromInventory(1); //argumento int = itemID
        //}
        //else if (!hasGivenItem)
        //{
        //    Debug.Log("False");
        //}
        //else
        //{
        //    Debug.LogWarning("stone count not handled by switch stament: " + hasGivenItem);
        //}

        if (Inventory.Instance.inventorySlots.Count > 0)
        {
            itemCount = Inventory.Instance.inventorySlots.Count;
        }

        if (itemCount >= 1)
        {
            story.variablesState["itemCount"] = itemCount;
        }
        else if (itemCount <= 0)
        {
            story.variablesState["itemCount"] = 0;
        }
    }

    public void EnterDialogueMode(TextAsset inkJSON)//coge el json con los dialogos
    {
        currentStory = new Story(inkJSON.text);//Crea la nueva historia, se inicializa con la info del json
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);

        dialogueVar.StartListening(currentStory);
        ChangeItemVar(currentStory);
        currentStory.BindExternalFunction("removeFromInventory", (int removeItemID) =>
        {
            Inventory.Instance.RemoveFromInventory(2);
        });
        currentStory.BindExternalFunction("hasGivenItem", (bool switchBool) =>
        {
            hasGivenItem = true;
        });
        ContinueStory();
    }

    IEnumerator ExitDialogueMode()//Es una corrutina porque comparte el imput con otras funciones, as� hay un peque�o espacio de tiempo y no se solapan
    {
        yield return new WaitForSeconds(0.2f);

        dialogueVar.StopListening(currentStory);
        currentStory.UnbindExternalFunction("hasGivenItem");
        currentStory.UnbindExternalFunction("removeFromInventory");

        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = ""; // dejamos el texto en un string vacia por si acaso
    }

    void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            //set the text
            //dialogueText.text = currentStory.Continue();
            if (displayTextCoroutine != null) //si ya esta en marcha para la corrutina para que no se vuelva loco el dialogo
            {
                StopCoroutine(displayTextCoroutine);
            }
            displayTextCoroutine = StartCoroutine(DisplayText(currentStory.Continue()));//siguiente linea de dialogo
            //Si hay elecci�n el dialogo activo, display 
        }
        else
        {
            StartCoroutine(ExitDialogueMode());
        }
    }

    void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices; //Devuele una lista de choices si la hay

        if (currentChoices.Count > choices.Length) //checkea la cantidad de decisiones y da error si sobrepasa el array
        {
            Debug.LogError("More coices than UI can support. Number of choices given:" + currentChoices.Count);
        }

        int index = 0;
        //buscar e inicializar los gobj de choices en UI, para las lineas de dialogo activas
        foreach (Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text; //choiceText es el UI text y lo igualamos al texto de las decisiones
            index++;
        }
        //Revisa las elecciones que quedan por hacer en UI y desactivalas
        for (int i = index; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }

        StartCoroutine(SelectFirtsChoise());
    }

    IEnumerator DisplayText(string line)
    {
        //vacia el texto del dialogo
        dialogueText.text = "";
        canContinueToNextLine = false;
        continueIcon.SetActive(false);
        HideChoices();

        //escribe letra por letra
        foreach (char letter in line.ToCharArray())
        {
            if (Input.GetKey(KeyCode.Space))
            {
                dialogueText.text = line;
                break;//para romper el loop si el player no quiere esperar a que termine 
            }

            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        canContinueToNextLine = true;
        continueIcon.SetActive(true);
        DisplayChoices();
    }

    void HideChoices()
    {
        foreach (GameObject choiceButton in choices)
        {
            choiceButton.SetActive(false);
        }
    }

    private IEnumerator SelectFirtsChoise()    //Buscar alternativas, apa�o pochillo para poder seleccionar las elecciones en los dialogos
    {
        EventSystem.current.SetSelectedGameObject(null);//Limpiamos event system y esperamos un frame
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);//seleccionamos una decision 
    }

    public void MakeChoice(int choiceIndex)//Para los botones
    {
        if (canContinueToNextLine && !Input.GetKey(KeyCode.Space)) //Apa�o para que no seleccione una decisi�n al darle al espacio
        {
            currentStory.ChooseChoiceIndex(choiceIndex);
            ContinueStory();
        }
    }

    public Ink.Runtime.Object GetVariableState(string varName)
    {
        Ink.Runtime.Object varValue = null;
        dialogueVar.var.TryGetValue(varName, out varValue); //ref diccionary
        if (varValue == null)
        {
            Debug.LogWarning("Ink Variable null:" + varName);
        }
        return varValue; //return si existe
    }

    //public void OnApplicationQuit()
    //{
    //    if (dialogueVar != null)//check :)
    //    {
    //        dialogueVar.SaveVariables();
    //    }
    //}
}