using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAggro : MonoBehaviour
{
    private EnemyManager enemy;
    void Start()
    {
        enemy = transform.parent.GetComponent<EnemyManager>();        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")){
            if(enemy.player == null){
                enemy.player = collision.transform;
            }
            enemy.withinRange = true;
            enemy.SwitchStates(enemy.chasingState);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")){
            enemy.withinRange = false;
            enemy.SwitchStates(enemy.idleState);
        }
    }
}
