using SaveLoadSystem;
using System;
using System.Collections.Generic;

[System.Serializable]
public class DecisionData
{
    public string decisionID;
    public bool isTaken;

    public DecisionData(string id, bool taken)
    {
        decisionID = id;
        isTaken = taken;
    }
}

[System.Serializable]
public class DecisionManagerData
{
    public List<DecisionData> decisions = new List<DecisionData>();

    public void MakeDecision(string decisionID)
    {
        DecisionData decision = decisions.Find(d => d.decisionID == decisionID);
        if (decision != null)
        {
            decision.isTaken = true;
        }
        else
        {
            decisions.Add(new DecisionData(decisionID, true));
        }
    }

    public bool CheckDecision(string decisionID)
    {
        DecisionData decision = decisions.Find(d => d.decisionID == decisionID);
        return decision != null && decision.isTaken;
    }

    public void ResetDecisions()
    {
        foreach (DecisionData decision in decisions)
        {
            decision.isTaken = false;
        }
    }
}

public class DecisionManager
{
    private DecisionManagerData decisionManagerData;

    public DecisionManager(PlayerData playerData)
    {
        decisionManagerData = new DecisionManagerData();
        decisionManagerData.decisions = playerData.decisions;
    }

    public void MakeDecision(string decisionID)
    {
        decisionManagerData.MakeDecision(decisionID);
    }

    public bool CheckDecision(string decisionID)
    {
        return decisionManagerData.CheckDecision(decisionID);
    }

    public void ResetDecisions()
    {
        decisionManagerData.ResetDecisions();
    }
}