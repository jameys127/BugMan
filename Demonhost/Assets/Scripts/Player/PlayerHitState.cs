using UnityEngine;

public class PlayerHitState : PlayerBaseState {
    private float hitTimer = 0.433f;
    private Timer timer;
    private float hitBack;
    public PlayerHitState(PlayerStateManager player) : base(player){
        timer = new Timer(hitTimer);
    }

    public override void EnterState()
    {
        hitBack = 4f;
        timer.OnTimerDone += LeaveState;
        timer.StartTimer();
        player.playerAnimation.PlayHit();
    }

    public override void FixedUpdateState()
    {
        player.rb.velocity = -player.enemyDirection * hitBack;
        hitBack *= 0.8f;
    }

    public override void UpdateState()
    {
        timer.Tick();
    }
    private void LeaveState(){
        timer.StopTimer();
        timer.OnTimerDone -= LeaveState;
        player.SwitchStates(player.idleState);
    }
}