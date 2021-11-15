using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private List<Enemy> enemies;
    [SerializeField] private Counter enemyCounter;

    public delegate void EnemyManagerDelegate();
    public EnemyManagerDelegate OnGoalCompletion;

    void Start()
    {
        SetCounter();
    }

    private void SetCounter()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].OnDefeat += OnEnemiesDefeated;
        }        
        enemyCounter.StartAmount(0, enemies.Count);
    }

    private void OnEnemiesDefeated()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i].IsDefeated)
            {
                enemies[i].OnDefeat -= OnEnemiesDefeated;
            }
        }
        enemyCounter.UpdateAmount(1);
        CheckLimit();
    }

    private void CheckLimit()
    {
        if(enemyCounter.LimitReached)
        {
            OnGoalCompletion();
        }
    }
}
