using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    private PlayerInputActions _playerInputActions;

    public event EventHandler OnInteractActionPressed;

    public event EventHandler OnLightAttackButtonPressed;
    public event EventHandler OnHeavyAttackButtonPressed;
    public event EventHandler OnRollButtonPressed;

    private void Awake()
    {
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Player.Enable();

        _playerInputActions.Player.Interact.performed += Interact_performed;
        _playerInputActions.Player.LightAttack.performed += LightAttack_performed;
        _playerInputActions.Player.HeavyAttack.performed += HeavyAttack_performed;
        _playerInputActions.Player.Roll.performed += Roll_performed;

    }

    private void Roll_performed(InputAction.CallbackContext obj)
    {
        OnRollButtonPressed?.Invoke(this, EventArgs.Empty);
    }

    private void HeavyAttack_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnHeavyAttackButtonPressed?.Invoke(this, EventArgs.Empty);
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
}
