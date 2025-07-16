using UnityEngine;

public class PlayerDeadState : PlayerBaseState
{
    public PlayerDeadState(PlayerStateManager player) : base(player){}
    public override void EnterState()
    {
        Debug.Log("you are now dead");
    }

    public override void FixedUpdateState()
    {
    }

    public override void UpdateState()
    {
    }
}