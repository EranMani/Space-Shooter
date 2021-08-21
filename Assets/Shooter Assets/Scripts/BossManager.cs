using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    public static BossManager instance;

    [SerializeField]
    private string m_bossName;

    [SerializeField]
    private int m_currentHealth = 100;

    [SerializeField]
    // private BattleShot[] m_shotsToFire;
    private BattlePhase[] m_phases;

    [SerializeField]
    private int m_currentPhase;

    [SerializeField]
    private Animator m_bossAnim;

    [SerializeField]
    private GameObject[] m_explosionPrefab;

    [SerializeField]
    private Transform[] m_explostionPoints;

    [SerializeField]
    private GameObject m_bossPrefab;

    [SerializeField]
    private bool m_battleEnding;

    [SerializeField]
    private float m_waitToEndLevel;

    [SerializeField]
    private int m_scoreValue = 1000;

    private void Awake()
    {
        instance = this;    
    }

    // Start is called before the first frame update
    void Start()
    {
        UIManager.instance.SetBossName(m_bossName);
        UIManager.instance.SetBossMaxHealthSlider(m_currentHealth);
        UIManager.instance.UpdateBossHealthSlider(m_currentHealth);
        UIManager.instance.ActivateBossHealthBar();

        AudioManager.instance.PlayBossMusic();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        for (int i = 0; i < m_shotsToFire.Length; i++)
        {
            m_shotsToFire[i].shotCounter -= Time.deltaTime;

            if (m_shotsToFire[i].shotCounter <= 0)
            {
                m_shotsToFire[i].shotCounter = m_shotsToFire[i].timeBetweenShots;
                Instantiate(m_shotsToFire[i].shotPrefab, m_shotsToFire[i].firePoint.position, m_shotsToFire[i].firePoint.rotation);
            }
        }
        */
        if (m_currentPhase == m_phases.Length - 1)
        {
            return;
        }

        if (m_currentHealth <= m_phases[m_currentPhase].healthToEndPhase)
        {
            m_phases[m_currentPhase].removeAtPhaseEnd.SetActive(false);
            Instantiate(m_phases[m_currentPhase].addAtPhaseEnd, m_phases[m_currentPhase].newSpawnPoint.position, m_phases[m_currentPhase].newSpawnPoint.rotation);
            
            m_currentPhase++;

            m_bossAnim.SetInteger("Phase", m_currentPhase + 1);
        }
        else
        {
            for (int i = 0; i < m_phases[m_currentPhase].phaseShots.Length; i++)
            {
                m_phases[m_currentPhase].phaseShots[i].shotCounter -= Time.deltaTime;

                if (m_phases[m_currentPhase].phaseShots[i].shotCounter <= 0)
                {
                    m_phases[m_currentPhase].phaseShots[i].shotCounter = m_phases[m_currentPhase].phaseShots[i].timeBetweenShots;
                    Instantiate(m_phases[m_currentPhase].phaseShots[i].shotPrefab, m_phases[m_currentPhase].phaseShots[i].firePoint.position, m_phases[m_currentPhase].phaseShots[i].firePoint.rotation);
                }
            }
        }
    }

    public void DamageBoss()
    {
        m_currentHealth--;
        UIManager.instance.UpdateBossHealthSlider(m_currentHealth);

        if (m_currentHealth <= 0 && !m_battleEnding)
        {
            StartCoroutine(BossFinalExplosionRoutine());
            
            m_battleEnding = true;
        }
    }

    private IEnumerator BossFinalExplosionRoutine()
    {
        m_bossAnim.enabled = false;
        UIManager.instance.DeactivateBossHealthBar();
        GameManager.instance.UpdateScore(m_scoreValue);

        yield return new WaitForSeconds(.1f);
        Instantiate(m_explosionPrefab[0], m_explostionPoints[0].position, m_explostionPoints[0].rotation);

        yield return new WaitForSeconds(.1f);
        Instantiate(m_explosionPrefab[0], m_explostionPoints[1].position, m_explostionPoints[1].rotation);

        yield return new WaitForSeconds(.1f);
        Instantiate(m_explosionPrefab[0], m_explostionPoints[2].position, m_explostionPoints[2].rotation);

        yield return new WaitForSeconds(.1f);
        Instantiate(m_explosionPrefab[0], m_explostionPoints[3].position, m_explostionPoints[3].rotation);

        yield return new WaitForSeconds(.2f);
        Instantiate(m_explosionPrefab[1], m_explostionPoints[4].position, m_explostionPoints[4].rotation);

        yield return new WaitForSeconds(.3f);
        Instantiate(m_explosionPrefab[1], m_explostionPoints[5].position, m_explostionPoints[5].rotation);

        m_bossPrefab.SetActive(false);
        yield return new WaitForSeconds(m_waitToEndLevel);
        StartCoroutine(GameManager.instance.LevelCompleteCoroutine());

        
    }

    
}

[System.Serializable]
public class BattleShot
{
    public GameObject shotPrefab;
    public float timeBetweenShots;
    public float shotCounter;
    public Transform firePoint;
}

[System.Serializable]
public class BattlePhase
{
    public BattleShot[] phaseShots;
    public int healthToEndPhase;
    public GameObject removeAtPhaseEnd;
    public GameObject addAtPhaseEnd;
    public Transform newSpawnPoint;
}
