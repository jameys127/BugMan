using System;
using UnityEngine;

public class PlayerDodgeState : PlayerBaseState
{
    private Timer timer;
    private Vector2 moveDirection;
    private float dodgeDuration = 3f;
    private float currentSpeed;


    public PlayerDodgeState(PlayerStateManager player) : base(player){
        timer = new Timer(dodgeDuration);
    }
    public override void EnterState()
    {
        moveDirection = player.moveInput;
        currentSpeed = player.dodgeSpeed;
        timer.OnTimerDone += DodgeComplete;
        timer.StartTimer();
        player.playerAnimation.PlayDodge(moveDirection);

    }
    public override void UpdateState()
    {
        timer.Tick();
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
        timer.StopTimer();
        timer.OnTimerDone -= DodgeComplete;
        player.SwitchStates(player.idleState);
    }

}
