using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SimonComboCopy : MonoBehaviour
{
    //[SerializeField] private GameInput gameInput;
    [SerializeField] private Player player;
    [SerializeField] private Animator animator;
    private EntertainmentManager _etpManager;
    private string currentCombo = ""; //
    private float desiredWeight = 0;
    private float weight = 0;
    private float weightChanger = -0.025f;
    private float inputTimer = 0;
    public float timeBetweenInputs = 1.5f;

    [Header("Combo Sequence")]
    [SerializeField] private float _comboWindowTimer = 0;
    [SerializeField] private bool _startComboWindowTimer;
    float comboWindow = 2;

    [Header("Weapon")]
    [SerializeField] Transform _weaponHandPos;
    public List<Weapontype> WeaponAnimation;
    [SerializeField] Weapontype _currentWeaponType;
    private int _weaponTypeIndex = 0;
    [SerializeField] private TextMeshProUGUI _text;

    void Start()
    {
        //_etpManager = GameObject.Find("Canvas").GetComponent<EntertainmentManager>();
        animator.SetTrigger("Awake");
        player.OnAttackPressed += Player_OnAttack;
        //player.OnAttackPressed += Player_OnHeavyAttackPressed;
        _currentWeaponType = WeaponAnimation[_weaponTypeIndex];
        Instantiate(_currentWeaponType.GetPrefab(), _weaponHandPos.position , _currentWeaponType.GetPrefab().transform.rotation);
    }

    private void Player_OnAttack(object sender, Player.OnAttackPressedEventArgs e)
    {
        _startComboWindowTimer = true;
        _comboWindowTimer = 0;
        //HandleInput(e.attackType);
        
    }
    //private void Player_OnHeavyAttackPressed(object sender, EventArgs e)
    //{
    //    HandleInput("H");
    //}

    //private void Player_OnLightAttackPressed(object sender, EventArgs e)
    //{
    //    HandleInput("L");
    //}

    private void HandleInput(string attackType)
    {
        // If combat layer is disabled, allow new input.

        if (inputTimer > timeBetweenInputs)
        {
            currentCombo += attackType; // Adds character to string combo
            Debug.Log(currentCombo);
            AnimateAttack();
            inputTimer = 0;
        }
    }

    private void AnimateAttack()
    {
        // CurrentCombo = "LL"
        animator.SetTrigger("Trigger_" + currentCombo);
        if (currentCombo.Length == 3 || currentCombo == "LH" || currentCombo == "HL")
        {
            Debug.LogError("Combo matched");
            _etpManager.increaseETP(15);
            currentCombo = "";
            timeBetweenInputs = 1.5f;
            
            
        }
    }

    private void Update()
    {
        _text.text = WeaponAnimation[_weaponTypeIndex].GetNameList(); //Display weapon name
        _currentWeaponType = WeaponAnimation[_weaponTypeIndex];


        inputTimer += Time.deltaTime;

        HandleAnimationLayers();

        if (Input.GetKeyDown(KeyCode.M)) //För testing
        {
            if (_weaponTypeIndex < WeaponAnimation.Count - 1)
            {
                _weaponTypeIndex++;
            }
            else
            {
                _weaponTypeIndex = 0;
            }

            
        }
        HandleWeapon(); // Override vilken vapen animation man ska ha beroende på vapen

        if (_startComboWindowTimer)
        {
            StartComboWindowCheck();
        }
    }

    private void HandleWeapon(/*int attackType*/)
    {
        var _setWeaponTypeAnimations = WeaponAnimation[_weaponTypeIndex];
        animator.runtimeAnimatorController = _setWeaponTypeAnimations.WeaponAnimations.AnimatorOV;
    }

    private void HandleAnimationLayers()
    {
        if (weight != desiredWeight)
        {
            weight -= (1 * weightChanger);

            if (weight < 0f)
            {
                weight = 0.01f;
            }
            else if (weight > 1f)
            {
                weight = 0.99f;
            }

            animator.SetLayerWeight(animator.GetLayerIndex("CombatTree"), weight);
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
        _startComboWindowTimer = true;
        Debug.Log("EnableCombatLayer: Combo=" + currentCombo);
        desiredWeight = 0.99f;
        weightChanger = -0.025f;
        weight = 0.01f;
    }


    void StartComboWindowCheck()
    {
        
        _comboWindowTimer += Time.deltaTime;

        if (_comboWindowTimer >= comboWindow)
        {
            EndCombo();
        }
    }

    void EndCombo()
    {
        _comboWindowTimer = 0;
        currentCombo = "";
        animator.SetTrigger("ComboBroken");
        _startComboWindowTimer = false;
        Debug.LogError("Combo Broken");
    }
}
