using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static WaveManager instance;

    public WaveObject[] waves;

    [SerializeField]
    private int m_currentWave;

    [SerializeField]
    private float m_timeToNextWave;

    [SerializeField]
    private bool m_canSpawnWaves;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        m_timeToNextWave = waves[0].m_timeToSpawn;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_canSpawnWaves)
        {
            m_timeToNextWave -= Time.deltaTime;
            if (m_timeToNextWave <= 0)
            {
                Instantiate(waves[m_currentWave].m_wave, transform.position, transform.rotation);

                if (m_currentWave < waves.Length - 1)
                {
                    m_currentWave++;
                    m_timeToNextWave = waves[m_currentWave].m_timeToSpawn;
                }
                else
                {
                    m_canSpawnWaves = false;
                }

            }
            
        }
        
    }
    public void ContinueSpawning()
    {
        if (m_currentWave <= waves.Length - 1 && m_timeToNextWave > 0)
        {
            m_canSpawnWaves = true;
        }
    }

    public void StopSpawnWaves()
    {
        m_canSpawnWaves = false;
    }
}

[System.Serializable]
public class WaveObject
{
    public float m_timeToSpawn;
    public EnemyWaveLogic m_wave;
}
