using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialBrokenGround : MonoBehaviour
{
    public float timeBetweenBroken = 0f;
    public float brokenTime = 3f;
    public bool canBreak = false;
    public void SpecialBrokenEvent()
    {
        if(canBreak)
        {
            timeBetweenBroken += Time.deltaTime;
            if(timeBetweenBroken > brokenTime)
            {
                gameObject.SetActive(false);
                timeBetweenBroken = 0;
            }
        }
    }
}
