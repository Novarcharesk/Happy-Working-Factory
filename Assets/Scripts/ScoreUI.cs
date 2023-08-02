using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    public Text[] playerScoreTexts; // Array to hold the Text components for both players' scores

    private int[] playerScores = new int[2]; // Array to hold the scores of Player 1 and Player 2

    public void Start()
    {
        UpdateScoreText();
    }

    public void IncreasePlayer1Score()
    {
        playerScores[0]++;
        UpdateScoreText();
    }

    public void IncreasePlayer2Score()
    {
        playerScores[1]++;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        for (int i = 0; i < playerScoreTexts.Length; i++)
        {
            playerScoreTexts[i].text = "Player " + (i + 1) + " Score: " + playerScores[i];
        }
    }
}