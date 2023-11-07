using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDamageHUD : MonoBehaviour
{
    [SerializeField] private Image _bloodSplatterImage = null;
    [SerializeField] private Image _hurtImage = null;
    [SerializeField] private float _hurtTimer = 1f;


    Color _splatterAlpha;
    Color _hurtAlpha;

    Color resetBloodColor;
    Color resetHurtColor;


    //[Header("Audio")]
    //[SerializeField] private AudioClip _hurtAudio = null;
    public HealthManager _playerHP;
    

    private void Start()
    {
        _hurtImage.enabled = false;
        _bloodSplatterImage.enabled = false;

        _playerHP = GameObject.FindWithTag("Player").GetComponent<HealthManager>();
        _playerHP.OnPlayerTakeDamage += UpdateAlphColor;

       
        _splatterAlpha = _bloodSplatterImage.color;
        _hurtAlpha = _hurtImage.color;

        resetBloodColor = _bloodSplatterImage.color;
        resetHurtColor = _hurtImage.color;

    }

    void UpdateAlphColor(object sender, System.EventArgs e)
    {
        Reset();
    }


    private void Update()
    {
        _bloodSplatterImage.color = _splatterAlpha;
        _hurtImage.color = _hurtAlpha;


        _splatterAlpha.a -= Time.deltaTime;
        _hurtAlpha.a -= Time.deltaTime;
    }

    private void Reset()
    {
        _hurtImage.enabled = true;
        _bloodSplatterImage.enabled = true;

        _splatterAlpha = resetBloodColor;
        _hurtAlpha = resetHurtColor;
    }

    
}
