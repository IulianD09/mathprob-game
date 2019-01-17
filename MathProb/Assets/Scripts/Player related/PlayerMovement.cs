using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public CharacterController2D controller;
    public Animator animator;
    public GameObject particleSpawn;
    public Health health;

    [Space]
    public float runSpeed = 100f;
    float horizontalMove = 0f;
    float playerBoudaryRadius = 0.5f;

    [Space]
    private float dashSpeed;
    private float dashTime;
    public float startDashTime;
    private int direction;
    public float setDashValue;

    [Space]
    bool jump = false;
    bool crouch = false;
    bool dash = false;

    [Space(2)]
    public bool isMoving = true;
    public bool isDashing = true;
    public bool activNoDmg = false;

    void Start()
    {
        health = GetComponent<Health>();
        animator = GetComponent<Animator>();
        dashTime = startDashTime;
    }

    // Update is called once per frame
    public void Update()
    {
        Vector3 pos = transform.position;

        if (Input.GetKey(KeyCode.C))
        {
            isMoving = false;
            if ((Input.GetButton("LookLeft") || Input.GetButton("LookRight")) && Input.GetButton("LookUp"))
            {
                animator.SetBool("DiagLook", true);
            }
            else
                animator.SetBool("DiagLook", false);
        }
        else
            isMoving = true;

        if (isMoving)
        {
            runSpeed = 150f;
        }else
            runSpeed = 0.001f;

        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        //Restrict the player to the camera's boumdaries
        //This is just for the Y
        if (pos.y + playerBoudaryRadius > Camera.main.orthographicSize)
        {
            pos.y = Camera.main.orthographicSize - playerBoudaryRadius;
        }
        if (pos.y - playerBoudaryRadius < -Camera.main.orthographicSize)
        {
            pos.y = -Camera.main.orthographicSize + playerBoudaryRadius;
        }

        //This is just for the X
        float screenRatio = (float)Screen.width / (float)Screen.height * 1.2f; //WARNING will be weird!
        float widthOrtho = Camera.main.orthographicSize * screenRatio;

        if (pos.x + playerBoudaryRadius > widthOrtho)
        {
            pos.x = widthOrtho - playerBoudaryRadius;
        }
        if (pos.x - playerBoudaryRadius < -widthOrtho)
        {
            pos.x = -widthOrtho + playerBoudaryRadius;
        }

        transform.position = pos;
        
        //The dash function
        if (isDashing)
        {
            Dash();
        }

        //animation for our player - the running one!
        //By typing "Mathf.Abs" we make the speed positive, because for the animation we cant use negative speed.I will'n play the animation
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        //Jumping with Space
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            animator.SetBool("IsJumping", true);
            animator.SetBool("IsCrouching", false);
        }

        //Crouching with S
        if (Input.GetButtonDown("Crouch"))
        {
            if (jump)
            {
                animator.SetBool("IsJumping", false);
                crouch = false;
                animator.SetBool("IsCrouching", false);
            }
            if (horizontalMove >= 0.01)
            {
                animator.SetBool("IsCrouching", false);
                crouch = false;
                if (Input.GetButtonDown("LookUp"))
                {
                    animator.SetBool("IsCrouching", false);
                }
            }
            else
            {
                animator.SetBool("IsCrouching", true);
                crouch = true;
            }
        }
        //else if DownArrow isn't pressed we arent gonna play the crouch animation
        else if (Input.GetButtonUp("Crouch"))
        {
            animator.SetBool("IsCrouching", false);
            crouch = false;
        }

/*
        if (Input.GetButtonDown("LookUp") && Input.GetButtonDown("Crouch"))
        {
            //Look up animation set to true
            animator.SetBool("LookUpD", true);
        }
        else if (Input.GetButtonUp("Crouch") && Input.GetButtonDown("LookUp"))
        {
            animator.SetBool("LookUpD", false);
            animator.SetBool("LookUpI", true);

        }
*/
        if (Input.GetButtonDown("LookUp"))
        {
            //When we are idleing and we start looking up the animation starts playing
            animator.SetBool("LookUpI", true);
        }
        else if (Input.GetButtonUp("LookUp"))
        {
            animator.SetBool("LookUpI",false);
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

    public void OnDashing(bool isDashing)
    {
        animator.SetBool("IsDashing", isDashing);
    }

    public void Dash()
    {
        /*
        if (dash)
        {
            animator.SetBool("IsDashing", true);
            runSpeed = 150f;
        }
        else
        {
            // Animation for the dash move
            animator.SetBool("IsDashing", false);
            dash = false;
          
        }
        */

        dashSpeed = setDashValue;

        //checking the direction if it is 0 it means we aren't dashing
        if (direction == 0)
        {
            health.noDmg = false;
            dash = false;
            animator.SetBool("IsDashing", false);
            //If the LShift and LeftArrow are pressed the direction sets to 1 and we spawn some particles
            if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.LeftArrow))
            {
                direction = 1;
                Instantiate(particleSpawn, transform.position, Quaternion.identity);
                dash = true;
            }
            //Else if the LShift and RightArrow are pressed the direction sets to 2 and we spawn some particles
            else if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.RightArrow))
            {
                direction = 2;
                Instantiate(particleSpawn, transform.position, Quaternion.identity);
                dash = true;
            }
            //If we are pressing only LShift and we are facing right, then the direction sets to 3 and start spawning particles
            if (controller.m_FacingRight == true && Input.GetKeyDown(KeyCode.LeftShift))
            {
                direction = 3;
                Instantiate(particleSpawn, transform.position, Quaternion.identity);
                dash = true;
            }

            //else if we are facing left and LShift is pressed, then the direction sets to 4 and spawns particles as well
            else if (controller.m_FacingRight == false && Input.GetKeyDown(KeyCode.LeftShift))
            {
                direction = 4;
                Instantiate(particleSpawn, transform.position, Quaternion.identity);
                dash = true;
            }
            if (controller.m_FacingRight == true && dash==true  && Input.GetKeyDown(KeyCode.LeftArrow))
            {
                direction = 5;
                controller.m_FacingRight = true;
                dash = true;
            }else if (controller.m_FacingRight == false && dash == true && Input.GetKeyDown(KeyCode.RightArrow))
            {
                direction = 6;
                controller.m_FacingRight = false;
                dash = true;
            }
        }
        else
        {
            //If we ran out of juice the direction sets to 0 because we aren't dashing
            if (dashTime <= 0)
            {
                direction = 0;
                controller.m_Rigidbody2D.velocity = Vector2.zero;
                controller.m_Rigidbody2D.gravityScale = 12f;
                //resseting the spawn dash time
                dashTime = startDashTime;
            }
            //else if the time btw dash is not 0
            else
            {
                //we slowly decrease the dash time
                dashTime -= Time.deltaTime;

                //if we move to the left with LShift and LeftArrow presssed..
                if (direction == 1) 
                {
                    controller.m_Rigidbody2D.velocity = Vector2.left * dashSpeed;
                    controller.m_Rigidbody2D.gravityScale = 0f;
                    animator.SetBool("IsDashing", true);

                }
                //else we will dash into the opposite direction
                else if (direction == 2)
                {
                    controller.m_Rigidbody2D.velocity = Vector2.right * dashSpeed;
                    controller.m_Rigidbody2D.gravityScale = 0f;
                    animator.SetBool("IsDashing", true);
                }
                //but if we dash with only LShift and we are facing right we will dash right without having to press arrows
                if (direction == 3)
                {
                    controller.m_Rigidbody2D.velocity = Vector2.right * dashSpeed;
                    controller.m_Rigidbody2D.gravityScale = 0f;
                    animator.SetBool("IsDashing", true);
                }
                //Same thing here but with the opposite direction
                else if (direction == 4)
                {
                    controller.m_Rigidbody2D.velocity = Vector2.left * dashSpeed;
                    controller.m_Rigidbody2D.gravityScale = 0f;
                    animator.SetBool("IsDashing", true);
                }
                if (direction == 5)
                {
                    animator.SetBool("IsDashing", true);
                }
                else if (direction == 6)
                {
                    animator.SetBool("IsDashing", true);
                }

                if (activNoDmg)
                {
                    if (direction == 1 || direction == 2 || direction == 3 || direction == 4 || direction == 5 || direction == 6) 
                    {
                        health.noDmg = true;
                    }else
                        health.noDmg = false;
                }
            }
        }      
    }

    public void Ultimate()
    {
     //Do the ultimate animation

     //Cleans the screen from any attacks and deals some damage to the boss

     //Resets the timer when the ultimate pesses and sets the new timer
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Boss"))
        {
            animator.SetBool("IsDashing", false);
        }
    }

    void FixedUpdate()
    {
       //Move our char
       controller.Move(horizontalMove * Time.fixedDeltaTime , crouch , jump, dash);
        jump = false;
        dash = false;
    }
}
