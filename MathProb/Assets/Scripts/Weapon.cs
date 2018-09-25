﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public Transform firePont;
    public GameObject bulletPrefab;
    public GameObject shootParticle;
    
    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
           Shoot();
        }
    }
    void Shoot()
    {
        Instantiate(bulletPrefab, firePont.position , firePont.rotation);
        Instantiate(shootParticle, firePont.position, firePont.rotation);
    }
}
