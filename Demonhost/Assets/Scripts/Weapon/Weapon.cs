using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] public float attackCounterResetCooldown;
    [SerializeField] private GameObject playerBugMan;
    private PlayerStateManager player;
    private Animator animator;
    private int counter;
    [SerializeField] private int counterLimit;
    public Timer attackCounterResetTimer;
    private SpriteRenderer spriteRenderer;
    private Vector3 positionOfWeapon;


    public void Enter(){
        attackCounterResetTimer.StopTimer();
        if(counter == counterLimit) counter = 0;

        positionOfWeapon = transform.parent.position;

        float adjustedAngle = player.angle -180f;
        transform.parent.rotation = Quaternion.Euler(0, 0, adjustedAngle);
        spriteRenderer.enabled = true;
        if(player.direction == 5 || player.direction == 1 || player.direction == 3 || player.direction == 7){
            transform.parent.position = new Vector3(transform.parent.position.x + 0.5f, transform.parent.position.y, transform.parent.position.z);
        }
        if(player.direction == 0 || player.direction == 4 || player.direction == 1 || player.direction == 5){
            transform.parent.position = new Vector3(transform.parent.position.x, transform.parent.position.y + 0.13f, transform.parent.position.z);
            spriteRenderer.sortingLayerName = "WeaponBehind";
        }
        animator.SetInteger("counter", counter);
        animator.SetInteger("direction", player.direction);
        animator.SetBool("test", true);
        // animator.SetBool("active", true);
    }

    void Awake()
    {
        player = playerBugMan.GetComponent<PlayerStateManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = gameObject.GetComponent<Animator>();
        attackCounterResetTimer = new Timer(attackCounterResetCooldown);
    }

    void Update()
    {
        attackCounterResetTimer.Tick();
    }

    public void DisableWeapon(){
        spriteRenderer.sortingLayerName = "WeaponInFront";
        transform.parent.position = positionOfWeapon;
        spriteRenderer.enabled = false;
        // animator.SetBool("active", false);
        animator.SetBool("test", false);
        counter++;
        attackCounterResetTimer.StartTimer();
    }

    private void ResetAttackCounter(){
        counter = 0;
    }

    void OnEnable()
    {
        attackCounterResetTimer.OnTimerDone += ResetAttackCounter;
    }

    void OnDisable()
    {
        attackCounterResetTimer.OnTimerDone -= ResetAttackCounter;
    }
}
