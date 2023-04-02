using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayManager : MonoBehaviour
{

    private void Start()
    {
        gameObject.SetActive(false);
    }
    public void GameOver()
    {
        gameObject.SetActive(true);
    }
    public void Restart()
    {
        SceneManager.LoadScene("Start");
    }
}
