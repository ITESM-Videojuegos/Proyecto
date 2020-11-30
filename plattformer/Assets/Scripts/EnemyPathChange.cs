using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathChange : MonoBehaviour
{
    public Node[] path;
    public float speed = 1;
    public float threshold;

    private int current;

    [SerializeField]
    Transform player;

    [SerializeField]
    float agroRange;

    private Renderer rend;
    private bool bandera;
    private Enemy enemy;

    void Start()
    {
        current = 0;
        bandera = true;
        enemy = GetComponent<Enemy>();
        rend = GetComponent<Renderer>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(DistanceCheck());
        enemy.SetHealth(150);
    }

    // Update is called once per frame
    void Update()
    {
        float disToPlayer = Vector2.Distance(transform.position, player.position);

        if(enemy.GetHealth() <= 50) { 
            rend.material.color = Color.red;
            bandera = false;
        }

        if (disToPlayer < agroRange)
        {
            if (enemy.GetHealth() <= 100)
            {
                speed = 2f;
            }
            
            if (bandera)
            {
                StartCoroutine(ChangeColor());
                bandera = false;
            }
            StopAllCoroutines();
            agroRange = 100;
            transform.position = Vector2.MoveTowards(transform.position,
               player.transform.position,
               speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position,
               path[current].transform.position,
               speed * Time.deltaTime);
        }

    }
    IEnumerator DistanceCheck()
    {

        while (true)
        {

            // is going to be superhard to get exactly to the point 
            float distance = Vector2.Distance(
                transform.position,
                path[current].transform.position
                );

            // we get there when we are close enough
            if (distance < threshold)
            {

                // move to the next one
                current++;
                // if out of bounds return to 0
                current %= path.Length;
            }

            yield return new WaitForSeconds(0.18f);
        }
    }

    IEnumerator ChangeColor()
    {
        rend.material.color = Color.yellow;
        yield return new WaitForSeconds(0.18f);
    }

}
