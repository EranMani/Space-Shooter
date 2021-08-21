using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField]
    private AudioSource m_levelMusic, m_bossMusic, m_victoryMusic, m_gameOverMusic;

    private void Awake()
    {
        instance = this;    
    }

    // Start is called before the first frame update
    void Start()
    {
        m_levelMusic.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void StopAllMusic()
    {
        m_levelMusic.Stop();
        m_bossMusic.Stop();
        m_victoryMusic.Stop();
        m_gameOverMusic.Stop();
    }

    public void PlayBossMusic()
    {
        StopAllMusic();
        m_bossMusic.Play();
    }

    public void PlayVictoryMusic()
    {
        StopAllMusic();
        m_victoryMusic.Play();
    }

    public void PlayGameOverMusic()
    {
        StopAllMusic();
        m_gameOverMusic.Play();
    }
}
