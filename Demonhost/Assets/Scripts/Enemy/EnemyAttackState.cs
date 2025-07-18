using System.Collections;
using UnityEngine;

public class EnemyAttackState : EnemyBaseState{
    private float attackStepSpeed;
    private Vector2 attackDirection;
    public EnemyAttackState(EnemyManager enemy) : base(enemy){}

    public override void EnterState()
    {
        attackStepSpeed = 5f;
        enemy.attackCooldown = 1.2f;
        enemy.rb.velocity = Vector2.zero;
        attackDirection = (enemy.transform.position - enemy.player.transform.position).normalized;
        enemy.animator.SetBool("Attacking", true);
    }

    public override void FixedUpdateState()
    {
        // if(enemy.attackStep){
        //     attackStepSpeed *= 0.8f;
        //     enemy.rb.velocity = -attackDirection * attackStepSpeed;
        // }else{
        enemy.rb.velocity = Vector2.zero;
        // }
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