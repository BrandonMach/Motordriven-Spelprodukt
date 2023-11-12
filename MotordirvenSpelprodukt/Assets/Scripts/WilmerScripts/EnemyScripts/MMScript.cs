using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class MMScript : MinionScript
{
    // Start is called before the first frame update

    public float ChaseDistance;
    

   

    [Header("Attacks")]
    [SerializeField] private Transform _weaponHolderTransform;



    public float Damage;
    public float ETPDecreaseValue;
    //public bool CanChase;

    protected override void Start()
    {
        base.Start();

        AttackRange = 4f;
        //_timeBetweenAttacks = 2;
        ChaseDistance = 4f;
     
        Anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        //_rb.AddForce(Vector3.down * _rb.mass * 9.81f, ForceMode.Force);
    }

    //protected override void OnAttack()
    //{
    //    base.OnAttack();
    //}

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }


    protected override void ResetTriggers()
    {
        // Call ResetTriggers on MinionScript
        base.ResetTriggers();


        // Only for MM minion
        Anim.ResetTrigger("LightAttack");
        Anim.ResetTrigger("HeavyAttack");
    }
}
