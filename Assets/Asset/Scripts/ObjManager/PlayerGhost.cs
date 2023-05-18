using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGhost : MonoBehaviour
{
    private PlayerController playerController;
    public GhostManager ghostManager;
    private void Start()
    {
        playerController = GetComponent<PlayerController>();
    }
    private void Update()
    {
        if(playerController.isDashing)
        {
            ghostManager.Active();
        }
    }
}
