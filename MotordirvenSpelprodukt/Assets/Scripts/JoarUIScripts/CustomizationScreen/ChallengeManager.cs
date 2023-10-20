using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChallengeManager : MonoBehaviour
{

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

    public void AddChallenge(Challenge challenge)
    {
        if (!AvailableChallenges.Contains(challenge))
        {
            AvailableChallenges.Add(challenge);
            Debug.Log("Challenge added to AvailableChallenges (List)");
        }
    }

    public void RemoveChallenge(Challenge challenge)
    {
        if (AvailableChallenges.Contains(challenge))
        {
            AvailableChallenges.Remove(challenge);
            Debug.Log("Challenge removed from AvailableChallenges (List)");
        }
    }

    public void ActivateChallenge(Challenge challenge)
    {
        if (!ActiveChallenges.Contains(challenge))
        {
            ActiveChallenges.Add(challenge);
            Debug.Log("Challenge added to ActiveChallenges (List)");
        }

    }

    public void DeActivateChallenge(Challenge challenge)
    {
        if (ActiveChallenges.Contains(challenge))
        {
            ActiveChallenges.Remove(challenge);
            Debug.Log("Challenge Removed from ActiveChallenges (List)");
        }

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
