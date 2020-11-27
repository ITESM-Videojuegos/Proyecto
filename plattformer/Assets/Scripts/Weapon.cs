using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private Transform firePointCrouch;
    [SerializeField] private GameObject bullet;
    private bool canShoot = true;
    private PlayerController player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

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
        FindObjectOfType<AudioManager>().Play("playerShoot");
        if(!player.isCrouching)
            Instantiate(bullet, firePoint.position, firePoint.rotation);
        else
            Instantiate(bullet, firePointCrouch.position, firePoint.rotation);
        StartCoroutine(ShootWait());
    }

    private IEnumerator ShootWait()
    {
        canShoot = false;
        yield return new WaitForSeconds(0.8f);
        canShoot = true;
    }
}
