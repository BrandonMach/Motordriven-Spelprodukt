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


    #region Wave Spawn

    public Transform[] SpawnPoints;

    public float _countdown;

    [SerializeField] public WaveBattleInfo[] _waveBattleInformation;
    [SerializeField] public static int _currentWaveBattleIndex; //Borde vara static

    //public WaveInfoHolder[] ScriptableObjectWaves;

    public int _currentWaveIndex = 0;




   public  bool readyToCountdown = false;

    int _enemyGameObjectsMinimum;

    public int debugInt;


    [SerializeField] private Animator _anim;
    [SerializeField] private TMPro.TextMeshProUGUI _waveText;

   
    public bool WavesAreSpawning;

    public System.EventHandler KilledAllWaves;

    bool stopSpawn;

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
        _currentWaveBattleIndex = GameManager.BattleIndex;
        GameLoopManager.Instance.OnMatchFinished += StopWaveSpawn;


        for (int i = 0; i < _waveBattleInformation[_currentWaveBattleIndex].waveInfoHolder.Count; i++)
        {
            _waveBattleInformation[_currentWaveBattleIndex].waveInfoHolder[i].EnemiesLeft = _waveBattleInformation[_currentWaveBattleIndex].waveInfoHolder[i].WaveMinions.Length;
        }


        readyToCountdown = true;
        


    }

    // Update is called once per frame
    void Update()
    {

        if (!stopSpawn)
        {
            _waveText.gameObject.SetActive(!GameLoopManager.Instance.MatchIsFinished);


            if (_currentWaveIndex >= _waveBattleInformation[_currentWaveBattleIndex].waveInfoHolder.Count && !GameLoopManager.Instance.MatchIsFinished)
            {
                if (GameManager.Instance._currentMatchType == GameManager.MatchType.WaveBattle)
                {

                    Debug.Log("You have survived every wave");
                    KilledAllWaves?.Invoke(this, System.EventArgs.Empty);
                }


                return;
            }
            else
            {

                if (readyToCountdown)
                {
                    //EntertainmentManager.Instance.CanGoOTC = false;
                    WavesAreSpawning = true;
                    _countdown -= Time.deltaTime;

                }
                else
                {
                    WavesAreSpawning = false;

                }

                

                if (_countdown <= 0 && !GameLoopManager.Instance.MatchIsFinished)
                {
                    readyToCountdown = false;
                    _countdown = _waveBattleInformation[_currentWaveBattleIndex].waveInfoHolder[_currentWaveIndex].timeToNextWave;
                    StartCoroutine(SpawnWave());
                }


                if (!GameLoopManager.Instance.MatchIsFinished && _waveBattleInformation[_currentWaveBattleIndex].waveInfoHolder[_currentWaveIndex].EnemiesLeft == 0)
                {

                    readyToCountdown = true;
                    _currentWaveIndex++;


                }
                
             

            }         
        }
    }

    private void StopWaveSpawn(object sender, System.EventArgs e)
    {
        stopSpawn = true;
    }


    private IEnumerator SpawnWave()
    {
        //Fade in wave text
        _waveText.text = "Wave " + (1+_currentWaveIndex).ToString();
        _anim.SetTrigger("ActivateFade");


        if (_currentWaveIndex < _waveBattleInformation[_currentWaveBattleIndex].waveInfoHolder.Count)
        {
           

            for (int i = 0; i < _waveBattleInformation[_currentWaveBattleIndex].waveInfoHolder[_currentWaveIndex].WaveMinions.Length; i++)
            {
                var randomSpawnPos = SpawnPoints[Random.Range(0, SpawnPoints.Length)];


                EnemyScript minion = Instantiate(_waveBattleInformation[_currentWaveBattleIndex].waveInfoHolder[_currentWaveIndex].WaveMinions[i], randomSpawnPos.position, Quaternion.identity);
                minion.transform.SetParent(randomSpawnPos);
                GameLoopManager.Instance.UpdateEnemyList();

                yield return new WaitForSeconds(_waveBattleInformation[_currentWaveBattleIndex].waveInfoHolder[_currentWaveIndex].timeToNextEnemy);
            }
        }

        

         
    }


   
}
