using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private PlayerStateManager player;
    [SerializeField] private HealthBar healthBar;
    private int currentHealth;
    void Awake()
    {
        player = GetComponent<PlayerStateManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = 100;
        healthBar.SetFullHealth(currentHealth);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DamageHealth(int damage){
        currentHealth -= damage;
        healthBar.DamageHealth(damage);
        if(currentHealth <= 0){
            player.Die();
        }else{
            player.SwitchStates(player.hitState);
        }
    }
}
