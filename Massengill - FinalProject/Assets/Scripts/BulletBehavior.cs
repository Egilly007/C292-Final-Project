using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public float speed = 20f;
    public float lifetime = 2f;

    // set by the spawner (player)
    [HideInInspector] public bool facingRight = true;

    void Start()
    {
        if (lifetime > 0f)
            Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // Move horizontally according to facingRight (right when true, left when false).
        float dir = facingRight ? 1f : -1f;
        transform.Translate(Vector2.right * speed * dir * Time.deltaTime, Space.World);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
