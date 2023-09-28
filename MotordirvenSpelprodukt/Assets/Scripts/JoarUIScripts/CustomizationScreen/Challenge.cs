using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Challenge : MonoBehaviour
{
    [SerializeField] public string ChallengeName;
    [SerializeField] public string Description;
    [SerializeField] private int _requirement;
    private bool _isCompleted;

    public event Action<int> OnChallengeCompleted;

    public Challenge(string name, string description, int requirement)
    {
        ChallengeName = name;
        Description = description;
        _requirement = requirement;
        _isCompleted = false;
    }
}
