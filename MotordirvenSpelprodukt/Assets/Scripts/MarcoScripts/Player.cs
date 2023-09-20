using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    [SerializeField] private GameInput _gameInput;

    public event EventHandler OnChangeControllerTypeButtonPressed;


    // Start is called before the first frame update
    void Start()
    {
        _gameInput.OnInteractActionPressed += GameInput_OnInteractActionPressed;
        _gameInput.OnLightAttackButtonPressed += GameInput_OnLightAttackButtonPressed;
    }

    private void GameInput_OnLightAttackButtonPressed(object sender, EventArgs e)
    {
    }

    private void GameInput_OnInteractActionPressed(object sender, System.EventArgs e)
    {
        OnChangeControllerTypeButtonPressed?.Invoke(this, e);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
