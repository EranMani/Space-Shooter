using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupLogic : MonoBehaviour
{
    [SerializeField]
    private bool m_isShield, m_isSpeed, m_isDoubleShot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (m_isShield)
            {
                HealthManager.Instance.ActivateShield();
            }

            if (m_isSpeed)
            {
                PlayerLogic.instance.ActivateSpeedBoost();
            }

            if (m_isDoubleShot)
            {
                PlayerLogic.instance.ActivateDoubleShot();
            }
        }

        Destroy(this.gameObject);
    }
}
