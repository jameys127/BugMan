using UnityEngine;

public class PlayerDodgeState : PlayerBaseState
{
    private Timer timer;
    private Vector2 moveDirection;


    public PlayerDodgeState(PlayerStateManager player) : base(player){
        timer = new Timer(0.3f);
    }
    public override void EnterState()
    {
        moveDirection = player.moveInput;
        timer.OnTimerDone += DodgeComplete;
        timer.StartTimer();
        // player.playerAnimation.PlayDodge(moveDirection.magnitude < 0.1f);

    }
    public override void UpdateState()
    {
        timer.Tick();
    }
    public override void FixedUpdateState()
    {
        if(moveDirection.magnitude < 0.1f){
            player.rb.velocity = new Vector2(1, 0) * player.dodgeSpeed;
        }else{
            player.rb.velocity = moveDirection * player.dodgeSpeed;
        }
    }

    private void DodgeComplete(){
        timer.StopTimer();
        timer.OnTimerDone -= DodgeComplete;
        player.SwitchStates(player.idleState);
    }

}
