using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Character : MonoBehaviour, IDamagable
{
    public float m_CurrentHealth;
    private float m_MaxHealth = 60;
    [SerializeField] private Char_Phys m_PlayerPhys;
    private Animator m_PlayerAnimator;

    private void Awake()
    {
        m_PlayerAnimator = GetComponent<Animator>();
    }

    private void Start()
    {
        m_CurrentHealth = m_MaxHealth;
    }

    private void Update()
    {
        Death();
        
    }

    private void Death()
    {

        if(gameObject.transform.position.y < -5)
        {

            Destroy(gameObject);
            SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
        }
        
    }

    public void Damage(float damageTaken)
    {
        m_PlayerAnimator.SetTrigger("Damage_Taken");
        m_CurrentHealth -= damageTaken;
        if (m_CurrentHealth <= 0)
        {
            Destroy(gameObject);
            SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
        }
    }

}
