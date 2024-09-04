using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    public UnityEvent OnEnemyDeath;
    
    [SerializeField] private int _health;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (--_health <= 0)
        {
            gameObject.SetActive(false);
            
            OnEnemyDeath?.Invoke();
        }
    }
}