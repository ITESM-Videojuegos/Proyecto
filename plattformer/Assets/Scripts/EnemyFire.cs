using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    public Transform enemyFirePoint;
    public GameObject bullet;
    
    public Transform player;

    Boolean bandera;

    [SerializeField]
    private float agroRange;
    private float disToPlayer;
    private bool facingRight = true;


    private IEnumerator enumerador;
    private Coroutine corrutina;

    // Update is called once per frame
    private void Start()
    {
        enumerador = Shoot();
        bandera = true;
    }
    void Update()
    {
        
        disToPlayer = Vector2.Distance(transform.position, player.position);

        if (disToPlayer < agroRange && bandera)
        {
            if (player.transform.position.x - transform.position.x < 0 && facingRight){
                transform.Rotate(0f, 180f, 0f);
                corrutina = StartCoroutine(enumerador);
                bandera = false;
                facingRight = false;
            }
            else if (player.transform.position.x - transform.position.x > 0 && !facingRight){
                transform.Rotate(0f, 180f, 0f);
                corrutina = StartCoroutine(enumerador);
                bandera = false;
                facingRight = true;
            }
            else{
                corrutina = StartCoroutine(enumerador);
                bandera = false;
            }
        }
        else if (disToPlayer > agroRange && !bandera)
        {
            StopAllCoroutines();
            bandera = true;
        }

    }


    private IEnumerator Shoot()
    {
        //Logica de disparo
        while (true)
        {
            Instantiate(bullet, enemyFirePoint.position, enemyFirePoint.rotation);
            yield return new WaitForSeconds(2.5f);
        }

    }
}
