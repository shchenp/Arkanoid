using System;
using Interfaces;
using MessagePipe;
using Messages;
using UnityEngine;
using VContainer;

public class PlayerStats : MonoBehaviour
{
    private int _lives;
    private int _score;
    
    private IDataContainer _dataContainer;
    
    private IDisposable _enemyDiedSubscription;
    private IDisposable _outOfBoundsSubscription;
    private IPublisher<ScoreUpdatedMessage> _scorePublisher;
    private IPublisher<LivesUpdatedMessage> _livesPublisher;

    [Inject]
    private void Construct(IDataContainer dataContainer
        ,ISubscriber<EnemyDiedMessage> enemyDiedSubscriber,
        ISubscriber<OutOfBoundsMessage> outOfBoundsSubscriber,
        IPublisher<ScoreUpdatedMessage> scorePublisher,
        IPublisher<LivesUpdatedMessage> livesPublisher)
    {
        _dataContainer = dataContainer;
        
        _enemyDiedSubscription = enemyDiedSubscriber.Subscribe(message => UpdateScore(message.Points));
        _outOfBoundsSubscription = outOfBoundsSubscriber.Subscribe(_ => DecreaseLives());
        
        _scorePublisher = scorePublisher;
        _livesPublisher = livesPublisher;
    }

    private void Start()
    {
        _lives = _dataContainer.GetLives();
        _score = _dataContainer.GetScore();
        
        _livesPublisher.Publish(new LivesUpdatedMessage(_lives));
        _scorePublisher.Publish(new ScoreUpdatedMessage(_score));
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
        _outOfBoundsSubscription?.Dispose();
    }
}