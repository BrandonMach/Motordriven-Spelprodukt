using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("HUH?");
            HealthManager manager = other.gameObject.GetComponent<HealthManager>();

            if (manager != null)
                manager.Die();
        }
    }
}
