using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class DismemberentEnemyScript : MonoBehaviour
{

    Animator _anim;

    [SerializeField] List<Rigidbody> _ragdollRigids;
    [SerializeField] List<DismembermentLimbsScript> _limbs;
    [SerializeField] Collider _mainCollider;
    [SerializeField] Rigidbody _rb;

    void Start()
    {
        _anim = GetComponent<Animator>();
        _ragdollRigids = new List<Rigidbody>(transform.GetComponentsInChildren <Rigidbody>());
        _ragdollRigids.Remove(GetComponent<Rigidbody>());
        _rb = GetComponent<Rigidbody>();
        DeactivateRagdoll();

        
    }

    // Update is called once per frame
    void Update()
    {
        //För testing
        //if (Input.GetKeyDown(KeyCode.N))
        //{
        //    //_limbs[Random.Range(0, _limbs.Count)].Dismember();
        //    //ActivateRagdoll();

        //    DismemberCharacter();d

        //}
    }

    public void DismemberCharacter()
    {
        _limbs[Random.Range(0, _limbs.Count)].Dismember();
        ActivateRagdoll();
    }

    void ActivateRagdoll()
    {
        //Add random force to ragdoll
        // ragdollparts.AddForce(new Vector3(Random.Range(-180, 180), Random.Range(-180, 180), Random.Range(-180, 180)) * Random.Range(1, 10));
        if (gameObject.GetComponent<NavMeshAgent>() != null)
        {
            GetComponent<NavMeshAgent>().enabled = false;
        }
        _mainCollider.enabled = false;
        _anim.enabled = false;


       
        

        foreach (var ragdollparts in _ragdollRigids)
        {
            ragdollparts.gameObject.GetComponent<Collider>().enabled = true;
            ragdollparts.useGravity = true;
            ragdollparts.isKinematic = false; //Unlocks ragdoll for rigidbody

            ragdollparts.angularVelocity = Vector3.zero;
            //Add random force to ragdoll
            //ragdollparts.AddForce(new Vector3(Random.Range(-180, 180), Random.Range(-180, 180), Random.Range(-180, 180)) * Random.Range(1, 10));
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
}
