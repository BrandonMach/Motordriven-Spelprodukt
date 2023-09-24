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
        Collider[] test = Physics.OverlapSphere(transform.position, _range);
       
    }

    private void RecieveAttackEvent(Player.OnAttackPressedEventArgs e)
    {
        _range = e.weaponRange;
        _damage = e.weaponDamage;
        _multiplier = e.CurrentAttack.DamageMultiplier;
        _effect = e.CurrentAttack.CurrentAttackEffect;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _range);
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
