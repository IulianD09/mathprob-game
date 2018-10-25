using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{

    public int health = 1000;
    public int takenDmg = 1;
    public int stageTwoHp;

    public GameObject[] explosionEff;
    public Animator anim;

    [Space(3)]
    public float timeBtwSpawn = 10;
    public float theCountdown = 10;

    //public GameObject deathEffect;

    [Header("X spawn range")]
    public float minX;
    public float maxX;

    [Header("Y spawn range")]
    public float minY;
    public float maxY;


    private void Start()
    {
        anim = GetComponent<Animator>();
    }
   
    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= stageTwoHp)
        {
            anim.SetTrigger("stageTwo");
        }

        if (health <= 0)
        {
            anim.SetTrigger("death");
            StartCoroutine(Die());
        }
    }
    IEnumerator Die()
    {
        if (theCountdown <= 0)
        {
            theCountdown = timeBtwSpawn;
        }
        else
        {
            theCountdown -= Time.deltaTime;
        }

        while (health <= 0)
        {
            Vector2 pos = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));

            GameObject explEff = explosionEff[Random.Range(0, explosionEff.Length)];

            yield return new WaitForSeconds(1f);
            Instantiate(explEff, pos, Quaternion.identity);

            Destroy(gameObject, 3f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //deal the player damage
        if (other.CompareTag("Player"))
        {
            Health hp = other.GetComponent<Health>();
            CameraShake shake = other.GetComponent<CameraShake>();

            hp.health -= takenDmg;
            hp.numOfHearts -= takenDmg;
          
             StartCoroutine(shake.CamShake());
        }
    }
}
