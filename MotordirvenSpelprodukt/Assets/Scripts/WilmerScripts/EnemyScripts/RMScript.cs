using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RMScript : MinionScript
{
    [SerializeField] private Transform _fireArrowPos;
    [SerializeField] private float _startFleeRange;
    [SerializeField] private ArrowManager _arrowManager;
    private NavMeshAgent _navMesh;
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
        _navMesh = GetComponent<NavMeshAgent>();
        _navMesh.speed = MovementSpeed;
        rang = AttackRange;
    }


    protected override void Update()
    {
        base.Update();
        if (CurrentState == EnemyState.fleeing)
        {
            _navMesh.enabled = true;
        }
        else _navMesh.enabled = false;
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
            _navMesh.SetDestination(goTo);
            FacePlayer();
        }
    }


    //protected override void HandleChase()
    //{
    //    ResetTriggers();
    //    Anim.SetTrigger("Walking");
    //    _agent.SetDestination(Player.Instance.transform.position);
    //}


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
