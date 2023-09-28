using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChallengeManager : MonoBehaviour
{
    public static List<Challenge> ActiveChallenges = new List<Challenge>();
    public event Action<int> OnChallengeCompleted;

    public static void AddChallenge(Challenge challenge)
    {
        if (!ActiveChallenges.Contains(challenge))
        {
            ActiveChallenges.Add(challenge);
        }
    }

    public static void RemoveChallenge(Challenge challenge)
    {
        if (ActiveChallenges.Contains(challenge))
        {
            ActiveChallenges.Remove(challenge);
        }
    }

    private static void ReverseIterateRemove()
    {
        for (int i = ActiveChallenges.Count - 1; i >= 0; i--)
        {
            if (ActiveChallenges[i].IsCompleted)
            {
                ActiveChallenges.RemoveAt(i);
            }
        }
    }

    public static void CheckChallenges(Player player)
    {
        foreach (Challenge challenge in ActiveChallenges)
        {
            if (!challenge.IsCompleted /* && player.killCount >= challenge.Requirement */) // TODO : add player.killcount in Player class
            {
                // player.GrantReward(challenge.Reward); TODO : add GrantReward in Player class
                challenge.IsCompleted = true;
            }
        }

        ReverseIterateRemove();
    }

}
