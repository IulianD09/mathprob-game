using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float speed = 20f;
    public int damage = 5;
    public Rigidbody2D rb;
    public GameObject impactEffect;

	// Update is called once per frame
	void Start () {
        rb.velocity = transform.right * speed;
	}
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Boss boss = hitInfo.GetComponent<Boss>();
        if (boss != null)
        {
            boss.TakeDamage(damage);
        }

        Instantiate(impactEffect , transform.position, Quaternion.identity);
        Destroy(gameObject);

    }


}
