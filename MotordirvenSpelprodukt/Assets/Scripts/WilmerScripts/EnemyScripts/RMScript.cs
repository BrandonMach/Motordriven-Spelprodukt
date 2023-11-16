using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RMScript : MinionScript
{
    [SerializeField] private Transform _fireArrowPos;
    [SerializeField] private float _startFleeRange;
    [SerializeField] private ArrowManager _arrowManager;
    private NavMeshAgent _agent;
    public float rang;
    public float StartFleeRange 
    { 
        get { return _startFleeRange; } 
        private set { _startFleeRange = value; } 
    } 


    protected override void Start()
    {
        base.Start();
        AttackRange = 20;
        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = MovementSpeed;
        rang = AttackRange;
    }


    protected override void Update()
    {
        base.Update();
    }


    protected override void HandleFleeing()
    {
        base.HandleFleeing();
        ResetTriggers();
        Anim.SetTrigger("Walking");
        if (CurrentState == EnemyState.fleeing)
        {
            Vector3 dir = transform.position - Player.Instance.transform.position;
            Vector3 goTo = transform.position + dir.normalized * 10;
            _agent.SetDestination(goTo);
        }
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

    protected override void HandleAttack()
    {
        base.HandleAttack();
        ResetTriggers();
        Anim.SetTrigger("Shoot");
    }

















    public void OnDestroy()
    {
        //Death animation
    }
}
