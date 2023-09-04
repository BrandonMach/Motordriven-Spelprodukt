using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MOBAMovmentTest : MonoBehaviour
{
    
    NavMeshAgent _agent;

    public float rotateSpeedMovement = 0.1f;
    float _rotateVelocity;


    void Start()
    {
        _agent = gameObject.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit _hit;

            //Checking if the raycast shot hits smoething thet uses the navmesg system 

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out _hit, Mathf.Infinity))
            {
                //Have the agent move to the raycast/ hit point
                _agent.SetDestination(_hit.point);

                //Rotation

                Quaternion _rotationToLookAt = Quaternion.LookRotation(_hit.point - transform.position);
                float _rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y, _rotationToLookAt.eulerAngles.y, ref _rotateVelocity, rotateSpeedMovement * (Time.deltaTime * 5));

                transform.eulerAngles = new Vector3(0, _rotationY, 0);
            }
        }
    }
}
