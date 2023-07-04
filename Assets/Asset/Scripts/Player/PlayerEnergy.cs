using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnergy : MonoBehaviour
{
    public float energyInParticle = 10f;
    public EnergyParticleManager energyParticleManager;
    private void Start()
    {
        energyParticleManager = FindObjectOfType<EnergyParticleManager>();
    }
    private void Update()
    {
        CheckParticleEnergy();
    }
    void CheckParticleEnergy()
    {
        if(energyParticleManager.newEnergyParticle != null)
        {
            for (int i = 0; i < energyParticleManager.energyParticles.Count; i++)
            {
                GameObject particle = energyParticleManager.energyParticles[i];
                if (particle != null)
                {
                    if (Vector3.Distance(transform.position + energyParticleManager.newVector, particle.transform.position) < 0.1f)
                    {
                        EnergyManager energyManager = FindObjectOfType<EnergyManager>();
                        energyManager.TakeEnergy(energyInParticle);
                        Destroy(particle);
                        energyParticleManager.energyParticles.RemoveAt(i);
                        i--;
                    }
                }
            }
        }
    }
}
