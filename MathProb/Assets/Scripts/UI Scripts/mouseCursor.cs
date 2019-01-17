using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseCursor : MonoBehaviour
{
    public float timeBtwSpawn = 0.1f;
    //public GameObject trailEff;

    public Animator anim;

    private void Start()
    {
        Cursor.visible = false;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = cursorPos;
        
        if(Input.GetMouseButtonDown(0))
        {
            anim.SetBool("click",true);

        }else if(Input.GetMouseButtonUp(0))
        {
            anim.SetBool("click", false);
        }
        /*
        if(timeBtwSpawn <=0)
        {
            Instantiate(trailEff,cursorPos,Quaternion.identity);
            timeBtwSpawn = 0.1f;
        } else
            timeBtwSpawn -= Time.deltaTime;
        */
    }
}
