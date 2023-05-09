using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecisionTreeNode : ScriptableObject
{
    public virtual DecisionTreeNode MakeDecision() { return null; } //al contrario de abstract, virtual tiene la opción se sobreescribir las clases que heredan de este metodo
    protected virtual DecisionTreeNode GetBranch() { return null; }
}