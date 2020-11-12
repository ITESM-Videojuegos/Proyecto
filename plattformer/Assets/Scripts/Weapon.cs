using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bullet;
    private bool canShoot = true;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            if (canShoot)
            {
                Shoot();
            }
        }
    }

    private void Shoot()
    {
        //Logica de disparo
        FindObjectOfType<AudioManager>().Play("playerShoot");
        Instantiate(bullet, firePoint.position, firePoint.rotation);
        StartCoroutine(ShootWait());
    }

    private IEnumerator ShootWait()
    {
        canShoot = false;
        yield return new WaitForSeconds(0.8f);
        canShoot = true;
    }
}
