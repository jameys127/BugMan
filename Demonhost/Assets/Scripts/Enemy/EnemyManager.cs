using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    //STATES
    public EnemyIdleState idleState;
    public EnemyHurtState hurtState;
    public EnemyChasingState chasingState;
    public EnemyDeadState deadState;
    public EnemyAttackState attackState;
    public EnemyBaseState currentState;

    //ENEMY COMPONENTS
    public Animator animator;
    public Rigidbody2D rb;
    public PolygonCollider2D pc;

    //ENEMY VARIABLES
    public int health;
    public Transform player;
    public float attackRange;

    //STATE TRIGGERS
    public bool hit;
    public Vector2 directionOfHit;
    public bool withinRange;
    public int facingDirection;
    public bool attackStep = false;
    

    void Awake()
    {
        idleState = new EnemyIdleState(this);
        hurtState = new EnemyHurtState(this);
        chasingState = new EnemyChasingState(this);
        deadState = new EnemyDeadState(this);
        attackState = new EnemyAttackState(this);
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        pc = GetComponent<PolygonCollider2D>();
    }
    void Start()
    {
        facingDirection = 1;
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
    public void IGotHurt(Vector2 attackDirection, int damage){
        health -= damage;
        directionOfHit = attackDirection;
        if(health <= 0){
            SwitchStates(deadState);
        }else{
            hit = true;
        }
    }

    public void AttackInterupt(){
        if(currentState is EnemyAttackState attackState){
            attackState.AttackInterupt();
        }
    }

    public void AttackStep(){
        attackStep = !attackStep;
    }
}
