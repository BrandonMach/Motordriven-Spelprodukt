using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Challenge : MonoBehaviour
{
    [SerializeField] private string _challengeName;
    [SerializeField] private string _description;
    [SerializeField] private int _requirement;
    [SerializeField] private int _reward;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private GameObject _challengeButton;

    private bool _isCompleted;
    private bool _isActivated;
    public TooltipTrigger trigger;

    public string ChallengeName { get => _challengeName; set => _challengeName = value; }
    public string Description { get => _description; set => _description = value; }
    public int Requirement { get => _requirement; set => _requirement = value; }
    public int Reward { get => _reward; set => _reward = value; }
    public TextMeshProUGUI Text { get => _text; set => _text = value; }
    public bool IsCompleted { get => _isCompleted; set => _isCompleted = value; }
    public bool IsActivated { get => _isActivated; set => _isActivated = value; }
    public GameObject ChallengeButton { get => _challengeButton; set => _challengeButton = value; }

    private void Start()
    {
        trigger = GetComponent<TooltipTrigger>();
        trigger.content = "Reward: " + _reward.ToString();
        trigger.header = _description;
        DontDestroyOnLoad(ChallengeButton);
    }

    private void Update()
    {

    }

    public void UpdateTriggerContent()
    {
        trigger.content = _description;
    }

    public Challenge(string name, string description, int requirement)
    {
        ChallengeName = name;
        Description = description;
        Requirement = requirement;
        IsCompleted = false;
        IsActivated = false;
          
        //ChallengeManager.Instance.ActivateChallenge(this);
    }

    public void ChangeActiveText()
    {
        Text.text = ChallengeName;
    }
}
