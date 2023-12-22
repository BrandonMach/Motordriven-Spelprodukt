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

    public GameObject EmotePosition;
    public GameObject[] Emotes;

    void Start()
    {
        GameLoopManager gameManager = GameLoopManager.Instance;
        CamManager = GameObject.FindWithTag("CamManager").GetComponent<SwitchCamera>();
        _playerDismemberent = Player.Instance.GetComponent<DismemberentEnemyScript>();
        _etp = EntertainmentManager.Instance;

        _anim = GetComponent<Animator>();
        gameManager.OnFightComplete += PlayDecisionAnimation;


        //King Emotes

        Emotes[1].SetActive(false);

        Emotes[0].SetActive(false);
        _etp.OnETPAngry += AngryEmote;
        _etp.OnETPExited += ExcitedEmote;

    }


    void Update()
    {
        

        
    }

    private void AngryEmote(object sender, System.EventArgs e)
    {
        Emotes[1].SetActive(false);

        Emotes[0].SetActive(true);
    }
    private void ExcitedEmote(object sender, System.EventArgs e)
    {
        Emotes[0].SetActive(false);
        Emotes[1].SetActive(true);
    }


    private void PlayDecisionAnimation(object sender, EventArgs e)
    {
        _anim.SetBool("Approved", true);
        _anim.SetFloat("ETP", (_etp.GetETP() / 100)); //Selects what animation to play based on ETP       
    }


    //King playes lose animation
    public void ExectutePlayer()
    {
        StartCoroutine(LoseScreen());
        CamManager.GoToExecute();
        LoseScreenScript.KingExecution = true;
        _playerDismemberent.DismemberCharacter();
            
    }

    private IEnumerator LoseScreen()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(4, LoadSceneMode.Single);
    }


    //King playes win animation
    public void GoToShop()
    {
        StartCoroutine(ShowMatchSummary());

    }

    private IEnumerator ShowMatchSummary()
    {
        SceneManager.LoadScene(9, LoadSceneMode.Additive);
        yield return new WaitForSeconds(10);

        if(GameManager.ArenaLayoutIndex < 2)
        {
            GameManager.ArenaLayoutIndex++;
        }
        

        GameManager.BattleIndex++;
        SceneManager.LoadScene(6, LoadSceneMode.Single);
    }
}
