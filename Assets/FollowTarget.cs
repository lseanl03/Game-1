using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    private void Start()
    {
    }

    private void Update()
    {
        if (transform.localScale.x != transform.parent.parent.localScale.x)
        {
            Vector3 newLocal = transform.localScale;
            newLocal.x = transform.parent.parent.localScale.x;
            transform.localScale = newLocal;
        }
    }
}
