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
            Instantiate(enemies[i], spawnPoints[i].transform.position, Quaternion.identity);
        }
    }
}
