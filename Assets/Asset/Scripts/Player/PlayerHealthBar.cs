using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    public Slider slider;
    public float targetProgress;
    public float fillSpeed;
    private void Awake()
    {
        slider = GetComponent<Slider>();
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
        if (slider.value < targetProgress)
        {
            slider.value += fillSpeed * Time.deltaTime;
        }
    }
    public void SetHealth(int health)
    {
        slider.value = health;
    }
    public void SetMaxHeath(int health)
    {
        slider.maxValue = health;
    }
    public void UpdateHealth(float health)
    {
        targetProgress = health;
    }
}
