using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] public float attackCounterResetCooldown;
    [SerializeField] private GameObject playerBugMan;
    [SerializeField] public float swingAnimationTime;
    public PlayerStateManager player;
    private Animator animator;
    private int counter;
    [SerializeField] private int counterLimit;
    public Timer attackCounterResetTimer;
    private SpriteRenderer spriteRenderer;
    private bool currentDirectionLeft;
    private bool? lastSideLeft = null;
    [SerializeField] public int damage;



    public void Enter(){
        attackCounterResetTimer.StopTimer();
        currentDirectionLeft = (player.angle >= 90 && player.angle < 270);
        if(lastSideLeft != null && lastSideLeft != currentDirectionLeft){
            counter++;
        }
        if(counter >= counterLimit) counter = 0;

        float adjustedAngle = player.angle -180f;
        transform.parent.rotation = Quaternion.Euler(0, 0, adjustedAngle);

        Vector3 direction3D = player.attackOffsetDirection;
        transform.parent.position += direction3D * 0.2f;
        
        
        spriteRenderer.enabled = true;
        
        if(player.direction == 0 || player.direction == 4 || player.direction == 1 || player.direction == 5){
            spriteRenderer.sortingLayerName = "WeaponBehind";
        }

        animator.SetInteger("counter", counter);
        animator.SetInteger("direction", player.direction);
        animator.SetBool("test", true);
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
        lastSideLeft = currentDirectionLeft;
        spriteRenderer.sortingLayerName = "Player";
        transform.parent.position = new Vector3(playerBugMan.transform.position.x, playerBugMan.transform.position.y - 0.1f, playerBugMan.transform.position.z);
        spriteRenderer.enabled = false;
        animator.SetBool("test", false);
        counter++;
        attackCounterResetTimer.StartTimer();
    }

    private void ResetAttackCounter(){
        counter = 0;
        lastSideLeft = null;
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
