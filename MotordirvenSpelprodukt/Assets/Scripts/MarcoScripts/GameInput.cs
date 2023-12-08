using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{

    public static GameInput Instance;
    private PlayerInputActions _playerInputActions;

    public event EventHandler OnInteractActionPressed;

    public event EventHandler OnLightAttackButtonPressed;
    public event EventHandler OnHeavyAttackButtonPressed;
    public event EventHandler OnEvadeButtonPressed;
    public event EventHandler OnPauseButtonPressed;
    public event EventHandler OnHealButtonPressed;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Player.Enable();

        _playerInputActions.Player.Interact.performed += Interact_performed;
        _playerInputActions.Player.LightAttack.performed += LightAttack_performed;
        _playerInputActions.Player.HeavyAttack.performed += HeavyAttack_performed;
        _playerInputActions.Player.Evade.performed += Evade_performed;
        _playerInputActions.Player.Pause.performed += Pause_performed;
        _playerInputActions.Player.Heal.performed += Heal_performed; ; 

    }

    private void Heal_performed(InputAction.CallbackContext obj)
    {
        OnHealButtonPressed?.Invoke(this, EventArgs.Empty);
    }

    private void Pause_performed(InputAction.CallbackContext obj)
    {
        OnPauseButtonPressed?.Invoke(this, EventArgs.Empty);
    }

    private void Evade_performed(InputAction.CallbackContext obj)
    {
        OnEvadeButtonPressed?.Invoke(this, EventArgs.Empty);
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
