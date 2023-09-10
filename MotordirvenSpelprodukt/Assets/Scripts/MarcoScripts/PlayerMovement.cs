using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    public enum InputMode
    {
        TwoStick,
        OneStick
    }

    [SerializeField] private InputMode _currentInputMode;

    [SerializeField] private GameInput _gameInput;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private PlayerAnimation _playerAnimation;

    private CharacterController _characterController;

    Vector3 _moveDirection;

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
        switch (_currentInputMode)
        {
            case InputMode.TwoStick:
                RotateWithInput();
                break;
            case InputMode.OneStick:
                Rotate();
                break;
            default:
                break;
        }

    }

    private void Move()
    {
        GetCameraValues();

        Vector2 inputvector = _gameInput.GetMovementVectorNormalized();
        _moveDirection = inputvector.x * _camRight + inputvector.y * _camForward;
        _characterController.Move(_moveDirection * _moveSpeed * Time.deltaTime);

        _isMoving = _moveDirection != Vector3.zero;
     
        _playerAnimation.Animate(_moveDirection);

    }

    private void Rotate()
    {
        if (_isMoving)
        {
            Quaternion currentRotation = transform.rotation;
            Quaternion newRotation = Quaternion.LookRotation(_moveDirection);
            transform.localRotation = Quaternion.Slerp(currentRotation, newRotation, _rotationSpeed * Time.deltaTime);
        }
    }

    private void RotateWithInput()
    {
        if (_isMoving)
        {
            Vector2 inputVector = _gameInput.GetDirectionVectorNormalized();
            _moveDirection = inputVector.x * _camRight + inputVector.y * _camForward;

            Quaternion currentRotation = transform.rotation;
            Quaternion newRotation = Quaternion.LookRotation(_moveDirection);
            transform.localRotation = Quaternion.Slerp(currentRotation, newRotation, _rotationSpeed * Time.deltaTime);
        }
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
