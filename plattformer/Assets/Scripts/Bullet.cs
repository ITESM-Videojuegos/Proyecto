using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private float speed = 20f;
    private int damage = 50;
    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
       rb.velocity = transform.right * speed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if(enemy != null)
        {
            enemy.takeDamage(damage);
            Destroy(gameObject);
        }

        Destroy(gameObject, 2);
    }
}
