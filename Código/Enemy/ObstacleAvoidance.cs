using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleAvoidance : ISteering
{
    private Transform self;
    private Transform target;
    private float radius;
    private float multiplier;
    private Collider[] objects;
    private LayerMask layerMask;

    public ObstacleAvoidance(Transform self, Transform target, float radius, int maxObjects, float multiplier, LayerMask mask)
    {
        this.self = self;
        this.target = target;
        this.radius = radius;
        objects = new Collider[maxObjects];
        layerMask = mask;
        this.multiplier = multiplier;
    }

    public Vector3 GetDirection()
    {
        Vector3 direction = (target.position - self.position).normalized;
        int countObjects = Physics.OverlapSphereNonAlloc(self.position, radius, objects, layerMask);
        Collider nearObject = null;
        float distanceNearObject = 0;
        for (int i = 0; i < countObjects; i++)
        {
            var current = objects[i];
            if (self.position == current.transform.position) continue;
            Vector3 nearPoint = current.ClosestPointOnBounds(self.position);
            float distanceCurrent = Vector3.Distance(self.position, nearPoint);
            if (nearObject == null)
            {
                nearObject = current;
                distanceNearObject = distanceCurrent;
            }
            else
            {
                if (distanceNearObject > distanceCurrent)
                {
                    nearObject = current;
                    distanceNearObject = distanceCurrent;
                }
            }
        }

        if (nearObject != null)
        {
            var posObj = nearObject.transform.position;
            Vector3 dirObstacleToSelf = (self.position - posObj);
            dirObstacleToSelf = dirObstacleToSelf.normalized * ((radius - distanceNearObject) / radius) * multiplier;
            direction += dirObstacleToSelf;
            direction = direction.normalized;
        }
        return direction;
    }

}
