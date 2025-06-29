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
    [HideInInspector]
    public int direction;

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
        if(currentState == newState) return;
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
            AttackDirection dir = GetMousePosition();
            direction = (int)dir;
            attackInput = true;
        }
        if(context.canceled){
            attackInput = false;
        }
    }

    public void OnAttackComplete(){
        if(currentState is PlayerAttackState attackState){
            attackInput = false;
            attackState.OnAttackComplete();
        }
    }
    public void SwingWeapon(){
        if(currentState is PlayerAttackState attackState){
            attackState.Swing();
        }
    }

    private AttackDirection GetMousePosition(){
        Vector3 mouseRelativePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mouseRelativePos - transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if(angle < 0) angle += 360;

        if(angle >= 290 || angle < 20) return AttackDirection.LOWERRIGHT;
        else if(angle >= 20 && angle < 70) return AttackDirection.UPPERRIGHT;
        else if(angle >= 70 && angle < 90) return AttackDirection.UPRIGHT;
        else if(angle >= 90 && angle < 110) return AttackDirection.UPLEFT;
        else if(angle >= 110 && angle < 160) return AttackDirection.UPPERLEFT;
        else if(angle >= 160 && angle < 250) return AttackDirection.LOWERLEFT;
        else if(angle >= 250 && angle < 270) return AttackDirection.DOWNLEFT;
        else                                return AttackDirection.DOWNRIGHT;
    }


    public enum AttackDirection{
        UPLEFT, //0
        UPRIGHT,//1
        DOWNLEFT,//2
        DOWNRIGHT,//3
        UPPERLEFT,//4
        UPPERRIGHT,//5
        LOWERLEFT,//6
        LOWERRIGHT//7
    }
}
