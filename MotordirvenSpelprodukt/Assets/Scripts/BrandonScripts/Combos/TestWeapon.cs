using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TestWeaponForAnimation")]
public class TestWeapon : ScriptableObject
{
    public List<AttackSO> AnimationType;
    public string WeaponTypeName;
}
