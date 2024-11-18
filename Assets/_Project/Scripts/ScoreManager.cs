using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private int m_Score;
    [SerializeField] private Text m_ScoreText;

    public void ResetScore()
    {
        SetScore(0);
    }

    public void IncrementScore()
    {
        SetScore(m_Score + 1); 
    }

    public void SetScore(int score)
    {
        m_Score = score;
        m_ScoreText.text = score.ToString();
    }

}
