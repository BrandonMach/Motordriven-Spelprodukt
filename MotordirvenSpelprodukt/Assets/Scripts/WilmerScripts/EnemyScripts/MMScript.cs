using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class MMScript : EnemyScript
{
    // Start is called before the first frame update

    public float ChaseDistance;


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
        _movementSpeed = 0.2f;
        _attackRange = 0.5f;
        _attackCooldown = 2;
        ChaseDistance = 2;

        
        Anim = GetComponent<Animator>();
        ac = Anim.runtimeAnimatorController;

    }

    // Update is called once per frame
    void Update()
    {
        base.Update();

        NTime = Anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
        if(NTime > 1.0f)
        {
            CurrentImpairement = Impairement.none;
        }
        //OnGround = Physics.CheckSphere(sphereCheck.position, sphereCheckRadius, groundLayer);
    }

    public void OnDestroy()
    {
        //Death animation
    }



}
