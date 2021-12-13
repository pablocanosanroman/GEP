using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinUI : MonoBehaviour
{
    public Text m_ScoreText;
    public Text m_PlayerHighScore;
    [SerializeField] private InGameUI m_InGameUI;
    [SerializeField] private GameObject m_NewRecord;
    public int m_Points;

    private void Start()
    {
        m_PlayerHighScore.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
    }

    private void Update()
    {
        ShowScore();
    }

    private void ShowScore()
    {
        m_Points = m_InGameUI.m_Points;
        m_ScoreText.text = m_Points.ToString();

        if(m_Points > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", m_Points);

            m_PlayerHighScore.text = m_Points.ToString();

            m_NewRecord.SetActive(true);
        }
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
