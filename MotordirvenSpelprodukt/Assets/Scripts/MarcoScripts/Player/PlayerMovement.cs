using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    public Vector3 _moveDirection {  get; private set; }


    [SerializeField] private RotateMode _currentRotateMode;
    [SerializeField] private InputMode _currentInputMode;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private Camera _mainCamera;

    private Player _playerScript;

    private Rigidbody _rigidbody;

    private CharacterController _characterController;


    private Vector3 _camForward;
    private Vector3 _camRight;
    private Vector3 _rotateInputDirection;

    private PlayerAnimation _playerAnimation;

    private Quaternion _newRotation;

    private bool _isMoving = false;
    private bool _canMove = true;

    private void Awake()
    {
        _playerAnimation = GetComponent<PlayerAnimation>();
        _playerScript = GetComponent<Player>();
        _characterController = GetComponent<CharacterController>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        
        _playerScript.ChangeControllerTypeButtonPressed += PlayerScript_OnChangeControllerTypeButtonPressed;
        _playerScript.DisableMovement += PlayerScript_OnDisableMovement;
        _playerScript.EnableMovement += PlayerScript_OnEnableMovement;

    }

    private void PlayerScript_OnEnableMovement(object sender, System.EventArgs e)
    {
        _canMove = true;
    }

    private void PlayerScript_OnDisableMovement(object sender, System.EventArgs e)
    {
        if (!_playerScript.IsDashing)
        {
            _rigidbody.velocity = Vector3.zero;
        }
        _canMove = false;

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
 
        GetMoveDir();
        
        _playerAnimation.Locomotion(_moveDirection, _rotateInputDirection);
    }

    private void FixedUpdate()
    {
        if (_canMove)
        {
            Move();


            switch (_currentRotateMode)
            {
                case RotateMode.TwoStick:
                    RotateWithInput();
                    break;
                case RotateMode.OneStick:
                    //RotateTowardsMovement();
                    SetRotateDirectionTowardsMovement();
                    break;
                default:
                    break;
            }
        }
    }

    private void GetMoveDir()
    {
        GetCameraValues();

        Vector2 inputvector = _playerScript.GameInput.GetMovementVectorNormalized();

        if (!_canMove)
        {
            _moveDirection = Vector3.zero;
        }
        else
        {
            _moveDirection = inputvector.x * _camRight + inputvector.y * _camForward;
        }
    }

    private void Move()
    {
        _rigidbody.velocity = new Vector3(_moveDirection.x * _moveSpeed, _rigidbody.velocity.y, _moveDirection.z * _moveSpeed);
        _isMoving = _moveDirection != Vector3.zero;
    }

    private void SetRotateDirectionTowardsMovement()
    {
        if (_isMoving)
        {
            _rotateInputDirection = _moveDirection.x * _camRight + _moveDirection.y * _camForward;
            _newRotation = Quaternion.LookRotation(_moveDirection); 
            Rotate();
        }
    }

    private void RotateWithInput()
    {
        switch (_currentInputMode)
        {
            case InputMode.Controller:
                Vector3 input = _playerScript.GameInput.GetDirectionVectorNormalized();
                _rotateInputDirection = input.x * _camRight + input.y * _camForward;
                break;
            case InputMode.MnK:

                Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                Plane groundPlane = new Plane(Vector3.up, transform.position);
                float rayLength;

                if (groundPlane.Raycast(cameraRay, out rayLength))
                {
                    //Get the point in worldspace where we have our mouse and calculate the direction from the player
                    Vector3 pointToLook = cameraRay.GetPoint(rayLength);
                    _rotateInputDirection = pointToLook - transform.position;
                    _rotateInputDirection.Normalize();
                }
                break;
            default:
                break;
        }

        _newRotation = Quaternion.LookRotation(_rotateInputDirection);

        bool isRotating = transform.rotation != _newRotation && _rotateInputDirection != Vector3.zero;
        if (isRotating)
        {
            Rotate();
        }
    }

    private void Rotate()
    {
         transform.rotation = Quaternion.Slerp(transform.rotation, _newRotation, _rotationSpeed * Time.fixedDeltaTime);
    }
    public bool IsMoving()
    {
        return _isMoving;
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
