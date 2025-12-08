using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel1 : MonoBehaviour
{
    public string sceneName = "Level1";

    public void LoadLevel1Scene()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(sceneName);
    }
}
