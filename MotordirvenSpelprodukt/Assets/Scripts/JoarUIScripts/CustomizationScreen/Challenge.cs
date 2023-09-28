using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Challenge : MonoBehaviour
{
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private int _requirement;
    private bool _isCompleted;

    public event Action<int> OnChallengeCompleted;

    public Challenge(string name, string description, int requirement)
    {
        _name = name;
        _description = description;
        _requirement = requirement;
        _isCompleted = false;
    }
}
