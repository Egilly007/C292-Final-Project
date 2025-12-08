using Unity.VisualScripting;
using UnityEngine;

public class ZombieBehavior : MonoBehaviour
{
    public int health = 4;
    public float speed = 4f;
    public float detectionRange = 10f;
    private float playerenemydistance;
    bool inSight = false;
    bool facingRight;   

    Vector3 offset;

    //component refs
    public Animator anim;
    private Transform playerpos;
    private SpriteRenderer sr;

    public GameObject money1;
    public GameObject money2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        playerpos = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        playerenemydistance = playerpos.position.x - transform.position.x;
        if (playerenemydistance > 0)
        {
            sr.flipX = true;
            facingRight = false;

        }
        else
        {
            sr.flipX = false;
            facingRight = true;
        }

        DetectPlayer();
        Attack();
        FlipRaycast();
    }

    void DetectPlayer()
    {
        inSight = false; // reset every frame

        Vector2 direction = (playerpos.position - transform.position).normalized;

        RaycastHit2D hit = Physics2D.Raycast(transform.position + offset, direction, detectionRange);

        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Player"))
            {
                inSight = true;
            }
        }

        Debug.DrawRay(transform.position + offset, direction * detectionRange, inSight ? Color.green : Color.red);
    }


    void Attack()
    {
        if (inSight)
        {
            anim.SetBool("Walk", true);
            transform.position = Vector2.MoveTowards(transform.position, playerpos.position, speed * Time.deltaTime);
        }
        else
        {
            anim.SetBool("Walk", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var player = collision.gameObject.GetComponent<MovePlayer>();
            player.TakeDamage(1);
            anim.SetBool("Attack", true);
        }
        else
        {
            anim.SetBool("Attack", false);
        }
    }

    public void TakeDamage(int damage)
    {
        health--;
        if (health <= 0)
        {
            Destroy(gameObject);

            int dropAmmount = Random.Range(1, 3);
            int dropChance = Random.Range(1, 2);

            if (dropChance == 1)
            {
                for (int i = 0; i < dropAmmount; i++)
                {
                    Instantiate(money1, transform.position, Quaternion.identity);
                }
            }
            else
            {
                for (int i = 0; i < dropAmmount; i++)
                {
                    Instantiate(money2, transform.position, Quaternion.identity);
                }
            }

        }
    }

    void FlipRaycast()
    {
        if (!facingRight)
        {
            offset = new Vector3(0.8f, 0f);
        }
        else
        {
            offset = new Vector3(-0.8f, 0f);
        }
    }
}
