using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnergy : MonoBehaviour
{
    public EnergyManager energyManager;
    public EnergyParticleManger energyParticleManger;
    public float energyInParticle = 10f;
    private void Start()
    {
    }
    private void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnergyParticle"))
        {
            Debug.Log("collision");
            energyManager.TakeEnergy(energyInParticle);
        }
    }
}
