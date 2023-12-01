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

    [SerializeField] TrailRenderer trailRenderer;
    bool didDamage;

    void Start()
    {
        _startPos = transform;
        defaultParent = transform.parent;
        _rb = GetComponent<Rigidbody>();
        _rb.isKinematic = false;
        //_rb.useGravity = false;
        Player.Instance.StartEvade += Player_OnEvade;
        trailRenderer.enabled = false;
        didDamage = false;
    }

    private void Player_OnEvade(object sender, System.EventArgs e)
    {
        
    }

    void Update()
    {
        if (_fired)
        {
           
            MoveArrow();
            _timeSinceFire += Time.deltaTime;

            if (_timeSinceFire >= _maxLifeTime)
            {
                
                _rb.isKinematic = false;
                transform.SetPositionAndRotation(_startPos.position, _startPos.rotation);
                _fired = false;
               
                transform.parent = defaultParent;
                //transform.position = defaultParent.position;
                _timeSinceFire = 0;
            }
        }
    }


    private void ResetArrow()
    {

    }


    private void MoveArrow()
    {
        
        //transform.Translate(Vector3.forward * _arrowSpeed * Time.deltaTime);
        //_rb.AddForce(transform.forward *  _arrowSpeed, ForceMode.Impulse);
        if (_rb.isKinematic == false)
        {
            _rb.velocity = transform.up * _arrowSpeed;
            trailRenderer.enabled = true;
        }
 
        //_rb.AddTorque(transform.up * _arrowSpeed*5);
    }


    public void FireArrow(Attack attack, Transform firePos, Vector3 direction)
    {
        this._attack = attack;
        transform.position = firePos.position;
        Quaternion desiredRot = Quaternion.Euler(90, 0, 0);
        transform.rotation = firePos.rotation * desiredRot;
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
        if (other.gameObject == Player.Instance.gameObject)
        {
            _rb.isKinematic = true;
            transform.SetParent(other.transform);
            trailRenderer.enabled = false;

            if (!didDamage)
            {
                didDamage = true;
                Player.Instance.TakeDamage(_attack);
            }
           
            
        }
    }


}
