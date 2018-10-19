using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public Transform firePont;
    public GameObject bulletPrefab;
    public GameObject shootParticle;

    private float timeBtwShots;
    public float startTimeBtwShots;

    // Update is called once per frame
    void Update () {

        if (timeBtwShots <= 0)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                Instantiate(bulletPrefab, firePont.position, firePont.rotation);
                Instantiate(shootParticle, firePont.position, firePont.rotation);
                timeBtwShots = startTimeBtwShots;
            }
        }
        else{
            timeBtwShots -= Time.deltaTime;
        }
    }
}
