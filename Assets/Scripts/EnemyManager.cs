using System;
using System.Collections.Generic;
using Bonuses;
using MessagePipe;
using Messages;
using UnityEngine;
using VContainer;
using Random = UnityEngine.Random;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private List<Enemy> _enemies;
    [SerializeField] private List<Bonus> _bonusesPrefabs;
    [SerializeField] private Color _bonusIndicatorColor;
    
    private int _enemyCount;
    private int _totalEnemyCount;
    
    private IDisposable _subscription;
    private IPublisher<EnemyCountUpdatedMessage> _enemyCountPublisher;
    private IPublisher<LevelPassedMessage> _winGamePublisher;

    [Inject]
    private void Construct(ISubscriber<EnemyDiedMessage> enemyDiedSubscriber,
        IPublisher<EnemyCountUpdatedMessage> enemyCountPublisher,
        IPublisher<LevelPassedMessage> winGamePublisher)
    {
        _subscription = enemyDiedSubscriber.Subscribe(_ => ReduceEnemyCount());
        _enemyCountPublisher = enemyCountPublisher;
        _winGamePublisher = winGamePublisher;
        
        _totalEnemyCount = _enemies.Count;
        _enemyCount = _totalEnemyCount;
    }

    private void Start()
    {
        _enemyCountPublisher.Publish(new EnemyCountUpdatedMessage(_enemyCount, _totalEnemyCount));

        SetBonuses();
    }

    private void SetBonuses()
    {
        var bonusesCount = _bonusesPrefabs.Count;
        
        for (int i = 0; i < bonusesCount; i++)
        {
            var randomEnemyIndex = Random.Range(0, _totalEnemyCount + 1);
            var enemy = _enemies[randomEnemyIndex];

            if (enemy.IsContainBonus)
            {
                i--;
                continue;
            }
            
            enemy.SetBonus(_bonusesPrefabs[i], _bonusIndicatorColor);
        }
    }

    public void ReduceEnemyCount()
    {
        _enemyCount--;
        
        _enemyCountPublisher.Publish(new EnemyCountUpdatedMessage(_enemyCount, _totalEnemyCount));

        if (_enemyCount == 0)
        {
            _winGamePublisher.Publish(new LevelPassedMessage());
        }
    }

    private void OnDestroy()
    {
        _subscription?.Dispose();
    }
}