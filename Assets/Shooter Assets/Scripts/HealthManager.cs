using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public static HealthManager Instance;

    [SerializeField]
    private int m_currentHealth;

    [SerializeField]
    private int m_maxHealth;

    [SerializeField]
    private GameObject m_deathEffect;

    [SerializeField]
    private float m_noDamageLength = 3f;

    [SerializeField]
    private SpriteRenderer m_SpriteRenderer;

    [SerializeField]
    private GameObject m_playerShield;

    [SerializeField]
    private int m_shieldPowe, m_shieldMaxPower = 2;

    private float m_noDamageCounter;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        m_currentHealth = m_maxHealth;
        UIManager.instance.UpdateHealthBar(m_maxHealth);
        UIManager.instance.UpdateHealthBarValue(m_currentHealth);

        m_shieldPowe = m_shieldMaxPower;
        UIManager.instance.UpdateShieldBar(m_shieldMaxPower);
        UIManager.instance.UpdateShieldBarValue(m_shieldPowe);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_noDamageCounter >= 0)
        {
            m_noDamageCounter -= Time.deltaTime;

            if (m_noDamageCounter <= 0)
            {
                m_SpriteRenderer.color = new Color(m_SpriteRenderer.color.r, m_SpriteRenderer.color.g,
                                           m_SpriteRenderer.color.b, 1f);
            }
        }
    }

    public void DamagePlayer()
    {
        if (m_noDamageCounter <= 0)
        {
            if (m_playerShield.activeInHierarchy)
            {
                m_shieldPowe--;
                UIManager.instance.UpdateShieldBarValue(m_shieldPowe);
                if (m_shieldPowe <= 0)
                {
                    m_playerShield.SetActive(false);
                }

                return;
            }

            m_currentHealth--;

            PlayerLogic.instance.DeactivateDoubleShot();
            UIManager.instance.UpdateHealthBarValue(m_currentHealth);

            if (m_currentHealth <= 0)
            {
                Instantiate(m_deathEffect, transform.position, Quaternion.identity);
                gameObject.SetActive(false);

                GameManager.instance.KillPlayer();

                WaveManager.instance.StopSpawnWaves();
            }
        }
        
    }

    public void Respawn()
    {
        gameObject.SetActive(true);
        m_currentHealth = m_maxHealth;
        UIManager.instance.UpdateHealthBarValue(m_currentHealth);

        m_noDamageCounter = m_noDamageLength;
        m_SpriteRenderer.color = new Color(m_SpriteRenderer.color.r, m_SpriteRenderer.color.g,
                                           m_SpriteRenderer.color.b, 0.5f);
    }

    public void ActivateShield()
    {
        m_playerShield.SetActive(true);
        m_shieldPowe = m_shieldMaxPower;

        UIManager.instance.UpdateShieldBar(m_shieldMaxPower);
        UIManager.instance.ActivateShieldUI();
    }

    
}
