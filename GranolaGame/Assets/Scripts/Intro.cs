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

    // Start is called before the first frame update
    void Start()
    {
        creditsCanvas.enabled = false;
        optionsCanvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
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
