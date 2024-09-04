using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _health;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (--_health <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}