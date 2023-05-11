using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    public Slider slider;
    private void Awake()
    {
        slider=GetComponent<Slider>();
    }
    public void SetMaxEnergy(float energy)
    {
        slider.maxValue = energy;
    }
    public void SetEnergy(float energy)
    {
        slider.value = energy;
    }
}
