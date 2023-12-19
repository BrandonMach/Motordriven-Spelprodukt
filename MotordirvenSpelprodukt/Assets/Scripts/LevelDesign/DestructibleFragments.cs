using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleFragments : MonoBehaviour
{
    [SerializeField] float delayColliderActivations = 1f;
    MeshCollider[] mcs;
    Rigidbody[] rbs;

    bool activatedOnce;
    // Start is called before the first frame update
    void Start()
    {
        mcs = GetComponentsInChildren<MeshCollider>();
        rbs = GetComponentsInChildren<Rigidbody>();
        activatedOnce = false;
        DisablePhysics();
    }

    private void Update()
    {
        if (activatedOnce) return;
        delayColliderActivations -= Time.deltaTime;

        if (delayColliderActivations < 0)
        {
            activatedOnce = true;

            foreach (var c in mcs)
                c.enabled = true;

            foreach (var rb in rbs)
                rb.useGravity = true;
        }
    }

    private void DisablePhysics()
    {
        foreach (var c in mcs)
            c.enabled = false;

        foreach (var rb in rbs)
            rb.useGravity = false;
    }
}
