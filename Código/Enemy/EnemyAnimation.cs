using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private Rigidbody rigidBody;
    private Enemy enemy;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
        rigidBody = GetComponent<Rigidbody>();
    }

    public void WalkAnimation(float velocity)
    {
        animator.SetFloat("Velocity", velocity);
    }

    public void ShootAnimation(bool isShooting)
    {
        animator.SetBool("IsShooting", isShooting);
    }

    public void DefeatAnimation(bool isDefeated)
    {
        animator.SetBool("IsDefeated", isDefeated);
    }
}
