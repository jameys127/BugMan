using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAggro : MonoBehaviour
{
    private EnemyManager enemy;
    private CircleCollider2D circleCollider;
    private float initialAggroRange = 3.5f;
    private float sustainedAggroRange = 8f;
    void Start()
    {
        enemy = transform.parent.GetComponent<EnemyManager>();
        circleCollider = GetComponent<CircleCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")){
            if(enemy.currentState is EnemyDeadState){
                return;
            }
            if(enemy.player == null){
                enemy.player = collision.transform;
            }
            enemy.withinRange = true;
            circleCollider.radius = sustainedAggroRange;
            enemy.SwitchStates(enemy.chasingState);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")){
            circleCollider.radius = initialAggroRange;
            enemy.withinRange = false;
            if(enemy.currentState is EnemyDeadState){
                return;
            }
            enemy.SwitchStates(enemy.idleState);
        }
    }
}
