using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHUDState<T> : State<T>
{
    private AimComponent aim;

    public PlayerHUDState(AimComponent aim)
    {
        this.aim = aim;
    }

    public override void Awake()
    {
        aim.TransitionToAim(false);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public override void Sleep()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
