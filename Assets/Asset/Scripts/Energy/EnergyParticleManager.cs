using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyParticleManager : MonoBehaviour
{
    public float speed = 20;

    public GameObject energyParticlePrefab;
    public GameObject newEnergyParticle;
    public List<GameObject> energyParticles;
    public Vector3 newVector = new Vector3(0, 2f, 0f);

    private void Start()
    {
        energyParticles = new List<GameObject>();
    }
    private void Update()
    {
        if(newEnergyParticle != null)
        {
            MoveEnergyParticles();
        }
    }
    public void SpawnEnergyParticle(GameObject obj)
    {
            newEnergyParticle = Instantiate(energyParticlePrefab, obj.transform.position + newVector, Quaternion.identity);
            energyParticles.Add(newEnergyParticle);
    }

    public void MoveEnergyParticles()
    {
        GameObject player = GameObject.Find("Player");
        if (player != null)
        {
            for (int i = 0; i < energyParticles.Count; i++)
            {
                GameObject particle = energyParticles[i];
                if (particle != null)
                {
                    particle.transform.position = 
                        Vector3.MoveTowards(particle.transform.position, player.transform.position + newVector, speed * Time.deltaTime);
                }
            }
        }
    }
}
