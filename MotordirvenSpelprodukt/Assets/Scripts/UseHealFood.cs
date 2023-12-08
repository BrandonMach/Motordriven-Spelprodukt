using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UseHealFood : MonoBehaviour
{

    [SerializeField] Sprite NoHealItemSprite;
    HealthManager _playerHealth;

    [SerializeField] Image _icon;
    [SerializeField] TextMeshProUGUI _healText;

    private Player _player;
    void Start()
    {
        _player = Player.Instance;

        _playerHealth = _player.gameObject.GetComponent<HealthManager>();
        _player.HealButtonPressed += Player_HealButtonPressed;
    }

    private void Player_HealButtonPressed(object sender, System.EventArgs e)
    {
        if (TransferableScript.Instance.HealItems.Count > 0)
        {

            _player.GetComponent<HealthManager>().HealDamage(TransferableScript.Instance.HealItems[TransferableScript.Instance.HealItems.Count - 1].HPToHeal);
            TransferableScript.Instance.HealItems.RemoveAt(TransferableScript.Instance.HealItems.Count - 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(TransferableScript.Instance.HealItems.Count == 0)
        {
            _icon.sprite = NoHealItemSprite;
            _healText.text = "0";
            
        }
        else
        {
            _icon.sprite = TransferableScript.Instance.HealItems[TransferableScript.Instance.HealItems.Count - 1].Foodimage;
            _healText.text = TransferableScript.Instance.HealItems[TransferableScript.Instance.HealItems.Count - 1].HPToHeal.ToString();
        }
    }
}
