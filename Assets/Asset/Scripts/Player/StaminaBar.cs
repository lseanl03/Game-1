using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    public Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();
        gameObject.gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (gameObject.activeSelf)
        {
            GameObject player = GameObject.Find("Player");
            if (player != null)
            {
                transform.position = player.transform.position + new Vector3(1.5f, 1.5f);
            }
        }
    }
    public void SetStamina(float energy)
    {
        slider.minValue = 0;
        slider.maxValue = energy;
    }
    public void UseStamina(float energy)
    {
        slider.value = energy;
        if(slider.value ==slider.minValue || slider.value==slider.maxValue)
        {
            gameObject.gameObject.SetActive(false);
        }
        else
        {
            gameObject.gameObject.SetActive(true);
        }
    }
}
