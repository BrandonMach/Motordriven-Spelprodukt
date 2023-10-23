using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    public void TakeDamage(Attack attack);
    //public void GetStunned(float stunDuration);
    //public void Die();
    //public void HealDamage(float damageHealed);

    //public void KnockUp(float force);

    ///// <summary>
    ///// Calculates the direction of the knockback and applies an impulse to the target rigidbody
    ///// </summary>
    //public void Knockback(Vector3 attackerPos, float knockBackForce);
}
