using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private GameInput gameInput;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private PlayerAnimation playerAnimation;

    private CharacterController characterController;

    private Vector3 camForward;
    private Vector3 camRight;

    private bool isMoving = false; 
    
    private void Start()
    {
        characterController = GetComponent<CharacterController>();

    }

    void Update()
    {
        Move();

    }

    private void Move()
    {
        GetCameraValues();

        Vector2 inputvector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDirectionNormalized = inputvector.x * camRight + inputvector.y * camForward;
        characterController.Move(moveDirectionNormalized * moveSpeed * Time.deltaTime);

        Quaternion currentRotation = transform.rotation;
        Quaternion newRotation = Quaternion.LookRotation(moveDirectionNormalized);


        isMoving = moveDirectionNormalized != Vector3.zero;
        if (isMoving)
        {
            transform.localRotation = Quaternion.Slerp(currentRotation, newRotation, rotationSpeed * Time.deltaTime);
        }
        playerAnimation.Animate(moveDirectionNormalized);
    }
        
    private void GetCameraValues()
    {
        camForward = mainCamera.transform.forward;
        camRight = mainCamera.transform.right;

        camForward.y = 0;
        camRight.y = 0;
        camForward = camForward.normalized;
        camRight = camRight.normalized;
    }
}
