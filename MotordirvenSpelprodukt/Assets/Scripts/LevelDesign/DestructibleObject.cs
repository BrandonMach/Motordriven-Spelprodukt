using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObject : MonoBehaviour, IDamagable
{
    [Header("Settings")]
    [SerializeField] private GameObject referencePrefab;
    [SerializeField] private GameObject destroyedPrefab;
    [SerializeField] private float removeDelay = 10;
    [SerializeField] private float removePhysicsDelay = 4;

    [Header("Debug")]
    [SerializeField] private float hp = 20;

    Rigidbody[] rbsFragments;
    MeshCollider[] mcFragments;

    void Awake()
    {
        referencePrefab.SetActive(true);
        destroyedPrefab.SetActive(false);

        rbsFragments = GetComponentsInChildren<Rigidbody>();
        mcFragments = GetComponentsInChildren<MeshCollider>();
    }

    // ONLY FOR DEBUG PURPOSES
    //public void Update()
    //{
    //    if (hp <= 0)
    //    {
    //        DestroyObjectWithDelay();
    //        StartCoroutine(DisablePhysicsWithDelay());
    //    }
    //}


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
        if (hp <= 0)
        {
            DestroyObjectWithDelay();
            StartCoroutine(DisablePhysicsWithDelay());
        }
    }

    IEnumerator DisablePhysicsWithDelay()
    {
        yield return new WaitForSeconds(removePhysicsDelay);
        Debug.Log("Removing physics from destructibles");

        foreach (Rigidbody rb in rbsFragments)
            Destroy(rb);

        foreach(MeshCollider coll in mcFragments)
            Destroy(coll);
    }
}

