using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyManager : MonoBehaviour
{
    public float maxEnergy = 100;
    public float currentEnergy=50;
    public bool canUseEnergy =false; 
    public EnergyBar energyBar;
    private void Start()
    {
        energyBar.SetMaxEnergy(maxEnergy);  
        energyBar.SetEnergy(currentEnergy);
    }
    public void TakeEnergy(float takeEnergy)
    {
        currentEnergy += takeEnergy;
        if (currentEnergy > maxEnergy)
        {
            currentEnergy = maxEnergy;
        }
        else if (currentEnergy < 0)
        {
            currentEnergy = 0;
        }
        energyBar.SetEnergy(currentEnergy);
        Debug.Log(currentEnergy);
    }
    public void ConsumptionEnergy(float energyConsumption)
    {
        if (currentEnergy >= energyConsumption && currentEnergy <= maxEnergy)
        {
            canUseEnergy = true;
            currentEnergy -= energyConsumption;
        }
        else
        {
            canUseEnergy = false;
        }
        energyBar.SetEnergy(currentEnergy);
        Debug.Log(currentEnergy);
    }
}
