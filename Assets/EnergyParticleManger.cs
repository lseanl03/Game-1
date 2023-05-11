using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyParticleManger : MonoBehaviour
{
    public float speed = 5;

    public GameObject energyParticle;
    public GameObject eParticle;
    
    private Vector3 newVector = new Vector3(0, 2f, 0f);
    private void Start()
    {
    }
    private void Update()
    {
        MoveEnergyParticle();
    }
    public void SpawnEnergyParticle(GameObject obj)
    {
        eParticle = Instantiate(energyParticle,obj.transform.position+ newVector,Quaternion.identity);
    }
    public void MoveEnergyParticle()
    {
        if(eParticle != null)
        {
            GameObject player = GameObject.Find("Player");
            if (player != null)
            {
                eParticle.transform.position =
                    Vector3.MoveTowards(eParticle.transform.position, player.transform.position + newVector, speed * Time.deltaTime);
                if ((Vector2.Distance(eParticle.transform.position, player.transform.position + newVector)<0.05f))
                {
                    Destroy(eParticle);
                }
            }
        }   
    }
}
