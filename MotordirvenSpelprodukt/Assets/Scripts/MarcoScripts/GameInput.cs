using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    private PlayerInputActions playerInputActions;

    public event EventHandler OnLightAttackButtonPressed;
    public event EventHandler OnHeavyAttackButtonPressed;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        playerInputActions.Player.LightAttack.performed += LightAttack_performed;
        playerInputActions.Player.HeavyAttack.performed += HeavyAttack_performed;       
    }

    private void HeavyAttack_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnHeavyAttackButtonPressed?.Invoke(this, EventArgs.Empty);
    }

    private void LightAttack_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnLightAttackButtonPressed?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
        return inputVector.normalized;
    }
}
