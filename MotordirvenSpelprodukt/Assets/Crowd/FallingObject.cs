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
 
    [SerializeField] GameObject _healthpack;
    

    public LayerMask Ground;



    public float MinDistance = 0.2f;
    public float MaxDistance = 0.4f;
    public float MaxForce = 0.1f;

    

    Vector3 _targetPosition;
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


            _targetPosition = new Vector3(posX + _playerPosition.x, -1.001f, /*posZ +*/ posZ + _playerPosition.z);
           // _indicator = Instantiate(IndicatorPrefab, _targetIndicatorPosition, IndicatorPrefab.transform.rotation);
            //_indicator.transform.SetParent(this.transform);
        }

        _startTime = Time.time;

        float throwPosX = Random.Range(-10, 10);
        float throwPosZ = Random.Range(-10, 10);
      

        //transform.position= _startPos; 

      

        _journeyLength = Vector3.Distance(transform.position, _targetPosition);
        isFalling = true;


        
    }

    // Update is called once per frame
    void Update()
    {
        _distCovered = (Time.time - _startTime) * _speed;

        //Fraction of journey completed equals current distance divided by total distance.
        float fractionOfJourney = _distCovered / _journeyLength;

        
        transform.position = Vector3.Lerp(transform.position, _targetPosition, fractionOfJourney);


        
        
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

                Destroy(gameObject);
               
            }
            if (Type == ObjectType.HealthPotion)
            {
                Destroy(gameObject);
                Instantiate(_healthpack, gameObject.transform.position + new  Vector3(0,2,0), Quaternion.identity);

            }
            
        }
    }

   
}
