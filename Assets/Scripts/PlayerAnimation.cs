using System;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private SpriteRenderer _visual;
    [SerializeField] private Animator _animator;

    void OnEnable()
    {
        _playerMovement.OnChangeState += OnChangeMovementState;
    }

    void OnDisable()
    {
        _playerMovement.OnChangeState -= OnChangeMovementState;
    }

    void Update()
    {
        if (_playerMovement.MoveInput.x > 0)
        {
            _visual.flipX = false;
        }
        else if (_playerMovement.MoveInput.x < 0)
        {
            _visual.flipX = true;
        }
    }

    private void OnChangeMovementState(PlayerMovement.MovementState state)
    {
        switch (state)
        {
            case PlayerMovement.MovementState.IDLE:
                _animator.SetBool("IsWalking", false);
                _animator.SetBool("IsGrounded", true);
                break;
            case PlayerMovement.MovementState.WALKING:
                _animator.SetBool("IsWalking", true);
                _animator.SetBool("IsGrounded", true);
                break;
            case PlayerMovement.MovementState.JUMPING:
                _animator.SetBool("IsGrounded", false);
                break;
            case PlayerMovement.MovementState.FALLING:
                _animator.SetBool("IsGrounded", false);
                break;
        }
    }
}
