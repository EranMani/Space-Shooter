using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOverTimeLogic : MonoBehaviour
{
    [SerializeField]
    private float m_lifeTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, m_lifeTime);
    }
}
