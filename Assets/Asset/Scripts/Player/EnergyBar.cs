using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

public class EnergyBar : MonoBehaviour
{
    public Slider slider;
    public GameObject player;

    private void Start()
    {
        slider = GetComponent<Slider>();
        gameObject.gameObject.SetActive(false);
    }

    private void Update()
    {
        transform.position = player.transform.position + new Vector3(1.5f, 1.5f, 0);
    }
    public void SetEnergy(float energy)
    {
        slider.minValue = 0;
        slider.maxValue = energy;
    }
    public void UseEnergy(float energy)
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
