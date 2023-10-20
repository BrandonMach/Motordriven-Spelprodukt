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
    [SerializeField] private TextMeshProUGUI _text; 
    public bool IsCompleted;
    

    public Challenge(string name, string description, int requirement)
    {
        ChallengeName = name;
        Description = description;
        Requirement = requirement;
        IsCompleted = false;

        ChallengeManager.Instance.AddChallenge(this);
    }

    public void ChangeActiveText()
    {
        _text.text = ChallengeName;
    }
}
