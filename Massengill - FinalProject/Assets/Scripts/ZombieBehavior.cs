using Unity.VisualScripting;
using UnityEngine;

public class ZombieBehavior : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    public Collider Hitbox;
    bool inSight = false;

    public Animator anim;
    private Transform player;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inSight = true;
        }
    }

    void Attack()
    {
        if (inSight == true)
        {
            anim.SetBool("Walk", true);
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
    }
}
