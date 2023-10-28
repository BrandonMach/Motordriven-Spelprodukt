using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class MMScript : EnemyScript
{
    // Start is called before the first frame update

    public float ChaseDistance;

    public Animator Anim;

    [Header("Attacks")]
    [SerializeField] private Transform _weaponHolderTransform;



    public float Damage;
    public float ETPDecreaseValue;
    //public bool CanChase;

    protected override void Start()
    {
        base.Start();

        _attackRange = 2f;
        _attackCooldown = 2;
        ChaseDistance = 2f;

        
        Anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();


        //OnGround = Physics.CheckSphere(sphereCheck.position, sphereCheckRadius, groundLayer);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }



}
