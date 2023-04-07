using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnergy : MonoBehaviour
{
    public EnergyBar energyBar;
    public PlayerController playerController;
    private void Start()
    {
        playerController=GetComponent<PlayerController>();
        energyBar.SetEnergy(playerController.nextTimeDash);
    }
    private void Update()
    {
        energyBar.UseEnergy(playerController.timeBetweenDash);
    }
}
