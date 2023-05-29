using Ink.Runtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class InternDialogue : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private float typingSpeed = 0.04f;

    [Header("Load Globals JSON")]
    [SerializeField] private TextAsset loadGlobalsJSON;

    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;

    [SerializeField] private TextMeshProUGUI dialogueText; //using TMPro;

    [SerializeField] private GameObject continueIcon;

    private Story currentStory; //using Ink.Runtime;

    public bool dialogueIsPlaying { get; private set; }//read only

    public bool canContinueToNextLine = false;

    private Coroutine displayTextCoroutine;

    private DialogueVariables dialogueVar;

    private static InternDialogue instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one Dialogue Manager in the scene");
        }
        instance = this;

        dialogueVar = new DialogueVariables(loadGlobalsJSON);
    }
    public static InternDialogue Instance()
    {
        return instance;
    }

    private void Start()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);       
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

    public void EnterDialogueMode(TextAsset inkJSON)//coge el json con los dialogos
    {
        currentStory = new Story(inkJSON.text);//Crea la nueva historia, se inicializa con la info del json
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);

        dialogueVar.StartListening(currentStory);

        ContinueStory();
    }

    IEnumerator ExitDialogueMode()//Es una corrutina porque comparte el imput con otras funciones, así hay un pequeño espacio de tiempo y no se solapan
    {
        yield return new WaitForSeconds(0.2f);

        dialogueVar.StopListening(currentStory);
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
            //Si hay elección el dialogo activo, display 
        }
        else
        {
            StartCoroutine(ExitDialogueMode());
        }
    }

    IEnumerator DisplayText(string line)
    {
        //vacia el texto del dialogo
        dialogueText.text = "";
        canContinueToNextLine = false;
        continueIcon.SetActive(false);

        //escribe letra por letra
        foreach (char letter in line.ToCharArray())
        {
            //if (Input.GetKey(KeyCode.Space))
            //{
            //    dialogueText.text = line;
            //    break;//para romper el loop si el player no quiere esperar a que termine 
            //}

            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        canContinueToNextLine = true;
        continueIcon.SetActive(true);
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

    public void OnApplicationQuit()
    {
        if (dialogueVar != null)//check :)
        {
            dialogueVar.SaveVariables();
        }
    }
}