using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttackState : PlayerBaseState
{
    private Timer timer;
    private Weapon weapon;
    public PlayerAttackState(PlayerStateManager player, Weapon weapon) : base(player){
        this.weapon = weapon;
        timer = new Timer(weapon.swingAnimationTime);
    }
    public override void EnterState()
    {
        player.attackInput = false;
        timer.OnTimerDone += SwitchBack;
        timer.StartTimer();
        player.rb.velocity = Vector2.zero;
        player.playerAnimation.PlayAttack(player.direction);
    }
    public override void UpdateState()
    {
        timer.Tick();
    }
    public override void FixedUpdateState()
    {
    }

    public void Swing(){
        weapon.Enter();
    }

    private void SwitchBack(){
        timer.StopTimer();
        timer.OnTimerDone -= SwitchBack;
        player.SwitchStates(player.idleState);
    }

}
