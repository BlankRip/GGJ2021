using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float speed = 5f;
    [SerializeField] float rotationSpeed = 10;
    [SerializeField] float lerpSpeed = 10;

    [Header("Jump")]
    public float jumpeForce = 5f;
    public float secondJumpeForce = 5f;
    public float dashForce = 5f;
    [SerializeField] float dJumpActivateGap = 1f;
    [Range(0, 0.5f)] [SerializeField] float groundCheckRadius = 0.2f;
    [SerializeField] LayerMask groundLayers;
    [SerializeField] Transform feetPoint;
    public Transform playerModel;

    public UnityEvent OnLanding;
    private float horizontalInput;
    private float currentSpeed, airSpeed;

    private Rigidbody2D myRb;
    private Quaternion trunAngle;
    private bool jump, secondJump, doublJump, grounded, wasGrounded, movementLock, lookLeft;

    private void Start()
    {
        myRb = GetComponent<Rigidbody2D>();
        currentSpeed = speed;

        if (OnLanding == null)
            OnLanding = new UnityEvent();
    }

    private void Update()
    {
        if (!movementLock)
        {
            horizontalInput = Input.GetAxis("Horizontal");

            // Player's Rotation Update
            if (horizontalInput < 0)
            {
                trunAngle = Quaternion.Euler(new Vector3(playerModel.rotation.x, 180f, playerModel.rotation.z));
                lookLeft = true;
            }
            else if (horizontalInput > 0)
            {
                trunAngle = Quaternion.Euler(new Vector3(playerModel.rotation.x, 0f, playerModel.rotation.z));
                lookLeft = false;
            }

            #region Jumping
            //If player is not sliding and is on the ground then trigger a jump
            if (grounded && (Input.GetKeyDown(KeyCode.Space)))
            {
                jump = true;

                if (true)
                {
                    StopCoroutine(ActivateDoubleJump());
                    StartCoroutine(ActivateDoubleJump());
                }
            }

            if (true)
            {
                if (!grounded && doublJump && (Input.GetKeyDown(KeyCode.Space)))
                {
                    secondJump = true;
                    doublJump = false;
                }
            }
            #endregion
        }

        #region Crouch
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            myRb.velocity = Vector3.zero;
            movementLock = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftControl))
            movementLock = false;
        #endregion

        #region Dash
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (lookLeft)
                Dash(-transform.right, dashForce);
            if (!lookLeft)
                Dash(transform.right, dashForce);
        }
        #endregion
    }

    private void FixedUpdate()
    {
        if (!movementLock)
        {
            #region Physics Overlaps & Ground Checks
            wasGrounded = grounded;
            if (Physics2D.OverlapCircle(new Vector2(feetPoint.position.x, feetPoint.position.y), groundCheckRadius, groundLayers))
            {
                grounded = true;

                //If player was not grounded before that means he was in the air
                //so that results in him just landing and calling the on land event
                if (!wasGrounded)
                    OnLanding.Invoke();
            }
            else
            {
                grounded = false;

                //If player was grounded before then he just went air bound
                //so we do things that needed to do when player goes air bound
                if (wasGrounded)
                {
                    airSpeed = currentSpeed;
                    doublJump = true;
                }
            }
            #endregion

            Movement();
        }
    }

    //The function that handels all the player movement
    private void Movement()
    {
        Vector2 targetVelocity;

        playerModel.rotation = Quaternion.Slerp(playerModel.rotation, trunAngle, rotationSpeed * Time.deltaTime);

        //Add a force if the jump is triggered
        if (jump)
        {
            myRb.AddForce(new Vector2(0, jumpeForce), ForceMode2D.Impulse);
            jump = false;
        }

        //Add a force if the double jump is triggered
        if (secondJump)
        {
            myRb.velocity = new Vector2(myRb.velocity.x, 0);
            myRb.AddForce(new Vector2(0, secondJumpeForce), ForceMode2D.Impulse);
            secondJump = false;
        }

        //If player is on the ground use regular speed else use the air bound speed
        if (grounded)
            targetVelocity = new Vector2(horizontalInput * currentSpeed, myRb.velocity.y);
        else
            targetVelocity = new Vector2(horizontalInput * airSpeed, myRb.velocity.y);

        myRb.velocity = Vector2.Lerp(myRb.velocity, targetVelocity, lerpSpeed * Time.deltaTime);
    }

    //Function for activating double jump
    private IEnumerator ActivateDoubleJump()
    {
        yield return new WaitForSeconds(dJumpActivateGap);
        doublJump = true;
    }

    private void Dash(Vector3 dashDirection, float dashforce)
    {
        myRb.AddForce(dashDirection * dashforce, ForceMode2D.Impulse);        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(feetPoint.position, groundCheckRadius);

        Gizmos.color = Color.red;
    }
}