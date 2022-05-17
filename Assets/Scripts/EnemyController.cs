using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public int maxHealth = 200;

    public int currentHealth;
    
    public int xp = 27;
    
    private Animator _mAnimator;

    private int _dmg = 30;

    
    // Start is called before the first frame update
    void Start()
    {
        _mAnimator = GetComponent<Animator>();
        currentHealth = maxHealth;
        _mAnimator.SetBool("isDead", false);
    }

    public void TakeDamage(int dmg)
    {
        currentHealth -= dmg;
        Debug.Log("vida tontito = " + currentHealth + " de " + maxHealth);

        if (!_mAnimator.GetBool("isDead"))
        {
            _mAnimator.SetTrigger("Hurt");
        }

        if (currentHealth <= 0)
        {
            Die();
        }
        
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        // cuando alguien toque a un enemigo, le resta vida
        if (col.gameObject.CompareTag("Player"))
        {
            col.gameObject.GetComponent<BanditController>().TakeDamage(_dmg);
            Debug.Log("el enemigo ha chocado con el jugador");
        }
    }

    public void Die()
    {
        _mAnimator.SetTrigger("Death");
        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject, 1f);
    }
}
