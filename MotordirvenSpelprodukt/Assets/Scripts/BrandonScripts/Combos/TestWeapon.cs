using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TestWeaponForAnimation")]
public class TestWeapon : ScriptableObject
{
    public List<AttackSO> LightAnimationType;
    public List<AttackSO> HeavyAnimationType;
    public string WeaponTypeName;
}
