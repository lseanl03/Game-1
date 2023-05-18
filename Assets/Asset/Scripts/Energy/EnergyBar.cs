using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    public Slider slider;
    public float targetProgress;
    public float fillSpeed;
    private void Awake()
    {
        slider=GetComponent<Slider>();
    }
    private void Start()
    {
    }
    private void Update()
    {
        if (slider.value > targetProgress)
        {
            slider.value -= fillSpeed * Time.deltaTime;
        }
        if(slider.value < targetProgress)
        {
            slider.value += fillSpeed * Time.deltaTime;
        }
    }
    public void SetEnergy(float energy)
    {
        slider.value = energy;
    }
    public void SetMaxEnergy(float energy)
    {
        slider.maxValue = energy;
    }
    public void UpdateEnergy(float energy)
    {
        targetProgress = energy;
    }
}
