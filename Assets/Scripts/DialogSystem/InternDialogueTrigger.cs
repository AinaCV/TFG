using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InternDialogueTrigger : MonoBehaviour
{
    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    public bool activateInternDialogue;

    private void Awake()
    {
        activateInternDialogue = false;
    }

    private void Update()
    {
        if (activateInternDialogue && !InternDialogue.Instance().dialogueIsPlaying)
        {
            InternDialogue.Instance().EnterDialogueMode(inkJSON);
        }

        if (Inventory.Instance.hasPendant)
        {
            activateInternDialogue = true;
        }
    }
}