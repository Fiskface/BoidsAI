using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSystem : MonoBehaviour
{
    public static Action upgraded;

    public GameObject upgradeMenu;
    public Image xpBar;
    public TextMeshProUGUI scoreText;
    public int xpToLevelUp = 25;
    public int xpIncreasePerLevel = 15;
    private int currentXP = 0;
    private int score = 0;

    private void OnEnable()
    {
        EventBus.enemyKilled += GetXP;
        EventBus.gameOver += GameOver;
    }

    private void OnDisable()
    {
        EventBus.enemyKilled -= GetXP;
        EventBus.gameOver -= GameOver;
    }

    private void GetXP()
    {
        
        score++;
        currentXP++;
        
        if (currentXP >= xpToLevelUp)
        {
            {
                currentXP -= xpToLevelUp;
                xpToLevelUp += xpIncreasePerLevel;
                GetUpgradeOptions();
            }
        }
        UpdateUI();
    }

    private void UpdateUI()
    {
        xpBar.fillAmount = (float)currentXP / (float)xpToLevelUp;
        scoreText.text = score.ToString();
    }

    private void GetUpgradeOptions()
    {
        Time.timeScale = 0;
        upgradeMenu.SetActive(true);
    }

    public void Upgrade()
    {
        Time.timeScale = 1;
        upgradeMenu.SetActive(false);
        upgraded?.Invoke();
    }

    private void GameOver()
    {
        PlayerPrefs.SetInt("score", score);
        SceneManager.LoadScene("GameOver");
    }
}
