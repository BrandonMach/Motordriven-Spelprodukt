using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class PlayerCombatAnimations : MonoBehaviour
{

    float _lastClickedTime;
    float _lastComboEnd;
    public int _comboCounter;

    [SerializeField] Animator _anim;
    private int _animLayer;
   

    EntertainmentManager _etpManager;





    void Start()
    {
        _etpManager = GameObject.Find("Canvas").GetComponent<EntertainmentManager>();
        _animLayer = _anim.GetLayerIndex("Attack");

        

    }

    // Update is called once per frame
    void Update()
    {
        //Temporary switch weapon and different combo animation should play. Animation is changed in WeaponAnimtion
        
        ExitAttack();


    }
    //void Attack(int attackType)
    //{

    //    if (Time.time - _lastComboEnd > 0.5f && _comboCounter <= _setWeaponTypeAnimations.Count)
    //    {
    //        CancelInvoke("EndCombo");

    //        if (Time.time - _lastClickedTime >= 0.2f)
    //        {
    //            _anim.runtimeAnimatorController = _setWeaponTypeAnimations[_comboCounter].AnimatorOV; //Override the animation controller based on how far into te combo you are.
    //            _anim.Play("Attack", _animLayer, 0);
    //            //Damage
    //            //Knockback
    //            //VFX

    //            //_comboCounter++;
    //            _lastClickedTime = Time.time;

    //            if (_comboCounter + 1 > _setWeaponTypeAnimations.Count)
    //            {
    //                _comboCounter = 0;
    //            }
    //        }
    //    }
    //}

    void ExitAttack() //Checksi if end of animation 
    {
        if (_anim.GetCurrentAnimatorStateInfo(_animLayer).normalizedTime > 0.9f && _anim.GetCurrentAnimatorStateInfo(_animLayer).IsTag("Attack"))
        {
            Invoke("EndCombo", 1);
        }
    }
    void EndCombo()
    {
        _comboCounter = 0;
        _lastComboEnd = Time.time;
    }
}
