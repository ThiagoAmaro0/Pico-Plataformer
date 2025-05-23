using System;
using System.Collections;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    [SerializeField] private float _jumpForce;
    [SerializeField] private Animator _animator;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out PlayerMovement player))
        {
            player.ForceJump(_jumpForce);
            _animator.SetTrigger("Jump");
        }
    }
}
