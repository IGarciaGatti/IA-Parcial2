using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPursuitState<T> : State<T>
{
    private Enemy enemy;
    private Transform target;
    private ISteering steering;
    private Roulette roulette;
    private Dictionary<bool, float> items;
    private float cooldown;
    private float currentCooldown;

    public EnemyPursuitState(Enemy enemy, Transform target, ISteering steering, float cooldown)
    {
        this.enemy = enemy;
        this.target = target;
        this.steering = steering;
        roulette = new Roulette();
        SetItems();
        this.cooldown = cooldown;
        currentCooldown = cooldown;
    }

    public override void Execute()
    {
        currentCooldown -= Time.deltaTime;
        Pursuit();             
    }

    private void SetItems()
    {
        items = new Dictionary<bool, float>();
        items[true] = 50;
        items[false] = 50;
    }

    private void Pursuit()
    {
        if(IsInRange() || roulette.Run(items))
        {           
            Shoot();                   
        }
        else
        {           
            enemy.Move(steering.GetDirection());
        }
    }

    private void Shoot()
    {
        if(currentCooldown <= 0)
        {
            Vector3 direction = target.position - enemy.transform.position;
            direction = direction.normalized;
            direction.y = 0;
            enemy.Shoot(direction);
            currentCooldown = cooldown;
        }       
    }

    private bool IsInRange()
    {
        return Vector3.Distance(enemy.transform.position, target.position) < 5;
    }
}
