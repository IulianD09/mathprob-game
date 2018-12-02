using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float speed = 20f;
    public int damage = 5;
    public float lifetime;

    public Rigidbody2D rb;
    public GameObject destroyEffect;
    
	private void Start ()
    {
        rb.velocity = transform.right * speed;
        if (Input.GetButton("Shoot") && Input.GetButton("LookUp"))
        {
            rb.velocity = transform.up * speed;

            transform.Rotate(0, 0, 90);
        }
        Invoke("DestroyBullet", lifetime);
    }
   
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Boss boss = hitInfo.GetComponent<Boss>();
        if (boss != null)
        {
            boss.TakeDamage(damage);
            Destroy(gameObject);
        }

    }
    void DestroyBullet()
    {
       // Instantiate(destroyEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
