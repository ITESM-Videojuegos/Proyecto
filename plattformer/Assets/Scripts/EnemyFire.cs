using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    public Transform enemyFirePoint;
    public GameObject bullet;
    public Boolean shoot;

    // Update is called once per frame
    void Update()
    {

        if (this.shoot) { Shoot(); }

    }

    public void Shoot()
    {
        //Logica de disparo
        Instantiate(bullet, enemyFirePoint.position, enemyFirePoint.rotation);
    }
}
