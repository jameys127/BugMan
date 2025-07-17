using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeadState : EnemyBaseState
{
    public EnemyDeadState(EnemyManager enemy) : base(enemy){}
    public override void EnterState()
    {
        enemy.rb.velocity = Vector2.zero;
        enemy.pc.enabled = false;
        Bloodsplatter();
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
    private void Bloodsplatter(){
        Quaternion rotation = Quaternion.FromToRotation(Vector2.right, enemy.directionOfHit);
        GameObject.Instantiate(enemy.bloodSplatter, enemy.transform.position, rotation);
    }
}
