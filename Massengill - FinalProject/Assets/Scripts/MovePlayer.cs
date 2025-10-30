using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovePlayer : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float jumpForce = 7f;
    public float maxSpeed = 4f;
    public float fireRange = 7f;    
    public bool velocityCapEnabled = true;
    public bool canJump = true;
    [HideInInspector] public bool flipped = false;
    public RaycastHit2D lastHit;

    public float fireCooldown = 0.25f;
    float lastFireTime;

    public GameObject spawnpoint;

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
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            canJump = false;
            anim.SetBool("Jumping", false);
        }
        if (!canJump)
        {
            anim.SetBool("Jumping", true);
        }
    }

    void anims()
    {
        if (Input.GetKeyDown(KeyCode.D) && canJump)
        {
            anim.SetBool("Running", true);
        }
        if (Input.GetKeyDown(KeyCode.A) && canJump)
        {
            anim.SetBool("Running", true);
        }
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            anim.SetBool("Running", false);
        }
        if (facingRight && Input.GetKey(KeyCode.A))
        {
            flipSprite();
        }
        if (!facingRight && Input.GetKey(KeyCode.D))
        {
            flipSprite();
        }
    }

    void flipSprite()
    {
        facingRight = !facingRight;
        spriteRenderer.flipX = !spriteRenderer.flipX;

        float spawnposx = spawnpoint.transform.localPosition.x;
        spawnposx = -spawnposx;
        spawnpoint.transform.localPosition = new Vector2(spawnposx, spawnpoint.transform.localPosition.y);
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
        if (Input.GetMouseButton(0) && Time.time - lastFireTime >= fireCooldown)
        {
            Vector2 origin = spawnpoint.transform.position;
            Vector2 fireDirection = facingRight ? Vector2.right : Vector2.left;

            lastHit = Physics2D.Raycast(origin, fireDirection, fireRange);

            if (lastHit.collider != null)
            {
                Debug.DrawLine(origin, lastHit.point, Color.red, 0.5f);
                Debug.Log("Hit: " + lastHit.collider.name);
            }
            else
            {
                Debug.DrawLine(origin, origin + fireDirection * fireRange, Color.red, 1f);
                Debug.Log("No Hit");
            }

            lastFireTime = Time.time;
        }

        if (lastHit.collider.CompareTag("Distructable") || lastHit.collider.CompareTag("Enemy"))
        {
            Destroy(lastHit.collider.gameObject);
        }
    }
}
