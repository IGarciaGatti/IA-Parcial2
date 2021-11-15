using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private EnemyManager enemyManager;
    [SerializeField] private HUDManager hudManager;
    
    void Start()
    {
        enemyManager.OnGoalCompletion += hudManager.Victory;
        hudManager.OnHUDEnter += player.HUDEnter;
        hudManager.OnHUDExit += player.HUDExit;
        player.OnDefeat += hudManager.Defeat;
    }
   
}
