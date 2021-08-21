using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShotLogic : MonoBehaviour
{
    [SerializeField]
    private float m_shotSpeed = 5f;

    [SerializeField]
    private GameObject m_impactEffect;

    [SerializeField]
    private GameObject m_explosionPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(m_shotSpeed * Time.deltaTime, 0f, 0f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Instantiate(m_impactEffect, transform.position, Quaternion.identity);

        if (other.tag == "SpaceObject")
        {
            Instantiate(m_explosionPrefab, other.transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            GameManager.instance.UpdateScore(15);
        }

        if (other.tag == "Enemy")
        {
            other.GetComponent<EnemyLogic>().DamageEnemy();
            GameManager.instance.UpdateScore(50);
        }

        if (other.tag == "Boss")
        {
            BossManager.instance.DamageBoss();
        }

        Destroy(this.gameObject);
    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
