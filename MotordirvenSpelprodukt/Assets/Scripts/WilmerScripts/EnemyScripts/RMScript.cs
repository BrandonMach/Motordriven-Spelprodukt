using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RMScript : MinionScript
{
    [SerializeField] private Transform _fireArrowPos;
    [SerializeField] private float _closestPlayerCanBe;
    [SerializeField] private ArrowManager _arrowManager;
    


    protected override void Start()
    {
        base.Start();
        AttackRange = 20;
    }


    protected override void Update()
    {
        base.Update();
    }


    
    public void FireArrowAnimEvent()
    {
        Attack attack = new Attack
        {
            AttackSO = _attackSOArray[0],
            Damage = _weapon.GetDamage()
        };

        _arrowManager.FireArrowFromPool(attack, _fireArrowPos, transform.forward);
    }



















    public void OnDestroy()
    {
        //Death animation
    }
}
