using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyBaseState{
    public EnemyIdleState(EnemyManager enemy) : base(enemy){}

    public override void EnterState()
    {
        enemy.rb.velocity = Vector3.zero;
        enemy.animator.SetBool("Idle", true);
    }

    public override void FixedUpdateState()
    {
    }

    public override void LeaveState()
    {
        enemy.animator.SetBool("Idle", false);
    }

    public override void UpdateState()
    {
        if(enemy.hit){
            enemy.SwitchStates(enemy.hurtState);
        }
    }
}