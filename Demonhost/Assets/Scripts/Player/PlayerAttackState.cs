using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttackState : PlayerBaseState
{
    private Weapon weapon;
    public PlayerAttackState(PlayerStateManager player, Weapon weapon) : base(player){
        this.weapon = weapon;
    }
    public override void EnterState()
    {
        Debug.Log("Attacking");
        weapon.Enter();
        player.rb.velocity = Vector2.zero;
        player.playerAnimation.PlayAttack();
    }
    public override void UpdateState()
    {
    }

    public void OnAttackComplete(PlayerStateManager player){
        player.SwitchStates(player.idleState);
    }
    public override void FixedUpdateState()
    {
    }
}
