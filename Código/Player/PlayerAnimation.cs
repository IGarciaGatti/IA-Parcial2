using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private Rigidbody rigidBody;
    private Player player;

    private void Awake()
    {
        player = GetComponent<Player>();
        rigidBody = GetComponent<Rigidbody>();
    }

    public void RunAnimation(float velocity)
    {
        animator.SetFloat("Velocity", velocity);
    }
    
    public void AimAnimation(bool isAiming)
    {
        animator.SetBool("IsAiming", isAiming);
    }

    public void DefeatAnimation(bool isDefeated)
    {
        animator.SetBool("IsDefeated", isDefeated);
    }
}
