using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudienceAnimationScipt : MonoBehaviour
{
    [SerializeField] Animator _anim;
    CrowdBehaviour _crowdManager;
    public bool ChallengerFans;
    private int _fansMultipier;
    [SerializeField] float animSpeed;
    void Start()
    {
        _anim = gameObject.GetComponent<Animator>();
        _crowdManager = GameObject.FindGameObjectWithTag("Crowd").GetComponent<CrowdBehaviour>();

        if (ChallengerFans)
        {
            _fansMultipier = 1;
        }
        else
        {
            _fansMultipier = -1;
        }

        _anim.speed = Random.RandomRange(0.5f, 1.6f);
        animSpeed = _anim.speed;
       
    }

    // Update is called once per frame
    void Update()
    {

        if (_crowdManager.GetCrowdEmotion() == CrowdBehaviour.CrowdEmotion.Angry)
        {
            _anim.SetInteger("Emotion", -1 *_fansMultipier);
        }

        else if (_crowdManager.GetCrowdEmotion() == CrowdBehaviour.CrowdEmotion.Excited)
        {
            _anim.SetInteger("Emotion", 1* _fansMultipier);
        }

        else if(_crowdManager.GetCrowdEmotion() == CrowdBehaviour.CrowdEmotion.Normal)
        {
            _anim.SetInteger("Emotion", 0);
        }

        if(_anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.95f)
        {
            _anim.SetFloat("AnimationIndex", Random.Range(0f, 1f));
        }
    }
}
