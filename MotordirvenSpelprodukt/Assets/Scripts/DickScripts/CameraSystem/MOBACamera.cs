using UnityEngine;
using UnityEngine.AI;

public class MOBACamera : MonoBehaviour
{
    [Header("Camera settings")]
    [SerializeField]
    CameraViewData viewData;

    [Header("Event & Target")]
    [SerializeField] private CameraEvent cameraEvent;
    [SerializeField] private Transform player;           // Remove later! (player should use camera event!)

    Transform targetTransform;
    float cameraSpeed;
    CameraState cameraState;

    public enum CameraState
    {
        STASIS,
        FOLLOWPLAYER,
        FOCUSTARGET
    }

    #region Register/unregister event handlers
    private void Awake()
    {
        cameraEvent.camFocusToTargetHandler += LookAt;
        cameraEvent.camFocusToPlayerHandler += ResumePlayerFocus;
    }

    private void OnDestroy()
    {
        cameraEvent.camFocusToTargetHandler -= LookAt;
        cameraEvent.camFocusToPlayerHandler -= ResumePlayerFocus;
    }
    #endregion

    private void Start()
    {
        targetTransform = player.transform;       // By default follow player. Let Game Manager class handle this (remove later)
        transform.localEulerAngles = viewData.angleOffset;
        cameraState = CameraState.FOLLOWPLAYER;
        cameraSpeed = 5;    // TODO: Get access to player movement speed
    }

    private void LateUpdate()
    {
        if (cameraState == CameraState.STASIS) return;
        if (cameraState == CameraState.FOLLOWPLAYER) UpdateCameraPos();
    }

    private void UpdateCameraPos()
    {
        Vector3 newPos = targetTransform.position + viewData.posOffset;
        Vector3 direction = newPos - transform.position;
        if (direction.magnitude < viewData.offsetMargin) return;

        transform.position = Vector3.Lerp(transform.position, newPos, cameraSpeed * Time.deltaTime);
    }

    private void ResumePlayerFocus()
    {
        targetTransform = player.transform;
        cameraState = CameraState.FOLLOWPLAYER;
    }

    private void LookAt(Transform targetTransform)
    {
        this.targetTransform = targetTransform;
        cameraState = CameraState.FOCUSTARGET;
    }
}
