using UnityEngine;
using UnityEngine.SceneManagement;

public class ShowLevelComplete : MonoBehaviour
{
    public Collider2D trigger;
    public GameObject levelCompleteText;
    public GameObject backButton;
    public GameObject blackOverlay;
    public MovePlayer player;

    public string mainMenuSceneName = "Main Menu";

    public bool pauseOnLevelComplete = true;
    bool levelCompleteShown = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (levelCompleteText != null) levelCompleteText.SetActive(false);
        if (backButton != null) backButton.SetActive(false);

        if (player == null)
        {
            var playerGO = GameObject.FindGameObjectWithTag("Player");
            if (playerGO != null) player = playerGO.GetComponent<MovePlayer>();
        }
    }

    void Update()
    {
        if (levelCompleteShown) return;

        if (player == null)
        {
            var playerGO = GameObject.FindGameObjectWithTag("Player");
            if (playerGO != null)
            {
                player = playerGO.GetComponent<MovePlayer>();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (levelCompleteShown) return;
        ShowLevelCompleted();
    }

    public void ShowLevelCompleted()
    {
        if (levelCompleteShown) return;

        if (levelCompleteText != null) levelCompleteText.SetActive(true);
        if (blackOverlay != null) blackOverlay.SetActive(true);
        if (backButton != null) backButton.SetActive(true);

        if (pauseOnLevelComplete) Time.timeScale = 0f;

        if (player != null)
        {
            player.enabled = false;
        }

        levelCompleteShown = true;
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuSceneName);
    }
}
