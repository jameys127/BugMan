using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateManager : MonoBehaviour
{
    //STATES
    private PlayerBaseState currentState;
    public PlayerAttackState attackState;
    public PlayerIdleState idleState;
    public PlayerWalkingState walkingState;

    //COMPONENT REFERENCES
    [HideInInspector]
    public Rigidbody2D rb;
    [HideInInspector]
    public PlayerAnimationController playerAnimation;
    [HideInInspector]
    public Vector2 moveInput;
    [HideInInspector]
    public bool attackInput;
    private Weapon weapon;

    //VARIABLES
    [Header("Variables for Player")]
    [SerializeField] public float moveSpeed;

    void Awake()
    {
        weapon = transform.Find("Weapon").GetComponent<Weapon>();
        idleState = new PlayerIdleState(this);
        walkingState = new PlayerWalkingState(this);
        attackState = new PlayerAttackState(this, weapon);


        rb = GetComponent<Rigidbody2D>();
        playerAnimation = GetComponent<PlayerAnimationController>();
        currentState = idleState;
        currentState.EnterState();
    }

    void Update()
    {
        currentState.UpdateState();
    }

    void FixedUpdate()
    {
        currentState.FixedUpdateState();
    }

    public void SwitchStates(PlayerBaseState newState){
        currentState = newState;
        newState.EnterState();
    }

    public void Move(InputAction.CallbackContext context){
        if(context.canceled){
            moveInput = Vector2.zero;
        }else{
            moveInput = context.ReadValue<Vector2>();
        }
    }

    public void Attack(InputAction.CallbackContext context){
        if(context.started){
            attackInput = true;
        }
        if(context.canceled){
            attackInput = false;
        }
    }

    public void OnAttackComplete(){
        if(currentState is PlayerAttackState attackState){
            attackState.OnAttackComplete(this);
        }
    }
}
