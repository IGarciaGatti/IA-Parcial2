using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private enum PlayerStates { HUD, Idle, Walk, Aim, Defeat }
    private FSM<PlayerStates> fsm;
    private Player player;
    private AimComponent aim;
    [SerializeField] private Transform followTarget;
    [SerializeField] private float rotationPower;
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private GameObject sideCamera;
    [SerializeField] private GameObject crosshair;

    private void Awake()
    {
        player = GetComponent<Player>();
        aim = new AimComponent(followTarget, rotationPower, mainCamera, sideCamera, crosshair);
        player.OnHUDEnter += OnHUDEnter;
        player.OnHUDExit += OnHUDExit;
        player.OnDefeat += OnDefeat;
    }

    void Start()
    {        
        InitializeFSM();
    }

    private void InitializeFSM()
    {
        var hudState = new PlayerHUDState<PlayerStates>(aim);
        var idleState = new PlayerIdleState<PlayerStates>(player, aim, PlayerStates.Walk, PlayerStates.Aim);
        var walkState = new PlayerWalkState<PlayerStates>(player, aim, PlayerStates.Idle, PlayerStates.Aim);
        var aimState = new PlayerAimState<PlayerStates>(player, aim, PlayerStates.Idle, PlayerStates.Walk);
        var defeatState = new PlayerDefeatState<PlayerStates>(player);

        hudState.AddTransition(PlayerStates.Idle, idleState);
        hudState.AddTransition(PlayerStates.Walk, walkState);
        hudState.AddTransition(PlayerStates.Aim, aimState);
        hudState.AddTransition(PlayerStates.Defeat, defeatState);

        idleState.AddTransition(PlayerStates.HUD, hudState);
        idleState.AddTransition(PlayerStates.Walk, walkState);
        idleState.AddTransition(PlayerStates.Aim, aimState);
        idleState.AddTransition(PlayerStates.Defeat, defeatState);

        aimState.AddTransition(PlayerStates.HUD, hudState);
        aimState.AddTransition(PlayerStates.Idle, idleState);
        aimState.AddTransition(PlayerStates.Walk, walkState);
        aimState.AddTransition(PlayerStates.Defeat, defeatState);

        walkState.AddTransition(PlayerStates.HUD, hudState);
        walkState.AddTransition(PlayerStates.Idle, idleState);
        walkState.AddTransition(PlayerStates.Aim, aimState);
        walkState.AddTransition(PlayerStates.Defeat, defeatState);

        fsm = new FSM<PlayerStates>();
        fsm.SetInitialState(hudState);
    }

    void FixedUpdate()
    {
        fsm.OnUpdate();
    }
   
    private void OnDefeat()
    {
        fsm.Transition(PlayerStates.Defeat);
    }

    private void OnHUDEnter()
    {
        fsm.Transition(PlayerStates.HUD);
    }

    private void OnHUDExit()
    {
        fsm.Transition(PlayerStates.Idle);
    }
}
