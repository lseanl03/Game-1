using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject overlayMenu;
    public GameObject optionMenu;
    public GameObject restartMenu;

    public Slider musicSlider, sfxSlider;


    public bool gameIsPause = false;
    private void Start()
    {
        Time.timeScale = 1.0f;
        HideCanvas(overlayMenu);
        HideCanvas(optionMenu);
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
          if (gameIsPause)
            {
                if(optionMenu.gameObject!=null)
                {
                    HideCanvas(optionMenu);
                }
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }
    public void ResumeGame()
    {
        FindObjectOfType<AudioManager>().PlaySFX("OnClick");
        HideCanvas(overlayMenu);
        gameIsPause = false;
        Time.timeScale = 1;
    }
    public void PauseGame()
    {
        FindObjectOfType<AudioManager>().PlaySFX("OnClick");
        ShowCanvas(overlayMenu);
        Time.timeScale = 0;
        gameIsPause = true;
    }
    public void BackToMainMenu()
    {
        FindObjectOfType<AudioManager>().PlaySFX("OnClick");
        SceneManager.LoadScene("MainMenu");
    }
    public void Restart()
    {
        FindObjectOfType<AudioManager>().PlaySFX("OnClick");
        SceneManager.LoadScene("Start 1");
    }
    public void GameOver()
    {
        ShowCanvas(restartMenu);
    }
    public void Options()
    {
        FindObjectOfType<AudioManager>().PlaySFX("OnClick");
        HideCanvas(overlayMenu);
        ShowCanvas(optionMenu);
    }
    public void CloseOptions()
    {
        HideCanvas(optionMenu);
        ShowCanvas(overlayMenu);
    }
    public void ClosePauseMenu()
    {
        FindObjectOfType<AudioManager>().PlaySFX("OnClick");
        HideCanvas(overlayMenu);
        gameIsPause = false;
        Time.timeScale = 1;
    }
    void HideCanvas(GameObject canvasObj) //?n
    {
        CanvasGroup canvasGroup = canvasObj.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0; //?? trong su?t
        canvasGroup.interactable = false; //t??ng tác 
        canvasGroup.blocksRaycasts = false; //ph?n h?i (chu?t, bàn phím...)
    }
    void ShowCanvas(GameObject canvasObj) //Hi?n th?
    {
        CanvasGroup canvasGroup = canvasObj.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 1; //?? trong su?t
        canvasGroup.interactable = true; //t??ng tác 
        canvasGroup.blocksRaycasts = true; //ph?n h?i (chu?t, bàn phím...)
    }
    public void ToggleMusic() //Chuy?n ??i b?t nh?c
    {
        AudioManager.instance.ToggleMusic();
        FindObjectOfType<AudioManager>().PlaySFX("OnClick");
    }
    public void ToggleSFX() //Chuy?n ??i âm thanh hi?u ?ng 
    {
        AudioManager.instance.ToggleSFX();
        FindObjectOfType<AudioManager>().PlaySFX("OnClick");
    }
    public void MusicVolume() //tích h?p nh?c và slider
    {
        AudioManager.instance.MusicVolume(musicSlider.value);
        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
    }

    public void SFXVolume() //tích h?p hi?u ?ng âm thanh và slider
    {
        AudioManager.instance.SFXVolume(sfxSlider.value);
        PlayerPrefs.SetFloat("SFXVolume", sfxSlider.value);
    }
}