using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private Transform startPos;
    [SerializeField] private Transform spawnEff;

    [Space(3)]

    [Header("Min Y move range")]
    [SerializeField] private Transform min;

    [Header("Max Y move range")]
    [SerializeField] private Transform max;

    [Space(3)]

    [Header("Minimum X move range")]
    [SerializeField] private float minX;

    [Header("Maximum X move range")]
    [SerializeField] private float maxX;

    Vector3 spawnPosition;
    private Transform player;
    private Vector2 target;
    public GameObject beforeSpawnEffect;
    Vector3 direction;

    public float speed;
    public float m_Speed1;
    public float units = 2.0f;

    public void Start()
    {
        direction = Vector3.down;

        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector2(player.position.x, player.position.y);

        // The starting position, If we want to make it random we can type Random.Range(min, max) insted of startPos.position.x
        spawnPosition = new Vector2(startPos.position.x * Time.deltaTime, max.position.y);
        InvokeRepeating("ChangePosition", 1f, 2f);
        //StartCoroutine(ChangePosition());
    }

    public void ChangePosition()
    { 
        transform.position = spawnPosition;
        spawnPosition = new Vector2(player.position.x, max.position.y);

        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (spawnPosition.x == target.x || spawnPosition.y == target.y)
        {
            Debug.Log("Destroyed gameObject!");
        }
        if (transform.position.x == player.position.x || transform.position.y == player.position.y)
        {
            Debug.Log("Damage TAKEN");
        }

        /*
        if (direction == Vector3.down)
        {
            m_Speed1 = 10.0f;

            if (transform.position.y <= min.position.y)
            {
                direction = Vector3.up;
            }
        }
        transform.Translate(direction * m_Speed1 * Time.deltaTime);
        if (direction == Vector3.up) 
        {
            m_Speed1 = 10.0f;

            if (transform.position.y >= max.position.y)
            {
                direction = Vector3.down;
            }
        }
        */

        if(direction == Vector3.down)
        {
            m_Speed1 = 5f;

            direction = Vector3.up;

            StartCoroutine(WaitTime());

            transform.position = max.position;

            transform.Translate(direction * m_Speed1 * Time.deltaTime);
        }
        else if (direction == Vector3.up)
        {
            m_Speed1 = 5f;

            direction = Vector3.down;

            StartCoroutine(WaitTime());

            transform.position = min.position;
            
            transform.Translate(direction * m_Speed1 * Time.deltaTime);
        }
    }
    IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(5f);
        Instantiate(beforeSpawnEffect, spawnEff.position, Quaternion.identity);
    }
}
