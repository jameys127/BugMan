using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [HideInInspector] public Animator animator;
    [HideInInspector] public int lastXDirection = -1;
    [HideInInspector] public int lastYDirection = -1;

    private int walkLeft = Animator.StringToHash("WalkLeft");
    private int walkRight = Animator.StringToHash("WalkRight");
    private int walkBackLeft = Animator.StringToHash("WalkBackLeft");
    private int walkBackRight = Animator.StringToHash("WalkBackRight");
    private int idleLeft = Animator.StringToHash("IdleLeft");
    private int idleRight = Animator.StringToHash("IdleRight");
    private int idleBackLeft = Animator.StringToHash("IdleBackLeft");
    private int idleBackRight = Animator.StringToHash("IdleBackRight");
    private int attackAnim = Animator.StringToHash("CerulAttackAnim");
    private int attackAnimFlip = Animator.StringToHash("CerulAttackAnimFlip");
    private int attackAnimBack = Animator.StringToHash("CerulAttackAnimBack");
    private int attackAnimBackFlip = Animator.StringToHash("CerulAttackAnimBackFlip");
    private int dodgeRight = Animator.StringToHash("CerulDodgeFlip");
    private int dodgeLeft = Animator.StringToHash("CerulDodge");
    private int dodgeFromIdle = Animator.StringToHash("CerulDodgeFromIdle");
    private int dodgeFromIdleFlip = Animator.StringToHash("CerulDodgeFromIdleFlip");
    private int hit = Animator.StringToHash("CerulHit");
    private int hitFlip = Animator.StringToHash("CerulHitFlip");



    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        animator.Play(idleLeft);
    }

    public void UpdateAnimation(Vector2 moveInput){
        if(moveInput.x != 0){
            lastXDirection = moveInput.x > 0 ? 1 : -1;
        }
        if(moveInput.y != 0){
            lastYDirection = moveInput.y > 0 ? 1 : -1;
        }
        PlayAnimation(moveInput);
    }

    private void PlayAnimation(Vector2 input){
        if(input.magnitude < 0.1f){
            if(lastYDirection == -1){
                animator.Play(lastXDirection > 0 ? idleRight : idleLeft);
                return;
            }else{
                animator.Play(lastXDirection > 0 ? idleBackRight : idleBackLeft);
                return;
            }
        }
        if(input.y > 0.1f){
            if(input.x > 0.1f){
                animator.Play(walkBackRight);
            }else if(input.x < -0.1f){
                animator.Play(walkBackLeft);
            }else{
                animator.Play(lastXDirection > 0 ? walkBackRight : walkBackLeft);
            }
        }else if(input.y < -0.1f){
            if(input.x > 0.1f){
                animator.Play(walkRight);
            }else if(input.x < -0.1f){
                animator.Play(walkLeft);
            }else{
                animator.Play(lastXDirection > 0 ? walkRight : walkLeft);
            }
        }else{
            animator.Play(input.x > 0 ? walkRight : walkLeft);
            lastYDirection = -1;
        }
    }

    public void PlayDodge(Vector2 moveDirection){
        if(moveDirection.magnitude < 0.1f){
            if(lastXDirection == 1){
                animator.Play(dodgeFromIdleFlip);
            }else{
                animator.Play(dodgeFromIdle);
            }
        }else{
            if(moveDirection.x > 0.1){
                animator.Play(dodgeRight);
            }else{
                animator.Play(dodgeLeft);
            }
        }
    }

    public void PlayAttack(int direction){
        switch (direction){
            case 0:
                animator.Play(attackAnimBack);
                lastXDirection = -1;
                lastYDirection = 1;
                break;
            case 1:
                animator.Play(attackAnimBackFlip);
                lastXDirection = 1;
                lastYDirection = 1;
                break;
            case 2:
                animator.Play(attackAnim);
                lastXDirection = -1;
                lastYDirection = -1;
                break;
            case 3:
                animator.Play(attackAnimFlip);
                lastXDirection = 1;
                lastYDirection = -1;
                break;
            case 4:
                animator.Play(attackAnimBack);
                lastXDirection = -1;
                lastYDirection = 1;
                break;
            case 5:
                animator.Play(attackAnimBackFlip);
                lastXDirection = 1;
                lastYDirection = 1;
                break;
            case 6:
                animator.Play(attackAnim);
                lastXDirection = -1;
                lastYDirection = -1;
                break;
            case 7:
                animator.Play(attackAnimFlip);
                lastXDirection = 1;
                lastYDirection = -1;
                break;
            default:
                break;
        }
    }

    public void PlayHit(){
        if(lastXDirection == 1){
            animator.Play(hitFlip);
        }else{
            animator.Play(hit);
        }
    }
}
