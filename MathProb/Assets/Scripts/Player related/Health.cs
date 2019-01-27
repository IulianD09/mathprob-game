using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{

    public int health;
    public int numOfHearts;
    [SerializeField]
    public bool immortal = false;
    public bool noDmg = false;

    public float speed;
    public float minX;
    public float maxX;

    [SerializeField]
    private float immortalTime;
    public float minY;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public SpriteRenderer spriteRenderer;

    public Transform player;
    public Collider2D colliderToDisable;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
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
            colliderToDisable.enabled = false;
        }

        if (transform.position.y <= minY)
            StartCoroutine(TakePlayerDamage());
        
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
        }
    }
    public IEnumerator TakePlayerDamage()
    {
        //CameraShake shake = other.GetComponent<CameraShake>();
        if (!immortal || !noDmg)
        {
            health --;
            //numOfHearts--;

            immortal = true;
            noDmg = true;

            if(health > 0)
            {
                StartCoroutine(IndicateImmortal());
            }

            yield return new WaitForSeconds(immortalTime);

            immortal = false;
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
