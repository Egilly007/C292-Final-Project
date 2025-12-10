using UnityEngine;

public class openDoor : MonoBehaviour
{
    public AudioClip openSound;
    public GameObject door;
    public GameObject door2;

    public KeyCode interactKey = KeyCode.E;

    bool playerInRange = false;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(interactKey))
        {
            if (door != null)
            {
                Destroy(door);
                AudioSource.PlayClipAtPoint(openSound, transform.position);
            }
            if (door2 != null)
            {
                Destroy(door2);
                AudioSource.PlayClipAtPoint(openSound, transform.position);
            }

            playerInRange = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
