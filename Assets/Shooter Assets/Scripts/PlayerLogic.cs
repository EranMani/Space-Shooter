using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic : MonoBehaviour
{
    public static PlayerLogic instance;

    [SerializeField]
    private float m_playerSpeed;

    [SerializeField]
    private Rigidbody2D m_rb;

    [SerializeField]
    private Transform m_bottomLeftLimit, m_topRightLimit;

    [SerializeField]
    private Transform m_shotPoint;

    [SerializeField]
    private GameObject m_shotPrefab;

    [SerializeField]
    private float m_timeBetweenShots = 1f;
    private float m_shotCounter;

    private float m_normalSpeed;

    [SerializeField]
    private float m_boostSpeed, m_boostLength;

    private float m_boostCounter;

    private bool m_doubleShotActive;

    private bool m_stopMovement;

    [SerializeField]
    private float m_doubleShotOffset;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        m_shotCounter = m_timeBetweenShots;
        m_normalSpeed = m_playerSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_stopMovement)
        {
            return;
        }

        PlayerMovement();

        if (Input.GetButtonDown("Fire1"))
        {
            PlayerShotOnce();
        }

        if (Input.GetButton("Fire1"))
        {
            PlayerShotAuto();
        }

        if (m_boostCounter > 0)
        {
            m_boostCounter -= Time.deltaTime;
            if (m_boostCounter <= 0)
            {
                m_playerSpeed = m_normalSpeed;
            }
        }
    }

    private void PlayerMovement()
    {
        m_rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * m_playerSpeed;

        float xLimit = Mathf.Clamp(transform.position.x, m_bottomLeftLimit.position.x, m_topRightLimit.position.x);
        float yLimit = Mathf.Clamp(transform.position.y, m_bottomLeftLimit.position.y, m_topRightLimit.position.y);

        transform.position = new Vector3(xLimit, yLimit, transform.position.z);
    }

    private void PlayerShotOnce()
    {
        if (!m_doubleShotActive)
        {
            Instantiate(m_shotPrefab, m_shotPoint.position, Quaternion.identity);
            m_shotCounter = m_timeBetweenShots;
        }
        else
        {
            Instantiate(m_shotPrefab, m_shotPoint.position + new Vector3(0f, m_doubleShotOffset, 0f), Quaternion.identity);
            Instantiate(m_shotPrefab, m_shotPoint.position - new Vector3(0f, m_doubleShotOffset, 0f), Quaternion.identity);
        }
        
    }

    private void PlayerShotAuto()
    {
        m_shotCounter -= Time.deltaTime;
        if (m_shotCounter <= 0)
        {
            if (!m_doubleShotActive)
            {
                Instantiate(m_shotPrefab, m_shotPoint.position, Quaternion.identity);
                m_shotCounter = m_timeBetweenShots;
            }
            else
            {
                Instantiate(m_shotPrefab, m_shotPoint.position + new Vector3(0f, m_doubleShotOffset, 0f), Quaternion.identity);
                Instantiate(m_shotPrefab, m_shotPoint.position - new Vector3(0f, m_doubleShotOffset, 0f), Quaternion.identity);
            }
            m_shotCounter = m_timeBetweenShots;
        }
    }

    public void ActivateSpeedBoost()
    {
        m_boostCounter = m_boostLength;
        m_playerSpeed = m_boostSpeed;
    }

    public void ActivateDoubleShot()
    {
        m_doubleShotActive = true;
    }

    public void DeactivateDoubleShot()
    {
        m_doubleShotActive = false;
    }

    public void StopPlayerMovement()
    {
        m_stopMovement = true;
    }

    public void EnablePlayerMovement()
    {
        m_stopMovement = false;
    }

    public float GetBoostSpeed()
    {
        return m_boostSpeed;
    }

    public float GetNormalSpeed()
    {
        return m_normalSpeed;
    }
}
