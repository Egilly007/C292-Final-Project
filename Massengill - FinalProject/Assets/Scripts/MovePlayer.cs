using UnityEngine;
using UnityEngine.InputSystem;

public class MovePlayer : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float jumpForce = 7f;
    public float maxSpeed = 4f;
    public bool velocityCapEnabled = true;
    public bool canJump = false;

    public float fireCooldown = 0.25f;
    float lastFireTime;

    public GameObject spawnpoint;
    public GameObject bullet;

    public Animator anim;

    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;

    bool facingRight = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movement();
        anims();
        capVelocity();
        Shoot();
    }

    void movement()
    {
        if (Input.GetKey(KeyCode.D))
        { 
            rb.AddForce(Vector2.right * moveSpeed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(Vector2.left * moveSpeed);
        }
        if (Input.GetKeyDown(KeyCode.Space) && canJump == true)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            canJump = false;
            anim.SetBool("Jumping", true);
        }
    }

    void anims()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (canJump)
            {
                anim.SetTrigger("Run");
            }
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            anim.SetTrigger("Run");
            flipSprite();
        }
        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A) || !canJump)
        {
            anim.SetTrigger("Backtoidle");
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            flipSprite();
        }
    }

    void flipSprite()
    {
        facingRight = !facingRight;
        spriteRenderer.flipX = !spriteRenderer.flipX;
    }

    void capVelocity()
    {
        if (rb.linearVelocityX > maxSpeed)
        {
            float cappedXVelocity = Mathf.Min(rb.linearVelocityX, maxSpeed);
            rb.linearVelocityX = cappedXVelocity;
        }
        if (rb.linearVelocityX < -maxSpeed)
        {
            float cappedXVelocity = Mathf.Max(rb.linearVelocityX, -maxSpeed);
            rb.linearVelocityX = cappedXVelocity;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            canJump = true;
            anim.SetBool("Jumping", false);
        }
    }

    void Shoot()
    {
        if(Input.GetMouseButton(0) && Time.time - lastFireTime >= fireCooldown)
        {
            Vector2 spawnpos = spawnpoint.transform.position;
            Instantiate(bullet, spawnpos, Quaternion.identity);
            lastFireTime = Time.time;
        }
    }
}
