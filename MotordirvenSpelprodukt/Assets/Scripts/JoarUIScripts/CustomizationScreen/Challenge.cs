using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Challenge : MonoBehaviour
{
    [SerializeField] public string ChallengeName;
    [SerializeField] public string Description;
    [SerializeField] public int Requirement;
    [SerializeField] public int Reward;
    [SerializeField] public TextMeshProUGUI Text; 
    public bool IsCompleted;
    

    public Challenge(string name, string description, int requirement)
    {
        ChallengeName = name;
        Description = description;
        Requirement = requirement;
        IsCompleted = false;

        //ChallengeManager.Instance.ActivateChallenge(this);
    }

    public void ChangeActiveText()
    {
        Text.text = ChallengeName;
    }
}
