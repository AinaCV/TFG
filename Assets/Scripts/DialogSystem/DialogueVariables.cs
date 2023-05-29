using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class DialogueVariables
{
    public Dictionary<string, Ink.Runtime.Object> varDictionary { get; private set; }//public para referrenciar el diccionario y cambiar el curso de la historia según las decisiones
    private Story globalVariablesStory;//privado para refernciarlo en el guardado y cargado de datos
    private const string saveVarKey = "Ink_Var";
    public Inventory i;
    public DialogueVariables(TextAsset loadGlobalsJSON)
    {
        globalVariablesStory = new Story(loadGlobalsJSON.text); //Crea la historia

        if (PlayerPrefs.HasKey(saveVarKey))
        {
            string jsonState = PlayerPrefs.GetString(saveVarKey);
            globalVariablesStory.state.LoadJson(jsonState);
        }

        //inicializa el diccionario
        varDictionary = new Dictionary<string, Ink.Runtime.Object>();
        foreach (string name in globalVariablesStory.variablesState)
        {
            Ink.Runtime.Object value = globalVariablesStory.variablesState.GetVariableWithName(name);
            varDictionary.Add(name, value);
            Debug.Log("Initialized global dialogue variable:" + name + "=" + value);
        }
    }

    public void SaveVariables()
    {
        if (globalVariablesStory != null)
        {
            //Carga los datos de las variables de la historia
            VariablesToStory(globalVariablesStory);
            PlayerPrefs.SetString(saveVarKey, globalVariablesStory.state.ToJson());//Cambiar esto por un save load de verdad, sin usar playerprefs
        }
    }

    public void StartListening(Story story)
    {
        VariablesToStory(story);//importante que se carguen las variables antes del observador 
        story.variablesState.variableChangedEvent += VarChanged;
    }
    public void StopListening(Story story)
    {
        story.variablesState.variableChangedEvent -= VarChanged;
    }

    void VarChanged(string name, Ink.Runtime.Object value)
    {
        if (varDictionary.ContainsKey(name))//contains key para comprobar si ya está en el diccionario
        {
            varDictionary.Remove(name);
            varDictionary.Add(name, value);
        }
    }

    void VariablesToStory(Story story)//carga las variables en los archivos ink
    {
        foreach (KeyValuePair<string, Ink.Runtime.Object> var in varDictionary)
        {
            story.variablesState.SetGlobal(var.Key, var.Value);
        }
    }
}