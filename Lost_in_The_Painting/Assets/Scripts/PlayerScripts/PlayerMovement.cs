using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    PlayerInputs playerInputs;
    CharacterController controller;
    Animator animator;
    public Transform groundCheck;
    public Transform playerSprite;
    public LayerMask whatIsGround;

    float horizontalMovement;
    float verticalMovement;
    float jumpTimeCounter;
    float playerDefaultScaleX;

    public float playerFallSpeed = -9.81f;
    public float radius;
    public float gravityMultiplier;
    public float jumpTime;
    public float moveSpeed;
    public float jumpForce;

    public bool isGrounded;
    bool jumpButton;
    bool keepJumping;
    bool facingRight = true;

    Vector3 moveDirection;
    public Vector3 velocity;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        playerInputs = GetComponent<PlayerInputs>();
        controller = GetComponent<CharacterController>();
        playerDefaultScaleX = playerSprite.localScale.x;
    }

    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, radius, whatIsGround);
        moveDirection = playerInputs.moveDirection;
        HandleInputBools();        
        HandleAll();
    }

    void HandleAll()
    {
        Movement();
        FlipPlayer();
    }

    //all player movement is here: Jumping, falling, and walking etc.
    void Movement()
    {
        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
            jumpTimeCounter = jumpTime;
            gravityMultiplier = 2;
        }

        if (!isGrounded)
        {
            velocity.y += playerFallSpeed * Time.deltaTime;
        }

        PlayerJump();

        controller.Move(moveDirection * moveSpeed * Time.deltaTime);
        controller.Move(velocity * gravityMultiplier * Time.deltaTime);
    }

    void PlayerJump()
    {
        if (isGrounded && jumpButton)
        {
            animator.SetTrigger("jump");
            velocity.y = Mathf.Sqrt(jumpForce * -2f * playerFallSpeed);
            gravityMultiplier = 4;
            print("JUMPING!");
        }

        if(keepJumping && jumpTimeCounter > 0)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * playerFallSpeed);
            jumpTimeCounter -= Time.deltaTime;
            print("Keep Jumping!");
        }
        else
        {
            jumpTimeCounter = 0;
        }
    }
    
    //Flips players sprite gameobjects X scale
    void FlipPlayer()
    {
        //takes the current moving direction the player is going
        float playerDirection = moveDirection.x;

        if (playerDirection == 1 && facingRight == false)
        {
            playerSprite.localScale = new Vector3(playerDefaultScaleX, playerSprite.localScale.y, playerSprite.localScale.z);
            facingRight = true;
        }
        else if (playerDirection == -1 && facingRight == true)
        {
            playerSprite.localScale = new Vector3(-playerDefaultScaleX, playerSprite.localScale.y, playerSprite.localScale.z);
            facingRight = false;
        }
    }


    //Handles InputBools, that are not affected by this script
    void HandleInputBools()
    {
        jumpButton = playerInputs.jumpButton;  
        keepJumping = playerInputs.keepJumping;
    }
}
