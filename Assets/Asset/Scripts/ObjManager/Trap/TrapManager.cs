using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapManager : MonoBehaviour
{
    public int damage = 2;
    public bool damageSender = false;
    public ParticleSystem particle;
    private void Start()
    {
        if(gameObject.GetComponent<ParticleSystem>())
        particle = GetComponent<ParticleSystem>();
    }
    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Player"))
        {
            damageSender = true;
            PlayerHealth playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
            if(!playerHealth.isDead)
            {
                playerHealth.PlayerTakeDamage(playerHealth.maxHealth);
                damageSender = false;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            damageSender= true;
            PlayerHealth playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
            if (!playerHealth.isDead)
            {
                playerHealth.PlayerTakeDamage(damage);
            }
        }
    }

}
