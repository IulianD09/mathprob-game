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
    
    public float dashSpeed;
    private float dashTime;
    public float startDashTime;
    private int direction;
    public float setDashValue;

    bool jump = false;
    bool crouch = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        dashTime = startDashTime;
    }

    // Update is called once per frame
    public void Update()
    {
        //The dash function
        Dash();

        //animation for our player - the running one!
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        //Jumping with W
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            jump = true;
            animator.SetTrigger("jump");
            animator.SetBool("IsJumping", true);
        }

        //Crouching with DownArrow
        if (Input.GetButtonDown("Crouch"))
        {
            animator.SetBool("IsJumping", false);
            crouch = true;
            animator.SetBool("IsCrouching", true);
        }
        //else if DownArrow isn't pressed we arent gonna play the rouch animation
        else if (Input.GetButtonUp("Crouch"))
        {
            animator.SetBool("IsCrouching", false);
            crouch = false;
        }
    }
   public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
    }

    public void OnCrouching(bool isCrouching)
    {
        animator.SetBool("IsCrouching", isCrouching);
    }

    public void Dash()
    {
        
        // Animation for the dash move
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        // the actual movement
        runSpeed = 85f;
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        dashSpeed = setDashValue;

        //checking the direction if it is 0 it means we aren't dashing
        if (direction == 0)
        {
            //If the LShift and LeftArrow are pressed the direction sets to 1 and we spawn some particles
            if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.LeftArrow))
            {
                direction = 1;
                Instantiate(particleSpawn, transform.position, Quaternion.identity);
            }
            //Else if the LShift and RightArrow are pressed the direction sets to 2 and we spawn some particles
            else if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.RightArrow))
            {
                direction = 2;
                Instantiate(particleSpawn, transform.position, Quaternion.identity);
            }
            //If we are pressing only LShift and we are facing right, then the direction sets to 3 and spawning particles
            if (controller.m_FacingRight == true && Input.GetKeyDown(KeyCode.LeftShift))
            {
                direction = 3;
                Instantiate(particleSpawn, transform.position, Quaternion.identity);
            }
            //else if we are facing left and LShift is pressed, then the direction sets to 4 and spanign particles as well
            else if (controller.m_FacingRight == false && Input.GetKeyDown(KeyCode.LeftShift)) 
            {
                direction = 4;
                Instantiate(particleSpawn, transform.position, Quaternion.identity);
            }
            // else if its 1, 2, 3 or 4 we are dashing
        }
        else
        {
            //If we ran out of juice the direction sets to 0 because we aren't dashing
            if (dashTime <= 0)
            {
                direction = 0;
                dashTime = startDashTime;
                controller.m_Rigidbody2D.velocity = Vector2.zero;
            }
            //else we can start dashing
            else
            {
                dashTime -= Time.deltaTime;
                
                //if we move to the left with LShift and LeftArrow presssed..
                if (direction == 1)
                {
                    controller.m_Rigidbody2D.velocity = Vector2.left * dashSpeed;
                }
                //else we will dash into the opposite direction
                else if (direction == 2)
                {
                    controller.m_Rigidbody2D.velocity = Vector2.right * dashSpeed;
                }
                //but if we dash with only LShift and we are facing right we will dash right without having to press arrows
                if (direction == 3)
                {
                    controller.m_Rigidbody2D.velocity = Vector2.right * dashSpeed;
                }
                //Same thing here but with the opposite direction
                else if (direction == 4)
                {
                    controller.m_Rigidbody2D.velocity = Vector2.left * dashSpeed;
                }
                if (crouch == true && Input.GetButtonDown("Crouch"))
                {
                    dashSpeed = 0f;
                    direction = 0;
                    setDashValue = 0f;
                    controller.m_Rigidbody2D.velocity = Vector2.zero;
                }
                if (jump)
                {
                    controller.m_Rigidbody2D.gravityScale = 0f;
                }
            }
        }      
    }

  void FixedUpdate()
    {
        //Move our char
        controller.Move(horizontalMove * Time.fixedDeltaTime , crouch , jump);
       jump = false;  

    }
}
