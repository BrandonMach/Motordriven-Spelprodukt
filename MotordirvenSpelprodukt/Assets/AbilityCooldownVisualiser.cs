using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AbilityCooldownVisualiser : MonoBehaviour
{
    // Start is called before the first frame update


    [Header("Rolling ability Cooldown")]
    public Image AbilityImage;
    public TextMeshProUGUI AbilityCooldownText;
    bool _isAbilityCooldown;
    float _currrentAbilityCooldown;

    float _cooldownDuration;
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
        AbilityImage.fillAmount = 0;

        switch (_abilityType)
        {
            case AbilityType.Dashing:
                _cooldownDuration = Player.Instance.GetComponent<PlayerDash>().CooldownDuration; //Get max cooldown
                
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
        switch (_abilityType)
        {
            case AbilityType.Dashing:
                _isAbilityCooldown = Player.Instance.GetComponent<PlayerDash>().IsOnCooldown;
                _currrentAbilityCooldown = Player.Instance.GetComponent<PlayerDash>().DashCooldown;
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
