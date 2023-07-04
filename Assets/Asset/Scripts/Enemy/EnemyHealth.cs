using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth = 10;

    public bool isDied = false;
    public EnemyHeathBar EnemyHeathBar;
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
        EnemyHeathBar.SetMaxHealth(maxHealth);
    }
    public void EnemyTakeDamage(int damage)
    {
        if (isDied) return;
        currentHealth -= damage;
        EnemyHeathBar.SetHealth(currentHealth);
        animator.SetTrigger("hurt");
        if(currentHealth <= 0)
        {
            EnergyParticleManager energyParticleManger = FindObjectOfType<EnergyParticleManager>();
            energyParticleManger.SpawnEnergyParticle(gameObject);
            Died();
        }
    }
    void Died()
    {
        isDied = true;
        capColl2d.enabled = false;
        boxColl2d.enabled = true;
        animator.SetTrigger("die");
        Invoke("Destroy", 1f);
    }
    void Destroy()
    {
        Destroy(gameObject);
    }
}
