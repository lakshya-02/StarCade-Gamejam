using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement parameters")]
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    public int jumpCounter = 0;
    private float horizontalInput;
    private float gravityScale = 1f;

    [Header("Layers")]
    [SerializeField] private LayerMask groundLayer;

    [Header("Refrences")]
    private Rigidbody2D body;
    private BoxCollider2D boxCollider;
    private Animator anim;
    private bool facingRight = true;
    private float originalScaleX;
    [SerializeField] private AudioClip JumpAudio;
    [SerializeField] private AudioClip RunAudio;
    private bool isRunning = false;
    private AudioSource runAudioSource;
    private AudioSource jumpAudioSource;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        
        // Store original scale magnitude
        originalScaleX = Mathf.Abs(transform.localScale.x);
        
        // Store initial gravity scale
        gravityScale = body.gravityScale;
        
        // Create a dedicated AudioSource for running sound
        runAudioSource = gameObject.AddComponent<AudioSource>();
        runAudioSource.loop = true;
        runAudioSource.playOnAwake = false;

        // Create a dedicated AudioSource for jumping sound
        jumpAudioSource = gameObject.AddComponent<AudioSource>();
        jumpAudioSource.loop = false;
        jumpAudioSource.playOnAwake = false;
    }

    private void Update()
    {
        // Get input from both WASD and Arrow keys
        horizontalInput = Input.GetAxis("Horizontal");
        
        // Flip gravity on mouse click
        if (Input.GetMouseButtonDown(0))
        {
            FlipGravity();
        }

        if (horizontalInput > 0.01f)
        {
            if (!facingRight)
            {
                transform.localScale = new Vector3(originalScaleX, transform.localScale.y, transform.localScale.z);
                facingRight = true;
            }
            body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y);
        }
        else if (horizontalInput < -0.01f)
        {
            if (facingRight)
            {
                transform.localScale = new Vector3(-originalScaleX, transform.localScale.y, transform.localScale.z);
                facingRight = false;
            }
            body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y);
        }

        anim.SetBool("run", horizontalInput != 0 && isGrounded());
        anim.SetBool("isGrounded", isGrounded());
        
        // Handle running audio
        bool shouldBeRunning = horizontalInput != 0 && isGrounded();
        if (shouldBeRunning && !isRunning)
        {
            if (RunAudio != null)
            {
                runAudioSource.clip = RunAudio;
                runAudioSource.Play();
            }
            isRunning = true;
        }
        else if (!shouldBeRunning && isRunning)
        {
            runAudioSource.Stop();
            isRunning = false;
        }
        
        if (isGrounded())
        {
            anim.SetBool("jump", false);
        }

        // Only Space key for jumping
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }
    private void Jump()
    {
        if (isGrounded())
        {
            jumpAudioSource.clip = JumpAudio;
            jumpAudioSource.Play();
            body.linearVelocity = new Vector2(body.linearVelocity.x, jumpPower);
            anim.SetBool("jump", true);
            jumpCounter++;
        }
    }
    
    private void FlipGravity()
    {
        gravityScale *= -1;
        body.gravityScale = gravityScale;
        
        // Flip the player sprite vertically
        transform.localScale = new Vector3(transform.localScale.x, -transform.localScale.y, transform.localScale.z);
    }
    
    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }
}