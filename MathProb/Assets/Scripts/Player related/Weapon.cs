﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public Transform firePoint;
    public GameObject bulletPrefab;

    private float timeBtwShots;
    public float startTimeBtwShots;

    // Update is called once per frame
    void Update()
    {

        if (timeBtwShots <= 0)
        {
            if (Input.GetKey(GameManager.GM.shoot))
            {
                Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                
                timeBtwShots = startTimeBtwShots;
            }
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }
}