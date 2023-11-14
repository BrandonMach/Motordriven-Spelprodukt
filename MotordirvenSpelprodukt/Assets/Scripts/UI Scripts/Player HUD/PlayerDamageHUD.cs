using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class PlayerDamageHUD : MonoBehaviour
{
    [SerializeField] private Image _bloodSplatterImage = null;
    [SerializeField] private Image _hurtImage = null;
    [SerializeField] private float _hurtTimer = 1f;


    Color _splatterAlpha;
    Color _hurtAlpha;

    Color resetBloodColor;
    Color resetHurtColor;

    [SerializeField] List<Sprite> _splattersImages;

    //[Header("Audio")]
    //[SerializeField] private AudioClip _hurtAudio = null;
    public HealthManager _playerHP;

    public CinemachineImpulseSource ImpulseSource;
    private void Start()
    {
        _hurtImage.enabled = false;
        _bloodSplatterImage.enabled = false;

        //_playerHP = GameObject.FindWithTag("Player").GetComponent<HealthManager>();
        _playerHP.OnPlayerTakeDamage += UpdateAlphColor;

       
        _splatterAlpha = _bloodSplatterImage.color;
        _hurtAlpha = _hurtImage.color;

        resetBloodColor = _bloodSplatterImage.color;
        resetHurtColor = _hurtImage.color;

    }

    void UpdateAlphColor(object sender, System.EventArgs e)
    {
        ImpulseSource.GenerateImpulse();
        Reset();
    }


    private void Update()
    {

        switch (_playerHP.CurrentHealthPoints)  
        {
            case > 80:
                _bloodSplatterImage.sprite = _splattersImages[0];
                break;
            case > 60:
                _bloodSplatterImage.sprite = _splattersImages[1];
                break;
            case > 40:
                _bloodSplatterImage.sprite = _splattersImages[2];
                break;
            case > 20:
                _bloodSplatterImage.sprite = _splattersImages[3];
                break;
            default:
                _bloodSplatterImage.sprite = _splattersImages[4];
                break;
        }


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
