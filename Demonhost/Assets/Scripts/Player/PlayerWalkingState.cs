using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWalkingState : PlayerBaseState
{
    private Vector2 moveInput;
    public override void EnterState(PlayerStateManager player)
    {
        Debug.Log("Walking");
    }
    public override void UpdateState(PlayerStateManager player, InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        if(moveInput.magnitude < 0.1f){
            player.SwitchStates(player.idleState);
        }
        player.rb.velocity = moveInput * player.moveSpeed;
        player.playerAnimation.UpdateAnimation(moveInput);
        
    }
}
