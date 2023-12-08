using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObject : MonoBehaviour, IDamagable
{
    [Header("Settings")]
    [SerializeField] private GameObject referencePrefab;
    [SerializeField] private GameObject destroyedPrefab;
    [SerializeField] private float removeDelay = 10;
    [SerializeField] private float disablePhysicsDelay = 4;

    [Header("Debug")]
    [SerializeField] private float hp = 20;

    Rigidbody[] rbsFragments;
    MeshCollider[] mcFragments;

    void Awake()
    {
        referencePrefab.SetActive(true);
        destroyedPrefab.SetActive(false);
        rbsFragments = destroyedPrefab.GetComponentsInChildren<Rigidbody>();
        mcFragments = destroyedPrefab.GetComponentsInChildren<MeshCollider>();
    }

    // Remove Update() when not debugging!
    //bool performed = false;
    //private void Update()
    //{
    //    if (hp <= 0 && !performed)
    //    {
    //        performed = true;
    //        DestroyObjectWithDelay();
    //        StartCoroutine(DisablePhysicsOnDelay());
    //    }
    //}

    /// <summary>
    /// IDamageable interface method
    /// </summary>
    /// <param name="attack"> Retrieve damage number from attack object </param>
    public void TakeDamage(Attack attack)
    {
        Debug.Log("Damage: " + attack.Damage);
        ReceiveDamage(attack.Damage);
    }

    public void ReceiveDamage(float damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            DestroyObjectWithDelay();
            StartCoroutine(DisablePhysicsOnDelay());
        }
    }

    private void DestroyObjectWithDelay()
    {
        referencePrefab.SetActive(false);
        destroyedPrefab.SetActive(true);
        Destroy(gameObject, removeDelay);
    }

    IEnumerator DisablePhysicsOnDelay()
    {
        yield return new WaitForSeconds(disablePhysicsDelay);
        Debug.Log("Disable physics");

        foreach (Rigidbody rb in rbsFragments)
            Destroy(rb);

        foreach(MeshCollider coll in mcFragments)
            Destroy(coll);
    }

}

