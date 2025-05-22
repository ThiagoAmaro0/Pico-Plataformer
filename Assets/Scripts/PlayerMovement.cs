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

    private Vector2 _moveInput;
    private bool _requestJump;
    private float _timeSinceGround;

    void FixedUpdate()
    {
        _rigidbody.linearVelocityX = _moveInput.x * _walkSpeed;

        if (_requestJump)
        {
            if (Time.time < _timeSinceGround + _coyoteTime)
            {
                _rigidbody.linearVelocityY = _jumpForce;
            }
        }
        if (IsGrounded())
        {
            _timeSinceGround = Time.time;
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

}
