using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 5f;
    private Animator animator;
    private Rigidbody2D rb;
    private Vector2 moveInput;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = moveInput * moveSpeed;
        // UpdateAnimation();
    }


    public void Move(InputAction.CallbackContext context){
        animator.SetBool("isMoving", true);
        if(context.canceled){
            animator.SetBool("isMoving", false);
            animator.SetFloat("lastInputX", moveInput.x);
            animator.SetFloat("lastInputY", moveInput.y);
        }
        moveInput = context.ReadValue<Vector2>();
        animator.SetFloat("inputX", moveInput.x);
        animator.SetFloat("inputY", moveInput.y);
    }
    private void UpdateAnimation(){
        bool isMoving = Mathf.Abs(moveInput.y) > 0 || Mathf.Abs(moveInput.x) > 0;
        animator.SetBool("isMoving", isMoving);
    }
}
