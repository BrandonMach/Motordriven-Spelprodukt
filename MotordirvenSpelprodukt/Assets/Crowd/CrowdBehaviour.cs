using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdBehaviour : MonoBehaviour
{
    //[SerializeField] private EntertainmentManager _etManager;

    [SerializeField] private Transform _playerPos;


    [SerializeField] private GameObject[] _fallingObjects;
    bool _throwObject = true;

    [SerializeField] AudienceAnimationScipt[] _crowdSections;
    public List<GameObject> _championFans;
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

    private void Awake()
    {
        _crowdSections = GameObject.FindObjectsOfType<AudienceAnimationScipt>();

    }

    void Start()
    {
        //Subscribes to events
        EntertainmentManager.Instance.OnETPNormal += NormalCrowd;
        EntertainmentManager.Instance.OnETPAngry += AngryCrowd;
        EntertainmentManager.Instance.OnETPExited += ExcitedCrowd;


        foreach (var audience in _crowdSections)
        {
            int setRandomChampionFans = Random.Range(0, 10);
            
            if(setRandomChampionFans > 5) //Make a audience memeber a Challenger fan randomly
            {
                audience.ChallengerFans = true;
                _championFans.Add(audience.gameObject);
            }
            
            
        }



    }


    // Update is called once per frame
    void Update()
    {
        

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
       // FMODSFXController.Instance.PlayCrowdCheer();
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
       // FMODSFXController.Instance.PlayCrowdBoo();
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

        int randomChampionFan = Random.Range(0, _championFans.Count);

        Instantiate(fallingObject, _championFans[randomChampionFan].transform.position , transform.rotation);
        yield return new WaitForSeconds(2);
        _throwObject = true;      
    }



}
