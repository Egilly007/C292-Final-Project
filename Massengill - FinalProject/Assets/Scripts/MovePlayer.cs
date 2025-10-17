using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 0.5f;
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    void anims()
    {
        if (Input.GetKeyDown(KeyCode.D))
        { 
            anim.SetTrigger("Run");
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            anim.SetTrigger("Run");
            flipSprite();
        }
        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A))
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
}
