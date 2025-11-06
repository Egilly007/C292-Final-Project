using Unity.VisualScripting;
using UnityEngine;

public class ZombieBehavior : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float detectionRange = 10f;

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
        Vector2 direction = (playerpos.position - transform.position).normalized;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, detectionRange);

        if(hit.collider != null && hit.collider.CompareTag("Player"))
        {
            inSight = true;
        }
        else
        {
            inSight = false;
        }

        Debug.DrawRay(transform.position, direction * detectionRange, inSight ? Color.green : Color.red);
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
}
