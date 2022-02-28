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
    public TextMeshProUGUI _UINumberOfEnemiesToKill;
    public TextMeshProUGUI _UILives;
    private int numberOfEnemiesKilled;
    public int numberOfEnemiesToKill;

    private void Start()
    {
        Time.timeScale = 1;
        numberOfEnemiesKilled = 0;
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
        Time.timeScale = 0;
    }

    public void pauseGame()
    {
        _pauseCanvas.enabled = true;
        Time.timeScale = 0;
    }

    public void EnemyKilled()
    {
        numberOfEnemiesKilled++;
        _UINumberOfEnemiesToKill.text = "Left: " + (numberOfEnemiesToKill - numberOfEnemiesKilled).ToString();
        if(numberOfEnemiesKilled >= numberOfEnemiesToKill)
        {
            openGameStateCanvas("Winner");
        }
    }
}
