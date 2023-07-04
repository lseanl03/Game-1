
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void GameStart()
    {
        SceneManager.LoadScene("Start 1");
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
