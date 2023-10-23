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

    private Collider _weaponCollider;

    //private GameObject _objWeapon;

    ////Orc special attack Slam Attack
    //public Collider _mainColider;
    //public GameObject _slamHitbox;

    public float Damage;
    public float ETPDecreaseValue;

    public float NTime;
    public bool CanChase;

    void Start()
    {
        _movementSpeed = 2;
        _attackRange = 6;
        _attackCooldown = 2;
        ChaseDistance = 4;
        CanAttack = true;
        Anim = GetComponent<Animator>();
        ac = Anim.runtimeAnimatorController;



        //_objWeapon = _weaponHolderTransform.GetChild(0).gameObject;
        //_weaponCollider = _objWeapon.GetComponent<Collider>();

        //_weaponCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        NTime = Anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }

    public void OnDestroy()
    {
        //Death animation
    }
    public void ActivateAttackHitBox()
    {
        //_weaponCollider.enabled = true;

    }

    public void DeactivateAttackHitBox()
    {
        //_weaponCollider.enabled = false;
    }


}
