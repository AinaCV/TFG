using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCStone : MonoBehaviour
{
    SkinnedMeshRenderer meshRenderer;

    void Start()
    {
        meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
    }

    void Update()
    {
        string stoneCount = ((Ink.Runtime.StringValue)DialogueManager.GetInstance().GetVariableState("hasGivenStone")).value;

        switch (stoneCount)
        {
            case "true":
                meshRenderer.material.color = Color.white;
                //do something
                break;
            case "false":
                meshRenderer.material.color = Color.red;
                //do something
                break;
            default:
                Debug.LogWarning("stone count not handled by switch stament: " + stoneCount);
                break;
        }
    }
}
