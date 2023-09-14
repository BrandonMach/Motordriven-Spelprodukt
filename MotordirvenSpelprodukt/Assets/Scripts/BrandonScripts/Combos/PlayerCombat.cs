using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    public List<AttackSO> Combo;
    float _lastClickedTime;
    float _lastComboEnd;
    int _comboCounter;

    [SerializeField] Animator _anim;
    Animator og;
    //Weapon

    void Start()
    {
        //_anim = GetComponent<Animator>();
        og = _anim;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.L))
        {
            Attack();
        }

        ExitAttack();
    }

    void Attack()
    {
        if(Time.time - _lastComboEnd > 0.5f && _comboCounter <= Combo.Count)
        {
            CancelInvoke("EndCombo");

            if(Time.time - _lastClickedTime >= 0.2f)
            {
                _anim.runtimeAnimatorController = Combo[_comboCounter].AnimatorOV;
                _anim.Play("Attack", 2,0);
                //Damage
                //Knockback
                //VFX

                _comboCounter++;
                _lastClickedTime = Time.time;

                if(_comboCounter +1 > Combo.Count)
                {
                    _comboCounter = 0;
                }
            }
        }
    }

    void ExitAttack() //Checksi if end of animation 
    {
        if(_anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f && _anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            Invoke("EndCombo", 1);
        }
    }   
    void EndCombo()
    {
        _comboCounter = 0;
        _lastComboEnd = Time.time;
        _anim.runtimeAnimatorController = og.runtimeAnimatorController;
    }
}
