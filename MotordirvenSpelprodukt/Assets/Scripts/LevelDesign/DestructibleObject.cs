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
    [SerializeField] private float destructionForce = 5f;

    [Header("Debug")]
    [SerializeField] private float hp = 150;

    Rigidbody[] rbsFragments;
    MeshCollider[] mcFragments;

    private bool _destroyed = false;

    void Awake()
    {
        referencePrefab.SetActive(true);
        destroyedPrefab.SetActive(false);

        rbsFragments = destroyedPrefab.GetComponentsInChildren<Rigidbody>();
        mcFragments = destroyedPrefab.GetComponentsInChildren<MeshCollider>();
        Debug.LogWarning(rbsFragments.Length);
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

    private void DestroyObjectWithDelay(Vector3 force)
    {
        referencePrefab.SetActive(false);
        destroyedPrefab.SetActive(true);
        ApplyForce(force);
        Destroy(gameObject, removeDelay);
    }

    public void TakeDamage(Attack attack)
    {
        hp -= attack.Damage;
        Vector3 forceDirection = Vector3.Normalize(transform.position - attack.AttackerPosition);

        if (hp <= 0)
        {
            // Destroy game object
            DestroyObjectWithDelay(forceDirection);
            StartCoroutine(DisablePhysicsWithDelay());
        }
    }

    void ApplyForce(Vector3 direction)
    {
        foreach (Rigidbody rb in rbsFragments)
            rb.velocity = direction*destructionForce;

        Debug.LogWarning(direction);
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out MMScript minion) && !_destroyed)
        {
            if (minion.CurrentState == MMScript.EnemyState.pushed)
            {
                Vector3 direction = Vector3.Normalize(transform.position - minion.transform.position);
                DestroyObjectWithDelay(direction);
                StartCoroutine(DisablePhysicsWithDelay());
            }
        }
        else if (other.TryGetComponent(out Player player) && !_destroyed)
        {
            if (player.CurrentPlayerState == Player.PlayerState.pushedBack)
            {
                Vector3 direction = Vector3.Normalize(transform.position - player.transform.position);
                DestroyObjectWithDelay(direction);
                StartCoroutine(DisablePhysicsWithDelay());
            }
        }
    }
}

