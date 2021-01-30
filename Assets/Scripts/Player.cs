using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("Health System")]
    [SerializeField] float maxHealth = 1200;
    public float healthDcayRate = -3;
    [SerializeField] Slider slider;

    [Header("Movement")]
    public float speed = 5f;
    [SerializeField] float rotationSpeed = 10;
    [SerializeField] float lerpSpeed = 10;

    [Header("Jump")]
    public bool jumpenni;
    public float jumpeForce = 5f;
    public float secondJumpeForce = 5f;
    [SerializeField] float dJumpActivateGap = 1f;
    [SerializeField] ParticleSystem jumpEffect;

    [Header("Dash")]
    public bool dasher;
    [SerializeField] float dashGap = 1f;
    public float dashForce = 5f;
    [SerializeField] ParticleSystem dashEffect;

    [Header("Other Requirements")]
    [Range(0, 0.5f)] [SerializeField] float groundCheckRadius = 0.2f;
    [SerializeField] LayerMask groundLayers;
    [SerializeField] Transform feetPoint;
    public Transform playerModel;
    public UnityEvent OnLanding;

    [Header("Audio")]
    public AudioSource playerSFXSRC;
    [SerializeField] AudioClip jumpSound;
    [SerializeField] AudioClip dashSound;

    private float currentHealth;
    private float horizontalInput;
    private float currentSpeed, airSpeed;
    private Image fill;
    private Color fillcolor;
    private Rigidbody2D myRb;
    private Quaternion trunAngle;
    private bool jump, secondJump, doublJump, canDash, dash, grounded, wasGrounded, movementLock, lookLeft;

    private void Start() {
        myRb = GetComponent<Rigidbody2D>();
        canDash = true;
        currentSpeed = speed;
        currentHealth = maxHealth;
        slider.value = slider.maxValue;
        fill = slider.fillRect.gameObject.GetComponent<Image>();
        fillcolor = fill.color;

        if (OnLanding == null)
            OnLanding = new UnityEvent();

        //movementLock = true;
    }

    private void Update()
    {
        HealthSystem();
        if (!movementLock) {
            horizontalInput = Input.GetAxis("Horizontal");

            // Player's Rotation Update
            if (horizontalInput < 0) {
                trunAngle = Quaternion.Euler(new Vector3(playerModel.rotation.x, 0f, playerModel.rotation.z));
                lookLeft = true;
            } else if (horizontalInput > 0) {
                trunAngle = Quaternion.Euler(new Vector3(playerModel.rotation.x, 180f, playerModel.rotation.z));
                lookLeft = false;
            }

            #region Jumping
            //If player is not sliding and is on the ground then trigger a jump
            if (grounded && (Input.GetKeyDown(KeyCode.Space))) {
                jump = true;
                
                playerSFXSRC.PlayOneShot(jumpSound);
                jumpEffect.Stop();
                jumpEffect.Clear();
                jumpEffect.Play();

                if (jumpenni) {
                    StopCoroutine(ActivateDoubleJump());
                    StartCoroutine(ActivateDoubleJump());
                }
            }

            if (jumpenni) {
                if (!grounded && doublJump && (Input.GetKeyDown(KeyCode.Space))) {
                    secondJump = true;
                    playerSFXSRC.PlayOneShot(jumpSound);
                    jumpEffect.Stop();
                    jumpEffect.Play();
                    doublJump = false;
                }
            }
            #endregion
        }

        #region Crouch
        if (Input.GetKeyDown(KeyCode.LeftControl)) {
            myRb.velocity = Vector3.zero;
            movementLock = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftControl))
            movementLock = false;
        #endregion

        #region Dash
        if(dasher) {
            if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
            {
                playerSFXSRC.PlayOneShot(dashSound);
                dashEffect.Stop();
                dashEffect.Clear();
                dashEffect.Play();
                dash = true;
            }
        }
        #endregion
    }

    private void FixedUpdate() {
        if (!movementLock) {
            #region Physics Overlaps & Ground Checks
            wasGrounded = grounded;
            if (Physics2D.OverlapCircle(new Vector2(feetPoint.position.x, feetPoint.position.y), groundCheckRadius, groundLayers)) {
                grounded = true;

                //If player was not grounded before that means he was in the air
                //so that results in him just landing and calling the on land event
                if (!wasGrounded)
                    OnLanding.Invoke();
            } else {
                grounded = false;

                //If player was grounded before then he just went air bound
                //so we do things that needed to do when player goes air bound
                if (wasGrounded) {
                    airSpeed = currentSpeed;
                    doublJump = true;
                }
            }
            #endregion

            Movement();
        }
    }

    private void HealthSystem() {
        if(currentHealth > 0) {
            if(movementLock)
                currentHealth += Time.deltaTime * healthDcayRate * 0.4f;
            else
                currentHealth += Time.deltaTime * healthDcayRate;
            currentHealth = Mathf.Clamp(currentHealth, -3, maxHealth);
            float barValue = Mathf.InverseLerp(0, maxHealth, currentHealth);
            slider.value = barValue;
            fillcolor.r = 1 - barValue;
            fillcolor.g = barValue;
            fill.color = fillcolor;
        }
        else
            GameManager.instance.GameOver();
    }

    public void Damaged(float dmage) {
        currentHealth -= dmage;
    }

    //The function that handels all the player movement
    private void Movement() {
        Vector2 targetVelocity;

        playerModel.rotation = Quaternion.Slerp(playerModel.rotation, trunAngle, rotationSpeed * Time.deltaTime);

        //Add a force if the jump is triggered
        if (jump) {
            myRb.AddForce(new Vector2(0, jumpeForce), ForceMode2D.Impulse);
            jump = false;
        }

        //Add a force if the double jump is triggered
        if (secondJump) {
            myRb.velocity = new Vector2(myRb.velocity.x, 0);
            myRb.AddForce(new Vector2(0, secondJumpeForce), ForceMode2D.Impulse);
            secondJump = false;
        }

        //Add a force if the dash is triggered
        if(dash) {
            dash = false;
            if (lookLeft)
                Dash(-transform.right);
            if (!lookLeft)
                Dash(transform.right);
        }

        //If player is on the ground use regular speed else use the air bound speed
        if (grounded)
            targetVelocity = new Vector2(horizontalInput * currentSpeed, myRb.velocity.y);
        else
            targetVelocity = new Vector2(horizontalInput * airSpeed, myRb.velocity.y);

        myRb.velocity = Vector2.Lerp(myRb.velocity, targetVelocity, lerpSpeed * Time.deltaTime);
    }

    //Function that makes the player dash in given direction
    private void Dash(Vector3 dashDirection)
    {
        myRb.AddForce(dashDirection * dashForce, ForceMode2D.Impulse);
        canDash = false;
        StartCoroutine(DashCooldown());
    }

    //Function for activating double jump
    private IEnumerator ActivateDoubleJump() {
        yield return new WaitForSeconds(dJumpActivateGap);
        doublJump = true;
    }

    private IEnumerator DashCooldown()
    {
        yield return new WaitForSeconds(dashGap);
        canDash = true;
    }

    public void LockMovement()
    {
        myRb.velocity = Vector3.zero;
        movementLock = true;
    }

    public void UnlockMovement()
    {
        movementLock = false;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(feetPoint.position, groundCheckRadius);

        Gizmos.color = Color.red;
    }
}