using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Action", menuName = "ScriptableObjects/Actions/Action")]
public class Action : DecisionTreeNode
{
    public bool activated = false;
    private List<Action> _actions;
    private string _ID;

    public override DecisionTreeNode MakeDecision()
    {
        return this;
    }

    public virtual void LateUpdate()
    {
        if (!activated)
            return;
        // Implementa los comportamientos aquí :D
    }

    public Action(List<Action> actions, string ID)
    {
        _actions = actions;
        _ID = ID;
    }

    public Action GetAction(string actionID)
    {
        foreach (Action action in _actions)
        {
            if (action._ID == actionID)
            {
                return action;
            }
        }
        return null;
    }
}