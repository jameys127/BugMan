using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator animator;
    private int lastXDirection = -1;
    private int lastYDirection = -1;

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

    public void PlayAttack(){
        animator.Play(lastXDirection > 0 ? attackAnimFlip : attackAnim);
    }
}
