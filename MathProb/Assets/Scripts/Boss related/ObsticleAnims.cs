using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObsticleAnims : MonoBehaviour
{
    public Animator anim;

    private void Start()
    {
        //InvokeRepeating("RandAnimation",0 ,1);
        RandAnimation();
    }

    void RandAnimation()
    {
        int rand = 0;

        do
        {
            rand = Random.Range(0, 2);

        } while (rand > 1 && rand < 0);

        Debug.Log(rand);

        switch (rand)
        {
            case 0:
                anim.SetBool("ZigZag", true);
                break;
            case 1:
                anim.SetBool("ZigZag", false);
                break;
        }
    }
}
