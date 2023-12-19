using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class ChallengeCompletedUI : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI _challengeText;
    [SerializeField] GameObject _completedChallengeBox;
    [SerializeField] Animator _animator;

    float _showTimer;
    float _showPeriod = 3.5f;
    bool _shouldShow;

    // Start is called before the first frame update
    void Start()
    {
        //GameManager.OnChallengeCompleted += HandleOnChallengeCompled;
        _completedChallengeBox.SetActive(false);
        GameLoopManager.OnChallengeCompleted += HandleOnChallengeCompled;
        _challengeText.text = "";
    }

    private void HandleOnChallengeCompled(EventArgs eventArgs, Challenge completedChallenge)
    {
        _animator.SetTrigger("ActivateFade");
        _challengeText.text = ($"{completedChallenge.ChallengeName} completed");
        _completedChallengeBox.SetActive(true);
        _shouldShow = true;
    }

    private void OnDestroy()
    {
        GameManager.OnChallengeCompleted -= HandleOnChallengeCompled;
    }

    // Update is called once per frame
    void Update()
    {
        if (_shouldShow)
        {
            _showTimer += Time.deltaTime;

            if (_showTimer >= _showPeriod)
            {
                _completedChallengeBox.SetActive(false);
                _challengeText.text = "";
                _shouldShow = false;
            }
        }
    }
}
