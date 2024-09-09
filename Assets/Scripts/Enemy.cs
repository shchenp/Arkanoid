using Bonuses;
using MessagePipe;
using Messages;
using UnityEngine;
using VContainer;

public class Enemy : MonoBehaviour
{
    public bool IsContainBonus { get; private set; }
    
    [SerializeField] private int _health;
    [SerializeField] private int _points;
    [SerializeField] private SpriteRenderer _bonusIndicator;

    private Bonus _bonusPrefab;
    private IPublisher<EnemyDiedMessage> _enemyDiedPublisher;

    [Inject]
    private void Construct(IPublisher<EnemyDiedMessage> enemyDiedPublisher)
    {
        _enemyDiedPublisher = enemyDiedPublisher;
    }

    public void SetBonus(Bonus bonus, Color bonusIndicatorColor)
    {
        IsContainBonus = true;
        
        _bonusPrefab = bonus;
        _bonusIndicator.color = bonusIndicatorColor;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (--_health <= 0)
        {
            if (IsContainBonus)
            {
                var bonus = Instantiate(_bonusPrefab);
                bonus.Initialize(transform.position);
            }
            
            gameObject.SetActive(false);
            _enemyDiedPublisher.Publish(new EnemyDiedMessage(_points));
        }
    }
}