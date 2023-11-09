using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class MMScript : EnemyScript
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

        _attackRange = 4f;
        //_timeBetweenAttacks = 2;
        ChaseDistance = 4f;

        
        Anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        
        


    }

    //protected override void OnAttack()
    //{
    //    base.OnAttack();
    //}

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }



}
