using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSprites : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite[] Nums;

    public Vector3 NumPos;

    private void Start()
    {
        StartCoroutine(ChangingSprite());
    }

    private IEnumerator ChangingSprite()
    {
        int rand = 0;

        while (rand <= Nums.Length + 1)
        {
            rand = Random.Range(0, Nums.Length);

            yield return new WaitForSeconds(2.7f);

            if (transform.position.x >= NumPos.x)
            {
                spriteRenderer.sprite = Nums[rand];

                if (rand == Nums.Length + 1)
                    rand--;
            }
            rand++;

            //Debug.Log("Rand " + rand);
        }
    }
}
