using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSwitch : Interactable
{
    private void Update()
    {

    }
    public override void Interact()
    {
        SpriteRenderer[] sprite = GetComponentsInChildren<SpriteRenderer>();
        for (int i = 0; i < sprite.Length; i++)
        {
            if (sprite[i].name == "Button")
            {
                turnOn = !turnOn;
                if(turnOn)
                {
                    sprite[i].color = Color.red;
                }
                else
                {
                    sprite[i].color = Color.green;
                }
            }
        }
    }
}
