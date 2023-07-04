using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHeathBar : MonoBehaviour
{
    public Slider slider;
    public float targetProgress = 0f;
    private void Awake()
    {
        slider = GetComponent<Slider>();
        gameObject.SetActive(false);
    }
    private void Start()
    {
    }
    private void FixedUpdate()
    {

    }
    private void Update()
    {
        if (gameObject.activeSelf)
        {
            if(slider.value > targetProgress)
            {
                float fillSpeed = targetProgress + slider.maxValue;
                slider.value -= fillSpeed * Time.deltaTime;
            }
        }
    }
    public void SetHealth(int health)
    {
        if (health < slider.maxValue)
            gameObject.SetActive(true);
        targetProgress = health;
    }
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }
}
