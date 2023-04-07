using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 10;
    public int currentHealth;
    public PlayerHealthBar healthBar;
    private Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        healthBar.SetMaxHeath(maxHealth);
    }
    public void PlayerTakeDamage(int damage)
    {
        currentHealth-=damage;
        healthBar.SetHealth(currentHealth);
        animator.SetTrigger("hurt");
        if(currentHealth <=0 )
        {
            Died();
        }
    }
    void Died()
    {
        animator.SetTrigger("die");
    }
}
