using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Intro : MonoBehaviour
{
    [SerializeField] Button playButton;
    [SerializeField] Button creditButton;
    [SerializeField] Button optionsButton;
    [SerializeField] Canvas menuCanvas;
    [SerializeField] Canvas creditsCanvas;
    [SerializeField] Canvas optionsCanvas;
    [SerializeField] Slider sfxVol;
    [SerializeField] Slider musicVol;

    // Start is called before the first frame update
    void Start()
    {
        creditsCanvas.enabled = false;
        optionsCanvas.enabled = false;
        sfxVol.value = PlayerPrefs.GetFloat("SFX Volume");
        musicVol.value = PlayerPrefs.GetFloat("Music Volume");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        PlayerPrefs.SetFloat("SFX Volume", sfxVol.value);
        PlayerPrefs.SetFloat("Music Volume", musicVol.value);
        SceneManager.LoadScene("Game Scene");
    }

    public void OpenCredits()
    {
        creditsCanvas.enabled = true;
        menuCanvas.enabled = false;
    }

    public void CloseCredits()
    {
        creditsCanvas.enabled = false;
        menuCanvas.enabled = true;
    }

    public void OpenOptions()
    {
        optionsCanvas.enabled = true;
        menuCanvas.enabled = false;
    }

    public void CloseOptions() 
    {
        optionsCanvas.enabled = false;
        menuCanvas.enabled = true;
    }
}
