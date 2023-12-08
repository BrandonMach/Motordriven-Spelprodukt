using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObject : MonoBehaviour, IDamagable
{
    [Header("Settings")]
    [SerializeField] private GameObject referencePrefab;
    [SerializeField] private GameObject destroyedPrefab;
    [SerializeField] private float removeDelay = 10;

    [Header("Debug")]
    [SerializeField] private float hp = 20;

    void Awake()
    {
        referencePrefab.SetActive(true);
        destroyedPrefab.SetActive(false);
    }

    public void ReceiveDamage(float damage)
    {
        hp -= damage;
        if (hp <= 0) DestroyObjectWithDelay();
    }

    private void DestroyObjectWithDelay()
    {
        referencePrefab.SetActive(false);
        destroyedPrefab.SetActive(true);
        Destroy(gameObject, removeDelay);
    }

    public void TakeDamage(Attack attack)
    {
        hp -= attack.Damage;
        if (hp <= 0) DestroyObjectWithDelay();
    }
}

