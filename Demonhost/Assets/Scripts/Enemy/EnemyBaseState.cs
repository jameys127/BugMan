using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBaseState{
    protected EnemyManager enemy;
    public EnemyBaseState(EnemyManager enemy){
        this.enemy = enemy;
    }
    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void FixedUpdateState();
}