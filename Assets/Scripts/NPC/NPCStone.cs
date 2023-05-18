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
        bool stoneCount = bool.Parse(((Ink.Runtime.StringValue)DialogueManager.GetInstance().GetVariableState("hasGivenStone")).value);

        //switch (stoneCount)
        //{
        //    case true:
        //        meshRenderer.material.color = Color.white;
        //        //do something
        //        break;
        //    case false:
        //        meshRenderer.material.color = Color.red;
        //        //do something
        //        break;
        //    default:
        //        Debug.LogWarning("stone count not handled by switch stament: " + stoneCount);
        //        break;
        //}

        if(stoneCount)
        {
            meshRenderer.material = mat1;
        }
        else if(!stoneCount)
        {
            meshRenderer.material= mat2 ;
        }
        else
        {
            Debug.LogWarning("stone count not handled by switch stament: " + stoneCount);
        }
    }
}
