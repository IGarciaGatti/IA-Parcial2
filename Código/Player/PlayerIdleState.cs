using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState<T> : State<T>
{
    private Player player;
    private T walkInput;
    private T aimInput;
    private AimComponent aim;
    private Vector3 rotation;

    public PlayerIdleState(Player player, AimComponent aim, T walkInput, T aimInput)
    {
        this.player = player;
        this.aim = aim;
        this.walkInput = walkInput;
        this.aimInput = aimInput;
    }

    public override void Execute()
    {
        MovementInput();
    }

    private void MovementInput()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        var mouseHorizontal = Input.GetAxis("Mouse X");
        var mouseVertical = Input.GetAxis("Mouse Y");

        rotation = aim.GetRotation(mouseHorizontal, mouseVertical);
        bool isAiming = Input.GetMouseButton(1);
       
        player.Rotate(rotation);
        aim.EulerAnglesReset();

        if (isAiming)
        {
            aim.TransitionToAim(true);            
            fsm.Transition(aimInput);
        }

        if (horizontal != 0 || vertical != 0)
        {
            fsm.Transition(walkInput);
        }
    }

}
