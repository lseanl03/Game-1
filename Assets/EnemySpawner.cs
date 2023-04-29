using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemies;
    public Transform[] spawnPoints;
    private void Start()
    {
        Spawn();
    }
    void Spawn()
    {
        for(int i = 0; i < enemies.Length; i++)
        {
            int randomPoint=Random.Range(0, enemies.Length);
            Instantiate(enemies[i], spawnPoints[randomPoint].transform.position, Quaternion.identity);
        }
    }
}
