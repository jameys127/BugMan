using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWalkingState : PlayerBaseState
{
    public PlayerWalkingState (PlayerStateManager player) : base(player){
        
    }
    public override void EnterState()
    {
    }
    public override void UpdateState()
    {
        if(player.moveInput.magnitude < 0.1f){
            player.SwitchStates(player.idleState);
        }
        if(player.attackInput){
            player.SwitchStates(player.attackState);
        }
        if(player.dodgeInput){
            player.SwitchStates(player.dodgeState);
        }
    }
    public override void FixedUpdateState()
    {
        player.rb.velocity = player.moveInput * player.moveSpeed;
        player.playerAnimation.UpdateAnimation(player.moveInput);
    }
}
