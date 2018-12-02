using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAimBullet : MonoBehaviour {

    public float speed = 20f;
    public int damage = 3;
    public float lifetime;

    public Rigidbody2D rb;
    public GameObject destroyEffect;
    public Collider2D m_Collider;

    private Transform boss;
    private Vector2 target;


    private void Start()
    {
        boss = GameObject.FindGameObjectWithTag("Boss").transform;
        target = new Vector2(boss.position.x, boss.position.y);

        m_Collider = GetComponent<Collider2D>();

        rb.velocity = transform.right * speed;
        if (Input.GetButton("Shoot") && Input.GetButton("LookUp"))
        {
            rb.velocity = transform.up * speed;

            //transform.Rotate(0, 0, 90);
        }
        Invoke("DestroyBullet", lifetime);
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target , speed * Time.deltaTime);
        if (transform.position.x == target.x && transform.position.y == target.y)
        {
            DestroyBullet();
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Boss"))
        {
            DestroyBullet();
            Debug.Log("Hitted!");
        }

        Boss m_boss = collision.GetComponent<Boss>();
        if (m_boss != null)
        {
            m_boss.TakeDamage(damage);
            Destroy(gameObject);
        }

        if (collision.CompareTag("Player"))
        {
            m_Collider.enabled = false;
        }
        else if(!collision.CompareTag("Player"))
            m_Collider.enabled = true;
    }
    void DestroyBullet()
    {
        Instantiate(destroyEffect, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
