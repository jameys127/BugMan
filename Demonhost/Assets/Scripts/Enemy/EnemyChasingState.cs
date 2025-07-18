using System;
using UnityEngine;

public class EnemyChasingState : EnemyBaseState{
    public EnemyChasingState(EnemyManager enemy) : base(enemy){}

    public override void EnterState()
    {
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
        if(Vector2.Distance(enemy.transform.position, enemy.player.transform.position) <= enemy.attackRange && 
           enemy.attackCooldown <= 0){
            enemy.SwitchStates(enemy.attackState);
        }
        if(enemy.player.transform.position.x > enemy.transform.position.x && enemy.facingDirection == -1 ||
        enemy.player.transform.position.x < enemy.transform.position.x && enemy.facingDirection == 1){
            FlipDirections();
        }
        if(enemy.hit){
            enemy.SwitchStates(enemy.hurtState);
        }
        
    }

    void FlipDirections(){
        enemy.facingDirection *= -1;
        enemy.transform.localScale = new Vector3(enemy.transform.localScale.x * -1, enemy.transform.localScale.y, enemy.transform.localScale.z);
    }
}