using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class FallingObjectType : MonoBehaviour
{
    // Start is called before the first frame update
    public enum ObjectType
    {
        Tomato,
        HealthPotion,
        CannonBall
    }

    public ObjectType Type;
    public GameObject Indicator;
    

    public LayerMask Ground;



    public float MinDistance = 0.2f;
    public float MaxDistance = 0.4f;
    public float MaxForce = 0.1f;

    Rigidbody rb;

    Vector3 _targetPosition;

    public float _speed = 1.0f;

    // Time when the movement started.
    private float _startTime;
    private Vector3 _startPos;
    public  float _journeyLength;
    public float _distCovered;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit,Ground))
        {
            //Arena area
            float posX = Random.Range(-10, 10);
            float posZ = Random.Range(-10, 10);


            _targetPosition = new Vector3(posX, 0.001f, posZ);
            Instantiate(Indicator, _targetPosition, Indicator.transform.rotation);
        }

        _startTime = Time.time;

        float throwPosX = Random.Range(-10, 10);
        float throwPosZ = Random.Range(-10, 10);
        _startPos = new Vector3(throwPosX, 10, throwPosZ);

        transform.position= _startPos; 
        _journeyLength = Vector3.Distance(_startPos, _targetPosition);
    }

    // Update is called once per frame
    void Update()
    {
        _distCovered = (Time.time - _startTime) * _speed;
       
        // Fraction of journey completed equals current distance divided by total distance.
        float fractionOfJourney = _distCovered / _journeyLength;

        transform.position = Vector3.Lerp(_startPos, _targetPosition, fractionOfJourney);
      
        
        if(Type == ObjectType.HealthPotion)
        {
            HoverObject();
        }
        

        
    }


    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == ("Player"))
        {
            if (Type == ObjectType.Tomato)
            {
                Debug.Log("Tomato");   
            }
            else if (Type == ObjectType.HealthPotion)
            {
                Debug.Log("Heal Player");
                Destroy(this.gameObject);
            }
            else if (Type == ObjectType.CannonBall)
            {
                Debug.Log("Cannonball");
            }
        }
        

        

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Target")
        {
            if (Type == ObjectType.HealthPotion)
            {
                HoverObject();
                Destroy(other.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
                Destroy(other.gameObject);
            }
            
        }
    }


    void HoverObject()
    {
        float distance = RaycastDownwardsFromMe();

        float fractionalPosition = (MaxDistance - distance) / (MaxDistance - MinDistance);
        if (fractionalPosition < 0) fractionalPosition = 0;
        if (fractionalPosition > 1) fractionalPosition = 1;
        float force = fractionalPosition * MaxForce;

        rb.AddForceAtPosition(Vector3.up * force, transform.position);
    }

    float RaycastDownwardsFromMe()
    {
        RaycastHit rch;
        if (Physics.Raycast(transform.position, -transform.up, out rch, MaxDistance))
        {
            Indicator.transform.position = rch.transform.position;
            
            return rch.distance;
        }

        // report no contact
       // contactTracker.ReportContactState(this, false);

        return 100;
    }
}
