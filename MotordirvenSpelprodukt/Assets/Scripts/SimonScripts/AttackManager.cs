using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{

    [SerializeField] private LayerMask _enemyLayerMask;
    private string _currentCombo;
    private float _range;
    private float _impactPosOffset = 1.5f;
    private float _damage;
    private float _multiplier;
    private Vector3 _colliderPos;
    private CurrentAttackSO.AttackEffect _effect;
    private CurrentAttackSO.AttackType _attackType;
    private Vector3 _checkPos;
    private string debugCurrentAttackMessage;
    private ICanAttack attacker;

    [SerializeField] private ParticleSystem stunEffect;

    void Start()
    {
        attacker = GetComponent<ICanAttack>();

        attacker.OnRegisterAttack += Attacker_OnAttack;
        //stunEffect = Resources.Load<ParticleSystem>("ObjStunnedEffect");
    }

    private void Attacker_OnAttack(object sender, OnAttackPressedEventArgs e)
    {
        RecieveAttackEvent(e);
        HandleAttack(e);
    }



    private void HandleAttack(OnAttackPressedEventArgs e)
    {
        Gizmos.color = Color.red;
        // Temporary fix until _range can be passed on via event.
        // SHOULD BE REMOVED LATER.
        Debug.Log(debugCurrentAttackMessage);
        switch (_attackType)
        {
            case CurrentAttackSO.AttackType.AOE:
                //_range = 5;
                _range *= 3;
                _checkPos = (transform.position + (transform.forward * _impactPosOffset) + (transform.up * transform.localScale.y));
                break;
            case CurrentAttackSO.AttackType.Directional:
                //_range = 1.5f;
                _checkPos = (transform.position + (transform.forward * _range) + (transform.up * transform.localScale.y));
                break;
            default:
                break;
        }


        Collider[] enemyHits = Physics.OverlapSphere(_checkPos, _range, _enemyLayerMask);

        for (int i = 0; i < enemyHits.Length; i++)
        {
            IDamagable enemy = enemyHits[i].GetComponent<IDamagable>();
            if (enemy != null)
            {
                enemy.TakeDamage(_damage, new Attack { AttackSO = e.CurrentAttackSO, Position = transform.position});
            }
        }
    }

    private void RecieveAttackEvent(OnAttackPressedEventArgs e)
    {
        _range = e.weaponSO.GetRange();
        _damage = e.weaponSO.GetDamage();
        //_range = 1.5f;
        //_damage = 25f;
        _multiplier = e.CurrentAttackSO.DamageMultiplier;
        _effect = e.CurrentAttackSO.CurrentAttackEffect;
        _attackType = e.CurrentAttackSO.currentAttackType;
        debugCurrentAttackMessage = e.CurrentAttackSO.name;
    }


    //private void HandelStun(IDamagable enemy)
    //{
    //    //enemy.TakeDamage(2.0f, new Attack { AttackEffect = , Position = transform.position });
    //    Vector3 pos = (enemy as MonoBehaviour).transform.position;
    //    pos = new Vector3(pos.x, pos.y + (enemy as MonoBehaviour).transform.localScale.y + 1, pos.z);
    //    Instantiate(stunEffect, pos, Quaternion.Euler(-90, 0, 0), transform);
    //}


    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.green;
        //Vector3 drawPos = new Vector3(transform.position.)
        Gizmos.DrawWireSphere(_checkPos, _range);
    }
    //private void HandleInput(Player.OnAttackPressedEventArgs e)
    //{
    //    switch (e.attackType1)
    //    {
    //        case Player.OnAttackPressedEventArgs.AttackType.Light:
    //            _currentCombo += "L";
    //            break;
    //        case Player.OnAttackPressedEventArgs.AttackType.Heavy:
    //            _currentCombo += "H";
    //            break;
    //        default:
    //            break;
    //    }
    //}


    //private void HandleAttack(Player.OnAttackPressedEventArgs e)
    //{
    //    switch (_currentCombo)
    //    {
    //        case "L":
    //            attack = new L_Attack();
    //            break;

    //        case "LL":
    //            attack = new L_Attack();
    //            break;

    //        case "LLH":
    //            attack = new LLH_Attack();
    //            break;
    //    }

    //    RaycastHit[] test = Physics.SphereCastAll(transform.position, e.weaponRange, transform.forward, e.weaponRange, LayerMask.NameToLayer("enemyLayer"));

    //    for (int i = 0; i < test.Length; i++)
    //    {
    //        //attack.Attack(test[i]);
    //    }
    //}
}
