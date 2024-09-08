using System;
using Interfaces;
using MessagePipe;
using Messages;
using UnityEngine;
using VContainer;

public class BallController : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Vector3 _initialPosition;
    private Rigidbody2D _rigidbody;
    private bool _isGameStarted;
    private InputHandler _inputHandler;
    
    private IParent _parent;
    private IPublisher<OutOfBoundsMessage> _outPublisher;
    private IDisposable _subscription;

    [Inject]
    private void Construct(IParent parent, IPublisher<OutOfBoundsMessage> outPublisher,
        ISubscriber<LivesUpdatedMessage> livesSubscriber)
    {
        _inputHandler = new InputHandler();
        _outPublisher = outPublisher;
        _subscription = livesSubscriber.Subscribe(message => TryResetPosition(message.Lives));

        _parent = parent;
    }

    private void Awake()
    {
        _initialPosition = transform.localPosition;
        
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

    private void TryResetPosition(int lives)
    {
        if (lives > 0)
        {
            _rigidbody.velocity = new Vector2(0, 0);
        
            transform.SetParent(_parent.GetTransform());
            transform.localPosition = _initialPosition;
        
            _isGameStarted = false;
            _rigidbody.isKinematic = true;   
        }
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag(GlobalConstants.OUTER_BOUND))
        {
            _outPublisher.Publish(new OutOfBoundsMessage());
        }
    }

    private void OnDestroy()
    {
        _subscription?.Dispose();
    }
}