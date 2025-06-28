using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private Animator animator;
    private int counter = 0;

    public void Enter(){
        Debug.Log($"{transform.name} enter");
        gameObject.SetActive(true);
        animator.SetBool("active", true);
        animator.SetInteger("counter", counter);
    }




    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisableWeapon(){
        gameObject.SetActive(false);
        animator.SetBool("active", false);
    }
}
