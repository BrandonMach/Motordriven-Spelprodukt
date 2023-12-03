using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdPlacerScript : MonoBehaviour
{
    public List<AudienceAnimationScipt> audiencePrefabs;
    public List<Transform> PlacerPositions;

    int amounts = 9;

    private void Awake()
    {
        for (int i = 0; i < amounts; i++)
        {
            Instantiate(audiencePrefabs[Random.Range(0, audiencePrefabs.Count)].gameObject, PlacerPositions[i]);
        }
    }

    void Start()
    {
       
    }

}
