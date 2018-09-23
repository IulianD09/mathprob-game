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
        // Animation for the dash move
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        // the actual movement
        runSpeed = 85f;
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        //setting up the dash speed value
        dashSpeed = 30f;
        //checking the direction if it is 0 it means we aren't dashing
        if (direction == 0)
        {
            if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.LeftArrow)){
                direction = 1;
                Instantiate(particleSpawn,transform.position,Quaternion.identity);
            }
            else if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.RightArrow)){
                direction = 2;
                Instantiate(particleSpawn, transform.position, Quaternion.identity);
            }
            // else we are dashing
        }else {
            if (dashTime <= 0){
                direction = 0;
                dashTime = startDashTime;
                controller.m_Rigidbody2D.velocity = Vector2.zero;
            } else{
                dashTime -= Time.deltaTime;

                if (direction == 1)
                {
                    controller.m_Rigidbody2D.velocity = Vector2.left * dashSpeed;
                }
                else if (direction == 2)
                {
                    controller.m_Rigidbody2D.velocity = Vector2.right * dashSpeed;           
                }
            }
        }
        // Dashing in left or right depends on which direction we are faceing even wihout have to press keys to move
        if (controller.m_FacingRight == true && Input.GetKeyDown(KeyCode.LeftShift))
        {
            controller.m_Rigidbody2D.velocity = Vector2.right * dashSpeed*2;
            Instantiate(particleSpawn, transform.position, Quaternion.identity);
        }
        else if (controller.m_FacingRight == false && Input.GetKeyDown(KeyCode.LeftShift))
        {
            controller.m_Rigidbody2D.velocity = Vector2.left * dashSpeed*2;
            Instantiate(particleSpawn, transform.position, Quaternion.identity);
        }

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

    

    //Dashing
    void Dash()
    {
 
         













    }
  void FixedUpdate()
    {
        //Move our char
        controller.Move(horizontalMove * Time.fixedDeltaTime , crouch , jump);
       jump = false;  

    }
}
