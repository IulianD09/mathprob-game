using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float speed = 20f;
    public int damage = 5;
    public float lifetime;
    public Vector3 diagonal;

    public Rigidbody2D rb;
    public GameObject destroyEffect;
    
	private void Start ()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.velocity = transform.right * speed;

        if (Input.GetKey(GameManager.GM.shoot) && Input.GetButton("LookUp"))
        {
            rb.velocity = transform.up * speed;

            transform.Rotate(0, 0, 90);
        }
        if (Input.GetKey(GameManager.GM.shoot) && (Input.GetButton("LookUp") && ( Input.GetButton("LookLeft") || Input.GetButton("LookRight")) ) )
        {
            if(Input.GetButton("LookLeft"))
            {
                diagonal = transform.up - transform.right;
                rb.velocity = (-diagonal * speed ) /1.5f;
                transform.Rotate(0, 0, -45);
            }
            if(Input.GetButton("LookRight"))
            {
                diagonal = transform.up - transform.right;
                rb.velocity = (-diagonal * speed) / 1.5f;
                transform.Rotate(0, 0, -45);
            }

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
