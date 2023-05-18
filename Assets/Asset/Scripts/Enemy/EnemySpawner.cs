using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemies;
    public Transform[] spawnPoints;
    public Transform holder;
    public float spawnTime = 3f;
    public float timer;
    public int count = 0;
    private void Start()
    {
        timer = spawnTime;
    }
    private void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            Spawn();
            timer = spawnTime;
        }
    }
    void Spawn()
    {
        if(count < 5)
        {
            if (spawnPoints.Length < 0 || enemies.Length < 0)
            {
                return;
            }
            int enemyIndex = Random.Range(0, enemies.Length);
            int pointIndex = Random.Range(0,0);
            GameObject enemy = Instantiate(enemies[enemyIndex], spawnPoints[pointIndex].transform.position, Quaternion.identity);
            count++;
            holder = transform.Find("Holder");
            enemy.transform.parent = holder.transform;
        }
    }
}
