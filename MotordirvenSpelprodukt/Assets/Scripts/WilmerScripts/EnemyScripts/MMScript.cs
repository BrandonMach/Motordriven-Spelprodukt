using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class MMScript : EnemyScript
{
    // Start is called before the first frame update

    public float ChaseDistance;
    public bool CanAttack;
    public bool StopChase;
    public bool AnimationPlaying = false;
    public Animator Anim;


    RuntimeAnimatorController ac;

    [Header("Attacks")]
    [SerializeField] private Transform _weaponHolderTransform;



    public float Damage;
    public float ETPDecreaseValue;

    public float NTime;
    //public bool CanChase;

    void Start()
    {
        _movementSpeed = 2;
        _attackRange = 6;
        _attackCooldown = 2;
        ChaseDistance = 4;
        CanAttack = true;
        CanChase = true;
        Impaired = true;
        Anim = GetComponent<Animator>();
        ac = Anim.runtimeAnimatorController;

    }

    // Update is called once per frame
    void Update()
    {
        NTime = Anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
        OnGround = Physics.CheckSphere(sphereCheck.position, sphereCheckRadius, groundLayer);
    }

    public void OnDestroy()
    {
        //Death animation
    }



}
