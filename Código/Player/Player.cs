using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IMove, IVelocity
{
    [SerializeField] private float minHealth;
    [SerializeField] private float maxHealth;    
    [SerializeField] private float minEnergy;
    [SerializeField] private float maxEnergy;
    [SerializeField] private float regenerationSpeed;
    [SerializeField] private float energyRechargeTime;
    [SerializeField] private float shootTimeOffset;
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private Bullet bullet;
    [SerializeField] private int enemyBulletLayer;
    [SerializeField] private Transform followTarget;
    [SerializeField] private Transform firePoint;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private EnergyBar energyBar;
    private HealthComponent health;
    private EnergyComponent energy;
    private Rigidbody rigidBody;
    private PlayerAnimation playerAnimation;
    
    public delegate void PlayerDelegate();
    public PlayerDelegate OnHUDEnter;
    public PlayerDelegate OnHUDExit;
    public PlayerDelegate OnDefeat;

    public float Velocity => rigidBody.velocity.magnitude;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        playerAnimation = GetComponent<PlayerAnimation>();
        health = new HealthComponent(minHealth, maxHealth, true, regenerationSpeed, healthBar);
        energy = new EnergyComponent(minEnergy, maxEnergy, energyRechargeTime, energyBar);
    }

    void Update()
    {
        health.Update();
        energy.Update();
    }

    public void Move(Vector3 direction)
    {
        direction.y = 0;
        rigidBody.AddRelativeForce(direction * speed);
        playerAnimation.RunAnimation(rigidBody.velocity.magnitude);
    }

    public void Rotate(Vector3 rotation)
    {
        rigidBody.rotation = Quaternion.Euler(0, rotation.y, 0);
    }

    public void Aim(bool condition)
    {
        playerAnimation.AimAnimation(condition);
    }

    public void Shoot()
    {
        if (energy.HasEnoughEnergy())
        {
            Bullet firedBullet = Instantiate(bullet);
            firedBullet.Fire(firePoint, followTarget.forward);
            energy.UseEnergy(20f);
        }       
    }

    public void Stop()
    {
        rigidBody.velocity = Vector3.zero;
        playerAnimation.RunAnimation(rigidBody.velocity.magnitude);
    }

    public void HUDEnter()
    {
        OnHUDEnter();
    }

    public void HUDExit()
    {
        OnHUDExit();
    }

    public void Defeat()
    {
        playerAnimation.DefeatAnimation(true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.layer == enemyBulletLayer)
            {
                health.DamageTaken(20);
                if (health.IsHealthDepleted())
                {
                    OnDefeat();
                }
            }
        }
    }
}
