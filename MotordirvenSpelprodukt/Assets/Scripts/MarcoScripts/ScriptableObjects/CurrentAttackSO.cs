using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class CurrentAttackSO : ScriptableObject
{
    public enum AttackEffect
    {
        None,
        Pushback,
        StunAndPushback,
        Knockup,
        Bleed,
        Stun
    }

    public enum AttackType
    {
        AOE,
        Directional
    }

    public AttackType currentAttackType;
    public AttackEffect CurrentAttackEffect;
    public float DamageMultiplier;
    public float Force;
    public float StunDuration;
    public bool Last;
    public string Name;
    public int ETPChange;
    // ETP Decrease for Enemies
    // ETP Increase for Player
}

