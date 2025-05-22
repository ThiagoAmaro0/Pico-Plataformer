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

    [Header("Ground Raycast Configuration")]
    [SerializeField] private LayerMask _groundLayerMask;
    [SerializeField] private float _raycastDistance;

    private MovementState _state;
    private Vector2 _moveInput;
    private bool _requestJump;
    private float _timeSinceGround;

    public Action<MovementState> OnChangeState;

    public Vector2 MoveInput { get => _moveInput; }

    void FixedUpdate()
    {
        _rigidbody.linearVelocityX = _moveInput.x * _walkSpeed;

        if (_requestJump)
        {
            if (Time.time < _timeSinceGround + _coyoteTime)
            {
                _rigidbody.linearVelocityY = _jumpForce;
                SetState(MovementState.JUMPING);
            }
        }
        if (IsGrounded())
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
    }

    private void SetState(MovementState state)
    {
        if (_state != state)
        {
            _state = state;
            OnChangeState?.Invoke(state);
        }
    }

    public bool IsGrounded()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, _raycastDistance, _groundLayerMask);
    }

    private void OnMove(InputValue input)
    {
        _moveInput = input.Get<Vector2>();
    }

    private void OnJump(InputValue input)
    {
        _requestJump = input.isPressed;
    }

    public enum MovementState
    {
        IDLE,
        WALKING,
        JUMPING,
        FALLING
    }

}
