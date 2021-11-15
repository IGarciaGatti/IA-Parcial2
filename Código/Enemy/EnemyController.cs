using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private enum EnemyStates { Idle, Patrol, Pursuit, Defeat }
    private Enemy enemy;
    private ISteering steering;    
    [SerializeField] private float timePrediction;
    [SerializeField] private float radius;
    [SerializeField] private float multiplier;
    [SerializeField] private int objectAvoidanceCount;
    [SerializeField] private float idleTime;   
    [SerializeField] private float shootCooldownTime;
    
    private bool playerInSight;
    private bool isDefeated;
    private Patrol patrol;
    private ObstacleAvoidance obstacleAvoidance;
    [SerializeField] private int patrolChance;
    [SerializeField] private int idleChance;

    [SerializeField] private Player playerTarget;
    [SerializeField] private LayerMask maskObs;

    private Pathfinding<Waypoint> pathfinding;
    [SerializeField] private Waypoint start;
    [SerializeField] private Waypoint finit;
    
    private FSM<EnemyStates> fsm;
    private INode root;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
        pathfinding = new Pathfinding<Waypoint>();
        enemy.OnPlayerInSight += OnPlayerInSight;
        enemy.OnDefeat += OnDefeat;
    }

    void Start()
    {       
        patrol = new Patrol(transform, pathfinding.GetPath(start, Satisfies, GetNeighbours, 500));
        obstacleAvoidance = new ObstacleAvoidance(transform, playerTarget.transform, radius, objectAvoidanceCount, multiplier, maskObs);
        InitializeTree();
        InitializeFSM();                    
    }

    private void InitializeFSM()
    {
        var patrolState = new EnemyPatrolState<EnemyStates>(enemy, patrol, root);
        var idleState = new EnemyIdleState<EnemyStates>(enemy, idleTime, root);
        var pursuitState = new EnemyPursuitState<EnemyStates>(enemy, playerTarget.transform, obstacleAvoidance, shootCooldownTime);
        var defeatState = new EnemyDefeatState<EnemyStates>(enemy);

        patrolState.AddTransition(EnemyStates.Idle, idleState);
        patrolState.AddTransition(EnemyStates.Pursuit, pursuitState);
        patrolState.AddTransition(EnemyStates.Defeat, defeatState);

        idleState.AddTransition(EnemyStates.Patrol, patrolState);
        idleState.AddTransition(EnemyStates.Pursuit, pursuitState);
        idleState.AddTransition(EnemyStates.Defeat, defeatState);

        pursuitState.AddTransition(EnemyStates.Idle, idleState);
        pursuitState.AddTransition(EnemyStates.Defeat, defeatState);

        fsm = new FSM<EnemyStates>();
        fsm.SetInitialState(patrolState);
    }

    private void InitializeTree()
    {
        INode patrolNode = new ActionNode(TransitionToPatrol);
        INode idleNode = new ActionNode(TransitionToIdle);
        INode pursuitNode = new ActionNode(TransitionToPursuit);
        INode defeatNode = new ActionNode(TransitionToDefeat);

        var items = new Dictionary<INode, float>();
        items[idleNode] = idleChance;
        items[patrolNode] = patrolChance;
        INode randomNode = new RandomNode(items);

        INode playerInSightNode = new QuestionNode(PlayerInSight, pursuitNode, randomNode);
        INode checkDefeatNode = new QuestionNode(IsDefeated, defeatNode, playerInSightNode);
        root = checkDefeatNode;
    }

    private bool Satisfies(Waypoint current)
    {
        return current == finit;
    }

    private List<Waypoint> GetNeighbours(Waypoint current)
    {
        var newList = new List<Waypoint>();
        for (int i = 0; i < current.neighbours.Count; i++)
        {
            var currentNeighbour = current.neighbours[i];
            newList.Add(currentNeighbour);
        }
        return newList;
    }

    void Update()
    {
        if(playerTarget != null)
        {
            fsm.OnUpdate();
        }
    }

    bool PlayerInSight()
    {
        return playerInSight;
    }

    bool IsDefeated()
    {
        return isDefeated;
    }

    void OnPlayerInSight()
    {
        playerInSight = true;
        root.Execute();
    }

    void OnDefeat()
    {
        isDefeated = true;
        root.Execute();
    }

    private void TransitionToPatrol()
    {
        fsm.Transition(EnemyStates.Patrol);
    }

    private void TransitionToIdle()
    {
        fsm.Transition(EnemyStates.Idle);
    }

    private void TransitionToDefeat()
    {
        fsm.Transition(EnemyStates.Defeat);
    }

    private void TransitionToPursuit()
    {
        fsm.Transition(EnemyStates.Pursuit);
    }

}
