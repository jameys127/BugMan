using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private int health;
    public Animator animator;
    public EnemyIdleState idleState;
    public EnemyHurtState hurtState;

    public EnemyBaseState currentState;

    public bool hit;

    void Awake()
    {
        idleState = new EnemyIdleState(this);
        hurtState = new EnemyHurtState(this);
        animator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        SwitchStates(idleState);
        health = 100;
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState();
    }

    void FixedUpdate()
    {
        currentState.FixedUpdateState();
    }

    public void SwitchStates(EnemyBaseState nextState){
        currentState = nextState;
        nextState.EnterState();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
    }
    public void IGotHurt(){
        Debug.Log("I got hurt");
        health -= 10;
        Debug.Log("My health is now: " + health);
        hit = true;
    }
    private void BackToIdleFromHit(){
        animator.SetBool("Hit", false);
        SwitchStates(idleState);
    }

}
