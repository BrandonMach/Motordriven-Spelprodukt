using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DismemberentEnemyScript : MonoBehaviour
{

    Animator _anim;

    [SerializeField] List<Rigidbody> _ragdollRigids;
    [SerializeField] List<DismembermentLimbsScript> _limbs;
    void Start()
    {
        _anim = GetComponent<Animator>();
        _ragdollRigids = new List<Rigidbody>(transform.GetComponentsInChildren <Rigidbody>());
        _ragdollRigids.Remove(GetComponent<Rigidbody>());

        DeactivateRagdoll();
    }

    // Update is called once per frame
    void Update()
    {
        //För testing
        if (Input.GetKeyDown(KeyCode.N))
        {
            _limbs[Random.Range(0, _limbs.Count)].GetHit();
        }
    }

    void ActivateRagdoll()
    {
        _anim.enabled = false;
        foreach (var ragdollparts in _ragdollRigids)
        {
            ragdollparts.useGravity = true;
            ragdollparts.isKinematic = false;
        }
    }

    void DeactivateRagdoll()
    {
        _anim.enabled = true;
        foreach (var ragdollparts in _ragdollRigids)
        {
            ragdollparts.useGravity = false;
            ragdollparts.isKinematic = true;
        }
    }

    public void GetKilled()
    {
        ActivateRagdoll();
    }
}
