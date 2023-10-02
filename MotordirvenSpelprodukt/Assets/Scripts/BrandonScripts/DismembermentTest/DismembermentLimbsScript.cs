using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DismembermentLimbsScript : MonoBehaviour
{
    [SerializeField] DismembermentLimbsScript[] _childLimbs;

    [SerializeField] GameObject _limbPrefabs;
    [SerializeField] GameObject _woundHole; //Lite annorlunda än från videon
    [SerializeField] GameObject _bloodPrefab;

    //For testing
    DismemberentEnemyScript dismsmebrentScript;

    void Start()
    {
        dismsmebrentScript = transform.root.GetComponent<DismemberentEnemyScript>();
        if(_woundHole != null)
        {
            _woundHole.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Dismember() //Dismembrent function i Idamage eller i health sckriptet gör get hoit kalla på detta
    {
        if(_childLimbs.Length > 0)
        {
            foreach (var limb in _childLimbs)
            {
                if(limb != null)
                {
                    limb.Dismember();
                }
            }
        }

        if(_woundHole != null)
        {
            _woundHole.SetActive(true);
            if(_bloodPrefab != null)
            {
                Instantiate(_bloodPrefab, transform.position, transform.rotation);
            }
        }

        if (_limbPrefabs != null)
        {
            Instantiate(_limbPrefabs, transform.position, transform.rotation); //Spawn dismembered limb from the severed limbs place
        }

        transform.localScale = Vector3.zero; //Scale down the limn to Zero

        Destroy(this);
    }
}
