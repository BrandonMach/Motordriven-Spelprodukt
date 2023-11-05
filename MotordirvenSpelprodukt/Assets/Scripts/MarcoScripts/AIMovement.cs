using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AIMovement : MonoBehaviour
{

    public bool GoTowardsPlayer = false;

    private Rigidbody _rigidbody;
    private EnemyScript _enemyScript;
    // Start is called before the first frame update

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _enemyScript = GetComponent<EnemyScript>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (GoTowardsPlayer)
        {
            _rigidbody.velocity = new Vector3(transform.forward.x * _enemyScript.MovementSpeed, _rigidbody.velocity.y, transform.forward.z * _enemyScript.MovementSpeed);
        }
    }
}
