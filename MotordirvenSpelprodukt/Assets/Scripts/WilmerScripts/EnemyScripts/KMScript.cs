using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KMScript : EnemyScript
{

    public Animator Anim;
    public float ChaseDistance;


    [Header("Attack")]
    [SerializeField] private Collider expolisionHitbox;
    [SerializeField] public float _diveRange;
    public ParticleSystem _explosion;
    [SerializeField] public bool PlayerInpact;

    protected override void Start()
    {
        base.Start();
        expolisionHitbox.enabled = false;
        _movementSpeed = 5; //Ramp up speed kan testas
        _attackRange = 0.2f; //?? kanske inte  behövs
        //_timeBetweenAttacks = 2; //Kanske inte behövs
        ChaseDistance = 2;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    protected override void OnAttack()
    {

        

        base.OnAttack();
        //Instantiate(_explosion, this.transform);
        this.GetComponent<HealthManager>().ReduceHealth(100);
    }
}
