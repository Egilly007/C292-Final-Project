using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel2 : MonoBehaviour
{
    public string sceneName = "Level2";

    public void LoadLevel2Scene()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(sceneName);
    }
}
