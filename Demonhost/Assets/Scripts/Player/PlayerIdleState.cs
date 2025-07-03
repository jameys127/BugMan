using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerIdleState : PlayerBaseState
{
    private float lastAttackTime;
    private float lastDodgeTime;
    public PlayerIdleState(PlayerStateManager player) : base(player){
    }
    public override void EnterState()
    {
        player.playerAnimation.UpdateAnimation(Vector2.zero);
        lastAttackTime = Time.time;
        lastDodgeTime = Time.time;
    }
    public override void UpdateState()
    {
        if(player.moveInput.magnitude > 0.1f){
            player.SwitchStates(player.walkingState);
        }
        if(player.attackInput && Time.time - lastAttackTime > 0.05f){
            player.SwitchStates(player.attackState);
        }
        if(player.dodgeInput && Time.time - lastAttackTime > 0.05f){
            player.SwitchStates(player.dodgeState);
        }
    }
    public override void FixedUpdateState()
    {
        player.rb.velocity = Vector2.zero;
    }
}
