using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovePlayer : MonoBehaviour
{
    int money = 0;

    public int health = 5;
    public float moveSpeed = 10f;
    public float jumpForce = 7f;
    public float maxSpeed = 4f;
    public float fireRange = 7f;
    public float fireCooldown = 0.25f;
    float lastFireTime;

    public bool velocityCapEnabled = true;
    public bool canJump = true;
    public bool flipped = false;
    public RaycastHit2D lastHit;

    public GameObject spawnpoint;
    public Animator anim;

    public LineRenderer lineRenderer;
    public float lineDuration = 0.5f;
    public Color lineColor = Color.red;
    public float lineWidth = 0.05f;

    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;

    bool facingRight = true;
    bool hasIncremented = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        if (lineRenderer != null)
        {
            lineRenderer.positionCount = 0;
            lineRenderer.enabled = false;
        }
    }

    void Update()
    {
        movement();
        anims();
        capVelocity();
        Shoot();
        Debug.Log(money);
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

            Vector3 lineEnd;
            if (lastHit.collider != null)
            {
                lineEnd = lastHit.point;
                Debug.DrawLine(origin, lastHit.point, Color.red, 0.5f);
                Debug.Log("Hit: " + lastHit.collider.name);

                var explosive = lastHit.collider.GetComponent<explosiveBehavior>();
                var destructible = lastHit.collider.GetComponent<NormalCrateBehavior>();
                var enemy = lastHit.collider.GetComponent<ZombieBehavior>();

                if (explosive != null)
                {
                    explosive.TriggerExplosion();
                    Destroy(lastHit.collider.gameObject);
                }

                if(destructible != null)
                {
                    destructible.TakeDamage(1);
                }

                if(enemy != null)
                {
                    enemy.TakeDamage(1);
                }
            }
            else
            {
                lineEnd = (Vector2)origin + fireDirection * fireRange;
                Debug.DrawLine(origin, origin + fireDirection * fireRange, Color.red, 1f);
                Debug.Log("No Hit");
            }

            StartCoroutine(ShowShotLine(origin, lineEnd, lineDuration));

            lastFireTime = Time.time;
        }
    }

    IEnumerator ShowShotLine(Vector3 start, Vector3 end, float duration)
    {
        if (lineRenderer != null)
        {
            lineRenderer.enabled = true;
            lineRenderer.positionCount = 2;
            lineRenderer.startWidth = lineWidth;
            lineRenderer.endWidth = lineWidth;
            lineRenderer.startColor = lineColor;
            lineRenderer.endColor = lineColor;
            lineRenderer.SetPosition(0, start);
            lineRenderer.SetPosition(1, end);

            yield return new WaitForSecondsRealtime(duration);

            lineRenderer.enabled = false;
            lineRenderer.positionCount = 0;
        }
        else
        {
            var go = new GameObject("TempShotLine");
            var lr = go.AddComponent<LineRenderer>();

            lr.material = new Material(Shader.Find("Sprites/Default"));

            lr.startWidth = lineWidth;
            lr.endWidth = lineWidth;
            lr.startColor = lineColor;
            lr.endColor = lineColor;
            lr.positionCount = 2;
            lr.useWorldSpace = true;

            lr.SetPosition(0, start);
            lr.SetPosition(1, end);
            lr.numCapVertices = 2;

            yield return new WaitForSecondsRealtime(duration);

            Destroy(go);
        }
    }

    public void TakeDamage(int damage)
    {
        int rand = Random.Range(0, 3);
        health -= damage;
        Debug.Log("Player has" + health);

        if (health <= 0)
        {
            Debug.Log("Player Died");
            //Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Money"))
        {
            if (!hasIncremented)
            {
                money++;
                hasIncremented = true;
            }
            Debug.Log("Player Money:" + money);
            Destroy(collision.gameObject);
        }
    }
}
