using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private GameObject _hasProgressGameObject;
    [SerializeField] private Slider _slider;
    [SerializeField] private float _refillSpeed;

    private float _targetFillAmount;

    private IHasProgress _hasProgress;

    private bool increaseHealth;
    public bool IsChampionHPbar;

    private void Start()
    {

        if (IsChampionHPbar)
        {
            _hasProgressGameObject = GameManager.Instance._championNy;
        }


        _hasProgress = _hasProgressGameObject.GetComponent<IHasProgress>(); //L�gger in objectet som ska p�verka Ehalthbar UI element


        if (_hasProgress == null)
        {
            Debug.LogError("Game Object: " + _hasProgressGameObject + " does not implement IHasProgress");
        }

        _hasProgress.OnProgressChanged += HasProgress_OnProgressChanged; //n�r OnProgressChanges Invokes 

        _targetFillAmount = _slider.value;
    }

  

    private void HasProgress_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        _targetFillAmount = e.progressNormalized;
        if (_targetFillAmount > _slider.value) 
        { 
            increaseHealth = true;
        }
        else
        {
            increaseHealth = false;
        }
    }

    private void Update()
    {
        if (_slider.value != _targetFillAmount)
        {
            if (increaseHealth && _slider.value < _targetFillAmount)
            {
                _slider.value += _refillSpeed * Time.deltaTime;
            }
            else if (!increaseHealth && _slider.value > _targetFillAmount)
            {
                _slider.value -= _refillSpeed * Time.deltaTime;
            }
        }
    }

    private void OnDestroy()
    {
        _hasProgress.OnProgressChanged -= HasProgress_OnProgressChanged;
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
