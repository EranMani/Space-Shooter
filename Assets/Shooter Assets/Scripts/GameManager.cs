using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField]
    private int m_currentLives = 3;

    [SerializeField]
    private float m_respawnTime = 3.0f;

    [SerializeField]
    private int m_currentScore;

    private int m_highScore;

    private bool m_levelEnding;

    private int m_levelScore;

    [SerializeField]
    private string m_nextLevel;

    [SerializeField]
    private float m_waitForLevelEnd = 5f;

    private bool m_canPause;

    private void Awake()
    {
        instance = this;
        
    }

    private void Start()
    {
        UIManager.instance.UpdateLivesText(m_currentLives);
        
        m_highScore = PlayerPrefs.GetInt("HighScore", 0);
        UIManager.instance.UpdateHighScore(m_highScore);

        m_currentScore = PlayerPrefs.GetInt("CurrentScore");
        UIManager.instance.UpdatePlayerScore(m_currentScore);

        m_currentLives = PlayerPrefs.GetInt("CurrentLives", 3);

        m_canPause = true;
    }

    private void Update()
    {
        if (m_levelEnding)
        {
            PlayerLogic.instance.transform.position += new Vector3(PlayerLogic.instance.GetNormalSpeed() * Time.deltaTime, 0f, 0f);
        }

        if (Input.GetKeyDown(KeyCode.Escape) && m_canPause)
        {
            PauseUnpause();
        }
    }

    public void KillPlayer()
    {
        m_currentLives--;
        UIManager.instance.UpdateLivesText(m_currentLives);

        if (m_currentLives > 0)
        {
            // respawn code
            StartCoroutine(RespawnCoroutine(m_respawnTime));
        }
        else
        {
            // game over code
            UIManager.instance.GameOverScreenActive();
            WaveManager.instance.StopSpawnWaves();
            AudioManager.instance.PlayGameOverMusic();
            PlayerPrefs.SetInt("HighScore", m_highScore);

            m_canPause = false;
        }
    }

    private IEnumerator RespawnCoroutine(float time)
    {
        yield return new WaitForSeconds(time);
        HealthManager.Instance.Respawn();
        WaveManager.instance.ContinueSpawning();
    }

    public void UpdateScore(int addedScore)
    {
        m_currentScore += addedScore;
        m_levelScore += addedScore;

        UIManager.instance.UpdatePlayerScore(m_currentScore);

        if (m_currentScore > m_highScore)
        {
            m_highScore = m_currentScore;
            UIManager.instance.UpdateHighScore(m_highScore);
            //PlayerPrefs.SetInt("HighScore", m_highScore);
        }
    }

    public IEnumerator LevelCompleteCoroutine()
    {
        UIManager.instance.LevelCompleteScreenActive();
        UIManager.instance.DeactivateHighScoreNoticeText();
        PlayerLogic.instance.StopPlayerMovement();
        m_levelEnding = true;
        AudioManager.instance.PlayVictoryMusic();

        m_canPause = false;

        yield return new WaitForSeconds(.5f);

        UIManager.instance.UpdateLeveScore(m_levelScore);
        UIManager.instance.ActivateLevelScoreText();

        yield return new WaitForSeconds(.5f);

        PlayerPrefs.SetInt("CurrentScore", m_currentScore);
        UIManager.instance.UpdateEndCurrentScore(m_currentScore);
        UIManager.instance.ActivateEndCurrentScoreText();

        if (m_currentScore > m_highScore)
        {
            yield return new WaitForSeconds(.5f);
            UIManager.instance.ActivateHighScoreNoticeText();
        }

        PlayerPrefs.SetInt("HighScore", m_highScore);
        PlayerPrefs.SetInt("CurrentLives", m_currentLives);

        yield return new WaitForSeconds(m_waitForLevelEnd);

        SceneManager.LoadScene(m_nextLevel);
    }

    public void PauseUnpause()
    {
        if (UIManager.instance.CheckIfPauseScreenActive())
        {
            UIManager.instance.DeactivatePauseScreen();
            Time.timeScale = 1f;
            PlayerLogic.instance.EnablePlayerMovement();
        }
        else
        {
            UIManager.instance.ActivatePauseScreen();
            Time.timeScale = 0f;
            PlayerLogic.instance.StopPlayerMovement();
        }
    }

    public void CanPauseGame(bool can)
    {
        if (can)
        {
            m_canPause = true;
        }
        else
        {
            m_canPause = false;
        }
    }
}
