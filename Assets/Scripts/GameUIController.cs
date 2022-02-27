using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class GameUIController : MonoBehaviour
{
    public Canvas _pauseCanvas;
    public Canvas _gameStateCanvas;
    public TextMeshProUGUI _UIMessage;

    private void Start()
    {
        Time.timeScale = 1;
    }
    public void unPauseGame()
    {
        _pauseCanvas.enabled = false;
        Time.timeScale = 1;
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void openGameStateCanvas(string message)
    {
        _gameStateCanvas.enabled = true;
        _UIMessage.text = message;
    }

    public void pauseGame()
    {
        _pauseCanvas.enabled = true;
        Time.timeScale = 0;
    }
}
