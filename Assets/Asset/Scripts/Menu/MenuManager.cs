using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject optionMenu;

    public Slider musicSlider, sfxSlider;

    private void Start()
    {
        ShowCanvas(mainMenu);
        HideCanvas(optionMenu);
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1);
        AudioManager.instance.MusicVolume(musicSlider.value);
        AudioManager.instance.SFXVolume(sfxSlider.value);
    }
    public void StartGame()
    {
        FindObjectOfType<AudioManager>().PlaySFX("OnClick");
        SceneManager.LoadScene("Start1");
    }
    public void Options()
    {
        FindObjectOfType<AudioManager>().PlaySFX("OnClick");
        HideCanvas(mainMenu);
        ShowCanvas(optionMenu);
    }
    public void CloseOptions()
    {
        FindObjectOfType<AudioManager>().PlaySFX("OnClick");
        HideCanvas(optionMenu);
        ShowCanvas(mainMenu);
    }
    public void ExitGame()
    {
        FindObjectOfType<AudioManager>().PlaySFX("OnClick");
        Application.Quit();
        Debug.Log("Quit");
    }
    void HideCanvas(GameObject canvasObj) 
    {
        CanvasGroup canvasGroup = canvasObj.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0; //?? trong su?t
        canvasGroup.interactable = false; //t??ng t�c 
        canvasGroup.blocksRaycasts = false; //ph?n h?i (chu?t, b�n ph�m...)
    }
    void ShowCanvas(GameObject canvasObj) //Hi?n th?
    {
        CanvasGroup canvasGroup = canvasObj.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 1; //?? trong su?t
        canvasGroup.interactable = true; //t??ng t�c 
        canvasGroup.blocksRaycasts = true; //ph?n h?i (chu?t, b�n ph�m...)
    }
    public void ToggleMusic() //Chuy?n ??i b?t nh?c
    {
        AudioManager.instance.ToggleMusic();
        FindObjectOfType<AudioManager>().PlaySFX("OnClick");
    }
    public void ToggleSFX() //Chuy?n ??i �m thanh hi?u ?ng 
    {
        AudioManager.instance.ToggleSFX();
        FindObjectOfType<AudioManager>().PlaySFX("OnClick");
    }
    public void MusicVolume() //t�ch h?p nh?c v� slider
    {
        AudioManager.instance.MusicVolume(musicSlider.value);
        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
    }

    public void SFXVolume() //t�ch h?p hi?u ?ng �m thanh v� slider
    {
        AudioManager.instance.SFXVolume(sfxSlider.value);
        PlayerPrefs.SetFloat("SFXVolume", sfxSlider.value);
    }
}
