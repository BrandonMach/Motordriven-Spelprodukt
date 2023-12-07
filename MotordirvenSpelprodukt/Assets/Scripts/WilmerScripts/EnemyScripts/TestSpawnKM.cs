using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpawnKM : MonoBehaviour
{
    [SerializeField] private GameObject kmPrefab;
    [SerializeField] private Vector3 spawnPos;
    [SerializeField] private KeyCode spawnKey;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(spawnKey))
        {
            Instantiate(kmPrefab, spawnPos, Quaternion.identity);
        }
    }
}
