using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public CharacterController2D controller;
    public Animator animator;
    public GameObject particleSpawn;

    public float runSpeed = 40f;
    public float Speed = 40f;
    float horizontalMove = 0f;
    public float dashTime;
    public float waitDashTime;


    bool jump = false;
    bool crouch = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        dashTime = .3f;
        waitDashTime +=Time.deltaTime;
    }

    // Update is called once per frame
    void Update() {

        //Dashing speed 
        Dash();

        //animation for our player - the running one!
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        
        //Jumping with W
        if (Input.GetKeyDown(KeyCode.W))
        {
            jump = true;
            animator.SetTrigger("jump");
            animator.SetBool("IsJumping", true);
        } 
      
        //Crouching with S
        if (Input.GetButtonDown("Crouch"))
        {
            animator.SetBool("IsJumping", false);
            crouch = true;
            animator.SetBool("IsCrouching", true);

        } else if (Input.GetButtonUp("Crouch"))
        {
            animator.SetBool("IsCrouching", false);
            crouch = false;
        }
    }

    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
    }

    public void OnCrouching(bool isCrouching) {
        animator.SetBool("IsCrouching", isCrouching);
    }

    //Dashing
    void Dash()
    {
        //We have to set the animaton for the dash effect just to be applyed on every if! 
            animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        //If LShift and A or D are pressed...
        if (Input.GetKey(KeyCode.LeftShift) && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))
        {
            //...then the runSpeed increases!
            runSpeed = 200f;
            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

            //also spawns a little particle after a dash!
            Instantiate(particleSpawn, transform.position, Quaternion.identity);
            Destroy(particleSpawn);

        }
        //Else if the LShift and the A or D keys aren't pressed...
        else
        {
            //...the runSpeed value gets back to normal!
            runSpeed = 85f;
            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        }

   
    }

    void FixedUpdate()
    {
        //Move our char
        controller.Move(horizontalMove * Time.fixedDeltaTime , crouch , jump);
       jump = false;  

    }
}
