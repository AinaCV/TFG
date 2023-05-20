using System.Collections;
using System.Collections.Generic;
using TMPro;
using Ink.Runtime;
using UnityEngine;

public class TextManager : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private float typeSpeed = 0.04f;

    [Header("Dialogue UI")]
    [SerializeField] private GameObject introPanel;

    [SerializeField] private TextMeshProUGUI introText; //using TMPro;

    [SerializeField] private TextAsset inkJSONIntro;

    private Story introStory; //using Ink.Runtime;

    public bool dialogueIsPlaying { get; private set; }//read only

    public bool canContinueToNextLine = false;

    private Coroutine displayIntroCoroutine;


    private void Start()
    {
        dialogueIsPlaying = false;
        introPanel.SetActive(false);
        StartCoroutine(StartIntro());
    }

    private void Update()
    {
        if (!dialogueIsPlaying)
        {
            return;
        }
        if (canContinueToNextLine)
        {
            ContinueStory();
        }
    }

    public void EnterDialogueMode(TextAsset inkJSON)//coge el json con los dialogos
    {
        introStory = new Story(inkJSON.text);//se inicializa con la info del json
        dialogueIsPlaying = true;
        introPanel.SetActive(true);

        ContinueStory();
    }

    void ExitDialogueMode()
    {
        dialogueIsPlaying = false;
        introPanel.SetActive(false);
        introText.text = ""; // dejamos el texto en un string vacia por si acaso
    }

    void ContinueStory()
    {
        if (introStory.canContinue)
        {
            //set the text
            //introText.text = introStory.Continue();
            if (displayIntroCoroutine != null) //si ya esta en marcha para la corrutina para que no se vuelva loco el dialogo
            {
                StopCoroutine(displayIntroCoroutine);
            }
            displayIntroCoroutine = StartCoroutine(DisplayText(introStory.Continue()));//siguiente linea de dialogo
            //Si hay elección el dialogo activo, display 
        }
        else
        {
            ExitDialogueMode();
        }
    }


    IEnumerator DisplayText(string line)
    {
        //vacia el texto del dialogo
        introText.text = "";
        canContinueToNextLine = false;
        //escribe letra por letra
        foreach (char letter in line.ToCharArray())
        {
            introText.text += letter;
            yield return new WaitForSeconds(typeSpeed);
        }
        canContinueToNextLine = true;
    }

    IEnumerator StartIntro()
    {
        yield return new WaitForSeconds(3f);
        EnterDialogueMode(inkJSONIntro);
    }
}