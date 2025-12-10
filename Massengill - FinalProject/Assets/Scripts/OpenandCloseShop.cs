using UnityEngine;

public class OpenandCloseShop : MonoBehaviour
{
    public GameObject blackBackground;
    public GameObject item1button;
    public GameObject item2button;
    public GameObject price1;
    public GameObject price2;
    public GameObject shopText;

    public KeyCode interactKey = KeyCode.R;
    public KeyCode closeKey = KeyCode.Q;

    public GameObject player;
    private MovePlayer movePlayer;

    bool playerInRange = false;
    bool shopOpen = false;

    void Start()
    {
        blackBackground.SetActive(false);
        item1button.SetActive(false);
        item2button.SetActive(false);
        price1.SetActive(false);
        price2.SetActive(false);
        shopText.SetActive(false);

        movePlayer = player.GetComponent<MovePlayer>();
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(interactKey))
        {
            ToggleShop();
        }

        if (shopOpen && Input.GetKeyDown(closeKey))
        {
            CloseShop();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
            player = collision.gameObject;
            movePlayer = player.GetComponent<MovePlayer>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;

            if (shopOpen)
                CloseShop();

            player = null;
            movePlayer = null;
        }
    }

    void ToggleShop()
    {
        if (shopOpen) CloseShop();
        else OpenShop();
    }

    void OpenShop()
    {
        Time.timeScale = 0f;
        shopOpen = true;

        blackBackground.SetActive(true);
        item1button.SetActive(true);
        item2button.SetActive(true);
        price1.SetActive(true);
        price2.SetActive(true);
        shopText.SetActive(true);

        movePlayer.enabled = false;
    }

    void CloseShop()
    {
        Time.timeScale = 1f;
        shopOpen = false;

        blackBackground.SetActive(false);
        item1button.SetActive(false);
        item2button.SetActive(false);
        price1.SetActive(false);
        price2.SetActive(false);
        shopText.SetActive(false);

        movePlayer.enabled = true;
    }
}
