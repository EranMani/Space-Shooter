using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShotLogic : MonoBehaviour
{
    [SerializeField]
    private float m_shotSpeed = 5f;

    [SerializeField]
    private GameObject m_impactEffect;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= transform.right * m_shotSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Instantiate(m_impactEffect, transform.position, Quaternion.identity);

        if (other.tag == "Player")
        {
            HealthManager.Instance.DamagePlayer();
        }

        Destroy(this.gameObject);
    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
