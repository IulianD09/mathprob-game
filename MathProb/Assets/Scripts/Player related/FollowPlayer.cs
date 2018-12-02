using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {
       
    private Transform player;
    private Vector2 target;

    public float speed = 20f;
    
    private void Start()
    {
       player = GameObject.Find("Player").transform;
       target = new Vector3(player.position.x, transform.position.y);

       InvokeRepeating("ChangePos", 5f, 2f);
    } 

    void ChangePos()
    {
        transform.position = new Vector3(target.x, transform.position.y, 0);

        if (transform.position.x == player.position.x && transform.position.y == player.position.y)
        {
            Debug.Log("Reached player");
        }
    }
}
