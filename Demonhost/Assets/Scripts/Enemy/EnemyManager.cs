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
    [HideInInspector] public Animator animator;
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public PolygonCollider2D pc;

    //ENEMY VARIABLES
    [Header("Player Health")]
    public int health;
    [HideInInspector] public Transform player;
    [Header("Distance from player before attacking")]
    public float attackRange;
    [Header("Blood Splatter Particle System")]
    public ParticleSystem bloodSplatter;

    //STATE TRIGGERS
    [HideInInspector] public bool hit;
    [HideInInspector] public Vector2 directionOfHit;
    [HideInInspector] public bool withinRange;
    [HideInInspector] public int facingDirection;
    [HideInInspector] public float attackCooldown = 1.5f;
    

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
    }

    void Update()
    {
        currentState.UpdateState();
        attackCooldown -= Time.deltaTime;
        if(attackCooldown < 0){
            attackCooldown = 0;
        }
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
}
