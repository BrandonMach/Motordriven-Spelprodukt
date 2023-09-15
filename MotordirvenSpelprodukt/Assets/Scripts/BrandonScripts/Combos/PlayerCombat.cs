using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class PlayerCombat : MonoBehaviour
{

    //public List<AttackSO> Combo;
    public List<TestWeapon> WeaponAnimation;
    [SerializeField] TestWeapon _currentWeaponType;
    private int _weaponTypeIndex = 0;
    [SerializeField] private TextMeshProUGUI _text;

    float _lastClickedTime;
    float _lastComboEnd;
    int _comboCounter;

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
    KeyCode _lightAttack;
    KeyCode _heavyAttack;
    KeyCode _specialAttack;

    EntertainmentManager _etpManager;
    public List<KeyCode> _lastUsedInputs = new List<KeyCode>();

    string ComboTree;




    void Start()
    {
        _etpManager = GameObject.Find("Canvas").GetComponent<EntertainmentManager>();
        og = _anim;



        //Combos



        for (int i = 0; i < ComboList.Count; i++)
        {
            for (int j = 0; j < ComboList[i]._buttonSequence.Count; j++)
            {
                //Debug.Log(ComboList[i]._buttonSequence[j]);
                ComboTree += ", " + ComboList[i]._buttonSequence[j].ToString();
            }

            _comboTreeInfoText.text += "Combo " +(i+1)+": " + (ComboTree.Remove(0, 1)) + "\n";
            ComboTree = "";

        }

    }

    // Update is called once per frame
    void Update()
    {
        //Temporary switch weapon and different combo animation should play. Animation is changed in WeaponAnimtion
        ChangeWeaponAnimaitonType();

        _text.text = WeaponAnimation[_weaponTypeIndex].WeaponTypeName; //Display weapon name
        _currentWeaponType = WeaponAnimation[_weaponTypeIndex];




        for (int i = 0; i < _attackInputs.Length; i++)
        {
            if (Input.GetKeyDown(_attackInputs[i])) //Checks if attack input has been pressed
            {

                //_comboCounter = _lastUsedInputs.Count;
                //Attack();

                _startComboWindowTimer = true;
                _comboWindowTimer = 0;


                if (_lastUsedInputs.Count == 3) //remove first inputed key, Only 3 input lenght Combos
                {
                    _lastUsedInputs.RemoveAt(0);
                }
                _lastUsedInputs.Add(_attackInputs[i]); //add latest key


                KeyCode[] comboAttempt = _lastUsedInputs.ToArray();



                for (int j = 0; j < ComboList.Count; j++)
                {
                    if (Enumerable.SequenceEqual(ComboList[j]._buttonSequence, comboAttempt))
                    {



                        Debug.LogError("Combo matched");
                        _etpManager.increaseETP(15);
                    }
                }
            }
        }

        

        if (_startComboWindowTimer)
        {
            StartComboWindowCheck();
        }


        if (Input.GetKeyDown(KeyCode.L))
        {
            Attack();
        }







        ExitAttack();
    }

    void Attack()
    {

        if(Time.time - _lastComboEnd > 0.5f && _comboCounter <= WeaponAnimation[_weaponTypeIndex].AnimationType.Count)
        {
            CancelInvoke("EndCombo");

            if(Time.time - _lastClickedTime >= 0.2f)
            {
                _anim.runtimeAnimatorController = WeaponAnimation[_weaponTypeIndex].AnimationType[_comboCounter].AnimatorOV;
                _anim.Play("Attack", 3,0);
                //Damage
                //Knockback
                //VFX

                _comboCounter++;
                _lastClickedTime = Time.time;

                if(_comboCounter +1> WeaponAnimation[_weaponTypeIndex].AnimationType.Count)
                {
                    _comboCounter = 0;
                }
            }
        }
    }

    void ExitAttack() //Checksi if end of animation 
    {
        if(_anim.GetCurrentAnimatorStateInfo(3).normalizedTime > 0.9f && _anim.GetCurrentAnimatorStateInfo(3).IsTag("Attack"))
        {
            Invoke("EndCombo", 1);
        }
    }   
    void EndCombo()
    {
        _comboCounter = 0;
        _lastComboEnd = Time.time;
    }

    void ChangeWeaponAnimaitonType()
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
