using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField]
    private GameObject m_gameOverScreen;

    [SerializeField]
    private GameObject m_levelCompleteScreen;

    [SerializeField]
    private Text m_livesText;

    [SerializeField]
    private Text m_scoreText, m_highScoreText;

    [SerializeField]
    private Slider m_healthBar, m_shieldBar;

    [SerializeField]
    private GameObject m_playerShieldUI;

    [SerializeField]
    private Text m_endLevelScore, m_endCurrentScore;

    [SerializeField]
    private GameObject m_highScoreNotice;

    [SerializeField]
    private GameObject m_pauseScreen;

    [SerializeField]
    private Slider m_bossHealthSlider;

    [SerializeField]
    private Text m_bossName;

    private string m_mainMenuScene = "MainMenu";

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GameOverScreenActive()
    {
        m_gameOverScreen.SetActive(true);
    }

    public void LevelCompleteScreenActive()
    {
        m_levelCompleteScreen.SetActive(true);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }

    public void QuitToMenu()
    {
        SceneManager.LoadScene(m_mainMenuScene);
        Time.timeScale = 1f;
    }

    public void UpdateLivesText(int lives)
    {
        m_livesText.text = "x " + lives;
    }

    public void UpdateHealthBar(int maxHealth)
    {
        m_healthBar.maxValue = maxHealth;
    }

    public void UpdateHealthBarValue(int value)
    {
        m_healthBar.value = value;
    }

    public void UpdatePlayerScore(int score)
    {
        m_scoreText.text = "SCORE: " + score;
    }

    public void UpdateHighScore(int score)
    {
        m_highScoreText.text = "HI-SCORE: " + score;
    }

    public void UpdateShieldBar(int maxShield)
    {
        m_shieldBar.maxValue = maxShield;
    }

    public void UpdateShieldBarValue(int value)
    {
        m_shieldBar.value = value;
        
    }

    public void ActivateShieldUI()
    {
        m_playerShieldUI.SetActive(true);
    }

    public void UpdateEndCurrentScore(int value)
    {      
        m_endCurrentScore.text = "Total score: " + value;
    }

    public void ActivateEndCurrentScoreText()
    {
        m_endCurrentScore.gameObject.SetActive(true);
    }

    public void UpdateLeveScore(int value)
    {
        m_endLevelScore.text = "Level score: " + value;
    }

    public void ActivateLevelScoreText()
    {
        m_endLevelScore.gameObject.SetActive(true);
    }

    public void ActivateHighScoreNoticeText()
    {
        m_highScoreNotice.gameObject.SetActive(true);
    }

    public void DeactivateHighScoreNoticeText()
    {
        m_highScoreNotice.gameObject.SetActive(false);
    }

    public bool CheckIfPauseScreenActive()
    {
        return m_pauseScreen.activeInHierarchy;
    }

   public void ActivatePauseScreen()
    {
        m_pauseScreen.gameObject.SetActive(true);
    }

    public void DeactivatePauseScreen()
    {
        m_pauseScreen.gameObject.SetActive(false);
    }

    public void ResumeGame()
    {
        GameManager.instance.PauseUnpause();
    }

    public void SetBossName(string name)
    {
        m_bossName.text = name;
    }

    public void SetBossMaxHealthSlider(int value)
    {
        m_bossHealthSlider.maxValue = value;
    }

    public void UpdateBossHealthSlider(int value)
    {
        m_bossHealthSlider.value = value;
    }

    public void ActivateBossHealthBar()
    {
        m_bossHealthSlider.gameObject.SetActive(true);
    }

    public void DeactivateBossHealthBar()
    {
        m_bossHealthSlider.gameObject.SetActive(false);
    }
}
