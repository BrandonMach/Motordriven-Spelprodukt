using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KingDecision : MonoBehaviour
{
    // Start is called before the first frame update
    public DismemberentEnemyScript _playerDismemberent;
    [SerializeField] SwitchCamera CamManager;
    void Start()
    {
        CamManager = GameObject.FindWithTag("CamManager").GetComponent<SwitchCamera>();
        _playerDismemberent = GameObject.FindWithTag("Player").GetComponent<DismemberentEnemyScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //King playes lose animation
    public void GoToLoseScreen()
    {
        CamManager.GoToExecute();
        LoseScreenScript.KingExecution = true;
        _playerDismemberent.PlayerDismember();      
        SceneManager.LoadScene(2, LoadSceneMode.Single);
    }


    //King playes win animation
    public void GoToShop()
    {
        //testar gå till concept save money

        SceneManager.LoadScene(3, LoadSceneMode.Single);
    }
}
