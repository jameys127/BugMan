using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWalkingState : PlayerBaseState
{
    public PlayerWalkingState (PlayerStateManager player) : base(player){
        
    }
    public override void EnterState()
    {
        Debug.Log("Walking");
    }
    public override void UpdateState()
    {
        if(player.moveInput.magnitude < 0.1f){
            player.SwitchStates(player.idleState);
        }
        if(player.attackInput){
            player.SwitchStates(player.attackState);
        }
    }
    public override void FixedUpdateState()
    {
        player.rb.velocity = player.moveInput * player.moveSpeed;
        player.playerAnimation.UpdateAnimation(player.moveInput);
    }
}
