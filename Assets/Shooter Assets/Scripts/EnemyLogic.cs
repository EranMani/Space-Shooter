using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLogic : MonoBehaviour
{
    [SerializeField]
    private float m_moveSpeed;

    [SerializeField]
    private Vector2 m_movementDirection;

    [SerializeField]
    private bool m_shouldChangeDirection;

    [SerializeField]
    private bool m_shouldStopAndShootPlayer;

    [SerializeField]
    private float m_changeDirectionXPoint;

    [SerializeField]
    private Vector2 m_chagedDirection;

    [SerializeField]
    private float m_timeBetweenShots, m_shotCounter;

    [SerializeField]
    private GameObject m_shotPrefab;

    [SerializeField]
    private Transform m_shotPoint;

    [SerializeField]
    private Transform m_stopPointToShoot;

    [SerializeField]
    private bool m_canShot;

    [SerializeField]
    private int m_currentHealth;

    [SerializeField]
    private GameObject m_deathEffect;

    [SerializeField]
    private GameObject m_explosionEffect;

    [SerializeField]
    private Transform m_playerPosition;

    [SerializeField]
    private GameObject[] m_powerups;

    [SerializeField]
    private int m_dropSuccessRate = 15;

    private bool m_allowShooting;

    private void Start()
    {
        m_shotCounter = m_timeBetweenShots;
    }

    // Update is called once per frame
    void Update()
    {
        EnemyMovement();
        EnemyShot();           
    }

    private void EnemyMovement()
    {
        // transform.position -= new Vector3(m_moveSpeed * Time.deltaTime, 0f, 0f);
        if (m_shouldStopAndShootPlayer)
        {
            if (transform.position.x > m_stopPointToShoot.position.x)
            {
                transform.position += new Vector3(m_movementDirection.x * m_moveSpeed * Time.deltaTime,
                                              m_movementDirection.y * m_moveSpeed * Time.deltaTime,
                                              0f);
            }
            else
            {
                m_moveSpeed = 1;
                Vector3 dir = transform.position - m_playerPosition.position;
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
        }

        if (!m_shouldChangeDirection)
        {
            transform.position += new Vector3(m_movementDirection.x * m_moveSpeed * Time.deltaTime,
                                              m_movementDirection.y * m_moveSpeed * Time.deltaTime,
                                              0f);
        }
        else
        {
            if (transform.position.x > m_changeDirectionXPoint)
            {
                transform.position += new Vector3(m_movementDirection.x * m_moveSpeed * Time.deltaTime,
                                                  m_movementDirection.y * m_moveSpeed * Time.deltaTime,
                                                  0f);
            }
            else
            {
                transform.position += new Vector3(m_chagedDirection.x * m_moveSpeed * Time.deltaTime,
                                                  m_chagedDirection.y * m_moveSpeed * Time.deltaTime,
                                                  0f);
            }
        }
    }

    private void EnemyShot()
    {
        if (m_allowShooting)
        {
            m_shotCounter -= Time.deltaTime;
            if (m_shotCounter <= 0)
            {
                Instantiate(m_shotPrefab, m_shotPoint.position, transform.rotation);
                m_shotCounter = m_timeBetweenShots;
            }
        }
    }

    public void DamageEnemy()
    {
        m_currentHealth--;
        if (m_currentHealth <= 0)
        {
            int randomChance = Random.Range(0, 100);
            if (randomChance < m_dropSuccessRate)
            {
                int randomPick = Random.Range(0, m_powerups.Length - 1);
                Instantiate(m_powerups[randomPick], transform.position, Quaternion.identity);
            }
            
            Destroy(this.gameObject);
            Instantiate(m_deathEffect, transform.position, Quaternion.identity);
            //Instantiate(m_explosionEffect, transform.position, Quaternion.identity);
        }
    }


    // as soon as the object left the screen and is not visible, it will be destroyed
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    // as soon as the object enter the screen and is visible, it will start shooting if he can
    private void OnBecameVisible()
    {
        if (m_canShot)
        {
            m_allowShooting = true;
        }
    }
}
