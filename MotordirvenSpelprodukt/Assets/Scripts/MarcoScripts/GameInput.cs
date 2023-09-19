using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;


public class GameInput : MonoBehaviour
{
    private PlayerInputActions _playerInputActions;

    public event EventHandler OnInteractActionPressed;

    public event EventHandler OnLightAttackButtonPressed;

    private void Awake()
    {
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Player.Enable();

        _playerInputActions.Player.Interact.performed += Interact_performed;
        _playerInputActions.Player.LightAttack.performed += LightAttack_performed;

    }

    private void LightAttack_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnLightAttackButtonPressed?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractActionPressed?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = _playerInputActions.Player.Move.ReadValue<Vector2>();
        return inputVector.normalized;
    }

    public Vector2 GetDirectionVectorNormalized()
    {
        Vector2 inputVector = _playerInputActions.Player.Look.ReadValue<Vector2>();
        return inputVector.normalized;
    }

    public Vector2 GetMousePosition()
    {
        Vector2 inputVector = _playerInputActions.Player.MousePos.ReadValue<Vector2>();
        return inputVector;
    }
}
