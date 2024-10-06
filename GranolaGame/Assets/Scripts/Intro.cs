using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class Intro : MonoBehaviour
{
    [SerializeField] Canvas menuCanvas;
    [SerializeField] Canvas creditsCanvas;
    [SerializeField] Canvas optionsCanvas;
    [SerializeField] Canvas highscoreCanvas;
    [SerializeField] Slider sfxSlider;
    [SerializeField] Slider musicSlider;
    [SerializeField] TMP_Text highscoreText;

    // Start is called before the first frame update
    void Start()
    {
        creditsCanvas.enabled = false;
        optionsCanvas.enabled = false;
        highscoreCanvas.enabled = false;
        if (!PlayerPrefs.HasKey("SFX Volume"))
        {
            PlayerPrefs.SetFloat("SFX Volume", 1f);
            PlayerPrefs.SetFloat("Music Volume", 1f);
        }
        sfxSlider.value = PlayerPrefs.GetFloat("SFX Volume");
        musicSlider.value = PlayerPrefs.GetFloat("Music Volume");
        if (!PlayerPrefs.HasKey("Highscore"))
        {
            PlayerPrefs.SetInt("Highscore", 0);
        }
        highscoreText.text = PlayerPrefs.GetInt("Highscore").ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartGame()
    {
        PlayerPrefs.SetFloat("SFX Volume", sfxSlider.value);
        PlayerPrefs.SetFloat("Music Volume", musicSlider.value);
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

    public void OpenHighscore()
    {
        highscoreCanvas.enabled = true;
        menuCanvas.enabled = false;
    }

    public void CloseHighscore()
    {
        highscoreCanvas.enabled = false;
        menuCanvas.enabled = true;
    }
}
