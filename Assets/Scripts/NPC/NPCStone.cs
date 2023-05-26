using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCStone : MonoBehaviour
{
    SkinnedMeshRenderer meshRenderer;
    public Material mat1, mat2;

    void Start()
    {
        meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
    }

    void Update()
    {
        bool hasGivenItem = bool.Parse(((Ink.Runtime.StringValue)DialogueManager.GetInstance().GetVariableState("hasGivenItem")).value);

        if(!hasGivenItem)
        {
            meshRenderer.material= mat2;
        }
        else if(hasGivenItem)
        {
            Inventory.Instance.RemoveFromInventory(1);
            meshRenderer.material = mat1;
        }
        else
        {
            Debug.LogWarning("stone count not handled by switch stament: " + hasGivenItem);
        }
    }
}