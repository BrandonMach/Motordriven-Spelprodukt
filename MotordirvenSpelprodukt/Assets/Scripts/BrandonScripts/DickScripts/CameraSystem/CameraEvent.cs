using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Event/CameraEvent")]
public class CameraEvent : Event
{
    public event Action<Transform> camFocusToTargetHandler;
    public event Action camFocusToPlayerHandler;

    public void EnableFocus(Transform lookAtTransform) => camFocusToTargetHandler?.Invoke(lookAtTransform);
    public void DisableFocus() => camFocusToPlayerHandler?.Invoke();
}
