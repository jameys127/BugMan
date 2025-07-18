using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerStateManager player) : base(player){
    }
    public override void EnterState()
    {
        player.playerAnimation.UpdateAnimation(Vector2.zero);
    }
    public override void UpdateState()
    {
        if(player.moveInput.magnitude > 0.1f){
            player.SwitchStates(player.walkingState);
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
        player.rb.velocity = Vector2.zero;
    }
}
