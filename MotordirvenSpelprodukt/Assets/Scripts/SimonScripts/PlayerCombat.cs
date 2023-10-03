using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private Player player;
    private string _currentCombo;
    private float _range;
    private float _impactPosOffset =1.5f;
    private float _damage;
    private float _multiplier;
    private Vector3 _colliderPos;
    private CurrentAttackSO.AttackEffect _effect;
    private Vector3 _checkPos;
    [SerializeField] private ParticleSystem stunEffect;

    void Start()
    {
        player.OnAttackPressed += Player_OnAttack;
        //stunEffect = Resources.Load<ParticleSystem>("ObjStunnedEffect");
    }

    private void Player_OnAttack(object sender, Player.OnAttackPressedEventArgs e)
    {
        RecieveAttackEvent(e);
        //HandleAttack(e);
    }



    private void HandleAttack()
    {
        Gizmos.color = Color.red;
        // Temporary fix until _range can be passed on via event.
        // SHOULD BE REMOVED LATER.
        switch (_effect)
        {
            case CurrentAttackSO.AttackEffect.None:
                break;
            case CurrentAttackSO.AttackEffect.Pushback:
                break;
            case CurrentAttackSO.AttackEffect.bleed: // Should be swapped to None here and on L scriptable object.
                // Normal attack, check infront of player.
                // Sphere starts at front edge of player and diameter = _range
                _range = 1.5f;
                _checkPos = (transform.position + (transform.forward * _range) + (transform.up * transform.localScale.y));
                break;
            case CurrentAttackSO.AttackEffect.AreaDamage:
            case CurrentAttackSO.AttackEffect.Stun:
                // Area attack, check around player 
                // Sphere is centered around player 
                _range = 5;
                _checkPos = (transform.position + (transform.forward * _impactPosOffset) + (transform.up * transform.localScale.y));
                break;
            default:
                break;
        }

        Collider[] enemyHits = Physics.OverlapSphere(_checkPos, _range);

        for (int i = 0; i < enemyHits.Length; i++)
        {
            IDamagable enemy = enemyHits[i].GetComponent<IDamagable>();
            if (enemy != null)
            {
                switch (_effect)
                {
                    case CurrentAttackSO.AttackEffect.None:
                        break;
                    case CurrentAttackSO.AttackEffect.Pushback:
                        break;
                    case CurrentAttackSO.AttackEffect.bleed:

                        break;
                    case CurrentAttackSO.AttackEffect.AreaDamage:
                        break;
                    case CurrentAttackSO.AttackEffect.Stun:
                        HandelStun(enemy);
                        break;
                    default:
                        break;
                }

                enemy.TakeDamage(1.0f);
            }
        }
        Gizmos.color = Color.green;
    }


    private void RecieveAttackEvent(Player.OnAttackPressedEventArgs e)
    {
        //_range = e.weaponSO.GetRange();
        //_damage = e.weaponSO.GetDamage();
        _range = 1.5f;
        _damage = 25f;
        _multiplier = e.CurrentAttackSO.DamageMultiplier;
        _effect = e.CurrentAttackSO.CurrentAttackEffect;
    }
    

    private void HandelStun(IDamagable enemy)
    {
        enemy.GetStunned(2.0f);
        Vector3 pos = (enemy as MonoBehaviour).transform.position;
        pos = new Vector3(pos.x, pos.y + 1, pos.z);
        Instantiate(stunEffect, pos, Quaternion.Euler(-90, 0, 0));
    }


    public void RegisterAttack()
    {
        HandleAttack();
    }


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
