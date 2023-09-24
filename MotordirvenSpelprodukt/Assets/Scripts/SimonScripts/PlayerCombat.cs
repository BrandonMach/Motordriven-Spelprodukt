using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private Player player;


    void Start()
    {
        player.OnAttackPressed += Player_OnAttack;
    }

    private void Player_OnAttack(object sender, Player.OnAttackPressedEventArgs e)
    {
        // Handle attack logic
        // Check for collision with enemy
        // Deal damage

        // if(sword collision with enemy)
        // {
        //
        



        switch (e.attackType)
        {
            default:
                break;
        }
    }


    void Update()
    {
        
    }
}
