using System.Collections;
using UnityEngine;

public class EnemyAttackState : EnemyBaseState{
    public EnemyAttackState(EnemyManager enemy) : base(enemy){}

    public override void EnterState()
    {
        enemy.attackCooldown = 1.2f;
        enemy.rb.velocity = Vector2.zero;
        enemy.animator.SetBool("Attacking", true);
    }

    public override void FixedUpdateState()
    {
        enemy.rb.velocity = Vector2.zero;
    }

    public override void LeaveState()
    {
        enemy.animator.SetBool("Attacking", false);
    }

    public override void UpdateState()
    {
        if(enemy.hit){
            enemy.SwitchStates(enemy.hurtState);
        }
    }

    public void AttackInterupt(){
        enemy.SwitchStates(enemy.idleState);
    }
}