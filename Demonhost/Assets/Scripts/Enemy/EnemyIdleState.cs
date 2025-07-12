using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyBaseState{
    public EnemyIdleState(EnemyManager enemy) : base(enemy){}

    public override void EnterState()
    {
        Debug.Log("please");
        enemy.animator.SetBool("Idle", true);
    }

    public override void FixedUpdateState()
    {
    }

    public override void UpdateState()
    {
        if(enemy.hit){
            enemy.animator.SetBool("Idle", false);
            enemy.SwitchStates(enemy.hurtState);
        }
    }
}