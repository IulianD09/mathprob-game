using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour {

    public int health = 1000;
    public GameObject[] explosionEff;

    [Space(3)]
    public float waitingForNextSpawn = 10;
    public float waitForDeath;
    public float theCountdown = 10;
    public float counter;

    //public GameObject deathEffect;
    [Header("X spawn range")]
    public float minX;
    public float maxX;

    [Header("Y spawn range")]
    public float minY;
    public float maxY;
    
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
        Die();
        }
    }

    private void Update()
    {
        theCountdown -= Time.deltaTime;
        if (theCountdown <= 0)
        {

            theCountdown = waitingForNextSpawn;
        }
    }
    void Die()
    {
        
        Vector2 pos = new Vector2(Random.Range(minX,maxY), Random.Range(minY,maxY));
        GameObject explEff =explosionEff[Random.Range(0,explosionEff.Length)];

        Instantiate(explEff,pos,Quaternion.identity);

        counter -= Time.deltaTime;
        if (counter <= 0)
        {
            counter = waitForDeath;
        }
        Destroy(gameObject);
    }
}
