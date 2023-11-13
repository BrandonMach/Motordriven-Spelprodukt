using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{  
    [SerializeField] private float _maxLifeTime;
    [SerializeField] private float _arrowSpeed;

    private Transform _startPos;
    //private Transform _firePos;
    private Transform defaultParent;
    private Attack _attack;
    private float _timeSinceFire;
    private Rigidbody _rb;
    private bool _fired;
    private Vector3 fireDirection;
   


    void Start()
    {
        _startPos = transform;
        defaultParent = transform.parent;
        _rb = GetComponent<Rigidbody>();
        //_rb.useGravity = false;
    }


    void Update()
    {
        if (_fired)
        {
            
            MoveArrow();
            _timeSinceFire += Time.deltaTime;

            if (_timeSinceFire >= _maxLifeTime)
            {
                //transform.SetPositionAndRotation(_startPos.position, _startPos.rotation);
                _fired = false;
                //transform.parent = defaultParent;
            }
        }
    }


    private void MoveArrow()
    {
        //transform.Translate(Vector3.forward * _arrowSpeed * Time.deltaTime);
        //_rb.AddForce(transform.forward *  _arrowSpeed, ForceMode.Impulse);
        _rb.velocity = fireDirection * _arrowSpeed;
    }


    public void FireArrow(Attack attack, Transform firePos, Vector3 direction)
    {
        this._attack = attack;
        transform.position = firePos.position;
        transform.rotation = firePos.rotation;
        fireDirection = direction;
        _rb.isKinematic = false;
        //_rb.useGravity = true;
        _fired = true;
        transform.SetParent(null);
        //transform.SetPositionAndRotation(pos, rot);
        //transform.localScale = scale;
        //transform.parent = null;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            Player.Instance.TakeDamage(_attack);
            _rb.isKinematic = true;
            transform.SetParent(other.transform);
        }
    }


}
