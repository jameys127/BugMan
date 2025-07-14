using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeadState : EnemyBaseState
{
    public EnemyDeadState(EnemyManager enemy) : base(enemy){}
    public override void EnterState()
    {
        enemy.rb.velocity = Vector2.zero;
        enemy.bc.enabled = false;
        enemy.animator.SetBool("Dead", true);
    }

    public override void FixedUpdateState()
    {
    }

    public override void LeaveState()
    {
    }

    public override void UpdateState()
    {

    }
}
