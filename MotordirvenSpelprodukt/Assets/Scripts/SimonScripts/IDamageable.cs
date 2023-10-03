using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    public void TakeDamage(float damage);
    public void GetStunned(float stunDuration);
    public void Die();
    public void HealDamage(float damageHealed);
    public void Knockback(Vector3 hitDirection, float knockBackForce);
}
