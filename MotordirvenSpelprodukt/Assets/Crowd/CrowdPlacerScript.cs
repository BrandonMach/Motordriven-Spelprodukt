using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdPlacerScript : MonoBehaviour
{
    public List<AudienceAnimationScipt> audiencePrefabs;

    int amounts = 9;
    public float yOffset = 0f;
    public float xOffset = 0;
    public float zOffset = 0;

    public int count;

    Transform _transform;


    void Start()
    {
        _transform = this.GetComponent<Transform>();
        for (int i = 0; i < amounts; i++)
        {
            switch (count)
            {
                case 0:
                    xOffset = 0;
                    zOffset = 0;
                    break;
                case 1:
                    xOffset = 1.5f;

                    break;
                case 2:
                    xOffset = -1.5f;

                    break;
                case 3:
                    xOffset = 0;
                    zOffset = 1.5f;
                    break;
                case 4:
                    xOffset = 1.5f;
                    zOffset = 1.5f;
                    break;
                case 5:
                    xOffset = -1.5f;
                    zOffset = 1.5f;
                    break;
                case 6:
                    xOffset = 0;
                    zOffset = -1.5f;
                    break;
                case 7:
                    xOffset = 1.5f;
                    zOffset = -1.5f;
                    break;
                case 8:
                    xOffset = -1.5f;
                    zOffset = -1.5f;
                    break;

                default:
                    break;
            }

            count++;

            Vector3 spawnPos = new Vector3(this.transform.position.x + xOffset, this.transform.position.y - yOffset, this.transform.position.z + zOffset);
            Quaternion rotation = _transform.rotation;

            Instantiate(audiencePrefabs[Random.Range(0, audiencePrefabs.Count)].gameObject, spawnPos, rotation);
        }


    }

    // Update is called once per frame
    void Update()
    {

       
    }
}
