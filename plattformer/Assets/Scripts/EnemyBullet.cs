using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{

    private float speed = 300f;
    private int damage = 50;
    public Rigidbody2D rb;

    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        
        rb.velocity = transform.right * speed * Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            player.TakeDamage(damage);
            Destroy(gameObject);
        }

        Destroy(gameObject, 2);
    }
}
