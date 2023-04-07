using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Transform respawnPoint;
    public Vector2 offset;
    public PlayerController playerController;
    public bool isRespawned=false;
    private void Start()
    {
        offset= respawnPoint.transform.position;
    }
    private void Update()
    {
        CheckDied();
    }
    void CheckDied()
    {
        if(!playerController.alive && !isRespawned)
        {
            isRespawned = true;
            Invoke("Respawn", 1f);
        }
    }
    void Respawn()
    {
        transform.position = offset;
    }
}
