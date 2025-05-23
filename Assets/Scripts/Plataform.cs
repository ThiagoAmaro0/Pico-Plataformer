using UnityEngine;

public class Plataform : MonoBehaviour
{
    [SerializeField] private Collider2D _collider;

    private Collider2D _playerCollider;
    private bool _canCollideWithPlayer;
    private bool _playerIsInside;

    void Awake()
    {
        _playerCollider = FindAnyObjectByType<PlayerMovement>().GetComponent<Collider2D>();
    }

    void Update()
    {
        if (_canCollideWithPlayer)
        {
            if (transform.position.y >= _playerCollider.transform.position.y)
            {
                _canCollideWithPlayer = false;
                _collider.isTrigger = true;
            }
        }
        else
        {
            if (transform.position.y <= _playerCollider.transform.position.y)
            {
                _canCollideWithPlayer = true;
                if (!_playerIsInside)
                    _collider.isTrigger = false;

            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider == _playerCollider)
        {
            _playerIsInside = true;
        }
    }
    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider == _playerCollider)
        {
            _playerIsInside = false;
            if (_canCollideWithPlayer)
                _collider.isTrigger = false;
        }
    }
}
