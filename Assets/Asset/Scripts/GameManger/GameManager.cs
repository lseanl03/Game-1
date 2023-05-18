using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public PlayerController controller;
    private void Start()
    {
        gameObject.SetActive(false);
    }
    private void Update()
    {
        if(!controller.alive)
        {
            gameObject.SetActive(true);
        }
    }
    public void Restart()
    {
        SceneManager.LoadScene("Start1");
    }
}
