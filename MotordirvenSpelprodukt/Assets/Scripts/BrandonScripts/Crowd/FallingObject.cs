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

    [SerializeField] [Range(0, 1f)] private float lerpPct = 0.5f;

    void Start()
    {
        rb = GetComponentInParent<Rigidbody>();
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit,Ground))
        {

            int pos = Random.Range(0, 10);
            _targetPosition = new Vector3(pos, 0.001f, pos);
            Instantiate(Indicator, _targetPosition, Indicator.transform.rotation);
        }

        transform.position = new Vector3(15, 15, 0);

    }

    // Update is called once per frame
    void Update()
    {


        transform.position = Vector3.Lerp(transform.position, (_targetPosition), Time.deltaTime);
        //transform.rotation = Vector3.Lerp(transform.position, _targetPosition, lerpPct);

        if(Type == ObjectType.HealthPotion)
        {
            HoverObject();
        }
        


    }


    private void OnCollisionEnter(Collision collision)
    {

        Debug.Log("Landar" + transform.position);
        if (collision.gameObject.tag == ("Player"))
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
