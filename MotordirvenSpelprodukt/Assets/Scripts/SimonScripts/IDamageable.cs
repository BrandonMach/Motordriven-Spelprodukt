using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    public void TakeDamage(float damage);
    public void GetStunned(float stunDuration);
    public void Die();
}
