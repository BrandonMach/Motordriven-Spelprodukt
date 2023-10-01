using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public float Damage;

    void Start()
    {
        _movementSpeed = 2;
        _attackRange = 4;
        _attackCooldown = 2;
        ChaseDistance = 4;
        _currentHealth = 100;
        _maxHealth = 100;
        CanAttack = true;

        ac = Anim.runtimeAnimatorController;



        _objWeapon = _weaponHolderTransform.GetChild(0).gameObject;
        _weaponCollider = _objWeapon.GetComponent<Collider>();

        _weaponCollider.enabled = false;

        
    }

    // Update is called once per frame
    void Update()
    {
        
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
        //Ignore player hitbox
        Physics.IgnoreCollision(GameObject.FindGameObjectWithTag("Player").GetComponent<Collider>(), _mainColider);
    }

    public void DeactivateSlamAttack()
    {
        
        Physics.IgnoreCollision(GameObject.FindGameObjectWithTag("Player").GetComponent<Collider>(), _mainColider,false);
    }
}
