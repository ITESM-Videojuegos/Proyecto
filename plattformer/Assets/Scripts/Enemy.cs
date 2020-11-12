using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int health = 100;
    private Animator anim;
    private Rigidbody2D rb;


    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            anim.SetTrigger("Death");
            rb.velocity = Vector2.zero;
        }
            
    }

    private void Die()
    {
        FindObjectOfType<AudioManager>().Play("enemyExplosion");
        Destroy(gameObject);
    }
}
