using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdBehaviour : MonoBehaviour
{
    [SerializeField] private EntertainmentManager _etManager;

    [SerializeField] private Transform _playerPos;
    [SerializeField] private AudioSource _cheering;
    [SerializeField] private AudioSource _booing;

    public Rect fallingArea;
    public GameObject fallingObject;
    bool _throwObject = true;


    bool _playCheering;
    bool _playBooing;

    public AudioSource[] Themes;
    public AudioSource NormalTheme, ExcitedTheme, AngryTheme; 
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

        //NormalTheme = Themes[0];
        //ExcitedTheme = Themes[1];
        //AngryTheme = Themes[2];

        NormalTheme.volume = 0.2f;
        ExcitedTheme.volume = 0;
        AngryTheme.volume = 0;
    }


    // Update is called once per frame
    void Update()
    {
        fallingArea = new Rect(0, 3, 10, 2);
        this.transform.position = _playerPos.position /*+ new Vector3(0, 10, 0)*/;

        if (_etManager.GetETP() > _etManager.GetExcitedThreshold() /*&& !_playCheering*/) //Bool för att den bara sla spelas en gång
        {
            
            _playCheering = true;
            _emotion = CrowdEmotion.Excited;

            //Swicth music track, before latest motion

            //StopAllCoroutines();
            StartCoroutine(FadeTheme(ExcitedTheme, LatestEmotion));
            LatestEmotion = _emotion;
           

            //PlayCheer();
        }
        else if(_etManager.GetETP() < _etManager.GetAngryThreshold() /*&& !_playBooing*/)
        {

            if (_throwObject)
            {
                StartCoroutine(ThrowTomato());
                //_throwObject = true;
            }
            
            //Swicth music track
            _playBooing = true;
            _emotion = CrowdEmotion.Angry;


            //Swicth music track, before latest motion
            //StopAllCoroutines();
            StartCoroutine(FadeTheme(AngryTheme, LatestEmotion));
            LatestEmotion = _emotion;
            
            //PlayBooo();
        }
        else
        {
            //Swicth music track
            _playCheering = false;
            _playBooing = false;
            _emotion = CrowdEmotion.Normal;


            //Swicth music track, before latest motion
            //StopAllCoroutines();
            StartCoroutine(FadeTheme(NormalTheme, LatestEmotion));
            LatestEmotion = _emotion;


        }


        if (Input.GetKeyDown(KeyCode.F))
        {
            ThrowTomato();
        }
        

    }

    private IEnumerator ThrowTomato()
    {
        _throwObject = false;
        float randonThrowPosX = Random.Range(-10, 10);
        float randonThrowPosY = Random.Range(-10, 10);
        Instantiate(fallingObject, _playerPos.position + new Vector3(randonThrowPosX, 20, randonThrowPosY), transform.rotation);
        yield return new WaitForSeconds(2);
        _throwObject = true;
        
    }

    void PlayCheer()
    {
        _cheering.Play();
    }

    void PlayBooo()
    {
        _booing.Play();
    }

    private IEnumerator FadeTheme (AudioSource newTheme, CrowdEmotion LatestEmotion)
    {

        float timeToFade = 1.25f;
        float timeElapsed = 0;

        if(LatestEmotion != _emotion)
        {
            if (LatestEmotion == CrowdEmotion.Excited)
            {
                while (timeElapsed < timeToFade)
                {
                    newTheme.volume = Mathf.Lerp(0, 0.2f, timeElapsed / timeToFade);
                    ExcitedTheme.volume = Mathf.Lerp(0.2f, 0, timeElapsed / timeToFade);
                    timeElapsed += Time.deltaTime;
                    yield return null;
                }
            }
            if (LatestEmotion == CrowdEmotion.Angry)
            {
                while (timeElapsed < timeToFade)
                {
                    newTheme.volume = Mathf.Lerp(0, 0.2f, timeElapsed / timeToFade);
                    AngryTheme.volume = Mathf.Lerp(0.2f, 0, timeElapsed / timeToFade);
                    timeElapsed += Time.deltaTime;
                    yield return null;
                }
            }

            if (LatestEmotion == CrowdEmotion.Normal)
            {
                while (timeElapsed < timeToFade)
                {
                    newTheme.volume = Mathf.Lerp(0, 0.2f, timeElapsed / timeToFade);
                    NormalTheme.volume = Mathf.Lerp(0.2f, 0, timeElapsed / timeToFade);
                    timeElapsed += Time.deltaTime;
                    yield return null;
                }
            }
        }
    }

}
