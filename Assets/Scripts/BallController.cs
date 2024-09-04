using UnityEngine;
using VContainer;

public class BallController : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Vector3 _initialPosition;
    private Rigidbody2D _rigidbody;
    private bool _isGameStarted;
    private InputHandler _inputHandler;

    [Inject]
    private void Construct()
    {
        _inputHandler = new InputHandler();
    }

    private void Awake()
    {
        _initialPosition = transform.position;
        
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.isKinematic = true;
    }

    private void Update()
    {
        if (!_isGameStarted && _inputHandler.IsSpacePressed())
        {
            StartGame();
        }
    }

    private void StartGame()
    {
        transform.SetParent(null);
        _isGameStarted = true;
        _rigidbody.isKinematic = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(GlobalConstants.PLAYER_TAG))
        {
            OnPlayerCollision(collision);
        }
    }

    private void OnPlayerCollision(Collision2D player)
    {
        var playerPosition = player.transform.position;
        var ballPosition = transform.position;

        var offset = ballPosition.x - playerPosition.x;
        var playerWidth = player.collider.bounds.size.x;
        
        var newDirectionX = offset / playerWidth;
        var newDirection = new Vector2(newDirectionX, 1).normalized;

        _rigidbody.velocity = newDirection * _speed;
    }
}