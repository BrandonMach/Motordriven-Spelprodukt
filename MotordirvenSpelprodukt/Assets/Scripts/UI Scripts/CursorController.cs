using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Search;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.UI;

public class CursorController : MonoBehaviour
{
    [SerializeField] RectTransform canvasRectTransform;

    private VirtualMouseInput virtualMouseInput;

    private void Awake()
    {
        //GameInput.Instance.OnGameDeviceChanged += HandleOnGameDeviceChanged;
        virtualMouseInput = GetComponent<VirtualMouseInput>();
        GamePadCursorToCenter();
    }

    private void OnDestroy()
    {
        GameInput.Instance.OnGameDeviceChanged -= HandleOnGameDeviceChanged;
    }

    private void Update()
    {
        //transform.localScale = Vector3.one * (1f / canvasRectTransform.localScale.x);
        transform.SetAsLastSibling();
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
        GamePadCursorToCenter();
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

    private void GamePadCursorToCenter()
    {
        virtualMouseInput.cursorTransform.anchoredPosition = new Vector2(Screen.width / 2f, Screen.height / 2f);
        //InputState.Change(virtualMouseInput.virtualMouse.position, centerOfScreen);
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
}
