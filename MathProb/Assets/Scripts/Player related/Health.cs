﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{

    public int health;
    public int numOfHearts;
    public bool immortal = false;
    public bool noDmg = false;

    [SerializeField]
    private float immortalTime;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public SpriteRenderer spriteRenderer;

    public GameObject player;

    private Vector3 startPos, prevPos;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        startPos.y = transform.position.y;
    }
    void Update()
    {
        prevPos.y = transform.position.y;

        if (health > numOfHearts)
        {
            health = numOfHearts;
        }
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }

            if (i < numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
            if (i == health && i == numOfHearts)
            {
                hearts[i].enabled = false;
            }
        }
        if (health == 0)
        {
            Dead();
        }

        if (transform.position.y <= -18.5)
        {
            StartCoroutine(TakePlayerDamage());

            if (immortal)
                noDmg = true;

            if (health > 0)
                player.transform.position = startPos;
            else if(health <= 0)
                player.transform.position = prevPos;

        }
    }
    public void Dead()
    {
        //Instantiate particles for the dead effect or add an animation
        Destroy(gameObject, 2f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Boss"))
        {
            StartCoroutine(TakePlayerDamage());
            noDmg = true;
        }
    }
    public IEnumerator TakePlayerDamage()
    {
        //CameraShake shake = other.GetComponent<CameraShake>();
        if (!immortal || !noDmg)
        {
            health -= 1;
            //numOfHearts--;

            immortal = true;
            noDmg = true;

            if(health > 0)
            {
                StartCoroutine(IndicateImmortal());
            }

            yield return new WaitForSeconds(immortalTime);

            if (immortal)
                immortal = false;
            if (noDmg)
                noDmg = false; 
        }
    }
    private IEnumerator IndicateImmortal()
    {
        while (immortal) 
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(.1f);

            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(.1f);
        }
    }
}
