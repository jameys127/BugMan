using UnityEngine;
using UnityEngine.InputSystem;

public abstract class PlayerBaseState
{
    protected PlayerStateManager player;
    public PlayerBaseState(PlayerStateManager player){
        this.player = player;
    }
    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void FixedUpdateState();
}
