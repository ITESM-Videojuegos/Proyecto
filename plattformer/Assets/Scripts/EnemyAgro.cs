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

    Rigidbody2D rb2d;

    EnemyFire ef = new EnemyFire();

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float disToPlayer = Vector2.Distance(transform.position, player.position);
        //print(disToPlayer); 

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
        if (transform.position.x < player.position.x)
        {
            ef.Shoot();

        }
        else
        {
            ef.Shoot();
        }
    }

    void StopShootingPlayer()
    {
        ef.shoot = false;
    }
}
