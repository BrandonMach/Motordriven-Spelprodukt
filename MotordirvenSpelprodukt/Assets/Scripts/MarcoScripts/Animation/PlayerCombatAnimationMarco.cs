using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class PlayerCombatAnimationMarco : MonoBehaviour
{

    //public List<AttackSO> Combo;
    public List<TestWeapon> WeaponAnimation;
    [SerializeField] TestWeapon _currentWeaponType;
    private int _weaponTypeIndex = 0;
    [SerializeField] private TextMeshProUGUI _text;

    float _lastClickedTime;
    float _lastComboEnd;
    public int _comboCounter;

    [SerializeField] Animator _anim;
    Animator og;
    //Weapon



    [Header("Combos")]

    [SerializeField] KeyCode[] _attackInputs; //Static attack inputs

    [SerializeField] TextMeshProUGUI _comboTreeInfoText;

    [Header("Combo Sequence")]
    [SerializeField] private float _comboWindowTimer = 0;

    [SerializeField] private bool _startComboWindowTimer;


    public List<AttackMovesSO> ComboList = new List<AttackMovesSO>();

    EntertainmentManager _etpManager;
    public List<KeyCode> _lastUsedInputs = new List<KeyCode>();

    string ComboTree;




    void Start()
    {
        _etpManager = GameObject.Find("Canvas").GetComponent<EntertainmentManager>();
        og = _anim;


        //Write out combo tree
        for (int i = 0; i < ComboList.Count; i++)
        {
            for (int j = 0; j < ComboList[i]._buttonSequence.Count; j++)
            {
                ComboTree += ", " + ComboList[i]._buttonSequence[j].ToString();
            }

            _comboTreeInfoText.text += "Combo " + (i + 1) + ": " + (ComboTree.Remove(0, 1)) + "\n";
            ComboTree = "";

        }

    }

    // Update is called once per frame
    void Update()
    {
        //Temporary switch weapon and different combo animation should play. Animation is changed in WeaponAnimtion
        ChangeWeaponTypeAnimation();

        _text.text = WeaponAnimation[_weaponTypeIndex].WeaponTypeName; //Display weapon name
        _currentWeaponType = WeaponAnimation[_weaponTypeIndex];


        for (int i = 0; i < _attackInputs.Length; i++)
        {
            if (Input.GetKeyDown(_attackInputs[i])) //Checks if attack input has been pressed
            {

                _startComboWindowTimer = true;
                _comboWindowTimer = 0;

                switch (_comboCounter)
                {
                    case 0:
                        CheckComboPath(_comboCounter, i);
                        break;
                    case 1:
                        CheckComboPath(_comboCounter, i);
                        break;
                    case 2:
                        CheckComboPath(_comboCounter, i);
                        break;
                }

                //Kolla om inpute är den fösta inputen i en av combo routes
                //Lägg in tangente i en lista som sparas
                //När nästa tangent klickas kolla om den är den andra tangenten i en av combo routesen
                //Lägg till den tangenten om den är 
                //När tradje tangenten klickas kolla om det är Special tangenten
            }
        }
        ExitAttack();

        if (_startComboWindowTimer)
        {
            StartComboWindowCheck();
        }



    }


    void CheckComboPath(int comboCounter, int attackButtonIndex)
    {
        List<AttackMovesSO> comboPath = new List<AttackMovesSO>();
        foreach (var item in ComboList)
        {
            if (_attackInputs[attackButtonIndex] == item._buttonSequence[_comboCounter])
            {
                comboPath.Add(item);
                Attack(attackButtonIndex);
                ExitAttack();
            }
        }

        if (_comboCounter == 2)
        {
            Debug.LogError("Combo matched");
            _etpManager.increaseETP(15);
        }
        else
        {
            _comboCounter++;
        }

    }



    void Attack(int attackType)
    {
        List<AttackSO> _setWeaponTypeAnimations = WeaponAnimation[_weaponTypeIndex].LightAnimationType;

        switch (attackType) //Switch what weapon animation List to pick the animation clips from
        {
            case 0:
                _setWeaponTypeAnimations = WeaponAnimation[_weaponTypeIndex].LightAnimationType;
                break;
            case 1:
                _setWeaponTypeAnimations = WeaponAnimation[_weaponTypeIndex].HeavyAnimationType;
                break;
            case 3:
                ///Special attack
                break;
        }

        if (Time.time - _lastComboEnd > 0.5f && _comboCounter <= _setWeaponTypeAnimations.Count)
        {
            CancelInvoke("EndCombo");

            if (Time.time - _lastClickedTime >= 0.2f)
            {
                _anim.runtimeAnimatorController = _setWeaponTypeAnimations[_comboCounter].AnimatorOV; //Override the animation controller based on how far into te combo you are.
                _anim.Play("Attack", 4, 0);
                //Damage
                //Knockback
                //VFX

                //_comboCounter++;
                _lastClickedTime = Time.time;

                if (_comboCounter + 1 > _setWeaponTypeAnimations.Count)
                {
                    _comboCounter = 0;
                }
            }
        }
    }

    void ExitAttack() //Checksi if end of animation 
    {
        if (_anim.GetCurrentAnimatorStateInfo(4).normalizedTime > 0.9f && _anim.GetCurrentAnimatorStateInfo(3).IsTag("Attack"))
        {
            Invoke("EndCombo", 1);
        }
    }
    void EndCombo()
    {
        _comboCounter = 0;
        _lastComboEnd = Time.time;
    }

    void ChangeWeaponTypeAnimation()
    {
        if (Input.GetKeyDown(KeyCode.Space))
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
    }

    void StartComboWindowCheck()
    {
        float comboWindow = 1;
        _comboWindowTimer += Time.deltaTime;

        if (_comboWindowTimer >= comboWindow)
        {
            _comboWindowTimer = 0;
            _lastUsedInputs.Clear();

            //Debug.LogError("Combo Broken");
        }
    }
}
