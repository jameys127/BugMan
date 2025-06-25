using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private PlayerAnimationController playerAnimator;
    


    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<PlayerAnimationController>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = moveInput * moveSpeed;
    }


    public void Move(InputAction.CallbackContext context){
        if(context.canceled){
            moveInput = Vector2.zero;
        }else{
            moveInput = context.ReadValue<Vector2>();
        }
        if(moveInput.magnitude > 0.1f || context.canceled){
            playerAnimator.UpdateAnimation(moveInput);
        }
    }
}
