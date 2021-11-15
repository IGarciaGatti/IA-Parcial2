using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDefeatState<T> : State<T>
{
    private Enemy enemy;

    public EnemyDefeatState(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public override void Awake()
    {
        enemy.Defeat();
    }
}
