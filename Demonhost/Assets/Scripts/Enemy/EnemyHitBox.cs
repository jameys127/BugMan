using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitBox : MonoBehaviour
{
    private PlayerStateManager player;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")){
            player = collision.transform.GetComponent<PlayerStateManager>();
            player.EnemyDirection((transform.parent.transform.position - player.transform.position).normalized);
            if(player.iFrames){
                Debug.Log("you were dodging");
            }else{
                player.health.DamageHealth(30);
            }
        }
    }
}
