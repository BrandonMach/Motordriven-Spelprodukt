using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.UI;

public class CursorController : MonoBehaviour
{

    //public enum GameDevice
    //{
    //    KeyboardMouse,
    //    Gamepad,
    //}

    //private GameDevice activeGameDevice;

    private VirtualMouseInput virtualMouseInput;

    private void Awake()
    {
        virtualMouseInput = GetComponent<VirtualMouseInput>();

       

        //InputSystem.onActionChange += handleOnActionChange;
    }

    private void Start()
    {
        GameInput.Instance.OnGameDeviceChanged += HandleOnGameDeviceChanged;
    }

    private void LateUpdate()
    {
        Vector2 virtualMousePosition = virtualMouseInput.virtualMouse.position.value;
        virtualMousePosition.x = Mathf.Clamp(virtualMousePosition.x, 0f, Screen.width);
        virtualMousePosition.y = Mathf.Clamp(virtualMousePosition.y, 0f, Screen.height);

        InputState.Change(virtualMouseInput.virtualMouse.position, virtualMousePosition);

        
    }

    private void HandleOnGameDeviceChanged(object sender, System.EventArgs e)
    {
        UpdateVisibility();
    }

    private void UpdateVisibility()
    {
        if (GameInput.Instance.GetActiveGameDevice() == GameInput.GameDevice.Gamepad)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Hide()
    {
        gameObject.SetActive(false);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void ToggleCursorMode()
    {
        if (virtualMouseInput.cursorMode == VirtualMouseInput.CursorMode.SoftwareCursor)
        {
            virtualMouseInput.cursorMode = VirtualMouseInput.CursorMode.HardwareCursorIfAvailable;
        }
        else
        {
            virtualMouseInput.cursorMode = VirtualMouseInput.CursorMode.SoftwareCursor;
        }
        
    }

    //private void handleOnActionChange(object arg1, InputActionChange inputActionChange)
    //{
    //    if (inputActionChange == InputActionChange.ActionPerformed && arg1 is InputAction)
    //    {
    //        InputAction inputAction = arg1 as InputAction;

    //        if (inputAction.activeControl.device.displayName == "VirtualMouse")
    //        {
    //            // Ignore virtual mouse
    //            return;
    //        }
    //        if (inputAction.activeControl.device is Gamepad)
    //        {
    //            if (activeGameDevice != GameDevice.Gamepad)
    //            {
    //                ChangeActiveGameDevice(GameDevice.Gamepad);
    //            }
    //        }
    //        else
    //        {
    //            if (activeGameDevice != GameDevice.KeyboardMouse)
    //            {
    //                ChangeActiveGameDevice(GameDevice.KeyboardMouse);
    //            }
    //        }          
    //    }
    //}

    //private void ChangeActiveGameDevice(GameDevice activeGameDevice)
    //{
    //    this.activeGameDevice = activeGameDevice;

    //    Cursor.visible = activeGameDevice == GameDevice.KeyboardMouse;
    //    OnGameDeviceChanged?.Invoke(this, EventArgs.Empty);
    //}

}
