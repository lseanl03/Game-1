using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHeathBar : MonoBehaviour
{
    public Slider slider;
    private void Start()
    {
        slider = GetComponent<Slider>();
        gameObject.SetActive(false);
    }
    public void SetHealth(int health)
    {
        if (health < slider.maxValue)
            gameObject.SetActive(true);
        slider.value = health;
        if (health <= 0)
            Invoke("Destroy", 0.5f);
    }
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }
    void Destroy()
    {
        Destroy(gameObject);
    }
}
