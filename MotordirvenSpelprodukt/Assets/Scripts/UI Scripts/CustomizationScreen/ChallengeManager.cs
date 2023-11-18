using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
//using static UnityEditor.Experimental.GraphView.GraphView;

public class ChallengeManager : MonoBehaviour
{
    // Is only to be instantiated in the CustomizationScreen, but can be reachable throughout the project with ChallengeManager.Instance;
    #region Singleton

    private static ChallengeManager _instance;
    public static ChallengeManager Instance { get => _instance; set => _instance = value; }
 

    private void Awake()
    {
        if (_instance != null)
        {
            Debug.LogWarning("More than one instance of ChallengeManager found");
            return;
        }
        _instance = this;

        DontDestroyOnLoad(gameObject);

        foreach (GameObject item in AvailableChallengeButtons)
        {
            
        }

        ActiveChallenges = new List<Challenge>();
    }
    #endregion

    GameManager _gameManager = GameManager.Instance;

    public List<Challenge> AvailableChallenges = new List<Challenge>();
    public List<Challenge> ActiveChallenges;

    public List<GameObject> AvailableChallengeButtons = new List<GameObject>();

    public event Action<Challenge> OnChallengeCompleted;

    [SerializeField] TextMeshProUGUI _firstActiveChallengeText;
    [SerializeField] TextMeshProUGUI _secondActiveChallengeText;

    private int _activeChallengesCounter;
    private const int _maxActiveChallenges = 2;
    private int _toggleActiveText = 0;

    private void Update()
    {
        foreach (Challenge challenge in AvailableChallenges)
        {
            if (challenge.IsCompleted)
            {
                challenge.ChallengeButton.SetActive(false);
            }
        }
    }

    public void DeActivateCompletedChallengeButton(Challenge challenge)
    {
        if (challenge.IsCompleted)
        {
            challenge.ChallengeButton.SetActive(false);
        }
    }

    public void UpdateActivesChallengesTMP(Challenge challenge)
    {

        if (challenge.IsActivated)
        {
            if (_toggleActiveText == 0)
            {
                _firstActiveChallengeText.text = challenge.ChallengeName.ToString();
                _toggleActiveText++;
            }
            else if (_toggleActiveText == 1)
            {
                _secondActiveChallengeText.text = challenge.ChallengeName.ToString();
                _toggleActiveText--;
            }
        }
        else
        {
            if (_firstActiveChallengeText.text == challenge.ChallengeName.ToString())
            {
                _firstActiveChallengeText.text = "";
                _toggleActiveText = 0;
            }
            else if(_secondActiveChallengeText.text == challenge.ChallengeName.ToString())
            {
                _secondActiveChallengeText.text = "";
                _toggleActiveText = 1;
            }
        }
    }

    private void RemoveText()
    {
        //_activeChallengesText.text -= challenge.ChallengeName + ", ";
    }

    public void AddChallenge(Challenge challenge)
    {
        if (!AvailableChallenges.Contains(challenge))
        {
            AvailableChallenges.Add(challenge);
            Debug.Log("Challenge added to AvailableChallenges (List)" + challenge.ChallengeName);
        }
    }

    public void RemoveChallenge(Challenge challenge)
    {
        if (AvailableChallenges.Contains(challenge))
        {
            AvailableChallenges.Remove(challenge);
            Debug.Log("Challenge removed from AvailableChallenges (List)" + challenge.ChallengeName);
        }
    }

    public void ActivateOrDeactivateChalleng(Challenge challenge)
    {
        if (ActiveChallenges.Contains(challenge))
        {
            DeActivateChallenge(challenge);
            UpdateActivesChallengesTMP(challenge);
        }
        else
        {
            if (_activeChallengesCounter < _maxActiveChallenges)
            {
                ActivateChallenge(challenge);
                UpdateActivesChallengesTMP(challenge);
            }
            else
            {
                Debug.Log("Active challenges limit reached");
                Challenge challengeToDeActivate = ActiveChallenges.FirstOrDefault();
                DeActivateChallenge(challengeToDeActivate);
                ActivateChallenge(challenge);
                UpdateActivesChallengesTMP(challenge);
            }
        }
    }

    public void ActivateChallenge(Challenge challenge)
    {
        ActiveChallenges.Add(challenge);
        _activeChallengesCounter++;
        challenge.IsActivated = true;

        Debug.Log(_activeChallengesCounter);
        Debug.Log("Challenge added to ActiveChallenges (List)" + challenge.ChallengeName);
    }

    public void DeActivateChallenge(Challenge challenge)
    {
        ActiveChallenges.Remove(challenge);
        _activeChallengesCounter--;
        challenge.IsActivated = false;

        Debug.Log("Challenge Removed from ActiveChallenges (List)" + challenge.ChallengeName);
    }

    public void ReverseIterateRemove()
    {
        for (int i = ActiveChallenges.Count - 1; i >= 0; i--)
        {
            if (ActiveChallenges[i].IsCompleted)
            {
                ActiveChallenges.RemoveAt(i);
            }
        }
    }

    public void CheckChallenges(Player player)
    {
        foreach (Challenge challenge in ActiveChallenges)
        {
            if (!challenge.IsCompleted /* && player.killCount >= challenge.Requirement */) // TODO : add player.killcount in Player class
            {
                // player.GrantReward(challenge.Reward); TODO : add GrantReward in Player class

                OnChallengeCompleted?.Invoke(challenge);
                challenge.IsCompleted = true;
            }
        }

        ReverseIterateRemove();
    }

}
