using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCStone : MonoBehaviour
{
    [SerializeField] private Color defaultColor = Color.white;
    [SerializeField] private Color redColor = Color.red;
    private MeshRenderer meshRenderer;
    void Start()
    {
        meshRenderer = GetComponentInChildren<MeshRenderer>();
    }

    void Update()
    {
        string stoneCount = ((Ink.Runtime.StringValue)DialogueManager.GetInstance().GetVariableState("1")).value;

        switch (stoneCount)
        {
            case "1":
                meshRenderer.material.color = defaultColor;
                //do something
                break;
            case "0":
                meshRenderer.material.color = redColor;
                //do something
                break;
            default:
                Debug.LogWarning("stone count not handled by switch stament: " + stoneCount);
                break;
        }
    }
}
