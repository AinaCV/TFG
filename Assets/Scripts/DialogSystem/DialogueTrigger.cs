using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Visual Cue")] //icono para hablara con el npc
    [SerializeField] private GameObject visualCue;
    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    private bool playerInTrigger;

    private void Awake()
    {
        playerInTrigger = false;
        visualCue.SetActive(false);
    }

    private void Update()
    {
        if (playerInTrigger && !DialogueManager.GetInstance().dialogueIsPlaying && !InternDialogue.Instance().dialogueIsPlaying)
        {
            visualCue.SetActive(true); //activamos el icono si está dentro del trigger
            if (Input.GetKeyDown(KeyCode.E))
            {
                DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
            }
        }
        else
        {
            visualCue.SetActive(false);
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
