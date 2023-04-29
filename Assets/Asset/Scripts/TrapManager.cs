using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapManager : MonoBehaviour
{
    int damage = 2;
    private void Start()
    {
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
            playerHealth.PlayerTakeDamage(damage);
        }
    }
}
