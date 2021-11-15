using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkState<T> : State<T>
{
    private Player player;
    private T idleInput;
    private T aimInput;
    private AimComponent aim;
    private Vector3 rotation;
    private Vector3 direction = Vector3.zero;

    public PlayerWalkState(Player player, AimComponent aim, T idleInput, T aimInput)
    {
        this.player = player;
        this.aim = aim;
        this.idleInput = idleInput;
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

        if (horizontal != 0 || vertical != 0)
        {
            direction.x = horizontal;
            direction.z = vertical;
            rotation = aim.GetRotation(mouseHorizontal, mouseVertical);
            bool isAiming = Input.GetMouseButton(1);

            player.Move(direction);           
            player.Rotate(rotation);
            aim.EulerAnglesReset();

            if (isAiming)
            {
                aim.TransitionToAim(true);
                fsm.Transition(aimInput);
            }
        }

        if (horizontal == 0 && vertical == 0)
        {
            player.Stop();
            fsm.Transition(idleInput);
        }
    }

}
