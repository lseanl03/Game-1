using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void GameStart()
    {
        SceneManager.LoadScene("Start");
    }
    public void Options()
    {
        Debug.Log("Options");
    }
    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

}
