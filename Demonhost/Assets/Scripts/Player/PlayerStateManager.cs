using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateManager : MonoBehaviour
{
    //STATES
    private PlayerBaseState currentState;
    public PlayerAttackState attackState = new PlayerAttackState();
    public PlayerIdleState idleState = new PlayerIdleState();
    public PlayerWalkingState walkingState = new PlayerWalkingState();

    //COMPONENT REFERENCES
    [HideInInspector]
    public Rigidbody2D rb;
    [HideInInspector]
    public PlayerAnimationController playerAnimation;
    private InputAction.CallbackContext context;

    //VARIABLES
    [Header("Variables for Player")]
    [SerializeField] public float moveSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnimation = GetComponent<PlayerAnimationController>();
        currentState = idleState;
        currentState.EnterState(this);
    }

    void FixedUpdate()
    {
        currentState.UpdateState(this, context);
    }

    public void SwitchStates(PlayerBaseState newState){
        currentState = newState;
        newState.EnterState(this);
    }

    public void Move(InputAction.CallbackContext context){
        this.context = context;
    }
}
