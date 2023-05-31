using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InternDialogueTrigger : MonoBehaviour
{
    [Header("Visual Cue")] //icono para hablara con el npc
    [SerializeField] private GameObject visualCue;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    //public bool activateInternDialogue;
    public bool playerInTrigger;

    private void Awake()
    {
        //activateInternDialogue = false;
        playerInTrigger = false;
        visualCue.SetActive(false);
    }

    private void Update()
    {
        Debug.Log(playerInTrigger);
        if (playerInTrigger && !InternDialogue.Instance().dialogueIsPlaying)
        {
            visualCue.SetActive(true);
            if (Inventory.Instance.hasGuideonsItem)
            {
                //activateInternDialogue = true;
                InternDialogue.Instance().EnterDialogueMode(inkJSON);
            }
        }
        else
        {
            visualCue.SetActive(false);
        }

        //if (activateInternDialogue && !InternDialogue.Instance().dialogueIsPlaying)
        //{
        //}
    }

    private void OnDisable()
    {
        visualCue.SetActive(true);
        if (Inventory.Instance.hasGuideonsItem)
        {
            //activateInternDialogue = true;
            InternDialogue.Instance().EnterDialogueMode(inkJSON);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();
        if (player != null)
        {
            playerInTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Player player = other.GetComponent<Player>();
        if (player != null)
        {
            playerInTrigger = false;
        }
    }
}