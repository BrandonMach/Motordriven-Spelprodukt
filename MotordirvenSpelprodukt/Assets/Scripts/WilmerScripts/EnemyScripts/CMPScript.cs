using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CMPScript : EnemyScript
{
    

    public float DamageBuff;

    public float ChaseDistance;

    public Animator Anim;

    public int AttackIndex;

    public bool CanAttack;
    public bool StopChase;
    public bool AnimationPlaying = false;

    RuntimeAnimatorController ac;


    [Header ("Attacks")]
    [SerializeField] private Transform _weaponHolderTransform;

    private Collider _weaponCollider;

    private GameObject _objWeapon;

    //Orc special attack Slam Attack
    public Collider _mainColider;
    public GameObject _slamHitbox;

    public float Damage;
    public float ETPDecreaseValue;

    public float NTime;
    public bool CanChase;

    [SerializeField] private CinemachineCollisionImpulseSource _impulseSource;


    protected override void Start()
    {
        base.Start();

        _movementSpeed = 2;
        _attackRange = 6;
        //_timeBetweenAttacks = 2;
        ChaseDistance = 4;
        CanAttack = true;

        ac = Anim.runtimeAnimatorController;



        _objWeapon = _weaponHolderTransform.GetChild(0).gameObject;
        _weaponCollider = _objWeapon.GetComponent<Collider>();

        _weaponCollider.enabled = false;

        _slamHitbox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
       NTime = Anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }


    public void ActivateAttackHitBox()
    {
        _weaponCollider.enabled = true;

    }

    public void DeactivateAttackHitBox()
    {
        _weaponCollider.enabled = false;
    }

    //Orc Attack
    public void ActivateSlamAttack()
    {
        _slamHitbox.SetActive(true);
        //Ignore player hitbox
        Physics.IgnoreCollision(GameObject.FindGameObjectWithTag("Player").GetComponent<Collider>(), _mainColider);
    }

    public void DeactivateSlamAttack()
    {
        _slamHitbox.SetActive(false);
        Physics.IgnoreCollision(GameObject.FindGameObjectWithTag("Player").GetComponent<Collider>(), _mainColider,false);
    }

    protected override void OnAttack()
    {
       
        base.OnAttack();
    }


}
