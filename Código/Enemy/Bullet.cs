using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float firingSpeed;
    [SerializeField] private float deleteTime;
    private float counter;
    private Rigidbody rigidBody;

    public delegate void BulletDelegate();
    public BulletDelegate OnPlayerHit;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        counter += Time.deltaTime;
        CheckDeleteTime();
    }

    public void Fire(Transform firePoint, Vector3 forward)
    {
        transform.forward = forward;
        transform.position = firePoint.position + firePoint.forward;
        rigidBody.AddForce(transform.forward * firingSpeed, ForceMode.Impulse);
    }

    private void CheckDeleteTime()
    {
        if(counter >= deleteTime)
        {
            Destroy();
        }
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision != null)
        {
            Destroy();
        }
    }
}
