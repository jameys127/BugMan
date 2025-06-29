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
        player.rb.velocity = Vector2.zero;
        player.playerAnimation.PlayAttack(player.direction);
    }
    public override void UpdateState()
    {
    }

    public void OnAttackComplete(){
        player.SwitchStates(player.idleState);
    }
    public override void FixedUpdateState()
    {
    }

    public void Swing(){
        weapon.Enter();
    }
}
