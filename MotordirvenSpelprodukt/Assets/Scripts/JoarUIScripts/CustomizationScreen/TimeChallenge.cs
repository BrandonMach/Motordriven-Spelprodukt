using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeChallenge : Challenge
{

    [SerializeField] float _timeForCompletion;

    public TimeChallenge(string name, string description, int requirement, float timeForCompletion) : base(name, description, requirement)
    {
        _timeForCompletion = timeForCompletion;
        ChallengeManager.Instance.AddChallenge(this);
    }

    public float TimeForCompletion { get => _timeForCompletion; set => _timeForCompletion = value; }
}
