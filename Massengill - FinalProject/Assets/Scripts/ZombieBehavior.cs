using Unity.VisualScripting;
using UnityEngine;

public class ZombieBehavior : MonoBehaviour
{
    public int health = 4;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float detectionRange = 0.5f;
    Vector3 offset = new Vector3(-0.8f, 0f);

    bool inSight = false;
    public Animator anim;
    private Transform playerpos;
    private SpriteRenderer sr;
    private float playerenemydistance;

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

        }
        else
        {
            sr.flipX = false;
        }

        DetectPlayer();
        Attack();
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
                Debug.Log("Player detected");
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

    public void TakeDamage(int damage)
    {
        health--;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
