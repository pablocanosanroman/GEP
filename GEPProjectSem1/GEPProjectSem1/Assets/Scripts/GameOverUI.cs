using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public void RestartLevel()
    {
        SceneManager.LoadScene("Level1", LoadSceneMode.Single);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("IntroScreen", LoadSceneMode.Single);
    }
}
