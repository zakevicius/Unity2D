using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Sprite[] lives;
    public Image livesImageDisplay;
    public Text scoreDisplay;
    public GameObject titleScreen;
    private int score = 0;

    public void UpdateLives(int currentLives)
    {
        livesImageDisplay.sprite = lives[currentLives];
    }

    public void UpdateScore(int points)
    {
        score += points;
        scoreDisplay.text = score.ToString();
    }    

    public void ShowTitleScreen()
    {
        titleScreen.SetActive(true);
    }

    public void HideTitleScreen()
    {
        score = 0;
        titleScreen.SetActive(false);
    }
}
