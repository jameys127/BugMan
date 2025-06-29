using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float attackCounterResetCooldown;
    private Animator animator;
    private int counter;
    [SerializeField] private int counterLimit;
    private Timer attackCounterResetTimer;
    private SpriteRenderer spriteRenderer;

    public void Enter(){
        attackCounterResetTimer.StopTimer();
        if(counter == counterLimit) counter = 0;
        Debug.Log($"{transform.name} enter");

        spriteRenderer.enabled = true;
        animator.SetBool("active", true);
        animator.SetInteger("counter", counter);
    }

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = gameObject.GetComponent<Animator>();
        attackCounterResetTimer = new Timer(attackCounterResetCooldown);
    }

    void Update()
    {
        attackCounterResetTimer.Tick();
    }

    public void DisableWeapon(){
        spriteRenderer.enabled = false;
        animator.SetBool("active", false);
        counter++;
        attackCounterResetTimer.StopTimer();
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
