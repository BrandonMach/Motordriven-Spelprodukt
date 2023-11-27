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
    public GameObject IndicatorPrefab;
    private GameObject _indicator;
    [SerializeField] GameObject _splashEffects;
    

    public LayerMask Ground;



    public float MinDistance = 0.2f;
    public float MaxDistance = 0.4f;
    public float MaxForce = 0.1f;

    

    Vector3 _targetIndicatorPosition;
    Vector3 _playerPosition;

    public float _speed = 0.1f;

    // Time when the movement started.
    private float _startTime;
   // private Vector3 _startPos;
    public  float _journeyLength;
    public float _distCovered;
    private bool isFalling;

    void Start()
    {
        _playerPosition = Player.Instance.transform.position;
 
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit,Ground))
        {
            //Arena area
            float posX = Random.Range(-2, 2);
            float posZ = Random.Range(-2, 2);


            _targetIndicatorPosition = new Vector3(/*posX +*/ _playerPosition.x, 0.001f, /*posZ +*/ _playerPosition.z);
            _indicator = Instantiate(IndicatorPrefab, _targetIndicatorPosition, IndicatorPrefab.transform.rotation);
            //_indicator.transform.SetParent(this.transform);
        }

        _startTime = Time.time;

        float throwPosX = Random.Range(-10, 10);
        float throwPosZ = Random.Range(-10, 10);
        //_startPos = new Vector3(throwPosX, 10, throwPosZ);

        //transform.position= _startPos; 
        _journeyLength = Vector3.Distance(transform.position, _targetIndicatorPosition);
        isFalling = true;


        
    }

    // Update is called once per frame
    void Update()
    {
        _distCovered = (Time.time - _startTime) * _speed;

        //Fraction of journey completed equals current distance divided by total distance.
        float fractionOfJourney = _distCovered / _journeyLength;

        
        transform.position = Vector3.Lerp(transform.position, _targetIndicatorPosition, fractionOfJourney);




        //if (isFalling)
        //{

        //    float distanceCovered = (Time.time - _startTime) * _speed;
        //    float journeyFraction = distanceCovered / _journeyLength;

        //    Vector3 currentPos = Vector3.Lerp(this.transform.position, _targetIndicatorPosition, journeyFraction);

        //    currentPos.y = Mathf.Lerp(this.transform.position.y, _targetIndicatorPosition.y, journeyFraction) /*+ Mathf.Sin(journeyFraction * Mathf.PI) * 2*/;
        //    if (currentPos.x != _targetIndicatorPosition.x && currentPos.z != _targetIndicatorPosition.x)
        //    {
        //        currentPos.y = Mathf.Lerp(this.transform.position.y, _targetIndicatorPosition.y + 0.01f, journeyFraction) /*+ Mathf.Sin(journeyFraction * Mathf.PI) * 2*/;
        //    }

        //    transform.position = currentPos;

        //    if (journeyFraction >= 1.0f)
        //    {

        //        isFalling = false;
        //        //Object has reached its destination
        //        //You can add additional logic here if needed
        //    }
        //    else
        //    {


        //    }


        //}



        if (Type == ObjectType.HealthPotion)
        {
           // HoverObject();
            //float y = Mathf.PingPong(Time.time * 2, 1) * 6 - 3;
            //transform.position = new Vector3(transform.position.x, y, transform.position.z);
        }
        

        
    }


   

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == ("Player"))
        {
            if (Type == ObjectType.Tomato)
            {
                Destroy(gameObject);
                Debug.Log("Tomato");
                Debug.LogError("Player take damage");
                other.gameObject.GetComponent<HealthManager>().ReduceHealth(5);
                Destroy(_indicator);

            }
            else if (Type == ObjectType.HealthPotion)
            {
                Debug.Log("Heal Player");
                other.gameObject.GetComponent<HealthManager>().HealDamage(20);
                Destroy(this.gameObject);
            }
            else if (Type == ObjectType.CannonBall)
            {
                Debug.Log("Cannonball");
            }
        }



        if(other.gameObject.tag == ("Ground") || other.gameObject.tag == ("EnemyTesting"))
        {
            if (Type == ObjectType.Tomato)
            {
                
                Debug.Log("Tomato Splash");

                Instantiate(_splashEffects, transform.position, _splashEffects.transform.rotation);

                Destroy(gameObject);
                Destroy(_indicator);

            }
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Kanske ta bort target
        if (other.gameObject.tag == ("Target"))
        {
            if (Type == ObjectType.Tomato)
            {

                Debug.Log("Tomato Splash");

                Instantiate(_splashEffects, transform.position, _splashEffects.transform.rotation);

                Destroy(gameObject);
                Destroy(_indicator);

            }
        }
    }


    void HoverObject()
    {


        //float y = Mathf.PingPong(Time.time * 2, 1) * 6 - 3;
        //transform.position = new Vector3(transform.position.x, y, transform.position.z);

        //float distance = RaycastDownwardsFromMe();

        //float fractionalPosition = (MaxDistance - distance) / (MaxDistance - MinDistance);
        //if (fractionalPosition < 0) fractionalPosition = 0;
        //if (fractionalPosition > 1) fractionalPosition = 1;
        //float force = fractionalPosition * MaxForce;

        //rb.AddForceAtPosition(Vector3.up * force, transform.position);
    }

    float RaycastDownwardsFromMe()
    {
        RaycastHit rch;
        if (Physics.Raycast(transform.position, -transform.up, out rch, MaxDistance))
        {
            IndicatorPrefab.transform.position = rch.transform.position;
            
            return rch.distance;
        }

        // report no contact
       // contactTracker.ReportContactState(this, false);

        return 100;
    }
}
