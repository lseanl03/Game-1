using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeleportBar : MonoBehaviour
{
    public Slider slider;
    public float fillSpeed;
    public float targetProgress;
    private void Awake()
    {
        slider = GetComponent<Slider>();
        gameObject.SetActive(false);
    }
    private void Update()
    {
        if (gameObject.activeSelf)
        {
            if(slider.value > 0)
            {
                slider.value -= fillSpeed * Time.deltaTime;
            }
            else
            {
                slider.value = slider.maxValue;
                gameObject.SetActive(false);
            }
        }
    }
    public void SetMaxTeleportTime( float maxActiveTime)
    {
        slider.maxValue = maxActiveTime;
    }
    public void SetTeleportTime( float teleportTime)
    {
        gameObject.SetActive(true);
        slider.value = teleportTime;
    }

}
