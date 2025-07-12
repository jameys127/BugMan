using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttackState : PlayerBaseState
{
    private Timer timer;
    private Weapon weapon;
    private float attackMovement;
    public bool repel = false;
    private float repelMovement;
    public PlayerAttackState(PlayerStateManager player, Weapon weapon) : base(player){
        this.weapon = weapon;
        timer = new Timer(weapon.swingAnimationTime);
    }
    public override void EnterState()
    {
        attackMovement = 4f;
        repelMovement = 5f;
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
        AttackStep();
        Repel();
    }

    public void Swing(){
        weapon.Enter();
    }

    public void Repel(){
        if(repel){
            player.rb.velocity = -player.attackOffsetDirection * repelMovement;
            repelMovement *= 0.8f;
        }
    }

    private void SwitchBack(){
        repel = false;
        timer.StopTimer();
        timer.OnTimerDone -= SwitchBack;
        player.SwitchStates(player.idleState);
    }

    private void AttackStep(){
        if(player.attackStep){
            attackMovement *= 0.9f;
            player.rb.velocity = player.attackOffsetDirection * attackMovement;
        }else{
            player.rb.velocity = Vector2.zero;
        }
    }

}
