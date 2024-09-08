using System;
using MessagePipe;
using Messages;
using UnityEngine;
using VContainer;

public class EnemyManager : MonoBehaviour
{
    private int _enemyCount;
    private int _totalEnemyCount;
    
    private IDisposable _subscription;
    private IPublisher<EnemyCountUpdatedMessage> _enemyCountPublisher;

    [Inject]
    private void Construct(ISubscriber<EnemyDiedMessage> enemyDiedSubscriber, IPublisher<EnemyCountUpdatedMessage> enemyCountPublisher)
    {
        _subscription = enemyDiedSubscriber.Subscribe(_ => ReduceEnemyCount());
        _enemyCountPublisher = enemyCountPublisher;
        
        _totalEnemyCount = transform.childCount;
        _enemyCount = _totalEnemyCount;
    }

    private void Start()
    {
        _enemyCountPublisher.Publish(new EnemyCountUpdatedMessage(_enemyCount, _totalEnemyCount));
    }

    public void ReduceEnemyCount()
    {
        _enemyCount--;
        
        _enemyCountPublisher.Publish(new EnemyCountUpdatedMessage(_enemyCount, _totalEnemyCount));
    }

    private void OnDestroy()
    {
        _subscription?.Dispose();
    }
}