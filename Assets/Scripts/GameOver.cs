using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField]
    protected TextMeshProUGUI gameOverTextGUI;

    protected const string winText = "YOU WIN\nTime Remaining: {0:0.000}\nExterminations: {1}\nScore: {2}";
    protected const string loseText = "GAME OVER\nExterminations: {0}";

    private void Start()
    {
        var timeRemaining = TimeSpan.FromSeconds(PlayerPrefs.GetFloat("timeRemaining", 0)).ToString("mm':'ss'.'fff");
        var score = (PlayerPrefs.GetInt("exterminations", 0) * PlayerPrefs.GetFloat("timeRemaining", 0) / 60f).ToString("n1");
        var win = PlayerPrefs.GetInt("gameWon", 0);
        gameOverTextGUI.text = win == 1 ?
            string.Format(winText, timeRemaining, PlayerPrefs.GetInt("exterminations", 0), score) :
            string.Format(loseText, PlayerPrefs.GetInt("exterminations", 0));
    }

    private void OnDestroy()
    {
        PlayerPrefs.DeleteAll();
    }
}
