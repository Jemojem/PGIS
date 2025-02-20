using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] float walkSpeed;
    [SerializeField] float dashSpeed;
    [SerializeField] float dashDuration;
    [SerializeField] float jumpSpeed;
    [SerializeField] LayerMask groundMask;
    [SerializeField] LayerMask enemyMask;

    [Header("References")]
    [SerializeField] Collider2D feet;

    [Header("Events")]
    [SerializeField] UnityEvent onDeath;

    [Header("Sounds")]
    [SerializeField] AudioClip jumpSound;
    [SerializeField] AudioClip dashSound;
    [SerializeField] AudioClip deathSound;


    private readonly int Walk = Animator.StringToHash(nameof(Walk));
    private readonly int Jump = Animator.StringToHash(nameof(Jump));
    private readonly int Dash = Animator.StringToHash(nameof(Dash));
    private readonly int Fall = Animator.StringToHash(nameof(Fall));
    private readonly int Death = Animator.StringToHash(nameof(Death));

    private float dashTimer;
    private bool isGrounded = false;
    private bool canDash = true;
    private bool isJumping, isDashing;
    private float horizontalInput, verticalInput;
    private int currentAnimation = 0;

    SpriteRenderer sprite;
    Animator animator;
    new Rigidbody2D rigidbody;
    AudioSource audioSource;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        dashTimer = 0;
    }

    private void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        isJumping = Input.GetKeyDown(KeyCode.Space);
        isDashing = Input.GetKeyDown(KeyCode.LeftShift);

        if (isDashing && !IsInDash() && canDash)
        {
            canDash = false;
            PlayAnimation(Dash);
            audioSource.PlayOneShot(dashSound);
            dashTimer = dashDuration;
        }
        else
        {
            if (!IsInDash())
            {
                rigidbody.velocity = new Vector2(horizontalInput * walkSpeed, rigidbody.velocity.y);
            }
            else
            {
                dashTimer -= Time.deltaTime;
                rigidbody.velocity = new Vector2((sprite.flipX ? -1f : 1f) * dashSpeed, 0);
            }
        }

        if(!IsInDash())
        {
            if (horizontalInput != 0)
            {
                sprite.flipX = horizontalInput < 0f;
            }

            if (isGrounded)
            {   
                if (currentAnimation != Jump || (!isGrounded && currentAnimation == Jump))
                    PlayAnimation(Walk);
            }
            else
            {
                if (rigidbody.velocity.y <= 0f)
                    PlayAnimation(Fall);
            }

            if (isJumping && isGrounded)
            {
                rigidbody.velocity += Vector2.up * jumpSpeed;
                PlayAnimation(Jump);
                audioSource.PlayOneShot(jumpSound);
            }
        }  
    }

    private bool IsInDash()
    {
        return dashTimer > 0f;
    }

    private void FixedUpdate()
    {
        if (Physics2D.OverlapBox(feet.bounds.center, feet.bounds.size * 0.5f, 0, groundMask.value) != null)
        {
            isGrounded = true;
            canDash = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    public void Disable()
    {
        enabled = false;
        rigidbody.velocity = Vector2.zero;
        rigidbody.gravityScale = 0f;
    }

    private void PlayAnimation(int index)
    {
        if(currentAnimation != index)
        {
            animator.SetTrigger(index);
            currentAnimation = index;
        } 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if((enemyMask.value & (1 << collision.gameObject.layer)) != 0)
        {
            PlayAnimation(Death);
            onDeath.Invoke();
            audioSource.PlayOneShot(deathSound);
            Disable();
        }
    }
}
