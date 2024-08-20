using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public TextMeshProUGUI scoreText;


    private void Awake()
    {
        scoreText.text = PlayerPrefs.GetInt("score").ToString();
    }

    public void Restart()
    {
        SceneManager.LoadScene("Main");
    }
}
