using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float m_LifeSpan;

    private void Update()
    {
        m_LifeSpan -= Time.deltaTime; 

        if(m_LifeSpan <= 0)
        {
            Destroy(gameObject);
        }
    }
}
