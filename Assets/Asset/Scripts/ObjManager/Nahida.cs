using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nahida : MonoBehaviour
{
    public bool onCollider=false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            onCollider = true;
            StartCoroutine(HideShow());
        }
    }
    private IEnumerator HideShow()
    {
        yield return new WaitForSeconds(0.5f);
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Color color = spriteRenderer.color;
        if(onCollider)
            color.a = 0.1f;
        else
            color.a = 1f;
        spriteRenderer.color = color;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            onCollider = false;
            StartCoroutine(HideShow());
        }
    }
}
