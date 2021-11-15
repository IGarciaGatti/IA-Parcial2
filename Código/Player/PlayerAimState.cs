using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimState<T> : State<T>
{
    private Player player;
    private AimComponent aim;
    private Vector3 rotation;
    private Vector3 direction = Vector3.zero;
    private T idleInput;
    private T walkInput;

    public PlayerAimState(Player player, AimComponent aim, T idleInput, T walkInput)
    {
        this.player = player;
        this.aim = aim;
        this.walkInput = walkInput;
        this.idleInput = idleInput;
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
        
        direction.x = horizontal;
        direction.z = vertical;
        rotation = aim.GetRotation(mouseHorizontal, mouseVertical);
        bool isAiming = Input.GetMouseButton(1);

        player.Aim(true);
        player.Move(direction);
        player.Rotate(rotation);
        aim.EulerAnglesReset();
        aim.TransitionToAim(isAiming);
           
        if (!isAiming)
        {
            aim.TransitionToAim(false);
            player.Aim(false);
            if (horizontal != 0 || vertical != 0)
            {               
                fsm.Transition(walkInput);
            }
            else
            {
                fsm.Transition(idleInput);
            }           
        }

        if (isAiming && Input.GetMouseButtonDown(0))
        {
            player.Shoot();
        }
    }
}
