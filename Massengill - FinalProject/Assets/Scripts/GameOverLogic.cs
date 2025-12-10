using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverLogic : MonoBehaviour
{
    public GameObject blackOverlay;
    public GameObject gameOverScreen;
    public GameObject restartButton;
    public MovePlayer player;

    public bool pauseOnGameOver = true;
    bool gameOverShown = false;

    void Start()
    {
        if (restartButton != null) restartButton.SetActive(false);
        if (blackOverlay != null) blackOverlay.SetActive(false);
        if (gameOverScreen != null) gameOverScreen.SetActive(false);

        if (player == null)
        {
            var playerGO = GameObject.FindGameObjectWithTag("Player");
            if (playerGO != null) player = playerGO.GetComponent<MovePlayer>();
        }
    }

    void Update()
    {
        if (gameOverShown) return;

        if (player == null)
        {
            var playerGO = GameObject.FindGameObjectWithTag("Player");
            if (playerGO != null)
            {
                player = playerGO.GetComponent<MovePlayer>();
            }

            if (player == null)
            {
                ShowGameOver();
                return;
            }
        }

        if (player != null && player.Health <= 0)
        {
            ShowGameOver();
        }
    }

    public void ShowGameOver()
    {
        if (gameOverShown) return;

        if (restartButton != null) restartButton.SetActive(true);
        if (blackOverlay != null) blackOverlay.SetActive(true);
        if (gameOverScreen != null) gameOverScreen.SetActive(true);

        if (pauseOnGameOver) Time.timeScale = 0f;

        if (player != null)
        {
            player.enabled = false;
        }

        gameOverShown = true;
    }

    // Call this from the UI Button's OnClick to restart the current level.
    public void RestartLevel()
    {
        // Ensure time is unpaused before reloading
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
