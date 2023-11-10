using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdBehaviour : MonoBehaviour
{
    [SerializeField] private EntertainmentManager _etManager;

    [SerializeField] private Transform _playerPos;
    [SerializeField] private AudioSource _cheering;
    [SerializeField] private AudioSource _booing;

    [SerializeField] private GameObject[] _fallingObjects;
    bool _throwObject = true;


    //public AudioSource[] Themes;
    //public AudioSource NormalTheme, ExcitedTheme, AngryTheme; 
    public enum CrowdEmotion
    {
        Normal,
        Excited,
        Angry
    }

    public CrowdEmotion _emotion;
    public CrowdEmotion LatestEmotion;
    public CrowdEmotion GetCrowdEmotion()
    {
        return _emotion;
    }
    void Start()
    {
        //Subscribes to events
        _etManager.OnETPNormal += NormalCrowd;
        _etManager.OnETPAngry += AngryCrowd;
        _etManager.OnETPExited += ExcitedCrowd;
        

        //NormalTheme.volume = 0.2f;
        //ExcitedTheme.volume = 0;
        //AngryTheme.volume = 0;
    }


    // Update is called once per frame
    void Update()
    {

       // this.transform.position = _playerPos.position /*+ new Vector3(0, 10, 0)*/;


        //Inte klar än med potions, health potion
        if (Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(ThrowObject(_fallingObjects[1]));     
        }
        
            
    }

    #region Exited Crowd
    private void ExcitedCrowd(object sender, System.EventArgs e)
    {
        _emotion = CrowdEmotion.Excited;

        //StartCoroutine(FadeTheme(ExcitedTheme, LatestEmotion));
        LatestEmotion = _emotion;
    }
    #endregion

    #region Angry Crowd
    private void AngryCrowd(object sender, System.EventArgs e)
    {
        if (_throwObject)
        {
            StartCoroutine(ThrowObject(_fallingObjects[0]));
        }

        _emotion = CrowdEmotion.Angry;

        //StartCoroutine(FadeTheme(AngryTheme, LatestEmotion));
        LatestEmotion = _emotion;
    }
    #endregion

    #region Normal Crowd
    private void NormalCrowd(object sender, System.EventArgs e)
    {
        _emotion = CrowdEmotion.Normal;
        //StartCoroutine(FadeTheme(NormalTheme, LatestEmotion));
        LatestEmotion = _emotion;
    }
    #endregion


    private IEnumerator ThrowObject(GameObject fallingObject)
    {
        _throwObject = false;
        float randonThrowPosX = Random.Range(-10, 10);
        float randonThrowPosY = Random.Range(-10, 10);
        Instantiate(fallingObject, _playerPos.position + new Vector3(randonThrowPosX, 20, randonThrowPosY), transform.rotation);
        yield return new WaitForSeconds(2);
        _throwObject = true;      
    }

    //private IEnumerator FadeTheme (AudioSource newTheme, CrowdEmotion LatestEmotion)
    //{

    //    float timeToFade = 1.25f;
    //    float timeElapsed = 0;

    //    if(LatestEmotion != _emotion)
    //    {
    //        if (LatestEmotion == CrowdEmotion.Excited)
    //        {
    //            while (timeElapsed < timeToFade)
    //            {
    //                newTheme.volume = Mathf.Lerp(0, 0.2f, timeElapsed / timeToFade);
    //                ExcitedTheme.volume = Mathf.Lerp(0.2f, 0, timeElapsed / timeToFade);
    //                timeElapsed += Time.deltaTime;
    //                yield return null;
    //            }
    //        }
    //        if (LatestEmotion == CrowdEmotion.Angry)
    //        {
    //            while (timeElapsed < timeToFade)
    //            {
    //                newTheme.volume = Mathf.Lerp(0, 0.2f, timeElapsed / timeToFade);
    //                AngryTheme.volume = Mathf.Lerp(0.2f, 0, timeElapsed / timeToFade);
    //                timeElapsed += Time.deltaTime;
    //                yield return null;
    //            }
    //        }

    //        if (LatestEmotion == CrowdEmotion.Normal)
    //        {
    //            while (timeElapsed < timeToFade)
    //            {
    //                newTheme.volume = Mathf.Lerp(0, 0.2f, timeElapsed / timeToFade);
    //                NormalTheme.volume = Mathf.Lerp(0.2f, 0, timeElapsed / timeToFade);
    //                timeElapsed += Time.deltaTime;
    //                yield return null;
    //            }
    //        }
    //    }
    //}

}
