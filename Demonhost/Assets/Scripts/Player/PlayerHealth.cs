using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private PlayerStateManager player;
    private int currentHealth;
    void Awake()
    {
        player = GetComponent<PlayerStateManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = 100;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DamageHealth(int damage){
        currentHealth -= damage;
        Debug.Log(currentHealth);
        if(currentHealth <= 0){
            player.Die();
        }
    }
}
