using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IMove
{
    [SerializeField] private float minHealth;
    [SerializeField] private float maxHealth;   
    [SerializeField] private float speed;
    private Rigidbody rigidBody;
    private bool isPursuing;
    [SerializeField] private float range;
    [SerializeField] private float angle;
    [SerializeField] private Player player;
    [SerializeField] private Bullet bullet;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float shootTimeOffset;
    [SerializeField] private int playerBulletLayer;
    [SerializeField] private LayerMask mask;
    [SerializeField] private LayerMask targetMask;

    private HealthComponent health;
    private EnemyAnimation enemyAnimation;
    private bool isDefeated;

    public bool IsDefeated => isDefeated;

    public delegate void EnemyDelegate();
    public EnemyDelegate OnPlayerInSight;
    public EnemyDelegate OnDefeat;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        enemyAnimation = GetComponent<EnemyAnimation>();
        health = new HealthComponent(minHealth, maxHealth, false, 0f);
    }

    private void Update()
    {
        if (IsInSight(player.transform))
        {
            OnPlayerInSight();
        }       
    }

    public void Move(Vector3 direction)
    {
        direction.y = 0;
        transform.forward = Vector3.Lerp(transform.forward, direction, 0.2f);
        rigidBody.velocity = direction * speed;
        enemyAnimation.WalkAnimation(rigidBody.velocity.magnitude);
    }

    public void Stop()
    {
        rigidBody.velocity = Vector3.zero;
        enemyAnimation.WalkAnimation(rigidBody.velocity.magnitude);
    }

    public void Shoot(Vector3 direction)
    {
        enemyAnimation.ShootAnimation(true);
        StartCoroutine(ShootRoutine(direction));            
    }

    public void Defeat()
    {
        isDefeated = true;
        enemyAnimation.DefeatAnimation(isDefeated);
    }

    public bool IsInSight(Transform target)
    {
        Vector3 diff = target.position - transform.position;
        float distance = diff.magnitude;
        if (distance > range) return false;

        Vector3 front = transform.forward;

        if (!InAngle(diff, front)) return false;

        if (!IsInView(diff.normalized, distance, mask)) return false;

        return true;
    }

    bool InAngle(Vector3 from, Vector3 to)
    {
        float angleToTarget = Vector3.Angle(from, to);
        return angleToTarget < angle / 2;
    }

    bool IsInView(Vector3 dirToTarget, float distance, LayerMask maskObstacle)
    {
        return !Physics.Raycast(transform.position, dirToTarget, distance, maskObstacle);
    }

    public List<Transform> LineOfSight()
    {
        var list = new List<Transform>();
        Collider[] colliders = Physics.OverlapSphere(transform.position, range, targetMask);

        if (colliders.Length == 0) return list;
        Vector3 front = transform.forward;
        for (int i = 0; i < colliders.Length; i++)
        {
            Collider current = colliders[i];
            Vector3 diff = current.transform.position - transform.position;
            if (!InAngle(diff, front)) continue;
            float distance = diff.magnitude;
            if (!IsInView(diff.normalized, distance, mask)) continue;
            list.Add(current.transform);
        }
        return list;
    }

    private void TakeDamage()
    {
        health.DamageTaken(20);
        if (!isPursuing)
        {
            OnPlayerInSight();
            isPursuing = true;
        }
        if (health.IsHealthDepleted())
        {
            OnDefeat();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.layer == playerBulletLayer)
            {
                TakeDamage();
            }
        }
    }

    IEnumerator ShootRoutine(Vector3 direction)
    {
        yield return new WaitForSeconds(shootTimeOffset);
        Bullet firedBullet = Instantiate(bullet);
        firedBullet.Fire(firePoint, direction);
        enemyAnimation.ShootAnimation(false);
    }

    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, range);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * range);
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, Quaternion.Euler(0, angle / 2, 0) * transform.forward * range);
        Gizmos.DrawRay(transform.position, Quaternion.Euler(0, -angle / 2, 0) * transform.forward * range);
    }
}
