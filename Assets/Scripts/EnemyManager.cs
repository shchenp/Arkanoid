using System;
using MessagePipe;
using UnityEngine;
using VContainer;

public class EnemyManager : MonoBehaviour
{
    private int _enemyCount;
    private IDisposable _subscription;

    [Inject]
    private void Construct(ISubscriber<EnemyDiedMessage> enemyDiedSubscriber)
    {
        _subscription = enemyDiedSubscriber.Subscribe(_ => ReduceEnemyCount());
        
        _enemyCount = transform.childCount;
    }

    public void ReduceEnemyCount()
    {
        _enemyCount--;
    }

    private void OnDestroy()
    {
        _subscription?.Dispose();
    }
}