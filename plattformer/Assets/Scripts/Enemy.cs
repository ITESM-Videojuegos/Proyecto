﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int health = 100;
    private Animator anim;
    private Rigidbody2D rb;
    private Counter counter;
    //private Renderer rend;
    public Color color = Color.white;


    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        //rend = GetComponent<Renderer>();
        try
        {
            counter = GameObject.FindGameObjectWithTag("Counter").GetComponent<Counter>();
        }catch(NullReferenceException ex)
        {
            //print("No hay counter");
        }

        
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            anim.SetTrigger("Death");
            rb.velocity = Vector2.zero;
        }
        else if (health <= 50)
        {
            //rend.material.color = color;
        }

    }

    private void Die()
    {
        FindObjectOfType<AudioManager>().Play("enemyExplosion");
        Destroy(gameObject);
        if(counter == null)
        {
            print("No hay counter");
        }
        else
        {
            counter.enemiesKilled++;
            print("Enemies Killed: " + counter.enemiesKilled);
        }
    }

    public int GetHealth()
    {
        return this.health;
    }

    public void SetHealth(int newHealth)
    {
        this.health = newHealth;
    }
}
