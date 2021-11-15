using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : ISteering
{
    private Transform self;
    private List<Waypoint> waypoints;
    private int nextPoint;

    public Patrol(Transform self, List<Waypoint> waypoints)
    {
        this.self = self;
        this.waypoints = waypoints;
    }

    public Vector3 GetDirection()
    {
        Vector3 point = waypoints[nextPoint].transform.position;
        Vector3 posPoint = point;
        posPoint.y = self.position.y;
        Vector3 direction = posPoint - self.transform.position;
        if (direction.magnitude < 0.2f)
        {
            if (nextPoint + 1 < waypoints.Count)
            {
                nextPoint++;
            }
            else
            {
                waypoints.Reverse();
                nextPoint = 0;
                return Vector3.zero;
            }
        }

        return direction.normalized;
    }
}
