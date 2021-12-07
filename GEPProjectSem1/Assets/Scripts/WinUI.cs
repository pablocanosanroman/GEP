using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinUI : MonoBehaviour
{
    public Text m_ScoreText;
    [SerializeField] private InGameUI m_InGameUI;
    

    private void Update()
    {
        ShowScore();
    }

    private void ShowScore()
    {
        m_ScoreText.text = m_InGameUI.m_PointsText.text;
    }

    public void RestartButton()
    {
        SceneManager.LoadScene("Level1", LoadSceneMode.Single);
    }

    public void ReturnButton()
    {
        SceneManager.LoadScene("IntroScreen", LoadSceneMode.Single);
    }
}
