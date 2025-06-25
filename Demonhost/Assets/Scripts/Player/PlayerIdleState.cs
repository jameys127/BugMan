using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerIdleState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        Debug.Log("idling");
        player.playerAnimation.UpdateAnimation(Vector2.zero);
    }
    public override void UpdateState(PlayerStateManager player, InputAction.CallbackContext context)
    {
        if(context.ReadValue<Vector2>().magnitude > 0.1f){
            player.SwitchStates(player.walkingState);
        }
    }
}
