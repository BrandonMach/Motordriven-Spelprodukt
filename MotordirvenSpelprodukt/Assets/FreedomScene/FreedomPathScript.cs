using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FreedomPathScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _currencyText;
    [SerializeField] TextMeshProUGUI _freedomPriceText;
    bool _showErrorPanel;
    [SerializeField] GameObject _errorPanel;
    Color _ogErrorPanelColor;
    Color _ogErrorTeextColor;

    float startTime = 0;

    bool _paymentConfirmed;

    int amountWaged = 0;
    float _tempPlayerCoins;
    int _tempFreedomPrice;
    public int FreedomPrice; //Öka destå fler du dödar i arenan
    private float _stagePriceMultiplier = 1.3f;
    void Start()
    {
       


        _freedomPriceText.text = "Freedom Price Pot:" + FreedomPrice;
        //_tempPlayerCoins = GameManager.PlayerCoins;
        _tempFreedomPrice = FreedomPrice;

        _ogErrorPanelColor = _errorPanel.GetComponent<Image>().color;
        _ogErrorTeextColor = _errorPanel.GetComponentInChildren<TextMeshProUGUI>().color;
    }

    void Update()
    {
        _tempPlayerCoins = GameManager.PlayerCoins;
        _currencyText.text = "Currency: " + _tempPlayerCoins;
       
        Debug.LogError("Actual gold: " + GameManager.PlayerCoins);

        if (_showErrorPanel)
        {
            startTime += Time.deltaTime;
            Color transparent = new Color(0, 0, 0, 0);
            _errorPanel.GetComponent<Image>().color = Color.Lerp(_ogErrorPanelColor, transparent, startTime);
            _errorPanel.GetComponentInChildren<TextMeshProUGUI>().color= Color.Lerp(_ogErrorTeextColor, transparent, startTime);
  
            if(startTime >= 1.2f)
            {        
                _errorPanel.SetActive(false);
                _showErrorPanel = false;
                startTime = 0;
            }
            
        }

        if(FreedomPrice >= 0)
        {
            Debug.Log("You win, you bought your freedom");
        }
    }


    public void PayforFreedom(int amount)
    {
        
        if ((_tempPlayerCoins - amount) >= 0 && _tempFreedomPrice >= 0 && (GameManager.PlayerCoins - amount) >= 0)
        {  
            if ((_tempFreedomPrice - amount) >= 0)
            {
                _freedomPriceText.color = Color.red;
                _freedomPriceText.text = "Freedom Price Pot: " + (_tempFreedomPrice - amount).ToString();
                _tempFreedomPrice -= amount;
                _tempPlayerCoins -= amount;
                amountWaged += amount;
            }            
        }
        else
        {
            _errorPanel.GetComponent<Image>().color = _ogErrorPanelColor;
            _errorPanel.GetComponentInChildren<TextMeshProUGUI>().color = _ogErrorTeextColor;
            _errorPanel.SetActive(true);
            _showErrorPanel = true;

        }
    }

    public void ConfirmAction()
    {
        _freedomPriceText.color = Color.white;
        FreedomPrice -= amountWaged;
        GameManager.PlayerCoins -= amountWaged;
        amountWaged = 0;
        _tempPlayerCoins = GameManager.PlayerCoins;
        
    }

    public void CancelPayment()
    {
        _freedomPriceText.color = Color.white;
        _freedomPriceText.text = "Freedom Price Pot:" + FreedomPrice;
        
        _tempFreedomPrice = FreedomPrice;
        _tempPlayerCoins = GameManager.PlayerCoins;
        amountWaged = 0;
    }

    public void GoToInventory()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
}
