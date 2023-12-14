using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class AbilityCooldownVisualiser : MonoBehaviour
{
    // Start is called before the first frame update
    static int AbilitiesActive = 0;

    [Header("Rolling ability Cooldown")]
    public Image AbilityImage;
    public TextMeshProUGUI AbilityCooldownText;
    bool _isAbilityCooldown;
    float _currrentAbilityCooldown;


    float _cooldownDuration;

    private PlayerDash _playerDash;

    public enum AbilityType
    {
        Dashing,
        Bleeding,
        Stunned,
        Airborne,

    }

    public AbilityType _abilityType;


    void Start()
    {
        _playerDash = Player.Instance.GetComponent<PlayerDash>();

        AbilityImage.fillAmount = 0;

        switch (_abilityType)
        {
            case AbilityType.Dashing:
                _cooldownDuration = _playerDash.CooldownDuration; //Get max cooldown
                break;
            case AbilityType.Bleeding:
                break;
            case AbilityType.Stunned:
                break;
            case AbilityType.Airborne:
                break;
            default:
                break;
        }
    }


    // Update is called once per frame
    void Update()
    {

        if (!Player.Instance.IsDestroyed())
        {
            switch (_abilityType)
            {
                case AbilityType.Dashing:
                    _isAbilityCooldown = _playerDash.IsOnCooldown;
                    _currrentAbilityCooldown = _playerDash.DashCooldown;
                    break;
                case AbilityType.Bleeding:
                    break;
                case AbilityType.Stunned:
                    break;
                case AbilityType.Airborne:
                    break;
                default:
                    break;
            }
        }
       
        //När en isAbility sätts igång +1 i active abilities. Lägg ability icon på x position
        if (_isAbilityCooldown)
        {
            if (_currrentAbilityCooldown <= 0f)
            {

                if(AbilityImage != null)
                {
                    AbilityImage.fillAmount = 0;
                }
              
            }
            else
            {
                if (AbilityImage != null)
                {
                    AbilityImage.fillAmount = _currrentAbilityCooldown / _cooldownDuration;
                }
            }
        }
    }


   





}
