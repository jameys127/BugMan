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
    public PlayerDodgeState dodgeState;

    //REFERENCES OF COMPONENTS ON THE PLAYER
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public PlayerAnimationController playerAnimation;
    private Weapon weapon;

    //PLAYER INPUT VARIABLES
    [HideInInspector] public Vector2 moveInput;
    [HideInInspector] public bool attackInput;
    [HideInInspector] public bool dodgeInput;
    [HideInInspector] public int direction;
    [HideInInspector] public Vector2 attackOffsetDirection;
    [HideInInspector] public float angle;
    [HideInInspector] public bool iFrames;
    [HideInInspector] public bool attackStep;

    //VARIABLES
    [Header("Variables for Player")]
    [SerializeField] public float moveSpeed;
    [SerializeField] public float dodgeSpeed;
    [HideInInspector] public float dodgeCooldown;
    private float repelSpeed = 6f;


    void Awake()
    {
        weapon = transform.Find("WeaponHolder").Find("Weapon").GetComponent<Weapon>();
        idleState = new PlayerIdleState(this);
        walkingState = new PlayerWalkingState(this);
        attackState = new PlayerAttackState(this, weapon);
        dodgeState = new PlayerDodgeState(this);


        rb = GetComponent<Rigidbody2D>();
        playerAnimation = GetComponent<PlayerAnimationController>();
        currentState = idleState;
        currentState.EnterState();

        attackStep = false;
    }
    void Start()
    {
        // Application.targetFrameRate = 15;
    }

    void Update()
    {
        currentState.UpdateState();

        if(dodgeCooldown > 0){
            dodgeCooldown -= Time.deltaTime;
        }else{
            dodgeCooldown = 0;
        }

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
    }

    public void Dodge(InputAction.CallbackContext context){
        if(context.started && dodgeCooldown == 0){
            dodgeInput = true;
        }
        if(context.canceled){
            dodgeInput = false;
        }
    }

    public void SwingWeapon(){
        if(currentState is PlayerAttackState attackState){
            attackState.Swing();
        }
    }

    public void IsStepping(){
        attackStep = !attackStep;
    }

    public void Repel(){
        if(currentState is PlayerAttackState attackState){
            attackState.repel = true;
        }
    }

    private float GetMouseAngle(){
        Vector3 mouseRelativePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mouseDirection = new Vector2(
            mouseRelativePos.x - transform.position.x,
            mouseRelativePos.y - transform.position.y
        ).normalized;
        attackOffsetDirection = mouseDirection;

        float angle = Mathf.Atan2(attackOffsetDirection.y, attackOffsetDirection.x) * Mathf.Rad2Deg;
        if(angle < 0) angle += 360;
        return angle;
    }

    private AttackDirection GetMousePosition(float angle){
        if(angle >= 300 || angle < 10) return AttackDirection.LOWERRIGHT;
        else if(angle >= 10 && angle < 70) return AttackDirection.UPPERRIGHT;
        else if(angle >= 70 && angle < 90) return AttackDirection.UPRIGHT;
        else if(angle >= 90 && angle < 110) return AttackDirection.UPLEFT;
        else if(angle >= 110 && angle < 170) return AttackDirection.UPPERLEFT;
        else if(angle >= 170 && angle < 240) return AttackDirection.LOWERLEFT;
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
