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
        bleed,
        AreaDamage
    }
    public AttackEffect CurrentAttackEffect;
    public float DamageMultiplier;

    public bool Last;
    public string Name;
 
}

