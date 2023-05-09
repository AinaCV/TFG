using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecisionTree : MonoBehaviour
{
    public DecisionTreeNode root;

    private void Start()
    {
        MakeDecisions();
    }

    private void MakeDecisions()
    {
        DecisionTreeNode current = root;
        while (current != null)
        {
            current = current.MakeDecision();
        }
    }
}
