using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Rigidbody2D _rigidbody;

    [Header("Movement Configuration")]
    [SerializeField] private float _walkSpeed = 4;
    [SerializeField] private float _jumpForce = 10;
    [SerializeField] private float _coyoteTime = 0.5f;
    [SerializeField] private float _jumpDuration = 0.5f;

    [Header("Ground Raycast Configuration")]
    [SerializeField] private LayerMask _groundLayerMask;
    [SerializeField] private float _raycastDistance;
    [SerializeField] private float _xRaycastOffset = 0.1f;

    private MovementState _state;
    private Vector2 _moveInput;
    private bool _requestJump;
    private bool _jumpThisFrame;
    private float _timeSinceGround;
    private float _timeSinceJumped;

    public Action<MovementState> OnChangeState;

    public Vector2 MoveInput { get => _moveInput; }

    void Update()
    {
        _rigidbody.linearVelocityX = _moveInput.x * _walkSpeed;

        if (_requestJump)
        {
            if (IsGrounded())
            {
                if (_jumpThisFrame)
                {
                    if (_rigidbody.linearVelocityY < _jumpForce)
                    {
                        _rigidbody.linearVelocityY = _jumpForce;
                        SetState(MovementState.JUMPING);
                    }
                }
            }
            else if (_state != MovementState.JUMPING)
            {
                if (Time.time < _timeSinceGround + _coyoteTime)
                {
                    if (_jumpThisFrame)
                    {
                        if (_rigidbody.linearVelocityY < _jumpForce)
                        {
                            _rigidbody.linearVelocityY = _jumpForce;
                            SetState(MovementState.JUMPING);
                        }
                    }
                }
            }
            else if (Time.time < _timeSinceJumped + _jumpDuration)
            {
                if (_rigidbody.linearVelocityY < _jumpForce)
                    _rigidbody.linearVelocityY = _jumpForce;
            }
        }
    }

    void LateUpdate()
    {
        if (IsGrounded() && _state != MovementState.JUMPING)
        {
            if (_moveInput.x == 0)
            {
                SetState(MovementState.IDLE);
            }
            else
            {
                SetState(MovementState.WALKING);
            }
            _timeSinceGround = Time.time;
        }
        else if (_rigidbody.linearVelocityY < 0)
        {
            SetState(MovementState.FALLING);
        }
        _jumpThisFrame = false;
    }

    private void SetState(MovementState state)
    {
        if (_state != state)
        {
            _state = state;
            if (_state == MovementState.JUMPING)
            {
                _timeSinceJumped = Time.time;
            }
            OnChangeState?.Invoke(state);
        }
    }

    public bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(_xRaycastOffset, 0, 0), Vector2.down, _raycastDistance, _groundLayerMask);
        if (hit)
        {
            return !hit.collider.isTrigger;
        }

        hit = Physics2D.Raycast(transform.position + new Vector3(-_xRaycastOffset, 0, 0), Vector2.down, _raycastDistance, _groundLayerMask);
        if (hit)
        {
            return !hit.collider.isTrigger;
        }
        return false;
    }

    private void OnMove(InputValue input)
    {
        _moveInput = input.Get<Vector2>();
    }

    private void OnJump(InputValue input)
    {
        _requestJump = input.isPressed;
        _jumpThisFrame = input.isPressed;
    }

    public void ForceJump(float velocity)
    {
        _rigidbody.linearVelocityY = velocity;
        SetState(MovementState.JUMPING);
    }

    public enum MovementState
    {
        IDLE,
        WALKING,
        JUMPING,
        FALLING
    }

}
