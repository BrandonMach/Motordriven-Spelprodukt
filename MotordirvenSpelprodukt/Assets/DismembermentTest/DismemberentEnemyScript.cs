using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DismemberentEnemyScript : MonoBehaviour
{

    Animator _anim;

    [SerializeField] List<Rigidbody> _ragdollRigids;
    [SerializeField] List<DismembermentLimbsScript> _limbs;
    [SerializeField] BoxCollider _mainCollider;
    [SerializeField] Rigidbody _rb;
    void Start()
    {
        _anim = GetComponent<Animator>();
        _ragdollRigids = new List<Rigidbody>(transform.GetComponentsInChildren <Rigidbody>());
        _ragdollRigids.Remove(GetComponent<Rigidbody>());
        _rb = GetComponent<Rigidbody>();
        DeactivateRagdoll();

        DeactivateRagdoll();
    }

    // Update is called once per frame
    void Update()
    {
        //För testing
        //if (Input.GetKeyDown(KeyCode.N))
        //{  
        //    _limbs[Random.Range(0, _limbs.Count)].Dismember();
        //    ActivateRagdoll();
        //}
    }

    public void PlayerDismember()
    {
        _limbs[Random.Range(0, _limbs.Count)].Dismember();
        ActivateRagdoll();
    }

    void ActivateRagdoll()
    {
        _mainCollider.enabled = false;
        _anim.enabled = false;
        foreach (var ragdollparts in _ragdollRigids)
        {
            ragdollparts.gameObject.GetComponent<Collider>().enabled = true;
            ragdollparts.useGravity = true;
            ragdollparts.isKinematic = false; //Unlocks ragdoll for rigidbody

            //Add random force to ragdoll
            ragdollparts.AddForce(new Vector3(Random.Range(-180, 180), Random.Range(-180, 180), Random.Range(-180, 180)) * Random.Range(1, 10));
        }
    }

    void DeactivateRagdoll()
    {
        _anim.enabled = true;
        foreach (var ragdollparts in _ragdollRigids)
        {
            ragdollparts.gameObject.GetComponent<Collider>().enabled = false;
            ragdollparts.useGravity = false;
            ragdollparts.isKinematic = true;
        }
    }

    public void GetKilled()
    {
        _limbs[Random.Range(0, _limbs.Count)].Dismember();
        ActivateRagdoll();
    }
}
