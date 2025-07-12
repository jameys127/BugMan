using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    //STATES
    public EnemyIdleState idleState;
    public EnemyHurtState hurtState;
    public EnemyChasingState chasingState;
    public EnemyBaseState currentState;

    //ENEMY COMPONENTS
    public Animator animator;
    public Rigidbody2D rb;

    //ENEMY VARIABLES
    private int health;
    public Transform player;

    //STATE TRIGGERS
    public bool hit;
    public Vector2 directionOfHit;
    public bool withinRange;

    void Awake()
    {
        idleState = new EnemyIdleState(this);
        hurtState = new EnemyHurtState(this);
        chasingState = new EnemyChasingState(this);
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        currentState = idleState;
        currentState.EnterState();
        health = 100;
    }

    void Update()
    {
        currentState.UpdateState();
    }

    void FixedUpdate()
    {
        currentState.FixedUpdateState();
    }

    public void SwitchStates(EnemyBaseState nextState){
        currentState.LeaveState();
        currentState = nextState;
        nextState.EnterState();
    }
    public void IGotHurt(Vector2 attackDirection){
        Debug.Log("I got hurt");
        health -= 10;
        Debug.Log("My health is now: " + health);
        directionOfHit = attackDirection;
        hit = true;
    }
}
