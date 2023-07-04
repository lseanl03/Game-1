using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 10;
    public int currentHealth;
    public bool isDead = false;
    public PlayerHealthBar healthBar;
    private Animator animator;
    private CapsuleCollider2D capColl2d;
    private BoxCollider2D boxColl2d;
    private void Start()
    {
        animator = GetComponent<Animator>();

        capColl2d = GetComponent<CapsuleCollider2D>();
        capColl2d.enabled = true;

        boxColl2d = GetComponent<BoxCollider2D>();
        boxColl2d.enabled = false;

        currentHealth = maxHealth;
        healthBar.SetMaxHeath(maxHealth);
        healthBar.SetHealth(currentHealth);
        healthBar.targetProgress = currentHealth;
        healthBar.fillSpeed = maxHealth/2;
    }
    public void PlayerTakeDamage(int damage)
    {
        if(isDead) return;
        currentHealth-=damage;
        healthBar.UpdateHealth(currentHealth);
        animator.SetTrigger("hurt");
        if(currentHealth <=0 )
        {
            Died();
        }
    }
    void Died()
    {
        isDead = true;
        capColl2d.enabled = false;
        boxColl2d.enabled = true;
        Debug.Log("isDied");
        animator.SetTrigger("die");  
    }
}
