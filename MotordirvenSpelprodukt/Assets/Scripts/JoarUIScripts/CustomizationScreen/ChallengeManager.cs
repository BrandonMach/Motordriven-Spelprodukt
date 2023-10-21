using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChallengeManager : MonoBehaviour
{
    // Is only to be instantiated in the CustomizationScreen, but can be reachable throughout the project with ChallengeManager.Instance.
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

    }
    #endregion


    public List<Challenge> AvailableChallenges = new List<Challenge>();
    public List<Challenge> ActiveChallenges = new List<Challenge>();
    public event Action<Challenge> OnChallengeCompleted;

    [SerializeField] TextMeshProUGUI _activeChallengesText;

    private int _activeChallengesCounter;
    private const int _maxActiveChallenges = 2;

    private void Update()
    {
        
    }

    public void UpdateActivesChallengesTMP()
    {
        if (_activeChallengesCounter <= _maxActiveChallenges)
        {
            foreach (Challenge challenge in ActiveChallenges)
            {
                _activeChallengesText.text += challenge.ChallengeName + ", ";
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
        }
        else
        {
            if (_activeChallengesCounter < _maxActiveChallenges)
            {
                ActivateChallenge(challenge);
                UpdateActivesChallengesTMP();
            }
            else
            {
                Debug.Log("Active challenges limit reached");
            }
        }
    }

    public void ActivateChallenge(Challenge challenge)
    {
        ActiveChallenges.Add(challenge);
        _activeChallengesCounter++;

        Debug.Log(_activeChallengesCounter);
        Debug.Log("Challenge added to ActiveChallenges (List)" + challenge.ChallengeName);
    }

    public void DeActivateChallenge(Challenge challenge)
    {
        ActiveChallenges.Remove(challenge);
        _activeChallengesCounter--;

        Debug.Log("Challenge Removed from ActiveChallenges (List)" + challenge.ChallengeName);
    }

    private void ReverseIterateRemove()
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
