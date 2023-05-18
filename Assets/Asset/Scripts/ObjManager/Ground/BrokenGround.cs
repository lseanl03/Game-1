using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenGround : MonoBehaviour
{
    public float timeBetweenBroken = 0f;
    public float brokenTime = 1f;
    public bool canBreak = false;
    public bool canActive = false;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player") 
            && collision.collider.GetComponent<PlayerController>().IsGrounded())
        {
            canBreak = true;
            canActive = true;
        }         
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player") 
            && collision.collider.GetComponent<PlayerController>().IsGrounded())
        {
            canBreak = false;
        }
    }
    public void BrokenEvent()
    {
        if (canBreak) //upon collision
        {
            timeBetweenBroken+=Time.deltaTime;
            if(timeBetweenBroken >= brokenTime)
            {
                canBreak = false;
                timeBetweenBroken = 0f;
                gameObject.SetActive(false);
            }
        }
        else // escape collision or start game
        {
            if (!gameObject.activeSelf)
            {
                if (timeBetweenBroken >= brokenTime)
                {
                    gameObject.SetActive(true);
                    timeBetweenBroken = 0f;
                    canActive = false;
                }
                else
                {
                    timeBetweenBroken += Time.deltaTime;
                }
            }
        }
    }
}
