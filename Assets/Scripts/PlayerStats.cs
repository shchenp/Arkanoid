using System;
using MessagePipe;
using Messages;
using UnityEngine;
using VContainer;

public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    private int _lives;

    private int _score;
    
    private IDisposable _enemyDiedSubscription;
    private IDisposable _outOfBoundsSubscriber;
    private IPublisher<ScoreUpdatedMessage> _scorePublisher;
    private IPublisher<LivesUpdatedMessage> _livesPublisher;

    [Inject]
    private void Construct(ISubscriber<EnemyDiedMessage> enemyDiedSubscriber,
        ISubscriber<OutOfBoundsMessage> outOfBoundsSubscriber,
        IPublisher<ScoreUpdatedMessage> scorePublisher,
        IPublisher<LivesUpdatedMessage> livesPublisher)
    {
        _enemyDiedSubscription = enemyDiedSubscriber.Subscribe(message => UpdateScore(message.Points));
        _outOfBoundsSubscriber = outOfBoundsSubscriber.Subscribe(_ => DecreaseLives());
        
        _scorePublisher = scorePublisher;
        _livesPublisher = livesPublisher;
    }

    private void Start()
    {
        _livesPublisher.Publish(new LivesUpdatedMessage(_lives));
    }

    private void DecreaseLives()
    {
        _lives--;
        _livesPublisher.Publish(new LivesUpdatedMessage(_lives));
        
        if (_lives <= 0)
        {
            // Game Over
            Debug.Log("Game Over");
        }
    }

    private void UpdateScore(int points)
    {
        _score += points;
        
        _scorePublisher.Publish(new ScoreUpdatedMessage(_score));
    }
    
    private void OnDestroy()
    {
        _enemyDiedSubscription?.Dispose();
        _outOfBoundsSubscriber?.Dispose();
    }
}