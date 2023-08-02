using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    public TMPro.TextMeshProUGUI[] playerScoreTexts; // Array to hold the TextMeshProUGUI components for both players' scores

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
            // Use custom labels 'P1:' and 'P2:' along with the respective player scores
            playerScoreTexts[i].text = "P" + (i + 1) + ": " + playerScores[i];
        }
    }
}