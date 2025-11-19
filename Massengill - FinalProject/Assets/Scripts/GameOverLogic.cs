using UnityEngine;

public class GameOverLogic : MonoBehaviour
{
    public GameObject blackOverlay;
    public GameObject gameOverScreen;
    public MovePlayer player;

    public bool pauseOnGameOver = true;
    bool gameOverShown = false;

    void Start()
    {
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

        if (player != null && player.health <= 0)
        {
            ShowGameOver();
        }
    }

    public void ShowGameOver()
    {
        if (gameOverShown) return;

        if (blackOverlay != null) blackOverlay.SetActive(true);
        if (gameOverScreen != null) gameOverScreen.SetActive(true);

        if (pauseOnGameOver) Time.timeScale = 0f;

        if (player != null)
        {
            player.enabled = false;
        }

        gameOverShown = true;
    }
}
