using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStamina : MonoBehaviour
{
    public StaminaBar staminaBar;
    public PlayerController playerController;
    private void Start()
    {
        playerController=GetComponent<PlayerController>();
        staminaBar.SetStamina(playerController.nextTimeDash);
    }
    private void Update()
    {
        staminaBar.UseStamina(playerController.timeBetweenDash);
    }
}
