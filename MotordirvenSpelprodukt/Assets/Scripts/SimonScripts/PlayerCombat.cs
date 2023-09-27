using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private Player player;
    private string _currentCombo;
    private float _range;
    private float _damage;
    private float _multiplier;
    private Vector3 _colliderPos;
    private CurrentAttackSO.AttackEffect _effect;

    void Start()
    {
        player.OnAttackPressed += Player_OnAttack;
    }

    private void Player_OnAttack(object sender, Player.OnAttackPressedEventArgs e)
    {
        // Handle attack logic
        // Check for collision with enemy
        // Deal damage

        RecieveAttackEvent(e);

        HandleAttack(e);
    }



    private void HandleAttack(Player.OnAttackPressedEventArgs e)
    {
        // TODO:
        // Set animation event to the animations to check when an attack should check for collisions.

        Collider[] enemyHits = Physics.OverlapSphere(transform.position + (transform.forward * _range) + (transform.up * transform.localScale.y), _range);

        for (int i = 0; i < enemyHits.Length; i++)
        {
            IDamagable enemy = enemyHits[i].GetComponent<IDamagable>();
            if (enemy != null)
            {
                enemy.TakeDamage();
            }
        }
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        //Vector3 drawPos = new Vector3(transform.position.)
        Gizmos.DrawWireSphere(transform.position + (transform.forward * _range) + (transform.up * transform.localScale.y), _range);
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


    void Update()
    {

    }
}
