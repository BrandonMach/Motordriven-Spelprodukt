using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private GameInput _gameInput;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private PlayerAnimation _playerAnimation;
    [SerializeField] private Player _playerScript;

    private CharacterController _characterController;

    //Used for making the movement correct based on the cameras position
    private Vector3 _camForward;
    private Vector3 _camRight;

    private bool _isMoving = false; 
    
    private void Start()
    {
        _characterController = GetComponent<CharacterController>();

    }

    void Update()
    {
        Move();

    }

    private void Move()
    {
        GetCameraValues();

        Vector2 inputvector = _gameInput.GetMovementVectorNormalized();
        Vector3 moveDirection = inputvector.x * _camRight + inputvector.y * _camForward;
        _characterController.Move(moveDirection * _moveSpeed * Time.deltaTime);

  


        _isMoving = moveDirection != Vector3.zero;
        if (_isMoving)
        {
            Quaternion currentRotation = transform.rotation;
            Quaternion newRotation = Quaternion.LookRotation(moveDirection);
            transform.localRotation = Quaternion.Slerp(currentRotation, newRotation, _rotationSpeed * Time.deltaTime);
        }
        _playerAnimation.Animate(moveDirection);
    }
        
    private void GetCameraValues()
    {
        _camForward = _mainCamera.transform.forward;
        _camRight = _mainCamera.transform.right;

        _camForward.y = 0;
        _camRight.y = 0;
        _camForward = _camForward.normalized;
        _camRight = _camRight.normalized;
    }
}
