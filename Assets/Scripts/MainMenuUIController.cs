using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class MainMenuUIController : MonoBehaviour
{
    public Canvas _controlsCanvas;
    public GameObject _GamePlayCanvas;
    public GameObject _CreditsCanvas;
    public GameObject BackButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("MainLevel");
    }
    public void BackToMenuGame()
    {
        _controlsCanvas.enabled = false;
        _CreditsCanvas.SetActive(false);
        _GamePlayCanvas.SetActive(false);
        BackButton.SetActive(false);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void OpenInstructions()
    {
        _controlsCanvas.enabled = true;
        BackButton.SetActive(true);
    }

    public void OpenCredits()
    {
        _CreditsCanvas.SetActive(true);
        BackButton.SetActive(true);
    }

    public void OpenGameCanvas()
    {
        _GamePlayCanvas.SetActive(true);
        BackButton.SetActive(true);
    }
}
