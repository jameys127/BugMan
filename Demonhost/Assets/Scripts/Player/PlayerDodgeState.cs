using System;
using UnityEngine;

public class PlayerDodgeState : PlayerBaseState
{
    private Timer movingDodgeTimer;
    private Timer idleDodgeTimer;
    private Vector2 moveDirection;
    private float dodgeDuration = 0.483f;
    private float idleDodgeDuration = 0.683f;
    private float currentSpeed;


    public PlayerDodgeState(PlayerStateManager player) : base(player){
        movingDodgeTimer = new Timer(dodgeDuration);
        idleDodgeTimer = new Timer(idleDodgeDuration);
    }
    public override void EnterState()
    {
        player.dodgeInput = false;
        moveDirection = player.moveInput;
        currentSpeed = player.dodgeSpeed;
        movingDodgeTimer.OnTimerDone += DodgeComplete;
        idleDodgeTimer.OnTimerDone += DodgeComplete;
        if(moveDirection.magnitude > 0.1f){
            movingDodgeTimer.StartTimer();
        }else{
            idleDodgeTimer.StartTimer();
        }
        player.IFrames();
        player.playerAnimation.PlayDodge(moveDirection);

    }
    public override void UpdateState()
    {
        movingDodgeTimer.Tick();
        idleDodgeTimer.Tick();
    }
    public override void FixedUpdateState()
    {
        if(moveDirection.magnitude < 0.1f){
            currentSpeed *= 0.9f;
            player.rb.velocity = new Vector2(player.playerAnimation.lastXDirection > 0 ? -1 : 1, 0) * currentSpeed;
        }else{
            player.rb.velocity = moveDirection * player.dodgeSpeed;
        }
    }

    public void DodgeComplete(){
        movingDodgeTimer.StopTimer();
        idleDodgeTimer.StopTimer();
        movingDodgeTimer.OnTimerDone -= DodgeComplete;
        idleDodgeTimer.OnTimerDone -= DodgeComplete;
        player.dodgeCooldown = 0.5f;
        player.SwitchStates(player.idleState);
    }

}
