using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrap : TrapManager
{
    private PlayerHealth playerHealth;
    private void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        damage = playerHealth.maxHealth;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!playerHealth.isDead)
            {
                playerHealth.PlayerTakeDamage(damage);
            }
        }
    }
}
