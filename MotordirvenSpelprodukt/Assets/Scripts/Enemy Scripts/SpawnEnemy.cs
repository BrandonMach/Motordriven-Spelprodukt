using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    
    private static SpawnEnemy _instance;
    public static SpawnEnemy Instance { get => _instance; set => _instance = value; }


    public Transform[] SpawnPoints;
    private List<int> pointUsed;
    public EnemyScript[] MinionTypes;

    public Transform _target;


    [SerializeField] int MinimumMinionCount;


    private void Awake()
    {
        if (_instance != null)
        {
            Debug.LogWarning("More than one instance of GameManager found");
            return;
        }
        _instance = this;
    }
   
    

    void Start()
    {
        _target = Player.Instance.transform;

        for (int i = 0; i < 5; i++)
        {
            SpawNewEnemy(i);
        }
    }

    // Update is called once per frame
    void Update()
    {


        if (!GameLoopManager.Instance.MatchIsFinished && GameLoopManager.Instance.EnemyGameObjects.Length < (2 + MinimumMinionCount))
        {
            SpawNewEnemy(Random.Range(0, SpawnPoints.Length));
        }


        if (Input.GetKeyDown(KeyCode.Y)) // För testing
        {
            SpawNewEnemy(Random.Range(0, SpawnPoints.Length));
        }
    }


    public void SpawNewEnemy(int spawnPointIndex)
    {
        Instantiate(MinionTypes[Random.Range(0,MinionTypes.Length)], SpawnPoints[spawnPointIndex].position,Quaternion.identity /* Quaternion.LookRotation(_target.position)*/);
        GameLoopManager.Instance.UpdateEnemyList();
        //pointUsed.Add(spawnPointIndex);
    }
}
