using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState<T> : State<T>
{
    private Enemy enemy;
    private float cooldownTime;
    private T input;
    private INode root;
    private float counter;

    public EnemyIdleState(Enemy enemy, float cooldownTime, INode root)
    {
        this.enemy = enemy;
        this.cooldownTime = cooldownTime;
        this.root = root;
    }

    public override void Awake()
    {
        enemy.Stop();
        counter = 0;
    }

    public override void Execute()
    {
        counter += Time.deltaTime;
        if (counter >= cooldownTime && input != null)
        {
            root.Execute();
        }
    }
}
