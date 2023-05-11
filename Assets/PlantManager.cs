using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantManager : MonoBehaviour
{
    public Animator animator;
    private void Start()
    {
        animator = GetComponentInParent<Animator>();
    }
    private void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            animator.SetTrigger("PlayerCollision");
        }
        else
        {
            animator.SetTrigger("ObjCollision");
        }
    }
    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Player"))
    //        animator.SetBool("PlayerCollision 0", true);
    //    else
    //    {
    //        animator.SetBool("ObjCollision 0", true);
    //    }
    //}
}
