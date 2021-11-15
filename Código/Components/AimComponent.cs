using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimComponent 
{
    private Transform followTarget;
    private float rotationPower;
    private GameObject mainCamera;
    private GameObject sideCamera;
    private GameObject crosshair;
    private Vector3 angles;

    public AimComponent(Transform followTarget, float rotationPower, GameObject mainCamera, GameObject sideCamera, GameObject crosshair)
    {
        this.followTarget = followTarget;
        this.rotationPower = rotationPower;
        this.mainCamera = mainCamera;
        this.sideCamera = sideCamera;
        this.crosshair = crosshair;
    }

    public Vector3 GetRotation(float horizontal, float vertical)
    {
        followTarget.rotation *= Quaternion.AngleAxis(horizontal * rotationPower, Vector3.up);
        followTarget.rotation *= Quaternion.AngleAxis(vertical * rotationPower, Vector3.right);

        angles = followTarget.localEulerAngles;
        angles.z = 0;

        var angle = followTarget.localEulerAngles.x;

        if (angle > 180 && angle < 340)
        {
            angles.x = 340;
        }
        else if (angle < 180 && angle > 40)
        {
            angles.x = 40;
        }

        followTarget.localEulerAngles = angles;
        return followTarget.eulerAngles;
    }

    public void TransitionToAim(bool isAiming)
    {
        if (isAiming)
        {
            mainCamera.SetActive(false);
            sideCamera.SetActive(true);
            crosshair.SetActive(true);
        }
        else
        {
            mainCamera.SetActive(true);
            sideCamera.SetActive(false);
            crosshair.SetActive(false);
        }
    }

    public void EulerAnglesReset()
    {
        if(angles != null)
        {
            followTarget.localEulerAngles = new Vector3(angles.x, 0, 0);
        }       
    }
}
