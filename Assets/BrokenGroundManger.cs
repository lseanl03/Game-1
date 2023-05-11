using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenGroundManger : MonoBehaviour
{
    private void Start()
    {
    }
    private void Update()
    {
        foreach (Transform brokenGr in transform)
        {
            if (brokenGr.GetComponent<BrokenGround>().canActive)
            {
                brokenGr.GetComponent<BrokenGround>().BrokenEvent();
            }
        }
    }
}
