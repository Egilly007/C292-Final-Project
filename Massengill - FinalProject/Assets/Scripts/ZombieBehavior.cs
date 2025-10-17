using UnityEngine;

public class ZombieBehavior : MonoBehaviour
{ 
    CircleCollider2D sight;
    Animator anim;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player in sight");
            anim.SetTrigger("Attack");
        }
    }
}
