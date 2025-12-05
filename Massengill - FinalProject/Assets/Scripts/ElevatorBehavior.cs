using System.Collections;
using UnityEngine;

public class ElevatorBehavior : MonoBehaviour
{
    public Collider2D trigger;

    public float rayDistance = 50f;
    public float delayBeforeMove = 3f;
    public float arriveYOffset = 0.5f;

    public RaycastHit2D lastHit;

    Vector3 offset = new Vector3(0, 0, -50);

    public Transform playerTransform;

    Coroutine moveCoroutine;

    void Update()
    {
        Debug.DrawRay(transform.position + offset, Vector2.down * rayDistance, Color.cyan);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        playerTransform = collision.transform;

        Vector2 origin = (Vector2)(transform.position + offset);
        lastHit = Physics2D.Raycast(origin, Vector2.down, rayDistance);

        if (moveCoroutine != null)
            StopCoroutine(moveCoroutine);

        moveCoroutine = StartCoroutine(MovePlayerAfterDelay(playerTransform, lastHit.point, delayBeforeMove));
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
            moveCoroutine = null;
        }

        playerTransform = null;
    }

    IEnumerator MovePlayerAfterDelay(Transform player, Vector2 groundPoint, float delay)
    {
        yield return new WaitForSeconds(delay);

        if (player == null)
            yield break;

        Vector2 origin = (Vector2)(transform.position + offset);
        lastHit = Physics2D.Raycast(origin, Vector2.down, rayDistance);
        Vector2 destination = lastHit.collider != null ? lastHit.point : groundPoint;

        Vector3 targetPos = new Vector3(destination.x, destination.y + arriveYOffset, player.position.z);

        var rb = player.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }

        player.position = targetPos;

        moveCoroutine = null;
    }
}
