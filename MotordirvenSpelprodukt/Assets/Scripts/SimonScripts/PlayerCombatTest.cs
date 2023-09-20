using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatTest : MonoBehaviour
{
    [SerializeField] private GameInput gameInput;
    [SerializeField] private Animator animator;
    private EntertainmentManager _etpManager;
    private string currentCombo = "";
    private float desiredWeight = 0;
    private float weight = 0;
    private float weightChanger = -0.025f;
    private float inputTimer = 0;
    public float timeBetweenInputs = 1.5f;


    void Start()
    {
        //_etpManager = GameObject.Find("Canvas").GetComponent<EntertainmentManager>();
        animator.SetTrigger("Awake");
        gameInput.OnLightAttackButtonPressed += GameInput_OnLightAttackButtonPressed;
        gameInput.OnHeavyAttackButtonPressed += GameInput_OnHeavyAttackButtonPressed;
    }

    private void GameInput_OnLightAttackButtonPressed(object sender, EventArgs e)
    {      
        HandleInput("L");
    }

    private void GameInput_OnHeavyAttackButtonPressed(object sender, EventArgs e)
    {
        HandleInput("H");
    }

    private void HandleInput(string attackType)
    {
        // If combat layer is disabled, allow new input.
        
        if (inputTimer > timeBetweenInputs)
        {
            currentCombo += attackType;
            Debug.Log(currentCombo);
            AnimateAttack();
            inputTimer = 0;
        }
    }

    private void AnimateAttack()
    {
        animator.SetTrigger(currentCombo);
        if (currentCombo.Length == 3 || currentCombo == "LH" || currentCombo == "HL")
        {
            Debug.LogError("Combo matched");
            //_etpManager.increaseETP(15);
            currentCombo = "";
            timeBetweenInputs = 100;
        }
    }

    private void Update()
    {
        inputTimer += Time.deltaTime;

        if (weight != desiredWeight)
        {
            weight -= (1 * weightChanger);

            if (weight < 0f)
            {
                weight = 0.01f;
            }
            else if(weight > 1f)
            {
                weight = 0.99f;
            }
            animator.SetLayerWeight(4, weight);
        }
    }

    public void DisableCombatLayer()
    {
        Debug.Log("DisableCombatLayer : Combo=" + currentCombo);
        desiredWeight = 0.01f;
        weightChanger = 0.025f;
        weight = 0.99f;
    }

    public void EnableCombatLayer()
    {
        Debug.Log("EnableCombatLayer: Combo=" + currentCombo);
        desiredWeight = 0.99f;
        weightChanger = -0.025f;
        weight = 0.01f;
    }
}
