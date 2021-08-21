using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScrollerLogic : MonoBehaviour
{
    [SerializeField]
    private Transform m_bg1, m_bg2;

    [SerializeField]
    private float m_scrollSpeed;

    private float m_bgWidth;

    // Start is called before the first frame update
    void Start()
    {
        m_bgWidth = m_bg1.GetComponent<SpriteRenderer>().sprite.bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        m_bg1.position -= new Vector3(m_scrollSpeed * Time.deltaTime, 0f, 0f);
        m_bg2.position -= new Vector3(m_scrollSpeed * Time.deltaTime, 0f, 0f);

        if (m_bg1.position.x < -m_bgWidth - 1)
        {
            m_bg1.position += new Vector3(m_bgWidth * 2, 0f, 0f);
        }

        if (m_bg2.position.x < -m_bgWidth - 1)
        {
            m_bg2.position += new Vector3(m_bgWidth * 2, 0f, 0f);
        }


    }
}
