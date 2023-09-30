using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMPScript : EnemyScript
{
    

    public float DamageBuff;

    public float ChaseDistance;

    public Animator Anim;

    public int AttackIndex;

    public bool CanAttack;
    public bool StopChase;
    public bool AnimationPlaying = false;

    RuntimeAnimatorController ac;

    void Start()
    {
        _movementSpeed = 10;
        _attackRange = 4;
        _attackCooldown = 2;
        ChaseDistance = 4;
        _currentHealth = 100;
        _maxHealth = 100;
        CanAttack = true;

        ac = Anim.runtimeAnimatorController;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float GetAnimationLenght(string AnimationName)
    {
        for (int i = 0; i < ac.animationClips.Length; i++)
        {
            Debug.LogError(ac.animationClips[i].name);

            if(ac.animationClips[i].name == AnimationName)
            {
                return ac.animationClips[i].length;
            }
        }

        return float.NaN;
    }
}
