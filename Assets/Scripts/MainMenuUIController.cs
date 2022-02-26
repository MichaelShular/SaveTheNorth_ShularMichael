using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class MainMenuUIController : MonoBehaviour
{
    public Canvas _instructionsCanvas;
    public Canvas _CreditsCanvas;
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
        _instructionsCanvas.enabled = false;
        _CreditsCanvas.enabled = false;
        BackButton.SetActive(false);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void OpenInstructions()
    {
        _instructionsCanvas.enabled = true;
        BackButton.SetActive(true);
    }

    public void OpenCredits()
    {
        _CreditsCanvas.enabled = true;
        BackButton.SetActive(true);
    }
}
