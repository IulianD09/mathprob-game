using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableColl : MonoBehaviour {

    public Collider2D[] coll;
    
    public bool evenRows = false;
    public bool oddRows = false;
    public bool succesively = false;

    [HideInInspector]
    public bool succesivelyV1 = false;
    [HideInInspector]
    public bool succesivelyV2 = false;
    [HideInInspector]
    public bool FirstAndLastV1 = false;
    [HideInInspector]
    public bool FirstAndLastV2 = false;

    public bool endAttack = true;
    public bool firstAndLast = false;

    public float wait;
    public float evnsTime;
    public float oddsTime;
    public float FandLTime;

    private void Start()
    {
        InvokeRepeating("Series", 2f, 5f);
        InvokeRepeating("FromFirstToLast", 1f, 5f);
    }

    private void LateUpdate()
    {
        //Disableing the colliders
        if (endAttack)
        {
            EndAttack(); 
        }
       
        //Enableing the even colliders
        if (evenRows)
        {
            StartCoroutine(EvenRows());   
        }

        //Enableing the odd colliders
        if (oddRows)
        {
            StartCoroutine(OddRows());   
        }
    }

    public void Series()
    {
        if (succesively)
        {
            StartCoroutine(Succesively());
        }
    }
    public void FromFirstToLast()
    {
        if (firstAndLast)
        {
            StartCoroutine(FirstAndLast());
        }
    }
    public IEnumerator Succesively()
    {
        endAttack = false;
        evenRows = false;
        oddRows = false;
        firstAndLast = false;

        int rand = Random.Range(0, 2);

        if (rand == 0) 
        {
            succesivelyV1 = true;

            if (succesivelyV1)
            {
                coll[0].enabled = true;

                yield return new WaitForSeconds(wait);
                coll[0].enabled = false;
                coll[1].enabled = true;

                yield return new WaitForSeconds(wait);
                coll[1].enabled = false;
                coll[2].enabled = true;

                yield return new WaitForSeconds(wait);
                coll[2].enabled = false;
                coll[3].enabled = true;

                yield return new WaitForSeconds(wait);
                coll[3].enabled = false;
            }
        }
        if (rand == 1) 
        {
            succesivelyV2 = true;

            if (succesivelyV2) 
            {
                coll[8].enabled = true;

                yield return new WaitForSeconds(wait);
                coll[8].enabled = false;
                coll[7].enabled = true;

                yield return new WaitForSeconds(wait);
                coll[7].enabled = false;
                coll[6].enabled = true;

                yield return new WaitForSeconds(wait);
                coll[6].enabled = false;
                coll[5].enabled = true;

                yield return new WaitForSeconds(wait);
                coll[5].enabled = false;
                coll[4].enabled = true;

                yield return new WaitForSeconds(wait);
                coll[4].enabled = false;
            }
        }   

        endAttack = true;
        succesively = false;
        succesivelyV1 = false;
        succesivelyV2 = false;
    }
    public IEnumerator EvenRows()
    {
        endAttack = false;
        oddRows = false;
        succesively = false;

        coll[0].enabled = true;
        coll[2].enabled = true;
        coll[4].enabled = true;
        coll[6].enabled = true;
        coll[8].enabled = true;

        yield return new WaitForSeconds(evnsTime);

        endAttack = true;
        evenRows = false;
    }
    public IEnumerator OddRows()
    {
        endAttack = false;
        evenRows = false;
        succesively = false;

        coll[1].enabled = true;
        coll[3].enabled = true;
        coll[5].enabled = true;
        coll[7].enabled = true;

        yield return new WaitForSeconds(oddsTime);

        oddRows = false;
        endAttack = true;
    }
    void EndAttack()
    {
        coll[0].enabled = false;
        coll[1].enabled = false;
        coll[2].enabled = false;
        coll[3].enabled = false;
        coll[4].enabled = false;
        coll[5].enabled = false;
        coll[6].enabled = false;
        coll[7].enabled = false;
        coll[8].enabled = false;
    }
    public IEnumerator FirstAndLast()
    {
        endAttack = false;
        evenRows = false;
        oddRows = false;
        succesively = false;

        int rand = Random.Range(0, 2);

        if (rand == 0) 
        {

        }
        if (rand == 1) 
        {
            
           
        }
        yield return null;
        endAttack = true;
        firstAndLast = false;
        FirstAndLastV1 = false;
        FirstAndLastV2 = false;
    }
}