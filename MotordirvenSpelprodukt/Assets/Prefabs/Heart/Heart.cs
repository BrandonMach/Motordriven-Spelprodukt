using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    private void Update()
    {
        Physics.IgnoreLayerCollision(7,7);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Player.Instance.gameObject)
        {
            other.gameObject.GetComponent<HealthManager>().HealDamage(20);
            Destroy(gameObject);
        }
    }
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject == Player.Instance.gameObject)
    //    {
    //        collision.gameObject.GetComponent<HealthManager>().HealDamage(20);
    //        Destroy(gameObject);
    //    }
    //}
}
