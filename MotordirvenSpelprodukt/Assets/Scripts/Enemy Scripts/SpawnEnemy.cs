using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    // Start is called before the first frame update

    

    private static SpawnEnemy _instance;
    public static SpawnEnemy Instance { get => _instance; set => _instance = value; }


    public Transform[] SpawnPoint;

    public EnemyScript[] MinionTypes;

    public Transform _target;


    GameManager _gameManager;
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
        _target = GameObject.FindWithTag("Player").transform;
       
        for (int i = 0; i < 5; i++)
        {
            SpawNewEnemy();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if(GameManager.EnemyGameObjects.Length < (2+MinimumMinionCount))
        {

            SpawNewEnemy();
        }


        if (Input.GetKeyDown(KeyCode.Y))
        {


            SpawNewEnemy();
            
        }
    }


    public void SpawNewEnemy()
    {
        Instantiate(MinionTypes[0], SpawnPoint[Random.Range(0, SpawnPoint.Length)].position, Quaternion.LookRotation(_target.position));
        GameManager.Instance.UpdateEnemyList();

    }
}
