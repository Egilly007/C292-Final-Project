using System.Collections;
using UnityEngine;

public class explosiveBehavior : MonoBehaviour
{
    public float delay = 5f;
    public float radius = 3f;
    public int damage = 5;
    public LayerMask damageMask = ~0;
    public GameObject explosionVFX;
    public float explosionForce = 40f;
    bool triggered = false;

    public AudioClip explosionSound;

    void Start()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!triggered)
        {
            StartCoroutine(ExplodeAfterDelay());
            triggered = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!triggered)
        {
            StartCoroutine(ExplodeAfterDelay());
            triggered = true;
        }
    }

    IEnumerator ExplodeAfterDelay()
    {
        yield return new WaitForSeconds(delay);
        TriggerExplosion();
    }

    public void TriggerExplosion()
    {
        AudioSource.PlayClipAtPoint(explosionSound, transform.position);
        if (explosionVFX != null)
            Instantiate(explosionVFX, transform.position, Quaternion.identity);

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radius, damageMask);

        foreach (var hit in hits)
        {
            var go = hit.gameObject;

            var player = go.GetComponent<MovePlayer>();
            if (player != null)
            {
                player.TakeDamage(damage);
            }
            else
            {
                var crate = go.GetComponent<NormalCrateBehavior>();
                if (crate != null)
                {
                    crate.TakeDamage(damage);
                }
                else
                {
                    go.SendMessage("TakeDamage", damage, SendMessageOptions.DontRequireReceiver);
                }
            }

            var rb = hit.attachedRigidbody;
            if (rb != null && explosionForce != 0f)
            {
                Vector2 dir = (rb.position - (Vector2)transform.position).normalized;
                rb.AddForce(dir * explosionForce, ForceMode2D.Impulse);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
