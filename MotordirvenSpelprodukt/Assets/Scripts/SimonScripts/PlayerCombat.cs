using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private Player player;
    private string _currentCombo;
    private BaseAttack attack;

    void Start()
    {
        player.OnAttackPressed += Player_OnAttack;
    }

    private void Player_OnAttack(object sender, Player.OnAttackPressedEventArgs e)
    {
        // Handle attack logic
        // Check for collision with enemy
        // Deal damage

        HandleInput(e);

        attack.Attack();
    }




    private void HandleInput(Player.OnAttackPressedEventArgs e)
    {
        switch (e.attackType1)
        {
            case Player.OnAttackPressedEventArgs.AttackType.Light:
                _currentCombo += "L";
                break;
            case Player.OnAttackPressedEventArgs.AttackType.Heavy:
                _currentCombo += "H";
                break;
            default:
                break;
        }


    }

    private void HandleAttack(Player.OnAttackPressedEventArgs e)
    {
        switch (_currentCombo)
        {
            case "L":
                attack = new L_Attack();
                break;

            case "LL":
                attack = new L_Attack();
                break;

            case "LLH":
                attack = new LLH_Attack();
                break;
        }

        RaycastHit[] test = Physics.SphereCastAll(transform.position, e.weaponRange, transform.forward, e.weaponRange, LayerMask.NameToLayer("enemyLayer"));

        for (int i = 0; i < test.Length; i++)
        {
            //attack.Attack(test[i]);
        }
    }


    void Update()
    {
        
    }
}
