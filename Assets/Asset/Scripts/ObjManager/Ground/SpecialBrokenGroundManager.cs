using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialBrokenGroundManager : MonoBehaviour
{
    private void Start()
    {
    }
    private void Update()
    {
        foreach (Transform specialBrokenGr in transform)
        {
            if (specialBrokenGr.GetComponent<SpecialBrokenGround>().canBreak)
            {
                specialBrokenGr.GetComponent<SpecialBrokenGround>().SpecialBrokenEvent();
            }
        }
    }
}
