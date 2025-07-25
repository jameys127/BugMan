using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOnHitExit : StateMachineBehaviour
{
    private EnemyManager enemy;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(enemy == null){
            enemy = animator.GetComponent<EnemyManager>();
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("Hit", false);
        if(enemy.withinRange){
            enemy.SwitchStates(enemy.chasingState);
        }else{
            enemy.SwitchStates(enemy.idleState);
        }
    }
}
