using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttackState : PlayerBaseState
{
    private Timer timer;
    private Weapon weapon;
    public PlayerAttackState(PlayerStateManager player, Weapon weapon) : base(player){
        this.weapon = weapon;
        timer = new Timer(weapon.attackCounterResetCooldown);
    }
    public override void EnterState()
    {
        player.attackInput = false;
        timer.OnTimerDone += DebugSwithBack;
        timer.StartTimer();
        player.rb.velocity = Vector2.zero;
        player.playerAnimation.PlayAttack(player.direction);
    }
    public override void UpdateState()
    {
        timer.Tick();
    }

    public void OnAttackComplete(){
        timer.StopTimer();
        timer.OnTimerDone -= DebugSwithBack;
        player.SwitchStates(player.idleState);
    }
    public override void FixedUpdateState()
    {
    }

    public void Swing(){
        weapon.Enter();
    }

    private void DebugSwithBack(){
        Debug.Log("I still don't really know why this happens");
        timer.StopTimer();
        timer.OnTimerDone -= DebugSwithBack;
        player.SwitchStates(player.idleState);
    }

}
