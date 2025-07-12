using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHitBox : MonoBehaviour
{
    [SerializeField] private GameObject PlayerObject;
    [SerializeField] private GameObject WeaponObject;
    private PlayerStateManager player;
    private Weapon weapon;
    private Vector2 attackDirection;


    void Awake()
    {
        player = PlayerObject.GetComponent<PlayerStateManager>();
        weapon = WeaponObject.GetComponent<Weapon>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy")){
            Debug.Log("hit an enemy");
            attackDirection = weapon.player.attackOffsetDirection;
            collision.gameObject.GetComponent<EnemyManager>().IGotHurt(attackDirection);
            player.Repel();
        }
    }
}
