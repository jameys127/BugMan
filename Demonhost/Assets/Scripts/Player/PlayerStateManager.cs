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
    [HideInInspector]
    public Vector2 attackOffsetDirection;
    public float angle;

    //VARIABLES
    [Header("Variables for Player")]
    [SerializeField] public float moveSpeed;

    void Awake()
    {
        weapon = transform.Find("WeaponHolder").Find("Weapon").GetComponent<Weapon>();
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
            angle = GetMouseAngle();
            AttackDirection dir = GetMousePosition(angle);
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

    private float GetMouseAngle(){
        Vector3 mouseRelativePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mouseDirection = new Vector2(
            mouseRelativePos.x - transform.position.x,
            mouseRelativePos.y - transform.position.y
        ).normalized;
        attackOffsetDirection = mouseDirection;
        Debug.Log($"AttackOffsetDirection magnitude: {attackOffsetDirection.magnitude}");

        float angle = Mathf.Atan2(attackOffsetDirection.y, attackOffsetDirection.x) * Mathf.Rad2Deg;
        if(angle < 0) angle += 360;
        return angle;
    }

    private AttackDirection GetMousePosition(float angle){
        if(angle >= 300 || angle < 20) return AttackDirection.LOWERRIGHT;
        else if(angle >= 20 && angle < 70) return AttackDirection.UPPERRIGHT;
        else if(angle >= 70 && angle < 90) return AttackDirection.UPRIGHT;
        else if(angle >= 90 && angle < 110) return AttackDirection.UPLEFT;
        else if(angle >= 110 && angle < 160) return AttackDirection.UPPERLEFT;
        else if(angle >= 160 && angle < 240) return AttackDirection.LOWERLEFT;
        else if(angle >= 240 && angle < 270) return AttackDirection.DOWNLEFT;
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
