using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public int health = 1000;
    public int takenDmg = 1;
    public int stageTwoHp;

    //private float timeBtwShots;
    //public float startTimeBtwShots;
    [Space]
    public GameObject[] explosionEff;
    [Space]
    public Transform[] beforeSpawnSpots;
    [Space]
    public Transform[] lighningSpawnSpots;
    [Space]
    public Animator anim;
    //public Transform[] firePoints;
    //public GameObject projectile;
    public GameObject beforeSpawnEffect;
    private GameObject boss;
    public Collider2D col2D;
    public Attack attack;
    public DisableColl disableColl;
    public GameObject beforeSpawnPrefab;
    public GameObject lightningPrefab;

    [Space(3)]
    private float timeBtwSpawn = 10;
    public float theCountdown = 10;
    public float waitSecs;
    public float waitSecsStTwo;
    public float wait;

    //public GameObject deathEffect;

    [Header("X spawn range")]
    public float minX;
    public float maxX;

    [Header("Y spawn range")]
    public float minY;
    public float maxY;

    //private int rand;
    //private int randStTwo;

    private void Start()
    {
        boss = GameObject.Find("Boss");
        anim = boss.GetComponent<Animator>();

        theCountdown = timeBtwSpawn;
        //timeBtwShots = startTimeBtwShots;

        InvokeRepeating("BossBattle", 0f, 5f);
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
            while (health <= 0)
            {
                Vector2 pos = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));

                GameObject explEff = explosionEff[Random.Range(0, explosionEff.Length)];

                yield return new WaitForSeconds(1f);
                Instantiate(explEff, pos, Quaternion.identity);

                Destroy(gameObject, 3f);
            }
            theCountdown -= Time.deltaTime;
        }
    }

    public void BossBattle()
    {
        // Checks if Stage 1 attack is playing
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("SQRTBoss_attack_s1")) 
        {
            // Debug.Log("Attack stage 1 is playing");
            StartCoroutine(Battle());
        }
        // Cheks if the Stage 2 attack is playing
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("SQRTBoss_attack_s2")) 
        {
            //  Debug.Log("Attack stage 2 is playing");
            StartCoroutine(StageTwo());
        }

        // Checks if the other animations are playing
        // if (anim.GetCurrentAnimatorStateInfo(0).IsName("SQRTBoss_idle_s2") ||anim.GetCurrentAnimatorStateInfo(0).IsName("SQRTBoss_idle") ||anim.GetCurrentAnimatorStateInfo(0).IsName("SQRTBoss_dead"))
        
    }
    IEnumerator Battle()
    {
       int rand = Random.Range(0, 2);

        switch (rand)
        {
            case 0:
                Debug.Log("Even Rows");

                Instantiate(beforeSpawnPrefab, beforeSpawnSpots[0].transform.position, Quaternion.identity);
                Instantiate(beforeSpawnPrefab, beforeSpawnSpots[2].transform.position, Quaternion.identity);
                Instantiate(beforeSpawnPrefab, beforeSpawnSpots[4].transform.position, Quaternion.identity);
                Instantiate(beforeSpawnPrefab, beforeSpawnSpots[6].transform.position, Quaternion.identity);
                Instantiate(beforeSpawnPrefab, beforeSpawnSpots[8].transform.position, Quaternion.identity);

                yield return new WaitForSeconds(1f);

                Instantiate(lightningPrefab, lighningSpawnSpots[0].transform.position, Quaternion.identity);
                Instantiate(lightningPrefab, lighningSpawnSpots[2].transform.position, Quaternion.identity);
                Instantiate(lightningPrefab, lighningSpawnSpots[4].transform.position, Quaternion.identity);
                Instantiate(lightningPrefab, lighningSpawnSpots[6].transform.position, Quaternion.identity);
                Instantiate(lightningPrefab, lighningSpawnSpots[8].transform.position, Quaternion.identity);

                disableColl.evenRows = true;

                break;
            case 1:
                Debug.Log("Odd Rows");

                Instantiate(beforeSpawnPrefab, beforeSpawnSpots[1].transform.position, Quaternion.identity);
                Instantiate(beforeSpawnPrefab, beforeSpawnSpots[3].transform.position, Quaternion.identity);
                Instantiate(beforeSpawnPrefab, beforeSpawnSpots[5].transform.position, Quaternion.identity);
                Instantiate(beforeSpawnPrefab, beforeSpawnSpots[7].transform.position, Quaternion.identity);

                yield return new WaitForSeconds(1f);

                Instantiate(lightningPrefab, lighningSpawnSpots[1].transform.position, Quaternion.identity);
                Instantiate(lightningPrefab, lighningSpawnSpots[3].transform.position, Quaternion.identity);
                Instantiate(lightningPrefab, lighningSpawnSpots[5].transform.position, Quaternion.identity);
                Instantiate(lightningPrefab, lighningSpawnSpots[7].transform.position, Quaternion.identity);

                disableColl.oddRows = true;

                break;
        }
    }
    IEnumerator StageTwo()
    {
        int randStTwo = Random.Range(0, 2);

        switch (randStTwo)
        {
            case 0:
                Debug.Log("Succesively");

                disableColl.succesively = true;

                if (disableColl.succesively)
                {
                    if(disableColl.succesivelyV1)
                    {
                        Instantiate(beforeSpawnPrefab, beforeSpawnSpots[0].transform.position, Quaternion.identity);
                        yield return new WaitForSeconds(wait);
                        Instantiate(beforeSpawnPrefab, beforeSpawnSpots[1].transform.position, Quaternion.identity);
                        yield return new WaitForSeconds(wait);
                        Instantiate(beforeSpawnPrefab, beforeSpawnSpots[2].transform.position, Quaternion.identity);
                        yield return new WaitForSeconds(wait);
                        Instantiate(beforeSpawnPrefab, beforeSpawnSpots[3].transform.position, Quaternion.identity);
                    }
                    if (disableColl.succesivelyV2)
                    {
                        Instantiate(beforeSpawnPrefab, beforeSpawnSpots[8].transform.position, Quaternion.identity);
                        yield return new WaitForSeconds(wait);
                        Instantiate(beforeSpawnPrefab, beforeSpawnSpots[7].transform.position, Quaternion.identity);
                        yield return new WaitForSeconds(wait);
                        Instantiate(beforeSpawnPrefab, beforeSpawnSpots[6].transform.position, Quaternion.identity);
                        yield return new WaitForSeconds(wait);
                        Instantiate(beforeSpawnPrefab, beforeSpawnSpots[5].transform.position, Quaternion.identity);
                        yield return new WaitForSeconds(wait);
                        Instantiate(beforeSpawnPrefab, beforeSpawnSpots[4].transform.position, Quaternion.identity);
                    }
                }

                yield return new WaitForSeconds(2f);

                disableColl.succesively = true;

                if (disableColl.succesively)
                {
                    if (disableColl.succesivelyV1)
                    {
                        Instantiate(lightningPrefab, lighningSpawnSpots[0].transform.position, Quaternion.identity);
                        yield return new WaitForSeconds(wait);
                        Instantiate(lightningPrefab, lighningSpawnSpots[1].transform.position, Quaternion.identity);
                        yield return new WaitForSeconds(wait);
                        Instantiate(lightningPrefab, lighningSpawnSpots[2].transform.position, Quaternion.identity);
                        yield return new WaitForSeconds(wait);
                        Instantiate(lightningPrefab, lighningSpawnSpots[3].transform.position, Quaternion.identity);
                    }
                    if (disableColl.succesivelyV2)
                    {
                        Instantiate(lightningPrefab, lighningSpawnSpots[8].transform.position, Quaternion.identity);
                        yield return new WaitForSeconds(wait);
                        Instantiate(lightningPrefab, lighningSpawnSpots[7].transform.position, Quaternion.identity);
                        yield return new WaitForSeconds(wait);
                        Instantiate(lightningPrefab, lighningSpawnSpots[6].transform.position, Quaternion.identity);
                        yield return new WaitForSeconds(wait);
                        Instantiate(lightningPrefab, lighningSpawnSpots[5].transform.position, Quaternion.identity);
                        yield return new WaitForSeconds(wait);
                        Instantiate(lightningPrefab, lighningSpawnSpots[4].transform.position, Quaternion.identity);
                    }
                }

                break;
            case 1:
                Debug.Log("First And Last");

                yield return new WaitForSeconds(2f);

                disableColl.firstAndLast = true;
                break;
        }
    }
}
