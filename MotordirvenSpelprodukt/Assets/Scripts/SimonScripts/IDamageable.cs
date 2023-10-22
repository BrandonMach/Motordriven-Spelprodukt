using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    public void TakeDamage(float damage);
    public void GetStunned(float stunDuration);
    public void Die();
    public void HealDamage(float damageHealed);

    /// <summary>
    /// Calculates the direction of the knockback and applies an impulse to the target rigidbody
    /// </summary>
    public void Knockback(Vector3 attackerPos, Vector3 receiverPos, float knockBackForce);
}
