using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public int maxHealth = 200;

    public int currentHealth;
    
    public int xp = 27;
    
    private Animator m_animator;

    
    // Start is called before the first frame update
    void Start()
    {
        m_animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        m_animator.SetBool("isDead", false);
    }

    public void TakeDamage(int dmg)
    {
        currentHealth -= dmg;
        //Debug.Log("vida tontito = " + currentHealth + " de " + maxHealth);

        if (!m_animator.GetBool("isDead"))
        {
            m_animator.SetTrigger("Hurt");
        }

        if (currentHealth <= 0)
        {
            Die();
        }
        
    }

    private void Die()
    {
        m_animator.SetTrigger("Death");
        GetComponent<Collider2D>().enabled = false;
    }
}
