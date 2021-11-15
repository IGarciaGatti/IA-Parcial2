using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolState<T> : State<T>
{
    private Enemy enemy;
    private ISteering steering;
    private INode root;

    public EnemyPatrolState(Enemy enemy, ISteering steering, INode root)
    {
        this.enemy = enemy;
        this.steering = steering;
        this.root = root;
    }

    public override void Execute()
    {
        Vector3 direction = steering.GetDirection();
        if(direction == Vector3.zero)
        {
            root.Execute();
        }
        else
        {
            enemy.Move(direction);
        }
    }

}
