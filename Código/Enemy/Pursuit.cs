using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pursuit : ISteering
{
    private Transform self;
    private Transform target;
    private IVelocity velocity;
    private float timePrediction;
    private float offset;

    public Pursuit(Transform self, Transform target, IVelocity velocity, float timePrediction, float offset = 2)
    {
        this.self = self;
        this.target = target;
        this.velocity = velocity;
        this.timePrediction = timePrediction;
        this.offset = offset;
    }

    public Vector3 GetDirection()
    {
        float multiplierDirection = (velocity.Velocity * timePrediction);
        float distance = Vector3.Distance(target.position, self.position);

        if (multiplierDirection >= distance)
        {
            multiplierDirection = distance / offset;
        }

        Vector3 finitPos = target.position + target.forward * multiplierDirection;
        Vector3 direction = finitPos - self.position;
        direction = direction.normalized;
        return direction;
    }
}
