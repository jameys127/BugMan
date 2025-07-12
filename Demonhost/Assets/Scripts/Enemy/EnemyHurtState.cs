using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHurtState : EnemyBaseState{
    private float hitback;

    public EnemyHurtState(EnemyManager enemy) : base(enemy){}

    public override void EnterState()
    {
        hitback = 4f;
        enemy.hit = false;
        enemy.animator.SetTrigger("Hit");
    }

    public override void FixedUpdateState()
    {
        enemy.rb.velocity = enemy.directionOfHit * hitback;
        hitback *= 0.8f;
    }

    public override void LeaveState()
    {
    }

    public override void UpdateState()
    {
    }
}