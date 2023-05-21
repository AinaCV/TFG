using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InternDialogueTrigger : MonoBehaviour
{
    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    private bool playerInInternDialogue;

    private void Awake()
    {
        playerInInternDialogue = false;
    }

    private void Update()
    {
        if (playerInInternDialogue && !InternDialogue.Instance().dialogueIsPlaying)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                InternDialogue.Instance().EnterDialogueMode(inkJSON);
            }
        }
            //playerInInternDialogue = true;
            //playerInInternDialogue = false;
    }
}