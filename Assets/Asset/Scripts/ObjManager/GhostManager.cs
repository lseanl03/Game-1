using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostManager : MonoBehaviour
{
    public float changeTime = 0.05f;
    public float timer = 0;
    public int count = 0;
    public bool active = false;
    private void Start()
    {
    }
    private void Update()
    {

    }
    public void Active()
    {
        GameObject player = GameObject.Find("Player");
        timer += Time.deltaTime;
        if (timer > changeTime)
        {
            timer = 0;
            if (count < transform.childCount)
            {
                Transform gh = transform.GetChild(count);
                if(!gh.gameObject.activeSelf)
                {
                    gh.gameObject.SetActive(true);
                    gh.transform.position = player.transform.position;
                    count++;
                }
            }
            else
            {
                count = 0;
            }
        }
    }
}
