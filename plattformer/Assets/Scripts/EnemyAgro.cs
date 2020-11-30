using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAgro : MonoBehaviour
{
    [SerializeField]
    Transform player;

    [SerializeField]
    float moveSpeed;

    [SerializeField]
    float agroRange;

    private Renderer rend;
    public Color color = Color.white;

    Rigidbody2D rb2d;

    EnemyFire ef = new EnemyFire();

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        float disToPlayer = Vector2.Distance(transform.position, player.position);

        if (disToPlayer < agroRange)
        {
            ShootPlayer();
        }
        else
        {
            StopShootingPlayer();
        }
    }

    void ShootPlayer()
    {
        print("/");
    }

    void StopShootingPlayer()
    {
        print('/');
    }
}
