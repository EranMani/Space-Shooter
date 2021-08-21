using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObjectLogic : MonoBehaviour
{
    [SerializeField]
    private float m_moveSpeed;

    // Update is called once per frame
    void Update()
    {
        transform.position -= new Vector3(m_moveSpeed * Time.deltaTime, 0f, 0f);
        //transform.Rotate(new Vector3(0,0,50) * m_moveSpeed * Time.deltaTime);
    }

    // as soon as the object left the screen and is not visible, it will be destroyed
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
