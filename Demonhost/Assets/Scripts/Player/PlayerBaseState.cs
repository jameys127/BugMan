using UnityEngine;
using UnityEngine.InputSystem;

public abstract class PlayerBaseState
{
    public abstract void EnterState(PlayerStateManager manager);
    public abstract void UpdateState(PlayerStateManager manager, InputAction.CallbackContext context);
}
