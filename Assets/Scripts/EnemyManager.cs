using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private int _enemyCount;

    private void Awake()
    {
        _enemyCount = transform.childCount;
    }

    public void ReduceEnemyCount()
    {
        _enemyCount--;
    }
}
