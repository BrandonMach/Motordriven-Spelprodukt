using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class SpawnEnemy : MonoBehaviour
{


    #region Singleton


    private static SpawnEnemy _instance;
    public static SpawnEnemy Instance { get => _instance; set => _instance = value; }

   
    #endregion

  


    public Transform[] SpawnPoints;
    private List<int> pointUsed;
    

    public Transform _target;


    [SerializeField] int MinimumMinionCount;





    #region Wave Spawn


    public Wave TestWave;


    public float _countdown;

    public WaveInfoHolder[] ScriptableObjectWaves;

    public int _currentWaveIndex = 0;




   public  bool readyToCountdown = false;

    int _enemyGameObjectsMinimum;

    public int debugInt;






    #endregion

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


        for (int i = 0; i < ScriptableObjectWaves.Length; i++)
        {
            ScriptableObjectWaves[i].EnemiesLeft = ScriptableObjectWaves[i].WaveMinions.Length;
        }


        readyToCountdown = true;
  
    }

    // Update is called once per frame
    void Update()
    {


        //if (!GameLoopManager.Instance.MatchIsFinished && GameLoopManager.Instance.EnemyGameObjects.Length < (2 + MinimumMinionCount))
        //{
        //    SpawNewEnemy(Random.Range(0, SpawnPoints.Length));
        //}

        if(_currentWaveIndex >= ScriptableObjectWaves.Length)
        {
            //Debug.Log("YOu survid´ved every wave");
            return;
        }


        if (Input.GetKeyDown(KeyCode.Y)) // För testing
        {
            SpawNewEnemy(Random.Range(0, SpawnPoints.Length));
            readyToCountdown = true;
        }


        debugInt = ScriptableObjectWaves[_currentWaveIndex].EnemiesLeft;


        if (readyToCountdown)
        {
            _countdown -= Time.deltaTime;
        }


        if (_countdown <= 0 )
        {
            readyToCountdown = false;
            _countdown = ScriptableObjectWaves[_currentWaveIndex].timeToNextWave;
            StartCoroutine(SpawnWave());
        }


        if(ScriptableObjectWaves[_currentWaveIndex].EnemiesLeft == 0)
        {
            readyToCountdown = true;
            _currentWaveIndex++;
        }

    }




    public void SpawNewEnemy(int spawnPointIndex)
    {
        Instantiate(TestWave.WaveMinions[Random.Range(0, TestWave.WaveMinions.Length)], SpawnPoints[spawnPointIndex].position, Quaternion.identity);
        GameLoopManager.Instance.UpdateEnemyList();
        //pointUsed.Add(spawnPointIndex);
    }


    private IEnumerator SpawnWave()
    {


        if(_currentWaveIndex < ScriptableObjectWaves.Length)
        {
            for (int i = 0; i < ScriptableObjectWaves[_currentWaveIndex].WaveMinions.Length; i++)
            {
                var randomSpawnPos = SpawnPoints[Random.Range(0, SpawnPoints.Length)];


                EnemyScript minion = Instantiate(ScriptableObjectWaves[_currentWaveIndex].WaveMinions[i], randomSpawnPos.position, Quaternion.identity);
                minion.transform.SetParent(randomSpawnPos);
                GameLoopManager.Instance.UpdateEnemyList();

                yield return new WaitForSeconds(ScriptableObjectWaves[_currentWaveIndex].timeToNextEnemy);
            }
        }
       
    
    }


   
}

[System.Serializable] //Viewable in inspector
public class Wave
{
    public EnemyScript[] WaveMinions;
    public float timeToNextEnemy;
    public float timeToNextWave;
    [HideInInspector] public int EnemiesLeft;
    
}
