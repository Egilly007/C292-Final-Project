using Unity.VisualScripting;
using UnityEngine;

public class ZombieBehavior : MonoBehaviour
{
    public int health = 4;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float detectionRange = 0.5f;

    bool inSight = false;
    public Animator anim;
    private Transform playerpos;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerpos = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        DetectPlayer();
        Attack();
    }

    void DetectPlayer()
    {
        inSight = false; // reset every frame

        Vector2 direction = (playerpos.position - transform.position).normalized;
        Vector3 offset = new Vector3(-0.8f, 0f);

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
