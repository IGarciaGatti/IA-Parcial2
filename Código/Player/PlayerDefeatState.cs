using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDefeatState<T> : State<T>
{
    private Player player;

    public PlayerDefeatState(Player player)
    {
        this.player = player;
    }

    public override void Awake()
    {
        player.Defeat();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
