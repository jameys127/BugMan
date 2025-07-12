using System;
using UnityEngine;

public class EnemyChasingState : EnemyBaseState{
    public EnemyChasingState(EnemyManager enemy) : base(enemy){}

    public override void EnterState()
    {
        Debug.Log("I am now chasing");
        enemy.animator.SetBool("Chasing", true);
    }

    public override void FixedUpdateState()
    {
        Vector2 direction = (enemy.player.position - enemy.transform.position).normalized;
        enemy.rb.velocity = direction * 2f;
    }

    public override void LeaveState()
    {
        enemy.animator.SetBool("Chasing", false);
    }

    public override void UpdateState()
    {
        if(enemy.hit){
            enemy.SwitchStates(enemy.hurtState);
        }
    }
}