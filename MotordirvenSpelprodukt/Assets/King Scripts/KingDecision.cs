using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class KingDecision : MonoBehaviour
{
    // Start is called before the first frame update
    public DismemberentEnemyScript _playerDismemberent;
    private EntertainmentManager _etp;
    [SerializeField] SwitchCamera CamManager;
    Animator _anim;
    void Start()
    {
        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        CamManager = GameObject.FindWithTag("CamManager").GetComponent<SwitchCamera>();
        _playerDismemberent = GameObject.FindWithTag("Player").GetComponent<DismemberentEnemyScript>();
        _etp = GameObject.Find("Canvas").GetComponent<EntertainmentManager>();

        _anim = GetComponent<Animator>();
        gameManager.OnChampionKilled += PlayDecisionAnimation;
        //_anim.SetBool("Approved", false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void PlayDecisionAnimation(object sender, EventArgs e)
    {
        Debug.Log("Champion Is dead");
        _anim.SetBool("Approved", true);
        _anim.SetFloat("ETP", (_etp.GetETP() / 100)); //Selects what animation to play based on ETP
    }


    //King playes lose animation
    public void ExectutePlayer()
    {
        CamManager.GoToExecute();
        LoseScreenScript.KingExecution = true;
        _playerDismemberent.PlayerDismember();
        StartCoroutine(LoseScreen());        
    }

    private IEnumerator LoseScreen()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(2, LoadSceneMode.Single);
    }


    //King playes win animation
    public void GoToShop()
    {
        //testar gå till concept save money

        SceneManager.LoadScene(3, LoadSceneMode.Single);
    }
}
