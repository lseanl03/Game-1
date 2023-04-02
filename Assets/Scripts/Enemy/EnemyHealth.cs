using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth = 10;
    public EnemyHeathBar EnemyHeathBar;
    private Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        EnemyHeathBar.SetMaxHealth(maxHealth);
    }
    public void EnemyTakeDamage(int damage)
    {
        currentHealth -= damage;
        EnemyHeathBar.SetHealth(currentHealth);
        animator.SetTrigger("hurt");
        if(currentHealth <= 0)
        {
            Died();
        }
    }
    void Died()
    {
        animator.SetTrigger("die");
        Invoke("Destroy", 1f);
    }
    void Destroy()
    {
        Destroy(gameObject);
    }
}
