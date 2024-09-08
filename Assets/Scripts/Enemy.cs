using MessagePipe;
using Messages;
using UnityEngine;
using VContainer;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private int _points;
    
    private IPublisher<EnemyDiedMessage> _enemyDiedPublisher;

    [Inject]
    private void Construct(IPublisher<EnemyDiedMessage> enemyDiedPublisher)
    {
        _enemyDiedPublisher = enemyDiedPublisher;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (--_health <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        _enemyDiedPublisher.Publish(new EnemyDiedMessage(_points));
    }
}