using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public bool flipped = false;
    private void Start()
    {
    }

    private void Update()
    {
        if (transform.localScale.x != transform.parent.parent.localScale.x)
        {
            if(!flipped)
            {
                Vector3 newLocal = transform.localScale;
                newLocal.x = transform.parent.parent.localScale.x;
                transform.localScale = newLocal;
                flipped = true;
            }
        }
        else
        {
            flipped = false; 
        }
    }
}
