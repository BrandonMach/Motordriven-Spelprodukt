using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelDesign
{
    public class GameObjectPlacements : MonoBehaviour
    {
        [Header("Static environment")]
        [SerializeField] private GameObject[] treePrefabs;
        [SerializeField] private PlacementData placementData;   // Data will be stored here by the pcg system
        [SerializeField] private bool destroyPlacement;

        List<GameObject> spawnedObjs;

        public int RandomTreeIndex => Random.Range(0, treePrefabs.Length);

        // Start is called before the first frame update
        void Start()
        {
            spawnedObjs = new List<GameObject>();

            for (int i = 0; i < placementData.treePositions.Length; i++)
            {
                GameObject obj = Instantiate(treePrefabs[RandomTreeIndex]);
                obj.transform.position = placementData.treePositions[i];
                obj.transform.rotation = Quaternion.Euler(placementData.treeRotations[i]);
                obj.transform.localScale = placementData.treeScales[i];
                obj.transform.parent = transform;
                spawnedObjs.Add(obj);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnValidate()
        {
            if (placementData == null || spawnedObjs.Count <= 0) return;
            DestroyObjs();
        }

        private void OnDrawGizmos()
        {
            if (placementData == null) return;
            Gizmos.color = Color.green;
            for (int i = 0; i < placementData.treePositions.Length; i++)
            {
                Gizmos.DrawCube(placementData.treePositions[i], new Vector3(1,2,1));
            }
        }

        private void DestroyObjs()
        {
            foreach (GameObject obj in spawnedObjs)
                Destroy(obj);
        }
    }
}
