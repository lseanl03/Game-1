using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Interactable : MonoBehaviour
{
    public bool turnOn = false;
    public abstract void Interact();
    private void Update()
    {

    }

}