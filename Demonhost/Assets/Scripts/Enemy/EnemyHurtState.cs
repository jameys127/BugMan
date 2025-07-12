using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHurtState : EnemyBaseState{
    public EnemyHurtState(EnemyManager enemy) : base(enemy){}

    public override void EnterState()
    {
        enemy.hit = false;
        enemy.animator.SetTrigger("Hit");
    }

    public override void FixedUpdateState()
    {
    }

    public override void UpdateState()
    {
    }
}