using Unity.VisualScripting;
using UnityEngine;

public class shopBehavior : MonoBehaviour
{
    public GameObject blackBackground;
    public GameObject item1button;
    public GameObject item2button;
    public GameObject price1;
    public GameObject price2;

    public GameObject shopText;
    public GameObject item1;
    public GameObject item2;

    public KeyCode interactKey = KeyCode.E;

    public GameObject player;
    private MovePlayer MovePlayer;


    private void Start()
    {
        MovePlayer = player.GetComponent<MovePlayer>();

        blackBackground.SetActive(false);
        item1button.SetActive(false);
        item2button.SetActive(false);
        shopText.SetActive(false);
        price1.SetActive(false);
        price2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(interactKey))
        {
            OpenShop();
        }

        CloseShop();
    }

    void OpenShop()
    {
        Time.timeScale = 0f;
        blackBackground.SetActive(true);
        item1button.SetActive(true);
        item2button.SetActive(true);
        shopText.SetActive(true);
        price1.SetActive(true);
        price2.SetActive(true);

    }

    void CloseShop()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            Time.timeScale = 1f;
            blackBackground.SetActive(false);
            item1button.SetActive(false);
            item2button.SetActive(false);
            shopText.SetActive(false);
            price1.SetActive(false);
            price2.SetActive(false);
        }
    }

    void spawnItems()
    {
        
    }
}
