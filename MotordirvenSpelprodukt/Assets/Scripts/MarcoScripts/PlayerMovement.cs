using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.HID;

public class PlayerMovement : MonoBehaviour
{
    public enum RotateMode
    {
        TwoStick,
        OneStick
    }

    public enum InputMode
    {
        Controller,
        MnK
    }

    [SerializeField] private RotateMode _currentRotateMode;
    [SerializeField] private InputMode _currentInputMode;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private Camera _mainCamera;

    private Player _playerScript;

    private CharacterController _characterController;

    Vector3 _moveDirection;

    private Vector3 _camForward;
    private Vector3 _camRight;
    private Vector3 _rotateDirection;

    private PlayerAnimation _playerAnimation;

    private bool _isMoving = false;

    private void Awake()
    {
        _playerAnimation = GetComponent<PlayerAnimation>();
        _playerScript = GetComponent<Player>();
    }

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _playerScript.OnChangeControllerTypeButtonPressed += PlayerScript_OnChangeControllerTypeButtonPressed;

    }

    private void PlayerScript_OnChangeControllerTypeButtonPressed(object sender, System.EventArgs e)
    {
        switch (_currentRotateMode)
        {
            case RotateMode.TwoStick:
                _currentRotateMode = RotateMode.OneStick;
                break;
            case RotateMode.OneStick:
                _currentRotateMode= RotateMode.TwoStick;
                break;
            default:
                break;
        }
    }

    void Update()
    {
        Move();
        switch (_currentRotateMode)
        {
            case RotateMode.TwoStick:
                RotateWithInput();
                break;
            case RotateMode.OneStick:
                Rotate();
                break;
            default:
                break;
        }

        _playerAnimation.Locomotion(_moveDirection, _rotateDirection);

    }

    private void Move()
    {
        GetCameraValues();

        Vector2 inputvector = _playerScript.GameInput.GetMovementVectorNormalized();
        _moveDirection = inputvector.x * _camRight + inputvector.y * _camForward;

        _characterController.SimpleMove(_moveDirection * _moveSpeed);

        _isMoving = _moveDirection != Vector3.zero;
    }

    private void Rotate()
    {
        if (_isMoving)
        {
            _rotateDirection = _moveDirection.x * _camRight + _moveDirection.y * _camForward;
            Quaternion currentRotation = transform.rotation;
            Quaternion newRotation = Quaternion.LookRotation(_moveDirection);
            transform.localRotation = Quaternion.Slerp(currentRotation, newRotation, _rotationSpeed * Time.deltaTime);
        }
    }

    private void RotateWithInput()
    {

        

        switch (_currentInputMode)
        {
            case InputMode.Controller:
                Vector3 input = _playerScript.GameInput.GetDirectionVectorNormalized();
                _rotateDirection = input.x * _camRight + input.y * _camForward;
                break;
            case InputMode.MnK:

                Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                Plane groundPlane = new Plane(Vector3.up, transform.position);
                float rayLength;

                if (groundPlane.Raycast(cameraRay, out rayLength))
                {
                    //Get the point in worldspace where we have our mouse and calculate the direction from the player
                    Vector3 pointToLook = cameraRay.GetPoint(rayLength);
                    _rotateDirection = pointToLook - transform.position;
                    _rotateDirection.Normalize();
                }

                break;
            default:
                break;
        }

        Quaternion currentRotation = transform.rotation;
        Quaternion newRotation = Quaternion.LookRotation(_rotateDirection);

        bool isRotating = currentRotation != newRotation && _rotateDirection != Vector3.zero;
        if (isRotating)
        {
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
