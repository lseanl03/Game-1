using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSwitch : Interactable
{
    public bool turnOn=false;
    private void Update()
    {
        
    }
    public override void Interact()
    {
        CheckInteract();
    }

    public void CheckInteract()
    {
        Debug.Log("turn on " + turnOn);
        if (turnOn)
        {
            return;
        }
        else
        {
            turnOn =true;
        }
        SpriteRenderer[] sprite = GetComponentsInChildren<SpriteRenderer>();
        for (int i = 0; i < sprite.Length; i++)
        {
            if (sprite[i].name == "Button")
            {
                if (turnOn)
                {
                    sprite[i].color = Color.green;
                }
                else
                {
                    sprite[i].color = Color.red;
                }            
            }
        }
    }
}
