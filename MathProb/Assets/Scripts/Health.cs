using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{

    public int health;
    public int numOfHearts;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
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
            if (i == health)
            {
                hearts[i].enabled = false;
            }
            if (i == numOfHearts)
            {
                hearts[i].enabled = false;
            }

        }
        if (health <= 0 && numOfHearts <= 0)
        {
            Dead();
        }

    }
    public void Dead()
    {
        //Instantiate particles for the dead effect or andd an animation
        Debug.Log("dead");
    }
    
}
