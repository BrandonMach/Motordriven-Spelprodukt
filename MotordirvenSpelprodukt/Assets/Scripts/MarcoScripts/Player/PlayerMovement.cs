using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.HID;

public class PlayerMovement : MonoBehaviour
{



    public Vector3 MoveDirection {  get; private set; }

    public float MoveSpeed { get { return _moveSpeed; } private set { _moveSpeed = value; } }

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _attackDashForce = 1.5f;
    [SerializeField] private Camera _mainCamera;

    //private Player _playerScript;

    private Rigidbody _rigidbody;

    private Vector3 _camForward;
    private Vector3 _camRight;
    private Vector3 _rotateInputDirection;

    private PlayerAnimation _playerAnimation;

    private bool _isMoving = false;
    public bool _canMove = true;

    private void Awake()
    {
        _playerAnimation = GetComponent<PlayerAnimation>();
        Player.Instance = GetComponent<Player>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Player.Instance.DisableMovement += PlayerScript_OnDisableMovement;
        Player.Instance.EnableMovement += PlayerScript_OnEnableMovement;
    }

    public void AttackDash()
    {
        _rigidbody.AddForce(transform.forward * _attackDashForce, ForceMode.Impulse);
    }

    private void PlayerScript_OnEnableMovement(object sender, System.EventArgs e)
    {
        _canMove = true;
    }

    private void PlayerScript_OnDisableMovement(object sender, System.EventArgs e)
    {
        if (!Player.Instance.IsDashing)
        {
            _rigidbody.velocity = Vector3.zero;
        }
        _canMove = false;

    }


    void Update()
    {
        if (GameLoopManager.Instance != null)
        {
            if (GameLoopManager.Instance.MatchIsFinished)
            {
                _canMove = false;

            }
        }

        if (_canMove)
        {
            GetMoveDir();
        }
        

        _playerAnimation.Locomotion(MoveDirection, _rotateInputDirection);
    }

    private void FixedUpdate()
    {
        if (_canMove)
        {
            Move();
            SetRotateDirectionTowardsMovement();
        }
    }

    private void GetMoveDir()
    {
        GetCameraValues();

        Vector2 inputvector = Player.Instance.GameInput.GetMovementVectorNormalized();
       // Vector2 inputvector = GameManager.Instance.gameObject.GetComponent<GameInput>().GetMovementVectorNormalized();

        if (!_canMove)
        {
            MoveDirection = Vector3.zero;
        }
        else
        {
            MoveDirection = inputvector.x * _camRight + inputvector.y * _camForward;
        }
    }

    private void Move()
    {
        _rigidbody.velocity = new Vector3(MoveDirection.x * _moveSpeed, _rigidbody.velocity.y, MoveDirection.z * _moveSpeed);
        _isMoving = MoveDirection != Vector3.zero;
    }

    private void SetRotateDirectionTowardsMovement()
    {
        if (_isMoving)
        {
            _rotateInputDirection = MoveDirection.x * _camRight + MoveDirection.y * _camForward;
            Quaternion newRotation = Quaternion.LookRotation(MoveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, _rotationSpeed * Time.fixedDeltaTime);
        }
    }

    //private void RotateWithInput()
    //{
    //    switch (_currentInputMode)
    //    {
    //        case InputMode.Controller:
    //            Vector3 input = Player.Instance.GameInput.GetDirectionVectorNormalized();
    //            _rotateInputDirection = input.x * _camRight + input.y * _camForward;
    //            break;
    //        case InputMode.MnK:

    //            Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
    //            Plane groundPlane = new Plane(Vector3.up, transform.position);
    //            float rayLength;

    //            if (groundPlane.Raycast(cameraRay, out rayLength))
    //            {
    //                //Get the point in worldspace where we have our mouse and calculate the direction from the player
    //                Vector3 pointToLook = cameraRay.GetPoint(rayLength);
    //                _rotateInputDirection = pointToLook - transform.position;
    //                _rotateInputDirection.Normalize();
    //            }
    //            break;
    //        default:
    //            break;
    //    }

    //    _newRotation = Quaternion.LookRotation(_rotateInputDirection);

    //    bool isRotating = transform.rotation != _newRotation && _rotateInputDirection != Vector3.zero;
    //    if (isRotating)
    //    {
    //        Rotate();
    //    }
    //}

    private void Rotate()
    {
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

    private void OnDestroy()
    {
        Player.Instance.DisableMovement -= PlayerScript_OnDisableMovement;
        Player.Instance.EnableMovement -= PlayerScript_OnEnableMovement;
    }
}
