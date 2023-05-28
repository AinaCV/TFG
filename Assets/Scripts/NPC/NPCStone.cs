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
        GiveItem();
    }

    void GiveItem()
    {
        bool hasGivenItem = bool.Parse(((Ink.Runtime.StringValue)DialogueManager.GetInstance().GetVariableState("hasGivenItem")).value);

        if (hasGivenItem)
        {
            Inventory.Instance.RemoveFromInventory(1); //argumento int = itemID
            meshRenderer.material = mat1;
        }
        else if (!hasGivenItem)
        {
            meshRenderer.material = mat2;
        }
        else
        {
            Debug.LogWarning("stone count not handled by switch stament: " + hasGivenItem);
        }

        //switch (numberOfItems)
        //{
        //    case > 1 //true:
        //      do something
        //        break;
        //    case <= 0 //false:
        //      do something  
        //        break;
        //    default:
        //        Debug.LogWarning("items count not handled by switch stament: " + numberOfItems);
        //        break;
        //}
    }
}