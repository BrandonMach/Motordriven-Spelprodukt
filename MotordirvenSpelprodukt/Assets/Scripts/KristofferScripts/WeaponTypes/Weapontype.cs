using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapontype : ScriptableObject
{
    [SerializeField] private Image image;
    [SerializeField] private float baseDamage;
    [SerializeField] private float weaponRange;
    [SerializeField] private float attackSpeed;
    [SerializeField] private Animation weaponAnimation;
    public Image GetImage(){return image;}
    public Animation GetAnimation(){return weaponAnimation;}
    public float GetDamage(){return baseDamage;}
    public float GetRange() { return weaponRange; }
    public float GetAttackSpeed(){return attackSpeed;}
}
