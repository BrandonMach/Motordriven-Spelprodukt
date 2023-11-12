using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KMScript : MinionScript
{

    public Animator Anim;
    public float ChaseDistance;


    [Header("Attack")]
    [SerializeField] private Collider expolisionHitbox;
    [SerializeField] public float _diveRange;
    public ParticleSystem _explosion;

    protected override void Start()
    {
        base.Start();
        expolisionHitbox.enabled = false;
        MovementSpeed = 5; //Ramp up speed kan testas
        AttackRange = 0.2f; //?? kanske inte  behövs
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
